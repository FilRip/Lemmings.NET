using System;

using Lemmings.NET.Constants;
using Lemmings.NET.Datatables;
using Lemmings.NET.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens;

internal class MainMenu
{
    private ELevelCategory _levelCategory;
    private int loopcolor = 0, dibujaloop = 1;
    private readonly MainMenuGfx _mainMenuGfx;
    private float frameWater = 0;
    private int _mouseLevelChoose;
    private OneLevel _currentMouseLevel;
    private float peakheight = 25;
    private int framblink1 = 0, framblink2 = 0, framblink3 = 0;
    private readonly int mmstartx, mmstarty, mmX;
    private bool Updown = true;
    private int levelACT;
    private bool blink1on = false, blink2on = false, blink3on = false;
    private readonly Rectangle mm1, mm2, mm3, mm4, mm5, mm6;

    internal MainMenuGfx MainMenuGfx
    {
        get { return _mainMenuGfx; }
    }
    public int MouseLevelChoose
    {
        get { return _mouseLevelChoose; }
        set
        {
            if (_mouseLevelChoose != value)
            {
                _mouseLevelChoose = value;
                _currentMouseLevel = MyGame.Instance.Levels.GetLevel(value);
            }
        }
    }

    internal MainMenu()
    {
        _mainMenuGfx = new MainMenuGfx();
        LoadGfx();
        mmstartx = 5;
        mmstarty = 80;
        mmX = 135;
        mm1 = new(mmstartx, mmstarty, _mainMenuGfx.mainMenuSign.Width, _mainMenuGfx.mainMenuSign.Height);
        mm2 = new(mmstartx, mmstarty + 100, _mainMenuGfx.mainMenuSign.Width, _mainMenuGfx.mainMenuSign.Height);
        mm3 = new(mmstartx, mmstarty + 200, _mainMenuGfx.mainMenuSign.Width, _mainMenuGfx.mainMenuSign.Height);
        mm4 = new(mmstartx, mmstarty + 300, _mainMenuGfx.mainMenuSign.Width, _mainMenuGfx.mainMenuSign.Height);
        mm5 = new(mmstartx, mmstarty + 400, _mainMenuGfx.mainMenuSign.Width, _mainMenuGfx.mainMenuSign.Height);
        mm6 = new(mmstartx, mmstarty + 500, _mainMenuGfx.mainMenuSign.Width, _mainMenuGfx.mainMenuSign.Height);
    }

    internal void LoadGfx()
    {
        _mainMenuGfx.Load(MyGame.Instance.GraphicsDevice, MyGame.Instance.Content);
    }

    internal void BackToMenu()
    {
        _levelCategory = ELevelCategory.None;
    }

