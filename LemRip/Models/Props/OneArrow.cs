using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneArrow : OneProp
{
    internal Rectangle Area;
    internal bool Right;
    internal Texture2D Arrow, EnvelopArrow;
    internal int Moving, Transparency;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.Arrow; }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    internal override void Update()
    {
        throw new System.NotImplementedException();
    }
}
