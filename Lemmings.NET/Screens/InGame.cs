using System;
using System.IO;

using Lemmings.NET.Constants;
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
    internal bool[] LevelEnd { get; set; } = new bool[MyGame.NumTotalLevels]; //full number of levels to see which are finished or not

    private float Contadortime2;
    private double actWaves444, actWaves333, actWaves;
    private bool Exploding, dibuja3, LevelEnded, ExitBad, ExitLevel;
    private int numSaved, actItem, Iexplo, cantidad22;
    private int sx, rest = 0, framesale, Contador2, Contador = 1, actLEM2;
    private bool mouseOnLem;
    private bool doorOn = true;
    private Rectangle bloqueo;
    private double frameWaves;
    private int walker_frame;
    private int builder_frame;
    private readonly int builder_frame_second = 1;
    private int Frame3;
    private readonly int Framesecond = 6;
    private readonly int Framesecond2 = 2;
    private readonly int Framesecond3 = 1;  // frame speed less all go crazy 6->ok framesecond=6 default framesecond2=3 default
    private int door1X, door1Y, actLEM;
    private int output1X, output1Y, ex11;
    private int frameDoor, frameExit; // 0--10   0--6
    private int exitFrame = 999, actualBlow; // frecuency lemmings go in
    private Rectangle exit_rect; // rectangle exit
    private Point x;
    private Color[] C25 { get; set; } = new Color[4096 * 4096]; // Maximun size of a color array used for mask all the level
    private bool initON = false;
    private readonly int maxnumberfalling = 210;
    private readonly int useumbrella = 100;
    private int framereal565;
    private float DoorExitDepth = 0.403f;  // default value--bigger than 0.5f is behind the terrain (0.6f level 58 for example)
    private Vector2 vectorFill;
    private Rectangle rectangleFill, rectangleFill2;
    private Color colorFill;
    private bool _endSongPlayed;
    private readonly double GRAVITY = 0.1; //0.1
    private int Lemsneeded = 1;
    private int numlemnow;
    private int z1;
    private int z2;
    private int z3;
    private bool luzmas = true, luzmas2 = true, draw_walker, draw_builder;
    private int alto, _below;
    private Point poslem;
    private Rectangle arrowLem;
    private readonly int PARTICLE_NUM = 24; //24
    private readonly int LIFE_COUNTER = 74; //64 original value
    private readonly int LIFE_VARIANCE = 16; //16
    private int Numlems = 1;
    private readonly Color[] Colormask22 = new Color[500 * 512];
    private readonly float Lem_depth = 0.300f;
    private readonly Color[] Colorsobre22 = new Color[500 * 512];
    private readonly Color[] Colormasktotal = new Color[500 * 512];
    private readonly Color[] Colormask2 = new Color[20 * 20];  // miner mask 20*20 && basher too 20*20
    private readonly Color[] Colorsobre2 = new Color[20 * 20];
    private int Frente;
    private int Frente2;
    private bool doorWaveOn;
    private int frameact;
    private readonly bool LockMouse;
    private readonly float SizeL = 1.35f; //1.2f was default in the beggining
    private Texture2D salida_ani1, salida_ani1_1, sale;
    private readonly Color[] Colormask33 = new Color[38 * 53]; // explode mask 38*53
    private readonly Color[] Colorsobre33 = new Color[38 * 53];
    private Texture2D puerta_ani;
    private readonly InGameMenu _inGameMenu;

    internal InGame()
    {
        _inGameMenu = new InGameMenu(this);
        LockMouse = false;
    }

    internal void LoadLevel(int newLevel, ContentManager content)
    {
        if (LemmingsNetGame.Instance.Music.WinMusic.State == SoundState.Playing)
            LemmingsNetGame.Instance.Music.WinMusic.Stop();
        if (LemmingsNetGame.Instance.Music.MenuMusic.State == SoundState.Playing)
            LemmingsNetGame.Instance.Music.MenuMusic.Stop();
        numlemnow = 0;
        numSaved = 0;
        frameDoor = 0;
        frameExit = 0;
        Frame3 = 0;
        Fade = true;
        doorOn = true;
        MillisecondsElapsed = 0;
        NumLemmings = 0;
        puerta_ani = content.Load<Texture2D>("puerta" + string.Format("{0}", LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor)); // type of door puerta1-2-3-4 etc.
        string xx455 = string.Format("{0}", LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit);
        salida_ani1 = content.Load<Texture2D>("salida" + xx455);
        salida_ani1_1 = content.Load<Texture2D>("salida" + xx455 + "_1");
        sale = content.Load<Texture2D>("sale");
        LemmingsNetGame.Instance.CurrentLevelNumber = newLevel;
        LemSkill = "";
        MyGame.Paused = false;
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

        Texture2D level = LemmingsNetGame.Instance.Content.Load<Texture2D>(LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].NameLev);
        Earth = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, level.Width, level.Height);
        Color[] pixels = new Color[level.Width * level.Height];
        level.GetData(pixels);
        Earth.SetData(pixels);
        Earth.GetData(C25, 0, Earth.Height * Earth.Width); //better here than moverlemming() for performance see issues 
                                                           //see differences with old getdata, see size important (x * y)
        door1X = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].doorX;
        door1Y = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].doorY;
        output1X = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].exitX;
        output1Y = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].exitY;
        // this is the depth of the exit and doors animated sprites -- See level 58 the exit is behind the mountain (0.6f)
        if (LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].DoorExitDepth != 0)
        {
            DoorExitDepth = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].DoorExitDepth;
        }
        else
        {
            DoorExitDepth = 0.403f;
        }
        NbClimberRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberClimbers;
        NbFloaterRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberUmbrellas;
        NbExploderRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberExploders;
        NbBlockerRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBlockers;
        NbBuilderRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBuilders;
        NbBasherRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBashers;
        NbMinerRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberMiners;
        NbDiggerRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberDiggers;
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
        Numlems = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TotalLemmings;
        Lemsneeded = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].NbLemmingsToSave;
        ScrollX = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].InitPosX;
        ScrollY = 0;
        Lemming = new Lem[Numlems];
        LemmingsNetGame.Instance.Levels.VariablesTraps();
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
        draw_walker = false;
        draw_builder = false;
        if (walker_frame > SizeSprites.walker_framesecond)
        {
            walker_frame = 0;
            draw_walker = true;
        }
        if (builder_frame > builder_frame_second)
        {
            builder_frame = 0;
            draw_builder = true;
        }
        if (Frame2 > Framesecond)
        {
            Frame2 = 0;
            Dibuja = true;
            if (!MyGame.Paused)
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
            MyGame.Paused = true;
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
        if (PlatsON && !MyGame.Paused)
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
                    if (LemmingsNetGame.Instance.DebugOsd.debug)
                        Earth.SetData(C25, 0, Earth.Width * Earth.Height); //set this only for debugger and see the real c25 redraw
                }
                Plats[i].frame++;
            }
        }

        if (AddsON && !MyGame.Paused)
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
        if (TrapsON && Dibuja && !MyGame.Paused)
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
        if (!MyGame.Paused)
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
            int xx66 = LemmingsNetGame.Instance.Levels.VarExit[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].numFram - 1;
            frameExit++;
            if (frameExit > xx66)
            {
                frameExit = 0;
            }
        }
        if (!MyGame.Paused)
            Door();
        _inGameMenu.Update();
        MyTexture = LemmingsNetGame.Instance.Content.Load<Texture2D>("luces/" + Contador);// okokokokokokokok

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
        mouseOnLem = false;  // scroll mouse on level landscape
        Scrolling();
        for (actLEM = 0; actLEM < NumLemmings; actLEM++)
        {
            if (doorOn)
                break; // start when door finish opening
            if (Lemming[actLEM].Dead)
                continue;
            // LOGIC BLOCKER BLOCKER BLOQUEO LOGIC bbbbbbbbbbbbbbbbbbbbllllllllloooooooccccccccccckkkkkkkkkkkeeeeeeeeeeeeedddddddddddddddddd
            int medx = 14;
            int medy = 14;
            for (int b = 0; b < NumLemmings; b++)
            {
                if (Lemming[b].Blocker && b != actLEM)
                {
                    bloqueo.X = Lemming[b].PosX;
                    bloqueo.Y = Lemming[b].PosY;
                    bloqueo.Width = 28;
                    bloqueo.Height = 28;
                    if (Lemming[actLEM].Miner)
                    {
                        bloqueo.X = Lemming[b].PosX + 10;
                        bloqueo.Y = Lemming[b].PosY;
                        bloqueo.Width = 9;
                        bloqueo.Height = 28;
                    }
                    poslem.X = Lemming[actLEM].PosX + medx;
                    poslem.Y = Lemming[actLEM].PosY + medy;
                    if (bloqueo.Contains(poslem))
                    {
                        if (Lemming[actLEM].Right)
                        {
                            if (Lemming[actLEM].PosX < Lemming[b].PosX)
                            {
                                Lemming[actLEM].Right = false;
                                break;
                            }
                        }
                        else
                        {
                            if (Lemming[actLEM].PosX > Lemming[b].PosX - 1)
                            {
                                Lemming[actLEM].Right = true;
                                break;
                            }
                        }
                        break;
                    }
                }

            }
            Lemming[actLEM].Onmouse = false; //LEMMING SKILL STRING MOUSE ON
            if ((Input.CurrentMouseState.X + 16 >= Lemming[actLEM].PosX - ScrollX && Input.CurrentMouseState.X + 16 <= Lemming[actLEM].PosX - ScrollX + 28
                    && Input.CurrentMouseState.Y + 16 >= Lemming[actLEM].PosY - ScrollY && Input.CurrentMouseState.Y + 16 <= Lemming[actLEM].PosY + 28 - ScrollY) && !mouseOnLem)
            {
                if (Lemming[actLEM].Walker)
                    LemSkill = "Walker";
                if (Lemming[actLEM].Builder)
                    LemSkill = "Builder";
                if (Lemming[actLEM].Basher)
                    LemSkill = "Basher";
                if (Lemming[actLEM].Blocker)
                    LemSkill = "Blocker";
                if (Lemming[actLEM].Miner)
                    LemSkill = "Miner";
                if (Lemming[actLEM].Digger)
                    LemSkill = "Digger";
                if (Lemming[actLEM].Climber)
                    LemSkill += ",C";
                if (Lemming[actLEM].Umbrella)
                    LemSkill += ",F";
                if (Lemming[actLEM].Climbing)
                    LemSkill = "Climber";
                if (Lemming[actLEM].Climbing && Lemming[actLEM].Umbrella)
                    LemSkill = "Climber,F";
                if ((Lemming[actLEM].Fall || Lemming[actLEM].Falling) && !Lemming[actLEM].Umbrella)
                    LemSkill = "Faller";
                if ((Lemming[actLEM].Fall || Lemming[actLEM].Falling) && Lemming[actLEM].Umbrella)
                    LemSkill = "Floater";
                mouseOnLem = true;
                Lemming[actLEM].Onmouse = true;
            } //  inside the mouse rectangle lemming ON
            if (TrapsON && !MyGame.Paused) //Traps logic and sounds
            {
                for (int ti = 0; ti < NumTotTraps; ti++)
                {
                    x.X = Lemming[actLEM].PosX + 14;
                    x.Y = Lemming[actLEM].PosY + 25;
                    if (Trap[ti].areaTrap.Contains(x) && !Trap[ti].isOn && Trap[ti].type == 666)
                    {
                        Trap[ti].isOn = true;
                        Lemming[actLEM].Active = false;
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Dead = true;
                        numlemnow--;
                        Lemming[actLEM].Explode = false;
                        Lemming[actLEM].Exploser = false;
                        switch (Trap[ti].sprite.Name)
                        {
                            case "traps/dead_marble":
                            case "traps/dead_marble2_fix":
                                if (LemmingsNetGame.Instance.Sfx.StrapTenton.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.StrapTenton.Stop();
                                }
                                LemmingsNetGame.Instance.Sfx.StrapTenton.Play();
                                break;
                            case "traps/dead_trampa":
                                if (LemmingsNetGame.Instance.Sfx.StrapMan.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.StrapMan.Stop();
                                }
                                LemmingsNetGame.Instance.Sfx.StrapMan.Play();
                                break;
                            case "traps/dead_soga":
                                if (LemmingsNetGame.Instance.Sfx.StrapChain.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.StrapChain.Stop();
                                }
                                LemmingsNetGame.Instance.Sfx.StrapChain.Play();
                                break;
                            case "traps/dead_bombona":
                                if (LemmingsNetGame.Instance.Sfx.StrapChupar.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.StrapChupar.Stop();
                                }
                                LemmingsNetGame.Instance.Sfx.StrapChupar.Play();
                                break;
                            case "traps/dead_10":
                                if (LemmingsNetGame.Instance.Sfx.StrapTenTonnes.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.StrapTenTonnes.Stop();
                                }
                                LemmingsNetGame.Instance.Sfx.StrapTenTonnes.Play();
                                break;
                            default:
                                if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.Die.Stop();
                                }
                                try
                                {
                                    LemmingsNetGame.Instance.Sfx.Die.Play();
                                }
                                catch (InstancePlayLimitException) { /* Ignore errors */ }
                                break;
                        }
                        break;
                    }
                    rectangleFill.X = Lemming[actLEM].PosX + 14;
                    rectangleFill.Y = Lemming[actLEM].PosY;
                    rectangleFill.Width = 1;
                    rectangleFill.Height = 28;
                    if (Trap[ti].areaTrap.Intersects(rectangleFill) && !Lemming[actLEM].Burned && !Lemming[actLEM].Drown && Trap[ti].type != 666)
                    {
                        switch (Trap[ti].sprite.Name)
                        {
                            case "traps/dead_spin":
                            case "traps/fuego1":
                            case "traps/fuego2":
                            case "traps/fuego3":
                            case "traps/fuego4":
                                if (LemmingsNetGame.Instance.Sfx.Fire.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.Fire.Stop();
                                }
                                try
                                {
                                    LemmingsNetGame.Instance.Sfx.Fire.Play();
                                }
                                catch (InstancePlayLimitException) { /* Ignore errors */ }
                                Lemming[actLEM].Burned = true;
                                Lemming[actLEM].Drown = false;
                                Lemming[actLEM].Explode = false;
                                Lemming[actLEM].Exploser = false;
                                Lemming[actLEM].Numframes = 14;
                                Lemming[actLEM].Actualframe = 0;
                                Lemming[actLEM].Active = false;
                                Lemming[actLEM].Walker = false;
                                Lemming[actLEM].Falling = false;
                                Lemming[actLEM].Fall = false;
                                break;
                            case "traps/acid":
                            case "traps/acid2":
                            case "traps/ice_water":
                            case "traps/ice_water2":
                            case "traps/water_blue":
                            case "traps/water_bubbles":
                                if (LemmingsNetGame.Instance.Sfx.Glup.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.Glup.Stop();
                                }
                                try
                                {
                                    LemmingsNetGame.Instance.Sfx.Glup.Play();
                                }
                                catch (InstancePlayLimitException) { /* Ignore errors */ }
                                Lemming[actLEM].Drown = true;
                                Lemming[actLEM].Burned = false;
                                Lemming[actLEM].Explode = false;
                                Lemming[actLEM].Exploser = false;
                                Lemming[actLEM].Falling = false;
                                Lemming[actLEM].Fall = false;
                                Lemming[actLEM].Numframes = SizeSprites.water_frames;
                                Lemming[actLEM].Actualframe = 0;
                                Lemming[actLEM].Active = false;
                                Lemming[actLEM].Walker = false;
                                break;
                            default:
                                if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.Die.Stop();
                                }
                                try
                                {
                                    LemmingsNetGame.Instance.Sfx.Die.Play();
                                }
                                catch (InstancePlayLimitException) { /* Ignore errors */ }
                                Lemming[actLEM].Explode = false;
                                Lemming[actLEM].Exploser = false;
                                Lemming[actLEM].Active = false;
                                Lemming[actLEM].Walker = false;
                                Lemming[actLEM].Dead = true;
                                numlemnow--;
                                break;
                        }
                    }
                }
            }
            // assign skills to lemmings //////////////////////////////////////////////
            if (mouseOnLem && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
            {
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.DIGGER && !Lemming[actLEM].Digger && Lemming[actLEM].Onmouse //DIGGER
                    && (Lemming[actLEM].Walker || Lemming[actLEM].Builder || Lemming[actLEM].Basher || Lemming[actLEM].Miner))
                {
                    NbDiggerRemaining--;
                    if (NbDiggerRemaining < 0)
                    {
                        NbDiggerRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Digger = true;
                        Lemming[actLEM].Fall = false;
                        Lemming[actLEM].Builder = false;
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Miner = false;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.digger_frames;
                        continue;
                    }
                }
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.CLIMBER && Lemming[actLEM].Onmouse && !Lemming[actLEM].Climber) //CLIMBER
                {
                    NbClimberRemaining--;
                    if (NbClimberRemaining < 0)
                    {
                        NbClimberRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Climber = true;
                        continue;
                    }
                }
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.FLOATER && Lemming[actLEM].Onmouse && !Lemming[actLEM].Umbrella && !Lemming[actLEM].Breakfloor) //FLOATER
                {
                    NbFloaterRemaining--;
                    if (NbFloaterRemaining < 0)
                    {
                        NbFloaterRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Umbrella = true;
                        continue;
                    }
                }
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.EXPLODER && Lemming[actLEM].Onmouse && !Lemming[actLEM].Exploser) //BOMBER
                {
                    NbExploderRemaining--;
                    if (NbExploderRemaining < 0)
                    {
                        NbExploderRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Exploser = true;
                        continue;
                    }
                }
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.BLOCKER && Lemming[actLEM].Onmouse && !Lemming[actLEM].Blocker //BLOCKER
                    && (Lemming[actLEM].Walker || Lemming[actLEM].Digger || Lemming[actLEM].Builder || Lemming[actLEM].Basher || Lemming[actLEM].Miner))
                {
                    NbBlockerRemaining--;
                    if (NbBlockerRemaining < 0)
                    {
                        NbBlockerRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Blocker = true;
                        Lemming[actLEM].Builder = false;
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Miner = false;
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Digger = false;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.blocker_frames;
                        continue;
                    }
                }
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.BUILDER && Lemming[actLEM].Onmouse && !Lemming[actLEM].Builder //BUILDER
                    && (Lemming[actLEM].Walker || Lemming[actLEM].Digger || Lemming[actLEM].Basher || Lemming[actLEM].Miner || Lemming[actLEM].Bridge))
                {
                    NbBuilderRemaining--;
                    if (NbBuilderRemaining < 0)
                    {
                        NbBuilderRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Bridge = false;
                        Lemming[actLEM].Builder = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Digger = false;
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Miner = false;
                        Lemming[actLEM].Numstairs = 0;
                        Lemming[actLEM].Numframes = SizeSprites.builder_frames;
                        continue;
                    }
                }
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.BASHER && Lemming[actLEM].Onmouse && !Lemming[actLEM].Basher //BASHER
                    && (Lemming[actLEM].Walker || Lemming[actLEM].Digger || Lemming[actLEM].Builder || Lemming[actLEM].Miner))
                {
                    NbBasherRemaining--;
                    if (NbBasherRemaining < 0)
                    {
                        NbBasherRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Basher = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Digger = false;
                        Lemming[actLEM].Builder = false;
                        Lemming[actLEM].Miner = false;
                        Lemming[actLEM].Numframes = SizeSprites.basher_frames;
                        continue;
                    }
                }
                if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.MINER && Lemming[actLEM].Onmouse && !Lemming[actLEM].Miner //MINER
                    && (Lemming[actLEM].Walker || Lemming[actLEM].Digger || Lemming[actLEM].Basher || Lemming[actLEM].Builder))
                {
                    NbMinerRemaining--;
                    if (NbMinerRemaining < 0)
                    {
                        NbMinerRemaining = 0;
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.MousePre.Play();
                        Lemming[actLEM].Miner = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Digger = false;
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Builder = false;
                        Lemming[actLEM].Numframes = SizeSprites.pico_frames;
                        continue;
                    }
                }

            }
            if (MyGame.Paused)
                continue;
            if (draw_builder && Lemming[actLEM].Builder)
            {
                Lemming[actLEM].Actualframe++;
                if (Lemming[actLEM].Actualframe > Lemming[actLEM].Numframes - 1 && !Lemming[actLEM].Explode)
                {
                    Lemming[actLEM].Actualframe = 0;
                }
            }
            if (draw_walker && !Lemming[actLEM].Builder && !Lemming[actLEM].Basher && !Lemming[actLEM].Miner
                && !Lemming[actLEM].Burned && !Lemming[actLEM].Drown)
            {
                Lemming[actLEM].Actualframe++;
                if (Lemming[actLEM].Actualframe > Lemming[actLEM].Numframes - 1 && !Lemming[actLEM].Explode)
                {
                    Lemming[actLEM].Actualframe = 0;
                }
                //be carefull with bomber frames actualization
            }
            if (Draw2 && (Lemming[actLEM].Basher || Lemming[actLEM].Miner
                || Lemming[actLEM].Burned || Lemming[actLEM].Drown)) // see careful frames
            {
                Lemming[actLEM].Actualframe++;
                if ((Lemming[actLEM].Burned || Lemming[actLEM].Drown) &&
                    (Lemming[actLEM].Actualframe > Lemming[actLEM].Numframes - 1))
                {
                    Lemming[actLEM].Burned = false;
                    Lemming[actLEM].Drown = false;
                    Lemming[actLEM].Dead = true;
                    Lemming[actLEM].Explode = false;
                    Lemming[actLEM].Exploser = false;
                    numlemnow--;
                }
                if (Lemming[actLEM].Actualframe > Lemming[actLEM].Numframes - 1 && !Lemming[actLEM].Explode)
                {
                    Lemming[actLEM].Actualframe = 0;
                }
            }
            if (Lemming[actLEM].Exit)
            {
                if (Lemming[actLEM].Actualframe == Lemming[actLEM].Numframes - 1)
                {
                    Lemming[actLEM].Dead = true;
                    Lemming[actLEM].Explode = false;
                    Lemming[actLEM].Exploser = false;
                    numlemnow--;
                    numSaved++;  // here is where the lemming go inside after door animation
                }
                continue;
            }
            int arriba = 0;
            _below = 0;
            int pixx = Lemming[actLEM].PosX + medx;
            for (int x55 = 0; x55 <= 8; x55++)
            {
                int pos_real = Lemming[actLEM].PosY + x55 + medy + medy;  ///////////// pixel por debajo -> beneath.............
                if (pos_real == Earth.Height)
                {
                    _below = 9;
                    break;
                }
                if (pos_real < 0)
                    pos_real = 0;
                if (pos_real > Earth.Height)
                {
                    Lemming[actLEM].Dead = true;
                    _below = 9;
                    Lemming[actLEM].Active = false;
                    numlemnow--;
                    Lemming[actLEM].Explode = false;
                    Lemming[actLEM].Exploser = false;
                    if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Die.Stop();
                    }
                    try
                    {
                        LemmingsNetGame.Instance.Sfx.Die.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                    break;
                }
                if (C25[(pos_real * Earth.Width) + pixx].R == 0 && C25[(pos_real * Earth.Width) + pixx].G == 0 && C25[(pos_real * Earth.Width) + pixx].B == 0)
                {
                    _below++;
                }
                else
                {
                    break;
                }
            }
            // very important to check digger and miner before change to falling
            if (Lemming[actLEM].Pixelscaida > useumbrella && !Lemming[actLEM].Falling && Lemming[actLEM].Umbrella
                && (!Lemming[actLEM].Digger && !Lemming[actLEM].Miner && !Lemming[actLEM].Builder) && Lemming[actLEM].Active)
            {
                Lemming[actLEM].Pixelscaida = 11;
                Lemming[actLEM].Falling = true;
                Lemming[actLEM].Fall = false;
                Lemming[actLEM].Actualframe = 0;
                Lemming[actLEM].Numframes = SizeSprites.floater_frames;
            }
            if ((_below > 8 && !Lemming[actLEM].Fall && (!Lemming[actLEM].Digger || !Lemming[actLEM].Miner)) && !Lemming[actLEM].Falling
                && !Lemming[actLEM].Explode && Lemming[actLEM].Active)
            {
                Lemming[actLEM].Fall = true;
                Lemming[actLEM].Pixelscaida = 0;
                Lemming[actLEM].Climbing = false;
                Lemming[actLEM].Walker = false;
                Lemming[actLEM].Actualframe = 0;
                Lemming[actLEM].Numframes = SizeSprites.faller_frames;
                Lemming[actLEM].Basher = false;
                Lemming[actLEM].Builder = false;
                Lemming[actLEM].Bridge = false;
                Lemming[actLEM].Miner = false;
                continue; // lemming fall when there's no floor on feet and fall down
            }
            if ((_below == 0) && (Lemming[actLEM].Fall || Lemming[actLEM].Falling) && (!Lemming[actLEM].Digger && !Lemming[actLEM].Miner)) //OJO LOCO A VECES AL CAVAR Y SIGUE WALKER
            {
                if (Lemming[actLEM].Pixelscaida <= maxnumberfalling)
                {
                    Lemming[actLEM].Pixelscaida = 0;
                    Lemming[actLEM].Framescut = false;
                    Lemming[actLEM].Falling = false;
                    Lemming[actLEM].Walker = true;
                    Lemming[actLEM].Fall = false;
                    Lemming[actLEM].Actualframe = 0;
                    Lemming[actLEM].Numframes = SizeSprites.walker_frames;  //8 walker;4 fall;16 digger;breakfloor 16;escala ...
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.Splat.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Splat.Stop();
                    }
                    try
                    {
                        LemmingsNetGame.Instance.Sfx.Splat.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                    Lemming[actLEM].Fall = false;
                    Lemming[actLEM].Walker = false;
                    Lemming[actLEM].Falling = false;
                    Lemming[actLEM].Explode = false;
                    Lemming[actLEM].Exploser = false;
                    Lemming[actLEM].Active = false;
                    Lemming[actLEM].Breakfloor = true;
                    Lemming[actLEM].Umbrella = false;
                    Lemming[actLEM].Numframes = SizeSprites.floor_frames;
                    Lemming[actLEM].Actualframe = 0;
                    continue;
                }
            }
            if ((_below == 0) && Lemming[actLEM].Walker && (!Lemming[actLEM].Digger && !Lemming[actLEM].Miner))
            {
                Lemming[actLEM].Pixelscaida = 0;
            }
            for (int x55 = 0; x55 <= 20; x55++)
            {
                int pos_real = Lemming[actLEM].PosY + medy + medy - x55;
                if (pos_real == Earth.Height)    // rompe los calculos si sale de la pantalla o se cuelga AARRIBBBAAAA
                {
                    Lemming[actLEM].Active = false;
                    break;
                }
                if (pos_real < Earth.Height && pos_real > 0)
                {
                    if (C25[(pos_real * Earth.Width) + pixx].R > 0 || C25[(pos_real * Earth.Width) + pixx].G > 0 || C25[(pos_real * Earth.Width) + pixx].B > 0)
                    {
                        arriba++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (Lemming[actLEM].Blocker && _below > 2)
            {
                Lemming[actLEM].Blocker = false;
                Lemming[actLEM].Fall = true;
                Lemming[actLEM].Pixelscaida = 0;
                Lemming[actLEM].Actualframe = 0;
                Lemming[actLEM].Numframes = SizeSprites.faller_frames;
                continue;
            }
            if (Lemming[actLEM].Miner && Draw2 && Lemming[actLEM].Actualframe == 42)  // miner logic pico logic
            {
                if (ArrowsON) // miner arrows logic areaTrap Intersects
                {
                    bool nominer = false;
                    arrowLem.X = Lemming[actLEM].PosX;
                    arrowLem.Y = Lemming[actLEM].PosY;
                    arrowLem.Width = 28;
                    arrowLem.Height = 28;
                    for (int wer3 = 0; wer3 < NumTotArrow; wer3++)
                    {
                        if (Arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Right && !Arrow[wer3].right)
                        {
                            nominer = true;
                            continue;
                        }
                        if (Arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Left && Arrow[wer3].right)
                        {
                            nominer = true;
                        }
                    }
                    if (nominer)
                    {
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                        Lemming[actLEM].Miner = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        continue;
                    }
                }
                if (Lemming[actLEM].Right)
                {
                    int width2 = 20;
                    int top2 = 20;
                    int px = Lemming[actLEM].PosX + 12;
                    int py = Lemming[actLEM].PosY + 14;
                    if (py < 0) // top of the level
                    {
                        py = 0;
                    }
                    if (px < 0) // left of the level
                    {
                        px = 0;
                    }
                    if (px + width2 >= Earth.Width)
                    {
                        width2 = Earth.Width - px;
                    }
                    if (py + top2 >= Earth.Height)
                    {
                        top2 = Earth.Height - py;
                    }
                    LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(Colormask2);
                    //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                    int amount = 0;
                    for (int yy88 = 0; yy88 < top2; yy88++)
                    {
                        int yypos888 = (yy88 + py) * Earth.Width;
                        for (int xx88 = 0; xx88 < width2; xx88++)
                        {
                            Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                            amount++;
                        }
                    }
                    for (int r = 0; r < amount; r++)
                    {
                        if (SteelON)
                        {
                            sx = r % width2;
                            int sy = r / width2;
                            x.X = px + sx;
                            x.Y = py + sy;
                            for (int xz = 0; xz < NumTOTsteel; xz++)
                            {
                                if (Steel[xz].area.Contains(x))
                                {
                                    sx = -777;
                                    break;
                                }
                            }
                            if (sx == -777)
                            {
                                Lemming[actLEM].Walker = true;
                                Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                Lemming[actLEM].Actualframe = 0;
                                Lemming[actLEM].Miner = false;
                                break;
                            }
                        }
                        if (Colorsobre2[r].R > 0 || Colorsobre2[r].G > 0 || Colorsobre2[r].B > 0)
                        {
                            Frente2++;
                        }
                        if (Colormask2[r].R > 0 || Colormask2[r].G > 0 || Colormask2[r].B > 0)
                        {
                            Colorsobre2[r].PackedValue = 0;
                        }
                    }
                    rectangleFill.X = px;
                    rectangleFill.Y = py;
                    rectangleFill.Width = width2;
                    rectangleFill.Height = top2;
                    Earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                    // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                    amount = 0;
                    for (int yy33 = 0; yy33 < top2; yy33++)
                    {
                        int yypos555 = (yy33 + py) * Earth.Width;
                        for (int xx33 = 0; xx33 < width2; xx33++)
                        {
                            C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                            amount++;
                        }
                    }
                    if (sx == -777)
                        continue;
                    Lemming[actLEM].PosX += 12;
                    Lemming[actLEM].PosY++;
                    if (Frente2 == 0)
                    {
                        Lemming[actLEM].Miner = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        continue;
                    }
                }
                else
                {
                    int width2 = 20;
                    int top2 = 20;
                    int px = Lemming[actLEM].PosX - 4;
                    if (px < 0)
                    {
                        px = 0;
                    }
                    int py = Lemming[actLEM].PosY + 14;
                    if (py < 0) // top of the level
                    {
                        py = 0;
                    }
                    if (px < 0) // left of the level
                    {
                        px = 0;
                    }
                    if (px + width2 >= Earth.Width)
                    {
                        width2 = Earth.Width - px;
                    }
                    if (py + top2 >= Earth.Height)
                    {
                        top2 = Earth.Height - py;
                    }
                    LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(Colormask2);
                    //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                    int amount = 0;
                    for (int yy88 = 0; yy88 < top2; yy88++)
                    {
                        int yypos888 = (yy88 + py) * Earth.Width;
                        for (int xx88 = 0; xx88 < width2; xx88++)
                        {
                            Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                            amount++;
                        }
                    }
                    for (int r = 0; r < amount; r++)
                    {
                        if (SteelON)
                        {
                            sx = r % width2;
                            int sy = r / width2;
                            x.X = px + sx;
                            x.Y = py + sy;
                            for (int xz = 0; xz < NumTOTsteel; xz++)
                            {
                                if (Steel[xz].area.Contains(x))
                                {
                                    sx = -777;
                                    break;
                                }
                            }
                            if (sx == -777)
                            {
                                Lemming[actLEM].Walker = true;
                                Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                Lemming[actLEM].Actualframe = 0;
                                Lemming[actLEM].Miner = false;
                                break;
                            }
                        }
                        if (Colorsobre2[amount - 1 - r].R > 0 || Colorsobre2[amount - 1 - r].G > 0 || Colorsobre2[amount - 1 - r].B > 0)
                        {
                            Frente2++;
                        }
                        if (Colormask2[amount - 1 - r].R > 0 || Colormask2[amount - 1 - r].G > 0 || Colormask2[amount - 1 - r].B > 0)
                        {
                            Colorsobre2[r].PackedValue = 0;
                        }
                    }
                    rectangleFill.X = px;
                    rectangleFill.Y = py;
                    rectangleFill.Width = width2;
                    rectangleFill.Height = top2;
                    Earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                    // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                    amount = 0;
                    for (int yy33 = 0; yy33 < top2; yy33++)
                    {
                        int yypos555 = (yy33 + py) * Earth.Width;
                        for (int xx33 = 0; xx33 < width2; xx33++)
                        {
                            C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                            amount++;
                        }
                    }
                    if (sx == -777)
                        continue;
                    Lemming[actLEM].PosX -= 12;
                    Lemming[actLEM].PosY++;
                    if (Frente2 == 0)
                    {
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        continue;
                    }
                }
                Frente2 = 0;  /////// PPPPPPPPIIIIIIIIIICCCCCCCCCCCCCCCCCOOOOOOOOOOOOOOOOOOO  BASHER LOGIC puto33
            }

            if (Lemming[actLEM].Basher && (Lemming[actLEM].Actualframe == 10 || Lemming[actLEM].Actualframe == 37) && Draw2)
            {
                if (ArrowsON) // basher arrows logic areaTrap Intersects
                {
                    bool nobasher = false;
                    arrowLem.X = Lemming[actLEM].PosX;
                    arrowLem.Y = Lemming[actLEM].PosY;
                    arrowLem.Width = 28;
                    arrowLem.Height = 28;
                    for (int wer3 = 0; wer3 < NumTotArrow; wer3++)
                    {
                        if (Arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Right && !Arrow[wer3].right)
                        {
                            nobasher = true;
                            continue;
                        }
                        if (Arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Left && Arrow[wer3].right)
                        {
                            nobasher = true;
                        }
                    }
                    if (nobasher)
                    {
                        if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Ting.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Ting.Play();
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        continue;
                    }
                }
                if (Lemming[actLEM].Right)
                {
                    int width2 = 20;
                    int top2 = 20;
                    int px = Lemming[actLEM].PosX + 14;
                    int py = Lemming[actLEM].PosY + 8;
                    if (py < 0) // top of the level
                    {
                        py = 0;
                    }
                    if (px < 0) // left of the level
                    {
                        px = 0;
                    }
                    if (px + width2 >= Earth.Width)
                    {
                        width2 = Earth.Width - px;
                    }
                    if (py + top2 >= Earth.Height)
                    {
                        top2 = Earth.Height - py;
                    }
                    LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(Colormask2);
                    //////// optimized for hd3000 laptop
                    int amount = 0;
                    for (int yy88 = 0; yy88 < top2; yy88++)
                    {
                        int yypos888 = (yy88 + py) * Earth.Width;
                        for (int xx88 = 0; xx88 < width2; xx88++)
                        {
                            Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                            amount++;
                        }
                    }
                    int xEmpty = 0;
                    int xErase = width2;
                    Frente = 0;
                    sx = 0;
                    for (int valX = 0; valX < width2; valX++)
                    {
                        Frente = 0;
                        for (int valY = 0; valY < top2; valY++)
                        {
                            if (SteelON)
                            {
                                x.X = px + valX;
                                x.Y = py + valY;
                                for (int xz = 0; xz < NumTOTsteel; xz++)
                                {
                                    if (Steel[xz].area.Contains(x))
                                    {
                                        sx = -777;
                                        break;
                                    }
                                }
                                if (sx == -777)
                                {
                                    Lemming[actLEM].Walker = true;
                                    Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                    Lemming[actLEM].Actualframe = 0;
                                    Lemming[actLEM].Basher = false;
                                    break;
                                }
                            }
                            if ((Colormask2[(valY * width2) + valX].R > 0 || Colormask2[(valY * width2) + valX].G > 0 || Colormask2[(valY * width2) + valX].B > 0) &&
                                (Colorsobre2[(valY * width2) + valX].R > 0 || Colorsobre2[(valY * width2) + valX].G > 0 || Colorsobre2[(valY * width2) + valX].B > 0))
                            {
                                Colorsobre2[(valY * width2) + valX].PackedValue = 0;
                                Frente++;
                            }
                        }
                        if (sx == -777)
                            break;
                        if (Frente == 0)
                            xEmpty = valX;
                        if (Frente > 0)
                        {
                            xErase = valX;
                        }
                        if (xEmpty > xErase)
                            break;
                    }
                    xEmpty++;
                    xErase++;
                    rectangleFill.X = px;
                    rectangleFill.Y = py;
                    rectangleFill.Width = width2;
                    rectangleFill.Height = top2;
                    Earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                    // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                    amount = 0;
                    for (int yy33 = 0; yy33 < top2; yy33++)
                    {
                        int yypos555 = (yy33 + py) * Earth.Width;
                        for (int xx33 = 0; xx33 < width2; xx33++)
                        {
                            C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                            amount++;
                        }
                    }
                    if (sx == -777)
                        continue;
                    if (xEmpty < xErase)
                        Lemming[actLEM].PosX += 14;
                    if (xEmpty > xErase || xErase == 21)
                    {
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        continue;
                    }
                }
                else
                {
                    int width2 = 20;
                    int top2 = 20;
                    int px = Lemming[actLEM].PosX - 5;
                    if (px < 0)
                    {
                        px = 0;
                    }
                    int py = Lemming[actLEM].PosY + 8;
                    if (py < 0) // top of the level
                    {
                        py = 0;
                    }
                    if (px < 0) // left of the level
                    {
                        px = 0;
                    }
                    if (px + width2 >= Earth.Width)
                    {
                        width2 = Earth.Width - px;
                    }
                    if (py + top2 >= Earth.Height)
                    {
                        top2 = Earth.Height - py;
                    }
                    int amount = 0;
                    for (int yy88 = 0; yy88 < top2; yy88++)
                    {
                        int yypos888 = (yy88 + py) * Earth.Width;
                        for (int xx88 = 0; xx88 < width2; xx88++)
                        {
                            Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                            amount++;
                        }
                    }
                    int xEmpty = width2;
                    int xErase = 0;
                    Frente = 0;
                    sx = 0;
                    for (int valX = width2 - 1; valX >= 0; valX--)
                    {
                        Frente = 0;
                        for (int valY = 0; valY < top2; valY++)
                        {
                            if (SteelON)
                            {
                                x.X = px + valX;
                                x.Y = py + valY;
                                for (int xz = 0; xz < NumTOTsteel; xz++)
                                {
                                    if (Steel[xz].area.Contains(x))
                                    {
                                        sx = -777;
                                        break;
                                    }
                                }
                                if (sx == -777)
                                {
                                    Lemming[actLEM].Walker = true;
                                    Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                    Lemming[actLEM].Actualframe = 0;
                                    Lemming[actLEM].Basher = false;
                                    break;
                                }
                            }
                            if ((Colormask2[valY * width2 + valX].R > 0 || Colormask2[valY * width2 + valX].G > 0 || Colormask2[valY * width2 + valX].B > 0) &&
                                (Colorsobre2[valY * width2 + valX].R > 0 || Colorsobre2[valY * width2 + valX].G > 0 || Colorsobre2[valY * width2 + valX].B > 0))
                            {
                                Colorsobre2[valY * width2 + valX].PackedValue = 0;
                                Frente++;
                            }
                        }
                        if (sx == -777)
                            break;
                        if (Frente == 0)
                            xEmpty = valX;
                        if (Frente > 0)
                        {
                            xErase = valX;
                        }
                        if (xEmpty < xErase)
                            break;
                    }
                    xEmpty++;
                    xErase++;
                    rectangleFill.X = px;
                    rectangleFill.Y = py;
                    rectangleFill.Width = width2;
                    rectangleFill.Height = top2;
                    Earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                    // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                    amount = 0;
                    for (int yy33 = 0; yy33 < top2; yy33++)
                    {
                        int yypos555 = (yy33 + py) * Earth.Width;
                        for (int xx33 = 0; xx33 < width2; xx33++)
                        {
                            C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                            amount++;
                        }
                    }
                    if (sx == -777)
                        continue;
                    if (xEmpty > xErase)
                        Lemming[actLEM].PosX -= 14;
                    if (xEmpty < xErase || xEmpty == 1) // xerase==20 nothing erases
                    {
                        Lemming[actLEM].Basher = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Actualframe = 0;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        continue;
                    }
                }
                Frente2 = 0;
                ////////////////////////////////////////////////////////////////////// PPPPPPPPPAAAAAAARRRRRRRRRRRRRRRREEEEEEEDDDDDDDDD
            }
            if (Lemming[actLEM].Basher && _below > 3)
            {
                Lemming[actLEM].Basher = false;
                Lemming[actLEM].Walker = true;
                Lemming[actLEM].Actualframe = 0;
                Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                continue;
            }
            if (Lemming[actLEM].Builder && draw_builder) // BUILDER LOGIC HERE chink sound see limits tooo FIX FIX FIX
            {
                if (Lemming[actLEM].Actualframe >= 48 && Lemming[actLEM].Numstairs < 12) // >=33 old with dibuja2
                // i need to cut on frame 33 of 56 because speed problems timings and x & y axis, see later to fix speed making stairs and fix positioning for get real 56 frames
                {
                    Frente = 0;
                    Lemming[actLEM].Actualframe = SizeSprites.builder_frames + 1;  // erase with // no compiling//  to see full frames
                    if (Lemming[actLEM].Right)
                    {
                        if (arriba > 1)
                        {
                            Lemming[actLEM].PosY += 6;
                            Lemming[actLEM].PosX -= 14;
                            Lemming[actLEM].Builder = false;
                            Lemming[actLEM].Walker = true;
                            Lemming[actLEM].Actualframe = 0;
                            Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            Lemming[actLEM].Numstairs = 0;
                            Lemming[actLEM].Right = false;
                            continue;
                        }
                        if (Lemming[actLEM].PosY < -24) //see ok was -24 but sometimes fails the u-turn
                        {
                            Lemming[actLEM].Builder = false;
                            Lemming[actLEM].Walker = true;
                            Lemming[actLEM].Actualframe = 0;
                            Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            Lemming[actLEM].PosY += 3;
                            Lemming[actLEM].PosX -= 6;
                            continue;
                        }
                        for (int y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                        {
                            int posi_real = (Lemming[actLEM].PosY + 24 + y) * Earth.Width + Lemming[actLEM].PosX;
                            for (int xx88 = 14; xx88 <= 28; xx88++)
                            {
                                if (C25[posi_real + xx88].R == 0 && C25[posi_real + xx88].G == 0 && C25[posi_real + xx88].B == 0)
                                {
                                    colorFill.R = Convert.ToByte(255 - (Lemming[actLEM].Numstairs * 5));
                                    colorFill.G = 0;
                                    colorFill.B = Convert.ToByte(255 - (Lemming[actLEM].Numstairs * 10));
                                    colorFill.A = 255;
                                    C25[posi_real + xx88] = colorFill;
                                } //steps color stairs
                                else
                                {
                                    if (xx88 == 19)
                                        Frente++;
                                }
                            }
                        }
                        Lemming[actLEM].Numstairs++;
                        Lemming[actLEM].PosY -= 3;
                        Lemming[actLEM].PosX += 6;
                        if (Lemming[actLEM].Numstairs >= 10)
                        {
                            if (LemmingsNetGame.Instance.Sfx.Chink.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.Chink.Stop();
                            }
                            LemmingsNetGame.Instance.Sfx.Chink.Play();
                        }
                        int amount = 0;
                        for (int ykk = 27; ykk < 31; ykk++)
                        {
                            int posi_real = (Lemming[actLEM].PosY + ykk) * Earth.Width + Lemming[actLEM].PosX;
                            for (int xkk = 0; xkk < 28; xkk++)
                            {
                                Colormask22[amount] = C25[posi_real + xkk];
                                amount++;
                            }
                        }
                        rectangleFill.X = Lemming[actLEM].PosX;
                        rectangleFill.Y = Lemming[actLEM].PosY + 27;
                        rectangleFill.Width = 28;
                        rectangleFill.Height = 4;
                        Earth.SetData(0, rectangleFill, Colormask22, 0, 28 * 4);
                        if (Frente == 3)
                        {
                            Lemming[actLEM].Builder = false;
                            Lemming[actLEM].Walker = true;
                            Lemming[actLEM].Actualframe = 0;
                            Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            Lemming[actLEM].Numstairs = 0;
                            Lemming[actLEM].PosX -= 7;
                            Lemming[actLEM].Right = false;
                        }
                        continue;
                    }
                    else
                    {
                        if (arriba > 1)
                        {
                            Lemming[actLEM].PosY += 6;
                            Lemming[actLEM].PosX += 15;
                            Lemming[actLEM].Builder = false;
                            Lemming[actLEM].Walker = true;
                            Lemming[actLEM].Actualframe = 0;
                            Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            Lemming[actLEM].Numstairs = 0;
                            Lemming[actLEM].Right = true;
                            continue;

                        }
                        if (Lemming[actLEM].PosY < -24) //see ok was -24
                        {
                            Lemming[actLEM].Builder = false;
                            Lemming[actLEM].Walker = true;
                            Lemming[actLEM].Actualframe = 0;
                            Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            Lemming[actLEM].PosY += 3;
                            Lemming[actLEM].PosX += 6;
                            continue;
                        }
                        for (int y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                        {
                            int posi_real = (Lemming[actLEM].PosY + 24 + y) * Earth.Width + Lemming[actLEM].PosX;
                            for (int xx88 = 0; xx88 <= 14; xx88++)
                            {
                                if (C25[posi_real + xx88].R == 0 && C25[posi_real + xx88].G == 0 && C25[posi_real + xx88].B == 0)
                                {
                                    colorFill.R = Convert.ToByte(255 - (Lemming[actLEM].Numstairs * 5));
                                    colorFill.G = 0;
                                    colorFill.B = Convert.ToByte(255 - (Lemming[actLEM].Numstairs * 10));
                                    colorFill.A = 255;
                                    C25[posi_real + xx88] = colorFill;
                                }//magenta stairs
                                else
                                {
                                    if (xx88 == 9)
                                        Frente++;
                                }
                            }
                        }
                        Lemming[actLEM].Numstairs++;
                        Lemming[actLEM].PosY -= 3;
                        Lemming[actLEM].PosX -= 6;
                        if (Lemming[actLEM].Numstairs >= 10)
                        {
                            if (LemmingsNetGame.Instance.Sfx.Chink.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.Chink.Stop();
                            }
                            LemmingsNetGame.Instance.Sfx.Chink.Play();
                        }
                        //earth.SetData<Color>(c25); //OPTIMIZED BUILDER SETDATA
                        int amount = 0;
                        int px = Lemming[actLEM].PosX;
                        if (px < 0)
                            px = 0;
                        for (int ykk = 27; ykk < 31; ykk++)
                        {
                            int posi_real = (Lemming[actLEM].PosY + ykk) * Earth.Width + px;
                            for (int xkk = 0; xkk < 28; xkk++)
                            {
                                Colormask22[amount] = C25[posi_real + xkk];
                                amount++;
                            }
                        }
                        rectangleFill.X = px;
                        rectangleFill.Y = Lemming[actLEM].PosY + 27;
                        rectangleFill.Width = 28;
                        rectangleFill.Height = 4;
                        Earth.SetData(0, rectangleFill, Colormask22, 0, 28 * 4);
                        if (Frente == 3)
                        {
                            Lemming[actLEM].Builder = false;
                            Lemming[actLEM].Walker = true;
                            Lemming[actLEM].Actualframe = 0;
                            Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            Lemming[actLEM].Numstairs = 0;
                            Lemming[actLEM].PosX += 8;
                            Lemming[actLEM].Right = true;
                        }
                        continue;
                    }
                }
                if (Lemming[actLEM].Numstairs >= 12 &&
                    !Lemming[actLEM].Bridge)
                {
                    Lemming[actLEM].Builder = false;
                    Lemming[actLEM].Bridge = true;
                    Lemming[actLEM].Pixelscaida = 0;
                    if (Lemming[actLEM].Right)
                    {
                        Lemming[actLEM].PosX -= 6;
                    }
                    else
                    {
                        Lemming[actLEM].PosX += 6;
                    }
                    Lemming[actLEM].Actualframe = 0;
                    Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                }
            }
            if (Lemming[actLEM].Bridge && Lemming[actLEM].Actualframe == 7 && Lemming[actLEM].Bridge)
            {
                Lemming[actLEM].Bridge = false;
                Lemming[actLEM].Walker = true;
                Lemming[actLEM].Actualframe = 0;
                Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                Lemming[actLEM].Numstairs = 0;
                continue;
            }
            if (Lemming[actLEM].Digger) ///// DIGGER DIGGER WARNING WARNING
            {
                if (_below == 0 || _below == 1) // 5 ok que no se aceleren a digger si hay mas de 2 juntos antes era <9 los pixeles debajo de sus pies
                {
                    int abajo2 = 0;
                    int pixx2 = Lemming[actLEM].PosX + 14;
                    for (int xx88 = 0; xx88 <= 4; xx88++)
                    {
                        int pos_real2 = Lemming[actLEM].PosY + xx88 + 28;  ///////////// pixel por debajo.............
                        if (pos_real2 == Earth.Height)
                        {
                            abajo2 = 9;
                            break;
                        }
                        if (pos_real2 < 0)
                            pos_real2 = 0;
                        if (pos_real2 > Earth.Height)
                        {
                            pos_real2 = Earth.Height;
                        }
                        if (C25[(pos_real2 * Earth.Width) + pixx2].R > 0 || C25[(pos_real2 * Earth.Width) + pixx2].G > 0 || C25[(pos_real2 * Earth.Width) + pixx2].B > 0)
                        {
                            abajo2++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if ((Lemming[actLEM].Actualframe == 11 || Lemming[actLEM].Actualframe == 26) && draw_walker)
                    {
                        sx = 0;
                        for (int y = 9; y <= 18; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                        {
                            int posi_real = (Lemming[actLEM].PosY + 14 + y) * Earth.Width + Lemming[actLEM].PosX;
                            if (Lemming[actLEM].PosY + 14 + y > Earth.Height)
                            {
                                break;
                            } // cortar si esta en el limite por debajo 512=earth.height
                            for (int xx88 = 4; xx88 <= 24; xx88++)
                            {
                                if (SteelON)
                                {
                                    x.X = Lemming[actLEM].PosX + xx88;
                                    x.Y = Lemming[actLEM].PosY + 14 + y;
                                    for (int xz = 0; xz < NumTOTsteel; xz++)
                                    {
                                        if (Steel[xz].area.Contains(x))
                                        {
                                            sx = -777; break;
                                        }
                                    }
                                    if (sx == -777)
                                    {
                                        Lemming[actLEM].Walker = true;
                                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                        Lemming[actLEM].Actualframe = 0;
                                        Lemming[actLEM].Digger = false;
                                        break;
                                    }
                                }
                                if (sx == -777)
                                    break;
                                colorFill.R = 0;
                                colorFill.G = 0;
                                colorFill.B = 0;
                                colorFill.A = 0;
                                C25[posi_real + xx88] = colorFill; // Color.TransparentBlack is the same i think. 0,0,0,0.
                            }
                        }
                        //earth.SetData<Color>(c25); //OPTIMIZED digger SETDATA
                        int amount = 0;
                        for (int ykk = 9; ykk <= 18; ykk++)
                        {
                            int posi_real = (Lemming[actLEM].PosY + 14 + ykk) * Earth.Width + Lemming[actLEM].PosX;
                            for (int xkk = 0; xkk < 28; xkk++)
                            {
                                Colormask22[amount] = C25[posi_real + xkk];
                                amount++;
                            }
                        }
                        rectangleFill.X = Lemming[actLEM].PosX;
                        rectangleFill.Y = Lemming[actLEM].PosY + 23;
                        rectangleFill.Width = 28;
                        rectangleFill.Height = 10;
                        Earth.SetData(0, rectangleFill, Colormask22, 0, 28 * 10);
                        if (sx == -777)
                            continue;
                        Lemming[actLEM].PosY += abajo2;
                        continue;
                    }
                }
                else
                {
                    if (Lemming[actLEM].PosY + 28 >= Earth.Height) // erase draws bottom when die and exit level height 21x10
                    {
                        for (int ykk = 0; ykk < 210; ykk++)
                        {
                            Colormask22[ykk].PackedValue = 0;
                        }
                        rectangleFill.Y = 502;
                        rectangleFill.X = Lemming[actLEM].PosX + 4;
                        rectangleFill.Width = 21;
                        rectangleFill.Height = 10;
                        Earth.SetData(0, rectangleFill, Colormask22, 0, 210);
                    }
                    Lemming[actLEM].Basher = false;
                    Lemming[actLEM].Builder = false;
                    Lemming[actLEM].Miner = false;
                    Lemming[actLEM].Climbing = false;
                    Lemming[actLEM].Digger = false;
                    Lemming[actLEM].Fall = true;
                    Lemming[actLEM].Walker = false;
                    Lemming[actLEM].Pixelscaida = 0;
                    Lemming[actLEM].Actualframe = 0;
                    Lemming[actLEM].Numframes = SizeSprites.faller_frames;
                    continue; //break o continue DUNNO I DON'T KNOW WHICH IS BETTER
                }

            }
            if (Lemming[actLEM].Climbing)
            {
                if (Lemming[actLEM].PosY <= -28) // top of level -- out of limits 28 size sprite lemming 28x28
                {
                    Lemming[actLEM].Climbing = false;
                    Lemming[actLEM].Fall = true;
                    Lemming[actLEM].Walker = false;
                    Lemming[actLEM].Pixelscaida = 0;
                    Lemming[actLEM].Numframes = SizeSprites.faller_frames;
                    Lemming[actLEM].Actualframe = 0;
                    Lemming[actLEM].Builder = false;
                    Lemming[actLEM].Right = !Lemming[actLEM].Right;
                    continue;
                }
                if (Lemming[actLEM].Right)
                {
                    int pos_real2 = Lemming[actLEM].PosY + 27;
                    if (C25[(pos_real2 * Earth.Width) + pixx - 2].R > 0 || C25[(pos_real2 * Earth.Width) + pixx - 2].G > 0 || C25[(pos_real2 * Earth.Width) + pixx - 2].B > 0)
                    {
                        Lemming[actLEM].Right = false;
                        Lemming[actLEM].PosX -= 2;   // 1 o 2 LOOK
                        Lemming[actLEM].Climbing = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        Lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
                else
                {
                    int pos_real2 = Lemming[actLEM].PosY + 27;
                    if (C25[(pos_real2 * Earth.Width) + pixx + 2].R > 0 || C25[(pos_real2 * Earth.Width) + pixx + 2].G > 0 || C25[(pos_real2 * Earth.Width) + pixx + 2].B > 0)
                    {
                        Lemming[actLEM].Right = true;
                        Lemming[actLEM].PosX += 2; // 1 o 2 LOOK
                        Lemming[actLEM].Climbing = false;
                        Lemming[actLEM].Walker = true;
                        Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        Lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
                if (arriba > 0 && Dibuja)
                {
                    Lemming[actLEM].PosY--;
                }
                if (arriba == 0)
                {
                    if (Lemming[actLEM].Right)
                    {
                        Lemming[actLEM].PosX++;
                    }
                    else
                    {
                        Lemming[actLEM].PosX--;
                    }
                    Lemming[actLEM].Climbing = false;
                    Lemming[actLEM].Walker = true;
                    Lemming[actLEM].Numframes = SizeSprites.walker_frames;
                    Lemming[actLEM].Actualframe = 0;
                    continue;
                }
            }
            if (Lemming[actLEM].Walker)
            {
                if (_below < 3 && Lemming[actLEM].Right)
                {
                    Lemming[actLEM].PosX++;
                    if (arriba < 16)
                    {
                        Lemming[actLEM].PosY -= arriba;
                    }
                }  //// <6 o <8 falla cava
                if (_below < 3 && Lemming[actLEM].Left)
                {
                    Lemming[actLEM].PosX--;
                    if (arriba < 16)
                    {
                        Lemming[actLEM].PosY -= arriba;
                    }
                }
                if (arriba >= 16)
                {
                    if (!Lemming[actLEM].Climber)
                    {
                        if (Lemming[actLEM].Right && arriba >= 16)
                        {
                            Lemming[actLEM].Right = false;
                            Lemming[actLEM].PosX -= 2;  // 1 o 2 LOOK
                        }
                        else
                        {
                            Lemming[actLEM].Right = true;
                            Lemming[actLEM].PosX += 2;  // 1 o 2 LOOK
                        }
                    }
                    else
                    {
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Climbing = true;
                        Lemming[actLEM].Numframes = SizeSprites.climber_frames;
                        Lemming[actLEM].Pixelscaida = 0;
                        Lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
            }
            if (Lemming[actLEM].Explode && Lemming[actLEM].Actualframe >= 47)
            {
                ////////////////////////////////////////////////////////////////////////////////////// EXPLODE MASK
                ///////////////// EXPLODING MASK LIMITS -- SIZE OF AREA ERASEABLE
                int ancho66 = 38;
                int alto66 = 53;
                int px = Lemming[actLEM].PosX - 5; //center the big explosion to 28x28 lemming sprite
                int py = Lemming[actLEM].PosY - 2;
                int py2 = 0;
                int px2 = 0;
                if (py < 0) // top of the level
                {
                    py2 = py * -1;
                    alto66 -= py2;
                    py = 0;
                }
                if (px < 0) // left of the level
                {
                    px2 = px * -1;
                    ancho66 -= px2;
                    px = 0;
                }
                if (px + ancho66 >= Earth.Width)  // right of the level
                {
                    ancho66 = Earth.Width - px;
                }
                if (py + alto66 >= Earth.Height) //bottom of the level
                {
                    alto66 = Earth.Height - py;
                }
                int amount = ancho66 * alto66;
                rectangleFill.X = px2;
                rectangleFill.Y = py2;
                rectangleFill.Width = ancho66;
                rectangleFill.Height = alto66;
                LemmingsNetGame.Instance.ScreenMainMenu.MainMenuGfx.Mascaraexplosion.GetData(0, rectangleFill, Colormask33, 0, amount);
                amount = 0;
                for (int yy88 = 0; yy88 < alto66; yy88++)
                {
                    int yypos888 = (yy88 + py) * Earth.Width;
                    for (int xx88 = 0; xx88 < ancho66; xx88++)
                    {
                        Colorsobre33[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                for (int r = 0; r < amount; r++)
                {
                    if (SteelON)
                    {
                        sx = r % ancho66;
                        int sy = r / ancho66;
                        x.X = px + sx;
                        x.Y = py + sy;
                        for (int xz = 0; xz < NumTOTsteel; xz++)
                        {
                            if (Steel[xz].area.Contains(x))
                            {
                                sx = -777;
                                break;
                            }
                        }
                        if (sx == -777)
                            continue;
                    }
                    if (Colormask33[r].R > 0 || Colormask33[r].G > 0 || Colormask33[r].B > 0)
                    {
                        Colorsobre33[r].PackedValue = 0;
                    }
                }
                rectangleFill.X = px;
                rectangleFill.Y = py;
                rectangleFill.Width = ancho66;
                rectangleFill.Height = alto66;
                Earth.SetData(0, rectangleFill, Colorsobre33, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < alto66; yy33++)
                {
                    int yypos555 = (yy33 + py) * Earth.Width;
                    for (int xx33 = 0; xx33 < ancho66; xx33++)
                    {
                        C25[yypos555 + px + xx33].PackedValue = Colorsobre33[amount].PackedValue;
                        amount++;
                    }
                }
                Lemming[actLEM].Dead = true;
                numlemnow--;
                Lemming[actLEM].Explode = false;
                Lemming[actLEM].Exploser = false;
                // luto luto sound fix
                if (LemmingsNetGame.Instance.Sfx.Explode.State == SoundState.Playing)
                {
                    LemmingsNetGame.Instance.Sfx.Explode.Stop();
                }
                try
                {
                    LemmingsNetGame.Instance.Sfx.Explode.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
                //explosions addons emitter - particles logic add
                int xExp = Lemming[actLEM].PosX + 14;
                int yExp = Lemming[actLEM].PosY + 14;
                LemmingsNetGame.Instance.Explosion[actItem, 0].MaxCounter = 0;
                LemmingsNetGame.Instance.Explosion[actItem, 0].Counter = 0;
                for (Iexplo = 0; Iexplo < PARTICLE_NUM; Iexplo++)
                {
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].MaxCounter = 0;
                    byte colorr = (byte)MyGame.Rnd.Next(255);
                    byte colorg = (byte)MyGame.Rnd.Next(255);
                    byte colorb = (byte)MyGame.Rnd.Next(255);
                    colorFill.R = colorr;
                    colorFill.G = colorg;
                    colorFill.B = colorb;
                    colorFill.A = 255;
                    int LifeCount = LIFE_COUNTER + (int)(MyGame.Rnd.NextDouble() * 2 * LIFE_VARIANCE) - LIFE_VARIANCE;
                    if (LifeCount > LemmingsNetGame.Instance.Explosion[actItem, 0].MaxCounter)
                        LemmingsNetGame.Instance.Explosion[0, 0].MaxCounter = LifeCount;
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].dx = (MyGame.Rnd.NextDouble() * (SizeSprites.MAX_DX - SizeSprites.MIN_DX) + SizeSprites.MIN_DX);
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].dy = (MyGame.Rnd.NextDouble() * (SizeSprites.MAX_DY - SizeSprites.MIN_DY) + SizeSprites.MIN_DY);
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].x = xExp;
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].y = yExp;
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].Color = colorFill;
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].LifeCtr = LifeCount;
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].Rotation = (float)MyGame.Rnd.NextDouble();
                    LemmingsNetGame.Instance.Explosion[actItem, Iexplo].Size = (float)(MyGame.Rnd.NextDouble() / 2);
                }
                Exploding = true;
                actItem++;
                if (actItem > MyGame.totalExplosions - 1)
                    actItem = MyGame.totalExplosions - 1;
                continue;
            }
            if (!Lemming[actLEM].Falling && Lemming[actLEM].Active)
            {
                if (_below >= 3)
                {
                    Lemming[actLEM].PosY += 3;
                    Lemming[actLEM].Pixelscaida += 3;
                }
                else
                {
                    Lemming[actLEM].PosY += _below;
                    Lemming[actLEM].Pixelscaida += _below;
                } // fall 3 MAX---MAX 3 FALL PIXELS
            }
            else
            {
                if (!Lemming[actLEM].Drown && Dibuja)
                {
                    if (_below >= 3)
                    {
                        Lemming[actLEM].PosY += 3;
                    }
                    else
                    {
                        Lemming[actLEM].PosY += _below;
                    }
                }
            }
            if (Lemming[actLEM].PosY < -27) // walker top of the screen
            {
                if (Lemming[actLEM].Right)
                {
                    Lemming[actLEM].Right = false;
                    Lemming[actLEM].PosX -= 3;
                    Lemming[actLEM].PosY++;
                }
                else
                {
                    Lemming[actLEM].Right = true;
                    Lemming[actLEM].PosX += 3;
                    Lemming[actLEM].PosY++;
                }
            }
            if (Lemming[actLEM].PosX < -16)// limits of the screen from LEFT
            {
                Lemming[actLEM].Active = false;
                Lemming[actLEM].Dead = true;
                numlemnow--;
                Lemming[actLEM].Explode = false;
                Lemming[actLEM].Exploser = false;
                if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                {
                    LemmingsNetGame.Instance.Sfx.Die.Stop();
                }
                try
                {
                    LemmingsNetGame.Instance.Sfx.Die.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
            }
            if (Lemming[actLEM].PosX + 14 > Earth.Width)// limits of the screen from RIGHT
            {
                Lemming[actLEM].Active = false;
                Lemming[actLEM].Dead = true;
                numlemnow--;
                Lemming[actLEM].Explode = false;
                Lemming[actLEM].Exploser = false;
                if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                {
                    LemmingsNetGame.Instance.Sfx.Die.Stop();
                }
                try
                {
                    LemmingsNetGame.Instance.Sfx.Die.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
            }

        }
    }

    internal void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null, null);
        graphics.Clear(Color.Black);  //BACKGROUND COLOR darkslategray,cornblue,dimgray,black,gray,lighslategray
                                      //draws back image for all the level
        if (LemmingsNetGame.Instance.ParticleTab != null)
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = 10;
            rectangleFill.Height = 10;
            colorFill.R = 255;
            colorFill.G = 255;
            colorFill.B = 255;
            colorFill.A = 150;
            for (int varParticle = 0; varParticle < MyGame.NumParticles; varParticle++)
            {
                spriteBatch.Draw(LemmingsNetGame.Instance.ParticleTab[varParticle].Sprite, LemmingsNetGame.Instance.ParticleTab[varParticle].Pos, rectangleFill, colorFill, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.50001f);
            }
        }
        bool rayLigths = true;
        // logic of background stars moving from -50 to 50
        actWaves333 = 50 * Math.Sin(actWaves / 60);  // 50 height of the wave  // 60 length of it
        actWaves444 = -70 * Math.Sin(actWaves / -80); // 10,100 -70,100
        if (LemmingsNetGame.Instance.CurrentLevelNumber != 159)
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = MyGame.GameResolution.X;
            rectangleFill.Height = (int)(MyGame.GameResolution.Y * 0.732);
            colorFill.R = 150;
            colorFill.G = 150;
            colorFill.B = 150;
            colorFill.A = 160;
            spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Logo_fondo, rectangleFill, rectangleFill, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
        }
        else
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = MyGame.GameResolution.X;
            rectangleFill.Height = (int)(MyGame.GameResolution.Y * 0.732);
            colorFill.R = 255;
            colorFill.G = 255;
            colorFill.B = 255;
            colorFill.A = 250;
            rectangleFill2.X = 0 + z1;
            rectangleFill2.Y = 0 - (int)actWaves333;
            rectangleFill2.Width = MyGame.GameResolution.X;
            rectangleFill2.Height = MyGame.GameResolution.Y - 188;
            spriteBatch.Draw(LemmingsNetGame.Instance.InGameMenuGfx.Logo666, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.8091f);
            Texture2D logo555 = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/ice outttt");
            rectangleFill2.X = 0 + (int)actWaves444;
            rectangleFill2.Y = 0 + (int)actWaves444;
            rectangleFill2.Width = MyGame.GameResolution.X;
            rectangleFill2.Height = MyGame.GameResolution.Y - 188;
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
                if (LemmingsNetGame.Instance.DebugOsd.debug)
                {
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, new Rectangle(Trap[r].areaTrap.Left - ScrollX, Trap[r].areaTrap.Top - ScrollY, Trap[r].areaTrap.Width, Trap[r].areaTrap.Height),
                        null, new Color(255, 255, 255, 140), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
            }
        }
        switch (LemmingsNetGame.Instance.CurrentLevelNumber)  // effect draws water cascade,stars,etc...
        {
            case 1:
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Agua2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                rayLigths = false;
                break;
            case 4:
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Agua2, new Rectangle(1530 - ScrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Agua2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.803f); //0.802f  
                rayLigths = false;
                break;
            case 5:
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Agua2, new Rectangle(760 - ScrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                break;
            case 6:
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Agua2, new Rectangle(2000 - ScrollX, -80, 260, 680), new Rectangle(0 + z3 * 192, 0, 192, 192),
                    new Color(255, 50, 80, 170), 0f, Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f                            
                break;
            default:
                break;
        }

        if (LemmingsNetGame.Instance.CurrentLevelNumber != 159) //nubes clouds moving in background
        {
            if (rayLigths)
            {
                spriteBatch.Draw(MyTexture, new Vector2(MyGame.GameResolution.X / 2, (MyGame.GameResolution.Y - 188) / 2), new Rectangle(0, 0, MyTexture.Width, MyTexture.Height), new Color(255, 255, 255, 10 + Contador * 2),
                    0.4f + Contador2 * 0.001f, new Vector2(MyTexture.Width / 2, MyTexture.Height / 2), 3f, SpriteEffects.FlipHorizontally, 0.805f); // okokok
            }
            // rayligts effect
            spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Nubes_2, new Rectangle(0, 50 - (int)actWaves444, MyGame.GameResolution.X, LemmingsNetGame.Instance.Sprites.Nubes_2.Height), new Rectangle(z1, 0, MyGame.GameResolution.X, LemmingsNetGame.Instance.Sprites.Nubes_2.Height),
                new Color(255, 255, 255, 110), 0f, Vector2.Zero, SpriteEffects.None, 0.804f);

            spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Nubes, new Rectangle(0, 220, MyGame.GameResolution.X, LemmingsNetGame.Instance.Sprites.Nubes.Height), new Rectangle(z2, 0, MyGame.GameResolution.X, LemmingsNetGame.Instance.Sprites.Nubes.Height), new Color(255, 255, 255, 110), 0f,
                Vector2.Zero, SpriteEffects.None, 0.803f);
        }
        spriteBatch.Draw(Earth, new Vector2(0, 0), new Rectangle(ScrollX, ScrollY, MyGame.GameResolution.X, MyGame.GameResolution.Y - 188), //512 size of window draw
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
                if (ExitBad && LemmingsNetGame.Instance.Sfx.OhNo.State != SoundState.Playing)
                    LemmingsNetGame.Instance.Sfx.OhNo.Play();
                else if (!ExitBad && LemmingsNetGame.Instance.Music.WinMusic.State != SoundState.Playing)
                    LemmingsNetGame.Instance.Music.WinMusic.Play();
            }
            _endSongPlayed = true;
            colorFill.R = 0; //color.black for this change to see differents options
            colorFill.G = 0;
            colorFill.B = 0;
            colorFill.A = 150;
            spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, new Rectangle(45, 32, 1005, 600), null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.001f);
            spriteBatch.Draw(LemmingsNetGame.Instance.ScreenMainMenu.MainMenuGfx.mainMenuSign2, new Rectangle(-200, -120, 1500, 900), null,
               Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.00005f);
            int percent = (100 * numSaved) / LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose].TotalLemmings;
            LemmingsNetGame.Instance.Fonts.TextLem("All lemmings accounted for:", new Vector2(150, 100), Color.Cyan, 1.5f, 0.0000000001f, spriteBatch);
            LemmingsNetGame.Instance.Fonts.TextLem("You rescued " + string.Format("{0}", percent) + "%",
                 new Vector2(270, 160), Color.Violet, 1.5f, 0.0000000001f, spriteBatch);
            percent = (100 * Lemsneeded) / LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose].TotalLemmings;
            LemmingsNetGame.Instance.Fonts.TextLem("You needed " + string.Format("{0}", percent) + "%",
                 new Vector2(300, 220), Color.DodgerBlue, 1.5f, 0.0000000001f, spriteBatch);
            LemmingsNetGame.Instance.Fonts.TextLem("Press <ESC> or <Left Mouse Button>", new Vector2(70, 400), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            if (ExitBad)
                LemmingsNetGame.Instance.Fonts.TextLem("to retry level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            else if (numSaved >= Lemsneeded)
            {
                LemmingsNetGame.Instance.Fonts.TextLem("to next level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            }
            else
            {
                LemmingsNetGame.Instance.Fonts.TextLem("to continue...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
            }
            LemmingsNetGame.Instance.Fonts.TextLem("Press <Enter> or <Right Mouse Button>", new Vector2(70, 520), Color.Yellow, 1.3f, 0.0000000001f, spriteBatch);
            LemmingsNetGame.Instance.Fonts.TextLem("to Main Menu...", new Vector2(100, 560), Color.Yellow, 1.3f, 0.0000000001f, spriteBatch);
        }
        int xx55 = LemmingsNetGame.Instance.Levels.VarDoor[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor].xWidth;
        int yy55 = LemmingsNetGame.Instance.Levels.VarDoor[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor].yWidth;
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
                        Sprite[ssi].pos.X = MyGame.GameResolution.X;
                    if (Sprite[ssi].pos.X > MyGame.GameResolution.X)
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
        int xx66 = LemmingsNetGame.Instance.Levels.VarExit[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].xWidth;
        int yy66 = LemmingsNetGame.Instance.Levels.VarExit[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].yWidth;
        int xx88 = LemmingsNetGame.Instance.Levels.VarExit[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreX;
        int xx99 = LemmingsNetGame.Instance.Levels.VarExit[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreX2;
        int yy88 = LemmingsNetGame.Instance.Levels.VarExit[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreY;
        int yy99 = LemmingsNetGame.Instance.Levels.VarExit[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreY2;
        frameact = (frameExit * yy66);
        if (Moreexits == null)
        {
            spriteBatch.Draw(salida_ani1_1, new Vector2(output1X - ScrollX - xx88, output1Y - yy88 - ScrollY), new Rectangle(0, frameact, xx66, yy66), Color.White,
                0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            spriteBatch.Draw(salida_ani1, new Vector2(output1X - ScrollX - xx99, output1Y - yy99 - ScrollY), new Rectangle(0, 0, salida_ani1.Width, salida_ani1.Height),
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            if (LemmingsNetGame.Instance.DebugOsd.debug) //exits debug
            {
                exit_rect = new Rectangle(output1X - 5, output1Y - 5, 10, 10);
                spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, new Rectangle(exit_rect.Left - ScrollX, exit_rect.Top - ScrollY, exit_rect.Width, exit_rect.Height), null,
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
                if (LemmingsNetGame.Instance.DebugOsd.debug) //exits debug
                {
                    exit_rect = new Rectangle(output1X - 5, output1Y - 5, 10, 10);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, new Rectangle(exit_rect.Left - ScrollX, exit_rect.Top - ScrollY, exit_rect.Width, exit_rect.Height), null,
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
            }
        }
        // infos various for test only

        for (actLEM = 0; actLEM < NumLemmings; actLEM++) //si lo hace de 100 a cero dibujara los primeros encima y mejorara el aspecto
        {
            if (doorOn)
                break;
            if (Lemming[actLEM].Dead)
                continue;
            if (Lemming[actLEM].Exploser && !Lemming[actLEM].Explode)
            {
                if (Lemming[actLEM].Time == 0)
                    Lemming[actLEM].Time = TotalTime;
                double timez = TotalTime - Lemming[actLEM].Time;
                int crono = (int)(6f - (float)timez);
                LemmingsNetGame.Instance.Fonts.TextLem(string.Format("{0}", crono), new Vector2(Lemming[actLEM].PosX + 3 - ScrollX, Lemming[actLEM].PosY - 10 - ScrollY), Color.White, 0.4f, 0.000000000004f, spriteBatch);
                if (crono <= 0)
                {
                    // luto luto sound monogame 3.2 works ok without catch exception
                    if (LemmingsNetGame.Instance.Sfx.OhNo.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.OhNo.Stop();
                    }
                    try
                    {
                        LemmingsNetGame.Instance.Sfx.OhNo.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                    Lemming[actLEM].Explode = true;
                    Lemming[actLEM].Active = false;
                    Lemming[actLEM].Umbrella = false;
                    Lemming[actLEM].Walker = false;
                    Lemming[actLEM].Digger = false;
                    Lemming[actLEM].Climber = false;
                    Lemming[actLEM].Fall = false;
                    Lemming[actLEM].Falling = false;
                    Lemming[actLEM].Climbing = false;
                    Lemming[actLEM].Exit = false;
                    Lemming[actLEM].Blocker = false;
                    Lemming[actLEM].Builder = false;
                    Lemming[actLEM].Bridge = false;
                    Lemming[actLEM].Basher = false;
                    Lemming[actLEM].Miner = false;
                    Lemming[actLEM].Actualframe = 0;
                    Lemming[actLEM].Numframes = SizeSprites.bomber_frames;
                }
            }
            int framereal55;
            if (Lemming[actLEM].Burned) // scale POSDraw x+0,y+0 at 1.2f x-5,y+0 at 1.35f
            {
                spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Squemado, new Vector2(Lemming[actLEM].PosX - ScrollX - 5, Lemming[actLEM].PosY - ScrollY), new Rectangle(0, Lemming[actLEM].Actualframe * 28, 32, 28),
                (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Lhiss, new Vector2(Lemming[actLEM].PosX - ScrollX, Lemming[actLEM].PosY - 20 - ScrollY), new Rectangle(0, 0, LemmingsNetGame.Instance.Gfx.Lhiss.Width, LemmingsNetGame.Instance.Gfx.Lhiss.Height),
                    Color.White, 0f, Vector2.Zero, (0.5f + (0.01f * Lemming[actLEM].Actualframe)), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Drown) // scale POSDraw x+0,y+10 at 1.2f x-8,y+7 at 1.35f  //puto ahoga
            {
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Drowner, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.water_xpos, Lemming[actLEM].PosY + SizeSprites.water_ypos - ScrollY), new Rectangle(Lemming[actLEM].Actualframe * SizeSprites.water_with, 0, SizeSprites.water_with, SizeSprites.water_height),
                    (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.water_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Walker)
            {
                framereal55 = (Lemming[actLEM].Actualframe * SizeSprites.walker_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Walker, new Vector2((Lemming[actLEM].PosX - ScrollX + SizeSprites.walker_xpos), Lemming[actLEM].PosY - ScrollY + SizeSprites.walker_ypos), new Rectangle(framereal55, 0, SizeSprites.walker_with, SizeSprites.walker_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.walker_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Blocker) // blocker scale POSDraw x-5 y+4 at 1.2f x-7 y+1 at 1.35f  //puto
            {
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.blocker_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Blocker, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.blocker_xpos, Lemming[actLEM].PosY + SizeSprites.blocker_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.blocker_with, SizeSprites.blocker_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.blocker_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                if (LemmingsNetGame.Instance.DebugOsd.debug)
                {
                    bloqueo = new Rectangle(Lemming[actLEM].PosX, Lemming[actLEM].PosY, 28, 28);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, new Rectangle(bloqueo.Left - ScrollX, bloqueo.Top - ScrollY, bloqueo.Width, bloqueo.Height), null,
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
            }
            if (Lemming[actLEM].Bridge) // scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f
            {
                framesale = (Lemming[actLEM].Actualframe * 26);
                spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Puente_nomas, new Vector2(Lemming[actLEM].PosX - ScrollX - 7, Lemming[actLEM].PosY - 7 - ScrollY), new Rectangle(0, framesale, 32, 26), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, (Lemming[actLEM].Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Builder)  //scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f  builder builder draws
            {
                if (Lemming[actLEM].Numstairs >= 10) // chink draws
                {
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Chink, new Vector2(Lemming[actLEM].PosX - ScrollX - 10, Lemming[actLEM].PosY - 30 - ScrollY), new Rectangle(0, 0, LemmingsNetGame.Instance.Sprites.Chink.Width, LemmingsNetGame.Instance.Sprites.Chink.Height),
                        Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * Lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                }
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.builder_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Puente, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.builder_xpos, Lemming[actLEM].PosY + SizeSprites.builder_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.builder_with, SizeSprites.builder_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.builder_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Miner)  //scale POSDraw x-5,y-2 at 1.2f x-9,y-7 at 1.35f pico pico miner miner
            {
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.pico_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Pico, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.pico_xpos + (Lemming[actLEM].Right ? 0 : 10), Lemming[actLEM].PosY + SizeSprites.pico_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.pico_with, SizeSprites.pico_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.pico_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Basher) //puto
            {           // scale basher RIGHT POSDRAW x-10,y+4 at 1.2f x-15,y+1 at 1.35f
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.basher_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Pared, new Vector2(Lemming[actLEM].PosX - ScrollX + (Lemming[actLEM].Right ? SizeSprites.basher_xpos : SizeSprites.basher_xposleft), Lemming[actLEM].PosY + SizeSprites.basher_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.basher_with, SizeSprites.basher_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.basher_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Explode) // explotando explotando bomber bomber
            {
                // bomber scale POSDraw x-5,y+4 at 1.2f x-9,y+2 at 1.35f
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.bomber_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Exploder, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.bomber_xpos, Lemming[actLEM].PosY + SizeSprites.bomber_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.bomber_with, SizeSprites.bomber_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.bomber_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Lohno, new Vector2(Lemming[actLEM].PosX - ScrollX - 20, Lemming[actLEM].PosY - 25 - ScrollY), new Rectangle(0, 0, LemmingsNetGame.Instance.Sprites.Lohno.Width, LemmingsNetGame.Instance.Sprites.Lohno.Height),
                    Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * Lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Breakfloor) // scale POSDraw x-5,y+4 at 1.2f  x-9,y+2 at 1.35f breakfloor breakfloor
            {
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.floor_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Rompesuelo, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.floor_xpos, Lemming[actLEM].PosY + SizeSprites.floor_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.floor_with, SizeSprites.floor_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floor_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                if (Lemming[actLEM].Actualframe == SizeSprites.floor_frames - 1)
                {
                    Lemming[actLEM].Dead = true;
                    numlemnow--;
                    Lemming[actLEM].Explode = false;
                    Lemming[actLEM].Exploser = false;
                }
            }
            if (Lemming[actLEM].Falling) //umbrella paraguas falling with umbrella
            {
                if (!Lemming[actLEM].Framescut && Lemming[actLEM].Actualframe == SizeSprites.floater_frames - 1)
                {
                    Lemming[actLEM].Framescut = true;
                    Lemming[actLEM].Actualframe = 0;
                    Lemming[actLEM].Numframes = SizeSprites.floater_frames - 1 - 4;
                }
                if (!Lemming[actLEM].Framescut)
                    framesale = (Lemming[actLEM].Actualframe * SizeSprites.floater_with);
                else
                    framesale = (Lemming[actLEM].Actualframe + 4) * SizeSprites.floater_with; // scale floater POSDraw x-5,y-4 at 1.2f x-9,y-7 at 1.35f
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Paraguas, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.floater_xpos, Lemming[actLEM].PosY + SizeSprites.floater_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.floater_with, SizeSprites.floater_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floater_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Fall) //fall cae
            {
                framereal55 = (Lemming[actLEM].Actualframe * SizeSprites.faller_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Falling, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.faller_xpos, Lemming[actLEM].PosY - ScrollY + SizeSprites.faller_ypos), new Rectangle(framereal55, 0, SizeSprites.faller_with, SizeSprites.faller_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.faller_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Exit && !Lemming[actLEM].Dead) //sale sale exit exit out out
            {
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.sale_with); // exit scale POSDraw  x-1,y+1 at 1.2f x-3,y-1 at 1.35f
                spriteBatch.Draw(sale, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.sale_xpos, Lemming[actLEM].PosY + SizeSprites.sale_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.sale_with, SizeSprites.sale_height), Color.White, 0f, Vector2.Zero, SizeSprites.sale_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
            if (Lemming[actLEM].Digger)
            {
                framereal55 = (Lemming[actLEM].Actualframe * SizeSprites.digger_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Digger, new Vector2(Lemming[actLEM].PosX - ScrollX + SizeSprites.digger_xpos, Lemming[actLEM].PosY + 6 - ScrollY + SizeSprites.digger_ypos), new Rectangle(framereal55, 0, SizeSprites.digger_with, SizeSprites.digger_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.digger_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
            }

            if (Lemming[actLEM].Climbing) // scale POSDraw x-5,y+6 at 1.2f x-8.y+3 at 1.35f  //puto33
            {
                framesale = (Lemming[actLEM].Actualframe * SizeSprites.climber_with);
                spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Climber, new Vector2(Lemming[actLEM].PosX - ScrollX + (Lemming[actLEM].Right ? SizeSprites.climber_xpos : SizeSprites.climber_xposleft), Lemming[actLEM].PosY + SizeSprites.climber_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.climber_with, SizeSprites.climber_height), (Lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.climber_size, (Lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
            }
        }
        if (Fade)
        {
            rest++;
            int rest2 = rest * 7;
            if (rest2 < 70)
                rest2 = 0;
            LemmingsNetGame.Instance.Gfx.DrawLine(spriteBatch, new Vector2(0, 0), new Vector2(MyGame.GameResolution.X, 0), new Color(0, 0, 0, 255 - rest2), MyGame.GameResolution.Y, 0f);
            if (Frame > 19)
            {
                Fade = false;
                rest = 0;
                TotalTime = 0;
                if (LemmingsNetGame.Instance.Sfx.Letsgo.State == SoundState.Stopped && !initON)
                {
                    LemmingsNetGame.Instance.Sfx.Letsgo.Play();
                    initON = true;
                }
            }

        }
        if (Exploding) // draws explosions particles explosion_particle
        {
            for (int Qexplo = 0; Qexplo < actItem; Qexplo++)
            {
                for (Iexplo = 0; Iexplo < PARTICLE_NUM; Iexplo++)
                {
                    if (LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr < 0)
                        continue;

                    vectorFill.X = (float)LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].x - ScrollX;
                    vectorFill.Y = (float)LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].y - ScrollY;
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Explosion_particle, vectorFill, new Rectangle(0, 0, LemmingsNetGame.Instance.Gfx.Explosion_particle.Width, LemmingsNetGame.Instance.Gfx.Explosion_particle.Height), LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].Color,
                        LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].Rotation, Vector2.Zero, LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].Size, SpriteEffects.None, 0.300f);
                    LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].Rotation += 0.03f;
                    LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].Size += 0.01f;
                }
            }

        }
        if (mouseOnLem)
        {
            LemSkill = "";
        }

        vectorFill.X = 650;
        vectorFill.Y = 518;
        LemmingsNetGame.Instance.Fonts.TextLem("Home:" + string.Format("{0}", numSaved) + "/" + string.Format("{0}", Lemsneeded), vectorFill, Color.Cyan, 1f, 0.1f, spriteBatch);
        vectorFill.X = 320;
        vectorFill.Y = 518;
        LemmingsNetGame.Instance.Fonts.TextLem("Out:" + string.Format("{0}", NumLemmings) + "/" + string.Format("{0}", Numlems), vectorFill, Color.Magenta, 1f, 0.1f, spriteBatch);
        vectorFill.X = 530;
        vectorFill.Y = 518;
        LemmingsNetGame.Instance.Fonts.TextLem("In:" + string.Format("{0}", numlemnow), vectorFill, Color.AliceBlue, 1f, 0.1f, spriteBatch);

        _inGameMenu.Draw(spriteBatch);

        spriteBatch.Draw((mouseOnLem ? LemmingsNetGame.Instance.MouseManager.MouseOverLemmings : LemmingsNetGame.Instance.MouseManager.MouseCross), new Vector2(Input.CurrentMouseState.X, Input.CurrentMouseState.Y), new Rectangle(0, 0, 34, 34), Color.White, 0f, Vector2.Zero,
            1f, SpriteEffects.None, 0f);
        spriteBatch.End();
    }

    internal void Update(GameTime gameTime)
    {
        MillisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
        if (Exploding && dibuja3 && !MyGame.Paused)  //logic explosions particles
        {
            int _totalExploding = actItem;
            for (int Qexplo = 0; Qexplo < actItem; Qexplo++)
            {
                int TopY = MyGame.GameResolution.Y;
                if (Earth != null)
                    TopY = Earth.Height - 2;
                int NumberAlive = 0;
                for (Iexplo = 0; Iexplo < PARTICLE_NUM; Iexplo++)
                {
                    if (LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr == -100)
                        NumberAlive++;
                    if (LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr > 0)
                    {
                        //this change alpha channel from half life and fade out every particle
                        int xx33 = LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr;
                        int yy33 = LemmingsNetGame.Instance.Explosion[Qexplo, 0].Counter;
                        int xx55 = (xx33 + yy33) / 2;
                        if (yy33 > xx55)
                        {
                            yy33 -= xx55;
                            int yy55 = yy33 * 100 / xx55;
                            yy55 *= 2;
                            if (yy55 > 255)
                                yy55 = 255;
                            LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].SetColorA(Convert.ToByte(255 - yy55)); //total alpha - % of death value
                        }
                        //calculate new position
                        LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].x += LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].dx;
                        LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].y += LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].dy + LemmingsNetGame.Instance.Explosion[Qexplo, 0].Counter * GRAVITY;
                        if (LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].y > TopY)
                        {
                            //explosion[qexplo, iexplo].y = topY;  //bottom of drawable sets y to max
                            LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr = -100;  //bottom of drawable area kills particle
                        }
                        // check life counter
                        if (LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr > 0)
                            LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr--;
                        if (LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr == 0)
                            LemmingsNetGame.Instance.Explosion[Qexplo, Iexplo].LifeCtr = -100;

                    }
                }
                LemmingsNetGame.Instance.Explosion[Qexplo, 0].Counter++;
                if (NumberAlive >= PARTICLE_NUM)
                {
                    _totalExploding--;
                }
            }
            if (_totalExploding == 0)  // no more particles[0....?][24] are ON
            {
                Exploding = false;
                actItem = 0;
            }
        }
        if (!LevelEnded && ((AllBlow && numlemnow == 0) || ZvTime < 0 || (NumLemmings == Numlems && numlemnow == 0)))
        {
            if (!MyGame.Paused)
                rest++;  // var to wait until menu appears gives this way 4 seconds plus more
            if (rest > 180)
            {
                Exploding = false;
                actItem = 0;  //see when finish time and are more particles ON
                LevelEnded = true;
                MyGame.Paused = true;
                if (numSaved < Lemsneeded)
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
            else if (numSaved >= Lemsneeded && LevelEnded)
                ExitLevel = true;
            else
            {
                if (!LevelEnded)
                {
                    ExitBad = true;
                    LevelEnded = true;
                    MyGame.Paused = true;
                }
                else
                {
                    MyGame.Paused = false;
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
            numSaved = 0;
        }
        if ((Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed) && LevelEnded)
        {
            if (!ExitBad)
            {
                MyGame.Paused = false;
                LevelEnded = false;
            }
            else
                ExitLevel = true;
            if (numSaved >= Lemsneeded)
                ExitLevel = true;
        }
        if (ExitLevel)
        {
            if (numSaved >= Lemsneeded) //see here if level is finished or not
            {
                LevelEnd[LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose] = true;
                BinaryWriter writer = new(File.Open(MyGame.SaveGameFileName, FileMode.Create));
                for (int i = 0; i < MyGame.NumTotalLevels; i++)
                {
                    //LevelEnd[za] = false; // first time create all the levels vars to false --> not finished
                    writer.Write(LevelEnd[i]);
                }
                writer.Write("(c) 2023 FilRip from CoolBytes");
                writer.Close();
                LemmingsNetGame.Instance.MustReadFile = true;
                LemmingsNetGame.Instance.CurrentLevelNumber++;
                if (LemmingsNetGame.Instance.CurrentLevelNumber >= MyGame.NumTotalLevels - 1)
                    LemmingsNetGame.Instance.CurrentLevelNumber = MyGame.NumTotalLevels - 1;
                LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose = LemmingsNetGame.Instance.CurrentLevelNumber;
                LemmingsNetGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                numSaved = 0;
                numlemnow = 0;
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
                LemmingsNetGame.Instance.ReloadContent();
                return; //next level
            }

            if (ExitBad) //repeat level
            {
                LemmingsNetGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                numSaved = 0;
                numlemnow = 0;
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
                LemmingsNetGame.Instance.ReloadContent();
                return;
            }
            CurrentMusic.Stop();
            LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose = 0;
            LevelEnded = false;
            ExitLevel = false;
            AllBlow = false;
            ZvTime = 0;
            ExitBad = false;
            NumLemmings = 0;
            LemmingsNetGame.Instance.ReloadContent();
            LemmingsNetGame.Instance.BackToMenu();
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
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            MyGame.Paused = !MyGame.Paused;
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
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.CLIMBER;
        }
        else if (NbFloaterRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D4) && Input.CurrentKeyState.IsKeyUp(Keys.D4))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.FLOATER;
        }
        else if (NbExploderRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D5) && Input.CurrentKeyState.IsKeyUp(Keys.D5))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.EXPLODER;
        }
        else if (NbBlockerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D6) && Input.CurrentKeyState.IsKeyUp(Keys.D6))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BLOCKER;
        }
        else if (NbBuilderRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D7) && Input.CurrentKeyState.IsKeyUp(Keys.D7))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BUILDER;
        }
        else if (NbBasherRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D8) && Input.CurrentKeyState.IsKeyUp(Keys.D8))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BASHER;
        }
        else if (NbMinerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D9) && Input.CurrentKeyState.IsKeyUp(Keys.D9))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.MINER;
        }
        else if (NbDiggerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D0) && Input.CurrentKeyState.IsKeyUp(Keys.D0))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.DIGGER;
        }
        Update_level();
    }

    private void Scrolling()
    {
        Point mousepos = Input.CurrentMouseState.Position;
        if (mousepos.X + 20 > MyGame.GameResolution.X &&
            ScrollX + MyGame.GameResolution.X < Earth.Width)
        {
            ScrollX += 5;
        }
        if (ScrollX + MyGame.GameResolution.X > Earth.Width)
        {
            ScrollX = Earth.Width - MyGame.GameResolution.X;
        }
        if (mousepos.X < -10 && ScrollX > 0)
        {
            ScrollX -= 5;
        }
        if (ScrollX < 0)
        {
            ScrollX = 0;
        }
        if (mousepos.Y + 20 > MyGame.GameResolution.Y && ScrollY + 512 < Earth.Height)
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
        if (mousepos.Y > MyGame.GameResolution.Y * (LemmingsNetGame.Instance.Scaled ? 2 : 1))
            mousepos.Y = MyGame.GameResolution.Y * (LemmingsNetGame.Instance.Scaled ? 2 : 1);
        if (mousepos.X < -14)
            mousepos.X = -14;
        if (mousepos.X > MyGame.GameResolution.X * (LemmingsNetGame.Instance.Scaled ? 2 : 1))
            mousepos.X = MyGame.GameResolution.X * (LemmingsNetGame.Instance.Scaled ? 2 : 1);
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
            int xx55 = LemmingsNetGame.Instance.Levels.VarDoor[LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor].numFram - 1;
            frameDoor++;
            if (frameDoor == 1 && LemmingsNetGame.Instance.Sfx.EntryLemmings.State == SoundState.Stopped && !doorWaveOn)
            {
                LemmingsNetGame.Instance.Sfx.EntryLemmings.Play();
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
            numlemnow++;
        }

        for (actLEM2 = 0; actLEM2 < NumLemmings; actLEM2++)
        {
            x.X = Lemming[actLEM2].PosX + 14;
            x.Y = Lemming[actLEM2].PosY + 25;
            if (Lemming[actLEM2].Exit && Lemming[actLEM2].Actualframe == 13) // change frame of yipee sound, old frame was init or 0 now different for frames
            {
                if (LemmingsNetGame.Instance.Sfx.Yippe.State == SoundState.Playing)
                {
                    LemmingsNetGame.Instance.Sfx.Yippe.Stop();
                }
                try
                {
                    LemmingsNetGame.Instance.Sfx.Yippe.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
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
