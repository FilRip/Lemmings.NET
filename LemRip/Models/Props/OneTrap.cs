using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props
{
    internal class OneTrap
    {
        internal int Type, VvX, VvY, NumFrames, ActFrame, Vvscroll;
        internal Color Color;
        internal Rectangle AreaDraw, AreaTrap;
        internal Vector2 Pos;
        internal float Depth;
        internal bool IsOn;
        internal Texture2D Sprite;
    }
}
