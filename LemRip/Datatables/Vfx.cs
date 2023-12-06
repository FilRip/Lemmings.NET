using Microsoft.Xna.Framework.Content;

namespace Lemmings.NET.Datatables;

internal class Vfx
{
#pragma warning disable S125, IDE0060, CA1822 // Sections of code should not be commented out
    //internal Effect Efecto { get; private set; }

    internal void Load(ContentManager content)
    {
        /*Efecto = content.Load<Effect>("efecto");
        MyGame.Instance.Vfx.Efecto.Parameters["rainbow"].SetValue(MyGame.Instance.ScreenMainMenu.MainMenuGfx.RainbowPic); //rainbowpic*/
    }
#pragma warning restore S125, IDE0060, CA1822 // Sections of code should not be commented out
}
