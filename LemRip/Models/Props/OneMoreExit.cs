using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneMoreExit : OneBaseProp
{
    internal Vector2 ExitMoreXY;
    private int X1, Y1, X2, Y2, X3, Y3, FrameAct;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.MoreExit; }
    }

    internal void Draw(SpriteBatch spriteBatch, int x1, int y1, int x2, int y2, int x3, int y3, int frameact)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
        X3 = x3;
        Y3 = y3;
        FrameAct = frameact;
        Draw(spriteBatch);
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        int output1X = (int)ExitMoreXY.X;
        int output1Y = (int)ExitMoreXY.Y;
        spriteBatch.Draw(MyGame.Instance.ScreenInGame.Salida_ani1_1, new Vector2(output1X - MyGame.Instance.ScreenInGame.ScrollX - X2, output1Y - Y2 - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(0, FrameAct, X1, Y1), Color.White,
            0f, Vector2.Zero, 1f, SpriteEffects.None, MyGame.Instance.ScreenInGame.DoorExitDepth);
        spriteBatch.Draw(MyGame.Instance.ScreenInGame.Salida_ani1, new Vector2(output1X - MyGame.Instance.ScreenInGame.ScrollX - X3, output1Y - Y3 - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(0, 0, MyGame.Instance.ScreenInGame.Salida_ani1.Width, MyGame.Instance.ScreenInGame.Salida_ani1.Height),
            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, MyGame.Instance.ScreenInGame.DoorExitDepth);

        if (MyGame.Instance.DebugOsd.Debug) //exits debug
        {
            Rectangle exit_rect = new(output1X - 5, output1Y - 5, 10, 10);
            spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(exit_rect.Left - MyGame.Instance.ScreenInGame.ScrollX, exit_rect.Top - MyGame.Instance.ScreenInGame.ScrollY, exit_rect.Width, exit_rect.Height), null,
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
    }

    internal override void Update()
    {
        throw new System.NotImplementedException();
    }
}
