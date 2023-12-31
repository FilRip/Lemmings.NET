﻿using System.Collections.Generic;

using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal class MainMenuGfx
{
    internal Texture2D RainbowPic { get; private set; }
    internal Color[] Looplogo { get; set; } = new Color[100 * 100];
    internal Color[] Looplogo2 { get; set; } = new Color[100 * 100];
    internal RenderTarget2D Colors88 { get; set; }
    internal RenderTarget2D Normals { get; set; }
    internal Texture2D mainMenuSign, mainMenuSign2, ranksign1, ranksign2, ranksign3, ranksign5, ranksign6, Mascaraexplosion;
    internal Dictionary<ELevelCategory, Texture2D> MiniLevels { get; set; }

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
        MiniLevels = new Dictionary<ELevelCategory, Texture2D>()
        {
            { ELevelCategory.Fun, content.Load<Texture2D>("levels/mini_levels1") },
            { ELevelCategory.Tricky, content.Load<Texture2D>("levels/mini_levels2") },
            { ELevelCategory.Taxing, content.Load<Texture2D>("levels/mini_levels3") },
            { ELevelCategory.Mayhem, content.Load<Texture2D>("levels/mini_levels4") },
            { ELevelCategory.Bonus, content.Load<Texture2D>("levels/mini_levels5") },
        };
    }
}
