using Microsoft.EntityFrameworkCore;
using SV.RDW.Data.Entities;
using SV.RDW.Data.Entities.ImportJson;
using SV.RDW.Data.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SV.RDW.Apps.Import
{
    public class ImportRoutine
    {

        private readonly BaseContext _baseContext;
        
        public ImportRoutine(BaseContext baseContext)
        {
            _baseContext = baseContext;
        }

        public async Task<int> Run()
        {
            var rdwClient = new RdwHttpClient();

            if (_baseContext.Voertuigen.Count() > 1000000)
            {
                return 0;
            }
            var counter = 0;
            var runtimer = DateTime.Now;

            var importdate = DateTime.Today.AddDays(-7);

            while (DateTime.Now.Subtract(runtimer).TotalMinutes <= 2)
            {
                if (_baseContext.Imports.Any(x => x.EersteToelatingDatum == importdate))
                {
                    importdate = importdate.AddDays(-1);
                }
                else
                {
                    var timer = DateTime.Now;
                    var vehicles = await rdwClient.GetVehicles(importdate);
                    var merken = vehicles.GetMerken();
                    await SaveMerken(merken);
                    var soorten = vehicles.GetVoertuigSoorten();
                    await SaveVoertuigSoorten(soorten);
                    var handelsbenamingen = vehicles.GetHandelsbenamingen();
                    await SaveHandelsbenamingen(handelsbenamingen);

                    var voertuigen = await SaveVoertuigen(vehicles);

                    await _baseContext.Imports.AddAsync(new Data.Entities.Import
                    {
                        Id = 0,
                        EersteToelatingDatum = importdate,
                        ImportSeconden = Convert.ToDecimal(DateTime.Now.Subtract(timer).TotalSeconds),
                        TotaalImport = voertuigen.Count(),
                        Voertuigen = voertuigen.ToHashSet<Data.Entities.Voertuig>()
                    });
                    await _baseContext.SaveChangesAsync();

                    counter += voertuigen.Count();

                    importdate = importdate.AddDays(-1);
                }
            }

            rdwClient.Dispose();
            return counter;
        }

        protected async Task SaveMerken(IEnumerable<string> merken) 
        {
            foreach(var merk in merken)
            {
                if (_baseContext.Merken.All(x => x.Naam != merk.ToUpper()))
                {
                    await _baseContext.Merken.AddAsync(new Merk
                    {
                        Id = 0,
                        Naam = merk.ToUpper()
                    });
                }
            }
            await _baseContext.SaveChangesAsync();
        }
        
        protected async Task SaveVoertuigSoorten(IEnumerable<string> voertuigsoorten)
        {
            foreach(var voertuigsoort in voertuigsoorten)
            {
                if (_baseContext.VoertuigSoorten.All(x => x.Naam != voertuigsoort))
                {
                    await _baseContext.VoertuigSoorten.AddAsync(new VoertuigSoort
                    {
                        Id = 0,
                        Naam = voertuigsoort
                    });
                }
            }
            await _baseContext.SaveChangesAsync();
        }

        protected async Task SaveHandelsbenamingen(IEnumerable<(string merk, string handelsbenaming)> handelsbenamingen)
        {
            var table = _baseContext.Handelsbenamingen.Include(x => x.Merk);
            foreach(var (merk, handelsbenaming) in handelsbenamingen)
            {
                if (!table.Any(x => x.Naam == handelsbenaming.ToUpper() && x.Merk.Naam == merk.ToUpper()))
                {
                    var merkEntity = _baseContext.Merken.Single(x => x.Naam == merk.ToUpper());
                    await _baseContext.Handelsbenamingen.AddAsync(new Handelsbenaming
                    {
                        Id = 0,
                        Naam = handelsbenaming.ToUpper(),
                        MerkId = merkEntity.Id
                    });
                }
            }
            await _baseContext.SaveChangesAsync();
        }

        protected async Task<IEnumerable<Data.Entities.Voertuig>> SaveVoertuigen(IEnumerable<Data.Entities.ImportJson.Voertuig> voertuigen)
        {
            var list = new List<Data.Entities.Voertuig>();
            foreach(var voertuigImport in voertuigen)
            {
                var merkId = _baseContext.Merken.Single(x => x.Naam == voertuigImport.merk.ToUpper()).Id;
                var handelsbenamingId = _baseContext.Handelsbenamingen.SingleOrDefault(x => x.Naam == voertuigImport.handelsbenaming.ToUpper() && x.MerkId == merkId)?.Id;
                var voertuigSoortId = _baseContext.VoertuigSoorten.SingleOrDefault(x => x.Naam == voertuigImport.voertuigsoort)?.Id;
                _ = decimal.TryParse(voertuigImport.massa_ledig_voertuig, out var massaLedig);

                var voertuig = new Data.Entities.Voertuig
                {
                    EersteToelating = GetDatum(voertuigImport.datum_eerste_toelating),
                    Inrichting = voertuigImport.inrichting,
                    Kenteken = voertuigImport.kenteken,
                    Kleur = voertuigImport.eerste_kleur,
                    MassaLedig = massaLedig,
                    Tenaamstelling = GetDatum(voertuigImport.datum_tenaamstelling),
                    VervalDatumAPK = GetDatum(voertuigImport.vervaldatum_apk),
                    MerkId = merkId,
                    HandelsbenamingId = handelsbenamingId,
                    VoertuigSoortId = voertuigSoortId,
                };
                _baseContext.Voertuigen.Add(voertuig);
                list.Add(voertuig);
            }
            await _baseContext.SaveChangesAsync();
            return list;
        }

        private DateTime GetDatum(string datum)
        {
            if (datum is null || datum.Length != 8)
            {
                return DateTime.MinValue;
            }

            return new DateTime(
                Convert.ToInt32(datum[..4]),
                Convert.ToInt32(datum[4..6]),
                Convert.ToInt32(datum[6..]), 0, 0, 0, DateTimeKind.Utc);
        }
    }
}
