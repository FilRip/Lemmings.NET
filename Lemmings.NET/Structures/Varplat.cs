using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs
{
    internal struct Varplat
    {
        public int frame, framesecond, numSteps, actStep, step;
        public bool up;
        public Rectangle areaDraw;
        public Texture2D sprite;
    }
}
