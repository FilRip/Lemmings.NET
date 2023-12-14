using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models;

internal abstract class OneTrap
{
    internal abstract ETypeTrap TypeTrap { get; }

    internal Vector2 Pos { get; set; }

    internal abstract void Draw(SpriteBatch spriteBatch);

    internal abstract void Update();
}
