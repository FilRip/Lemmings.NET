using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs;

public struct Particles
{
    public Vector2 Direction { get; set; }
    public Vector2 Pos { get; set; }
    public void SetPosX(float x)
    {
        Pos = new Vector2(x, Pos.Y);
    }
    public void SetPosY(float y)
    {
        Pos = new Vector2(Pos.X, y);
    }
    public float DirectionTime { get; set; }
    public float Lifetime { get; set; }
    public Texture2D Sprite { get; set; }

}
