using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Serilog;
using SV.RDW.Data.Entities;
using SV.RDW.Migrations.MySQL;
using SV.RDW.Migrations.PostgreSQL;

namespace SV.RDW.Apps.Demo;

internal class Zoeken : BaseQuery
{
    private IIncludableQueryable<Voertuig, VoertuigSoort?> _pgContext;
    private IIncludableQueryable<Voertuig, VoertuigSoort?> _myContext;

    public Zoeken(MySQLContext mySQLContext, PostgreSQLContext postgreSQLContext) : base(mySQLContext, postgreSQLContext)
    {
        _pgContext = _postgreSQLContext.Voertuigen.Include(x => x.Handelsbenaming)
                                                  .Include(x => x.Merk)
                                                  .Include(x => x.VoertuigSoort);
        _myContext = _mySQLContext.Voertuigen.Include(x => x.Handelsbenaming)
                                             .Include(x => x.Merk)
                                             .Include(x => x.VoertuigSoort);
    }

    internal void Kenteken()
    {
        var (kenteken, type) = Menu.ZoekKenteken();
        IQueryable<Voertuig> pgVoertuigen;
        IQueryable<Voertuig> myVoertuigen;

        switch (type)
        {
            case 'L': // Gebruik ToLower
                kenteken = "%" + kenteken + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.Like(x.Kenteken.ToLower(), kenteken.ToLower()));
                myVoertuigen = _myContext.Where(x => EF.Functions.Like(x.Kenteken.ToLower(), kenteken.ToLower()));
                break;

            case 'U': // Gebruik ToUpper
                kenteken = "%" + kenteken + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.Like(x.Kenteken.ToUpper(), kenteken.ToUpper()));
                myVoertuigen = _myContext.Where(x => EF.Functions.Like(x.Kenteken.ToUpper(), kenteken.ToUpper()));
                break;

            case 'S': // Gebruik StringComparison.InvariantCultureIgnoreCase
                pgVoertuigen = _pgContext.Where(x => string.Compare(x.Kenteken, kenteken, 
                                                                        StringComparison.InvariantCultureIgnoreCase) == 0);
                myVoertuigen = _myContext.Where(x => string.Compare(x.Kenteken, kenteken,
                                                                        StringComparison.InvariantCultureIgnoreCase) == 0);
                break;

