using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models
{
    internal struct Vartraps
    {
        internal int type, vvX, vvY, numFrames, actFrame, vvscroll;
        internal byte R, G, B, transparency;
        internal Rectangle areaDraw, areaTrap;
        internal Vector2 pos;
        internal float depth;
        internal bool isOn;
        internal Texture2D sprite;
    }
}
