using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneAdd : OneProp
{
    internal int Framesecond, NumFrames;
    internal Rectangle AreaDraw;
    internal Texture2D Sprite;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.Add; }
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
