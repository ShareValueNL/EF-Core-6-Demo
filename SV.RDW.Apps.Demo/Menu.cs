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
            style.AddStyle("7 ", Color.Yellow);
            style.AddStyle("Q ", Color.Yellow);

            Colorful.Console.WriteLineStyled("┌──────────────────────────┬──────────────────────────┐", style);
            Colorful.Console.WriteLineStyled("│ 1 - Totaal Count()       │ 2 - Totaal per maand     │", style);
            Colorful.Console.WriteLineStyled("│ 3 - Verdeling soorten    │ 4 - Top 10 automerken    │", style);
            Colorful.Console.WriteLineStyled("│ 5 - Zoek kenteken        │ 6 - Zoek merk            │", style);
            Colorful.Console.WriteLineStyled("│ 7 - Bouw query           │ Q - Afsluiten            │", style);
            Colorful.Console.WriteLineStyled("└──────────────────────────┴──────────────────────────┘", style);
        }

        Console.Write("Selecteer optie: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var c = Char.ToUpper(Console.ReadKey().KeyChar);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        return c;
    }
}