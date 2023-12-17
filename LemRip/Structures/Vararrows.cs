using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs;

internal struct Vararrows
{
    internal Rectangle Area;
    internal bool Right;
    internal Texture2D Arrow, EnvelopArrow;
    internal int Moving, Transparency;
}
