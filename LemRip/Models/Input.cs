using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Models;

internal static class Input
{
    internal static KeyboardState PreviousKeyState { get; set; }
    internal static KeyboardState CurrentKeyState { get; set; }
    internal static MouseState PreviousMouseState { get; set; }
    internal static MouseState CurrentMouseState { get; set; }
    internal static GameTime CurrentGameTime { get; set; }

    internal static Vector2 CurrentMousePosition
    {
        get
        {
            return new Vector2(CurrentMouseState.Position.X - 17, CurrentMouseState.Position.Y - 17);
        }
    }

    internal static bool ShiftPressed
    {
        get
        {
            return (PreviousKeyState.IsKeyDown(Keys.LeftShift) || PreviousKeyState.IsKeyDown(Keys.RightShift));
        }
    }

    internal static void SetKeyboardState(KeyboardState newState)
    {
        PreviousKeyState = CurrentKeyState;
        CurrentKeyState = newState;
    }

    internal static void SetMouseState(MouseState newState)
    {
        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = newState;
    }
}
