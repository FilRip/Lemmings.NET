using System;
using System.Collections.Generic;
using System.Linq;

using Lemmings.NET.Constants;
using Lemmings.NET.Datatables;
using Lemmings.NET.Helpers;
using Lemmings.NET.Models;
using Lemmings.NET.Models.Props;
using Lemmings.NET.Structs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens;

internal class InGame
{
    #region Properties

    internal int NumTOTsteel { get; set; }
    internal int ScrollX { get; set; }
    internal int NumACTdoor { get; set; }
    internal int NumTOTdoors { get; set; } = 1;
    internal int NumTOTplats { get; set; }
    internal int ScrollY { get; set; }
    internal int NumTOTexits { get; set; } = 1;
    internal float Countertime { get; set; }
    internal List<OneLemming> AllLemmings { get; set; }
    internal int Frame2 { get; set; }
    internal int NbClimberRemaining { get; set; }
    internal int NbFloaterRemaining { get; set; }
    internal int NbExploderRemaining { get; set; }
    internal int NbBlockerRemaining { get; set; }
    internal int NbBuilderRemaining { get; set; }
    internal int NbBasherRemaining { get; set; }
    internal int NbMinerRemaining { get; set; }
    internal int NbDiggerRemaining { get; set; }
    internal bool AllBlow { get; set; }
    internal double MillisecondsElapsed { get; set; }
    internal Texture2D Earth { get; set; }
    internal string LemSkill { get; set; } = "";
    internal Varplat[] Plats { get; set; }
    internal Varadds[] Adds { get; set; }
    internal bool SteelON { get; set; }
    internal bool PlatsON { get; set; }
    internal bool ArrowsON { get; set; }
    internal bool AddsON { get; set; }
    internal int NumTotTraps { get; set; }
    internal int NumTotArrow { get; set; }
    internal int R1 { get; set; }
    internal int R2 { get; set; }
    internal int R3 { get; set; }
    internal int ZvTime { get; set; }
    internal bool Fade { get; set; } = true;
    internal Vararrows[] Arrow { get; set; }
    internal Varsteel[] Steel { get; set; }
    internal Varmoredoors[] MoreDoors { get; set; }
    internal Varmoreexits[] Moreexits { get; set; }
    internal bool Drawing { get; set; } = true;
    internal bool Draw2 { get; set; } = true;
    internal double TotalTime { get; set; }
    internal int Frame { get; set; }
    internal SoundEffectInstance CurrentMusic { get; set; }
    internal Texture2D MyTexture { get; set; }
    internal InGameMenu InGameMenu
    {
        get { return _inGameMenu; }
    }
    internal int ActItem { get; set; }
    internal bool Exploding { get; set; }
    internal int Frente2 { get; set; }
    internal int Frente { get; set; }
    internal bool MouseOnLem { get; set; }
    internal bool Draw_walker { get; set; }
    internal bool Draw_builder { get; set; }
    internal Color[] Colormask33 { get; set; } = new Color[38 * 53]; // explode mask 38*53
    internal Color[] Colormask2 { get; set; } = new Color[20 * 20];  // miner mask 20*20 && basher too 20*20
    internal Color[] Colorsobre2 { get; set; } = new Color[20 * 20];
    internal Color[] C25 { get; set; } = new Color[4096 * 4096]; // Maximun size of a color array used for mask all the level
    internal Color[] Colormask22 { get; set; } = new Color[500 * 512];
    internal int NumSaved
    {
        get
        {
            return AllLemmings?.Count(l => l.Exit) ?? 0;
        }
    }
    internal Color[] Colorsobre33 { get; set; } = new Color[38 * 53];
    internal OneLevel CurrentLevel { get; set; }
    internal bool ExitBad { get; set; }
    internal int Numlemnow { get; set; }
    internal int Lemsneeded { get; set; } = 1;
    internal EndLevel EndLevelScreen { get; set; }
    #endregion

    #region Fields

    private float Countertime2;
    private double actWaves444, actWaves333, actWaves;
    private bool drawing3, LevelEnded, ExitLevel, BackToMainMenu;
    private int amount22;
    private int rest = 0, Contador2, Counter = 1;
    private bool doorOn = true;
    private double frameWaves;
    private int walker_frame;
    private int builder_frame;
    private readonly int builder_frame_second = 1;
    private int Frame3;
    private readonly int Framesecond = 6;
    private readonly int Framesecond2 = 2;
    private readonly int Framesecond3 = 1;  // frame speed less all go crazy 6->ok framesecond=6 default framesecond2=3 default
    private int door1X, door1Y;
    private int output1X, output1Y;
    private int frameDoor, frameExit; // 0--10   0--6
    private int exitFrame = 999, actualBlow; // frecuency lemmings go in
    private Rectangle exit_rect; // rectangle exit
    private Point x;
    private bool initON = false;
    private int framereal565;
    private float DoorExitDepth = 0.403f;  // default value--bigger than 0.5f is behind the terrain (0.6f level 58 for example)
    private Vector2 vectorFill;
    private Rectangle rectangleFill, rectangleFill2;
    private Color colorFill;
    private readonly double GRAVITY = 0.1; //0.1
    public int Z1 { get; set; }
    private int z2;
    private int z3;
    private bool luzmas = true, luzmas2 = true;
    private int alto;
    private int TotalNumLemmings = 1;
    private readonly Color[] Colorsobre22 = new Color[500 * 512];
    private readonly Color[] Colormasktotal = new Color[500 * 512];
    private bool doorWaveOn;
    private int frameact;
    private readonly bool LockMouse;
    private Texture2D salida_ani1, salida_ani1_1;
    private Texture2D puerta_ani;
    private readonly InGameMenu _inGameMenu;
    private List<OnePropSprite> _listSprites;
    private List<OneTrap> _listTraps;

    #endregion

    internal InGame()
    {
        _inGameMenu = new InGameMenu(this);
        EndLevelScreen = new EndLevel();
        LockMouse = false;
    }

