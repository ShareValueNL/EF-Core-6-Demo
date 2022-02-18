using SV.RDW.Data.Entities.ImportJson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace SV.RDW.Apps.Import
{
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
            var vehicles = new List<Voertuig>();

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
            PropertyInfo[] propertyInfos = typeof(Voertuig).GetProperties();
            var select = string.Join(",", propertyInfos.Select(x => x.Name));

            var response = await GetAsync($"m9d7-ebf2.json?$select={select}&$limit={_limit}&$offset={offset}&datum_eerste_toelating={firstAdmission:yyyyMMdd}");
            var result = await response.Content.ReadFromJsonAsync<List<Voertuig>>();
            
            return result;
        }

        public async Task<KeyValuePair<decimal, List<Voertuig>>> GetVehiclesWithTiming(DateTime date)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var vehicles = await GetVehicles(date);

            stopwatch.Stop();

            return new KeyValuePair<decimal, List<Voertuig>>((decimal)stopwatch.Elapsed.TotalSeconds, vehicles);
        }
    }
}
