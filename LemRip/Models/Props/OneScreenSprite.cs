using System;

using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneScreenSprite : OneBaseProp
{
    internal int AxisX;
    internal int AxisY;
    internal Color Color;
    internal int Framesecond;
    internal int ActVect;
    internal Vector2 Dest;
    internal Vector2 Center;
    internal float Depth;
    internal float Rotation;
    internal float Scale;
    internal float Typescroll;
    internal float Speed;
    internal bool MinusScrollX;
    internal bool Minus;
    internal bool Calc;
    internal Texture2D Sprite;
    internal Vector3[] Path;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.Sprite; }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        int swidth = Sprite.Width / AxisX;
        int sheight = Sprite.Height / AxisY;
        int sx1 = 0;
        int sy1 = 0;

        if (ActFrame != 0)
        {
            sx1 = swidth * (ActFrame % AxisX);
            sy1 = sheight * (ActFrame / AxisX);
        }
        if (Typescroll > 0)
        {
            Pos.X -= Typescroll;
            if (Pos.X < 0 - (Sprite.Width * Scale))
                Pos.X = GlobalConst.GameResolution.X;
            if (Pos.X > GlobalConst.GameResolution.X)
                Pos.X = -100;
            spriteBatch.Draw(Sprite, new Vector2(Pos.X, Pos.Y - MyGame.Instance.ScreenInGame.ScrollY),
                new Rectangle(sx1, sy1, swidth, sheight), Color,
                Rotation, Vector2.Zero, Scale, SpriteEffects.None, Depth);
        }
        else
        {
            if (Sprite.Name == MyGame.Instance.Gfx.Spider.Name) // 64x64 sprite frame size
            {
                int xxAnim;
                if (MinusScrollX)
                    xxAnim = (int)Pos.X - MyGame.Instance.ScreenInGame.ScrollX + 32;
                else
                    xxAnim = (int)Pos.X + 32;
                spriteBatch.Draw(Sprite, new Vector2(xxAnim, Pos.Y - MyGame.Instance.ScreenInGame.ScrollY - 32),
                    new Rectangle(sx1, sy1, swidth, sheight), Color,
                    Rotation, Center, Scale, SpriteEffects.None, Depth);
            }
            else
            {
                int xxAnim;
                if (MinusScrollX)
                    xxAnim = (int)Pos.X - MyGame.Instance.ScreenInGame.ScrollX;
                else
                    xxAnim = (int)Pos.X;
                spriteBatch.Draw(Sprite, new Vector2(xxAnim, Pos.Y - MyGame.Instance.ScreenInGame.ScrollY),
                    new Rectangle(sx1, sy1, swidth, sheight), Color,
                    Rotation, Vector2.Zero, Scale, SpriteEffects.None, Depth);
            }
        }
    }

    internal override void Update()
    {
        Frame++;
        if (Sprite.Name == MyGame.Instance.Gfx.Fire.Name && Frame > Framesecond)
        {
            Frame = 0;
            if (Minus)
                ActFrame -= 2;
            else
                ActFrame++; // 2 frames less to return to zero better effect i think

            if (ActFrame > 14 && !Minus)
            {
                ActFrame = 15;
                Minus = true;
            }
            if (ActFrame < 0 && Minus)
            {
                Minus = false;
                ActFrame = 1;
            }
            return;
        }
        if (Frame > Framesecond)
        {
            Frame = 0;
            ActFrame++;
            if (ActFrame > (AxisX * AxisY) - 1)
                ActFrame = 0;
        }
        if (Speed != 0)  // spider destination puto puto puto
        {
            if (Calc)
            {
                Calc = false;
                if (!Minus)
                {
                    Pos = new(Path[ActVect].X, Path[ActVect].Y);
                    Dest = new(Path[ActVect + 1].X, Path[ActVect + 1].Y);
                    Speed = Path[ActVect].Z;
                }
                else
                {
                    Pos = new(Path[ActVect + 1].X, Path[ActVect + 1].Y);
                    Dest = new(Path[ActVect].X, Path[ActVect].Y);
                    Speed = Path[ActVect].Z;
                }
                if (!Minus)
                    ActVect++;
                else
                    ActVect--;
                if (ActVect > Path.Length - 2 && !Minus)
                {
                    ActVect--;
                    Minus = true;
                }
                if (ActVect < 0 && Minus)
                {
                    ActVect++;
                    Minus = false;
                }

                return;
            }
            Vector2 direction_sprite = Vector2.Normalize(Dest - Pos);
            Pos += direction_sprite * Speed;
            float distance = Vector2.Distance(Pos, Dest);
            if (distance < 1)
            {
                Calc = true;
                return;
            }
            Rotation = (float)Math.Atan2(direction_sprite.X, direction_sprite.Y) * -1;
        }
    }
}
