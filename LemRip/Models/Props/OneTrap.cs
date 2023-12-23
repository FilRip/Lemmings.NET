using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneTrap : OneProp
{
    internal int Type, VvX, VvY, NumFrames, Vvscroll;
    internal Color Color;
    internal Rectangle AreaDraw, AreaTrap;
    internal float Depth;
    internal bool IsOn;
    internal Texture2D Sprite;

    internal OneTrap()
    {
        Color = new Color(255, 255, 255, 255);
    }

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.Trap; }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        int tYheight = Sprite.Height / NumFrames;
        if (Type != 555 && Type != 666)
        {
            int vv444 = 0;
            switch (Vvscroll)
            {
                case 1:
                    vv444 = MyGame.Instance.ScreenInGame.Z1;
                    break;
                case 2:
                    vv444 = -MyGame.Instance.ScreenInGame.Z1;
                    break;
                default:
                    break;
            }
            Rectangle rectangleFill = new()
            {
                X = AreaDraw.X - MyGame.Instance.ScreenInGame.ScrollX,
                Y = AreaDraw.Y - MyGame.Instance.ScreenInGame.ScrollY,
                Width = AreaDraw.Width,
                Height = tYheight,
            };
            Rectangle rectangleFill2 = new()
            {
                X = 0 + vv444,
                Y = tYheight * ActFrame,
                Width = AreaDraw.Width,
                Height = tYheight,
            };
            spriteBatch.Draw(Sprite, rectangleFill, rectangleFill2, Color, 0f, Vector2.Zero, SpriteEffects.None, Depth);
        }
        else
        {
            int spY = Sprite.Height / NumFrames;
            Rectangle rectangleFill = new()
            {
                X = (int)Pos.X - MyGame.Instance.ScreenInGame.ScrollX - VvX,
                Y = (int)Pos.Y - VvY - MyGame.Instance.ScreenInGame.ScrollY,
                Width = Sprite.Width,
                Height = spY,
            };
            Rectangle rectangleFill2 = new()
            {
                X = 0,
                Y = spY * ActFrame,
                Width = Sprite.Width,
                Height = spY,
            };
            spriteBatch.Draw(Sprite, rectangleFill, rectangleFill2, Color, 0f, Vector2.Zero, SpriteEffects.None, Depth);
        }
        if (MyGame.Instance.DebugOsd.Debug)
        {
            spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(AreaTrap.Left - MyGame.Instance.ScreenInGame.ScrollX,
                AreaTrap.Top - MyGame.Instance.ScreenInGame.ScrollY, AreaTrap.Width, AreaTrap.Height), null, new Color(255, 255, 255, 140),
                0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
    }

    internal override void Update()
    {
        if (!IsOn)
        {
            ActFrame++;
            if (ActFrame > NumFrames - 1)
                ActFrame = 0;
            if (Type == 666)
                ActFrame = 0;
        }
        else
        {
            ActFrame++;
            if (ActFrame > NumFrames - 1)
            {
                IsOn = false;
                ActFrame = 0;
            }
        }
    }
}
