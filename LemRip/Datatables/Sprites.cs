using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal class Sprites
{
    internal Texture2D Climber { get; private set; }
    internal Texture2D Builder { get; private set; }
    internal Texture2D Walker { get; private set; }
    internal Texture2D Blocker { get; private set; }
    internal Texture2D Digger { get; private set; }
    internal Texture2D Falling { get; private set; }
    internal Texture2D Exploder { get; private set; }
    internal Texture2D Drowner { get; private set; }
    internal Texture2D EyeBlink1 { get; private set; }
    internal Texture2D EyeBlink2 { get; private set; }
    internal Texture2D EyeBlink3 { get; private set; }
    internal Texture2D Circulo_led { get; private set; }
    internal Texture2D Avanzar { get; private set; }
    internal Texture2D Mas { get; private set; }
    internal Texture2D Menos { get; private set; }
    internal Texture2D Paraguas { get; private set; }
    internal Texture2D Puente { get; private set; }
    internal Texture2D Pausa { get; private set; }
    internal Texture2D HorizontalDigger { get; private set; }
    internal Texture2D Miner { get; private set; }
    internal Texture2D Bomba { get; private set; }
    internal Texture2D Flattened { get; private set; }
    internal Texture2D Water2 { get; private set; }
    internal Texture2D Nubes_2 { get; private set; }
    internal Texture2D Nubes { get; private set; }
    internal Texture2D Cuadrado_menu { get; private set; }
    internal Texture2D Chink { get; private set; }
    internal Texture2D Lohno { get; private set; }

    internal void LoadContent(ContentManager content)
    {
        Walker = content.Load<Texture2D>("walker_ok");
        Falling = content.Load<Texture2D>("cae_ok");
        Digger = content.Load<Texture2D>("cavar_ok");
        Climber = content.Load<Texture2D>("escala");
        Exploder = content.Load<Texture2D>("explota");
        Blocker = content.Load<Texture2D>("blocker");
        Drowner = content.Load<Texture2D>("ahoga");
        EyeBlink1 = content.Load<Texture2D>("lem1/blink1");
        EyeBlink2 = content.Load<Texture2D>("lem1/blink2");
        EyeBlink3 = content.Load<Texture2D>("lem1/blink3");
        Circulo_led = content.Load<Texture2D>("circulo_brillante");
        Avanzar = content.Load<Texture2D>("avanzar");
        Mas = content.Load<Texture2D>("mas");
        Menos = content.Load<Texture2D>("menos");
        Paraguas = content.Load<Texture2D>("paraguas");
        Puente = content.Load<Texture2D>("puente");
        HorizontalDigger = content.Load<Texture2D>("pared");
        Flattened = content.Load<Texture2D>("rompesuelo");
        Miner = content.Load<Texture2D>("pico");
        Pausa = content.Load<Texture2D>("pausa");
        Bomba = content.Load<Texture2D>("bomba");
        Cuadrado_menu = content.Load<Texture2D>("border");
        Water2 = content.Load<Texture2D>("Animations/water2");
        Nubes_2 = content.Load<Texture2D>("fondos/nubes2");
        Nubes = content.Load<Texture2D>("fondos/nubes");
        Chink = content.Load<Texture2D>("sprite/chink");
        Lohno = content.Load<Texture2D>("sprite/ohno");
    }
}
