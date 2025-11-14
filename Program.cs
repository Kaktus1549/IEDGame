using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;

// ==============================================================
// ===============  SPECTRE IED GAME – FULLSCREEN ===============
// ==============================================================

public class IEDGameSystem
{
    private static int _bombTime = 900;   // seconds
    private static string _armCode = "1234";
    private static string _defuseCode = "9999";

    private static string _currentInput = "";
    private static bool _armed = false;

    private static bool _freezing = false;
    private static int _freezeLeft = 25;

    private static readonly string _path = "./IED.conf";
    private static readonly string _logFile = "game_log.txt";

    private static readonly string _exploded = "<YOUR BASE64>";
    private static readonly string _saved = "<YOUR BASE64>";

    // ==========================================================
    // =============== FULLSCREEN WRAPPER (IMPORTANT) ===========
    // ==========================================================

    private static Layout WrapFullscreen(IRenderable content)
    {
        // Invisible border
        var panel = new Panel(content)
            .Border(BoxBorder.None)
            .Padding(0, 0)
            .Expand();

        var layout = new Layout("root")
            .SplitRows(
                new Layout("content").Update(panel).Ratio(1)
            );

        return layout;
    }

    public static void SetCodes(string arm, string defuse)
    {
        if (!string.IsNullOrEmpty(arm))
            _armCode = arm;
        if (!string.IsNullOrEmpty(defuse))
            _defuseCode = defuse;
    }

    // ==============================================================
    // ==================== MAIN ENTRY ==============================
    // ==============================================================

    public static async Task<bool> StartGame(string borderColor)
    {
        _currentInput = "";
        _armed = false;
        _freezing = false;
        _freezeLeft = 25;

        await RunArmingScreen(borderColor);
        _currentInput = "";
        return await RunCountdown(borderColor);
    }

    // ==============================================================
    // ==================== ARMING SCREEN ===========================
    // ==============================================================

    private static async Task RunArmingScreen(string color)
    {
        var panel = BuildArmingPanel(color);

        await AnsiConsole.Live(WrapFullscreen(panel))
            .AutoClear(false)
            .StartAsync(async ctx =>
            {
                while (!_armed)
                {
                    HandleArmingInput();

                    panel = BuildArmingPanel(color);
                    ctx.UpdateTarget(WrapFullscreen(panel));

                    await Task.Delay(50);
                }
            });
    }

    private static void HandleArmingInput()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                if (_currentInput == _armCode)
                    _armed = true;

