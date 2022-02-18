using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SV.RDW.Migrations.MySQL;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SV.RDW.Apps.Import
{
    public class ImportFunction
    {
        private readonly MySQLContext _context;

        public ImportFunction(MySQLContext context)
        {
            _context = context;
        }

        [FunctionName("ImportFunction")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Timer trigger function executed at: {DateTime.Now}");

            var lastImport = _context.Imports.OrderByDescending(x => x.EersteToelatingDatum).FirstOrDefault();

            var endDate = DateTime.Today;
            var initialStartDate = endDate.AddDays(-6);
            var startDate = lastImport.EersteToelatingDatum > initialStartDate ? lastImport.EersteToelatingDatum : initialStartDate;

            using (var client = new RdwHttpClient())
            {
                for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1))
                {
                    var result = await client.GetVehiclesWithTiming(date);                    

                    var import = new Data.Entities.Import()
                    {
                        EersteToelatingDatum = date,
                        TotaalImport = result.Value.Count,
                        //Voertuigen = result.Value.ToHashSet<Data.Entities.Voertuig>(), Dit werkt niet maar snapte de gedachtegang hier ook nog niet
                        ImportSeconden = result.Key
                    };

                    await _context.Imports.AddAsync(import);

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
