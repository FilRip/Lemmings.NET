using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables
{
    internal class Mouse
    {
        public Texture2D MouseCross { get; private set; }
        public Texture2D MouseOverLemmings { get; private set; }

        public void LoadContent(ContentManager Content)
        {
            MouseOverLemmings = Content.Load<Texture2D>("raton_on1");
            MouseCross = Content.Load<Texture2D>("raton_off1");
        }
    }
}
