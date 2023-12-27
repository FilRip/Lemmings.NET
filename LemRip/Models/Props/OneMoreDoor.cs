using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneMoreDoor : OneBaseProp
{
    internal Vector2 DoorMoreXY;
    private int X, Y;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.MoreDoor; }
    }

    internal void Draw(SpriteBatch spriteBatch, int x, int y)
    {
        X = x;
        Y = y;
        Draw(spriteBatch);
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(MyGame.Instance.ScreenInGame.AnimatedDoor,
                         new Vector2((int)DoorMoreXY.X - MyGame.Instance.ScreenInGame.ScrollX, (int)DoorMoreXY.Y - MyGame.Instance.ScreenInGame.ScrollY),
                         new Rectangle(0, MyGame.Instance.ScreenInGame.FrameReal565, X, Y),
            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, MyGame.Instance.ScreenInGame.DoorExitDepth);
    }

    internal override void Update()
    {
        throw new System.NotImplementedException();
    }
}
