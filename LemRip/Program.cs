using System;
using System.Windows.Forms;

namespace Lemmings.NET;

internal static class Program
{
    [STAThread()]
    internal static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        using var game = new MyGame();
        game.Run();
    }
}
