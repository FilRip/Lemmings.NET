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
        int x2 = AreaDraw.X - AreaDraw.Width / 2;
        int y = AreaDraw.Y;
        int w = Sprite.Width;
        int h = Sprite.Height;
        spriteBatch.Draw(Sprite, new Rectangle(x2 - MyGame.Instance.ScreenInGame.ScrollX, y - MyGame.Instance.ScreenInGame.ScrollY - 5, AreaDraw.Width, AreaDraw.Height),
            new Rectangle(0, 0, w, h), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.56f);
    }

    internal override void Update()
    {
        if (Frame > Framesecond)
        {
            bool goUP = Up;
            Frame = 0;
            if (goUP)
                ActStep++;
            else
                ActStep--;
            if (goUP)
                AreaDraw.X = AreaDraw.Y - Step;
            else
                AreaDraw.Y += Step;
            if (ActStep >= NumSteps - 1)
                Up = false;
            if (ActStep < 1)
                Up = true;
            int px = AreaDraw.X - (AreaDraw.Width / 2);
            int alto = Step * NumSteps;
            int positioYOrig = AreaDraw.Y + (ActStep * Step);
            bool realLine = false;
            for (int y55 = 0; y55 < alto; y55++)
            {
                for (int x55 = 0; x55 < AreaDraw.Width; x55++)
                {
                    if (y55 == (alto - 1) - ActStep * Step)
                        realLine = true;
                    if (realLine)
                    {
                        MyGame.Instance.ScreenInGame.LevelOverlay[((positioYOrig - (alto - y55)) * MyGame.Instance.ScreenInGame.Earth.Width) + x55 + px] = Color.White;
                    }
                    else
                    {
                        MyGame.Instance.ScreenInGame.LevelOverlay[((positioYOrig - (alto - y55)) * MyGame.Instance.ScreenInGame.Earth.Width) + x55 + px] = Color.Transparent;

                    }
                }
            }
            if (MyGame.Instance.DebugOsd.Debug)
            {
                MyGame.Instance.ScreenInGame.Earth.SetData(MyGame.Instance.ScreenInGame.LevelOverlay,
                                                           0,
                                                           MyGame.Instance.ScreenInGame.Earth.Width * MyGame.Instance.ScreenInGame.Earth.Height); //set this only for debugger and see the real c25 redraw
            }
        }
        Frame++;
    }
}
