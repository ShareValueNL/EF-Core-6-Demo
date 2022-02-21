using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Serilog;
using SV.RDW.Data.Entities;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;

namespace SV.RDW.Apps.Demo;

internal class Toppers : BaseQuery
{
    private IIncludableQueryable<Voertuig, VoertuigSoort?> _pgVoertuigenMetSoorten;
    private IIncludableQueryable<Voertuig, VoertuigSoort?> _myVoertuigenMetSoorten;

    private IIncludableQueryable<Voertuig, Merk?> _pgVoertuigenMetMerk; 
    private IIncludableQueryable<Voertuig, Merk?> _myVoertuigenMetMerk;

    public Toppers(MySQLContext mySQLContext, PostgreSQLContext postgreSQLContext) : base(mySQLContext, postgreSQLContext)
    {
        _pgVoertuigenMetSoorten = _postgreSQLContext.Voertuigen.Include(x => x.VoertuigSoort);
        _myVoertuigenMetSoorten = _mySQLContext.Voertuigen.Include(x => x.VoertuigSoort);

        _pgVoertuigenMetMerk = _postgreSQLContext.Voertuigen.Include(x => x.Merk);
        _myVoertuigenMetMerk = _mySQLContext.Voertuigen.Include(x => x.Merk);
    }

    internal void PerSoort()
    {
        var pgNu = DateTime.Now;
        var pgSoorten = _pgVoertuigenMetSoorten.GroupBy(x => x.VoertuigSoort.Naam)
                 .Select(g => new { g.Key, Count = g.Count() })
                 .OrderByDescending(x => x.Count);
        var pgDict = new Dictionary<string, int>();
        foreach (var item in pgSoorten)
        {
            pgDict.Add(item.Key, item.Count);
        }
        var pgTijd = DateTime.Now.Subtract(pgNu).TotalMilliseconds;

        var myNu = DateTime.Now;
        var mySoorten = _myVoertuigenMetSoorten.GroupBy(x => x.VoertuigSoort.Naam)
                 .Select(g => new { g.Key, Count = g.Count() })
                 .OrderByDescending(x => x.Count);
        var myDict = new Dictionary<string, int>();
        foreach (var item in mySoorten)
        {
            myDict.Add(item.Key, item.Count);   
        }
        var myTijd = DateTime.Now.Subtract(myNu).TotalMilliseconds;

        var msg = "\n| Voertuigsoort                            | Aantal | Aantal |";

        foreach (string key in pgDict.Keys)
        {
            msg += $"\n| {key,-40} | {pgDict[key], 6} | {myDict[key], 6} |";
        }
        
        msg += $"\n\n| Tijd                                     | {pgTijd,6:0.0} | {myTijd,6:0.0} |";

        Log.Information(msg);
    }

    internal void Automerken()
    {
        var pgNu = DateTime.Now;
        var pgSoorten = _pgVoertuigenMetMerk.GroupBy(x => x.Merk.Naam)
                 .Select(g => new { g.Key, Count = g.Count() })
                 .OrderByDescending(x => x.Count)
                 .Take(25);
        var pgDict = new Dictionary<string, int>();
        foreach (var item in pgSoorten)
        {
            pgDict.Add(item.Key, item.Count);
        }
        var pgTijd = DateTime.Now.Subtract(pgNu).TotalMilliseconds;

        var myNu = DateTime.Now;
        var mySoorten = _myVoertuigenMetMerk.GroupBy(x => x.Merk.Naam)
                 .Select(g => new { g.Key, Count = g.Count() })
                 .OrderByDescending(x => x.Count)
                 .Take(25);
        var myDict = new Dictionary<string, int>();
        foreach (var item in mySoorten)
        {
            myDict.Add(item.Key, item.Count);
        }
        var myTijd = DateTime.Now.Subtract(myNu).TotalMilliseconds;

        var msg = "\n| Merk                 | Aantal | Aantal |";

        foreach (string key in pgDict.Keys)
        {
            msg += $"\n| {key,-20} | {pgDict[key],6} | {myDict[key],6} |";
        }

        msg += $"\n\n| Tijd                 | {pgTijd,6:0.0} | {myTijd,6:0.0} |";

        Log.Information(msg);
    }
}

