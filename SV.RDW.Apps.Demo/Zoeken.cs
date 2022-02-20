using Serilog;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;

namespace SV.RDW.Apps.Demo;

internal class Zoeken : BaseQuery
{
    public Zoeken(MySQLContext mySQLContext, PostgreSQLContext postgreSQLContext) : base(mySQLContext, postgreSQLContext)
    {
    }

    internal void Kenteken()
    {
        throw new NotImplementedException();
    }

    internal void Merk()
    {
        throw new NotImplementedException();
    }
}

