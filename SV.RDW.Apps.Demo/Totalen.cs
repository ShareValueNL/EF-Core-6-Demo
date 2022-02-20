
using Serilog;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;

namespace SV.RDW.Apps.Demo;

internal class Totalen : BaseQuery
{
    public Totalen(MySQLContext mySQLContext, PostgreSQLContext postgreSQLContext) : base(mySQLContext, postgreSQLContext)
    {
    }

    internal void Count()
    {
        var dt1 = DateTime.Now;
        var count1 = _postgreSQLContext.Voertuigen.Count();
        var tijd1 = DateTime.Now.Subtract(dt1).TotalMilliseconds;

        Log.Information($"PostgreSQL Voertuigen Aantal: {count1} ({tijd1:0.0} ms).");

        var dt2 = DateTime.Now;
        var count2 = _postgreSQLContext.Voertuigen.Count();
        var tijd2 = DateTime.Now.Subtract(dt1).TotalMilliseconds;

        Log.Information($"MySQL Voertuigen Aantal: {count1} ({tijd2:0.0} ms).");

        string msg = " is sneller. Verschil: ";
        if (tijd1 < tijd2)
        {
            msg = "PostgreSQL" + msg;
        }
        else
        {
            msg = "MySQL" + msg;
        }
        var high = Math.Max(tijd1, tijd2);
        var low = Math.Min(tijd1, tijd1);

        msg += $"{(high - low):0.0} ms, of {-1 + 1 * high / low:P1}%";
        
        Log.Information(msg);
    }

    internal void TotaalPerMaand()
    {
        throw new NotImplementedException();
    }

}
