using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables
{
    internal class InGameMenuGfx
    {
        internal Texture2D Logo666 { get; private set; }

        internal void Load(ContentManager content)
        {
            Logo666 = content.Load<Texture2D>("fondos/star2");
        }
    }
}
