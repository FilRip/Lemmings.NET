using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OnePlat : OneBaseProp
{
    internal int Framesecond, NumSteps, ActStep, Step;
    internal bool Up;
    internal Rectangle AreaDraw;
    internal Texture2D Sprite;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.Plat; }
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
