using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal class Vfx
{
    internal Effect Efecto { get; private set; }

    internal void Load(ContentManager content)
    {
        Efecto = content.Load<Effect>("efecto");
        MyGame.Instance.Vfx.Efecto.Parameters["rainbow"].SetValue(MyGame.Instance.ScreenMainMenu.MainMenuGfx.RainbowPic); //rainbowpic
    }
}