    internal void LoadLevel(int newLevel, ContentManager content)
    {
        if (MyGame.Instance.Music.WinMusic.State == SoundState.Playing)
            MyGame.Instance.Music.WinMusic.Stop();
        if (MyGame.Instance.Music.MenuMusic.State == SoundState.Playing)
            MyGame.Instance.Music.MenuMusic.Stop();
        CurrentLevel = Levels.GetLevel(newLevel);
        Numlemnow = 0;
        frameDoor = 0;
        frameExit = 0;
        Frame3 = 0;
        Fade = true;
        doorOn = true;
        MillisecondsElapsed = 0;
        puerta_ani = content.Load<Texture2D>("puerta" + string.Format("{0}", CurrentLevel.TypeOfDoor)); // type of door puerta1-2-3-4 etc.
        string xx455 = string.Format("{0}", CurrentLevel.TypeOfExit);
        salida_ani1 = content.Load<Texture2D>("salida" + xx455);
        salida_ani1_1 = content.Load<Texture2D>("salida" + xx455 + "_1");
        MyGame.Instance.CurrentLevelNumber = newLevel;
        LemSkill = "";
        GlobalConst.Paused = false;
        ZvTime = 0;
        AllBlow = false;
        actualBlow = 0;
        exitFrame = 999;
        _inGameMenu.CurrentSelectedSkill = ECurrentSkill.NONE;
        Moreexits = null;
        MoreDoors = null;
        _listTraps = null;
        Arrow = null;
        _listSprites = null;
        NumTOTexits = 1;
        NumTOTdoors = 1;
        NumTotTraps = 0;
        NumTotArrow = 0;
        doorWaveOn = false;
        initON = false;
        PlatsON = false;
        AddsON = false;
        ArrowsON = false;
        SteelON = false;
        NumTOTsteel = 0;
        LevelEnded = false;
        EndLevelScreen.EndSongPlayed = false;
        ExitLevel = false;
        BackToMainMenu = false;
        ExitBad = false;

        Texture2D level = MyGame.Instance.Content.Load<Texture2D>(CurrentLevel.NameLev);
        Earth = new Texture2D(MyGame.Instance.GraphicsDevice, level.Width, level.Height);
        Color[] pixels = new Color[level.Width * level.Height];
        level.GetData(pixels);
        Earth.SetData(pixels);
        Earth.GetData(C25, 0, Earth.Height * Earth.Width); //better here than moverlemming() for performance see issues 
                                                           //see differences with old getdata, see size important (x * y)
        door1X = CurrentLevel.DoorX;
        door1Y = CurrentLevel.DoorY;
        output1X = CurrentLevel.ExitX;
        output1Y = CurrentLevel.ExitY;
        // this is the depth of the exit and doors animated sprites -- See level 58 the exit is behind the mountain (0.6f)
        if (CurrentLevel.DoorExitDepth != 0)
        {
            DoorExitDepth = CurrentLevel.DoorExitDepth;
        }
        else
        {
            DoorExitDepth = 0.403f;
        }
        NbClimberRemaining = CurrentLevel.NumberClimbers;
        NbFloaterRemaining = CurrentLevel.NumberUmbrellas;
        NbExploderRemaining = CurrentLevel.NumberExploders;
        NbBlockerRemaining = CurrentLevel.NumberBlockers;
        NbBuilderRemaining = CurrentLevel.NumberBuilders;
        NbBasherRemaining = CurrentLevel.NumberBashers;
        NbMinerRemaining = CurrentLevel.NumberMiners;
        NbDiggerRemaining = CurrentLevel.NumberDiggers;
        if (NbClimberRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.CLIMBER;
        }
        else if (NbFloaterRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.FLOATER;
        }
        else if (NbExploderRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.EXPLODER;
        }
        else if (NbBlockerRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BLOCKER;
        }
        else if (NbBuilderRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BUILDER;
        }
        else if (NbBasherRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BASHER;
        }
        else if (NbMinerRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.MINER;
        }
        else if (NbDiggerRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.DIGGER;
        }
        _inGameMenu.Init();
        TotalNumLemmings = CurrentLevel.TotalLemmings;
        Lemsneeded = CurrentLevel.NbLemmingsToSave;
        ScrollX = CurrentLevel.InitPosX;
        ScrollY = 0;
        AllLemmings = [];
        _listSprites = CurrentLevel.ListProps.OfType<OnePropSprite>().ToList();
        _listTraps = CurrentLevel.ListProps.OfType<OneTrap>().ToList();
    }

