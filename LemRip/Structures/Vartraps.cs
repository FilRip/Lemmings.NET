using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs;

internal struct Vartraps
{
    internal int Type, VvX, VvY, NumFrames, ActFrame, Vvscroll;
    internal byte R, G, B, Transparency;
    internal Rectangle AreaDraw, AreaTrap;
    internal Vector2 Pos;
    internal float Depth;
    internal bool IsOn;
    internal Texture2D Sprite;

    internal void SetIsOn(bool value)
    {
        IsOn = value;
    }
}
