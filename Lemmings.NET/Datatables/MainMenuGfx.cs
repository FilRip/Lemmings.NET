using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal class MainMenuGfx
{
    public Texture2D RainbowPic { get; private set; }
    public Color[] Looplogo { get; set; } = new Color[100 * 100];
    public Color[] Looplogo2 { get; set; } = new Color[100 * 100];
    public RenderTarget2D Colors88 { get; set; }
    public RenderTarget2D Normals { get; set; }
    public Texture2D mainMenuSign, mainMenuSign2, ranksign1, ranksign2, ranksign3, ranksign5, ranksign6, Mascaraexplosion;

    public void Load(GraphicsDevice GraphicsDevice, ContentManager content)
    {
        RainbowPic = content.Load<Texture2D>("surge-rainbow"); // texture to the effect shine shader // test -> surge-rainbow2 - surge-rainbow3 - surge-rainbow
        RainbowPic.GetData(Looplogo, 0, RainbowPic.Width * RainbowPic.Height);
        int widthl = GraphicsDevice.PresentationParameters.BackBufferWidth;
        int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
        Colors88 = new RenderTarget2D(GraphicsDevice, widthl, height);
        Normals = new RenderTarget2D(GraphicsDevice, widthl, height);
        mainMenuSign2 = content.Load<Texture2D>("cubo");
        mainMenuSign = content.Load<Texture2D>("lem1/menusign_04");
        ranksign1 = content.Load<Texture2D>("lem1/ranksign_01");
        ranksign2 = content.Load<Texture2D>("lem1/ranksign_02");
        ranksign3 = content.Load<Texture2D>("lem1/ranksign_03");
        ranksign5 = content.Load<Texture2D>("lem1/ranksign_05");
        ranksign6 = content.Load<Texture2D>("lem1/ranksign_06");
        Mascaraexplosion = content.Load<Texture2D>("mascara_explode");
    }
}
