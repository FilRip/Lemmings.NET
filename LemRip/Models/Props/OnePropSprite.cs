using System;

using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OnePropSprite : OneProp
{
    internal int AxisX { get; set; }

    internal int AxisY { get; set; }

    internal Color Color { get; set; }

    internal int Framesecond { get; set; }

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

    internal override ETypeTrap TypeTrap
    {
        get { return ETypeTrap.Sprite; }
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
            Pos = new(Pos.X - Typescroll, Pos.Y);
            if (Pos.X < 0 - (Sprite.Width * Scale))
                Pos = new(GlobalConst.GameResolution.X, Pos.Y);
            if (Pos.X > GlobalConst.GameResolution.X)
                Pos = new(-100, Pos.Y);
            spriteBatch.Draw(Sprite, new Vector2(Pos.X, Pos.Y - MyGame.Instance.ScreenInGame.ScrollY),
            new Rectangle(sx1, sy1, swidth, sheight), Color,
                Rotation, Vector2.Zero, Scale, SpriteEffects.None, Depth);
        }
        else
        {
            if (Sprite.Name == "touch/arana") // 64x64 sprite frame size
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
