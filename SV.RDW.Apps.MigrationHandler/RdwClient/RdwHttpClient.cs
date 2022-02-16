using SV.RDW.Entities;

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
            var content = await response.Content.ReadAsStringAsync();

            return await response.Content.ReadFromJsonAsync<List<Voertuig>>();
        }
    }
}
