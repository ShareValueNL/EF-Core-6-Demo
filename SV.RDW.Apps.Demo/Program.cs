using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SV.RDW.Apps.Demo;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;
using System.Drawing;

using var log = new LoggerConfiguration()
    .WriteTo.Console(
        theme: AnsiConsoleTheme.Code,
        outputTemplate: "[{Timestamp:dd.MM.yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

Log.Logger = log;
Colorful.Console.ForegroundColor = Color.LightGray;
Colorful.Console.WriteAscii("ShareValue's");
Colorful.Console.WriteAscii("Tech Thursday");
Console.WriteLine("24 februari 2022");
Console.WriteLine("EF Core 6 Demo");

var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
var config = builder.Build();
string connectionStringPostgres = config["ConnectionStrings:postgres"];
string connectionStringMySql = config["ConnectionStrings:mysql"];


var serviceProvider = new ServiceCollection()
           .AddNpgsql<PostgreSQLContext>(connectionStringPostgres)
           .AddDbContext<MySQLContext>(options =>
           {
               options.EnableDetailedErrors(true);
               options.UseMySql(connectionStringMySql, ServerVersion.AutoDetect(connectionStringMySql));
           })
           .BuildServiceProvider();

var postgresContext = serviceProvider.GetRequiredService<PostgreSQLContext>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var mySqlContext = serviceProvider.GetRequiredService<MySQLContext>();

var totalen = new Totalen(mySqlContext, postgresContext);
var toppers = new Toppers(mySqlContext, postgresContext);
var zoeken = new Zoeken(mySqlContext, postgresContext);

char optie = char.MinValue;
char prevoptie = char.MinValue;
char repoptie = char.MinValue;
bool show = true;
int i = 0;
do
{
    if (repoptie == char.MinValue)
    {
        optie = Menu.ToConsole(show);
    }
    else
    {
        optie = repoptie;
        i--;
        if (i == 0)
        {
            repoptie = char.MinValue;
        }
    }
    show = false;
    switch (optie)
    {
        case '1':
            // Totaal Count()
            totalen.Count();
            break;
        case '2':
            // Totaal per maand
            totalen.TotaalPerMaand();
            break;
        case '3':
            // Verdeling soorten
            toppers.PerSoort();
            break;
        case '4':
            // Top 10 automerken
            toppers.Automerken();
            break;
        case '5':
            // Zoek kenteken
            zoeken.Kenteken();
            break;
        case '6':
            // Zoek merk
            zoeken.Merk();
            break;
        case '7':
            // Bouw query
            zoeken.Query();
            break;
        case 'M':
            // Menu
            show = true;
            break;
        case 'C':
            // Scherm leeg
            Console.Clear();
            break;
        case 'Q':
            // Afsluiten
            break;
        case 'R':
            // Herhalen
            repoptie = prevoptie;
            i = 5;
            break;
        default:
            Console.WriteLine("Deze optie bestaat niet. Kies uit het menu.");
            break;
    }
    prevoptie = optie;
    Console.WriteLine();
}
while (optie != 'Q');

Console.WriteLine("Applicatie wordt afgesloten.");
