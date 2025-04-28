using System.Text;
using Spectre.Console;
using Spectre.Console.Rendering;
using System.Diagnostics;

public class SpectreUI
{    
    private static int _decentralizationTime = 900; // Time in seconds for decentralization
    private static string _exploded = "CiDilojilojilpPilpPilojilojilojilojilogg4paT4paI4paI4paI4paI4paI4paEICAgIOKWk+KWiOKWiOKWiOKWiOKWiOKWhCDilpPilojilojilojilojilojiloTiloTiloTilojilojilojilojilojilpMg4paS4paI4paI4paI4paI4paIICAg4paI4paI4paI4paEICAgIOKWiCAg4paE4paE4paEICAgICDiloTiloTiloTilojilojilojilojilojilpPilpPilojilojilojilojilogg4paT4paI4paI4paI4paI4paI4paEIArilpPilojilojilpLilpPiloggICDiloAg4paS4paI4paI4paAIOKWiOKWiOKWjCAgIOKWkuKWiOKWiOKWgCDilojilojilozilpPiloggICDiloDilpMgIOKWiOKWiOKWkiDilpPilpLilpLilojilojilpIgIOKWiOKWiOKWkiDilojilogg4paA4paIICAg4paIIOKWkuKWiOKWiOKWiOKWiOKWhCAgIOKWkyAg4paI4paI4paSIOKWk+KWkuKWk+KWiCAgIOKWgCDilpLilojilojiloAg4paI4paI4paMCuKWkuKWiOKWiOKWkuKWkuKWiOKWiOKWiCAgIOKWkeKWiOKWiCAgIOKWiOKWjCAgIOKWkeKWiOKWiCAgIOKWiOKWjOKWkuKWiOKWiOKWiCAg4paSIOKWk+KWiOKWiOKWkSDilpLilpHilpLilojilojilpEgIOKWiOKWiOKWkuKWk+KWiOKWiCAg4paA4paIIOKWiOKWiOKWkuKWkuKWiOKWiCAg4paA4paI4paEIOKWkiDilpPilojilojilpEg4paS4paR4paS4paI4paI4paIICAg4paR4paI4paIICAg4paI4paMCuKWkeKWiOKWiOKWkeKWkuKWk+KWiCAg4paEIOKWkeKWk+KWiOKWhCAgIOKWjCAgIOKWkeKWk+KWiOKWhCAgIOKWjOKWkuKWk+KWiCAg4paE4paRIOKWk+KWiOKWiOKWkyDilpEg4paS4paI4paIICAg4paI4paI4paR4paT4paI4paI4paSICDilpDilozilojilojilpLilpHilojilojiloTiloTiloTiloTilojilojilpEg4paT4paI4paI4paTIOKWkSDilpLilpPiloggIOKWhCDilpHilpPilojiloQgICDilowK4paR4paI4paI4paR4paR4paS4paI4paI4paI4paI4paS4paR4paS4paI4paI4paI4paI4paTICAgIOKWkeKWkuKWiOKWiOKWiOKWiOKWkyDilpHilpLilojilojilojilojilpIg4paS4paI4paI4paSIOKWkSDilpEg4paI4paI4paI4paI4paT4paS4paR4paS4paI4paI4paRICAg4paT4paI4paI4paRIOKWk+KWiCAgIOKWk+KWiOKWiOKWkiDilpLilojilojilpIg4paRIOKWkeKWkuKWiOKWiOKWiOKWiOKWkuKWkeKWkuKWiOKWiOKWiOKWiOKWkyAK4paR4paTICDilpHilpEg4paS4paRIOKWkSDilpLilpLilpMgIOKWkiAgICAg4paS4paS4paTICDilpIg4paR4paRIOKWkuKWkSDilpEg4paSIOKWkeKWkSAgIOKWkSDilpLilpHilpLilpHilpLilpEg4paRIOKWkuKWkSAgIOKWkiDilpIgIOKWkuKWkiAgIOKWk+KWkuKWiOKWkSDilpIg4paR4paRICAg4paR4paRIOKWkuKWkSDilpEg4paS4paS4paTICDilpIgCiDilpIg4paRIOKWkSDilpEgIOKWkSDilpEg4paSICDilpIgICAgIOKWkSDilpIgIOKWkiAg4paRIOKWkSAg4paRICAg4paRICAgICAg4paRIOKWkiDilpLilpEg4paRIOKWkeKWkSAgIOKWkSDilpLilpEgIOKWkiAgIOKWkuKWkiDilpEgICDilpEgICAgIOKWkSDilpEgIOKWkSDilpEg4paSICDilpIgCiDilpIg4paRICAg4paRICAgIOKWkSDilpEgIOKWkSAgICAg4paRIOKWkSAg4paRICAgIOKWkSAgICDilpEgICAgICDilpEg4paRIOKWkSDilpIgICAgIOKWkSAgIOKWkSDilpEgICDilpEgICDilpIgICAg4paRICAgICAgICAg4paRICAgIOKWkSDilpEgIOKWkSAKIOKWkSAgICAg4paRICDilpEgICDilpEgICAgICAgICAg4paRICAgICAgIOKWkSAg4paRICAgICAgICAgICAg4paRIOKWkSAgICAgICAgICAg4paRICAgICAgIOKWkSAg4paRICAgICAgICAgICDilpEgIOKWkSAgIOKWkSAgICAKICAgICAgICAgICAg4paRICAgICAgICAgIOKWkSAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICDilpEgICAgICAK";
    private static string _saved = "CuKWiOKWiOKVlyAgIOKWiOKWiOKVlyDilojilojilojilojilojilojilZcg4paI4paI4pWXICAg4paI4paI4pWXICAgIOKWiOKWiOKWiOKWiOKWiOKWiOKWiOKVlyDilojilojilojilojilojilZcg4paI4paI4pWXICAg4paI4paI4pWX4paI4paI4paI4paI4paI4paI4paI4pWX4paI4paI4paI4paI4paI4paI4pWXICAgICDilojilojilZfilojilojilojilojilojilojilojilojilZfilojilojilZcK4pWa4paI4paI4pWXIOKWiOKWiOKVlOKVneKWiOKWiOKVlOKVkOKVkOKVkOKWiOKWiOKVl+KWiOKWiOKVkSAgIOKWiOKWiOKVkSAgICDilojilojilZTilZDilZDilZDilZDilZ3ilojilojilZTilZDilZDilojilojilZfilojilojilZEgICDilojilojilZHilojilojilZTilZDilZDilZDilZDilZ3ilojilojilZTilZDilZDilojilojilZcgICAg4paI4paI4pWR4pWa4pWQ4pWQ4paI4paI4pWU4pWQ4pWQ4pWd4paI4paI4pWRCiDilZrilojilojilojilojilZTilZ0g4paI4paI4pWRICAg4paI4paI4pWR4paI4paI4pWRICAg4paI4paI4pWRICAgIOKWiOKWiOKWiOKWiOKWiOKWiOKWiOKVl+KWiOKWiOKWiOKWiOKWiOKWiOKWiOKVkeKWiOKWiOKVkSAgIOKWiOKWiOKVkeKWiOKWiOKWiOKWiOKWiOKVlyAg4paI4paI4pWRICDilojilojilZEgICAg4paI4paI4pWRICAg4paI4paI4pWRICAg4paI4paI4pWRCiAg4pWa4paI4paI4pWU4pWdICDilojilojilZEgICDilojilojilZHilojilojilZEgICDilojilojilZEgICAg4pWa4pWQ4pWQ4pWQ4pWQ4paI4paI4pWR4paI4paI4pWU4pWQ4pWQ4paI4paI4pWR4pWa4paI4paI4pWXIOKWiOKWiOKVlOKVneKWiOKWiOKVlOKVkOKVkOKVnSAg4paI4paI4pWRICDilojilojilZEgICAg4paI4paI4pWRICAg4paI4paI4pWRICAg4pWa4pWQ4pWdCiAgIOKWiOKWiOKVkSAgIOKVmuKWiOKWiOKWiOKWiOKWiOKWiOKVlOKVneKVmuKWiOKWiOKWiOKWiOKWiOKWiOKVlOKVnSAgICDilojilojilojilojilojilojilojilZHilojilojilZEgIOKWiOKWiOKVkSDilZrilojilojilojilojilZTilZ0g4paI4paI4paI4paI4paI4paI4paI4pWX4paI4paI4paI4paI4paI4paI4pWU4pWdICAgIOKWiOKWiOKVkSAgIOKWiOKWiOKVkSAgIOKWiOKWiOKVlwogICDilZrilZDilZ0gICAg4pWa4pWQ4pWQ4pWQ4pWQ4pWQ4pWdICDilZrilZDilZDilZDilZDilZDilZ0gICAgIOKVmuKVkOKVkOKVkOKVkOKVkOKVkOKVneKVmuKVkOKVnSAg4pWa4pWQ4pWdICDilZrilZDilZDilZDilZ0gIOKVmuKVkOKVkOKVkOKVkOKVkOKVkOKVneKVmuKVkOKVkOKVkOKVkOKVkOKVnSAgICAg4pWa4pWQ4pWdICAg4pWa4pWQ4pWdICAg4pWa4pWQ4pWdCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAK";
    private static string _path = "./IED.conf";
    public static bool IEDGame(string borderColor)
    {
        int totalSeconds = _decentralizationTime;
        int current = 0;

        while (true)
        {
        // Progress bar string (15 characters) – escape markup brackets.
        int maxBarLength = Math.Max(Console.WindowWidth - 20, 10);
        int filledLength = (int)((double)current / totalSeconds * maxBarLength);
        string bar = new string('█', filledLength) + new string('─', maxBarLength - filledLength);
        string barMarkup = $"          [[{bar}]] {current:D2}/{totalSeconds:D2}";

        var content = new RowsRenderable(
            new Align(new Markup("Remaining time before IED explodes: \n"), HorizontalAlignment.Center),
            new Align(new Markup(barMarkup), HorizontalAlignment.Center)
        );

        var popup = new Panel(content)
            .Header("")
            .Border(BoxBorder.None)
            .BorderStyle(Style.Parse(borderColor));

        AnsiConsole.Clear();
        // Display as a popup centered on the screen.
        AnsiConsole.Write(new Align(popup, HorizontalAlignment.Center, VerticalAlignment.Middle));

        // Handle completion or cancellation.
        if (current == totalSeconds)
            break;

        Thread.Sleep(1000);
        // Check if value in _path is equal to "IED enabled"
        if (File.Exists(_path))
        {
            string fileContent = File.ReadAllText(_path);
            if (!fileContent.Contains("IED enabled"))
            {
                // If the file contains "IED enabled", break the loop.
                return true;
            }
        }
        current++;
        }
        Console.Clear();
        return false;
    }

