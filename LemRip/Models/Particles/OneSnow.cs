using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Particles
{
    internal class OneSnow : OneParticle
    {
        internal Vector2 Direction;
        internal Vector2 Pos;
        internal float DirectionTime;
        internal float Lifetime;
        internal Texture2D Sprite;

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Pos, new Rectangle(0, 0, 10, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.50001f);
        }

        internal override void Update(GameTime gameTime)
        {
            DirectionTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Lifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Pos += Direction;
            Pos.X -= DirectionTime;
            Pos.Y -= (float)GlobalConst.Rnd.NextDouble();
            if (DirectionTime < 0)
            {
                DirectionTime = (float)GlobalConst.Rnd.NextDouble() * 3;
            }
            if (Pos.Y > GlobalConst.GameResolution.Y)
                Pos.Y = 0;
        }
    }
}
