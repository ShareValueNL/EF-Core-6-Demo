
using Serilog;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;
using System.Globalization;

namespace SV.RDW.Apps.Demo;

internal class Totalen : BaseQuery
{
    public Totalen(MySQLContext mySQLContext, PostgreSQLContext postgreSQLContext) : base(mySQLContext, postgreSQLContext)
    {
    }

    internal void Count()
    {
        int pgCount = 0;
        double pgTijd = Timer(() => { pgCount = _postgreSQLContext.Voertuigen.Count(); });

        Log.Information($"PostgreSQL Voertuigen Aantal: {pgCount} ({pgTijd:0.0} ms).");

        int myCount = 0;
        double myTijd = Timer(() => { myCount = _postgreSQLContext.Voertuigen.Count(); });

        Log.Information($"MySQL Voertuigen Aantal: {myCount} ({myTijd:0.0} ms).");

        string msg = " is sneller. Verschil: ";
        if (pgTijd < myTijd)
        {
            msg = "PostgreSQL" + msg;
        }
        else
        {
            msg = "MySQL" + msg;
        }
        var high = Math.Max(pgTijd, myTijd);
        var low = Math.Min(pgTijd, myTijd);

        msg += $"{(high - low):0.0} ms, of {-1 + 1 * high / low:P1}";
        
        Log.Information(msg);
    }


    internal void TotaalPerMaand()
    {
        var startdatum = new DateTime(2022, 2, 1);
        var aantalmaanden = 20;
        string msg = "\nDatum     | Aantal Postgres | Aantal MySQL | Tijd Postgres | Tijd MySQL | Tijdsverschil (s) |    (%) |";

        decimal pgTijdTotaal = 0;
        decimal myTijdTotaal = 0;
        for (int i = 0; i<aantalmaanden; i++)
        {
            var datum = startdatum.AddMonths(-i);
            var pgMaand = _postgreSQLContext.Imports.Where(x => x.EersteToelatingDatum >= datum && x.EersteToelatingDatum < datum.AddMonths(1));
            var myMaand = _mySQLContext.Imports.Where(x => x.EersteToelatingDatum >= datum && x.EersteToelatingDatum < datum.AddMonths(1));
            var pgAantal = pgMaand.Sum(x => x.TotaalImport);
            var myAantal = myMaand.Sum(x => x.TotaalImport);
            var pgTijd = pgMaand.Sum(x => x.ImportSeconden);
            var myTijd = myMaand.Sum(x => x.ImportSeconden);
            pgTijdTotaal += pgTijd;
            myTijdTotaal += myTijd;
            var maand = datum.ToString("MMM yyyy").PadLeft(9);
            msg += $"\n{maand} | {pgAantal,15} | {myAantal,12} | {pgTijd,13:0} | {myTijd,10:0} | {pgTijd-myTijd,17:0} | {-1+pgTijd/myTijd,6:P1} |";
        }
        msg += $"\nTotaal    |                 |              | {pgTijdTotaal,13:0} | {myTijdTotaal,10:0} | {pgTijdTotaal - myTijdTotaal,17:0} | {-1+pgTijdTotaal/myTijdTotaal,6:P1} |";

        Log.Information(msg);
        
    }

}
