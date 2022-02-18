using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SV.RDW.Data.Entities.ImportJson;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;
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

        // Dit moeten we ergens bij gaan houden waar we gebleven waren
        private DateTime _firstAdmissionDate = new DateTime(2022, 1, 3);

        [FunctionName("ImportFunction")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Timer trigger function executed at: {DateTime.Now}");

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            string connectionStringPostgres = config["ConnectionStrings:Postgres"];
            string connectionStringMySql = config["ConnectionStrings:mysql"];


            var serviceProvider = new ServiceCollection()
                       .AddNpgsql<PostgreSQLContext>(connectionStringPostgres)
                       .AddDbContext<MySQLContext>(options =>
                       {
                           options.UseMySql(ServerVersion.AutoDetect(connectionStringMySql));
                       })
                       .BuildServiceProvider();


            var vehicles = await new RdwHttpClient().GetVehicles(_firstAdmissionDate);

            // await _context.Voertuigen.AddRangeAsync(vehicles);
        }
    }
}
