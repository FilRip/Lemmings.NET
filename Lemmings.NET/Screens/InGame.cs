using System;
using System.Linq;

using Lemmings.NET.Constants;
using Lemmings.NET.Helpers;
using Lemmings.NET.Models;
using Lemmings.NET.Structs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens;

internal class InGame
{
    internal int NumTOTsteel { get; set; }
    internal int NumLemmings { get; set; }
    internal int ScrollX { get; set; }
    internal int NumACTdoor { get; set; }
    internal int NumTOTdoors { get; set; } = 1;
    internal int NumTOTplats { get; set; }
    internal int ScrollY { get; set; }
    internal int NumTOTexits { get; set; } = 1;
    internal float Contadortime { get; set; }
    internal Lem[] Lemming { get; set; }
    internal int Frame2 { get; set; }
    internal Varsprites[] Sprite { get; set; }
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
    internal Vartraps[] Trap { get; set; }
    internal Varplat[] Plats { get; set; }
    internal Varadds[] Adds { get; set; }
    internal bool SteelON { get; set; }
    internal bool PlatsON { get; set; }
    internal bool ArrowsON { get; set; }
    internal bool AddsON { get; set; }
    internal bool TrapsON { get; set; }
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
    internal bool Dibuja { get; set; } = true;
    internal bool Draw2 { get; set; } = true;
    internal double TotalTime { get; set; }
    internal int Frame { get; set; }
    internal SoundEffectInstance CurrentMusic { get; set; }
    internal Texture2D MyTexture { get; set; }
    internal int ActLEM { get; set; }
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
            return Lemming.Count(l => l.Exit);
        }
    }
    internal Color[] Colorsobre33 { get; set; } = new Color[38 * 53];

    private float Contadortime2;
    private double actWaves444, actWaves333, actWaves;
    private bool dibuja3, LevelEnded, ExitBad, ExitLevel;
    private int cantidad22;
    private int rest = 0, framesale, Contador2, Contador = 1, actLEM2;
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
    private int output1X, output1Y, ex11;
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
    private bool _endSongPlayed;
    private readonly double GRAVITY = 0.1; //0.1
    private int Lemsneeded = 1;
    internal int Numlemnow { get; set; }
    private int z1;
    private int z2;
    private int z3;
    private bool luzmas = true, luzmas2 = true;
    private int alto;
    private int Numlems = 1;
    private readonly float Lem_depth = 0.300f;
    private readonly Color[] Colorsobre22 = new Color[500 * 512];
    private readonly Color[] Colormasktotal = new Color[500 * 512];
    private bool doorWaveOn;
    private int frameact;
    private readonly bool LockMouse;
    private readonly float SizeL = 1.35f; //1.2f was default in the beggining
    private Texture2D salida_ani1, salida_ani1_1, sale;
    private Texture2D puerta_ani;
    private readonly InGameMenu _inGameMenu;

    internal InGame()
    {
        _inGameMenu = new InGameMenu(this);
        LockMouse = false;
    }

    internal void LoadLevel(int newLevel, ContentManager content)
    {
        if (MyGame.Instance.Music.WinMusic.State == SoundState.Playing)
            MyGame.Instance.Music.WinMusic.Stop();
        if (MyGame.Instance.Music.MenuMusic.State == SoundState.Playing)
            MyGame.Instance.Music.MenuMusic.Stop();
        Numlemnow = 0;
        frameDoor = 0;
        frameExit = 0;
        Frame3 = 0;
        Fade = true;
        doorOn = true;
        MillisecondsElapsed = 0;
        NumLemmings = 0;
        puerta_ani = content.Load<Texture2D>("puerta" + string.Format("{0}", MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfDoor)); // type of door puerta1-2-3-4 etc.
        string xx455 = string.Format("{0}", MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit);
        salida_ani1 = content.Load<Texture2D>("salida" + xx455);
        salida_ani1_1 = content.Load<Texture2D>("salida" + xx455 + "_1");
        sale = content.Load<Texture2D>("sale");
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
        Trap = null;
        Arrow = null;
        Sprite = null;
        NumTOTexits = 1;
        NumTOTdoors = 1;
        NumTotTraps = 0;
        NumTotArrow = 0;
        doorWaveOn = false;
        initON = false;
        TrapsON = false;
        PlatsON = false;
        AddsON = false;
        ArrowsON = false;
        SteelON = false;
        NumTOTsteel = 0;
        LevelEnded = false;
        _endSongPlayed = false;
        ExitLevel = false;
        ExitBad = false;

        Texture2D level = MyGame.Instance.Content.Load<Texture2D>(MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].NameLev);
        Earth = new Texture2D(MyGame.Instance.GraphicsDevice, level.Width, level.Height);
        Color[] pixels = new Color[level.Width * level.Height];
        level.GetData(pixels);
        Earth.SetData(pixels);
        Earth.GetData(C25, 0, Earth.Height * Earth.Width); //better here than moverlemming() for performance see issues 
                                                           //see differences with old getdata, see size important (x * y)
        door1X = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].doorX;
        door1Y = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].doorY;
        output1X = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].exitX;
        output1Y = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].exitY;
        // this is the depth of the exit and doors animated sprites -- See level 58 the exit is behind the mountain (0.6f)
        if (MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].DoorExitDepth != 0)
        {
            DoorExitDepth = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].DoorExitDepth;
        }
        else
        {
            DoorExitDepth = 0.403f;
        }
        NbClimberRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberClimbers;
        NbFloaterRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberUmbrellas;
        NbExploderRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberExploders;
        NbBlockerRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberBlockers;
        NbBuilderRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberBuilders;
        NbBasherRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberBashers;
        NbMinerRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberMiners;
        NbDiggerRemaining = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].numberDiggers;
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
        Numlems = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TotalLemmings;
        Lemsneeded = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].NbLemmingsToSave;
        ScrollX = MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].InitPosX;
        ScrollY = 0;
        Lemming = new Lem[Numlems];
        MyGame.Instance.Levels.VariablesTraps();
    }

    private void Update_level()
    {
        builder_frame++;
        walker_frame++;
        frameWaves++;
        Frame2++;
        Frame3++;
        Dibuja = false;
        Draw2 = false;
        dibuja3 = false;
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
            Dibuja = true;
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
            dibuja3 = true;
            actWaves++;
        } // change add of actwaves to see differences in speed  +=2,+=5
        // stop all things for exit prepare
        if (LevelEnded)
        {
            GlobalConst.Paused = true;
        }
        MoverLemming();
        if (Sprite != null) //sprites logic if necessary puto77
        {
            for (int ssi = 0; ssi < Sprite.Length; ssi++)
            {
                Sprite[ssi].frame++;
                if (Sprite[ssi].sprite.Name == "touch/fire_sprites_other" && Sprite[ssi].frame > Sprite[ssi].framesecond)
                {
                    Sprite[ssi].frame = 0;
                    if (Sprite[ssi].minus)
                        Sprite[ssi].actFrame -= 2;
                    else
                        Sprite[ssi].actFrame++; // 2 frames less to return to zero better effect i think
                    if (Sprite[ssi].actFrame > 14 && !Sprite[ssi].minus)
                    {
                        Sprite[ssi].actFrame = 15;
                        Sprite[ssi].minus = true;
                    }
                    if (Sprite[ssi].actFrame < 0 && Sprite[ssi].minus)
                    {
                        Sprite[ssi].minus = false;
                        Sprite[ssi].actFrame = 1;
                    }
                    continue;
                }
                if (Sprite[ssi].frame > Sprite[ssi].framesecond)
                {
                    Sprite[ssi].frame = 0;
                    Sprite[ssi].actFrame++;
                    if (Sprite[ssi].actFrame > (Sprite[ssi].axisX * Sprite[ssi].axisY) - 1)
                        Sprite[ssi].actFrame = 0;
                }
                if (Sprite[ssi].speed != 0)  // spider destination puto puto puto
                {
                    if (Sprite[ssi].calc)
                    {
                        Sprite[ssi].calc = false;
                        if (!Sprite[ssi].minus)
                        {
                            Sprite[ssi].pos.X = Sprite[ssi].path[Sprite[ssi].actVect].X;
                            Sprite[ssi].pos.Y = Sprite[ssi].path[Sprite[ssi].actVect].Y;
                            Sprite[ssi].speed = Sprite[ssi].path[Sprite[ssi].actVect].Z;
                            Sprite[ssi].dest.X = Sprite[ssi].path[Sprite[ssi].actVect + 1].X;
                            Sprite[ssi].dest.Y = Sprite[ssi].path[Sprite[ssi].actVect + 1].Y;
                        }
                        else
                        {
                            Sprite[ssi].dest.X = Sprite[ssi].path[Sprite[ssi].actVect].X;
                            Sprite[ssi].dest.Y = Sprite[ssi].path[Sprite[ssi].actVect].Y;
                            Sprite[ssi].speed = Sprite[ssi].path[Sprite[ssi].actVect].Z;
                            Sprite[ssi].pos.X = Sprite[ssi].path[Sprite[ssi].actVect + 1].X;
                            Sprite[ssi].pos.Y = Sprite[ssi].path[Sprite[ssi].actVect + 1].Y;
                        }
                        if (!Sprite[ssi].minus)
                        {
                            Sprite[ssi].actVect++;
                        }
                        else
                        {
                            Sprite[ssi].actVect--;
                        }
                        if (Sprite[ssi].actVect > Sprite[ssi].path.Length - 2 && !Sprite[ssi].minus)
                        {
                            Sprite[ssi].actVect--; Sprite[ssi].minus = true;
                        }
                        if (Sprite[ssi].actVect < 0 && Sprite[ssi].minus)
                        {
                            Sprite[ssi].actVect++; Sprite[ssi].minus = false;
                        }

                        continue; // control when arrive to LAST destination point actvect
                    }
                    Vector2 direction_sprite = Vector2.Normalize(Sprite[ssi].dest - Sprite[ssi].pos);
                    Sprite[ssi].pos = Sprite[ssi].pos + direction_sprite * Sprite[ssi].speed;
                    float distance = Vector2.Distance(Sprite[ssi].pos, Sprite[ssi].dest);
                    if (distance < 1)
                    {
                        Sprite[ssi].calc = true;
                        continue; // control when arrive to destination point
                    }
                    Sprite[ssi].rotation = (float)Math.Atan2(direction_sprite.X, direction_sprite.Y) * -1;
                }
            }
        }
        if (PlatsON && !GlobalConst.Paused)
        {
            for (int i = 0; i < NumTOTplats; i++)
            {
                if (Plats[i].frame > Plats[i].framesecond)
                {
                    bool goUP = Plats[i].up;
                    Plats[i].frame = 0;
                    if (goUP)
                        Plats[i].actStep++;
                    else
                        Plats[i].actStep--;
                    if (goUP)
                        Plats[i].areaDraw.Y -= Plats[i].step;
                    else
                        Plats[i].areaDraw.Y += Plats[i].step;
                    if (Plats[i].actStep >= Plats[i].numSteps - 1)
                        Plats[i].up = false;
                    if (Plats[i].actStep < 1)
                        Plats[i].up = true;
                    int px = Plats[i].areaDraw.X - (Plats[i].areaDraw.Width / 2);
                    alto = Plats[i].step * Plats[i].numSteps;
                    int positioYOrig = Plats[i].areaDraw.Y + (Plats[i].actStep * Plats[i].step);
                    bool realLine = false;
                    for (int y55 = 0; y55 < alto; y55++)
                    {
                        for (int x55 = 0; x55 < Plats[i].areaDraw.Width; x55++)
                        {
                            if (y55 == (alto - 1) - Plats[i].actStep * Plats[i].step)
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
                Plats[i].frame++;
            }
        }

        if (AddsON && !GlobalConst.Paused)
        {
            int startposy = Adds[0].sprite.Height / Adds[0].numFrames; // height of each frame inside the whole sprite
            int framepos = startposy * Adds[0].actFrame; // actual y position of the frame
            int ancho = Adds[0].sprite.Width;
            int amount = ancho * startposy; // height frame
            rectangleFill.X = 0;
            rectangleFill.Y = framepos;
            rectangleFill.Width = ancho;
            rectangleFill.Height = startposy;
            Adds[0].sprite.GetData(0, rectangleFill, Colormask22, 0, amount);
            rectangleFill.X = Adds[0].areaDraw.X;
            rectangleFill.Y = Adds[0].areaDraw.Y;
            rectangleFill.Width = ancho;
            rectangleFill.Height = startposy;
            Earth.SetData(0, rectangleFill, Colormask22, 0, amount);
            int py = Adds[0].areaDraw.Y;
            int px = Adds[0].areaDraw.X;
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
            if (Adds[0].frame > Adds[0].framesecond)
            {
                Adds[0].frame = 0;
                Adds[0].actFrame++;
                if (Adds[0].actFrame >= Adds[0].numFrames)
                    Adds[0].actFrame = 0;
            }
            Adds[0].frame++;
        }
        if (TrapsON && Dibuja && !GlobalConst.Paused)
        {
            for (int s = 0; s < NumTotTraps; s++)
            {
                if (!Trap[s].isOn)
                {
                    Trap[s].actFrame++;
                    if (Trap[s].actFrame > Trap[s].numFrames - 1)
                        Trap[s].actFrame = 0;
                    if (Trap[s].type == 666)
                        Trap[s].actFrame = 0;
                }
                else
                {
                    Trap[s].actFrame++;
                    if (Trap[s].actFrame > Trap[s].numFrames - 1)
                    {
                        Trap[s].isOn = false;
                        Trap[s].actFrame = 0;
                    }
                }
            }
        }
        if (!GlobalConst.Paused)
        {
            Contadortime++;
        }
        Contadortime2++;
        TotalTime = Contadortime / 60; //real time of the level see to stop when finish or zvtime<0
        if (doorOn)
        {
            Contadortime = 0;
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
        if ((Contadortime2 / 4) % 2 == 0) //velocidad del refresco efecto de luces
        {
            if (luzmas)
            {
                Contador++;
                if (Contador >= maxluz)
                {
                    Contador = maxluz - 2;
                    luzmas = false;
                }
            }
            else
            {
                Contador--;
                if (Contador <= 0)
                {
                    Contador = 2;
                    luzmas = true;
                }
            }
        }// abajo calculos nubes nubes2 y waterfall
        z1 = (int)Contadortime2 / 3;
        z2 = (int)Contadortime2 / 10;
        z3 = (int)Contadortime2 / 9;
        z3 %= 4; // mumero de frames del agua a ver 4 de 5 que tiene la ultima esta vacia nose porque
        if (Dibuja)
        {
            int xx66 = MyGame.Instance.Levels.VarExit[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit].numFram - 1;
            frameExit++;
            if (frameExit > xx66)
            {
                frameExit = 0;
            }
        }
        if (!GlobalConst.Paused)
            Door();
        _inGameMenu.Update();
        MyTexture = MyGame.Instance.Content.Load<Texture2D>("luces/" + Contador);// okokokokokokokok

        if (Dibuja && NumTotArrow > 0) // dibuja or dibuja2 test performance-- this is the worst part of the code NEED OPTIMIZATION
        {
            for (int xz = 0; xz < NumTotArrow; xz++)
            {
                cantidad22 = Arrow[xz].area.Width * Arrow[xz].area.Height;
                Arrow[xz].flechas.GetData(Colormask22, 0, Arrow[xz].flechas.Height * Arrow[xz].flechas.Width);
                //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                int py = Arrow[xz].area.Y;
                int px = Arrow[xz].area.X;
                int alto66 = Arrow[xz].area.Height;
                int ancho66 = Arrow[xz].area.Width;
                cantidad22 = 0;
                for (int yy88 = 0; yy88 < alto66; yy88++)
                {
                    int yypos888 = (yy88 + py) * Earth.Width;
                    for (int xx88 = 0; xx88 < ancho66; xx88++)
                    {
                        Colorsobre22[cantidad22].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                        cantidad22++;
                    }
                }
                if (!Arrow[xz].right) //left arrows
                {
                    Arrow[xz].desplaza++;
                    if (Arrow[xz].desplaza < 0)
                    {
                        Arrow[xz].desplaza = Arrow[xz].flechas.Width - 1;
                    }
                    for (int y4 = 0; y4 < Arrow[xz].area.Height; y4++)
                    {
                        for (int x4 = 0; x4 < Arrow[xz].area.Width; x4++)
                        {
                            int posy456 = y4 % Arrow[xz].flechas.Height;
                            int posx456 = x4 % Arrow[xz].flechas.Width;
                            posx456 = (Arrow[xz].flechas.Width - 1) - ((posx456 + Arrow[xz].desplaza) % Arrow[xz].flechas.Width); // left perfecto
                            Colormasktotal[(y4 * Arrow[xz].area.Width) + x4].PackedValue = Colormask22[(posy456 * Arrow[xz].flechas.Width) + posx456].PackedValue;
                        }
                    }
                    for (int r = 0; r < cantidad22; r++)
                    {
                        if (Colorsobre22[r].R > 0 || Colorsobre22[r].G > 0 || Colorsobre22[r].B > 0)
                        {
                            Colorsobre22[r].PackedValue = Colormasktotal[r].PackedValue;
                        }
                    }
                    Arrow[xz].flechassobre.SetData(Colorsobre22, 0, Arrow[xz].flechassobre.Height * Arrow[xz].flechassobre.Width);
                }
                else //right arrows
                {
                    Arrow[xz].desplaza--;
                    if (Arrow[xz].desplaza < 0)
                    {
                        Arrow[xz].desplaza = Arrow[xz].flechas.Width - 1;
                    }
                    for (int y4 = 0; y4 < Arrow[xz].area.Height; y4++)
                    {
                        for (int x4 = 0; x4 < Arrow[xz].area.Width; x4++)
                        {
                            int posy456 = y4 % Arrow[xz].flechas.Height;
                            int posx456 = x4 % Arrow[xz].flechas.Width;
                            posx456 = ((posx456 + Arrow[xz].desplaza) % Arrow[xz].flechas.Width);  //Left okok
                            Colormasktotal[(y4 * Arrow[xz].area.Width) + x4].PackedValue = Colormask22[(posy456 * Arrow[xz].flechas.Width) + posx456].PackedValue;
                        }
                    }
                    for (int r = 0; r < cantidad22; r++)
                    {
                        if (Colorsobre22[r].R > 0 || Colorsobre22[r].G > 0 || Colorsobre22[r].B > 0)
                        {
                            Colorsobre22[r].PackedValue = Colormasktotal[r].PackedValue;
                        }
                    }
                    Arrow[xz].flechassobre.SetData(Colorsobre22, 0, Arrow[xz].flechassobre.Height * Arrow[xz].flechassobre.Width);
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
        for (ActLEM = 0; ActLEM < NumLemmings; ActLEM++)
        {
            Lemming[ActLEM].Moving();
        }
    }

    internal void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null, null);
        graphics.Clear(Color.Black);  //BACKGROUND COLOR darkslategray,cornblue,dimgray,black,gray,lighslategray
                                      //draws back image for all the level
        if (MyGame.Instance.ParticleTab != null)
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = 10;
            rectangleFill.Height = 10;
            colorFill.R = 255;
            colorFill.G = 255;
            colorFill.B = 255;
            colorFill.A = 150;
            for (int varParticle = 0; varParticle < GlobalConst.NumParticles; varParticle++)
            {
                spriteBatch.Draw(MyGame.Instance.ParticleTab[varParticle].Sprite, MyGame.Instance.ParticleTab[varParticle].Pos, rectangleFill, colorFill, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.50001f);
            }
        }
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
            rectangleFill2.X = 0 + z1;
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
        if (TrapsON) //draw traps
        {
            for (int r = 0; r < NumTotTraps; r++)
            {
                int tYheight = Trap[r].sprite.Height / Trap[r].numFrames;
                if (Trap[r].type != 555 && Trap[r].type != 666)
                {
                    int vv444 = 0;
                    switch (Trap[r].vvscroll)
                    {
                        case 1:
                            vv444 = z1;
                            break;
                        case 2:
                            vv444 = -z1;
                            break;
                        default:
                            break;
                    }
                    colorFill.R = 255;
                    colorFill.G = 255;
                    colorFill.B = 255;
                    colorFill.A = Trap[r].transparency;
                    if (Trap[r].R != 255 && Trap[r].R > 0)
                        colorFill.R = Trap[r].R;
                    if (Trap[r].G != 255 && Trap[r].G > 0)
                        colorFill.G = Trap[r].G;
                    if (Trap[r].B != 255 && Trap[r].B > 0)
                        colorFill.B = Trap[r].B;
                    rectangleFill.X = Trap[r].areaDraw.X - ScrollX;
                    rectangleFill.Y = Trap[r].areaDraw.Y - ScrollY;
                    rectangleFill.Width = Trap[r].areaDraw.Width;
                    rectangleFill.Height = tYheight;
                    rectangleFill2.X = 0 + vv444;
                    rectangleFill2.Y = tYheight * Trap[r].actFrame;
                    rectangleFill2.Width = Trap[r].areaDraw.Width;
                    rectangleFill2.Height = tYheight;
                    spriteBatch.Draw(Trap[r].sprite, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, Trap[r].depth);
                }
                else
                {
                    colorFill.R = 255;
                    colorFill.G = 255;
                    colorFill.B = 255;
                    colorFill.A = Trap[r].transparency;
                    if (Trap[r].R != 255 && Trap[r].R > 0)
                        colorFill.R = Trap[r].R;
                    if (Trap[r].G != 255 && Trap[r].G > 0)
                        colorFill.G = Trap[r].G;
                    if (Trap[r].B != 255 && Trap[r].B > 0)
                        colorFill.B = Trap[r].B;
                    int spY = Trap[r].sprite.Height / Trap[r].numFrames;
                    rectangleFill.X = (int)Trap[r].pos.X - ScrollX - Trap[r].vvX;
                    rectangleFill.Y = (int)Trap[r].pos.Y - Trap[r].vvY - ScrollY;
                    rectangleFill.Width = Trap[r].sprite.Width;
                    rectangleFill.Height = spY;
                    rectangleFill2.X = 0;
                    rectangleFill2.Y = spY * Trap[r].actFrame;
                    rectangleFill2.Width = Trap[r].sprite.Width;
                    rectangleFill2.Height = spY;
                    spriteBatch.Draw(Trap[r].sprite, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, Trap[r].depth);
                }
                if (MyGame.Instance.DebugOsd.Debug)
                {
                    spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(Trap[r].areaTrap.Left - ScrollX, Trap[r].areaTrap.Top - ScrollY, Trap[r].areaTrap.Width, Trap[r].areaTrap.Height),
                        null, new Color(255, 255, 255, 140), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
            }
        }
        switch (MyGame.Instance.CurrentLevelNumber)  // effect draws water cascade,stars,etc...
        {
            case 1:
                spriteBatch.Draw(MyGame.Instance.Sprites.Agua2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                rayLigths = false;
                break;
            case 4:
                spriteBatch.Draw(MyGame.Instance.Sprites.Agua2, new Rectangle(1530 - ScrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f
                spriteBatch.Draw(MyGame.Instance.Sprites.Agua2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.803f); //0.802f  
                rayLigths = false;
                break;
            case 5:
                spriteBatch.Draw(MyGame.Instance.Sprites.Agua2, new Rectangle(760 - ScrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                break;
            case 6:
                spriteBatch.Draw(MyGame.Instance.Sprites.Agua2, new Rectangle(2000 - ScrollX, -80, 260, 680), new Rectangle(0 + z3 * 192, 0, 192, 192),
                    new Color(255, 50, 80, 170), 0f, Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f                            
                break;
            default:
                break;
        }

        if (MyGame.Instance.CurrentLevelNumber != 159) //nubes clouds moving in background
        {
            if (rayLigths)
            {
                spriteBatch.Draw(MyTexture, new Vector2(GlobalConst.GameResolution.X / 2, (GlobalConst.GameResolution.Y - 188) / 2), new Rectangle(0, 0, MyTexture.Width, MyTexture.Height), new Color(255, 255, 255, 10 + Contador * 2),
                    0.4f + Contador2 * 0.001f, new Vector2(MyTexture.Width / 2, MyTexture.Height / 2), 3f, SpriteEffects.FlipHorizontally, 0.805f); // okokok
            }
            // rayligts effect
            spriteBatch.Draw(MyGame.Instance.Sprites.Nubes_2, new Rectangle(0, 50 - (int)actWaves444, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes_2.Height), new Rectangle(z1, 0, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes_2.Height),
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
                spriteBatch.Draw(Arrow[xz].flechassobre, new Vector2(Arrow[xz].area.X - ScrollX, Arrow[xz].area.Y - ScrollY),
                    new Rectangle(0, 0, Arrow[xz].flechassobre.Width, Arrow[xz].flechassobre.Height),
                    new Color(255, 255, 255, Arrow[xz].transparency), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.499f);
            }
        }
        //menu for ending level or not
        if (LevelEnded)
        {
            if (!_endSongPlayed)
            {
                if (CurrentMusic.State == SoundState.Playing)
                    CurrentMusic.Stop();
                if (ExitBad && MyGame.Instance.Sfx.OhNo.State != SoundState.Playing)
                    MyGame.Instance.Sfx.OhNo.Play();
                else if (!ExitBad && MyGame.Instance.Music.WinMusic.State != SoundState.Playing)
                    MyGame.Instance.Music.WinMusic.Play();
            }
            _endSongPlayed = true;
            colorFill.R = 0; //color.black for this change to see differents options
            colorFill.G = 0;
            colorFill.B = 0;
            colorFill.A = 150;
            spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(45, 32, 1005, 600), null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.001f);
            spriteBatch.Draw(MyGame.Instance.ScreenMainMenu.MainMenuGfx.mainMenuSign2, new Rectangle(-200, -120, 1500, 900), null,
               Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.00005f);
            int percent = (100 * NumSaved) / MyGame.Instance.Levels.AllLevel[MyGame.Instance.ScreenMainMenu.MouseLevelChoose].TotalLemmings;
            MyGame.Instance.Fonts.TextLem("All lemmings accounted for:", new Vector2(150, 100), Color.Cyan, 1.5f, 0.0000000001f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("You rescued " + string.Format("{0}", percent) + "%",
                 new Vector2(270, 160), Color.Violet, 1.5f, 0.0000000001f, spriteBatch);
            percent = (100 * Lemsneeded) / MyGame.Instance.Levels.AllLevel[MyGame.Instance.ScreenMainMenu.MouseLevelChoose].TotalLemmings;
            MyGame.Instance.Fonts.TextLem("You needed " + string.Format("{0}", percent) + "%",
                 new Vector2(300, 220), Color.DodgerBlue, 1.5f, 0.0000000001f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("Press <ESC> or <Left Mouse Button>", new Vector2(70, 400), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            if (ExitBad)
                MyGame.Instance.Fonts.TextLem("to retry level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            else if (NumSaved >= Lemsneeded)
            {
                MyGame.Instance.Fonts.TextLem("to next level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            }
            else
            {
                MyGame.Instance.Fonts.TextLem("to continue...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            }
            MyGame.Instance.Fonts.TextLem("Press <Enter> or <Right Mouse Button>", new Vector2(70, 520), Color.Yellow, 1.3f, 0.0000000001f, spriteBatch);
            MyGame.Instance.Fonts.TextLem("to Main Menu...", new Vector2(100, 560), Color.Yellow, 1.3f, 0.0000000001f, spriteBatch);
        }
        int xx55 = MyGame.Instance.Levels.VarDoor[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfDoor].xWidth;
        int yy55 = MyGame.Instance.Levels.VarDoor[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfDoor].yWidth;
        framereal565 = (frameDoor * yy55);
        if (Sprite != null) //draw sprites
        {
            for (int ssi = 0; ssi < Sprite.Length; ssi++)
            {
                int swidth = Sprite[ssi].sprite.Width / Sprite[ssi].axisX;
                int sheight = Sprite[ssi].sprite.Height / Sprite[ssi].axisY;
                int sx1 = 0;
                int sy1 = 0;
                if (Sprite[ssi].actFrame != 0)
                {
                    sx1 = swidth * (Sprite[ssi].actFrame % Sprite[ssi].axisX);
                    sy1 = sheight * (Sprite[ssi].actFrame / Sprite[ssi].axisX);
                }
                if (Sprite[ssi].typescroll > 0)
                {
                    Sprite[ssi].pos.X -= Sprite[ssi].typescroll;
                    if (Sprite[ssi].pos.X < 0 - (Sprite[ssi].sprite.Width * Sprite[ssi].scale))
                        Sprite[ssi].pos.X = GlobalConst.GameResolution.X;
                    if (Sprite[ssi].pos.X > GlobalConst.GameResolution.X)
                        Sprite[ssi].pos.X = -100;
                    spriteBatch.Draw(Sprite[ssi].sprite, new Vector2(Sprite[ssi].pos.X, Sprite[ssi].pos.Y - ScrollY),
                        new Rectangle(sx1, sy1, swidth, sheight), new Color(Sprite[ssi].R, Sprite[ssi].G, Sprite[ssi].B, Sprite[ssi].transparency),
                        Sprite[ssi].rotation, Vector2.Zero, Sprite[ssi].scale, SpriteEffects.None, Sprite[ssi].depth);
                }
                else
                {
                    if (Sprite[ssi].sprite.Name == "touch/arana") // 64x64 sprite frame size
                    {
                        int xxAnim;
                        if (Sprite[ssi].minusScrollx)
                        {
                            xxAnim = (int)Sprite[ssi].pos.X - ScrollX + 32;
                        }
                        else
                        {
                            xxAnim = (int)Sprite[ssi].pos.X + 32;
                        }
                        spriteBatch.Draw(Sprite[ssi].sprite, new Vector2(xxAnim, Sprite[ssi].pos.Y - ScrollY - 32),
                            new Rectangle(sx1, sy1, swidth, sheight), new Color(Sprite[ssi].R, Sprite[ssi].G, Sprite[ssi].B, Sprite[ssi].transparency),
                            Sprite[ssi].rotation, Sprite[ssi].center, Sprite[ssi].scale, SpriteEffects.None, Sprite[ssi].depth);
                    }
                    else
                    {
                        int xxAnim;
                        if (Sprite[ssi].minusScrollx)
                        {
                            xxAnim = (int)Sprite[ssi].pos.X - ScrollX;
                        }
                        else
                        {
                            xxAnim = (int)Sprite[ssi].pos.X;
                        }
                        spriteBatch.Draw(Sprite[ssi].sprite, new Vector2(xxAnim, Sprite[ssi].pos.Y - ScrollY),
                            new Rectangle(sx1, sy1, swidth, sheight), new Color(Sprite[ssi].R, Sprite[ssi].G, Sprite[ssi].B, Sprite[ssi].transparency),
                            Sprite[ssi].rotation, Vector2.Zero, Sprite[ssi].scale, SpriteEffects.None, Sprite[ssi].depth);
                    }
                }
            }
        }
        if (PlatsON)
        {
            for (int i = 0; i < NumTOTplats; i++)
            {
                int x2 = Plats[i].areaDraw.X - Plats[i].areaDraw.Width / 2;
                int y = Plats[i].areaDraw.Y;
                int w = Plats[i].sprite.Width;
                int h = Plats[i].sprite.Height;
                spriteBatch.Draw(Plats[i].sprite, new Rectangle(x2 - ScrollX, y - ScrollY - 5, Plats[i].areaDraw.Width, Plats[i].areaDraw.Height),
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
            for (int i = 0; i < NumTOTdoors; i++)
            {
                door1X = (int)MoreDoors[i].doorMoreXY.X;
                door1Y = (int)MoreDoors[i].doorMoreXY.Y;
                spriteBatch.Draw(puerta_ani, new Vector2(door1X - ScrollX, door1Y - ScrollY), new Rectangle(0, framereal565, xx55, yy55),
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            }
        }
        int xx66 = MyGame.Instance.Levels.VarExit[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit].xWidth;
        int yy66 = MyGame.Instance.Levels.VarExit[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit].yWidth;
        int xx88 = MyGame.Instance.Levels.VarExit[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit].moreX;
        int xx99 = MyGame.Instance.Levels.VarExit[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit].moreX2;
        int yy88 = MyGame.Instance.Levels.VarExit[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit].moreY;
        int yy99 = MyGame.Instance.Levels.VarExit[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfExit].moreY2;
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
            for (int ex22 = 0; ex22 < NumTOTexits; ex22++)
            {
                output1X = (int)Moreexits[ex22].exitMoreXY.X;
                output1Y = (int)Moreexits[ex22].exitMoreXY.Y;
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
        // infos various for test only

        for (ActLEM = 0; ActLEM < NumLemmings; ActLEM++) //si lo hace de 100 a cero dibujara los primeros encima y mejorara el aspecto
        {
            if (doorOn)
                break;
            if (Lemming[ActLEM].Dead || Lemming[ActLEM].Exit)
                continue;
            if (Lemming[ActLEM].Exploser && !Lemming[ActLEM].Explode)
            {
                if (Lemming[ActLEM].Time == 0)
                    Lemming[ActLEM].Time = TotalTime;
                double timez = TotalTime - Lemming[ActLEM].Time;
                int crono = (int)(6f - (float)timez);
                MyGame.Instance.Fonts.TextLem(string.Format("{0}", crono), new Vector2(Lemming[ActLEM].PosX + 3 - ScrollX, Lemming[ActLEM].PosY - 10 - ScrollY), Color.White, 0.4f, 0.000000000004f, spriteBatch);
                if (crono <= 0)
                {
                    // luto luto sound monogame 3.2 works ok without catch exception
                    MyGame.Instance.Sfx.OhNo.Replay();
                    Lemming[ActLEM].Explode = true;
                    Lemming[ActLEM].Active = false;
                    Lemming[ActLEM].Umbrella = false;
                    Lemming[ActLEM].Walker = false;
                    Lemming[ActLEM].Digger = false;
                    Lemming[ActLEM].Climber = false;
                    Lemming[ActLEM].Fall = false;
                    Lemming[ActLEM].Falling = false;
                    Lemming[ActLEM].Climbing = false;
                    Lemming[ActLEM].Exit = false;
                    Lemming[ActLEM].Blocker = false;
                    Lemming[ActLEM].Builder = false;
                    Lemming[ActLEM].Bridge = false;
                    Lemming[ActLEM].Basher = false;
                    Lemming[ActLEM].Miner = false;
                    Lemming[ActLEM].Actualframe = 0;
                    Lemming[ActLEM].Numframes = SizeSprites.bomber_frames;
                }
            }
            int framereal55;
            if (Lemming[ActLEM].Burned) // scale POSDraw x+0,y+0 at 1.2f x-5,y+0 at 1.35f
            {
                spriteBatch.Draw(MyGame.Instance.Gfx.Squemado, new Vector2(Lemming[ActLEM].PosX - ScrollX - 5, Lemming[ActLEM].PosY - ScrollY), new Rectangle(0, Lemming[ActLEM].Actualframe * 28, 32, 28),
                (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
                spriteBatch.Draw(MyGame.Instance.Gfx.Lhiss, new Vector2(Lemming[ActLEM].PosX - ScrollX, Lemming[ActLEM].PosY - 20 - ScrollY), new Rectangle(0, 0, MyGame.Instance.Gfx.Lhiss.Width, MyGame.Instance.Gfx.Lhiss.Height),
                    Color.White, 0f, Vector2.Zero, (0.5f + (0.01f * Lemming[ActLEM].Actualframe)), SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Drown) // scale POSDraw x+0,y+10 at 1.2f x-8,y+7 at 1.35f  //puto ahoga
            {
                spriteBatch.Draw(MyGame.Instance.Sprites.Drowner, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.water_xpos, Lemming[ActLEM].PosY + SizeSprites.water_ypos - ScrollY), new Rectangle(Lemming[ActLEM].Actualframe * SizeSprites.water_with, 0, SizeSprites.water_with, SizeSprites.water_height),
                    (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.water_size, SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Walker)
            {
                framereal55 = (Lemming[ActLEM].Actualframe * SizeSprites.walker_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Walker, new Vector2((Lemming[ActLEM].PosX - ScrollX + SizeSprites.walker_xpos), Lemming[ActLEM].PosY - ScrollY + SizeSprites.walker_ypos), new Rectangle(framereal55, 0, SizeSprites.walker_with, SizeSprites.walker_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.walker_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Blocker) // blocker scale POSDraw x-5 y+4 at 1.2f x-7 y+1 at 1.35f  //puto
            {
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.blocker_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Blocker, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.blocker_xpos, Lemming[ActLEM].PosY + SizeSprites.blocker_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.blocker_with, SizeSprites.blocker_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.blocker_size, SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
                if (MyGame.Instance.DebugOsd.Debug)
                {
                    Rectangle bloqueo = new(Lemming[ActLEM].PosX, Lemming[ActLEM].PosY, 28, 28);
                    spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(bloqueo.Left - ScrollX, bloqueo.Top - ScrollY, bloqueo.Width, bloqueo.Height), null,
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
            }
            if (Lemming[ActLEM].Bridge) // scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f
            {
                framesale = (Lemming[ActLEM].Actualframe * 26);
                spriteBatch.Draw(MyGame.Instance.Gfx.Puente_nomas, new Vector2(Lemming[ActLEM].PosX - ScrollX - 7, Lemming[ActLEM].PosY - 7 - ScrollY), new Rectangle(0, framesale, 32, 26), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, (Lemming[ActLEM].Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Builder)  //scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f  builder builder draws
            {
                if (Lemming[ActLEM].Numstairs >= 10) // chink draws
                {
                    spriteBatch.Draw(MyGame.Instance.Sprites.Chink, new Vector2(Lemming[ActLEM].PosX - ScrollX - 10, Lemming[ActLEM].PosY - 30 - ScrollY), new Rectangle(0, 0, MyGame.Instance.Sprites.Chink.Width, MyGame.Instance.Sprites.Chink.Height),
                        Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * Lemming[ActLEM].Actualframe), SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
                }
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.builder_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Puente, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.builder_xpos, Lemming[ActLEM].PosY + SizeSprites.builder_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.builder_with, SizeSprites.builder_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.builder_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Miner)  //scale POSDraw x-5,y-2 at 1.2f x-9,y-7 at 1.35f pico pico miner miner
            {
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.pico_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Pico, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.pico_xpos + (Lemming[ActLEM].Right ? 0 : 10), Lemming[ActLEM].PosY + SizeSprites.pico_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.pico_with, SizeSprites.pico_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.pico_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Basher) //puto
            {           // scale basher RIGHT POSDRAW x-10,y+4 at 1.2f x-15,y+1 at 1.35f
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.basher_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Pared, new Vector2(Lemming[ActLEM].PosX - ScrollX + (Lemming[ActLEM].Right ? SizeSprites.basher_xpos : SizeSprites.basher_xposleft), Lemming[ActLEM].PosY + SizeSprites.basher_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.basher_with, SizeSprites.basher_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.basher_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Explode) // explotando explotando bomber bomber
            {
                // bomber scale POSDraw x-5,y+4 at 1.2f x-9,y+2 at 1.35f
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.bomber_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Exploder, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.bomber_xpos, Lemming[ActLEM].PosY + SizeSprites.bomber_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.bomber_with, SizeSprites.bomber_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.bomber_size, SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
                spriteBatch.Draw(MyGame.Instance.Sprites.Lohno, new Vector2(Lemming[ActLEM].PosX - ScrollX - 20, Lemming[ActLEM].PosY - 25 - ScrollY), new Rectangle(0, 0, MyGame.Instance.Sprites.Lohno.Width, MyGame.Instance.Sprites.Lohno.Height),
                    Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * Lemming[ActLEM].Actualframe), SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Breakfloor) // scale POSDraw x-5,y+4 at 1.2f  x-9,y+2 at 1.35f breakfloor breakfloor
            {
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.floor_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Rompesuelo, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.floor_xpos, Lemming[ActLEM].PosY + SizeSprites.floor_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.floor_with, SizeSprites.floor_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floor_size, SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
                if (Lemming[ActLEM].Actualframe == SizeSprites.floor_frames - 1)
                {
                    Lemming[ActLEM].Dead = true;
                    Numlemnow--;
                    Lemming[ActLEM].Explode = false;
                    Lemming[ActLEM].Exploser = false;
                }
            }
            if (Lemming[ActLEM].Falling) //umbrella paraguas falling with umbrella
            {
                if (!Lemming[ActLEM].Framescut && Lemming[ActLEM].Actualframe == SizeSprites.floater_frames - 1)
                {
                    Lemming[ActLEM].Framescut = true;
                    Lemming[ActLEM].Actualframe = 0;
                    Lemming[ActLEM].Numframes = SizeSprites.floater_frames - 1 - 4;
                }
                if (!Lemming[ActLEM].Framescut)
                    framesale = (Lemming[ActLEM].Actualframe * SizeSprites.floater_with);
                else
                    framesale = (Lemming[ActLEM].Actualframe + 4) * SizeSprites.floater_with; // scale floater POSDraw x-5,y-4 at 1.2f x-9,y-7 at 1.35f
                spriteBatch.Draw(MyGame.Instance.Sprites.Paraguas, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.floater_xpos, Lemming[ActLEM].PosY + SizeSprites.floater_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.floater_with, SizeSprites.floater_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floater_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Fall) //fall cae
            {
                framereal55 = (Lemming[ActLEM].Actualframe * SizeSprites.faller_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Falling, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.faller_xpos, Lemming[ActLEM].PosY - ScrollY + SizeSprites.faller_ypos), new Rectangle(framereal55, 0, SizeSprites.faller_with, SizeSprites.faller_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.faller_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Exit && !Lemming[ActLEM].Dead) //sale sale exit exit out out
            {
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.sale_with); // exit scale POSDraw  x-1,y+1 at 1.2f x-3,y-1 at 1.35f
                spriteBatch.Draw(sale, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.sale_xpos, Lemming[ActLEM].PosY + SizeSprites.sale_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.sale_with, SizeSprites.sale_height), Color.White, 0f, Vector2.Zero, SizeSprites.sale_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
            if (Lemming[ActLEM].Digger)
            {
                framereal55 = (Lemming[ActLEM].Actualframe * SizeSprites.digger_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Digger, new Vector2(Lemming[ActLEM].PosX - ScrollX + SizeSprites.digger_xpos, Lemming[ActLEM].PosY + 6 - ScrollY + SizeSprites.digger_ypos), new Rectangle(framereal55, 0, SizeSprites.digger_with, SizeSprites.digger_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.digger_size, SpriteEffects.None, Lem_depth + (ActLEM * 0.00001f));
            }

            if (Lemming[ActLEM].Climbing) // scale POSDraw x-5,y+6 at 1.2f x-8.y+3 at 1.35f  //puto33
            {
                framesale = (Lemming[ActLEM].Actualframe * SizeSprites.climber_with);
                spriteBatch.Draw(MyGame.Instance.Sprites.Climber, new Vector2(Lemming[ActLEM].PosX - ScrollX + (Lemming[ActLEM].Right ? SizeSprites.climber_xpos : SizeSprites.climber_xposleft), Lemming[ActLEM].PosY + SizeSprites.climber_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.climber_with, SizeSprites.climber_height), (Lemming[ActLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.climber_size, (Lemming[ActLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (ActLEM * 0.00001f));
            }
        }
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
        if (MouseOnLem)
        {
            LemSkill = "";
        }

        vectorFill.X = 650;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem("Home:" + string.Format("{0}", NumSaved) + "/" + string.Format("{0}", Lemsneeded), vectorFill, Color.Cyan, 1f, 0.1f, spriteBatch);
        vectorFill.X = 320;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem("Out:" + string.Format("{0}", NumLemmings) + "/" + string.Format("{0}", Numlems), vectorFill, Color.Magenta, 1f, 0.1f, spriteBatch);
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
        if (Exploding && dibuja3 && !GlobalConst.Paused)  //logic explosions particles
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
        if (!LevelEnded && ((AllBlow && Numlemnow == 0) || ZvTime < 0 || (NumLemmings == Numlems && Numlemnow == 0)))
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
            if (Input.PreviousKeyState.IsKeyDown(Keys.LeftShift) || Input.PreviousKeyState.IsKeyDown(Keys.RightShift))
                ScrollX -= 10;
            Scrolling();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.Right))
        {
            ScrollX += 5;
            if (Input.PreviousKeyState.IsKeyDown(Keys.LeftShift) || Input.PreviousKeyState.IsKeyDown(Keys.RightShift))
                ScrollX += 10;
            Scrolling();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.Up))
        {
            ScrollY -= 5;
            if (Input.PreviousKeyState.IsKeyDown(Keys.LeftShift) || Input.PreviousKeyState.IsKeyDown(Keys.RightShift))
                ScrollY -= 10;
            Scrolling();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.Down))
        {
            ScrollY += 5;
            if (Input.PreviousKeyState.IsKeyDown(Keys.LeftShift) || Input.PreviousKeyState.IsKeyDown(Keys.RightShift))
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
            if (NumSaved >= Lemsneeded) //see here if level is finished or not
            {
                SaveGame.FinishedLevel[MyGame.Instance.CurrentLevelNumber] = true;
                SaveGame.SaveSavedGame();
                MyGame.Instance.CurrentLevelNumber++;
                if (MyGame.Instance.CurrentLevelNumber >= GlobalConst.NumTotalLevels - 1)
                    MyGame.Instance.CurrentLevelNumber = GlobalConst.NumTotalLevels - 1;
                MyGame.Instance.ScreenMainMenu.MouseLevelChoose = MyGame.Instance.CurrentLevelNumber;
                MyGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                Numlemnow = 0;
                NumLemmings = 0;
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

            if (ExitBad) //repeat level
            {
                MyGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                Numlemnow = 0;
                NumLemmings = 0;
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
            CurrentMusic.Stop();
            MyGame.Instance.ScreenMainMenu.MouseLevelChoose = 0;
            LevelEnded = false;
            ExitLevel = false;
            AllBlow = false;
            ZvTime = 0;
            ExitBad = false;
            NumLemmings = 0;
            MyGame.Instance.ReloadContent();
            MyGame.Instance.BackToMenu();
            return;
        }

        if (AllBlow && actualBlow < NumLemmings) // crash crash TEST TEST
        {
            if (!Lemming[actualBlow].Dead && !Lemming[actualBlow].Explode)
                Lemming[actualBlow].Exploser = true;
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
            int xx55 = MyGame.Instance.Levels.VarDoor[MyGame.Instance.Levels.AllLevel[MyGame.Instance.CurrentLevelNumber].TypeOfDoor].numFram - 1;
            frameDoor++;
            if (frameDoor == 1 && MyGame.Instance.Sfx.EntryLemmings.State == SoundState.Stopped && !doorWaveOn)
            {
                MyGame.Instance.Sfx.EntryLemmings.Play();
                doorWaveOn = true;
            }
            if (frameDoor > xx55)
            {
                CurrentMusic.IsLooped = true;
                CurrentMusic.Play();
                doorOn = false;
                frameDoor = xx55;
            }
        }
        bool pullLemmings = false;
        float delayPercent = 27 - _inGameMenu.FrequencyNumber * 0.26f; // see to fix speed of lemmings release on door only when change frecuency (not so good)
        if (Dibuja && !doorOn)
        {
            exitFrame++;
            if (exitFrame >= (int)delayPercent)
            {
                exitFrame = 0;
                pullLemmings = true;
            }
        }
        //test to see difference with anterior process
        if (pullLemmings && NumLemmings != Numlems && !AllBlow)
        {
            if (NumTOTdoors > 1 && MoreDoors != null) // more than 1 door is different calculation
            {
                door1Y = (int)MoreDoors[NumACTdoor].doorMoreXY.Y;
                door1X = (int)MoreDoors[NumACTdoor].doorMoreXY.X;
                NumACTdoor++;
                if (NumACTdoor >= NumTOTdoors)
                    NumACTdoor = 0;
                Lemming[NumLemmings].PosY = door1Y;
                Lemming[NumLemmings].PosX = door1X + 35;
            }
            else
            {
                Lemming[NumLemmings].PosY = door1Y;
                Lemming[NumLemmings].PosX = door1X + 35;
            }
            Lemming[NumLemmings].PosY = door1Y;
            Lemming[NumLemmings].PosX = door1X + 35;
            Lemming[NumLemmings].Numframes = 0;
            Lemming[NumLemmings].Right = true;
            Lemming[NumLemmings].Fall = true;
            Lemming[NumLemmings].Walker = false;
            Lemming[NumLemmings].Pixelscaida = 0;
            Lemming[NumLemmings].Numframes = SizeSprites.faller_frames;
            Lemming[NumLemmings].Actualframe = 0;
            Lemming[NumLemmings].Onmouse = false;
            Lemming[NumLemmings].Active = true;
            Lemming[NumLemmings].Exit = false;
            Lemming[NumLemmings].Dead = false;
            Lemming[NumLemmings].Digger = false;
            Lemming[NumLemmings].Climber = false;
            Lemming[NumLemmings].Climbing = false;
            Lemming[NumLemmings].Umbrella = false;
            Lemming[NumLemmings].Falling = false;
            Lemming[NumLemmings].Framescut = false;
            Lemming[NumLemmings].Breakfloor = false;
            Lemming[NumLemmings].Exploser = false;
            Lemming[NumLemmings].Explode = false;
            Lemming[NumLemmings].Time = 0;
            Lemming[NumLemmings].Blocker = false;
            Lemming[NumLemmings].Builder = false;
            Lemming[NumLemmings].Basher = false;
            Lemming[NumLemmings].Miner = false;
            Lemming[NumLemmings].Bridge = false;
            Lemming[NumLemmings].Burned = false;
            Lemming[NumLemmings].Drown = false;
            NumLemmings++;
            Numlemnow++;
        }

        for (actLEM2 = 0; actLEM2 < NumLemmings; actLEM2++)
        {
            x.X = Lemming[actLEM2].PosX + 14;
            x.Y = Lemming[actLEM2].PosY + 25;
            if (Lemming[actLEM2].Exit && Lemming[actLEM2].Actualframe == 13) // change frame of yipee sound, old frame was init or 0 now different for frames
            {
                MyGame.Instance.Sfx.Yippe.Replay();
            }
            if (Moreexits == null)
            {
                if (exit_rect.Contains(x) && !Lemming[actLEM2].Exit && !Lemming[actLEM2].Explode)
                {
                    Lemming[actLEM2].PosX = output1X - 19;
                    Lemming[actLEM2].PosY = output1Y - 30;
                    Lemming[actLEM2].Active = false;
                    Lemming[actLEM2].Walker = false;
                    Lemming[actLEM2].Fall = false;
                    Lemming[actLEM2].Falling = false;
                    Lemming[actLEM2].Exit = true;
                    Lemming[actLEM2].Numframes = SizeSprites.sale_frames;
                    Lemming[actLEM2].Actualframe = 0;
                }
            }
            else
            {
                for (ex11 = 0; ex11 < NumTOTexits; ex11++) // more than one EXIT place
                {
                    output1X = (int)Moreexits[ex11].exitMoreXY.X;
                    output1Y = (int)Moreexits[ex11].exitMoreXY.Y;
                    exit_rect.X = output1X - 5;
                    exit_rect.Y = output1Y - 5;
                    exit_rect.Width = 10;
                    exit_rect.Height = 10;
                    if (exit_rect.Contains(x) && !Lemming[actLEM2].Exit && !Lemming[actLEM2].Explode)
                    {
                        Lemming[actLEM2].PosX = output1X - 19; //14+5 middle of the exit rect
                        Lemming[actLEM2].PosY = output1Y - 30; //25+5
                        Lemming[actLEM2].Active = false;
                        Lemming[actLEM2].Walker = false;
                        Lemming[actLEM2].Fall = false;
                        Lemming[actLEM2].Falling = false;
                        Lemming[actLEM2].Exit = true;
                        Lemming[actLEM2].Numframes = SizeSprites.sale_frames;
                        Lemming[actLEM2].Actualframe = 0; // break; //i'm not sure if it's necessary this break
                    }
                }
            }
        }
    }
}
