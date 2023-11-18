using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables
{
    internal class Fonts
    {
        public Texture2D Lemmings;
        public SpriteFont Standard;

        internal void LoadContent(ContentManager Content)
        {
            Lemmings = Content.Load<Texture2D>("lemmfont");
            Standard = Content.Load<SpriteFont>("spriteFont1");
        }
    }
}
