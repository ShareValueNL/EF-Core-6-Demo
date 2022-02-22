using System.Drawing;

namespace SV.RDW.Apps.Demo;
internal class Menu
{

    public static char ToConsole(bool showmenu)
    {
        if (showmenu)
        {
            var style = new Colorful.StyleSheet(Color.LightGray);
            style.AddStyle("┌", Color.Green);
            style.AddStyle("┐", Color.Green);
            style.AddStyle("└", Color.Green);
            style.AddStyle("┘", Color.Green);
            style.AddStyle("┬", Color.Green);
            style.AddStyle("┴", Color.Green);
            style.AddStyle("─", Color.Green);
            style.AddStyle("│", Color.Green);
            style.AddStyle("1 ", Color.Yellow);
            style.AddStyle("2 ", Color.Yellow);
            style.AddStyle("3 ", Color.Yellow);
            style.AddStyle("4 ", Color.Yellow);
            style.AddStyle("5 ", Color.Yellow);
            style.AddStyle("6 ", Color.Yellow);
            style.AddStyle("Q ", Color.Yellow);
            style.AddStyle("25", Color.LightGray);

            Colorful.Console.WriteLineStyled("┌──────────────────────────┬──────────────────────────┐", style);
            Colorful.Console.WriteLineStyled("│ 1 - Totaal Count()       │ 2 - Totaal per maand     │", style);
            Colorful.Console.WriteLineStyled("│ 3 - Verdeling soorten    │ 4 - Top 25 automerken    │", style);
            Colorful.Console.WriteLineStyled("│ 5 - Zoek kenteken        │ 6 - Zoek merk            │", style);
            Colorful.Console.WriteLineStyled("│                          │ Q - Afsluiten            │", style);
            Colorful.Console.WriteLineStyled("└──────────────────────────┴──────────────────────────┘", style);
        }

        Console.Write("Selecteer optie: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var c = Char.ToUpper(Console.ReadKey().KeyChar);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        return c;
    }

    public static (string kenteken, char type) ZoekKenteken()
    {
        Console.Write("Geef (deel van een kenteken), zonder streepjes: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var kenteken = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.White;

        var type = VraagOptie();

        return (kenteken, type);
    }

    public static (string merk, char type) ZoekMerk()
    {
        Console.Write("Geef (deel van een merk): ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var merk = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.White;

        var type = VraagOptie();

        return (merk, type);
    }

    private static char VraagOptie()
    {
        Console.WriteLine("Opties:");
        Console.WriteLine("L - Gebruik ToLower");
        Console.WriteLine("U - Gebruik ToUpper");
        Console.WriteLine("S - Gebruik StringComparison.InvariantCultureIgnoreCase");
        Console.WriteLine("I - Gebruik ILIKE (Postgres) / LIKE (MySQL)");
        Console.WriteLine("C - Gebruik LIKE (Postgres) / Contains (MySQL)");
        Console.WriteLine("E - Gebruik exact");

        Console.Write("Selecteer optie: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var type = Char.ToUpper(Console.ReadKey().KeyChar);
        Console.ForegroundColor = ConsoleColor.White;

        return type;
    }
}