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
    public class AzureFunction
    {
        [FunctionName("ImportFunction")]
        public async Task Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
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
                           options.UseMySql(connectionStringMySql, ServerVersion.AutoDetect(connectionStringMySql));
                       })
                       .BuildServiceProvider();

            var pgContext = serviceProvider.GetRequiredService<PostgreSQLContext>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var myContext = serviceProvider.GetRequiredService<MySQLContext>();

            var pgRoutine = new ImportRoutine(pgContext);
            var pgCount = await pgRoutine.Run();
            var myRoutine = new ImportRoutine(myContext);
            var myCount = await myRoutine.Run();

            log.LogInformation($"PostgreSQL import aantal: {pgCount}");
            log.LogMetric("PostgreSQL.Import", pgCount);

            log.LogInformation($"MySQL import aantal: {myCount}");
            log.LogMetric("MySQL.Import", myCount);

        }
    }
}
