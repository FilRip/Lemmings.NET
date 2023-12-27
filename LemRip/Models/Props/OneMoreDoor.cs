using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneMoreDoor : OneBaseProp
{
    internal Vector2 DoorMoreXY;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.MoreDoor; }
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
