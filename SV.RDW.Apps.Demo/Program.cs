using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;

using var log = new LoggerConfiguration()
    .WriteTo.Console(
        theme: AnsiConsoleTheme.Code,
        outputTemplate: "[{Timestamp:dd.MM.yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

Log.Logger = log;
Log.Information("ShareValue Tech Thursday - 24 februari 2022");
Log.Information("EF Core 6 Demo");

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
