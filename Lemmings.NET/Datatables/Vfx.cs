using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal class Vfx
{
    internal Effect Efecto { get; private set; }

    internal void Load(ContentManager content)
    {
        Efecto = content.Load<Effect>("efecto");
    }
}