    private void Update_level()
    {
        builder_frame++;
        walker_frame++;
        frameWaves++;
        Frame2++;
        Frame3++;
        Drawing = false;
        Draw2 = false;
        drawing3 = false;
        Draw_walker = false;
        Draw_builder = false;
        if (walker_frame > SizeSprites.walker_framesecond)
        {
            walker_frame = 0;
            Draw_walker = true;
        }
        if (builder_frame > builder_frame_second)
        {
            builder_frame = 0;
            Draw_builder = true;
        }
        if (Frame2 > Framesecond)
        {
            Frame2 = 0;
            Drawing = true;
            if (!GlobalConst.Paused)
                Frame++;
        } //without this Frame affects door speed exit
        if (Frame3 > Framesecond2)
        {
            Frame3 = 0;
            Draw2 = true;
        }
        if (frameWaves > Framesecond3)
        {
            frameWaves = 0;
            drawing3 = true;
            actWaves++;
        } // change add of actwaves to see differences in speed  +=2,+=5

        // stop all things for exit prepare
        if (LevelEnded)
        {
            GlobalConst.Paused = true;
        }

        MoverLemming();

        if (_listSprites?.Count > 0)
        {
            foreach (OnePropSprite spr in _listSprites)
            {
                spr.Update();
            }
        }

        if (PlatsON &&
            !GlobalConst.Paused &&
            Plats != null)
        {
            foreach (Varplat plat in Plats)
            {
                if (plat.Frame > plat.Framesecond)
                {
                    bool goUP = plat.Up;
                    plat.SetFrame(0);
                    if (goUP)
                        plat.SetActStep(plat.ActStep + 1);
                    else
                        plat.SetActStep(plat.ActStep - 1);
                    if (goUP)
                        plat.SetAreaDrawX(plat.AreaDraw.Y - plat.Step);
                    else
                        plat.SetAreaDrawY(plat.AreaDraw.Y + plat.Step);
                    if (plat.ActStep >= plat.NumSteps - 1)
                        plat.SetUp(false);
                    if (plat.ActStep < 1)
                        plat.SetUp(true);
                    int px = plat.AreaDraw.X - (plat.AreaDraw.Width / 2);
                    alto = plat.Step * plat.NumSteps;
                    int positioYOrig = plat.AreaDraw.Y + (plat.ActStep * plat.Step);
                    bool realLine = false;
                    for (int y55 = 0; y55 < alto; y55++)
                    {
                        for (int x55 = 0; x55 < plat.AreaDraw.Width; x55++)
                        {
                            if (y55 == (alto - 1) - plat.ActStep * plat.Step)
                                realLine = true;
                            if (realLine)
                            {
                                C25[((positioYOrig - (alto - y55)) * Earth.Width) + x55 + px] = Color.White;
                            }
                            else
                            {
                                C25[((positioYOrig - (alto - y55)) * Earth.Width) + x55 + px] = Color.Transparent;

                            }
                        }
                    }
                    if (MyGame.Instance.DebugOsd.Debug)
                        Earth.SetData(C25, 0, Earth.Width * Earth.Height); //set this only for debugger and see the real c25 redraw
                }
                plat.SetFrame(plat.Frame + 1);
            }
        }

        if (AddsON && !GlobalConst.Paused)
        {
            int startposy = Adds[0].Sprite.Height / Adds[0].NumFrames; // height of each frame inside the whole sprite
            int framepos = startposy * Adds[0].ActFrame; // actual y position of the frame
            int ancho = Adds[0].Sprite.Width;
            int amount = ancho * startposy; // height frame
            rectangleFill.X = 0;
            rectangleFill.Y = framepos;
            rectangleFill.Width = ancho;
            rectangleFill.Height = startposy;
            Adds[0].Sprite.GetData(0, rectangleFill, Colormask22, 0, amount);
            rectangleFill.X = Adds[0].AreaDraw.X;
            rectangleFill.Y = Adds[0].AreaDraw.Y;
            rectangleFill.Width = ancho;
            rectangleFill.Height = startposy;
            Earth.SetData(0, rectangleFill, Colormask22, 0, amount);
            int py = Adds[0].AreaDraw.Y;
            int px = Adds[0].AreaDraw.X;
            int cantidad99 = 0;
            for (int yy99 = 0; yy99 < startposy; yy99++)
            {
                int yypos99 = (yy99 + py) * Earth.Width;
                for (int xx99 = 0; xx99 < ancho; xx99++)
                {
                    C25[yypos99 + px + xx99].PackedValue = Colormask22[cantidad99].PackedValue;
                    cantidad99++;
                }
            }
            if (Adds[0].Frame > Adds[0].Framesecond)
            {
                Adds[0].Frame = 0;
                Adds[0].ActFrame++;
                if (Adds[0].ActFrame >= Adds[0].NumFrames)
                    Adds[0].ActFrame = 0;
            }
            Adds[0].Frame++;
        }
        if (_listTraps?.Count > 0 && Drawing && !GlobalConst.Paused)
        {
            foreach (OneTrap trap in _listTraps)
            {
                trap.Update();
            }
        }
        if (!GlobalConst.Paused)
        {
            Countertime++;
        }
        Countertime2++;
        TotalTime = Countertime / 60; //real time of the level see to stop when finish or zvtime<0
        if (doorOn)
        {
            Countertime = 0;
            TotalTime = 0;
        }
        int maxluz = 14; // numero de ciclos de variar el rectangle del EFECTO DE LUCES 50 normalmente
        int maxluz2 = 200;
        if (luzmas2)
        {
            Contador2++;
            if (Contador2 >= maxluz2)
            {
                Contador2 = maxluz2 - 2;
                luzmas2 = false;
            }
        }
        else
        {
            Contador2--;
            if (Contador2 <= 0)
            {
                Contador2 = 2;
                luzmas2 = true;
            }
        }
        if ((Countertime2 / 4) % 2 == 0) //velocidad del refresco efecto de luces
        {
            if (luzmas)
            {
                Counter++;
                if (Counter >= maxluz)
                {
                    Counter = maxluz - 2;
                    luzmas = false;
                }
            }
            else
            {
                Counter--;
                if (Counter <= 0)
                {
                    Counter = 2;
                    luzmas = true;
                }
            }
        }// abajo calculos nubes nubes2 y waterfall
        Z1 = (int)Countertime2 / 3;
        z2 = (int)Countertime2 / 10;
        z3 = (int)Countertime2 / 9;
        z3 %= 4; // mumero de frames del agua a ver 4 de 5 que tiene la ultima esta vacia nose porque
        if (Drawing)
        {
            int xx66 = MyGame.Instance.Props.GetExit(CurrentLevel.TypeOfExit).NumFrame - 1;
            frameExit++;
            if (frameExit > xx66)
            {
                frameExit = 0;
            }
        }
        if (!GlobalConst.Paused)
            Door();
        _inGameMenu.Update();
        MyTexture = MyGame.Instance.Content.Load<Texture2D>("luces/" + Counter);// okokokokokokokok

        if (Drawing && NumTotArrow > 0) // dibuja or dibuja2 test performance-- this is the worst part of the code NEED OPTIMIZATION
        {
            for (int xz = 0; xz < NumTotArrow; xz++)
            {
                amount22 = Arrow[xz].Area.Width * Arrow[xz].Area.Height;
                Arrow[xz].Arrow.GetData(Colormask22, 0, Arrow[xz].Arrow.Height * Arrow[xz].Arrow.Width);
                //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                int py = Arrow[xz].Area.Y;
                int px = Arrow[xz].Area.X;
                int alto66 = Arrow[xz].Area.Height;
                int ancho66 = Arrow[xz].Area.Width;
                amount22 = 0;
                for (int yy88 = 0; yy88 < alto66; yy88++)
                {
                    int yypos888 = (yy88 + py) * Earth.Width;
                    for (int xx88 = 0; xx88 < ancho66; xx88++)
                    {
                        Colorsobre22[amount22].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                        amount22++;
                    }
                }
                if (!Arrow[xz].Right) //left arrows
                {
                    Arrow[xz].Moving++;
                    if (Arrow[xz].Moving < 0)
                    {
                        Arrow[xz].Moving = Arrow[xz].Arrow.Width - 1;
                    }
                    for (int y4 = 0; y4 < Arrow[xz].Area.Height; y4++)
                    {
                        for (int x4 = 0; x4 < Arrow[xz].Area.Width; x4++)
                        {
                            int posy456 = y4 % Arrow[xz].Arrow.Height;
                            int posx456 = x4 % Arrow[xz].Arrow.Width;
                            posx456 = (Arrow[xz].Arrow.Width - 1) - ((posx456 + Arrow[xz].Moving) % Arrow[xz].Arrow.Width); // left perfecto
                            Colormasktotal[(y4 * Arrow[xz].Area.Width) + x4].PackedValue = Colormask22[(posy456 * Arrow[xz].Arrow.Width) + posx456].PackedValue;
                        }
                    }
                    for (int r = 0; r < amount22; r++)
                    {
                        if (Colorsobre22[r].R > 0 || Colorsobre22[r].G > 0 || Colorsobre22[r].B > 0)
                        {
                            Colorsobre22[r].PackedValue = Colormasktotal[r].PackedValue;
                        }
                    }
                    Arrow[xz].EnvelopArrow.SetData(Colorsobre22, 0, Arrow[xz].EnvelopArrow.Height * Arrow[xz].EnvelopArrow.Width);
                }
                else //right arrows
                {
                    Arrow[xz].Moving--;
                    if (Arrow[xz].Moving < 0)
                    {
                        Arrow[xz].Moving = Arrow[xz].Arrow.Width - 1;
                    }
                    for (int y4 = 0; y4 < Arrow[xz].Area.Height; y4++)
                    {
                        for (int x4 = 0; x4 < Arrow[xz].Area.Width; x4++)
                        {
                            int posy456 = y4 % Arrow[xz].Arrow.Height;
                            int posx456 = x4 % Arrow[xz].Arrow.Width;
                            posx456 = ((posx456 + Arrow[xz].Moving) % Arrow[xz].Arrow.Width);  //Left okok
                            Colormasktotal[(y4 * Arrow[xz].Area.Width) + x4].PackedValue = Colormask22[(posy456 * Arrow[xz].Arrow.Width) + posx456].PackedValue;
                        }
                    }
                    for (int r = 0; r < amount22; r++)
                    {
                        if (Colorsobre22[r].R > 0 || Colorsobre22[r].G > 0 || Colorsobre22[r].B > 0)
                        {
                            Colorsobre22[r].PackedValue = Colormasktotal[r].PackedValue;
                        }
                    }
                    Arrow[xz].EnvelopArrow.SetData(Colorsobre22, 0, Arrow[xz].EnvelopArrow.Height * Arrow[xz].EnvelopArrow.Width);
                }
            }
        }
    }

