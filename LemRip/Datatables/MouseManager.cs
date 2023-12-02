using Lemmings.NET.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal class MouseManager
{
    internal Texture2D MouseCross { get; private set; }
    internal Texture2D MouseOverLemmings { get; private set; }

    internal void LoadContent(ContentManager content)
    {
        MouseOverLemmings = content.Load<Texture2D>("raton_on1");
        MouseCross = content.Load<Texture2D>("raton_off1");
    }

    internal void Draw(SpriteBatch spriteBatch, bool MouseOnLem = false, bool withOffset = false)
    {
        spriteBatch.Draw((MouseOnLem ? MouseOverLemmings : MouseCross), new Vector2(Input.CurrentMouseState.X - (withOffset ? 17 : 0), Input.CurrentMouseState.Y - (withOffset ? 17 : 0)), new Rectangle(0, 0, 34, 34), Color.White, 0f, Vector2.Zero,
            1f, SpriteEffects.None, 0f);
    }
}
