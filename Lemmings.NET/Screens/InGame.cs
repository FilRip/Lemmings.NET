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
    private double actWaves444 = 0, actWaves333 = 0, actWaves = 0;
    private bool Exploding, dibuja3, LevelEnded, ExitBad, ExitLevel;
    private int numSaved, actItem, Iexplo, cantidad22;
    public int numTOTsteel = 0;
    public int NumLemmings { get; set; } = 0;
    private int sx, rest = 0, numTOTdoors = 1, numACTdoor = 0, numTOTexits = 1, framesale, numTOTplats = 0, Contador2, Contador = 1, actLEM2;
    public float Contadortime2, Contadortime;
    public int ScrollX { get; set; }
    public int ScrollY { get; set; }
    private bool mouseOnLem = false;
    private bool doorOn = true;
    public Lem[] Lemming { get; set; }
    private Rectangle bloqueo;
    private double frameWaves = 0;
    private int walker_frame = 0;
    private int builder_frame = 0;
    private readonly int builder_frame_second = 1;
    public int Frame2 = 0;
    private int Frame3 = 0;
    private readonly int Framesecond = 6;
    private readonly int Framesecond2 = 2;
    private readonly int Framesecond3 = 1;  // frame speed less all go crazy 6->ok framesecond=6 default framesecond2=3 default
    private Varsprites[] Sprite { get; set; }
    public int _nbClimberRemaining = 99, _nbFloaterRemaining = 88, _nbExploderRemaining = 77, _nbBlockerRemaining = 66, _nbBuilderRemaining = 55, _nbBasherRemaining = 44, _nbMinerRemaining = 33, _nbDiggerRemaining = 99;
    public bool AllBlow { get; set; } = false;
    private int door1X, door1Y, actLEM;
    private int output1X, output1Y, ex11;
    private int frameDoor, frameExit; // 0--10   0--6
    private int exitFrame = 999, actualBlow; // frecuency lemmings go in
    private Rectangle exit_rect; // rectangle exit
    private Point x;
    internal double MillisecondsElapsed { get; set; }
    internal Texture2D Earth { get; set; }
    private Color[] C25 { get; set; } = new Color[4096 * 4096]; // Maximun size of a color array used for mask all the level
    public string LemSkill { get; set; } = "";
    private Vartraps[] trap;
    private Varplat[] plats;
    private Varadds[] adds;
    private bool initON = false;
    public bool SteelON = false;
    private bool TrapsON = false, PlatsON = false, ArrowsON = false, AddsON = false;
    private readonly int maxnumberfalling = 210;
    private readonly int useumbrella = 100;
    private int NumTotTraps = 0;
    private int NumTotArrow = 0;
    private int framereal565;
    private float DoorExitDepth = 0.403f;  // default value--bigger than 0.5f is behind the terrain (0.6f level 58 for example)
    internal int r1 = 0, r2 = 0, r3 = 0;
    public int ZvTime { get; set; } = 0;
    private Vector2 vectorFill;
    private Rectangle rectangleFill, rectangleFill2;
    private Color colorFill;
    private bool _endSongPlayed;
    private readonly double GRAVITY = 0.1; //0.1
    private int Lemsneeded = 1;
    public bool fade = true;
    private Vararrows[] arrow;
    public Varsteel[] steel;
    private Varmoredoors[] moreDoors;
    private Varmoreexits[] moreexits;
    private int numlemnow = 0;
    private int z1 = 0;
    private int z2 = 0;
    private int z3 = 0;
    public bool Dibuja { get; set; } = true;
    private bool luzmas = true, luzmas2 = true, draw_walker = false, draw_builder = false;
    public bool Draw2 { get; set; } = true;
    public double TotalTime { get; set; }
    private int alto, _below;
    public int Frame;
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
    private int Frente = 0;
    private int Frente2 = 0;
    private bool doorWaveOn = false;
    private int frameact;
    internal Texture2D MyTexture { get; set; }
    private readonly bool LockMouse;
    private readonly float SizeL = 1.35f; //1.2f was default in the beggining
    private Texture2D salida_ani1, salida_ani1_1, sale;
    private readonly Color[] Colormask33 = new Color[38 * 53]; // explode mask 38*53
    private readonly Color[] Colorsobre33 = new Color[38 * 53];
    private Texture2D puerta_ani;

    internal SoundEffectInstance CurrentMusic { get; set; }

    private readonly InGameMenu _inGameMenu;

    internal readonly bool[] LevelEnd = new bool[MyGame.NumTotalLevels]; //full number of levels to see which are finished or not

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
        fade = true;
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
        moreexits = null;
        moreDoors = null;
        trap = null;
        arrow = null;
        Sprite = null;
        numTOTexits = 1;
        numTOTdoors = 1;
        NumTotTraps = 0;
        NumTotArrow = 0;
        doorWaveOn = false;
        initON = false;
        TrapsON = false;
        PlatsON = false;
        AddsON = false;
        ArrowsON = false;
        SteelON = false;
        numTOTsteel = 0;
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
        _nbClimberRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberClimbers;
        _nbFloaterRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberUmbrellas;
        _nbExploderRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberExploders;
        _nbBlockerRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBlockers;
        _nbBuilderRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBuilders;
        _nbBasherRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBashers;
        _nbMinerRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberMiners;
        _nbDiggerRemaining = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberDiggers;
        if (_nbClimberRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.CLIMBER;
        }
        else if (_nbFloaterRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.FLOATER;
        }
        else if (_nbExploderRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.EXPLODER;
        }
        else if (_nbBlockerRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BLOCKER;
        }
        else if (_nbBuilderRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BUILDER;
        }
        else if (_nbBasherRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BASHER;
        }
        else if (_nbMinerRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.MINER;
        }
        else if (_nbDiggerRemaining > 0)
        {
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.DIGGER;
        }
        _inGameMenu.Init();
        Numlems = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TotalLemmings;
        Lemsneeded = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].NbLemmingsToSave;
        ScrollX = LemmingsNetGame.Instance.Levels.AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].InitPosX;
        ScrollY = 0;
        Lemming = new Lem[Numlems];
        VariablesTraps();
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
            for (int i = 0; i < numTOTplats; i++)
            {
                if (plats[i].frame > plats[i].framesecond)
                {
                    bool goUP = plats[i].up;
                    plats[i].frame = 0;
                    if (goUP)
                        plats[i].actStep++;
                    else
                        plats[i].actStep--;
                    if (goUP)
                        plats[i].areaDraw.Y -= plats[i].step;
                    else
                        plats[i].areaDraw.Y += plats[i].step;
                    if (plats[i].actStep >= plats[i].numSteps - 1)
                        plats[i].up = false;
                    if (plats[i].actStep < 1)
                        plats[i].up = true;
                    int px = plats[i].areaDraw.X - (plats[i].areaDraw.Width / 2);
                    alto = plats[i].step * plats[i].numSteps;
                    int positioYOrig = plats[i].areaDraw.Y + (plats[i].actStep * plats[i].step);
                    bool realLine = false;
                    for (int y55 = 0; y55 < alto; y55++)
                    {
                        for (int x55 = 0; x55 < plats[i].areaDraw.Width; x55++)
                        {
                            if (y55 == (alto - 1) - plats[i].actStep * plats[i].step)
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
                plats[i].frame++;
            }
        }

        if (AddsON && !MyGame.Paused)
        {
            int startposy = adds[0].sprite.Height / adds[0].numFrames; // height of each frame inside the whole sprite
            int framepos = startposy * adds[0].actFrame; // actual y position of the frame
            int ancho = adds[0].sprite.Width;
            int amount = ancho * startposy; // height frame
            rectangleFill.X = 0;
            rectangleFill.Y = framepos;
            rectangleFill.Width = ancho;
            rectangleFill.Height = startposy;
            adds[0].sprite.GetData(0, rectangleFill, Colormask22, 0, amount);
            rectangleFill.X = adds[0].areaDraw.X;
            rectangleFill.Y = adds[0].areaDraw.Y;
            rectangleFill.Width = ancho;
            rectangleFill.Height = startposy;
            Earth.SetData(0, rectangleFill, Colormask22, 0, amount);
            int py = adds[0].areaDraw.Y;
            int px = adds[0].areaDraw.X;
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
            if (adds[0].frame > adds[0].framesecond)
            {
                adds[0].frame = 0;
                adds[0].actFrame++;
                if (adds[0].actFrame >= adds[0].numFrames)
                    adds[0].actFrame = 0;
            }
            adds[0].frame++;
        }
        if (TrapsON && Dibuja && !MyGame.Paused)
        {
            for (int s = 0; s < NumTotTraps; s++)
            {
                if (!trap[s].isOn)
                {
                    trap[s].actFrame++;
                    if (trap[s].actFrame > trap[s].numFrames - 1)
                        trap[s].actFrame = 0;
                    if (trap[s].type == 666)
                        trap[s].actFrame = 0;
                }
                else
                {
                    trap[s].actFrame++;
                    if (trap[s].actFrame > trap[s].numFrames - 1)
                    {
                        trap[s].isOn = false;
                        trap[s].actFrame = 0;
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
                cantidad22 = arrow[xz].area.Width * arrow[xz].area.Height;
                arrow[xz].flechas.GetData(Colormask22, 0, arrow[xz].flechas.Height * arrow[xz].flechas.Width);
                //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                int py = arrow[xz].area.Y;
                int px = arrow[xz].area.X;
                int alto66 = arrow[xz].area.Height;
                int ancho66 = arrow[xz].area.Width;
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
                if (!arrow[xz].right) //left arrows
                {
                    arrow[xz].desplaza++;
                    if (arrow[xz].desplaza < 0)
                    {
                        arrow[xz].desplaza = arrow[xz].flechas.Width - 1;
                    }
                    for (int y4 = 0; y4 < arrow[xz].area.Height; y4++)
                    {
                        for (int x4 = 0; x4 < arrow[xz].area.Width; x4++)
                        {
                            int posy456 = y4 % arrow[xz].flechas.Height;
                            int posx456 = x4 % arrow[xz].flechas.Width;
                            posx456 = (arrow[xz].flechas.Width - 1) - ((posx456 + arrow[xz].desplaza) % arrow[xz].flechas.Width); // left perfecto
                            Colormasktotal[(y4 * arrow[xz].area.Width) + x4].PackedValue = Colormask22[(posy456 * arrow[xz].flechas.Width) + posx456].PackedValue;
                        }
                    }
                    for (int r = 0; r < cantidad22; r++)
                    {
                        if (Colorsobre22[r].R > 0 || Colorsobre22[r].G > 0 || Colorsobre22[r].B > 0)
                        {
                            Colorsobre22[r].PackedValue = Colormasktotal[r].PackedValue;
                        }
                    }
                    arrow[xz].flechassobre.SetData(Colorsobre22, 0, arrow[xz].flechassobre.Height * arrow[xz].flechassobre.Width);
                }
                else //right arrows
                {
                    arrow[xz].desplaza--;
                    if (arrow[xz].desplaza < 0)
                    {
                        arrow[xz].desplaza = arrow[xz].flechas.Width - 1;
                    }
                    for (int y4 = 0; y4 < arrow[xz].area.Height; y4++)
                    {
                        for (int x4 = 0; x4 < arrow[xz].area.Width; x4++)
                        {
                            int posy456 = y4 % arrow[xz].flechas.Height;
                            int posx456 = x4 % arrow[xz].flechas.Width;
                            posx456 = ((posx456 + arrow[xz].desplaza) % arrow[xz].flechas.Width);  //Left okok
                            Colormasktotal[(y4 * arrow[xz].area.Width) + x4].PackedValue = Colormask22[(posy456 * arrow[xz].flechas.Width) + posx456].PackedValue;
                        }
                    }
                    for (int r = 0; r < cantidad22; r++)
                    {
                        if (Colorsobre22[r].R > 0 || Colorsobre22[r].G > 0 || Colorsobre22[r].B > 0)
                        {
                            Colorsobre22[r].PackedValue = Colormasktotal[r].PackedValue;
                        }
                    }
                    arrow[xz].flechassobre.SetData(Colorsobre22, 0, arrow[xz].flechassobre.Height * arrow[xz].flechassobre.Width);
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
                    if (trap[ti].areaTrap.Contains(x) && !trap[ti].isOn && trap[ti].type == 666)
                    {
                        trap[ti].isOn = true;
                        Lemming[actLEM].Active = false;
                        Lemming[actLEM].Walker = false;
                        Lemming[actLEM].Dead = true;
                        numlemnow--;
                        Lemming[actLEM].Explode = false;
                        Lemming[actLEM].Exploser = false;
                        switch (trap[ti].sprite.Name)
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
                    if (trap[ti].areaTrap.Intersects(rectangleFill) && !Lemming[actLEM].Burned && !Lemming[actLEM].Drown && trap[ti].type != 666)
                    {
                        switch (trap[ti].sprite.Name)
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
                    _nbDiggerRemaining--;
                    if (_nbDiggerRemaining < 0)
                    {
                        _nbDiggerRemaining = 0;
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
                    _nbClimberRemaining--;
                    if (_nbClimberRemaining < 0)
                    {
                        _nbClimberRemaining = 0;
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
                    _nbFloaterRemaining--;
                    if (_nbFloaterRemaining < 0)
                    {
                        _nbFloaterRemaining = 0;
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
                    _nbExploderRemaining--;
                    if (_nbExploderRemaining < 0)
                    {
                        _nbExploderRemaining = 0;
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
                    _nbBlockerRemaining--;
                    if (_nbBlockerRemaining < 0)
                    {
                        _nbBlockerRemaining = 0;
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
                    _nbBuilderRemaining--;
                    if (_nbBuilderRemaining < 0)
                    {
                        _nbBuilderRemaining = 0;
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
                    _nbBasherRemaining--;
                    if (_nbBasherRemaining < 0)
                    {
                        _nbBasherRemaining = 0;
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
                    _nbMinerRemaining--;
                    if (_nbMinerRemaining < 0)
                    {
                        _nbMinerRemaining = 0;
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
                        if (arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Right && !arrow[wer3].right)
                        {
                            nominer = true;
                            continue;
                        }
                        if (arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Left && arrow[wer3].right)
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
                            for (int xz = 0; xz < numTOTsteel; xz++)
                            {
                                if (steel[xz].area.Contains(x))
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
                            for (int xz = 0; xz < numTOTsteel; xz++)
                            {
                                if (steel[xz].area.Contains(x))
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
                        if (arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Right && !arrow[wer3].right)
                        {
                            nobasher = true;
                            continue;
                        }
                        if (arrow[wer3].area.Intersects(arrowLem) && Lemming[actLEM].Left && arrow[wer3].right)
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
                                for (int xz = 0; xz < numTOTsteel; xz++)
                                {
                                    if (steel[xz].area.Contains(x))
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
                                for (int xz = 0; xz < numTOTsteel; xz++)
                                {
                                    if (steel[xz].area.Contains(x))
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
                                    for (int xz = 0; xz < numTOTsteel; xz++)
                                    {
                                        if (steel[xz].area.Contains(x))
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
                        for (int xz = 0; xz < numTOTsteel; xz++)
                        {
                            if (steel[xz].area.Contains(x))
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
                int tYheight = trap[r].sprite.Height / trap[r].numFrames;
                if (trap[r].type != 555 && trap[r].type != 666)
                {
                    int vv444 = 0;
                    switch (trap[r].vvscroll)
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
                    colorFill.A = trap[r].transparency;
                    if (trap[r].R != 255 && trap[r].R > 0)
                        colorFill.R = trap[r].R;
                    if (trap[r].G != 255 && trap[r].G > 0)
                        colorFill.G = trap[r].G;
                    if (trap[r].B != 255 && trap[r].B > 0)
                        colorFill.B = trap[r].B;
                    rectangleFill.X = trap[r].areaDraw.X - ScrollX;
                    rectangleFill.Y = trap[r].areaDraw.Y - ScrollY;
                    rectangleFill.Width = trap[r].areaDraw.Width;
                    rectangleFill.Height = tYheight;
                    rectangleFill2.X = 0 + vv444;
                    rectangleFill2.Y = tYheight * trap[r].actFrame;
                    rectangleFill2.Width = trap[r].areaDraw.Width;
                    rectangleFill2.Height = tYheight;
                    spriteBatch.Draw(trap[r].sprite, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, trap[r].depth);
                }
                else
                {
                    colorFill.R = 255;
                    colorFill.G = 255;
                    colorFill.B = 255;
                    colorFill.A = trap[r].transparency;
                    if (trap[r].R != 255 && trap[r].R > 0)
                        colorFill.R = trap[r].R;
                    if (trap[r].G != 255 && trap[r].G > 0)
                        colorFill.G = trap[r].G;
                    if (trap[r].B != 255 && trap[r].B > 0)
                        colorFill.B = trap[r].B;
                    int spY = trap[r].sprite.Height / trap[r].numFrames;
                    rectangleFill.X = (int)trap[r].pos.X - ScrollX - trap[r].vvX;
                    rectangleFill.Y = (int)trap[r].pos.Y - trap[r].vvY - ScrollY;
                    rectangleFill.Width = trap[r].sprite.Width;
                    rectangleFill.Height = spY;
                    rectangleFill2.X = 0;
                    rectangleFill2.Y = spY * trap[r].actFrame;
                    rectangleFill2.Width = trap[r].sprite.Width;
                    rectangleFill2.Height = spY;
                    spriteBatch.Draw(trap[r].sprite, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, trap[r].depth);
                }
                if (LemmingsNetGame.Instance.DebugOsd.debug)
                {
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, new Rectangle(trap[r].areaTrap.Left - ScrollX, trap[r].areaTrap.Top - ScrollY, trap[r].areaTrap.Width, trap[r].areaTrap.Height),
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
                spriteBatch.Draw(arrow[xz].flechassobre, new Vector2(arrow[xz].area.X - ScrollX, arrow[xz].area.Y - ScrollY),
                    new Rectangle(0, 0, arrow[xz].flechassobre.Width, arrow[xz].flechassobre.Height),
                    new Color(255, 255, 255, arrow[xz].transparency), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.499f);
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
            for (int i = 0; i < numTOTplats; i++)
            {
                int x2 = plats[i].areaDraw.X - plats[i].areaDraw.Width / 2;
                int y = plats[i].areaDraw.Y;
                int w = plats[i].sprite.Width;
                int h = plats[i].sprite.Height;
                spriteBatch.Draw(plats[i].sprite, new Rectangle(x2 - ScrollX, y - ScrollY - 5, plats[i].areaDraw.Width, plats[i].areaDraw.Height),
                    new Rectangle(0, 0, w, h), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.56f);
            }
        }
        if (moreDoors == null)
        {
            spriteBatch.Draw(puerta_ani, new Vector2(door1X - ScrollX, door1Y - ScrollY), new Rectangle(0, framereal565, xx55, yy55),
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
        }
        else
        {
            for (int i = 0; i < numTOTdoors; i++)
            {
                door1X = (int)moreDoors[i].doorMoreXY.X;
                door1Y = (int)moreDoors[i].doorMoreXY.Y;
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
        if (moreexits == null)
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
            for (int ex22 = 0; ex22 < numTOTexits; ex22++)
            {
                output1X = (int)moreexits[ex22].exitMoreXY.X;
                output1Y = (int)moreexits[ex22].exitMoreXY.Y;
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
        if (fade)
        {
            rest++;
            int rest2 = rest * 7;
            if (rest2 < 70)
                rest2 = 0;
            LemmingsNetGame.Instance.Gfx.DrawLine(spriteBatch, new Vector2(0, 0), new Vector2(MyGame.GameResolution.X, 0), new Color(0, 0, 0, 255 - rest2), MyGame.GameResolution.Y, 0f);
            if (Frame > 19)
            {
                fade = false;
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
                fade = true;
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
                fade = true;
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
        if (_nbClimberRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D3) && Input.CurrentKeyState.IsKeyUp(Keys.D3))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.CLIMBER;
        }
        else if (_nbFloaterRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D4) && Input.CurrentKeyState.IsKeyUp(Keys.D4))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.FLOATER;
        }
        else if (_nbExploderRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D5) && Input.CurrentKeyState.IsKeyUp(Keys.D5))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.EXPLODER;
        }
        else if (_nbBlockerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D6) && Input.CurrentKeyState.IsKeyUp(Keys.D6))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BLOCKER;
        }
        else if (_nbBuilderRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D7) && Input.CurrentKeyState.IsKeyUp(Keys.D7))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BUILDER;
        }
        else if (_nbBasherRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D8) && Input.CurrentKeyState.IsKeyUp(Keys.D8))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.BASHER;
        }
        else if (_nbMinerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D9) && Input.CurrentKeyState.IsKeyUp(Keys.D9))
        {
            LemmingsNetGame.Instance.Sfx.PlaySoundMenu();
            _inGameMenu.CurrentSelectedSkill = ECurrentSkill.MINER;
        }
        else if (_nbDiggerRemaining > 0 && Input.PreviousKeyState.IsKeyDown(Keys.D0) && Input.CurrentKeyState.IsKeyUp(Keys.D0))
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
            if (numTOTdoors > 1 && moreDoors != null) // more than 1 door is different calculation
            {
                door1Y = (int)moreDoors[numACTdoor].doorMoreXY.Y;
                door1X = (int)moreDoors[numACTdoor].doorMoreXY.X;
                numACTdoor++;
                if (numACTdoor >= numTOTdoors)
                    numACTdoor = 0;
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
            if (moreexits == null)
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
                for (ex11 = 0; ex11 < numTOTexits; ex11++) // more than one EXIT place
                {
                    output1X = (int)moreexits[ex11].exitMoreXY.X;
                    output1Y = (int)moreexits[ex11].exitMoreXY.Y;
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

    private void VariablesTraps()
    {
        switch (LemmingsNetGame.Instance.CurrentLevelNumber)
        {
            case 1:
                Sprite = new Varsprites[6];
                Sprite[0].actFrame = 0;
                Sprite[0].axisX = 4;
                Sprite[0].axisY = 4;
                Sprite[0].depth = 0.806f;
                Sprite[0].R = 255;
                Sprite[0].G = 255;
                Sprite[0].B = 255;
                Sprite[0].transparency = 200;
                Sprite[0].pos = new Vector2(0, 0);
                Sprite[0].scale = 9f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[0].rotation = 0f;
                Sprite[0].framesecond = 4;
                Sprite[0].frame = 0;
                Sprite[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("sprite/magma_mask");
                Sprite[0].minusScrollx = false;
                Sprite[1].actFrame = 0;
                Sprite[1].axisX = 8;
                Sprite[1].axisY = 8;
                Sprite[1].depth = 0.406f;
                Sprite[1].R = 255;
                Sprite[1].G = 255;
                Sprite[1].B = 255;
                Sprite[1].transparency = 255;
                Sprite[1].pos = new Vector2(1188, 337); //340
                Sprite[1].scale = 0.35f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[1].rotation = 0.1f;
                Sprite[1].framesecond = 0;
                Sprite[1].frame = 0;
                Sprite[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("sprite/flame");
                Sprite[1].minusScrollx = true;
                Sprite[2].actFrame = 0;
                Sprite[2].axisX = 8;
                Sprite[2].axisY = 8;
                Sprite[2].depth = 0.405f;
                Sprite[2].R = 255;
                Sprite[2].G = 225;
                Sprite[2].B = 225;
                Sprite[2].transparency = 255;
                Sprite[2].pos = new Vector2(1136, 337);
                Sprite[2].scale = 0.35f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[2].rotation = 0.05f;
                Sprite[2].framesecond = 0;
                Sprite[2].frame = 0;
                Sprite[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("sprite/flame");
                Sprite[2].minusScrollx = true;
                Sprite[3].actFrame = 0;
                Sprite[3].axisX = 6;
                Sprite[3].axisY = 1;
                Sprite[3].depth = 0.405f;
                Sprite[3].R = 255;
                Sprite[3].G = 225;
                Sprite[3].B = 225;
                Sprite[3].transparency = 255;
                Sprite[3].pos = new Vector2(0, 0);
                Sprite[3].scale = 0.5f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[3].rotation = 0f;
                Sprite[3].framesecond = 1;
                Sprite[3].frame = 0;
                Sprite[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("touch/arana");
                Sprite[3].calc = true;
                Sprite[3].minusScrollx = true;
                Sprite[3].dest = new Vector2(0, 0);
                Sprite[3].speed = 0.578f;  // this field is important for move logic of sprites != 0
                Sprite[3].actVect = 0;
                Sprite[3].center.X = ((Sprite[3].sprite.Width / Sprite[3].axisX) / 2);
                Sprite[3].center.Y = ((Sprite[3].sprite.Height / Sprite[3].axisY) / 2);
                Sprite[3].path = new Vector3[7];
                Sprite[3].path[0] = new Vector3(48, 65, 1.5f);
                Sprite[3].path[1] = new Vector3(200, 140, 1.7f);
                Sprite[3].path[2] = new Vector3(238, 139, 1.9f);
                Sprite[3].path[3] = new Vector3(146, 407, 1.6f);
                Sprite[3].path[4] = new Vector3(326, 475, 2f);
                Sprite[3].path[5] = new Vector3(405, 322, 1.2f);
                Sprite[3].path[6] = new Vector3(470, 211, 1.5f);
                Sprite[4].actFrame = 0;
                Sprite[4].axisX = 2;
                Sprite[4].axisY = 10;
                Sprite[4].depth = 0.505f;
                Sprite[4].R = 255;
                Sprite[4].G = 225;
                Sprite[4].B = 225;
                Sprite[4].transparency = 255;
                Sprite[4].pos = new Vector2(120, -190);
                Sprite[4].scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[4].rotation = 1.57f;
                Sprite[4].framesecond = 2;
                Sprite[4].frame = 0;
                Sprite[4].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("touch/fire_sprites_other");
                Sprite[4].minusScrollx = false;
                Sprite[4].minus = false;
                Sprite[5].calc = true;
                Sprite[5].actFrame = 0;
                Sprite[5].axisX = 6;
                Sprite[5].axisY = 1;
                Sprite[5].depth = 0.405f;
                Sprite[5].R = 255;
                Sprite[5].G = 225;
                Sprite[5].B = 225;
                Sprite[5].transparency = 255;
                Sprite[5].pos = new Vector2(0, 0);
                Sprite[5].scale = 0.3f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[5].rotation = 0f;
                Sprite[5].framesecond = 2;
                Sprite[5].frame = 0;
                Sprite[5].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("touch/arana");
                Sprite[5].minusScrollx = true;
                Sprite[5].dest = new Vector2(0, 0);
                Sprite[5].speed = 0.578f;  // this field is important for move logic of sprites != 0
                Sprite[5].actVect = 0;
                Sprite[5].center.X = ((Sprite[3].sprite.Width / Sprite[3].axisX) / 2);
                Sprite[5].center.Y = ((Sprite[3].sprite.Height / Sprite[3].axisY) / 2);
                Sprite[5].path = new Vector3[6];
                Sprite[5].path[0] = new Vector3(1000, 5, 1.5f);
                Sprite[5].path[1] = new Vector3(1090, 95, 1.7f);
                Sprite[5].path[2] = new Vector3(1069, 252, 1.9f);
                Sprite[5].path[3] = new Vector3(1173, 300, 1.6f);
                Sprite[5].path[4] = new Vector3(1241, 138, 2f);
                Sprite[5].path[5] = new Vector3(1300, 5, 1.2f);

                break;
            case 4:
                Sprite = new Varsprites[1];
                Sprite[0].actFrame = 0;
                Sprite[0].axisX = 4;
                Sprite[0].axisY = 4;
                Sprite[0].depth = 0.806f;
                Sprite[0].R = 255;
                Sprite[0].G = 255;
                Sprite[0].B = 255;
                Sprite[0].transparency = 200;
                Sprite[0].pos = new Vector2(0, 0);
                Sprite[0].scale = 9f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[0].rotation = 0f;
                Sprite[0].framesecond = 4;
                Sprite[0].frame = 0;
                Sprite[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("sprite/magma_mask");
                Sprite[0].minusScrollx = false;
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(820, 462, 1529 - 820, 40); // 512-40=462 bottom of the screen
                trap[0].areaTrap = new Rectangle(820, 467, 1529 - 820, 10); //normally +5 on Y and 10 of height
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                break;
            case 5:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1; // kind of variable scroll the trap 1=z1--
                trap[0].areaDraw = new Rectangle(510, 480, 300, 32); //512-32=480 bottom of the screen
                trap[0].areaTrap = new Rectangle(510, 485, 300, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/ice_water");
                trap[1].vvscroll = 2; // kind of variable scroll the trap 1=z1 -- 2=-z1 --
                trap[1].areaDraw = new Rectangle(518, 460, 280, 32);
                trap[1].areaTrap = new Rectangle(518, 465, 280, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 130;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/ice_water");
                break;
            case 6:
            case 47:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 2;
                trap[0].areaDraw = new Rectangle(320, 472, 2189 - 320, 40);
                trap[0].areaTrap = new Rectangle(320, 477, 2189 - 320, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 160;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1162 - 159 + 30, 108 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1162, 108);
                trap[1].vvX = 159;
                trap[1].vvY = 27;
                trap[1].depth = 0.400000009f;
                trap[1].transparency = 200;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 7:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(192, 472, 3220 - 192, 40);
                trap[0].areaTrap = new Rectangle(192, 477, 3220 - 192, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                break;
            case 8:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(325, 480, 3500 - 325, 32);
                trap[0].areaTrap = new Rectangle(325, 485, 3500 - 325, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 9:
            case 56:
                NumTotTraps = 4;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 462, MyGame.GameResolution.X, 40);
                trap[0].areaTrap = new Rectangle(0, 470, MyGame.GameResolution.X, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(507 - 23, 202 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(507, 202);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(852 - 23, 399 - 38 / 2, 23 * 2, 10); //see .pos
                trap[2].numFrames = 16;
                trap[2].actFrame = 0;
                trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[2].isOn = false;
                trap[2].pos = new Vector2(852, 399);
                trap[2].vvX = 32;
                trap[2].vvY = 38;
                trap[2].depth = 0.200000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(164 - 23, 393 - 38 / 2, 23 * 2, 10);
                trap[3].numFrames = 16;
                trap[3].actFrame = 0;
                trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[3].isOn = false;
                trap[3].pos = new Vector2(164, 393);
                trap[3].vvX = 32;
                trap[3].vvY = 38;
                trap[3].depth = 0.200000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 10:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(380, 490, 1843 - 380, 32);
                trap[0].areaTrap = new Rectangle(380, 495, 1843 - 380, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].G = 150;
                trap[0].B = 20;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 480, 2303, 32);
                trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.400000009f;
                trap[1].G = 20;
                trap[1].B = 20;
                trap[1].transparency = 170;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 11:
            case 78:
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = false;
                arrow[0].area = new Rectangle(468, 161, 773 - 468, 440 - 161);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(239, 440, 1227 - 239, 512 - 440);
                break;
            case 13:
            case 101:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 472, 3203, 40);
                trap[0].areaTrap = new Rectangle(0, 477, 3203, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 14:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(180, 480, 2884 - 180, 32);
                trap[0].areaTrap = new Rectangle(180, 485, 2884 - 180, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 15:
            case 61:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(3317 - 5, 455 - 5, 10, 10);//see .pos minus 5 on both axis
                trap[0].numFrames = 37;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(3317, 455);
                trap[0].vvX = 10;
                trap[0].vvY = 71;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 16:
            case 63:
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 472, 2205, 512);
                trap[0].areaTrap = new Rectangle(0, 475, 2205, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(0, 0);
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(845 - 30, 250 - 30 / 2, 30 * 2, 10);//see .pos
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(845, 250);
                trap[1].vvX = 30;
                trap[1].vvY = 30;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(895 - 30, 250 - 30 / 2, 30 * 2, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(895, 250);
                trap[2].vvX = 30;
                trap[2].vvY = 30;
                trap[2].depth = 0.600000008f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 17:
            case 66:
                numTOTdoors = 4; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[17].doorX, LemmingsNetGame.Instance.Levels.AllLevel[17].doorY);
                moreDoors[1].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[17].doorX + 220, LemmingsNetGame.Instance.Levels.AllLevel[17].doorY);
                moreDoors[2].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[17].doorX + 430, LemmingsNetGame.Instance.Levels.AllLevel[17].doorY);
                moreDoors[3].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[17].doorX + 640, LemmingsNetGame.Instance.Levels.AllLevel[17].doorY);//IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                SteelON = true; numTOTsteel = 2;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(261, 373, 1154 - 261, 450 - 373);
                steel[1].area = new Rectangle(997, 284, 1153 - 997, 370 - 284);
                NumTotTraps = 4;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(391 - 5, 497 - 5, 10, 10);
                trap[0].numFrames = 15;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(391, 497);
                trap[0].vvX = 31;
                trap[0].vvY = 57;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(590 - 5, 497 - 5, 10, 10);
                trap[1].numFrames = 15;
                trap[1].actFrame = 0;
                trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(590, 497);
                trap[1].vvX = 31;
                trap[1].vvY = 57;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(792 - 5, 497 - 5, 10, 10);
                trap[2].numFrames = 15;
                trap[2].actFrame = 0;
                trap[2].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(792, 497);
                trap[2].vvX = 31;
                trap[2].vvY = 57;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(998 - 5, 497 - 5, 10, 10);
                trap[3].numFrames = 15;
                trap[3].actFrame = 0;
                trap[3].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[3].isOn = false;
                trap[3].pos = new Vector2(998, 497);
                trap[3].vvX = 31;
                trap[3].vvY = 57;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                break;
            case 18:
            case 79:
                NumTotTraps = 6;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(875 - 30, 454 - 30 / 2, 30 * 2, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(875, 454);
                trap[0].vvX = 30;
                trap[0].vvY = 30;
                trap[0].depth = 0.600000008f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(460 - 30 + 4, 162 - 30 / 2, 30 * 2 - 4, 10);//see .pos
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(460, 162);
                trap[1].vvX = 30;
                trap[1].vvY = 30;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(890 - 30 + 4, 158 - 30 / 2, 30 * 2 - 4, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(890, 158);
                trap[2].vvX = 30;
                trap[2].vvY = 30;
                trap[2].depth = 0.600000008f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(587 - 30 + 4, 251 - 30 / 2, 30 * 2 - 4, 10);
                trap[3].numFrames = 8;
                trap[3].actFrame = 0;
                trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[3].isOn = false;
                trap[3].pos = new Vector2(587, 251);
                trap[3].vvX = 30;
                trap[3].vvY = 30;
                trap[3].depth = 0.600000008f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[4].areaTrap = new Rectangle(957 - 30 + 4, 312 - 30 / 2, 30 * 2 - 4, 10);
                trap[4].numFrames = 8;
                trap[4].actFrame = 0;
                trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[4].isOn = false;
                trap[4].pos = new Vector2(957, 312);
                trap[4].vvX = 30;
                trap[4].vvY = 30;
                trap[4].depth = 0.600000008f;
                trap[4].transparency = 255;
                trap[4].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[5].areaTrap = new Rectangle(529 - 30 + 4, 377 - 30 / 2, 30 * 2 - 4, 10);
                trap[5].numFrames = 8;
                trap[5].actFrame = 0;
                trap[5].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[5].isOn = false;
                trap[5].pos = new Vector2(529, 377);
                trap[5].vvX = 30;
                trap[5].vvY = 30;
                trap[5].depth = 0.600000008f;
                trap[5].transparency = 255;
                trap[5].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 20:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(127, 462, 3638 - 127, 40); // 512-40=462 bottom of the screen
                trap[0].areaTrap = new Rectangle(127, 467, 3638 - 127, 10); //normally +5 on Y and 10 of height
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1121 - 23, 376 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1121, 376);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 21:
            case 116:
                NumTotTraps = 4;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 470, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 470, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(1001, 480, 1133 - 1001, 32);
                trap[1].areaTrap = new Rectangle(1001, 485, 1133 - 1001, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 170;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[2].areaDraw = new Rectangle(3143, 480, 3757 - 3143, 32);
                trap[2].areaTrap = new Rectangle(3143, 485, 3757 - 3143, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 170;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(2510 - 5, 473 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[3].numFrames = 15;
                trap[3].actFrame = 0;
                trap[3].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[3].isOn = false;
                trap[3].pos = new Vector2(2510, 473);
                trap[3].vvX = 16;
                trap[3].vvY = 42;  //38
                trap[3].depth = 0.400000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                break;
            case 23:
                numTOTexits = 2;
                moreexits = new Varmoreexits[numTOTexits];
                moreexits[0].exitMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[23].exitX, LemmingsNetGame.Instance.Levels.AllLevel[23].exitY); //73,460 ----- LEVEL 23 TWO EXITS
                moreexits[1].exitMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[23].exitX, 180);//73,180 //IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(2475, 472, 2594 - 2475, 40);
                trap[0].areaTrap = new Rectangle(2475, 477, 2594 - 2475, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(606 - 159 + 30, 308 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(606, 308);
                trap[1].vvX = 159;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 24:
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = true;
                arrow[0].area = new Rectangle(754, 143, 860 - 754, 216 - 143);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(862, 129, 1304 - 862, 306 - 129);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1477 - 5, 345 - 5, 10, 10);
                trap[0].numFrames = 15;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1477, 345);
                trap[0].vvX = 47;
                trap[0].vvY = 87;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_marble2_fix");
                trap[1].areaDraw = new Rectangle(1071, 225, 1266 - 1071, 40); // 512-40=462 bottom of the screen
                trap[1].areaTrap = new Rectangle(1071, 230, 1266 - 1071, 10); //normally +5 on Y and 10 of height
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                break;
            case 26:
            case 103:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(2797 - 5, 245 - 5, 10, 10);//see .pos minus 5 on both axis
                trap[0].numFrames = 37;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(2797, 245);
                trap[0].vvX = 10;
                trap[0].vvY = 71;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                trap[1].areaDraw = new Rectangle(0, 480, 3007, 32);
                trap[1].areaTrap = new Rectangle(0, 485, 3007, 10);// not necessary
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.400000009f;
                trap[1].transparency = 170;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 27:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 487, 2624, 40);
                trap[0].areaTrap = new Rectangle(0, 492, 2624, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                break;
            case 28:
            case 95:
                NumTotTraps = 4;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(253, 472, 861 - 253, 40);
                trap[0].areaTrap = new Rectangle(253, 477, 861 - 253, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].vvscroll = 2;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1334 - 159 + 30, 192 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1334, 192);
                trap[1].vvX = 159;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                trap[2].areaDraw = new Rectangle(2038, 472, 2622 - 2038, 40);
                trap[2].areaTrap = new Rectangle(2038, 477, 2622 - 2038, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].vvscroll = 1;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(1495, 192 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[3].numFrames = 10;
                trap[3].actFrame = 0;
                trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[3].isOn = false;
                trap[3].pos = new Vector2(1495, 192);
                trap[3].vvX = 0;
                trap[3].vvY = 27;
                trap[3].depth = 0.600000009f;
                trap[3].B = 160;
                trap[3].G = 160;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 29:
            case 99:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(1812, 480, 2205 - 1812, 32);
                trap[0].areaTrap = new Rectangle(1812, 485, 2205 - 1812, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.200000009f; //lemmings depth 0.300f
                trap[0].transparency = 190;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 30:
            case 114:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(71, 472, 1150 - 71, 40);
                trap[0].areaTrap = new Rectangle(71, 477, 1150 - 71, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 230;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            //tricky levels tricky
            case 31:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(0, 480, 3022, 32);
                trap[0].areaTrap = new Rectangle(0, 480, 3022, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 33:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 2;
                trap[0].areaDraw = new Rectangle(370, 480, 1479 - 370, 32);
                trap[0].areaTrap = new Rectangle(370, 480, 1479 - 370, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 34:
            case 67:
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = false;
                arrow[0].area = new Rectangle(1063, 45, 1214 - 1063, 284 - 45);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 190;
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(84, 284, 1682 - 84, 330 - 284);
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 3030, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 3030, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 35:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(728, 480, 3277 - 728, 32);
                trap[0].areaTrap = new Rectangle(728, 485, 3277 - 728, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 36:
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 490, 2088, 512); //special size
                trap[0].areaTrap = new Rectangle(0, 495, 2088, 10);
                trap[0].vvscroll = 2;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(0, 0);
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].B = 130;
                trap[0].G = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(444, 341 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(444, 341);
                trap[1].vvX = 0;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(1761 - 159 + 30, 251 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[2].numFrames = 10;
                trap[2].actFrame = 0;
                trap[2].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[2].isOn = false;
                trap[2].pos = new Vector2(1761, 251);
                trap[2].vvX = 159;
                trap[2].vvY = 27;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 37:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 1281, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 1281, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 120;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 38:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 472, 2088, 40); //normal size
                trap[0].areaTrap = new Rectangle(0, 482, 2088, 10);
                trap[0].vvscroll = 2;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(0, 0);
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].B = 130;
                trap[0].G = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 39:
            case 96:
                ArrowsON = true; NumTotArrow = 2;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = true;
                arrow[0].area = new Rectangle(685, 336, 781 - 685, 421 - 336);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                arrow[1].right = false;
                arrow[1].area = new Rectangle(2466, 110, 2577 - 2466, 213 - 110); // mask texture full steel zone
                arrow[1].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[1].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[1].area.Width, arrow[1].area.Height);
                arrow[1].desplaza = 0;
                arrow[1].transparency = 255;
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 3153, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 3153, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 120;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                SteelON = true; numTOTsteel = 2;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(553, 216, 2775 - 553, 330 - 216);
                steel[1].area = new Rectangle(2698, 144, 2851 - 2698, 216 - 144);
                break;
            case 40:
            case 105:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[40].doorX, LemmingsNetGame.Instance.Levels.AllLevel[40].doorY);
                moreDoors[1].doorMoreXY = new Vector2(2240, LemmingsNetGame.Instance.Levels.AllLevel[40].doorY);
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 472, 2853, 40); //normal size
                trap[0].areaTrap = new Rectangle(0, 482, 2853, 10);
                trap[0].vvscroll = 1;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(0, 0);
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].B = 80;
                trap[0].G = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 41:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(1027, 480, 1553 - 1027, 32);
                trap[0].areaTrap = new Rectangle(1027, 485, 1553 - 1027, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 43:
            case 115:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 441, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 441, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 49:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(0, 490, 3391, 32);
                trap[0].areaTrap = new Rectangle(0, 495, 3391, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 50:
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = false;
                arrow[0].area = new Rectangle(1318, 164, 1478 - 1318, 460 - 164);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                NumTotTraps = 5;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(217, 411 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(217, 411);
                trap[0].vvX = 0;
                trap[0].vvY = 27;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(754 - 30, 160 - 30 / 2, 30 * 2, 10);//see .pos
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(754, 160);
                trap[1].vvX = 30;
                trap[1].vvY = 30;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(801 - 30, 160 - 30 / 2, 30 * 2, 10);//see .pos
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(801, 160);
                trap[2].vvX = 30;
                trap[2].vvY = 30;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(1770 - 30, 169 - 30 / 2, 30 * 2, 10);//see .pos
                trap[3].numFrames = 8;
                trap[3].actFrame = 0;
                trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[3].isOn = false;
                trap[3].pos = new Vector2(1770, 169);
                trap[3].vvX = 30;
                trap[3].vvY = 30;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[4].areaTrap = new Rectangle(1821 - 30, 169 - 30 / 2, 30 * 2, 10);//see .pos
                trap[4].numFrames = 8;
                trap[4].actFrame = 0;
                trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[4].isOn = false;
                trap[4].pos = new Vector2(1821, 169);
                trap[4].vvX = 30;
                trap[4].vvY = 30;
                trap[4].depth = 0.600000009f;
                trap[4].transparency = 255;
                trap[4].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 52:
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(384, 480, 3197 - 384, 32);
                trap[0].areaTrap = new Rectangle(384, 485, 3197 - 384, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.4000000011f;
                trap[0].transparency = 200;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 480, 278, 32);
                trap[1].areaTrap = new Rectangle(0, 485, 278, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.4000000011f;
                trap[1].transparency = 200;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[2].areaDraw = new Rectangle(3197, 480, 3649 - 3197, 32);
                trap[2].areaTrap = new Rectangle(3197, 485, 3649 - 3197, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.6000000011f;
                trap[2].transparency = 200;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 55:
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(2511 - 5, 334 - 5, 10, 10);//see .pos minus 5 on both axis
                trap[0].numFrames = 37;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(2511, 334);
                trap[0].vvX = 10;
                trap[0].vvY = 71;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                trap[1].vvscroll = 1;
                trap[1].areaDraw = new Rectangle(1300, 480, 4089 - 1300, 32);
                trap[1].areaTrap = new Rectangle(1300, 485, 4089 - 1300, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.6000000011f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[2].vvscroll = 2;
                trap[2].areaDraw = new Rectangle(0, 480, 787, 32);
                trap[2].areaTrap = new Rectangle(0, 485, 787, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.6000000011f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 57:
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = false;
                arrow[0].area = new Rectangle(1306, 35, 1805 - 1306, 318 - 35);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 2;
                trap[0].areaDraw = new Rectangle(0, 480, 777, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 777, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 58:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(0, 480, 3117, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 3117, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(1780, 239, 2006 - 1780, 280 - 239);
                break;
            case 59:
                numTOTdoors = 3; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[59].doorX, LemmingsNetGame.Instance.Levels.AllLevel[59].doorY);
                moreDoors[1].doorMoreXY = new Vector2(459, 150);
                moreDoors[2].doorMoreXY = new Vector2(770, 46);
                NumTotTraps = 7;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(623 - 23, 429 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(623, 429);
                trap[0].vvX = 32;
                trap[0].vvY = 38;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1266 - 23, 451 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1266, 451);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(1591 - 23, 132 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[2].numFrames = 16;
                trap[2].actFrame = 0;
                trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[2].isOn = false;
                trap[2].pos = new Vector2(1591, 132);
                trap[2].vvX = 32;
                trap[2].vvY = 38;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(1604 - 23, 367 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[3].numFrames = 16;
                trap[3].actFrame = 0;
                trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[3].isOn = false;
                trap[3].pos = new Vector2(1604, 367);
                trap[3].vvX = 32;
                trap[3].vvY = 38;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[4].areaTrap = new Rectangle(2224 - 23, 153 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[4].numFrames = 16;
                trap[4].actFrame = 0;
                trap[4].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[4].isOn = false;
                trap[4].pos = new Vector2(2224, 153);
                trap[4].vvX = 32;
                trap[4].vvY = 38;
                trap[4].depth = 0.600000009f;
                trap[4].transparency = 255;
                trap[4].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[5].areaTrap = new Rectangle(2154 - 23, 332 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[5].numFrames = 16;
                trap[5].actFrame = 0;
                trap[5].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[5].isOn = false;
                trap[5].pos = new Vector2(2154, 332);
                trap[5].vvX = 32;
                trap[5].vvY = 38;
                trap[5].depth = 0.600000009f;
                trap[5].transparency = 255;
                trap[5].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[6].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[6].areaTrap = new Rectangle(2763 - 23, 334 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[6].numFrames = 16;
                trap[6].actFrame = 0;
                trap[6].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[6].isOn = false;
                trap[6].pos = new Vector2(2763, 334);
                trap[6].vvX = 32;
                trap[6].vvY = 38;
                trap[6].depth = 0.600000009f;
                trap[6].transparency = 255;
                trap[6].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 60:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 2;
                trap[0].areaDraw = new Rectangle(0, 480, 3090, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 3090, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 224;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            // TAXING LEVELS TAXING //////////////////////////////////////////////////////////////////////////
            case 62:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[62].doorX, LemmingsNetGame.Instance.Levels.AllLevel[62].doorY);
                moreDoors[1].doorMoreXY = new Vector2(1962, LemmingsNetGame.Instance.Levels.AllLevel[62].doorY);
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(1614, 129, 1967 - 1614, 253 - 129);
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(2352 - 5, 157 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 15;
                trap[0].actFrame = 0;
                trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(2352, 157);
                trap[0].vvX = 16;
                trap[0].vvY = 42;  //38
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1052 - 5, 459 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 15;
                trap[1].actFrame = 0;
                trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1052, 459);
                trap[1].vvX = 16;
                trap[1].vvY = 42;  //38
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(2712 - 5, 134 - 5, 10, 10);
                trap[2].numFrames = 12;
                trap[2].actFrame = 0;
                trap[2].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[2].isOn = false;
                trap[2].pos = new Vector2(2712, 134);
                trap[2].vvX = 40;
                trap[2].vvY = 105;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_10");
                break;
            case 64:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[64].doorX, LemmingsNetGame.Instance.Levels.AllLevel[64].doorY);
                moreDoors[1].doorMoreXY = new Vector2(1174, LemmingsNetGame.Instance.Levels.AllLevel[64].doorY);
                SteelON = true; numTOTsteel = 2;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(1000, 272, 1507 - 1000, 318 - 272);
                steel[1].area = new Rectangle(1265, 319, 1309 - 1265, 442 - 319);
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1003 - 12, 175 - 18, 15, 36);
                trap[0].numFrames = 7;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1003, 175);
                trap[0].vvX = 30;
                trap[0].vvY = 26;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_left");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(908, 272 - 18, 15, 36);
                trap[1].numFrames = 7;
                trap[1].actFrame = 0;
                trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(908, 272);
                trap[1].vvX = 0;
                trap[1].vvY = 26;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_right");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(1574 - 5, 445 - 5, 10, 10);//see .pos minus 5 on both axis
                trap[2].numFrames = 37;
                trap[2].actFrame = 0;
                trap[2].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(1574, 445);
                trap[2].vvX = 10;
                trap[2].vvY = 71;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 65:
                numTOTexits = 2;
                moreexits = new Varmoreexits[numTOTexits];
                moreexits[0].exitMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[65].exitX, LemmingsNetGame.Instance.Levels.AllLevel[65].exitY);
                moreexits[1].exitMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[65].exitX, 461);
                SteelON = true; numTOTsteel = 2;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(161, 436, 390 - 161, 488 - 436);
                steel[1].area = new Rectangle(537, 160, 610 - 537, 309 - 160);
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(611, 196 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(611, 196);
                trap[0].vvX = 0;
                trap[0].vvY = 27;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(611, 274 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(611, 274);
                trap[1].vvX = 0;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[2].vvscroll = 0;
                trap[2].areaDraw = new Rectangle(0, 472, 1152, 40);
                trap[2].areaTrap = new Rectangle(0, 477, 1152, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 68:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 3405, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 3405, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 69:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 771, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 771, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(1448, 480, 2304 - 1448, 32);
                trap[1].areaTrap = new Rectangle(1448, 485, 2304 - 1448, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.400000009f; //lemmings depth 0.300f
                trap[1].transparency = 200;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 70:
                NumTotTraps = 5;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(439, 342 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(439, 342);
                trap[0].vvX = 0;
                trap[0].vvY = 27;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1767 - 159 + 30, 217 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1767, 217);
                trap[1].vvX = 159;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                trap[2].vvscroll = 0;
                trap[2].areaDraw = new Rectangle(0, 472, 1275, 40);
                trap[2].areaTrap = new Rectangle(0, 477, 1275, 10);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[3].vvscroll = 0;
                trap[3].areaDraw = new Rectangle(1435, 472, 2090 - 1435, 40);
                trap[3].areaTrap = new Rectangle(1435, 477, 2090 - 1435, 10);
                trap[3].numFrames = 8;
                trap[3].actFrame = 0;
                trap[3].type = 1;
                trap[3].isOn = false;
                trap[3].pos = Vector2.Zero;
                trap[3].vvX = 0;
                trap[3].vvY = 0;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[4].vvscroll = 0;
                trap[4].areaDraw = new Rectangle(1281, 482, 1429 - 1281, 40);
                trap[4].areaTrap = new Rectangle(1291, 497, 1, 1); //null kill area
                trap[4].numFrames = 8;
                trap[4].actFrame = 0;
                trap[4].type = 1;
                trap[4].isOn = false;
                trap[4].pos = Vector2.Zero;
                trap[4].vvX = 0;
                trap[4].vvY = 0;
                trap[4].depth = 0.600000009f;
                trap[4].transparency = 255;
                trap[4].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 71:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 3604, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 3604, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].vvscroll = 2;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 72:
                NumTotTraps = 6;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(0, 472, 3000, 40);
                trap[0].areaTrap = new Rectangle(0, 477, 3000, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].vvscroll = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(752, 205 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(752, 205);
                trap[1].vvX = 0;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(1336 - 30, 178 - 30 / 2, 30 * 2, 10);//see .pos
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(1336, 178);
                trap[2].vvX = 30;
                trap[2].vvY = 30;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(1386 - 30, 178 - 30 / 2, 30 * 2, 10);//see .pos
                trap[3].numFrames = 8;
                trap[3].actFrame = 0;
                trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[3].isOn = false;
                trap[3].pos = new Vector2(1386, 178);
                trap[3].vvX = 30;
                trap[3].vvY = 30;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[4].areaTrap = new Rectangle(1642 - 30, 119 - 30 / 2, 30 * 2, 10);//see .pos
                trap[4].numFrames = 8;
                trap[4].actFrame = 0;
                trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[4].isOn = false;
                trap[4].pos = new Vector2(1642, 119);
                trap[4].vvX = 30;
                trap[4].vvY = 30;
                trap[4].depth = 0.600000009f;
                trap[4].transparency = 255;
                trap[4].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[5].areaTrap = new Rectangle(1681 - 30, 119 - 30 / 2, 30 * 2, 10);//see .pos
                trap[5].numFrames = 8;
                trap[5].actFrame = 0;
                trap[5].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[5].isOn = false;
                trap[5].pos = new Vector2(1691, 119);
                trap[5].vvX = 30;
                trap[5].vvY = 30;
                trap[5].depth = 0.600000009f;
                trap[5].transparency = 255;
                trap[5].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 73:
                ArrowsON = true; NumTotArrow = 2;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = true;
                arrow[0].area = new Rectangle(1737, 121, 1932 - 1737, 326 - 121);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow2");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                arrow[1].right = true;
                arrow[1].area = new Rectangle(478, 42, 631 - 478, 374 - 42); // mask texture full steel zone
                arrow[1].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[1].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[1].area.Width, arrow[1].area.Height);
                arrow[1].desplaza = 0;
                arrow[1].transparency = 255;
                break;
            case 74:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 4002, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 4002, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].vvscroll = 1;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.400000009f; //lemmings depth 0.300f
                trap[0].transparency = 150;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 76:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(994, 472, 2544 - 994, 40);
                trap[0].areaTrap = new Rectangle(994, 477, 2544 - 994, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvscroll = 2;
                trap[0].R = 140;
                trap[0].G = 120;
                trap[0].B = 190;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 77:
                numTOTexits = 2;
                moreexits = new Varmoreexits[numTOTexits];
                moreexits[0].exitMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[77].exitX, LemmingsNetGame.Instance.Levels.AllLevel[77].exitY);
                moreexits[1].exitMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[77].exitX, 180);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(2475, 472, 2594 - 2475, 40);
                trap[0].areaTrap = new Rectangle(2475, 477, 2594 - 2475, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(606 - 159 + 30, 308 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(606, 308);
                trap[1].vvX = 159;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 81:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(963 - 159 + 30, 358 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(963, 358);
                trap[0].vvX = 159;
                trap[0].vvY = 27;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(487, 358 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(487, 358);
                trap[1].vvX = 0;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 83:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(0, 472, 1281, 40);
                trap[0].areaTrap = new Rectangle(0, 477, 1281, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvscroll = 1;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 84:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(0, 472, 3283, 40);
                trap[0].areaTrap = new Rectangle(0, 477, 3283, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvscroll = 1;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 86:
                numTOTdoors = 3; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[86].doorX, LemmingsNetGame.Instance.Levels.AllLevel[86].doorY);
                moreDoors[1].doorMoreXY = new Vector2(930, 382);
                moreDoors[2].doorMoreXY = new Vector2(500, 56);
                NumTotTraps = 4;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(297, 472, 836 - 297, 512);
                trap[0].areaTrap = new Rectangle(297, 475, 836 - 297, 10);
                trap[0].vvscroll = 1;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(0, 0);
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(930 - 30, 128 - 30 / 2, 30 * 2, 10);//see .pos
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(930, 128);
                trap[1].vvX = 30;
                trap[1].vvY = 30;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(972 - 30, 128 - 30 / 2, 30 * 2, 10);//see .pos
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(972, 128);
                trap[2].vvX = 30;
                trap[2].vvY = 30;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(88, 129 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[3].numFrames = 10;
                trap[3].actFrame = 0;
                trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[3].isOn = false;
                trap[3].pos = new Vector2(88, 129);
                trap[3].vvX = 0;
                trap[3].vvY = 27;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 87:
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 2083, 32); //512-32=480 bottom of the screen
                trap[0].areaTrap = new Rectangle(0, 485, 2083, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.300000009f;
                trap[0].transparency = 230;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/ice_water");
                trap[1].vvscroll = 2;
                trap[1].areaDraw = new Rectangle(102, 126 - 32, 426 - 102, 32);
                trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 170;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[2].vvscroll = 1;
                trap[2].areaDraw = new Rectangle(1640, 126 - 32, 1965 - 1640, 32);
                trap[2].areaTrap = new Rectangle(0, 0, 0, 0);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.6000000011f;
                trap[2].transparency = 170;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 88:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(2425 - 5, 238 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(2425, 238);
                trap[0].vvX = 33;
                trap[0].vvY = 25;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_laser");
                trap[1].vvscroll = 2;
                trap[1].areaDraw = new Rectangle(0, 480, 3517, 32);
                trap[1].areaTrap = new Rectangle(0, 485, 3517, 10);// not necessary
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 89:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(881, 480, 1481 - 881, 32);
                trap[0].areaTrap = new Rectangle(881, 485, 1481 - 881, 10);// not necessary
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 90:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 472, 3207, 40);
                trap[0].areaTrap = new Rectangle(0, 477, 3207, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            // MAYHEM LEVELS MAYHEM /////////////////////////////////////////////////////////////////////
            case 92:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(830, 219 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(830, 219);
                trap[0].vvX = 0;
                trap[0].vvY = 27;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[1].areaDraw = new Rectangle(1318, 472, 1800 - 1318, 40);
                trap[1].areaTrap = new Rectangle(1318, 477, 1800 - 1318, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 93:
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = true;
                arrow[0].area = new Rectangle(754, 143, 860 - 754, 216 - 143);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(862, 129, 1304 - 862, 306 - 129);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1477 - 5, 345 - 5, 10, 10);
                trap[0].numFrames = 15;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1477, 345);
                trap[0].vvX = 47;
                trap[0].vvY = 87;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_marble2_fix");
                trap[1].areaDraw = new Rectangle(1071, 233, 1266 - 1071, 32); // 512-40=462 bottom of the screen
                trap[1].areaTrap = new Rectangle(1071, 238, 1266 - 1071, 10); //normally +5 on Y and 10 of height
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 97:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(127, 480, 3638 - 127, 32); // 512-40=462 bottom of the screen
                trap[0].areaTrap = new Rectangle(127, 485, 3638 - 127, 10); //normally +5 on Y and 10 of height
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1121 - 23, 376 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1121, 376);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 98:
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = false;
                arrow[0].area = new Rectangle(1006, 50, 1166 - 1006, 393 - 50);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(1006, 398, 1105 - 1006, 512 - 398);
                break;
            case 102:
                SteelON = true; numTOTsteel = 7;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(912, 46, 986 - 912, 380 - 46);
                steel[1].area = new Rectangle(605, 231, 1295 - 605, 306 - 231);
                steel[2].area = new Rectangle(449, 153, 676 - 449, 229 - 153);
                steel[3].area = new Rectangle(66, 155, 370 - 66, 228 - 155);
                steel[4].area = new Rectangle(255, 306, 337 - 255, 379 - 306);
                steel[5].area = new Rectangle(104, 303, 180 - 104, 381 - 303);
                steel[6].area = new Rectangle(909, 438, 986 - 909, 512 - 428);
                break;
            case 104:
                SteelON = true; numTOTsteel = 3;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(742, 398, 1566 - 742, 467 - 398);
                steel[1].area = new Rectangle(579, 323, 742 - 579, 440 - 323);
                steel[2].area = new Rectangle(1447, 324, 1651 - 1447, 399 - 324);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(661, 330, 1576 - 661, 40);
                trap[0].areaTrap = new Rectangle(661, 335, 1576 - 661, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                trap[1].vvscroll = 2;
                trap[1].areaDraw = new Rectangle(661, 350, 1576 - 661, 40);
                trap[1].areaTrap = new Rectangle(661, 355, 1576 - 661, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/acid");
                break;
            case 106:
            case 117:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(334 - 23, 303 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(334, 303);
                trap[0].vvX = 32;
                trap[0].vvY = 38;
                trap[0].depth = 0.200000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1484 - 23, 358 - 38 / 2, 23 * 2, 10); //see .pos
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1484, 358);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 107:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(0, 480, 2187, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 2187, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 108:
                numTOTdoors = 4; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[108].doorX, LemmingsNetGame.Instance.Levels.AllLevel[108].doorY);
                moreDoors[1].doorMoreXY = new Vector2(1500, LemmingsNetGame.Instance.Levels.AllLevel[108].doorY);
                moreDoors[2].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[108].doorX, 376);
                moreDoors[3].doorMoreXY = new Vector2(1500, 376);
                break;
            case 113:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(1341, 480, 2508 - 1341, 32);
                trap[0].areaTrap = new Rectangle(1341, 485, 2508 - 1341, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].vvscroll = 1;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 118:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1975 - 5, 285 - 5, 10, 10);
                trap[0].numFrames = 15;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1975, 285);
                trap[0].vvX = 47;
                trap[0].vvY = 87;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_marble2_fix");
                trap[1].areaDraw = new Rectangle(0, 480, 3922, 32); // 512-40=462 bottom of the screen
                trap[1].areaTrap = new Rectangle(0, 485, 3922, 10); //normally +5 on Y and 10 of height
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 120:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[120].doorX, LemmingsNetGame.Instance.Levels.AllLevel[120].doorY);
                moreDoors[1].doorMoreXY = new Vector2(4094, LemmingsNetGame.Instance.Levels.AllLevel[120].doorY);
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1589 - 5, 434 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 15;
                trap[0].actFrame = 0;
                trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1589, 434);
                trap[0].vvX = 16;
                trap[0].vvY = 42;  //38
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(368 - 5, 474 - 5, 10, 10);
                trap[1].numFrames = 12;
                trap[1].actFrame = 0;
                trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(368, 474);
                trap[1].vvX = 40;
                trap[1].vvY = 105;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_10");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(3301 - 5, 453 - 5, 10, 10);
                trap[2].numFrames = 12;
                trap[2].actFrame = 0;
                trap[2].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[2].isOn = false;
                trap[2].pos = new Vector2(3301, 453);
                trap[2].vvX = 40;
                trap[2].vvY = 105;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_10");
                break;
            //bonus levels bonus ////////////////////////////////////////////////////////////////////////////////////////
            case 124:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1341 - 23, 324 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1341, 324);
                trap[0].vvX = 32;
                trap[0].vvY = 38;
                trap[0].depth = 0.200000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(936 - 23, 355 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(936, 355);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 126:
                ArrowsON = true; NumTotArrow = 2;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = false;
                arrow[0].area = new Rectangle(605, 0, 700 - 605, 245);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                arrow[1].right = false;
                arrow[1].area = new Rectangle(952, 0, 1047 - 952, 245); // mask texture full steel zone
                arrow[1].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow2");
                arrow[1].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[1].area.Width, arrow[1].area.Height);
                arrow[1].desplaza = 0;
                arrow[1].transparency = 255;
                break;
            case 127:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 2; // kind of variable scroll the trap 1=z1--
                trap[0].areaDraw = new Rectangle(0, 480, 2148, 32); //512-32=480 bottom of the screen
                trap[0].areaTrap = new Rectangle(0, 485, 2148, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.300000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/ice_water");
                break;
            case 130:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1016 - 5, 213 - 5, 10, 10);//see .pos minus 5 on both axis
                trap[0].numFrames = 37;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1016, 213);
                trap[0].vvX = 10;
                trap[0].vvY = 71;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 132:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 2; // kind of variable scroll the trap 1=z1--
                trap[0].areaDraw = new Rectangle(0, 480, 3909, 32); //512-32=480 bottom of the screen
                trap[0].areaTrap = new Rectangle(0, 485, 3909, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.300000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/ice_water");
                trap[1].vvscroll = 1; // kind of variable scroll the trap 1=z1--
                trap[1].areaDraw = new Rectangle(0, 485, 3909, 32); //512-32=480 bottom of the screen
                trap[1].areaTrap = new Rectangle(0, 490, 3909, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/ice_water");
                break;
            case 133:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 0;
                trap[0].areaDraw = new Rectangle(395, 472, 1647 - 395, 40);
                trap[0].areaTrap = new Rectangle(395, 477, 1647 - 395, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 134:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[134].doorX, LemmingsNetGame.Instance.Levels.AllLevel[134].doorY);
                moreDoors[1].doorMoreXY = new Vector2(160, 92);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1080 - 12, 406 - 18, 15, 36);
                trap[0].numFrames = 7;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1080, 406);
                trap[0].vvX = 30;
                trap[0].vvY = 26;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_left");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(144, 118 - 18, 15, 36);
                trap[1].numFrames = 7;
                trap[1].actFrame = 0;
                trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(144, 118);
                trap[1].vvX = 0;
                trap[1].vvY = 26;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_right");
                break;
            case 136:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(172, 490, 1843 - 172, 32);
                trap[0].areaTrap = new Rectangle(172, 495, 1843 - 172, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].R = 20;
                trap[0].B = 20;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].vvscroll = 2;
                trap[1].areaDraw = new Rectangle(0, 480, 2049, 32);
                trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].R = 20;
                trap[1].B = 20;
                trap[1].transparency = 170;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 137:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1146 - 159 + 30, 235 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1146, 235);
                trap[0].vvX = 159;
                trap[0].vvY = 27;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 138:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[138].doorX, LemmingsNetGame.Instance.Levels.AllLevel[138].doorY);
                moreDoors[1].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[138].doorX - 300, LemmingsNetGame.Instance.Levels.AllLevel[138].doorY);
                //moredoors[1].doormorexy = new Vector2(1110,220); TEST THIS OPTION -- BASHER TO LEFT FAILS??????
                ArrowsON = true; NumTotArrow = 1;
                arrow = new Vararrows[NumTotArrow];
                arrow[0].right = false;
                arrow[0].area = new Rectangle(961, 219, 1011 - 961, 409 - 219);
                arrow[0].flechas = LemmingsNetGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                arrow[0].flechassobre = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                arrow[0].desplaza = 0;
                arrow[0].transparency = 255;
                break;
            case 139:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(2208 - 5, 430 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 15;
                trap[0].actFrame = 0;
                trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(2208, 430);
                trap[0].vvX = 16;
                trap[0].vvY = 42;  //38
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(2843 - 5, 430 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 15;
                trap[1].actFrame = 0;
                trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(2843, 430);
                trap[1].vvX = 16;
                trap[1].vvY = 42;  //38
                trap[1].depth = 0.400000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                break;
            case 140:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(133, 155 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(133, 155);
                trap[0].vvX = 0;
                trap[0].vvY = 27;
                trap[0].depth = 0.400000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 141:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[141].doorX, LemmingsNetGame.Instance.Levels.AllLevel[141].doorY);
                moreDoors[1].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[141].doorX + 400, LemmingsNetGame.Instance.Levels.AllLevel[141].doorY);
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(671 - 23, 184 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(671, 184);
                trap[0].vvX = 32;
                trap[0].vvY = 38;
                trap[0].depth = 0.200000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 143:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 1632, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 1632, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 144:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(134, 326);
                moreDoors[1].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[144].doorX, LemmingsNetGame.Instance.Levels.AllLevel[144].doorY);
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(624 - 23, 461 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(624, 461);
                trap[0].vvX = 32;
                trap[0].vvY = 38;
                trap[0].depth = 0.200000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 145:
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(974 - 23, 229 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(974, 229);
                trap[0].vvX = 32;
                trap[0].vvY = 38;
                trap[0].depth = 0.200000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(110 - 23, 357 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(110, 357);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(970 - 23, 357 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[2].numFrames = 16;
                trap[2].actFrame = 0;
                trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[2].isOn = false;
                trap[2].pos = new Vector2(970, 357);
                trap[2].vvX = 32;
                trap[2].vvY = 38;
                trap[2].depth = 0.200000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 147:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(79, 480, 2489 - 79, 32);
                trap[0].areaTrap = new Rectangle(79, 485, 2489 - 79, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].G = 130;
                trap[0].R = 130;
                trap[0].B = 180;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 200;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 149:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, 1794, 32);
                trap[0].areaTrap = new Rectangle(0, 485, 1794, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1022 - 23, 301 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1022, 301);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 150:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(690 - 5, 467 - 5, 10, 10);//see .pos minus 5 on both axis
                trap[0].numFrames = 37;
                trap[0].actFrame = 0;
                trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(690, 467);
                trap[0].vvX = 10;
                trap[0].vvY = 71;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_soga");

                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1328 - 5, 427 - 5, 10, 10);//see .pos minus 5 on both axis
                trap[1].numFrames = 37;
                trap[1].actFrame = 0;
                trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1328, 427);
                trap[1].vvX = 10;
                trap[1].vvY = 71;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 151:
                NumTotTraps = 3;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 472, 2177, 512);
                trap[0].areaTrap = new Rectangle(0, 475, 2177, 10);
                trap[0].vvscroll = 1;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(0, 0);
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1113 - 30, 225 - 30 / 2, 30 * 2, 10);//see .pos
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1113, 225);
                trap[1].vvX = 30;
                trap[1].vvY = 30;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(1163 - 30, 225 - 30 / 2, 30 * 2, 10);//see .pos
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(1163, 225);
                trap[2].vvX = 30;
                trap[2].vvY = 30;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 152:
                NumTotTraps = 4;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 472, 1493, 512);
                trap[0].areaTrap = new Rectangle(0, 475, 1493, 10);
                trap[0].vvscroll = 2;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[0].isOn = false;
                trap[0].pos = new Vector2(0, 0);
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(990 - 30, 336 - 30 / 2, 30 * 2, 10);//see .pos
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[1].isOn = false;
                trap[1].pos = new Vector2(990, 336);
                trap[1].vvX = 30;
                trap[1].vvY = 30;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(1035 - 30, 336 - 30 / 2, 30 * 2, 10);//see .pos
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                trap[2].isOn = false;
                trap[2].pos = new Vector2(1035, 336);
                trap[2].vvX = 30;
                trap[2].vvY = 30;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(638 - 159 + 30, 24 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[3].numFrames = 10;
                trap[3].actFrame = 0;
                trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[3].isOn = false;
                trap[3].pos = new Vector2(638, 24);
                trap[3].vvX = 159;
                trap[3].vvY = 27;
                trap[3].depth = 0.400000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 153:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 490, 1158, 32); //size smaller to fit
                trap[0].areaTrap = new Rectangle(0, 492, 1158, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.6000000011f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(883 - 23, 396 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(883, 396);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.200000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 154:
                NumTotTraps = 9;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(459 - 23, 231 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[0].numFrames = 16;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(459, 231);
                trap[0].vvX = 32;
                trap[0].vvY = 38;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(935 - 23, 182 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[1].numFrames = 16;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(935, 182);
                trap[1].vvX = 32;
                trap[1].vvY = 38;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[2].areaTrap = new Rectangle(1574 - 23, 129 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[2].numFrames = 16;
                trap[2].actFrame = 0;
                trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[2].isOn = false;
                trap[2].pos = new Vector2(1574, 129);
                trap[2].vvX = 32;
                trap[2].vvY = 38;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[3].areaTrap = new Rectangle(2095 - 23, 21 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[3].numFrames = 16;
                trap[3].actFrame = 0;
                trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[3].isOn = false;
                trap[3].pos = new Vector2(2095, 21);
                trap[3].vvX = 32;
                trap[3].vvY = 38;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[4].areaTrap = new Rectangle(40 - 23, 255 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[4].numFrames = 16;
                trap[4].actFrame = 0;
                trap[4].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[4].isOn = false;
                trap[4].pos = new Vector2(40, 255);
                trap[4].vvX = 32;
                trap[4].vvY = 38;
                trap[4].depth = 0.600000009f;
                trap[4].transparency = 255;
                trap[4].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[5].areaTrap = new Rectangle(507 - 23, 52 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[5].numFrames = 16;
                trap[5].actFrame = 0;
                trap[5].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[5].isOn = false;
                trap[5].pos = new Vector2(507, 52);
                trap[5].vvX = 32;
                trap[5].vvY = 38;
                trap[5].depth = 0.600000009f;
                trap[5].transparency = 255;
                trap[5].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[6].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[6].areaTrap = new Rectangle(1272 - 23, 205 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[6].numFrames = 16;
                trap[6].actFrame = 0;
                trap[6].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[6].isOn = false;
                trap[6].pos = new Vector2(1272, 205);
                trap[6].vvX = 32;
                trap[6].vvY = 38;
                trap[6].depth = 0.600000009f;
                trap[6].transparency = 255;
                trap[6].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[7].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[7].areaTrap = new Rectangle(1963 - 23, 307 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[7].numFrames = 16;
                trap[7].actFrame = 0;
                trap[7].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[7].isOn = false;
                trap[7].pos = new Vector2(1963, 307);
                trap[7].vvX = 32;
                trap[7].vvY = 38;
                trap[7].depth = 0.600000009f;
                trap[7].transparency = 255;
                trap[7].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                trap[8].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[8].areaTrap = new Rectangle(2192 - 23, 460 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                trap[8].numFrames = 16;
                trap[8].actFrame = 0;
                trap[8].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                trap[8].isOn = false;
                trap[8].pos = new Vector2(2192, 460);
                trap[8].vvX = 32;
                trap[8].vvY = 38;
                trap[8].depth = 0.600000009f;
                trap[8].transparency = 255;
                trap[8].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 159:
                numTOTdoors = 4; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[159].doorX, LemmingsNetGame.Instance.Levels.AllLevel[159].doorY);
                moreDoors[1].doorMoreXY = new Vector2(559, 131);
                moreDoors[2].doorMoreXY = new Vector2(96, 327);
                moreDoors[3].doorMoreXY = new Vector2(1033, 419);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(1172 - 5, 381 - 5, 10, 10); //normally +5 on Y and 10 of height
                trap[0].numFrames = 9;
                trap[0].actFrame = 0;
                trap[0].type = 666;
                trap[0].isOn = false;
                trap[0].pos = new Vector2(1172, 381);
                trap[0].vvX = 32;
                trap[0].vvY = 48;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_almeja");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1176 - 5, 269 - 5, 10, 10); //normally +5 on Y and 10 of height
                trap[1].numFrames = 29;
                trap[1].actFrame = 0;
                trap[1].type = 666;
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1176, 269);
                trap[1].vvX = 32;
                trap[1].vvY = 120;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_bombona");
                Sprite = new Varsprites[2];
                Sprite[0].actFrame = 0;
                Sprite[0].axisX = 1;
                Sprite[0].axisY = 1;
                Sprite[0].depth = 0.20888886f;
                Sprite[0].R = 255;
                Sprite[0].G = 255;
                Sprite[0].B = 255;
                Sprite[0].transparency = 200;
                Sprite[0].pos = new Vector2(100, 100);
                Sprite[0].scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[0].rotation = 0f;
                Sprite[0].framesecond = 0;
                Sprite[0].frame = 0;
                Sprite[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("sprite/nube1");
                Sprite[0].minusScrollx = true;
                Sprite[0].typescroll = 3f;
                Sprite[1].actFrame = 0;
                Sprite[1].axisX = 1;
                Sprite[1].axisY = 1;
                Sprite[1].depth = 0.28888805f;
                Sprite[1].R = 255;
                Sprite[1].G = 225;
                Sprite[1].B = 225;
                Sprite[1].transparency = 200;
                Sprite[1].pos = new Vector2(300, 300);
                Sprite[1].scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[1].rotation = 0f;
                Sprite[1].framesecond = 0;
                Sprite[1].frame = 0;
                Sprite[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("sprite/nube2");
                Sprite[1].minusScrollx = true;
                Sprite[1].typescroll = 2;
                break;
            case 160:
                numTOTdoors = 2; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[160].doorX, LemmingsNetGame.Instance.Levels.AllLevel[160].doorY);
                moreDoors[1].doorMoreXY = new Vector2(1280, 462);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[0].areaTrap = new Rectangle(357, 333 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[0].numFrames = 10;
                trap[0].actFrame = 0;
                trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[0].isOn = false;
                trap[0].pos = new Vector2(357, 333);
                trap[0].vvX = 0;
                trap[0].vvY = 27;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(64 - 159 + 30, 333 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                trap[1].numFrames = 10;
                trap[1].actFrame = 0;
                trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                trap[1].isOn = false;
                trap[1].pos = new Vector2(64, 333);
                trap[1].vvX = 159;
                trap[1].vvY = 27;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 161:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(892, 862, 1120 - 892, 32);
                trap[0].areaTrap = new Rectangle(892, 867, 1120 - 892, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                trap[1].areaTrap = new Rectangle(1451 - 5, 469 - 5, 10, 10); //normally +5 on Y and 10 of height
                trap[1].numFrames = 29;
                trap[1].actFrame = 0;
                trap[1].type = 666;
                trap[1].isOn = false;
                trap[1].pos = new Vector2(1451, 469);
                trap[1].vvX = 32;
                trap[1].vvY = 120;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/dead_bombona");
                break;
            case 162:
                numTOTdoors = 3; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[162].doorX, LemmingsNetGame.Instance.Levels.AllLevel[162].doorY);
                moreDoors[1].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[162].doorX + 180, LemmingsNetGame.Instance.Levels.AllLevel[162].doorY);
                moreDoors[2].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[162].doorX, 345);
                SteelON = true; numTOTsteel = 2;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(458, 0, 501 - 458, 319);
                steel[1].area = new Rectangle(145, 269, 277 - 145, 320 - 269);
                break;
            case 163:
                numTOTdoors = 3;
                numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[163].doorX, LemmingsNetGame.Instance.Levels.AllLevel[163].doorY);
                moreDoors[1].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[163].doorX, 220);
                moreDoors[2].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[163].doorX, 382);
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].vvscroll = 1;
                trap[0].areaDraw = new Rectangle(853, 504, 1932 - 853, 32);
                trap[0].areaTrap = new Rectangle(853, 509, 1932 - 853, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 170;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(624, 483 - 32, 672 - 624, 32);
                trap[1].areaTrap = new Rectangle(624, 483 - 27, 672 - 624, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 170;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                Sprite = new Varsprites[3];
                Sprite[0].actFrame = 0;
                Sprite[0].axisX = 1;
                Sprite[0].axisY = 7;
                Sprite[0].depth = 0.406f;
                Sprite[0].R = 255;
                Sprite[0].G = 255;
                Sprite[0].B = 255;
                Sprite[0].transparency = 255;
                Sprite[0].pos = new Vector2(404, 295); //340
                Sprite[0].scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[0].rotation = 0f;
                Sprite[0].framesecond = 4;
                Sprite[0].frame = 0;
                Sprite[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("antorcha_l2");
                Sprite[0].minusScrollx = true;
                Sprite[1].actFrame = 0;
                Sprite[1].axisX = 1;
                Sprite[1].axisY = 7;
                Sprite[1].depth = 0.406f;
                Sprite[1].R = 255;
                Sprite[1].G = 255;
                Sprite[1].B = 255;
                Sprite[1].transparency = 255;
                Sprite[1].pos = new Vector2(1615, 387); //340
                Sprite[1].scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[1].rotation = 0f;
                Sprite[1].framesecond = 2;
                Sprite[1].frame = 0;
                Sprite[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("antorcha_l2");
                Sprite[1].minusScrollx = true;
                Sprite[2].actFrame = 0;
                Sprite[2].axisX = 1;
                Sprite[2].axisY = 7;
                Sprite[2].depth = 0.405f;
                Sprite[2].R = 255;
                Sprite[2].G = 225;
                Sprite[2].B = 225;
                Sprite[2].transparency = 255;
                Sprite[2].pos = new Vector2(1095, 92);
                Sprite[2].scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[2].rotation = 0f;
                Sprite[2].framesecond = 6;
                Sprite[2].frame = 0;
                Sprite[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("antorcha_l2");
                Sprite[2].minusScrollx = true;
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(315, 270, 356 - 315, 320 - 270);
                break;
            case 165:
                NumTotTraps = 1;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 480, MyGame.GameResolution.X, 32);
                trap[0].areaTrap = new Rectangle(0, 485, MyGame.GameResolution.X, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                SteelON = true; numTOTsteel = 2;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(511, 242, 552 - 511, 346 - 242);
                steel[1].area = new Rectangle(735, 243, 776 - 735, 291 - 243);
                break;
            case 166:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 485, 1882, 32);
                trap[0].areaTrap = new Rectangle(0, 490, 1882, 10);
                trap[0].vvscroll = 1;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 504, 1882, 32);
                trap[1].areaTrap = new Rectangle(0, 509, 1882, 10);
                trap[1].vvscroll = 2;
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                SteelON = true; numTOTsteel = 1;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(224, 321, 1835 - 224, 373 - 321);
                break;
            case 167:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(580, 456 - 32, 806 - 580, 32);
                trap[0].areaTrap = new Rectangle(580, 456 - 27, 806 - 580, 10);
                trap[0].vvscroll = 1;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600000009f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(1209, 536 - 32, 1391 - 1209, 32);
                trap[1].areaTrap = new Rectangle(1209, 536 - 27, 1391 - 1209, 10);
                trap[1].vvscroll = 2;
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                SteelON = true; numTOTsteel = 5;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(539, 55, 581 - 539, 480 - 55);
                steel[1].area = new Rectangle(808, 268, 848 - 808, 479 - 268);
                steel[2].area = new Rectangle(1165, 376, 1208 - 1165, 536 - 376);
                steel[3].area = new Rectangle(1389, 324, 1432 - 1389, 536 - 324);
                steel[4].area = new Rectangle(270, 430, 539 - 270, 479 - 430);
                break;
            case 168:
                SteelON = true; numTOTsteel = 3;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(1389, 81, 1429 - 1389, 454 - 81);
                steel[1].area = new Rectangle(1432, 215, 1565 - 1432, 262 - 215);
                steel[2].area = new Rectangle(1702, 188, 2281 - 1702, 239 - 188);
                break;
            case 169:
                SteelON = true; numTOTsteel = 6;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(1479, 107, 1655 - 1479, 212 - 107);
                steel[1].area = new Rectangle(1434, 162, 1476 - 1434, 266 - 162);
                steel[2].area = new Rectangle(1390, 215, 1431 - 1390, 319 - 215);
                steel[3].area = new Rectangle(1345, 270, 1386 - 1345, 424 - 270);
                steel[4].area = new Rectangle(1300, 322, 1342 - 1300, 373 - 322);
                steel[5].area = new Rectangle(1165, 482, 1655 - 1165, 532 - 482);
                NumTotTraps = 4;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(0, 515 - 32, 3091, 32);
                trap[0].vvscroll = 1;
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600005f;
                trap[0].transparency = 255;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(0, 536 - 32, 3091, 32);
                trap[1].areaTrap = new Rectangle(0, 536 - 27, 3091, 10);
                trap[1].vvscroll = 2;
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600000009f;
                trap[1].transparency = 255;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[2].areaDraw = new Rectangle(1028, 483 - 32, 1165 - 1028, 32);
                trap[2].numFrames = 8;
                trap[2].actFrame = 0;
                trap[2].type = 1;
                trap[2].isOn = false;
                trap[2].pos = Vector2.Zero;
                trap[2].vvX = 0;
                trap[2].vvY = 0;
                trap[2].depth = 0.600000009f;
                trap[2].transparency = 255;
                trap[2].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[3].areaDraw = new Rectangle(1387, 375 - 32, 1613 - 1387, 32);
                trap[3].numFrames = 8;
                trap[3].actFrame = 0;
                trap[3].type = 1;
                trap[3].isOn = false;
                trap[3].pos = Vector2.Zero;
                trap[3].vvX = 0;
                trap[3].vvY = 0;
                trap[3].depth = 0.600000009f;
                trap[3].transparency = 255;
                trap[3].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 170:
                NumTotTraps = 2;
                TrapsON = true;
                trap = new Vartraps[NumTotTraps];
                trap[0].areaDraw = new Rectangle(402, 447 - 32, 987 - 402, 32);
                trap[0].areaTrap = new Rectangle(402, 447 - 27, 987 - 402, 10);
                trap[0].numFrames = 8;
                trap[0].actFrame = 0;
                trap[0].type = 1;
                trap[0].isOn = false;
                trap[0].pos = Vector2.Zero;
                trap[0].vvX = 0;
                trap[0].vvY = 0;
                trap[0].depth = 0.600005f;
                trap[0].transparency = 200;
                trap[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                trap[1].areaDraw = new Rectangle(222, 178 - 32, 583 - 222, 32);
                trap[1].areaTrap = new Rectangle(222, 178 - 27, 583 - 222, 10);
                trap[1].numFrames = 8;
                trap[1].actFrame = 0;
                trap[1].type = 1;
                trap[1].isOn = false;
                trap[1].pos = Vector2.Zero;
                trap[1].vvX = 0;
                trap[1].vvY = 0;
                trap[1].depth = 0.600005f;
                trap[1].transparency = 190;
                trap[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 171:
                numTOTplats = 2; PlatsON = true;
                plats = new Varplat[numTOTplats];
                plats[0].actStep = 0;
                plats[0].framesecond = 8;
                plats[0].frame = 0;
                plats[0].numSteps = 22;
                plats[0].up = true;
                plats[0].step = 5;
                plats[0].areaDraw = new Rectangle(710, 1110, 220, 60);
                plats[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/elevator");
                plats[1].actStep = 0;
                plats[1].framesecond = 0;
                plats[1].frame = 0;
                plats[1].numSteps = 30;
                plats[1].up = true;
                plats[1].step = 1;
                plats[1].areaDraw = new Rectangle(528, 1216, 200, 35);
                plats[1].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/elevator");
                AddsON = true;
                adds = new Varadds[1];
                adds[0].areaDraw = new Rectangle(250, 1271, 100, 50); //y 110 orig
                adds[0].numFrames = 8;
                adds[0].actFrame = 0;
                adds[0].frame = 0;
                adds[0].framesecond = 2;
                adds[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("traps/water_blue");
                break;
            case 172:
                SteelON = true; numTOTsteel = 26;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(464 + 16, 82 + 16, 534 - 464, 160 - 82);  // add +16 on x & y only first for mouse sprite fix on "F1"
                steel[1].area = new Rectangle(608 + 16, 109 + 16, 676 - 608, 185 - 109);
                steel[2].area = new Rectangle(370 + 16, 490 + 16, 745 - 370, 495 - 490);
                steel[3].area = new Rectangle(471 + 16, 437 + 16, 539 - 471, 491 - 437);
                steel[4].area = new Rectangle(349 + 16, 286 + 16, 414 - 349, 364 - 286);
                steel[5].area = new Rectangle(462 + 16, 222 + 16, 528 - 462, 299 - 222);
                steel[6].area = new Rectangle(303 + 16, 150 + 16, 372 - 303, 228 - 150);
                steel[7].area = new Rectangle(709 + 16, 329 + 16, 911 - 709, 410 - 329);
                steel[8].area = new Rectangle(675 + 16, 465 + 16, 777 - 675, 488 - 465);
                steel[9].area = new Rectangle(169 + 16, 249 + 16, 268 - 169, 407 - 249);
                steel[10].area = new Rectangle(172 + 16, 117 + 16, 234 - 172, 247 - 117);
                steel[11].area = new Rectangle(830 + 16, 71 + 16, 965 - 830, 120 - 71);
                steel[12].area = new Rectangle(594 + 16, 267 + 16, 627 - 594, 374 - 267);
                steel[13].area = new Rectangle(813 + 16, 278 + 16, 941 - 813, 326 - 278);
                steel[14].area = new Rectangle(763 + 16, 148 + 16, 832 - 763, 197 - 148);
                steel[15].area = new Rectangle(797 + 16, 121 + 16, 865 - 797, 146 - 121);
                steel[16].area = new Rectangle(526 + 16, 349 + 16, 593 - 526, 401 - 349);
                steel[17].area = new Rectangle(731 + 16, 179 + 16, 763 - 731, 230 - 179);
                steel[18].area = new Rectangle(698 + 16, 214 + 16, 730 - 698, 264 - 214);
                steel[19].area = new Rectangle(663 + 16, 228 + 16, 699 - 663, 277 - 228);
                steel[20].area = new Rectangle(628 + 16, 241 + 16, 668 - 628, 290 - 241);
                steel[21].area = new Rectangle(561 + 16, 322 + 16, 596 - 561, 347 - 322);
                steel[22].area = new Rectangle(271 + 16, 437 + 16, 403 - 271, 489 - 437);
                steel[23].area = new Rectangle(268 + 16, 356 + 16, 303 - 268, 462 - 356);
                steel[24].area = new Rectangle(745 + 16, 411 + 16, 814 - 745, 461 - 411);
                steel[25].area = new Rectangle(302 + 16, 410 + 16, 336 - 302, 436 - 410);
                break;
            case 173:
                SteelON = true; numTOTsteel = 3;
                steel = new Varsteel[numTOTsteel];
                steel[0].area = new Rectangle(1506 + 16, 176 + 16, 1571 - 1506, 496 - 176);  // add +16 on x & y only first for mouse sprite fix on "F1"
                steel[1].area = new Rectangle(2526 + 16, 153 + 16, 2592 - 2526, 402 - 153);
                break;
            case 177:
                Sprite = new Varsprites[1];
                Sprite[0].calc = true;
                Sprite[0].actFrame = 0;
                Sprite[0].axisX = 6;
                Sprite[0].axisY = 1;
                Sprite[0].depth = 0.405f;
                Sprite[0].R = 255;
                Sprite[0].G = 225;
                Sprite[0].B = 225;
                Sprite[0].transparency = 255;
                Sprite[0].pos = new Vector2(0, 0);
                Sprite[0].scale = 0.5f; //1f->normal size -- 0.5f->half size -- etc.
                Sprite[0].rotation = 0f;
                Sprite[0].framesecond = 2;
                Sprite[0].frame = 0;
                Sprite[0].sprite = LemmingsNetGame.Instance.Content.Load<Texture2D>("touch/arana");
                Sprite[0].minusScrollx = true;
                Sprite[0].dest = new Vector2(0, 0);
                Sprite[0].speed = 0.578f;  // this field is important for move logic of sprites != 0
                Sprite[0].actVect = 0;
                Sprite[0].center.X = ((Sprite[0].sprite.Width / Sprite[0].axisX) / 2);
                Sprite[0].center.Y = ((Sprite[0].sprite.Height / Sprite[0].axisY) / 2);
                Sprite[0].path = new Vector3[6];
                Sprite[0].path[0] = new Vector3(402 - 20, 109 + 50, 1.5f);
                Sprite[0].path[1] = new Vector3(424 - 20, 231 + 50, 0.3f);
                Sprite[0].path[2] = new Vector3(461 - 20, 230 + 50, 1.9f);
                Sprite[0].path[3] = new Vector3(462 - 20, 164 + 50, 1.6f);
                Sprite[0].path[4] = new Vector3(525 - 20, 162 + 50, 0.7f);
                Sprite[0].path[5] = new Vector3(525 - 20, 280 + 50, 1.2f);
                break;
            case 179:
                numTOTdoors = 3; numACTdoor = 0;
                moreDoors = new Varmoredoors[numTOTdoors];
                moreDoors[0].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[179].doorX, LemmingsNetGame.Instance.Levels.AllLevel[179].doorY);
                moreDoors[1].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[179].doorX + 100, LemmingsNetGame.Instance.Levels.AllLevel[179].doorY + 160);
                moreDoors[2].doorMoreXY = new Vector2(LemmingsNetGame.Instance.Levels.AllLevel[179].doorX + 190, LemmingsNetGame.Instance.Levels.AllLevel[179].doorY + 330);
                break;
            default:
                ArrowsON = false;
                TrapsON = false;
                SteelON = false;
                PlatsON = false;
                AddsON = false;
                break;
        }

    }
}
