using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SV.RDW.Data.Entities.ImportJson;
using SV.RDW.Migrations.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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

            // await _context.Voertuigen.AddRangeAsync(vehicles);
        }
    }
}
