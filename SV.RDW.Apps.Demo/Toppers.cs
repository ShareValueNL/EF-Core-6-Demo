using Serilog;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;

namespace SV.RDW.Apps.Demo;

internal class Toppers : BaseQuery
{
    public Toppers(MySQLContext mySQLContext, PostgreSQLContext postgreSQLContext) : base(mySQLContext, postgreSQLContext)
    {
    }

    internal void PerSoort()
    {
        throw new NotImplementedException();
    }

    internal void Automerken()
    {
        throw new NotImplementedException();
    }
}

