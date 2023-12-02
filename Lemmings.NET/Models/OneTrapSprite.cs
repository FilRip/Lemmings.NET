using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lemmings.NET.Models
{
    internal class OneTrapSprite
    {
        internal int AxisX { get; set; }
        internal int AxisY { get; set; }
        internal int ActFrame { get; set; }
        internal int Transparency { get; set; }
        internal int R { get; set; }
        internal int G { get; set; }
        internal int B { get; set; }
        internal int Framesecond { get; set; }
        internal int Frame { get; set; }
        internal int ActVect { get; set; }
        internal Vector2 Pos { get; set; }
        internal Vector2 Dest { get; set; }
        internal Vector2 Center { get; set; }
        internal float Depth { get; set; }
        internal float Rotation { get; set; }
        internal float Scale { get; set; }
        internal float Typescroll { get; set; }
        internal float Speed { get; set; }
        internal bool MinusScrollx { get; set; }
        internal bool Minus { get; set; }
        internal bool Calc { get; set; }
        internal Texture2D Sprite { get; set; }
        internal Vector3[] Path { get; set; }
    }
}