            case 'I': // Gebruik ILIKE (Postgres) / LIKE (MySQL)
                kenteken = "%" + kenteken + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.ILike(x.Kenteken, kenteken));
                myVoertuigen = _myContext.Where(x => EF.Functions.Like(x.Kenteken, kenteken));
                break;

            case 'C': // Gebruik LIKE (Postgres) / Contains (MySQL)
                myVoertuigen = _myContext.Where(x => x.Kenteken.Contains(kenteken));
                kenteken = "%" + kenteken + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.Like(x.Kenteken, kenteken));
                break;

            case 'E': // Gebruik exact
                pgVoertuigen = _pgContext.Where(x => x.Kenteken == kenteken);
                myVoertuigen = _myContext.Where(x => x.Kenteken == kenteken);
                break;

            default:
                Console.WriteLine("Er is geen geldige optie gekozen. Operatie wordt afgebroken");
                return;
        }

        int pgCount = 0;
        int myCount = 0;
        var pgTijd = Timer(() =>
        {
            string msg = ShowVoertuigen(pgVoertuigen);
            pgCount = pgVoertuigen.Count();
            Log.Information(msg);
        });
        var myTijd = Timer(() =>
        {
            string msg = ShowVoertuigen(myVoertuigen);
            myCount = myVoertuigen.Count();
            Log.Information(msg);
        });

        Log.Information($"PostgreSQL deed er {pgTijd:0.0} ms over om {pgCount} voertuigen op te halen.");
        Log.Information($"MySQL deed er {myTijd:0.0} ms over om {myCount} voertuigen op te halen.");
    }

    private string ShowVoertuigen(IQueryable<Voertuig> voertuigen)
    {
        string msg = "\n| Kenteken | Merk                 | Handelsbenaming                          | Kleur      | Inrichting      |";
        foreach (var voertuig in voertuigen)
        {
            msg += $"\n| {voertuig.Kenteken,8} | {voertuig.Merk.Naam,-20} | {voertuig.Handelsbenaming.Naam,-40} | {voertuig.Kleur,-10} | {voertuig.Inrichting,-15} |";
        }
        return msg;
    }

    internal void Merk()
    {
        var (merk, type) = Menu.ZoekMerk();

        IQueryable<Voertuig> pgVoertuigen;
        IQueryable<Voertuig> myVoertuigen;

        switch (type)
        {
            case 'L': // Gebruik ToLower
                merk = "%" + merk + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.Like(x.Merk.Naam.ToLower(), merk.ToLower()));
                myVoertuigen = _myContext.Where(x => EF.Functions.Like(x.Merk.Naam.ToLower(), merk.ToLower()));
                break;

            case 'U': // Gebruik ToUpper
                merk = "%" + merk + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.Like(x.Merk.Naam.ToUpper(), merk.ToUpper()));
                myVoertuigen = _myContext.Where(x => EF.Functions.Like(x.Merk.Naam.ToUpper(), merk.ToUpper()));
                break;

            case 'S': // Gebruik StringComparison.InvariantCultureIgnoreCase
                pgVoertuigen = _pgContext.Where(x => string.Compare(x.Merk.Naam, merk,
                                                                        StringComparison.InvariantCultureIgnoreCase) == 0);
                myVoertuigen = _myContext.Where(x => string.Compare(x.Merk.Naam, merk,
                                                                        StringComparison.InvariantCultureIgnoreCase) == 0);
                break;

            case 'I': // Gebruik ILIKE (Postgres) / LIKE (MySQL)
                merk = "%" + merk + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.ILike(x.Merk.Naam, merk));
                myVoertuigen = _myContext.Where(x => EF.Functions.Like(x.Merk.Naam, merk));
                break;

            case 'C': // Gebruik LIKE (Postgres) / Contains (MySQL)
                myVoertuigen = _myContext.Where(x => x.Merk.Naam.Contains(merk));
                merk = "%" + merk + "%";
                pgVoertuigen = _pgContext.Where(x => EF.Functions.Like(x.Merk.Naam, merk));
                break;

            case 'E': // Gebruik exact
                pgVoertuigen = _pgContext.Where(x => x.Merk.Naam == merk);
                myVoertuigen = _myContext.Where(x => x.Merk.Naam == merk);
                break;

            default:
                Console.WriteLine("Er is geen geldige optie gekozen. Operatie wordt afgebroken");
                return;
        }

        int pgCount = 0;
        int myCount = 0;
        var pgTijd = Timer(() =>
        {
            var pgVoertuigenGroupBy = pgVoertuigen.GroupBy(x =>
                            new GroupByObject { Merk = x.Merk.Naam, Type = x.Handelsbenaming.Naam, Soort = x.VoertuigSoort.Naam })
                            .Select(g => new KeyCountObject { Key = g.Key, Count = g.Count() });

            string msg = ShowMerk(pgVoertuigenGroupBy);
            pgCount = pgVoertuigenGroupBy.Count();
            Log.Information(msg);
        });
        var myTijd = Timer(() =>
        { 
            var myVoertuigenGroupBy = myVoertuigen.GroupBy(x =>
                            new GroupByObject { Merk = x.Merk.Naam, Type = x.Handelsbenaming.Naam, Soort = x.VoertuigSoort.Naam })
                            .Select(g => new KeyCountObject { Key = g.Key, Count = g.Count() });

            string msg = ShowMerk(myVoertuigenGroupBy);
            myCount = myVoertuigenGroupBy.Count();
            Log.Information(msg);
        });

        Log.Information($"PostgreSQL deed er {pgTijd:0.0} ms over om {pgCount} regels op te halen.");
        Log.Information($"MySQL deed er {myTijd:0.0} ms over om {myCount} regels op te halen.");
    }

    private string ShowMerk(IQueryable<KeyCountObject> voertuigen)
    {
       
        string msg = "\n| Merk                 | Handelsbenaming                          | Soort                                 | Aantal |";
        foreach (var voertuig in voertuigen)
        {
           msg += $"\n| {voertuig.Key.Merk,-20} | {voertuig.Key.Type,-40} | {voertuig.Key.Soort,-37} | {voertuig.Count,6} |";
        }
        return msg;
    }

    public class GroupByObject
    {
        public string Merk { get; set; } 

        public string Type { get; set; }

        public string Soort { get; set; }
    }
    
    public class KeyCountObject
    {
        public GroupByObject Key { get; set; }

        public int Count { get; set; }
    }



}