                _currentInput = "";
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (_currentInput.Length > 0)
                    _currentInput = _currentInput[..^1];
            }
            else if (!char.IsControl(key.KeyChar))
            {
                _currentInput += key.KeyChar;
            }
        }
    }

    private static Panel BuildArmingPanel(string color)
    {
        return new Panel(
            new RowsRenderable(
                new Align(new Markup("[bold red]IED PREPARATION[/]"), HorizontalAlignment.Center),
                new Align(new Markup("Enter arming code to start the device:\n"), HorizontalAlignment.Center),
                new Align(new Markup($"Code: [yellow]{_currentInput}[/]"), HorizontalAlignment.Center),
                new Align(new Markup("[grey](Press Enter to confirm)[/]"), HorizontalAlignment.Center)
            )
        )
        .Border(BoxBorder.None)
        .Padding(1, 1)
        .Expand();
    }

    // ==============================================================
    // ====================== COUNTDOWN =============================
    // ==============================================================

    private static async Task<bool> RunCountdown(string color)
    {
        int current = 0;
        int bombMs = 0;
        int freezeMs = 0;

        var panel = BuildCountdownPanel(current, color);

        return await AnsiConsole.Live(WrapFullscreen(panel))
            .AutoClear(false)
            .StartAsync(async ctx =>
            {
                while (true)
                {
                    await Task.Delay(50);
                    HandleCountdownInput();

                    if (_freezing)
                    {
                        freezeMs += 50;
                        if (freezeMs >= 1000)
                        {
                            freezeMs = 0;
                            _freezeLeft--;
                            if (_freezeLeft <= 0)
                                return true;
                        }
                    }
                    else
                    {
                        bombMs += 50;
                        if (bombMs >= 1000)
                        {
                            bombMs = 0;
                            current++;

                            if (current >= _bombTime)
                                return false;

                            if (File.Exists(_path))
                            {
                                var content = File.ReadAllText(_path);
                                if (!content.Contains("IED enabled", StringComparison.OrdinalIgnoreCase))
                                    return true;
                            }
                        }
                    }

                    panel = BuildCountdownPanel(current, color);
                    ctx.UpdateTarget(WrapFullscreen(panel));
                }
            });
    }

    private static void HandleCountdownInput()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);

            if (_freezing)
            {
                if (key.Key == ConsoleKey.Spacebar)
                {
                    _freezing = false;
                    _freezeLeft = 25;
                    _currentInput = "";
                }
                continue;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                if (_currentInput == _defuseCode)
                {
                    _freezing = true;
                    _freezeLeft = 25;
                }
                _currentInput = "";
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (_currentInput.Length > 0)
                    _currentInput = _currentInput[..^1];
            }
            else if (!char.IsControl(key.KeyChar))
            {
                _currentInput += key.KeyChar;
            }
        }
    }

    private static Panel BuildCountdownPanel(int current, string color)
    {
        // Fixed, safe width (never overflows)
        int barWidth = Math.Max(10, Math.Min(Console.WindowWidth - 20, 80));

        double progress = (double)current / _bombTime;
        progress = Math.Clamp(progress, 0.0, 1.0);

        int filled = (int)(progress * barWidth);
        string bar = new string('█', filled) + new string('─', barWidth - filled);

        string status = _freezing
            ? $"[green]DEFUSING... {_freezeLeft}s remaining (SPACE to cancel)[/]"
            : "[red]Bomb ticking...[/]";

        return new Panel(
            new RowsRenderable(
                new Align(new Markup("[bold red]IMPROVISED EXPLOSIVE DEVICE[/]"), HorizontalAlignment.Center),
                new Align(new Markup(status), HorizontalAlignment.Center),
                new Markup(""),
                new Align(new Markup($"[[{bar}]]  {current:D3}/{_bombTime:D3} s"), HorizontalAlignment.Center),
                new Markup(""),
                new Align(new Markup($"Enter code: [yellow]{_currentInput}[/]"), HorizontalAlignment.Center),
                new Align(new Markup("[grey](Press Enter to submit code)[/]"), HorizontalAlignment.Center)
            )
        )
        .Border(BoxBorder.None)
        .Padding(1, 1)
        .Expand();
    }

    // ==============================================================
    // ====================== END SCREEN ============================
    // ==============================================================

    public static void ShowEnd(bool defused)
    {
        string base64 = defused ? _saved : _exploded;
        string message;

        try
        {
            message = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }
        catch
        {
            message = defused ? "IED has been successfully defused!" : "IED exploded!";
        }

        var panel = new Panel(
            new RowsRenderable(
                new Align(new Markup(message), HorizontalAlignment.Center),
                new Markup(""),
                new Align(new Markup("[green]Press ENTER to exit[/]"), HorizontalAlignment.Center)
            )
        )
        .Border(BoxBorder.None)
        .Padding(1, 1)
        .Expand();

        AnsiConsole.Clear();
        AnsiConsole.Write(WrapFullscreen(panel));

        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

        File.AppendAllText(_logFile,
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Game Over! {(defused ? "Saved" : "Exploded")}\n");

        Console.CursorVisible = true;
    }
}

// ==============================================================
// ======================= PROGRAM MAIN ==========================
// ==============================================================

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.TreatControlCAsInput = true;
        Console.CancelKeyPress += (sender, e) => e.Cancel = true;

        string armCode = null;
        string defuseCode = null;

        foreach (var a in args)
        {
            if (a.StartsWith("--arm-code="))
                armCode = a.Substring("--arm-code=".Length);
            else if (a.StartsWith("--defuse-code="))
                defuseCode = a.Substring("--defuse-code=".Length);
        }

        IEDGameSystem.SetCodes(armCode, defuseCode);

        bool defused = await IEDGameSystem.StartGame("red");
        IEDGameSystem.ShowEnd(defused);

        Process.GetCurrentProcess().Kill();
    }
}

// ==============================================================
// =============== UTILITY – ROWS RENDERABLE ====================
// ==============================================================

public sealed class RowsRenderable : IRenderable
{
    private readonly IRenderable[] _rows;

    public RowsRenderable(params IRenderable[] rows)
    {
        _rows = rows;
    }

    public Measurement Measure(RenderOptions options, int maxWidth)
        => new Measurement(0, maxWidth);

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        foreach (var row in _rows)
            foreach (var segment in row.Render(options, maxWidth))
                yield return segment;
    }
}