    // Helper renderable that stacks items vertically.
    public sealed class RowsRenderable : IRenderable
    {
        private readonly IRenderable[] _rows;
        public RowsRenderable(params IRenderable[] rows) => _rows = rows;

        public Measurement Measure(RenderOptions options, int maxWidth) =>
            new Measurement(0, maxWidth);

        public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
        {
            foreach (var row in _rows)
                foreach (var seg in row.Render(options, maxWidth))
                    yield return seg;
        }
    }

    public static void WriteGameLog(string path, bool exploded){
        // Create a new log entry
        string logEntry = $"{DateTime.Now}: Game Over! {(exploded ? "Exploded" : "Saved")}\n";
        // Append the log entry to the file
        File.AppendAllText(path, logEntry);
        // Print the log entry to the console
        Console.WriteLine(logEntry);
    }


    public static void EndGame(bool exploded){
        // Create new layout and display the final screen
        // Render also finalScreen from base64

        string finalMessage = "";
        if (!exploded)
        {
            // from base64 convert _exploded to string
            finalMessage = Encoding.UTF8.GetString(Convert.FromBase64String(_exploded));
        }
        else
        {
            finalMessage = Encoding.UTF8.GetString(Convert.FromBase64String(_saved));
        }
        
        int padLines = Math.Max(0, Console.WindowHeight - 27);
        RowsRenderable rows = new RowsRenderable(
            new Align(new Markup(finalMessage), HorizontalAlignment.Center),
            new Align(new Markup("Press [green]Enter[/] to exit"), HorizontalAlignment.Center)
        );
        var popup = new Panel(rows)
            .Header("[bold]GAME OVER[/]")
            .Border(BoxBorder.Rounded)
            .BorderStyle(Style.Parse("yellow"))
            .Expand();
        AnsiConsole.Clear();
        AnsiConsole.Write(new Align(popup, HorizontalAlignment.Center, VerticalAlignment.Middle));
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        AnsiConsole.Clear();
        Console.CursorVisible = true;
        WriteGameLog("game_log.txt", exploded);
        Process.GetCurrentProcess().Kill();
    }
    

}

public class Program
{
    public static void Main(string[] args)
    {
        // ── Prevent Ctrl+C / Ctrl+Break from killing us ──
        Console.TreatControlCAsInput = true; 
        Console.CancelKeyPress += (sender, e) =>
        {
            // swallow the event and do nothing
            e.Cancel = true;
        };

        // Now your game loop will never be aborted by Ctrl+C (SIGINT)
        // or Ctrl+Break; they’ll just arrive as input that you ignore.
        
        string borderColor = "red";
        bool result = SpectreUI.IEDGame(borderColor);
        SpectreUI.EndGame(result);
    }
}