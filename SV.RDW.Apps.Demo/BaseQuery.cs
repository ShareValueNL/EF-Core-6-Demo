using Serilog;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;

namespace SV.RDW.Apps.Demo;

internal class BaseQuery
{
    protected readonly MySQLContext _mySQLContext;
    protected readonly PostgreSQLContext _postgreSQLContext;

    public BaseQuery(MySQLContext mySQLContext, PostgreSQLContext postgreSQLContext)
    {
        _mySQLContext = mySQLContext;
        _postgreSQLContext = postgreSQLContext;
    }

    public void Query()
    {
        Console.Write("Voer de query in:");
        var query = Console.ReadLine();
    }
}

