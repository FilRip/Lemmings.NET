using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Models;

internal static class Input
{
    public static KeyboardState PreviousKeyState { get; set; }
    public static KeyboardState CurrentKeyState { get; set; }
    public static MouseState PreviousMouseState { get; set; }
    public static MouseState CurrentMouseState { get; set; }
    public static GameTime CurrentGameTime { get; set; }

    public static void SetKeyboardState(KeyboardState newState)
    {
        PreviousKeyState = CurrentKeyState;
        CurrentKeyState = newState;
    }

    public static void SetMouseState(MouseState newState)
    {
        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = newState;
    }
}
