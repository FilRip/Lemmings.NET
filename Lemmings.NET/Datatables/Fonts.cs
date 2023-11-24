using Microsoft.Xna.Framework;
using System;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables
{
    internal class Fonts
    {
        public Texture2D Lemmings { get; private set; }
        public SpriteFont Standard { get; private set; }

        public void TextLem(string txt, Vector2 start, Color pinta, float size, float layer, SpriteBatch spriteBatch)
        {
            for (int i = 0; i <= txt.Length - 1; i++)
            {
                int j = Convert.ToInt32(txt[i]);
                start.X += 19 * size;  // ancho de lemfont (18X26) 18+1 para dejar espacio entre chars
                if (j == 32)
                    continue;
                Rectangle rectangleFill;
                rectangleFill.X = 0;
                rectangleFill.Y = 26 * (j - 33);
                rectangleFill.Width = 18;
                rectangleFill.Height = 26;
                spriteBatch.Draw(Lemmings, start, rectangleFill, pinta, 0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }
        }

        internal void LoadContent(ContentManager content)
        {
            Lemmings = content.Load<Texture2D>("lemmfont");
            Standard = content.Load<SpriteFont>("spriteFont1");
        }
    }
}
