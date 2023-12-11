using Lemmings.NET.Constants;

using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Interfaces;

internal interface ITrap
{
    ETypeTrap TypeTrap { get; }

    void Draw(SpriteBatch spriteBatch);

    void Update();
}
