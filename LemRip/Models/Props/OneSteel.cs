using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props
{
    internal class OneSteel : OneProp
    {
        internal Rectangle Area;

        internal override ETypeTrap TypeTrap
        {
            get { return ETypeTrap.Steel; }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        internal override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