    internal void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
    {
        if (MyGame.Instance.Music.WinMusic.State == SoundState.Playing)
            MyGame.Instance.Music.WinMusic.Stop();
        // rainbow over lemmings logo text into rendertarget
        graphics.SetRenderTarget(_mainMenuGfx.Colors88);
        graphics.Clear(ClearOptions.Target, Color.Transparent, 1.0f, 0);
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

        if (dibujaloop % 5 == 0) //surge-rainbowpic is 45x75 px
        {
            loopcolor++;
            if (loopcolor > 44)
                loopcolor = 1;
            _mainMenuGfx.RainbowPic.SetData(_mainMenuGfx.Looplogo, 0, 45 * 75); // init full logo to apply second mask
            _mainMenuGfx.RainbowPic.GetData(0, new Rectangle(loopcolor, 0, 45 - loopcolor, 75), _mainMenuGfx.Looplogo2, 0, (45 - loopcolor) * 75);
            _mainMenuGfx.RainbowPic.SetData(0, new Rectangle(0, 0, 45 - loopcolor, 75), _mainMenuGfx.Looplogo2, 0, (45 - loopcolor) * 75);

            _mainMenuGfx.RainbowPic.GetData(0, new Rectangle(0, 0, loopcolor, 75), _mainMenuGfx.Looplogo2, 0, loopcolor * 75);
            _mainMenuGfx.RainbowPic.SetData(0, new Rectangle(45 - loopcolor, 0, loopcolor, 75), _mainMenuGfx.Looplogo2, 0, loopcolor * 75);
        }
        MyGame.Instance.Vfx.Efecto.Parameters["rainbow"].SetValue(_mainMenuGfx.RainbowPic); //rainbowpic
        MyGame.Instance.Vfx.Efecto.CurrentTechnique.Passes[0].Apply();
        spriteBatch.Draw(MyGame.Instance.Gfx.Text, new Vector2(0, 0), Color.White);
        spriteBatch.End();

        // light NMAP effect over lemmings logo with mouse pos into other rendertarget
        Vector2 cratePosition = new(215, 20);
        // Draw all the normals, in the same place as the textures
        graphics.SetRenderTarget(_mainMenuGfx.Normals);
        graphics.Clear(ClearOptions.Target, new Color(128, 128, 255, 255), 1.0f, 0); // Clear the target with the default normal, pointing up (0, 0, 1)
        graphics.Clear(ClearOptions.Target,
            new Color(128, 128, 255, 255), 1.0f, 0); // Clear the target with the default normal, pointing up (0, 0, 1)
        spriteBatch.Begin();
        spriteBatch.Draw(MyGame.Instance.Gfx.CrateNormals, cratePosition, Color.White);
        spriteBatch.End();
        graphics.SetRenderTarget(MyGame.Instance.MainRenderTarget);

        //normal target
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null);
        graphics.Clear(Color.Black); //new Color(255, 0, 255, 255)
        Point x = new(Input.CurrentMouseState.Position.X, Input.CurrentMouseState.Position.Y);
        ELevelCategory previousLevelCat = _levelCategory;
        if (mm1.Contains(x))
        {
            _levelCategory = ELevelCategory.Fun;
        }
        else if (mm2.Contains(x))
        {
            _levelCategory = ELevelCategory.Tricky;
        }
        else if (mm3.Contains(x))
        {
            _levelCategory = ELevelCategory.Taxing;
        }
        else if (mm4.Contains(x))
        {
            _levelCategory = ELevelCategory.Mayhem;
        }
        else if (mm5.Contains(x))
        {
            _levelCategory = ELevelCategory.Bonus;
        }
        else if (mm6.Contains(x))
        {
            _levelCategory = ELevelCategory.User;
        }
        if (_levelCategory != previousLevelCat)
        {
            MouseLevelChoose = 0;
        }
        spriteBatch.Draw(MyGame.Instance.Gfx.Logo_fondo, new Rectangle(0, 0, GlobalConst.GameResolution.X, GlobalConst.GameResolution.Y), new Rectangle(0, 0, GlobalConst.GameResolution.X, GlobalConst.GameResolution.Y), new Color(255, 255, 255, 100));
        spriteBatch.Draw(MyGame.Instance.Gfx.Backlogo, new Vector2(215, 20), Color.White);
        spriteBatch.Draw(MyGame.Instance.Sprites.EyeBlink1, new Vector2(239, 58), new Rectangle(0, framblink1 * 12, MyGame.Instance.Sprites.EyeBlink1.Width, 12), Color.White,
            0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
        spriteBatch.Draw(MyGame.Instance.Sprites.EyeBlink2, new Vector2(463, 58), new Rectangle(0, framblink2 * 12, MyGame.Instance.Sprites.EyeBlink2.Width, 12), Color.White,
            0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
        spriteBatch.Draw(MyGame.Instance.Sprites.EyeBlink3, new Vector2(703, 50), new Rectangle(0, framblink3 * 12, MyGame.Instance.Sprites.EyeBlink3.Width, 12), Color.White,
            0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
        //water effect waves okokok mainMenu
        frameWater++;
        int width = 628; // ok for wave wrapping with flatness 50,100
        int[] terrainContour = new int[width];
        float offset = width / 3.0f;
        float flatness = 100; //wave length
        for (int xwe = 0; xwe < width; xwe++)
        {
            double height = peakheight * Math.Sin(xwe / flatness) + offset;
            terrainContour[xwe] = (int)height;
        }
        Color[] foregroundColors = new Color[width * 512];

        for (int xqw = 0; xqw < width; xqw++)
        {
            for (int y = 0; y < 512; y++)
            {
                if (y > terrainContour[xqw] && y < terrainContour[xqw] + 250) // this way width of waves=200
                    foregroundColors[xqw + y * width] = new Color(0, 0, 255 - y / 4, 255);
                else
                    foregroundColors[xqw + y * width] = Color.Transparent;
            }
        }
        if (frameWater % 3 == 0)
        {
            if (Updown)
            {
                peakheight++;
                if (peakheight > 40)
                    Updown = false;
            }
            else
            {
                peakheight--;
                if (peakheight < 25)
                    Updown = true;
            }
        }
        Texture2D foregroundTexture = new(MyGame.Instance.GraphicsDevice, width, 512, false, SurfaceFormat.Color);
        foregroundTexture.SetData(foregroundColors);
        Rectangle rectangleFill = new();
        Rectangle rectangleFill2 = new();
        rectangleFill.X = 0;
        rectangleFill.Y = 0;
        rectangleFill.Width = GlobalConst.GameResolution.X;
        rectangleFill.Height = GlobalConst.GameResolution.Y;
        rectangleFill2.X = 0 + (int)frameWater * 4;
        rectangleFill2.Y = 0;
        rectangleFill2.Width = GlobalConst.GameResolution.X;
        rectangleFill2.Height = GlobalConst.GameResolution.Y;
        Color colorFill = new()
        {
            R = 255,
            G = 255,
            B = 255,
            A = 100,
        };
        spriteBatch.Draw(foregroundTexture, rectangleFill, rectangleFill2, colorFill);
        rectangleFill2.X = 0 - (int)frameWater;
        rectangleFill2.Y = 100;
        rectangleFill2.Width = GlobalConst.GameResolution.X;
        rectangleFill2.Height = GlobalConst.GameResolution.Y;
        colorFill.A = 80;
        spriteBatch.Draw(foregroundTexture, rectangleFill, rectangleFill2, colorFill); // second wave position depth by order of draw
        if (MyGame.Instance.ParticleTab != null)
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = 10;
            rectangleFill.Height = 10;
            for (int varParticle = 0; varParticle < GlobalConst.NumParticles; varParticle++)
            {
                spriteBatch.Draw(MyGame.Instance.ParticleTab[varParticle].Sprite, MyGame.Instance.ParticleTab[varParticle].Pos, rectangleFill, Color.Magenta, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.90001f);
            }
        }
        if (_levelCategory == ELevelCategory.Fun)
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty), new Color(255, 255, 255, 255));
        }
        else
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty), new Color(80, 80, 80, 255));
        }
        if (_levelCategory == ELevelCategory.Tricky)
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 100), new Color(255, 255, 255, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign3, new Vector2(mmstartx + 34, mmstarty + 125), new Color(255, 255, 255, 255));
        }
        else
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 100), new Color(80, 80, 80, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign3, new Vector2(mmstartx + 34, mmstarty + 125), new Color(80, 80, 80, 255));
        }
        if (_levelCategory == ELevelCategory.Taxing)
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 200), new Color(255, 255, 255, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign2, new Vector2(mmstartx + 34, mmstarty + 225), new Color(255, 255, 255, 255));
        }
        else
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 200), new Color(80, 80, 80, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign2, new Vector2(mmstartx + 34, mmstarty + 225), new Color(80, 80, 80, 255));
        }
        if (_levelCategory == ELevelCategory.Mayhem)
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 300), new Color(255, 255, 255, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign1, new Vector2(mmstartx + 34, mmstarty + 325), new Color(255, 255, 255, 255));
        }
        else
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 300), new Color(80, 80, 80, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign1, new Vector2(mmstartx + 34, mmstarty + 325), new Color(80, 80, 80, 255));
        }
        if (_levelCategory == ELevelCategory.Bonus)
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 400), new Color(255, 255, 255, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign5, new Vector2(mmstartx + 34, mmstarty + 425), new Color(255, 255, 255, 255));
        }
        else
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 400), new Color(80, 80, 80, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign5, new Vector2(mmstartx + 34, mmstarty + 425), new Color(80, 80, 80, 255));
        }
        if (_levelCategory == ELevelCategory.User)
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 500), new Color(255, 255, 255, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign6, new Vector2(mmstartx + 34, mmstarty + 525), new Color(255, 255, 255, 255));
        }
        else
        {
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign, new Vector2(mmstartx, mmstarty + 500), new Color(80, 80, 80, 255));
            spriteBatch.Draw(_mainMenuGfx.ranksign6, new Vector2(mmstartx + 34, mmstarty + 525), new Color(80, 80, 80, 255));
        }
        if ((int)_levelCategory <= (int)ELevelCategory.Bonus && _levelCategory != ELevelCategory.None)
        {
            colorFill.R = 0;  // black with transparency at 170
            colorFill.G = 0;
            colorFill.B = 0;
            colorFill.A = 170;
            spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            int mmx = mmX;
            int mmy = 130;
            x = new Point(Input.CurrentMouseState.Position.X, Input.CurrentMouseState.Position.Y);
            MouseLevelChoose = 0;
            for (int s = 1; s < (_levelCategory == ELevelCategory.Bonus ? 37 : 31); s++)
            {
                Rectangle mmlev = new(mmx, mmy, 130, 55);
                if (mmlev.Contains(x))
                {
                    MouseLevelChoose = s + (30 * (((int)_levelCategory) - 1));
                    if (SaveGame.FinishedLevel[MouseLevelChoose])
                        colorFill = Color.ForestGreen;
                    else
                        colorFill = Color.Red;
                    spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    break;
                }
                mmx += 135;
                if (s % 7 == 0)
                {
                    mmx = mmX;
                    mmy += 70;
                }
            }
            if (MyGame.Instance.ScreenInGame.MyTexture == null || MyGame.Instance.ScreenInGame.MyTexture.Name != "levels/mini_levels" + ((int)_levelCategory).ToString())
            {
                MyGame.Instance.ScreenInGame.MyTexture = _mainMenuGfx.MiniLevels[_levelCategory];
            }
            spriteBatch.Draw(MyGame.Instance.ScreenInGame.MyTexture, new Vector2(mmX, 130), Color.White);
        }
        else if (_levelCategory == ELevelCategory.User)
        {
            colorFill.R = 0;  // black with transparency at 170
            colorFill.G = 0;
            colorFill.B = 0;
            colorFill.A = 170;
            spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(_mainMenuGfx.mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            int mmx = mmX;
            int mmy = 130;
            x = new Point(Input.CurrentMouseState.Position.X, Input.CurrentMouseState.Position.Y);
            MouseLevelChoose = 0;
            for (int s = 1; s < 26; s++) //number user levels to show okok be careful
            {
                Rectangle mmlev = new(mmx, mmy, 130, 55);
                if (mmlev.Contains(x))
                {
                    MouseLevelChoose = 156 + s;
                    if (SaveGame.FinishedLevel[MouseLevelChoose])
                        colorFill = Color.ForestGreen;
                    else
                        colorFill = Color.Red;
                    spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    break;
                }
                mmx += 135;
                if (s % 7 == 0)
                {
                    mmx = mmX;
                    mmy += 70;
                }
            }
            mmx = mmX;
            mmy = 130;
            for (int s = 1; s < 26; s++) //number user levels to show okok be careful
            {
                MyGame.Instance.ScreenInGame.MyTexture = MyGame.Instance.Content.Load<Texture2D>("levels/user/user" + string.Format("{0,3:D3}", s));
                spriteBatch.Draw(MyGame.Instance.ScreenInGame.MyTexture, new Rectangle(mmx, mmy, 130, 55), new Rectangle(0, 0, MyGame.Instance.ScreenInGame.MyTexture.Width, MyGame.Instance.ScreenInGame.MyTexture.Height), Color.White);
                mmx += 135;
                if (s % 7 == 0)
                {
                    mmx = mmX;
                    mmy += 70;
                }
            }
        }
        if (MouseLevelChoose != 0 && MouseLevelChoose <= GlobalConst.NumTotalLevels - 1) // MENU SHOW LEVELS DETAILS
        {
            int mmKX = 100;
            int mmKY = 555;
            int mmKplusY = 27;
            levelACT = MouseLevelChoose;
            if ((int)_levelCategory <= (int)ELevelCategory.Mayhem && _levelCategory != ELevelCategory.None)
                levelACT -= 30 * (((int)_levelCategory) - 1);
            else if (levelACT > 120 && levelACT <= 156)
                levelACT -= 120;
            else if (levelACT > 156)
                levelACT -= 156;
            MyGame.Instance.Fonts.TextLem("Level " + string.Format("{0}", levelACT), new Vector2(mmKX, mmKY), Color.Red, 1f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem(_currentMouseLevel.NameOfLevel, new Vector2(mmKX + 200, mmKY), Color.Red, 1f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("Number of Lemmings " + string.Format("{0}", _currentMouseLevel.TotalLemmings), new Vector2(mmKX, mmKY + mmKplusY), Color.Blue, 1f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem(string.Format("{0}", _currentMouseLevel.NbLemmingsToSave) + " to be saved", new Vector2(mmKX, mmKY + mmKplusY * 2), Color.Green, 1f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("Release Rate " + string.Format("{0}", _currentMouseLevel.MinFrequencyComming), new Vector2(mmKX, mmKY + mmKplusY * 3), Color.Yellow, 1f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("Time " + string.Format("{0}", _currentMouseLevel.TotalTime) + " Minutes", new Vector2(mmKX, mmKY + mmKplusY * 4), Color.Cyan, 1f, 0.1f, spriteBatch);
            if (_levelCategory == ELevelCategory.Fun)
            {
                MyGame.Instance.Fonts.TextLem("Rating FUN", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f, spriteBatch);
            }
            else if (_levelCategory == ELevelCategory.Tricky)
            {
                MyGame.Instance.Fonts.TextLem("Rating TRICKY", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f, spriteBatch);
            }
            else if (_levelCategory == ELevelCategory.Taxing)
            {
                MyGame.Instance.Fonts.TextLem("Rating TAXING", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f, spriteBatch);
            }
            else if (_levelCategory == ELevelCategory.Mayhem)
            {
                MyGame.Instance.Fonts.TextLem("Rating MAYHEM", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f, spriteBatch);
            }
            else if (MouseLevelChoose > 120 && MouseLevelChoose <= 156)
            {
                MyGame.Instance.Fonts.TextLem("Rating BONUS", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f, spriteBatch);
            }
            else if (MouseLevelChoose > 156)
            {
                MyGame.Instance.Fonts.TextLem("Rating USER", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f, spriteBatch);
            }
            int mmKindX = 960;
            int mmKindY = 580;
            int mmPlusy = 15;
            MyGame.Instance.Fonts.TextLem("Climbers: " + string.Format("{0}", _currentMouseLevel.NumberClimbers), new Vector2(mmKindX, mmKindY), Color.Linen, 0.5f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("Floaters: " + string.Format("{0}", _currentMouseLevel.NumberUmbrellas), new Vector2(mmKindX, mmKindY + mmPlusy), Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem(" Bombers: " + string.Format("{0}", _currentMouseLevel.NumberExploders), new Vector2(mmKindX, mmKindY + mmPlusy * 2), Color.SteelBlue, 0.5f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("Blockers: " + string.Format("{0}", _currentMouseLevel.NumberBlockers), new Vector2(mmKindX, mmKindY + mmPlusy * 3), Color.Red, 0.5f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("Builders: " + string.Format("{0}", _currentMouseLevel.NumberBuilders), new Vector2(mmKindX, mmKindY + mmPlusy * 4), Color.Orange, 0.5f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem(" Bashers: " + string.Format("{0}", _currentMouseLevel.NumberBashers), new Vector2(mmKindX, mmKindY + mmPlusy * 5), Color.Violet, 0.5f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("  Miners: " + string.Format("{0}", _currentMouseLevel.NumberMiners), new Vector2(mmKindX, mmKindY + mmPlusy * 6), Color.Turquoise, 0.5f, 0.1f, spriteBatch);
            MyGame.Instance.Fonts.TextLem(" Diggers: " + string.Format("{0}", _currentMouseLevel.NumberDiggers), new Vector2(mmKindX, mmKindY + mmPlusy * 7), Color.Tomato, 0.5f, 0.1f, spriteBatch);
        }

        spriteBatch.Draw(_mainMenuGfx.Colors88, new Vector2(560, 480), new Rectangle(0, 0, _mainMenuGfx.Colors88.Width, _mainMenuGfx.Colors88.Height), Color.White, 0f, Vector2.Zero, .8f,
            SpriteEffects.None, 0.0001f);
        spriteBatch.End();

        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

        MyGame.Instance.MouseManager.Draw(spriteBatch, withOffset: true);

        spriteBatch.End();
    }

    internal void Update()
    {
        dibujaloop++;
        if (Input.PreviousKeyState.IsKeyDown(Keys.Escape) && Input.CurrentKeyState.IsKeyUp(Keys.Escape))
        {
            MyGame.Instance.Exit();
        }
        MyGame.Instance.ScreenInGame.Frame2++;
        MyGame.Instance.ScreenInGame.Dibuja = false;
        if (MyGame.Instance.ScreenInGame.Frame2 > 6)
        {
            MyGame.Instance.ScreenInGame.Frame2 = 0;
            MyGame.Instance.ScreenInGame.Frame++;
            MyGame.Instance.ScreenInGame.Dibuja = true;
        }
        if (MyGame.Instance.ScreenInGame.R1 == 0)
        {
            MyGame.Instance.ScreenInGame.R1 = GlobalConst.Rnd.Next(1, 30);
        }
        if (MyGame.Instance.ScreenInGame.R2 == 0)
        {
            MyGame.Instance.ScreenInGame.R2 = GlobalConst.Rnd.Next(1, 45);
        }
        if (MyGame.Instance.ScreenInGame.R3 == 0)
        {
            MyGame.Instance.ScreenInGame.R3 = GlobalConst.Rnd.Next(1, 35);
        }
        if (MyGame.Instance.ScreenInGame.Frame % MyGame.Instance.ScreenInGame.R1 == 0 && !blink1on)
        {
            framblink1 = 0;
            blink1on = true;
        }  // bbbbbbbbbbbbbbllllllllllllllblinking eyes menu 1-2-3
        if (blink1on && MyGame.Instance.ScreenInGame.Dibuja)
        {
            framblink1++;
            if (framblink1 > 8)
            {
                blink1on = false;
                MyGame.Instance.ScreenInGame.R1 = 0;
            }
        }
        if (MyGame.Instance.ScreenInGame.Frame % MyGame.Instance.ScreenInGame.R2 == 0 && !blink2on)
        {
            framblink2 = 0;
            blink2on = true;
        }
        if (blink2on && MyGame.Instance.ScreenInGame.Dibuja)
        {
            framblink2++;
            if (framblink2 > 8)
            {
                blink2on = false;
                MyGame.Instance.ScreenInGame.R2 = 0;
            }
        }
        if (MyGame.Instance.ScreenInGame.Frame % MyGame.Instance.ScreenInGame.R3 == 0 && !blink3on)
        {
            framblink3 = 0;
            blink3on = true;
        }
        if (blink3on && MyGame.Instance.ScreenInGame.Dibuja)
        {
            framblink3++;
            if (framblink3 > 8)
            {
                blink3on = false;
                MyGame.Instance.ScreenInGame.R3 = 0;
            }
        }
        if (MouseLevelChoose != 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.CurrentScreen = ECurrentScreen.InGame;
            MyGame.Instance.CurrentLevelNumber = MouseLevelChoose;
            MyGame.Instance.ScreenInGame.Frame = 0;
            MyGame.Instance.ScreenInGame.Frame2 = 0;
            MyGame.Instance.ReloadContent();
        }
    }
}
