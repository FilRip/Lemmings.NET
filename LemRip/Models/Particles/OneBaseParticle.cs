using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Particles;

internal abstract class OneBaseParticle
{
    internal abstract void Draw(SpriteBatch spriteBatch);

    internal abstract void Update(GameTime gameTime);
}
