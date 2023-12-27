using System;

using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Particles
{
    internal class OneExplosion : OneParticle
    {
        internal double x, y, dx, dy;
        internal Color Color;
        internal int LifeCtr;
        internal int MaxCounter;
        internal int Counter;
        internal float Rotation;
        internal float Size;
        internal Vector2 vectorFill;
        private int TopY;

        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (LifeCtr < 0)
                return;

            vectorFill.X = (float)x - MyGame.Instance.ScreenInGame.ScrollX;
            vectorFill.Y = (float)y - MyGame.Instance.ScreenInGame.ScrollY;
            spriteBatch.Draw(MyGame.Instance.Gfx.Explosion_particle, vectorFill, new Rectangle(0, 0, MyGame.Instance.Gfx.Explosion_particle.Width, MyGame.Instance.Gfx.Explosion_particle.Height), Color,
                Rotation, Vector2.Zero, Size, SpriteEffects.None, 0.300f);
            Rotation += 0.03f;
            Size += 0.01f;
        }

        internal bool Update(GameTime gameTime, int topY)
        {
            TopY = topY;
            bool result = LifeCtr == -100;
            Update(gameTime);
            return result;
        }

        internal override void Update(GameTime gameTime)
        {
            if (LifeCtr > 0)
            {
                //this change alpha channel from half life and fade out every particle
                int xx33 = LifeCtr;
                int yy33 = Counter;
                int xx55 = (xx33 + yy33) / 2;
                if (yy33 > xx55)
                {
                    yy33 -= xx55;
                    int yy55 = yy33 * 100 / xx55;
                    yy55 *= 2;
                    if (yy55 > 255)
                        yy55 = 255;
                    Color.A = Convert.ToByte(255 - yy55);
                }
                //calculate new position
                x += dx;
                y += dy + Counter * GlobalConst.GRAVITY;
                if (y > TopY)
                {
                    //explosion[qexplo, iexplo].y = topY;  //bottom of drawable sets y to max
                    LifeCtr = -100;  //bottom of drawable area kills particle
                }
                // check life counter
                if (LifeCtr > 0)
                    LifeCtr--;
                if (LifeCtr == 0)
                    LifeCtr = -100;
            }
        }
    }
}
