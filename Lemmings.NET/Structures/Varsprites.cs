using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs;

internal struct Varsprites
{
    internal int AxisX, AxisY, ActFrame, Transparency, R, G, B, Framesecond, Frame, ActVect;
    internal Vector2 Pos, Dest, Center;
    internal float Depth, Rotation, Scale, Typescroll, Speed;
    internal bool MinusScrollx, Minus, Calc;
    internal Texture2D Sprite;
    internal Vector3[] Path;
}
