using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models
{
    internal struct Varsprites
    {
        internal int axisX, axisY, actFrame, transparency, R, G, B, framesecond, frame, actVect;
        internal Vector2 pos, dest, center;
        internal float depth, rotation, scale, typescroll, speed;
        internal bool minusScrollx, minus, calc;
        internal Texture2D sprite;
        internal Vector3[] path;
    }
}