    private void MoverLemming() //lemmings logic called every update
    {
        MouseOnLem = false;  // scroll mouse on level landscape
        Scrolling();
        if (doorOn)
            return; // start when door finish opening
        foreach (OneLemming lemming in AllLemmings) // NumLemmings
        {
            lemming.Moving();
        }
    }

    internal void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null, null);
        graphics.Clear(Color.Black);  //BACKGROUND COLOR darkslategray,cornblue,dimgray,black,gray,lighslategray

        bool rayLigths = true;
        // logic of background stars moving from -50 to 50
        actWaves333 = 50 * Math.Sin(actWaves / 60);  // 50 height of the wave  // 60 length of it
        actWaves444 = -70 * Math.Sin(actWaves / -80); // 10,100 -70,100
        if (MyGame.Instance.CurrentLevelNumber != 159)
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = GlobalConst.GameResolution.X;
            rectangleFill.Height = (int)(GlobalConst.GameResolution.Y * 0.732);
            colorFill.R = 150;
            colorFill.G = 150;
            colorFill.B = 150;
            colorFill.A = 160;
            spriteBatch.Draw(MyGame.Instance.Gfx.Logo_fondo, rectangleFill, rectangleFill, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
        }
        else
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = GlobalConst.GameResolution.X;
            rectangleFill.Height = (int)(GlobalConst.GameResolution.Y * 0.732);
            colorFill.R = 255;
            colorFill.G = 255;
            colorFill.B = 255;
            colorFill.A = 250;
            rectangleFill2.X = 0 + Z1;
            rectangleFill2.Y = 0 - (int)actWaves333;
            rectangleFill2.Width = GlobalConst.GameResolution.X;
            rectangleFill2.Height = GlobalConst.GameResolution.Y - 188;
            spriteBatch.Draw(MyGame.Instance.InGameMenuGfx.Logo666, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.8091f);
            Texture2D logo555 = MyGame.Instance.Content.Load<Texture2D>("fondos/ice outttt");
            rectangleFill2.X = 0 + (int)actWaves444;
            rectangleFill2.Y = 0 + (int)actWaves444;
            rectangleFill2.Width = GlobalConst.GameResolution.X;
            rectangleFill2.Height = GlobalConst.GameResolution.Y - 188;
            colorFill.R = 150;
            colorFill.G = 150;
            colorFill.B = 150;
            colorFill.A = 120;
            spriteBatch.Draw(logo555, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
        }
        if (_listTraps?.Count > 0) //draw traps
        {
            foreach (OneTrap trap in _listTraps)
            {
                trap.Draw(spriteBatch);
            }
        }
        switch (MyGame.Instance.CurrentLevelNumber)  // effect draws water cascade,stars,etc...
        {
            case 1:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                rayLigths = false;
                break;
            case 4:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(1530 - ScrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.803f); //0.802f  
                rayLigths = false;
                break;
            case 5:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(760 - ScrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                break;
            case 6:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(2000 - ScrollX, -80, 260, 680), new Rectangle(0 + z3 * 192, 0, 192, 192),
                    new Color(255, 50, 80, 170), 0f, Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f                            
                break;
            default:
                break;
        }

        if (MyGame.Instance.CurrentLevelNumber != 159) //nubes clouds moving in background
        {
            if (rayLigths)
            {
                spriteBatch.Draw(MyTexture, new Vector2(GlobalConst.GameResolution.X / 2, (GlobalConst.GameResolution.Y - 188) / 2), new Rectangle(0, 0, MyTexture.Width, MyTexture.Height), new Color(255, 255, 255, 10 + Counter * 2),
                    0.4f + Contador2 * 0.001f, new Vector2(MyTexture.Width / 2, MyTexture.Height / 2), 3f, SpriteEffects.FlipHorizontally, 0.805f); // okokok
            }
            // rayligts effect
            spriteBatch.Draw(MyGame.Instance.Sprites.Nubes_2, new Rectangle(0, 50 - (int)actWaves444, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes_2.Height), new Rectangle(Z1, 0, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes_2.Height),
                new Color(255, 255, 255, 110), 0f, Vector2.Zero, SpriteEffects.None, 0.804f);

            spriteBatch.Draw(MyGame.Instance.Sprites.Nubes, new Rectangle(0, 220, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes.Height), new Rectangle(z2, 0, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes.Height), new Color(255, 255, 255, 110), 0f,
                Vector2.Zero, SpriteEffects.None, 0.803f);
        }
        spriteBatch.Draw(Earth, new Vector2(0, 0), new Rectangle(ScrollX, ScrollY, GlobalConst.GameResolution.X, GlobalConst.GameResolution.Y - 188), //512 size of window draw
            Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.500f);
        if (NumTotArrow > 0)
        {
            for (int xz = 0; xz < NumTotArrow; xz++)
            {
                spriteBatch.Draw(Arrow[xz].EnvelopArrow, new Vector2(Arrow[xz].Area.X - ScrollX, Arrow[xz].Area.Y - ScrollY),
                    new Rectangle(0, 0, Arrow[xz].EnvelopArrow.Width, Arrow[xz].EnvelopArrow.Height),
                    new Color(255, 255, 255, Arrow[xz].Transparency), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.499f);
            }
        }

        //menu for ending level or not
        if (LevelEnded)
        {
            EndLevelScreen.Draw(spriteBatch);
        }

        OneEntry entry = MyGame.Instance.Props.GetEntry(CurrentLevel.TypeOfDoor);
        int xx55 = entry.Width;
        int yy55 = entry.Height;
        framereal565 = (frameDoor * yy55);

        if (_listSprites?.Count > 0)
        {
            foreach (OnePropSprite spr in _listSprites)
            {
                spr.Draw(spriteBatch);
            }
        }

        if (PlatsON)
        {
            foreach (Varplat plat in Plats)
            {
                int x2 = plat.AreaDraw.X - plat.AreaDraw.Width / 2;
                int y = plat.AreaDraw.Y;
                int w = plat.Sprite.Width;
                int h = plat.Sprite.Height;
                spriteBatch.Draw(plat.Sprite, new Rectangle(x2 - ScrollX, y - ScrollY - 5, plat.AreaDraw.Width, plat.AreaDraw.Height),
                    new Rectangle(0, 0, w, h), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.56f);
            }
        }
        if (MoreDoors == null)
        {
            spriteBatch.Draw(puerta_ani, new Vector2(door1X - ScrollX, door1Y - ScrollY), new Rectangle(0, framereal565, xx55, yy55),
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
        }
        else
        {
            foreach (Vector2 moreDoor in MoreDoors.Select(m => m.DoorMoreXY))
            {
                door1X = (int)moreDoor.X;
                door1Y = (int)moreDoor.Y;
                spriteBatch.Draw(puerta_ani, new Vector2(door1X - ScrollX, door1Y - ScrollY), new Rectangle(0, framereal565, xx55, yy55),
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            }
        }
        OneExit exit = MyGame.Instance.Props.GetExit(CurrentLevel.TypeOfExit);
        int xx66 = exit.Width;
        int yy66 = exit.Height;
        int xx88 = exit.MoreX;
        int xx99 = exit.MoreX2;
        int yy88 = exit.MoreY;
        int yy99 = exit.MoreY2;
        frameact = (frameExit * yy66);
        if (Moreexits == null)
        {
            spriteBatch.Draw(salida_ani1_1, new Vector2(output1X - ScrollX - xx88, output1Y - yy88 - ScrollY), new Rectangle(0, frameact, xx66, yy66), Color.White,
                0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            spriteBatch.Draw(salida_ani1, new Vector2(output1X - ScrollX - xx99, output1Y - yy99 - ScrollY), new Rectangle(0, 0, salida_ani1.Width, salida_ani1.Height),
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            if (MyGame.Instance.DebugOsd.Debug) //exits debug
            {
                exit_rect = new Rectangle(output1X - 5, output1Y - 5, 10, 10);
                spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(exit_rect.Left - ScrollX, exit_rect.Top - ScrollY, exit_rect.Width, exit_rect.Height), null,
                    Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            }
        }
        else
        {
            if (Moreexits != null)
            {
                foreach (Vector2 moreExits in Moreexits.Select(m => m.ExitMoreXY))
                {
                    output1X = (int)moreExits.X;
                    output1Y = (int)moreExits.Y;
                    spriteBatch.Draw(salida_ani1_1, new Vector2(output1X - ScrollX - xx88, output1Y - yy88 - ScrollY), new Rectangle(0, frameact, xx66, yy66), Color.White,
                        0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    spriteBatch.Draw(salida_ani1, new Vector2(output1X - ScrollX - xx99, output1Y - yy99 - ScrollY), new Rectangle(0, 0, salida_ani1.Width, salida_ani1.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    if (MyGame.Instance.DebugOsd.Debug) //exits debug
                    {
                        exit_rect = new Rectangle(output1X - 5, output1Y - 5, 10, 10);
                        spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(exit_rect.Left - ScrollX, exit_rect.Top - ScrollY, exit_rect.Width, exit_rect.Height), null,
                            Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    }
                }
            }
        }
        // infos various for test only

        if (!doorOn)
            foreach (OneLemming lemming in AllLemmings) //si lo hace de 100 a cero dibujara los primeros encima y mejorara el aspecto
                lemming.Draw(spriteBatch);

        if (Fade)
        {
            rest++;
            int rest2 = rest * 7;
            if (rest2 < 70)
                rest2 = 0;
            MyGame.Instance.Gfx.DrawLine(spriteBatch, new Vector2(0, 0), new Vector2(GlobalConst.GameResolution.X, 0), new Color(0, 0, 0, 255 - rest2), GlobalConst.GameResolution.Y, 0f);
            if (Frame > 19)
            {
                Fade = false;
                rest = 0;
                TotalTime = 0;
                if (MyGame.Instance.Sfx.Letsgo.State == SoundState.Stopped && !initON)
                {
                    MyGame.Instance.Sfx.Letsgo.Play();
                    initON = true;
                }
            }

        }
        if (Exploding) // draws explosions particles explosion_particle
        {
            for (int Qexplo = 0; Qexplo < ActItem; Qexplo++)
            {
                for (int Iexplo = 0; Iexplo < GlobalConst.PARTICLE_NUM; Iexplo++)
                {
                    if (MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr < 0)
                        continue;

                    vectorFill.X = (float)MyGame.Instance.Explosion[Qexplo, Iexplo].x - ScrollX;
                    vectorFill.Y = (float)MyGame.Instance.Explosion[Qexplo, Iexplo].y - ScrollY;
                    spriteBatch.Draw(MyGame.Instance.Gfx.Explosion_particle, vectorFill, new Rectangle(0, 0, MyGame.Instance.Gfx.Explosion_particle.Width, MyGame.Instance.Gfx.Explosion_particle.Height), MyGame.Instance.Explosion[Qexplo, Iexplo].Color,
                        MyGame.Instance.Explosion[Qexplo, Iexplo].Rotation, Vector2.Zero, MyGame.Instance.Explosion[Qexplo, Iexplo].Size, SpriteEffects.None, 0.300f);
                    MyGame.Instance.Explosion[Qexplo, Iexplo].Rotation += 0.03f;
                    MyGame.Instance.Explosion[Qexplo, Iexplo].Size += 0.01f;
                }
            }
        }
        if (!MouseOnLem)
        {
            LemSkill = "";
        }

        vectorFill.X = 650;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem("Home:" + string.Format("{0}", NumSaved) + "/" + string.Format("{0}", Lemsneeded), vectorFill, Color.Cyan, 1f, 0.1f, spriteBatch);
        vectorFill.X = 320;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem("Out:" + string.Format("{0}", AllLemmings.Count) + "/" + string.Format("{0}", TotalNumLemmings), vectorFill, Color.Magenta, 1f, 0.1f, spriteBatch);
        vectorFill.X = 530;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem("In:" + string.Format("{0}", Numlemnow), vectorFill, Color.AliceBlue, 1f, 0.1f, spriteBatch);

        _inGameMenu.Draw(spriteBatch);

        MyGame.Instance.MouseManager.Draw(spriteBatch, MouseOnLem);

        spriteBatch.End();
    }

    internal void Update(GameTime gameTime)
    {
        MillisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
        if (Exploding && drawing3 && !GlobalConst.Paused)  //logic explosions particles
        {
            int _totalExploding = ActItem;
            for (int Qexplo = 0; Qexplo < ActItem; Qexplo++)
            {
                int TopY = GlobalConst.GameResolution.Y;
                if (Earth != null)
                    TopY = Earth.Height - 2;
                int NumberAlive = 0;
                for (int Iexplo = 0; Iexplo < GlobalConst.PARTICLE_NUM; Iexplo++)
                {
                    if (MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr == -100)
                        NumberAlive++;
                    if (MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr > 0)
                    {
                        //this change alpha channel from half life and fade out every particle
                        int xx33 = MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr;
                        int yy33 = MyGame.Instance.Explosion[Qexplo, 0].Counter;
                        int xx55 = (xx33 + yy33) / 2;
                        if (yy33 > xx55)
                        {
                            yy33 -= xx55;
                            int yy55 = yy33 * 100 / xx55;
                            yy55 *= 2;
                            if (yy55 > 255)
                                yy55 = 255;
                            MyGame.Instance.Explosion[Qexplo, Iexplo].SetColorA(Convert.ToByte(255 - yy55)); //total alpha - % of death value
                        }
                        //calculate new position
                        MyGame.Instance.Explosion[Qexplo, Iexplo].x += MyGame.Instance.Explosion[Qexplo, Iexplo].dx;
                        MyGame.Instance.Explosion[Qexplo, Iexplo].y += MyGame.Instance.Explosion[Qexplo, Iexplo].dy + MyGame.Instance.Explosion[Qexplo, 0].Counter * GRAVITY;
                        if (MyGame.Instance.Explosion[Qexplo, Iexplo].y > TopY)
                        {
                            //explosion[qexplo, iexplo].y = topY;  //bottom of drawable sets y to max
                            MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr = -100;  //bottom of drawable area kills particle
                        }
                        // check life counter
                        if (MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr > 0)
                            MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr--;
                        if (MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr == 0)
                            MyGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr = -100;

                    }
                }
                MyGame.Instance.Explosion[Qexplo, 0].Counter++;
                if (NumberAlive >= GlobalConst.PARTICLE_NUM)
                {
                    _totalExploding--;
                }
            }
            if (_totalExploding == 0)  // no more particles[0....?][24] are ON
            {
                Exploding = false;
                ActItem = 0;
            }
        }
        if (!LevelEnded && ((AllBlow && Numlemnow == 0) || ZvTime < 0 || (AllLemmings.Count == TotalNumLemmings && Numlemnow == 0)))
        {
            if (!GlobalConst.Paused)
                rest++;  // var to wait until menu appears gives this way 4 seconds plus more
            if (rest > 180)
            {
                Exploding = false;
                ActItem = 0;  //see when finish time and are more particles ON
                LevelEnded = true;
                GlobalConst.Paused = true;
                if (NumSaved < Lemsneeded)
                    ExitBad = true;
            }
        }
        if (Input.PreviousKeyState.IsKeyDown(Keys.Left))
        {
            ScrollX -= 5;
            if (Input.ShiftPressed)
                ScrollX -= 10;
            Scrolling();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.Right))
        {
            ScrollX += 5;
            if (Input.ShiftPressed)
                ScrollX += 10;
            Scrolling();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.Up))
        {
            ScrollY -= 5;
            if (Input.ShiftPressed)
                ScrollY -= 10;
            Scrolling();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.Down))
        {
            ScrollY += 5;
            if (Input.ShiftPressed)
                ScrollY += 10;
            Scrolling();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.Escape) && Input.CurrentKeyState.IsKeyUp(Keys.Escape))
        {
            if (ExitBad && LevelEnded)
                ExitLevel = true;
            else if (NumSaved >= Lemsneeded && LevelEnded)
                ExitLevel = true;
            else
            {
                if (!LevelEnded)
                {
                    ExitBad = true;
                    LevelEnded = true;
                    GlobalConst.Paused = true;
                }
                else
                {
                    GlobalConst.Paused = false;
                    LevelEnded = false;
                }
            }
        }
        if (((Input.PreviousKeyState.IsKeyDown(Keys.Enter) && Input.CurrentKeyState.IsKeyUp(Keys.Enter)) ||
            (Input.PreviousMouseState.RightButton == ButtonState.Released && Input.CurrentMouseState.RightButton == ButtonState.Pressed))
            && LevelEnded)
        {
            ExitLevel = true;
            ExitBad = false;
            BackToMainMenu = true;
        }
        if ((Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed) && LevelEnded)
        {
            if (!ExitBad)
            {
                GlobalConst.Paused = false;
                LevelEnded = false;
            }
            else
                ExitLevel = true;
            if (NumSaved >= Lemsneeded)
                ExitLevel = true;
        }
        if (ExitLevel)
        {
            if (ExitBad) //repeat level
            {
                MyGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                Numlemnow = 0;
                Fade = true;
                MillisecondsElapsed = 0;
                doorOn = true;
                Frame = 0;
                Frame2 = 0;
                Frame3 = 0;
                frameDoor = 0;
                frameExit = 0;
                rest = 0;
                LevelEnded = false;
                ExitLevel = false;
                AllBlow = false;
                ZvTime = 0;
                ExitBad = false;
                MyGame.Instance.ReloadContent();
                return;
            }

            if (NumSaved >= Lemsneeded) //see here if level is finished or not
            {
                SaveGame.AddFinishedGame(MyGame.Instance.CurrentLevelNumber, 0, AllLemmings.Count(l => l.Exit));
                if (!BackToMainMenu)
                {
                    MyGame.Instance.CurrentLevelNumber++;
                    if (MyGame.Instance.CurrentLevelNumber >= GlobalConst.NumTotalLevels - 1)
                        MyGame.Instance.CurrentLevelNumber = GlobalConst.NumTotalLevels - 1;
                    MyGame.Instance.ScreenMainMenu.MouseLevelChoose = MyGame.Instance.CurrentLevelNumber;
                    MyGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                    Numlemnow = 0;
                    Fade = true;
                    MillisecondsElapsed = 0;
                    doorOn = true;
                    Frame = 0;
                    Frame2 = 0;
                    Frame3 = 0;
                    frameDoor = 0;
                    frameExit = 0;
                    rest = 0;
                    LevelEnded = false;
                    ExitLevel = false;
                    AllBlow = false;
                    ZvTime = 0;
                    ExitBad = false;
                    MyGame.Instance.ReloadContent();
                    return; //next level
                }
            }

            CurrentMusic.Stop();
            MyGame.Instance.ScreenMainMenu.MouseLevelChoose = 0;
            LevelEnded = false;
            ExitLevel = false;
            AllBlow = false;
            ZvTime = 0;
            ExitBad = false;
            BackToMainMenu = false;
            MyGame.Instance.ReloadContent();
            MyGame.Instance.BackToMenu();
            return;
        }

        if (AllBlow && actualBlow < AllLemmings.Count) // crash crash TEST TEST
        {
            if (!AllLemmings[actualBlow].Dead && !AllLemmings[actualBlow].Explode)
                AllLemmings[actualBlow].Exploser = true;
            actualBlow++;
        }
        if (Input.PreviousKeyState.IsKeyDown(Keys.P) && Input.CurrentKeyState.IsKeyUp(Keys.P))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            GlobalConst.Paused = !GlobalConst.Paused;
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.D1))
            _inGameMenu._decreaseOn = true;
        else if (Input.PreviousKeyState.IsKeyUp(Keys.D1) && _inGameMenu._decreaseOn)
            _inGameMenu._decreaseOn = false;
        else if (Input.PreviousKeyState.IsKeyDown(Keys.D2))
            _inGameMenu._increaseOn = true;
        else if (Input.PreviousKeyState.IsKeyUp(Keys.D2) && _inGameMenu._increaseOn)
            _inGameMenu._increaseOn = false;
        if (NbClimberRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D3) && Input.CurrentKeyState.IsKeyUp(Keys.D3))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.CLIMBER;
        }
        else if (NbFloaterRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D4) && Input.CurrentKeyState.IsKeyUp(Keys.D4))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.FLOATER;
        }
        else if (NbExploderRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D5) && Input.CurrentKeyState.IsKeyUp(Keys.D5))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.EXPLODER;
        }
        else if (NbBlockerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D6) && Input.CurrentKeyState.IsKeyUp(Keys.D6))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BLOCKER;
        }
        else if (NbBuilderRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D7) && Input.CurrentKeyState.IsKeyUp(Keys.D7))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BUILDER;
        }
        else if (NbBasherRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D8) && Input.CurrentKeyState.IsKeyUp(Keys.D8))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BASHER;
        }
        else if (NbMinerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D9) && Input.CurrentKeyState.IsKeyUp(Keys.D9))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.MINER;
        }
        else if (NbDiggerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D0) && Input.CurrentKeyState.IsKeyUp(Keys.D0))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.DIGGER;
        }
        Update_level();
    }

    private void Scrolling()
    {
        Point mousepos = Input.CurrentMouseState.Position;
        if (mousepos.X + 20 > GlobalConst.GameResolution.X &&
            ScrollX + GlobalConst.GameResolution.X < Earth.Width)
        {
            ScrollX += 5;
        }
        if (ScrollX + GlobalConst.GameResolution.X > Earth.Width)
        {
            ScrollX = Earth.Width - GlobalConst.GameResolution.X;
        }
        if (mousepos.X < -10 && ScrollX > 0)
        {
            ScrollX -= 5;
        }
        if (ScrollX < 0)
        {
            ScrollX = 0;
        }
        if (mousepos.Y + 20 > GlobalConst.GameResolution.Y && ScrollY + 512 < Earth.Height)
        {
            ScrollY += 5;
        }
        if (ScrollY + 512 > Earth.Height)
        {
            ScrollY = Earth.Height - 512;
        }
        if (mousepos.Y < -10 && ScrollY > 0)
        {
            ScrollY -= 5;
        }
        if (ScrollY < 0)
        {
            ScrollY = 0;
        }
        if (mousepos.Y < -14)
            mousepos.Y = -14;
        if (mousepos.Y > GlobalConst.GameResolution.Y * (MyGame.Instance.Scaled ? 2 : 1))
            mousepos.Y = GlobalConst.GameResolution.Y * (MyGame.Instance.Scaled ? 2 : 1);
        if (mousepos.X < -14)
            mousepos.X = -14;
        if (mousepos.X > GlobalConst.GameResolution.X * (MyGame.Instance.Scaled ? 2 : 1))
            mousepos.X = GlobalConst.GameResolution.X * (MyGame.Instance.Scaled ? 2 : 1);
        if (LockMouse)
            Mouse.SetPosition(mousepos.X, mousepos.Y); // setposition //this is for my son kids don't know move mouse so good  
    }

    private void Door()
    {
        exit_rect.X = output1X - 5;
        exit_rect.Y = output1Y - 5;
        exit_rect.Width = 10;
        exit_rect.Height = 10;
        if (Draw2 && doorOn && Frame > 30)
        {
            TotalTime = 0;
            int xx55 = MyGame.Instance.Props.GetEntry(CurrentLevel.TypeOfDoor).NumFrame - 1;
            frameDoor++;
            if (frameDoor == 1 && MyGame.Instance.Sfx.EntryLemmings.State == SoundState.Stopped && !doorWaveOn)
            {
                MyGame.Instance.Sfx.EntryLemmings.Play();
                doorWaveOn = true;
            }
            if (frameDoor > xx55)
            {
                CurrentMusic.IsLooped = true;
                if (!SaveGame.MuteMusic)
                    CurrentMusic.Play();
                doorOn = false;
                frameDoor = xx55;
            }
        }
        bool pullLemmings = false;
        float delayPercent = 27 - _inGameMenu.FrequencyNumber * 0.26f; // see to fix speed of lemmings release on door only when change frecuency (not so good)
        if (Drawing && !doorOn)
        {
            exitFrame++;
            if (exitFrame >= (int)delayPercent)
            {
                exitFrame = 0;
                pullLemmings = true;
            }
        }
        //test to see difference with anterior process
        if (pullLemmings && AllLemmings.Count != TotalNumLemmings && !AllBlow)
        {
            if (NumTOTdoors > 1 && MoreDoors != null) // more than 1 door is different calculation
            {
                door1Y = (int)MoreDoors[NumACTdoor].DoorMoreXY.Y;
                door1X = (int)MoreDoors[NumACTdoor].DoorMoreXY.X;
                NumACTdoor++;
                if (NumACTdoor >= NumTOTdoors)
                    NumACTdoor = 0;
            }
            AllLemmings.Add(new OneLemming()
            {
                NumLemming = AllLemmings.Count,
                PosY = door1Y,
                PosX = door1X + 35,
                Numframes = SizeSprites.faller_frames,
                Right = true,
                Fall = true,
                Walker = false,
                PixelsDrop = 0,
                Actualframe = 0,
                Onmouse = false,
                Active = true,
                Exit = false,
                Dead = false,
                Digger = false,
                Climber = false,
                Climbing = false,
                Umbrella = false,
                Falling = false,
                Framescut = false,
                Breakfloor = false,
                Exploser = false,
                Explode = false,
                Time = 0,
                Blocker = false,
                Builder = false,
                Basher = false,
                Miner = false,
                Bridge = false,
                Burned = false,
                Drown = false,
            });
            Numlemnow++;
        }

        foreach (OneLemming lemming in AllLemmings)
        {
            x.X = lemming.PosX + 14;
            x.Y = lemming.PosY + 25;
            if (lemming.Exit && lemming.Actualframe == 13) // change frame of yipee sound, old frame was init or 0 now different for frames
            {
                if (MyGame.Instance.Sfx.Yippe.State == SoundState.Playing && Draw2)
                    MyGame.Instance.Sfx.Yippe.Stop();
                if (MyGame.Instance.Sfx.Yippe.State == SoundState.Stopped)
                    MyGame.Instance.Sfx.Yippe.Play();
            }
            if (Moreexits == null)
            {
                if (exit_rect.Contains(x) && !lemming.Exit && !lemming.Explode)
                {
                    lemming.PosX = output1X - 19;
                    lemming.PosY = output1Y - 30;
                    lemming.Active = false;
                    lemming.Walker = false;
                    lemming.Fall = false;
                    lemming.Falling = false;
                    lemming.Exit = true;
                    lemming.Numframes = SizeSprites.sale_frames;
                    lemming.Actualframe = 0;
                }
            }
            else
            {
                if (Moreexits != null)
                {
                    foreach (Vector2 moreExitPos in Moreexits.Select(me => me.ExitMoreXY)) // more than one EXIT place
                    {
                        output1X = (int)moreExitPos.X;
                        output1Y = (int)moreExitPos.Y;
                        exit_rect.X = output1X - 5;
                        exit_rect.Y = output1Y - 5;
                        exit_rect.Width = 10;
                        exit_rect.Height = 10;
                        if (exit_rect.Contains(x) && !lemming.Exit && !lemming.Explode)
                        {
                            lemming.PosX = output1X - 19; //14+5 middle of the exit rect
                            lemming.PosY = output1Y - 30; //25+5
                            lemming.Active = false;
                            lemming.Walker = false;
                            lemming.Fall = false;
                            lemming.Falling = false;
                            lemming.Exit = true;
                            lemming.Numframes = SizeSprites.sale_frames;
                            lemming.Actualframe = 0; // break; //i'm not sure if it's necessary this break
                        }
                    }
                }
            }
        }
    }
}
