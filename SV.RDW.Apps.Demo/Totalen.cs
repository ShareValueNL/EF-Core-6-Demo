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
        throw new NotImplementedException();
    }

    internal void TotaalPerMaand()
    {
        throw new NotImplementedException();
    }

}
