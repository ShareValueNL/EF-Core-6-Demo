using Microsoft.EntityFrameworkCore;
using Serilog;
using SV.RDW.Data.Entities;
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

    protected static double Timer(Action p)
    {
        var start = DateTime.Now;
        p.Invoke();
        return  DateTime.Now.Subtract(start).TotalMilliseconds;
    }

}

