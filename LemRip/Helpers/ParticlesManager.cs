using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Helpers;

internal class ParticlesManager
{
    internal Vector2 Direction { get; set; }
    internal Vector2 Pos { get; set; }
    internal void SetPosX(float x)
    {
        Pos = new Vector2(x, Pos.Y);
    }
    internal void SetPosY(float y)
    {
        Pos = new Vector2(Pos.X, y);
    }
    internal float DirectionTime { get; set; }
    internal float Lifetime { get; set; }
    internal Texture2D Sprite { get; set; }
}
