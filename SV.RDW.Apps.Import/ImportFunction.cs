using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SV.RDW.Entities;
using SV.RDW.Migrations.MySQL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SV.RDW.Apps.Import
{
    public class ImportFunction
    {
        private readonly MySQLContext _context;

        // Dit moeten we ergens bij gaan houden waar we gebleven waren
        private DateTime _firstAdmissionDate = new DateTime(2022, 1, 3);

        public ImportFunction(MySQLContext context)
        {
            _context = context;
        }

        [FunctionName("ImportFunction")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Timer trigger function executed at: {DateTime.Now}");

            var vehicles = await new RdwHttpClient().GetVehicles(_firstAdmissionDate);

            await _context.Voertuigen.AddRangeAsync(vehicles);
        }
    }

    public class RdwHttpClient : HttpClient
    {
        private int _limit = 1000;
        private int _offsetStep = 1000;

        public RdwHttpClient()
        {
            BaseAddress = new Uri("https://opendata.rdw.nl/resource/");
        }

        public async Task<List<Voertuig>> GetVehicles(DateTime firstAdmission)
        {
            var offset = 0;
            var responseEmpty = false;
            List<Voertuig> vehicles = new List<Voertuig>();

            while(!responseEmpty)
            {
                var result = await GetVehicles(offset, firstAdmission);
                responseEmpty = result.Count == 0;

                vehicles.AddRange(result);

                offset += _offsetStep;
            }

            return vehicles;
        }

        public async Task<List<Voertuig>> GetVehicles(int offset, DateTime firstAdmission)
        {
            var response = await GetAsync($"m9d7-ebf2.json?$limit={_limit}&$offset={offset}&datum_eerste_toelating={firstAdmission:yyyyMMdd}");

            return await response.Content.ReadAsAsync<List<Voertuig>>();
        }
    }
}
