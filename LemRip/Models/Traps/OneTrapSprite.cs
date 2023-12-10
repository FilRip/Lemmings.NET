using Lemmings.NET.Constants;
using Lemmings.NET.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Traps;

internal class OneTrapSprite : ITrap
{
    internal int AxisX { get; set; }

    internal int AxisY { get; set; }

    internal int ActFrame { get; set; }

    internal Color Color { get; set; }

    internal int Framesecond { get; set; }

    internal int Frame { get; set; }

    internal int ActVect { get; set; }

    internal Vector2 Dest { get; set; }

    internal Vector2 Center { get; set; }

    internal float Depth { get; set; }

    internal float Rotation { get; set; }

    internal float Scale { get; set; }

    internal float Typescroll { get; set; }

    internal float Speed { get; set; }

    internal bool MinusScrollX { get; set; }

    internal bool Minus { get; set; }

    internal bool Calc { get; set; }

    internal Texture2D Sprite { get; set; }

    internal Vector3[] Path { get; set; }

    public ETypeTrap TypeTrap
    {
        get { return ETypeTrap.Sprite; }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // TODO : Draw trap
    }
}
