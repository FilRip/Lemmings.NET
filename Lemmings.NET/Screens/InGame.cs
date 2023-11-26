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

namespace Lemmings.NET.Screens
{
    internal class InGame
    {
        private double actWaves444 = 0, actWaves333 = 0, actWaves = 0;
        private bool Exploding, dibuja3, LevelEnded, ExitBad, ExitLevel;
        private int numSaved, actItem, Iexplo, cantidad22;
        public int numTOTsteel = 0;
        public int numLemmings { get; set; } = 0;
        private int sx, rest = 0, numTOTdoors = 1, numACTdoor = 0, numTOTexits = 1, framesale, numTOTplats = 0, Contador2, Contador = 1, actLEM2;
        public float Contadortime2, Contadortime;
        public int ScrollX { get; set; }
        public int ScrollY { get; set; }
        private bool mouseOnLem = false;
        private bool doorOn = true;
        public Lem[] lemming { get; set; }
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
        public bool _allBlow { get; set; } = false;
        private int door1X, door1Y, actLEM;
        private int output1X, output1Y, ex11;
        private int frameDoor, frameExit; // 0--10   0--6
        private int exitFrame = 999, actualBlow; // frecuency lemmings go in
        private Rectangle exit_rect; // rectangle exit
        private Point x;
        internal double millisecondsElapsed { get; set; }
        internal Texture2D earth { get; set; }
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
        public int zvTime { get; set; } = 0;
        private Vector2 vectorFill, vectorFill2;
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
        public bool dibuja { get; set; } = true;
        private bool luzmas = true, luzmas2 = true, draw_walker = false, draw_builder = false;
        public bool draw2 { get; set; } = true;
        public double totalTime { get; set; }
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

        internal Varlev[] AllLevel { get; set; } = new Varlev[MyGame.NumTotalLevels]; // Number of levels for now...
        internal readonly bool[] LevelEnd = new bool[MyGame.NumTotalLevels]; //full number of levels to see which are finished or not
        readonly Door[] varDoor = new Door[9];
        readonly Exit[] varExit = new Exit[11];

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
            millisecondsElapsed = 0;
            numLemmings = 0;
            puerta_ani = content.Load<Texture2D>("puerta" + string.Format("{0}", AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor)); // type of door puerta1-2-3-4 etc.
            string xx455 = string.Format("{0}", AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit);
            salida_ani1 = content.Load<Texture2D>("salida" + xx455);
            salida_ani1_1 = content.Load<Texture2D>("salida" + xx455 + "_1");
            sale = content.Load<Texture2D>("sale");
            LemmingsNetGame.Instance.CurrentLevelNumber = newLevel;
            LemSkill = "";
            MyGame.Paused = false;
            zvTime = 0;
            _allBlow = false;
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

            Texture2D level = LemmingsNetGame.Instance.Content.Load<Texture2D>(AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].NameLev);
            earth = new Texture2D(LemmingsNetGame.Instance.GraphicsDevice, level.Width, level.Height);
            Color[] pixels = new Color[level.Width * level.Height];
            level.GetData(pixels);
            earth.SetData(pixels);
            earth.GetData(C25, 0, earth.Height * earth.Width); //better here than moverlemming() for performance see issues 
                                                               //see differences with old getdata, see size important (x * y)
            door1X = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].doorX;
            door1Y = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].doorY;
            output1X = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].exitX;
            output1Y = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].exitY;
            // this is the depth of the exit and doors animated sprites -- See level 58 the exit is behind the mountain (0.6f)
            if (AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].DoorExitDepth != 0)
            {
                DoorExitDepth = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].DoorExitDepth;
            }
            else
            {
                DoorExitDepth = 0.403f;
            }
            _nbClimberRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberClimbers;
            _nbFloaterRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberUmbrellas;
            _nbExploderRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberExploders;
            _nbBlockerRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBlockers;
            _nbBuilderRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBuilders;
            _nbBasherRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberBashers;
            _nbMinerRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberMiners;
            _nbDiggerRemaining = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].numberDiggers;
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
            Numlems = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TotalLemmings;
            Lemsneeded = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].NbLemmingsToSave;
            ScrollX = AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].InitPosX;
            ScrollY = 0;
            lemming = new Lem[Numlems];
            VariablesTraps();
        }

        private void Update_level()
        {
            builder_frame++;
            walker_frame++;
            frameWaves++;
            Frame2++;
            Frame3++;
            dibuja = false;
            draw2 = false;
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
                dibuja = true;
                if (!MyGame.Paused)
                    Frame++;
            } //without this Frame affects door speed exit
            if (Frame3 > Framesecond2)
            {
                Frame3 = 0;
                draw2 = true;
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
                                    C25[((positioYOrig - (alto - y55)) * earth.Width) + x55 + px] = Color.White;
                                }
                                else
                                {
                                    C25[((positioYOrig - (alto - y55)) * earth.Width) + x55 + px] = Color.Transparent;

                                }
                            }
                        }
                        if (LemmingsNetGame.Instance.DebugOsd.debug)
                            earth.SetData(C25, 0, earth.Width * earth.Height); //set this only for debugger and see the real c25 redraw
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
                earth.SetData(0, rectangleFill, Colormask22, 0, amount);
                int py = adds[0].areaDraw.Y;
                int px = adds[0].areaDraw.X;
                int cantidad99 = 0;
                for (int yy99 = 0; yy99 < startposy; yy99++)
                {
                    int yypos99 = (yy99 + py) * earth.Width;
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
            if (TrapsON && dibuja && !MyGame.Paused)
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
            totalTime = Contadortime / 60; //real time of the level see to stop when finish or zvtime<0
            if (doorOn)
            {
                Contadortime = 0;
                totalTime = 0;
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
            if (dibuja)
            {
                int xx66 = varExit[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].numFram - 1;
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

            if (dibuja && NumTotArrow > 0) // dibuja or dibuja2 test performance-- this is the worst part of the code NEED OPTIMIZATION
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
                        int yypos888 = (yy88 + py) * earth.Width;
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
            for (actLEM = 0; actLEM < numLemmings; actLEM++)
            {
                if (doorOn)
                    break; // start when door finish opening
                if (lemming[actLEM].Dead)
                    continue;
                // LOGIC BLOCKER BLOCKER BLOQUEO LOGIC bbbbbbbbbbbbbbbbbbbbllllllllloooooooccccccccccckkkkkkkkkkkeeeeeeeeeeeeedddddddddddddddddd
                int medx = 14;
                int medy = 14;
                for (int b = 0; b < numLemmings; b++)
                {
                    if (lemming[b].Blocker && b != actLEM)
                    {
                        bloqueo.X = lemming[b].PosX;
                        bloqueo.Y = lemming[b].PosY;
                        bloqueo.Width = 28;
                        bloqueo.Height = 28;
                        if (lemming[actLEM].Miner)
                        {
                            bloqueo.X = lemming[b].PosX + 10;
                            bloqueo.Y = lemming[b].PosY;
                            bloqueo.Width = 9;
                            bloqueo.Height = 28;
                        }
                        poslem.X = lemming[actLEM].PosX + medx;
                        poslem.Y = lemming[actLEM].PosY + medy;
                        if (bloqueo.Contains(poslem))
                        {
                            if (lemming[actLEM].Right)
                            {
                                if (lemming[actLEM].PosX < lemming[b].PosX)
                                {
                                    lemming[actLEM].Right = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (lemming[actLEM].PosX > lemming[b].PosX - 1)
                                {
                                    lemming[actLEM].Right = true;
                                    break;
                                }
                            }
                            break;
                        }
                    }

                }
                lemming[actLEM].Onmouse = false; //LEMMING SKILL STRING MOUSE ON
                if ((Input.CurrentMouseState.X + 16 >= lemming[actLEM].PosX - ScrollX && Input.CurrentMouseState.X + 16 <= lemming[actLEM].PosX - ScrollX + 28
                        && Input.CurrentMouseState.Y + 16 >= lemming[actLEM].PosY - ScrollY && Input.CurrentMouseState.Y + 16 <= lemming[actLEM].PosY + 28 - ScrollY) && !mouseOnLem)
                {
                    if (lemming[actLEM].Walker)
                        LemSkill = "Walker";
                    if (lemming[actLEM].Builder)
                        LemSkill = "Builder";
                    if (lemming[actLEM].Basher)
                        LemSkill = "Basher";
                    if (lemming[actLEM].Blocker)
                        LemSkill = "Blocker";
                    if (lemming[actLEM].Miner)
                        LemSkill = "Miner";
                    if (lemming[actLEM].Digger)
                        LemSkill = "Digger";
                    if (lemming[actLEM].Climber)
                        LemSkill += ",C";
                    if (lemming[actLEM].Umbrella)
                        LemSkill += ",F";
                    if (lemming[actLEM].Climbing)
                        LemSkill = "Climber";
                    if (lemming[actLEM].Climbing && lemming[actLEM].Umbrella)
                        LemSkill = "Climber,F";
                    if ((lemming[actLEM].Fall || lemming[actLEM].Falling) && !lemming[actLEM].Umbrella)
                        LemSkill = "Faller";
                    if ((lemming[actLEM].Fall || lemming[actLEM].Falling) && lemming[actLEM].Umbrella)
                        LemSkill = "Floater";
                    mouseOnLem = true;
                    lemming[actLEM].Onmouse = true;
                } //  inside the mouse rectangle lemming ON
                if (TrapsON && !MyGame.Paused) //Traps logic and sounds
                {
                    for (int ti = 0; ti < NumTotTraps; ti++)
                    {
                        x.X = lemming[actLEM].PosX + 14;
                        x.Y = lemming[actLEM].PosY + 25;
                        if (trap[ti].areaTrap.Contains(x) && !trap[ti].isOn && trap[ti].type == 666)
                        {
                            trap[ti].isOn = true;
                            lemming[actLEM].Active = false;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Dead = true;
                            numlemnow--;
                            lemming[actLEM].Explode = false;
                            lemming[actLEM].Exploser = false;
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
                        rectangleFill.X = lemming[actLEM].PosX + 14;
                        rectangleFill.Y = lemming[actLEM].PosY;
                        rectangleFill.Width = 1;
                        rectangleFill.Height = 28;
                        if (trap[ti].areaTrap.Intersects(rectangleFill) && !lemming[actLEM].Burned && !lemming[actLEM].Drown && trap[ti].type != 666)
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
                                    lemming[actLEM].Burned = true;
                                    lemming[actLEM].Drown = false;
                                    lemming[actLEM].Explode = false;
                                    lemming[actLEM].Exploser = false;
                                    lemming[actLEM].Numframes = 14;
                                    lemming[actLEM].Actualframe = 0;
                                    lemming[actLEM].Active = false;
                                    lemming[actLEM].Walker = false;
                                    lemming[actLEM].Falling = false;
                                    lemming[actLEM].Fall = false;
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
                                    lemming[actLEM].Drown = true;
                                    lemming[actLEM].Burned = false;
                                    lemming[actLEM].Explode = false;
                                    lemming[actLEM].Exploser = false;
                                    lemming[actLEM].Falling = false;
                                    lemming[actLEM].Fall = false;
                                    lemming[actLEM].Numframes = SizeSprites.water_frames;
                                    lemming[actLEM].Actualframe = 0;
                                    lemming[actLEM].Active = false;
                                    lemming[actLEM].Walker = false;
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
                                    lemming[actLEM].Explode = false;
                                    lemming[actLEM].Exploser = false;
                                    lemming[actLEM].Active = false;
                                    lemming[actLEM].Walker = false;
                                    lemming[actLEM].Dead = true;
                                    numlemnow--;
                                    break;
                            }
                        }
                    }
                }
                // assign skills to lemmings //////////////////////////////////////////////
                if (mouseOnLem && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
                {
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.DIGGER && !lemming[actLEM].Digger && lemming[actLEM].Onmouse //DIGGER
                        && (lemming[actLEM].Walker || lemming[actLEM].Builder || lemming[actLEM].Basher || lemming[actLEM].Miner))
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
                            lemming[actLEM].Digger = true;
                            lemming[actLEM].Fall = false;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.digger_frames;
                            continue;
                        }
                    }
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.CLIMBER && lemming[actLEM].Onmouse && !lemming[actLEM].Climber) //CLIMBER
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
                            lemming[actLEM].Climber = true;
                            continue;
                        }
                    }
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.FLOATER && lemming[actLEM].Onmouse && !lemming[actLEM].Umbrella && !lemming[actLEM].Breakfloor) //FLOATER
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
                            lemming[actLEM].Umbrella = true;
                            continue;
                        }
                    }
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.EXPLODER && lemming[actLEM].Onmouse && !lemming[actLEM].Exploser) //BOMBER
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
                            lemming[actLEM].Exploser = true;
                            continue;
                        }
                    }
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.BLOCKER && lemming[actLEM].Onmouse && !lemming[actLEM].Blocker //BLOCKER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Builder || lemming[actLEM].Basher || lemming[actLEM].Miner))
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
                            lemming[actLEM].Blocker = true;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.blocker_frames;
                            continue;
                        }
                    }
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.BUILDER && lemming[actLEM].Onmouse && !lemming[actLEM].Builder //BUILDER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Basher || lemming[actLEM].Miner || lemming[actLEM].Bridge))
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
                            lemming[actLEM].Bridge = false;
                            lemming[actLEM].Builder = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Numstairs = 0;
                            lemming[actLEM].Numframes = SizeSprites.builder_frames;
                            continue;
                        }
                    }
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.BASHER && lemming[actLEM].Onmouse && !lemming[actLEM].Basher //BASHER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Builder || lemming[actLEM].Miner))
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
                            lemming[actLEM].Basher = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Numframes = SizeSprites.basher_frames;
                            continue;
                        }
                    }
                    if (_inGameMenu.CurrentSelectedSkill == ECurrentSkill.MINER && lemming[actLEM].Onmouse && !lemming[actLEM].Miner //MINER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Basher || lemming[actLEM].Builder))
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
                            lemming[actLEM].Miner = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Numframes = SizeSprites.pico_frames;
                            continue;
                        }
                    }

                }
                if (MyGame.Paused)
                    continue;
                if (draw_builder && lemming[actLEM].Builder)
                {
                    lemming[actLEM].Actualframe++;
                    if (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1 && !lemming[actLEM].Explode)
                    {
                        lemming[actLEM].Actualframe = 0;
                    }
                }
                if (draw_walker && !lemming[actLEM].Builder && !lemming[actLEM].Basher && !lemming[actLEM].Miner
                    && !lemming[actLEM].Burned && !lemming[actLEM].Drown)
                {
                    lemming[actLEM].Actualframe++;
                    if (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1 && !lemming[actLEM].Explode)
                    {
                        lemming[actLEM].Actualframe = 0;
                    }
                    //be carefull with bomber frames actualization
                }
                if (draw2 && (lemming[actLEM].Basher || lemming[actLEM].Miner
                    || lemming[actLEM].Burned || lemming[actLEM].Drown)) // see careful frames
                {
                    lemming[actLEM].Actualframe++;
                    if ((lemming[actLEM].Burned || lemming[actLEM].Drown) &&
                        (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1))
                    {
                        lemming[actLEM].Burned = false;
                        lemming[actLEM].Drown = false;
                        lemming[actLEM].Dead = true;
                        lemming[actLEM].Explode = false;
                        lemming[actLEM].Exploser = false;
                        numlemnow--;
                    }
                    if (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1 && !lemming[actLEM].Explode)
                    {
                        lemming[actLEM].Actualframe = 0;
                    }
                }
                if (lemming[actLEM].Exit)
                {
                    if (lemming[actLEM].Actualframe == lemming[actLEM].Numframes - 1)
                    {
                        lemming[actLEM].Dead = true;
                        lemming[actLEM].Explode = false;
                        lemming[actLEM].Exploser = false;
                        numlemnow--;
                        numSaved++;  // here is where the lemming go inside after door animation
                    }
                    continue;
                }
                int arriba = 0;
                _below = 0;
                int pixx = lemming[actLEM].PosX + medx;
                for (int x55 = 0; x55 <= 8; x55++)
                {
                    int pos_real = lemming[actLEM].PosY + x55 + medy + medy;  ///////////// pixel por debajo -> beneath.............
                    if (pos_real == earth.Height)
                    {
                        _below = 9;
                        break;
                    }
                    if (pos_real < 0)
                        pos_real = 0;
                    if (pos_real > earth.Height)
                    {
                        lemming[actLEM].Dead = true;
                        _below = 9;
                        lemming[actLEM].Active = false;
                        numlemnow--;
                        lemming[actLEM].Explode = false;
                        lemming[actLEM].Exploser = false;
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
                    if (C25[(pos_real * earth.Width) + pixx].R == 0 && C25[(pos_real * earth.Width) + pixx].G == 0 && C25[(pos_real * earth.Width) + pixx].B == 0)
                    {
                        _below++;
                    }
                    else
                    {
                        break;
                    }
                }
                // very important to check digger and miner before change to falling
                if (lemming[actLEM].Pixelscaida > useumbrella && !lemming[actLEM].Falling && lemming[actLEM].Umbrella
                    && (!lemming[actLEM].Digger && !lemming[actLEM].Miner && !lemming[actLEM].Builder) && lemming[actLEM].Active)
                {
                    lemming[actLEM].Pixelscaida = 11;
                    lemming[actLEM].Falling = true;
                    lemming[actLEM].Fall = false;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = SizeSprites.floater_frames;
                }
                if ((_below > 8 && !lemming[actLEM].Fall && (!lemming[actLEM].Digger || !lemming[actLEM].Miner)) && !lemming[actLEM].Falling
                    && !lemming[actLEM].Explode && lemming[actLEM].Active)
                {
                    lemming[actLEM].Fall = true;
                    lemming[actLEM].Pixelscaida = 0;
                    lemming[actLEM].Climbing = false;
                    lemming[actLEM].Walker = false;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = SizeSprites.faller_frames;
                    lemming[actLEM].Basher = false;
                    lemming[actLEM].Builder = false;
                    lemming[actLEM].Bridge = false;
                    lemming[actLEM].Miner = false;
                    continue; // lemming fall when there's no floor on feet and fall down
                }
                if ((_below == 0) && (lemming[actLEM].Fall || lemming[actLEM].Falling) && (!lemming[actLEM].Digger && !lemming[actLEM].Miner)) //OJO LOCO A VECES AL CAVAR Y SIGUE WALKER
                {
                    if (lemming[actLEM].Pixelscaida <= maxnumberfalling)
                    {
                        lemming[actLEM].Pixelscaida = 0;
                        lemming[actLEM].Framescut = false;
                        lemming[actLEM].Falling = false;
                        lemming[actLEM].Walker = true;
                        lemming[actLEM].Fall = false;
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Numframes = SizeSprites.walker_frames;  //8 walker;4 fall;16 digger;breakfloor 16;escala ...
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
                        lemming[actLEM].Fall = false;
                        lemming[actLEM].Walker = false;
                        lemming[actLEM].Falling = false;
                        lemming[actLEM].Explode = false;
                        lemming[actLEM].Exploser = false;
                        lemming[actLEM].Active = false;
                        lemming[actLEM].Breakfloor = true;
                        lemming[actLEM].Umbrella = false;
                        lemming[actLEM].Numframes = SizeSprites.floor_frames;
                        lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
                if ((_below == 0) && lemming[actLEM].Walker && (!lemming[actLEM].Digger && !lemming[actLEM].Miner))
                {
                    lemming[actLEM].Pixelscaida = 0;
                }
                for (int x55 = 0; x55 <= 20; x55++)
                {
                    int pos_real = lemming[actLEM].PosY + medy + medy - x55;
                    if (pos_real == earth.Height)    // rompe los calculos si sale de la pantalla o se cuelga AARRIBBBAAAA
                    {
                        lemming[actLEM].Active = false;
                        break;
                    }
                    if (pos_real < earth.Height && pos_real > 0)
                    {
                        if (C25[(pos_real * earth.Width) + pixx].R > 0 || C25[(pos_real * earth.Width) + pixx].G > 0 || C25[(pos_real * earth.Width) + pixx].B > 0)
                        {
                            arriba++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (lemming[actLEM].Blocker && _below > 2)
                {
                    lemming[actLEM].Blocker = false;
                    lemming[actLEM].Fall = true;
                    lemming[actLEM].Pixelscaida = 0;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = SizeSprites.faller_frames;
                    continue;
                }
                if (lemming[actLEM].Miner && draw2 && lemming[actLEM].Actualframe == 42)  // miner logic pico logic
                {
                    if (ArrowsON) // miner arrows logic areaTrap Intersects
                    {
                        bool nominer = false;
                        arrowLem.X = lemming[actLEM].PosX;
                        arrowLem.Y = lemming[actLEM].PosY;
                        arrowLem.Width = 28;
                        arrowLem.Height = 28;
                        for (int wer3 = 0; wer3 < NumTotArrow; wer3++)
                        {
                            if (arrow[wer3].area.Intersects(arrowLem) && lemming[actLEM].Right && !arrow[wer3].right)
                            {
                                nominer = true;
                                continue;
                            }
                            if (arrow[wer3].area.Intersects(arrowLem) && lemming[actLEM].Left && arrow[wer3].right)
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
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            continue;
                        }
                    }
                    if (lemming[actLEM].Right)
                    {
                        int width2 = 20;
                        int top2 = 20;
                        int px = lemming[actLEM].PosX + 12;
                        int py = lemming[actLEM].PosY + 14;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + width2 >= earth.Width)
                        {
                            width2 = earth.Width - px;
                        }
                        if (py + top2 >= earth.Height)
                        {
                            top2 = earth.Height - py;
                        }
                        LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                        int amount = 0;
                        for (int yy88 = 0; yy88 < top2; yy88++)
                        {
                            int yypos888 = (yy88 + py) * earth.Width;
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
                                    lemming[actLEM].Walker = true;
                                    lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                    lemming[actLEM].Actualframe = 0;
                                    lemming[actLEM].Miner = false;
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
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        amount = 0;
                        for (int yy33 = 0; yy33 < top2; yy33++)
                        {
                            int yypos555 = (yy33 + py) * earth.Width;
                            for (int xx33 = 0; xx33 < width2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                                amount++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        lemming[actLEM].PosX += 12;
                        lemming[actLEM].PosY++;
                        if (Frente2 == 0)
                        {
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            continue;
                        }
                    }
                    else
                    {
                        int width2 = 20;
                        int top2 = 20;
                        int px = lemming[actLEM].PosX - 4;
                        if (px < 0)
                        {
                            px = 0;
                        }
                        int py = lemming[actLEM].PosY + 14;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + width2 >= earth.Width)
                        {
                            width2 = earth.Width - px;
                        }
                        if (py + top2 >= earth.Height)
                        {
                            top2 = earth.Height - py;
                        }
                        LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                        int amount = 0;
                        for (int yy88 = 0; yy88 < top2; yy88++)
                        {
                            int yypos888 = (yy88 + py) * earth.Width;
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
                                    lemming[actLEM].Walker = true;
                                    lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                    lemming[actLEM].Actualframe = 0;
                                    lemming[actLEM].Miner = false;
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
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        amount = 0;
                        for (int yy33 = 0; yy33 < top2; yy33++)
                        {
                            int yypos555 = (yy33 + py) * earth.Width;
                            for (int xx33 = 0; xx33 < width2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                                amount++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        lemming[actLEM].PosX -= 12;
                        lemming[actLEM].PosY++;
                        if (Frente2 == 0)
                        {
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            continue;
                        }
                    }
                    Frente2 = 0;  /////// PPPPPPPPIIIIIIIIIICCCCCCCCCCCCCCCCCOOOOOOOOOOOOOOOOOOO  BASHER LOGIC puto33
                }

                if (lemming[actLEM].Basher && (lemming[actLEM].Actualframe == 10 || lemming[actLEM].Actualframe == 37) && draw2)
                {
                    if (ArrowsON) // basher arrows logic areaTrap Intersects
                    {
                        bool nobasher = false;
                        arrowLem.X = lemming[actLEM].PosX;
                        arrowLem.Y = lemming[actLEM].PosY;
                        arrowLem.Width = 28;
                        arrowLem.Height = 28;
                        for (int wer3 = 0; wer3 < NumTotArrow; wer3++)
                        {
                            if (arrow[wer3].area.Intersects(arrowLem) && lemming[actLEM].Right && !arrow[wer3].right)
                            {
                                nobasher = true;
                                continue;
                            }
                            if (arrow[wer3].area.Intersects(arrowLem) && lemming[actLEM].Left && arrow[wer3].right)
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
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            continue;
                        }
                    }
                    if (lemming[actLEM].Right)
                    {
                        int width2 = 20;
                        int top2 = 20;
                        int px = lemming[actLEM].PosX + 14;
                        int py = lemming[actLEM].PosY + 8;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + width2 >= earth.Width)
                        {
                            width2 = earth.Width - px;
                        }
                        if (py + top2 >= earth.Height)
                        {
                            top2 = earth.Height - py;
                        }
                        LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop
                        int amount = 0;
                        for (int yy88 = 0; yy88 < top2; yy88++)
                        {
                            int yypos888 = (yy88 + py) * earth.Width;
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
                                        lemming[actLEM].Walker = true;
                                        lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                        lemming[actLEM].Actualframe = 0;
                                        lemming[actLEM].Basher = false;
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
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        amount = 0;
                        for (int yy33 = 0; yy33 < top2; yy33++)
                        {
                            int yypos555 = (yy33 + py) * earth.Width;
                            for (int xx33 = 0; xx33 < width2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                                amount++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        if (xEmpty < xErase)
                            lemming[actLEM].PosX += 14;
                        if (xEmpty > xErase || xErase == 21)
                        {
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            continue;
                        }
                    }
                    else
                    {
                        int width2 = 20;
                        int top2 = 20;
                        int px = lemming[actLEM].PosX - 5;
                        if (px < 0)
                        {
                            px = 0;
                        }
                        int py = lemming[actLEM].PosY + 8;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + width2 >= earth.Width)
                        {
                            width2 = earth.Width - px;
                        }
                        if (py + top2 >= earth.Height)
                        {
                            top2 = earth.Height - py;
                        }
                        int amount = 0;
                        for (int yy88 = 0; yy88 < top2; yy88++)
                        {
                            int yypos888 = (yy88 + py) * earth.Width;
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
                                        lemming[actLEM].Walker = true;
                                        lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                        lemming[actLEM].Actualframe = 0;
                                        lemming[actLEM].Basher = false;
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
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, amount);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        amount = 0;
                        for (int yy33 = 0; yy33 < top2; yy33++)
                        {
                            int yypos555 = (yy33 + py) * earth.Width;
                            for (int xx33 = 0; xx33 < width2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[amount].PackedValue;
                                amount++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        if (xEmpty > xErase)
                            lemming[actLEM].PosX -= 14;
                        if (xEmpty < xErase || xEmpty == 1) // xerase==20 nothing erases
                        {
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            continue;
                        }
                    }
                    Frente2 = 0;
                    ////////////////////////////////////////////////////////////////////// PPPPPPPPPAAAAAAARRRRRRRRRRRRRRRREEEEEEEDDDDDDDDD
                }
                if (lemming[actLEM].Basher && _below > 3)
                {
                    lemming[actLEM].Basher = false;
                    lemming[actLEM].Walker = true;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = SizeSprites.walker_frames;
                    continue;
                }
                if (lemming[actLEM].Builder && draw_builder) // BUILDER LOGIC HERE chink sound see limits tooo FIX FIX FIX
                {
                    if (lemming[actLEM].Actualframe >= 48 && lemming[actLEM].Numstairs < 12) // >=33 old with dibuja2
                    // i need to cut on frame 33 of 56 because speed problems timings and x & y axis, see later to fix speed making stairs and fix positioning for get real 56 frames
                    {
                        Frente = 0;
                        lemming[actLEM].Actualframe = SizeSprites.builder_frames + 1;  // erase with // no compiling//  to see full frames
                        if (lemming[actLEM].Right)
                        {
                            if (arriba > 1)
                            {
                                lemming[actLEM].PosY += 6;
                                lemming[actLEM].PosX -= 14;
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].Right = false;
                                continue;
                            }
                            if (lemming[actLEM].PosY < -24) //see ok was -24 but sometimes fails the u-turn
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                lemming[actLEM].PosY += 3;
                                lemming[actLEM].PosX -= 6;
                                continue;
                            }
                            for (int y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                int posi_real = (lemming[actLEM].PosY + 24 + y) * earth.Width + lemming[actLEM].PosX;
                                for (int xx88 = 14; xx88 <= 28; xx88++)
                                {
                                    if (C25[posi_real + xx88].R == 0 && C25[posi_real + xx88].G == 0 && C25[posi_real + xx88].B == 0)
                                    {
                                        colorFill.R = Convert.ToByte(255 - (lemming[actLEM].Numstairs * 5));
                                        colorFill.G = 0;
                                        colorFill.B = Convert.ToByte(255 - (lemming[actLEM].Numstairs * 10));
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
                            lemming[actLEM].Numstairs++;
                            lemming[actLEM].PosY -= 3;
                            lemming[actLEM].PosX += 6;
                            if (lemming[actLEM].Numstairs >= 10)
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
                                int posi_real = (lemming[actLEM].PosY + ykk) * earth.Width + lemming[actLEM].PosX;
                                for (int xkk = 0; xkk < 28; xkk++)
                                {
                                    Colormask22[amount] = C25[posi_real + xkk];
                                    amount++;
                                }
                            }
                            rectangleFill.X = lemming[actLEM].PosX;
                            rectangleFill.Y = lemming[actLEM].PosY + 27;
                            rectangleFill.Width = 28;
                            rectangleFill.Height = 4;
                            earth.SetData(0, rectangleFill, Colormask22, 0, 28 * 4);
                            if (Frente == 3)
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].PosX -= 7;
                                lemming[actLEM].Right = false;
                            }
                            continue;
                        }
                        else
                        {
                            if (arriba > 1)
                            {
                                lemming[actLEM].PosY += 6;
                                lemming[actLEM].PosX += 15;
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].Right = true;
                                continue;

                            }
                            if (lemming[actLEM].PosY < -24) //see ok was -24
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                lemming[actLEM].PosY += 3;
                                lemming[actLEM].PosX += 6;
                                continue;
                            }
                            for (int y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                int posi_real = (lemming[actLEM].PosY + 24 + y) * earth.Width + lemming[actLEM].PosX;
                                for (int xx88 = 0; xx88 <= 14; xx88++)
                                {
                                    if (C25[posi_real + xx88].R == 0 && C25[posi_real + xx88].G == 0 && C25[posi_real + xx88].B == 0)
                                    {
                                        colorFill.R = Convert.ToByte(255 - (lemming[actLEM].Numstairs * 5));
                                        colorFill.G = 0;
                                        colorFill.B = Convert.ToByte(255 - (lemming[actLEM].Numstairs * 10));
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
                            lemming[actLEM].Numstairs++;
                            lemming[actLEM].PosY -= 3;
                            lemming[actLEM].PosX -= 6;
                            if (lemming[actLEM].Numstairs >= 10)
                            {
                                if (LemmingsNetGame.Instance.Sfx.Chink.State == SoundState.Playing)
                                {
                                    LemmingsNetGame.Instance.Sfx.Chink.Stop();
                                }
                                LemmingsNetGame.Instance.Sfx.Chink.Play();
                            }
                            //earth.SetData<Color>(c25); //OPTIMIZED BUILDER SETDATA
                            int amount = 0;
                            int px = lemming[actLEM].PosX;
                            if (px < 0)
                                px = 0;
                            for (int ykk = 27; ykk < 31; ykk++)
                            {
                                int posi_real = (lemming[actLEM].PosY + ykk) * earth.Width + px;
                                for (int xkk = 0; xkk < 28; xkk++)
                                {
                                    Colormask22[amount] = C25[posi_real + xkk];
                                    amount++;
                                }
                            }
                            rectangleFill.X = px;
                            rectangleFill.Y = lemming[actLEM].PosY + 27;
                            rectangleFill.Width = 28;
                            rectangleFill.Height = 4;
                            earth.SetData(0, rectangleFill, Colormask22, 0, 28 * 4);
                            if (Frente == 3)
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].PosX += 8;
                                lemming[actLEM].Right = true;
                            }
                            continue;
                        }
                    }
                    if (lemming[actLEM].Numstairs >= 12 &&
                        !lemming[actLEM].Bridge)
                    {
                        lemming[actLEM].Builder = false;
                        lemming[actLEM].Bridge = true;
                        lemming[actLEM].Pixelscaida = 0;
                        if (lemming[actLEM].Right)
                        {
                            lemming[actLEM].PosX -= 6;
                        }
                        else
                        {
                            lemming[actLEM].PosX += 6;
                        }
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Numframes = SizeSprites.walker_frames;
                    }
                }
                if (lemming[actLEM].Bridge && lemming[actLEM].Actualframe == 7 && lemming[actLEM].Bridge)
                {
                    lemming[actLEM].Bridge = false;
                    lemming[actLEM].Walker = true;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = SizeSprites.walker_frames;
                    lemming[actLEM].Numstairs = 0;
                    continue;
                }
                if (lemming[actLEM].Digger) ///// DIGGER DIGGER WARNING WARNING
                {
                    if (_below == 0 || _below == 1) // 5 ok que no se aceleren a digger si hay mas de 2 juntos antes era <9 los pixeles debajo de sus pies
                    {
                        int abajo2 = 0;
                        int pixx2 = lemming[actLEM].PosX + 14;
                        for (int xx88 = 0; xx88 <= 4; xx88++)
                        {
                            int pos_real2 = lemming[actLEM].PosY + xx88 + 28;  ///////////// pixel por debajo.............
                            if (pos_real2 == earth.Height)
                            {
                                abajo2 = 9;
                                break;
                            }
                            if (pos_real2 < 0)
                                pos_real2 = 0;
                            if (pos_real2 > earth.Height)
                            {
                                pos_real2 = earth.Height;
                            }
                            if (C25[(pos_real2 * earth.Width) + pixx2].R > 0 || C25[(pos_real2 * earth.Width) + pixx2].G > 0 || C25[(pos_real2 * earth.Width) + pixx2].B > 0)
                            {
                                abajo2++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if ((lemming[actLEM].Actualframe == 11 || lemming[actLEM].Actualframe == 26) && draw_walker)
                        {
                            sx = 0;
                            for (int y = 9; y <= 18; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                int posi_real = (lemming[actLEM].PosY + 14 + y) * earth.Width + lemming[actLEM].PosX;
                                if (lemming[actLEM].PosY + 14 + y > earth.Height)
                                {
                                    break;
                                } // cortar si esta en el limite por debajo 512=earth.height
                                for (int xx88 = 4; xx88 <= 24; xx88++)
                                {
                                    if (SteelON)
                                    {
                                        x.X = lemming[actLEM].PosX + xx88;
                                        x.Y = lemming[actLEM].PosY + 14 + y;
                                        for (int xz = 0; xz < numTOTsteel; xz++)
                                        {
                                            if (steel[xz].area.Contains(x))
                                            {
                                                sx = -777; break;
                                            }
                                        }
                                        if (sx == -777)
                                        {
                                            lemming[actLEM].Walker = true;
                                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                                            lemming[actLEM].Actualframe = 0;
                                            lemming[actLEM].Digger = false;
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
                                int posi_real = (lemming[actLEM].PosY + 14 + ykk) * earth.Width + lemming[actLEM].PosX;
                                for (int xkk = 0; xkk < 28; xkk++)
                                {
                                    Colormask22[amount] = C25[posi_real + xkk];
                                    amount++;
                                }
                            }
                            rectangleFill.X = lemming[actLEM].PosX;
                            rectangleFill.Y = lemming[actLEM].PosY + 23;
                            rectangleFill.Width = 28;
                            rectangleFill.Height = 10;
                            earth.SetData(0, rectangleFill, Colormask22, 0, 28 * 10);
                            if (sx == -777)
                                continue;
                            lemming[actLEM].PosY += abajo2;
                            continue;
                        }
                    }
                    else
                    {
                        if (lemming[actLEM].PosY + 28 >= earth.Height) // erase draws bottom when die and exit level height 21x10
                        {
                            for (int ykk = 0; ykk < 210; ykk++)
                            {
                                Colormask22[ykk].PackedValue = 0;
                            }
                            rectangleFill.Y = 502;
                            rectangleFill.X = lemming[actLEM].PosX + 4;
                            rectangleFill.Width = 21;
                            rectangleFill.Height = 10;
                            earth.SetData(0, rectangleFill, Colormask22, 0, 210);
                        }
                        lemming[actLEM].Basher = false;
                        lemming[actLEM].Builder = false;
                        lemming[actLEM].Miner = false;
                        lemming[actLEM].Climbing = false;
                        lemming[actLEM].Digger = false;
                        lemming[actLEM].Fall = true;
                        lemming[actLEM].Walker = false;
                        lemming[actLEM].Pixelscaida = 0;
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Numframes = SizeSprites.faller_frames;
                        continue; //break o continue DUNNO I DON'T KNOW WHICH IS BETTER
                    }

                }
                if (lemming[actLEM].Climbing)
                {
                    if (lemming[actLEM].PosY <= -28) // top of level -- out of limits 28 size sprite lemming 28x28
                    {
                        lemming[actLEM].Climbing = false;
                        lemming[actLEM].Fall = true;
                        lemming[actLEM].Walker = false;
                        lemming[actLEM].Pixelscaida = 0;
                        lemming[actLEM].Numframes = SizeSprites.faller_frames;
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Builder = false;
                        lemming[actLEM].Right = !lemming[actLEM].Right;
                        continue;
                    }
                    if (lemming[actLEM].Right)
                    {
                        int pos_real2 = lemming[actLEM].PosY + 27;
                        if (C25[(pos_real2 * earth.Width) + pixx - 2].R > 0 || C25[(pos_real2 * earth.Width) + pixx - 2].G > 0 || C25[(pos_real2 * earth.Width) + pixx - 2].B > 0)
                        {
                            lemming[actLEM].Right = false;
                            lemming[actLEM].PosX -= 2;   // 1 o 2 LOOK
                            lemming[actLEM].Climbing = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            lemming[actLEM].Actualframe = 0;
                            continue;
                        }
                    }
                    else
                    {
                        int pos_real2 = lemming[actLEM].PosY + 27;
                        if (C25[(pos_real2 * earth.Width) + pixx + 2].R > 0 || C25[(pos_real2 * earth.Width) + pixx + 2].G > 0 || C25[(pos_real2 * earth.Width) + pixx + 2].B > 0)
                        {
                            lemming[actLEM].Right = true;
                            lemming[actLEM].PosX += 2; // 1 o 2 LOOK
                            lemming[actLEM].Climbing = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Numframes = SizeSprites.walker_frames;
                            lemming[actLEM].Actualframe = 0;
                            continue;
                        }
                    }
                    if (arriba > 0 && dibuja)
                    {
                        lemming[actLEM].PosY--;
                    }
                    if (arriba == 0)
                    {
                        if (lemming[actLEM].Right)
                        {
                            lemming[actLEM].PosX++;
                        }
                        else
                        {
                            lemming[actLEM].PosX--;
                        }
                        lemming[actLEM].Climbing = false;
                        lemming[actLEM].Walker = true;
                        lemming[actLEM].Numframes = SizeSprites.walker_frames;
                        lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
                if (lemming[actLEM].Walker)
                {
                    if (_below < 3 && lemming[actLEM].Right)
                    {
                        lemming[actLEM].PosX++;
                        if (arriba < 16)
                        {
                            lemming[actLEM].PosY -= arriba;
                        }
                    }  //// <6 o <8 falla cava
                    if (_below < 3 && lemming[actLEM].Left)
                    {
                        lemming[actLEM].PosX--;
                        if (arriba < 16)
                        {
                            lemming[actLEM].PosY -= arriba;
                        }
                    }
                    if (arriba >= 16)
                    {
                        if (!lemming[actLEM].Climber)
                        {
                            if (lemming[actLEM].Right && arriba >= 16)
                            {
                                lemming[actLEM].Right = false;
                                lemming[actLEM].PosX -= 2;  // 1 o 2 LOOK
                            }
                            else
                            {
                                lemming[actLEM].Right = true;
                                lemming[actLEM].PosX += 2;  // 1 o 2 LOOK
                            }
                        }
                        else
                        {
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Climbing = true;
                            lemming[actLEM].Numframes = SizeSprites.climber_frames;
                            lemming[actLEM].Pixelscaida = 0;
                            lemming[actLEM].Actualframe = 0;
                            continue;
                        }
                    }
                }
                if (lemming[actLEM].Explode && lemming[actLEM].Actualframe >= 47)
                {
                    ////////////////////////////////////////////////////////////////////////////////////// EXPLODE MASK
                    ///////////////// EXPLODING MASK LIMITS -- SIZE OF AREA ERASEABLE
                    int ancho66 = 38;
                    int alto66 = 53;
                    int px = lemming[actLEM].PosX - 5; //center the big explosion to 28x28 lemming sprite
                    int py = lemming[actLEM].PosY - 2;
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
                    if (px + ancho66 >= earth.Width)  // right of the level
                    {
                        ancho66 = earth.Width - px;
                    }
                    if (py + alto66 >= earth.Height) //bottom of the level
                    {
                        alto66 = earth.Height - py;
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
                        int yypos888 = (yy88 + py) * earth.Width;
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
                    earth.SetData(0, rectangleFill, Colorsobre33, 0, amount);
                    // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                    amount = 0;
                    for (int yy33 = 0; yy33 < alto66; yy33++)
                    {
                        int yypos555 = (yy33 + py) * earth.Width;
                        for (int xx33 = 0; xx33 < ancho66; xx33++)
                        {
                            C25[yypos555 + px + xx33].PackedValue = Colorsobre33[amount].PackedValue;
                            amount++;
                        }
                    }
                    lemming[actLEM].Dead = true;
                    numlemnow--;
                    lemming[actLEM].Explode = false;
                    lemming[actLEM].Exploser = false;
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
                    int xExp = lemming[actLEM].PosX + 14;
                    int yExp = lemming[actLEM].PosY + 14;
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
                if (!lemming[actLEM].Falling && lemming[actLEM].Active)
                {
                    if (_below >= 3)
                    {
                        lemming[actLEM].PosY += 3;
                        lemming[actLEM].Pixelscaida += 3;
                    }
                    else
                    {
                        lemming[actLEM].PosY += _below;
                        lemming[actLEM].Pixelscaida += _below;
                    } // fall 3 MAX---MAX 3 FALL PIXELS
                }
                else
                {
                    if (!lemming[actLEM].Drown && dibuja)
                    {
                        if (_below >= 3)
                        {
                            lemming[actLEM].PosY += 3;
                        }
                        else
                        {
                            lemming[actLEM].PosY += _below;
                        }
                    }
                }
                if (lemming[actLEM].PosY < -27) // walker top of the screen
                {
                    if (lemming[actLEM].Right)
                    {
                        lemming[actLEM].Right = false;
                        lemming[actLEM].PosX -= 3;
                        lemming[actLEM].PosY++;
                    }
                    else
                    {
                        lemming[actLEM].Right = true;
                        lemming[actLEM].PosX += 3;
                        lemming[actLEM].PosY++;
                    }
                }
                if (lemming[actLEM].PosX < -16)// limits of the screen from LEFT
                {
                    lemming[actLEM].Active = false;
                    lemming[actLEM].Dead = true;
                    numlemnow--;
                    lemming[actLEM].Explode = false;
                    lemming[actLEM].Exploser = false;
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
                if (lemming[actLEM].PosX + 14 > earth.Width)// limits of the screen from RIGHT
                {
                    lemming[actLEM].Active = false;
                    lemming[actLEM].Dead = true;
                    numlemnow--;
                    lemming[actLEM].Explode = false;
                    lemming[actLEM].Exploser = false;
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
            spriteBatch.Draw(earth, new Vector2(0, 0), new Rectangle(ScrollX, ScrollY, MyGame.GameResolution.X, MyGame.GameResolution.Y - 188), //512 size of window draw
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
                int percent = (100 * numSaved) / AllLevel[LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose].TotalLemmings;
                LemmingsNetGame.Instance.Fonts.TextLem("All lemmings accounted for:", new Vector2(150, 100), Color.Cyan, 1.5f, 0.0000000001f, spriteBatch);
                LemmingsNetGame.Instance.Fonts.TextLem("You rescued " + string.Format("{0}", percent) + "%",
                     new Vector2(270, 160), Color.Violet, 1.5f, 0.0000000001f, spriteBatch);
                percent = (100 * Lemsneeded) / AllLevel[LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose].TotalLemmings;
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
            int xx55 = varDoor[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor].xWidth;
            int yy55 = varDoor[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor].yWidth;
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
            int xx66 = varExit[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].xWidth;
            int yy66 = varExit[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].yWidth;
            int xx88 = varExit[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreX;
            int xx99 = varExit[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreX2;
            int yy88 = varExit[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreY;
            int yy99 = varExit[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfExit].moreY2;
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

            for (actLEM = 0; actLEM < numLemmings; actLEM++) //si lo hace de 100 a cero dibujara los primeros encima y mejorara el aspecto
            {
                if (doorOn)
                    break;
                if (lemming[actLEM].Dead)
                    continue;
                if (lemming[actLEM].Exploser && !lemming[actLEM].Explode)
                {
                    if (lemming[actLEM].Time == 0)
                        lemming[actLEM].Time = totalTime;
                    double timez = totalTime - lemming[actLEM].Time;
                    int crono = (int)(6f - (float)timez);
                    LemmingsNetGame.Instance.Fonts.TextLem(string.Format("{0}", crono), new Vector2(lemming[actLEM].PosX + 3 - ScrollX, lemming[actLEM].PosY - 10 - ScrollY), Color.White, 0.4f, 0.000000000004f, spriteBatch);
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
                        lemming[actLEM].Explode = true;
                        lemming[actLEM].Active = false;
                        lemming[actLEM].Umbrella = false;
                        lemming[actLEM].Walker = false;
                        lemming[actLEM].Digger = false;
                        lemming[actLEM].Climber = false;
                        lemming[actLEM].Fall = false;
                        lemming[actLEM].Falling = false;
                        lemming[actLEM].Climbing = false;
                        lemming[actLEM].Exit = false;
                        lemming[actLEM].Blocker = false;
                        lemming[actLEM].Builder = false;
                        lemming[actLEM].Bridge = false;
                        lemming[actLEM].Basher = false;
                        lemming[actLEM].Miner = false;
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Numframes = SizeSprites.bomber_frames;
                    }
                }
                int framereal55;
                if (lemming[actLEM].Burned) // scale POSDraw x+0,y+0 at 1.2f x-5,y+0 at 1.35f
                {
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Squemado, new Vector2(lemming[actLEM].PosX - ScrollX - 5, lemming[actLEM].PosY - ScrollY), new Rectangle(0, lemming[actLEM].Actualframe * 28, 32, 28),
                    (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Lhiss, new Vector2(lemming[actLEM].PosX - ScrollX, lemming[actLEM].PosY - 20 - ScrollY), new Rectangle(0, 0, LemmingsNetGame.Instance.Gfx.Lhiss.Width, LemmingsNetGame.Instance.Gfx.Lhiss.Height),
                        Color.White, 0f, Vector2.Zero, (0.5f + (0.01f * lemming[actLEM].Actualframe)), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Drown) // scale POSDraw x+0,y+10 at 1.2f x-8,y+7 at 1.35f  //puto ahoga
                {
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Drowner, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.water_xpos, lemming[actLEM].PosY + SizeSprites.water_ypos - ScrollY), new Rectangle(lemming[actLEM].Actualframe * SizeSprites.water_with, 0, SizeSprites.water_with, SizeSprites.water_height),
                        (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.water_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Walker)
                {
                    framereal55 = (lemming[actLEM].Actualframe * SizeSprites.walker_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Walker, new Vector2((lemming[actLEM].PosX - ScrollX + SizeSprites.walker_xpos), lemming[actLEM].PosY - ScrollY + SizeSprites.walker_ypos), new Rectangle(framereal55, 0, SizeSprites.walker_with, SizeSprites.walker_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.walker_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Blocker) // blocker scale POSDraw x-5 y+4 at 1.2f x-7 y+1 at 1.35f  //puto
                {
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.blocker_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Blocker, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.blocker_xpos, lemming[actLEM].PosY + SizeSprites.blocker_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.blocker_with, SizeSprites.blocker_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.blocker_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    if (LemmingsNetGame.Instance.DebugOsd.debug)
                    {
                        bloqueo = new Rectangle(lemming[actLEM].PosX, lemming[actLEM].PosY, 28, 28);
                        spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, new Rectangle(bloqueo.Left - ScrollX, bloqueo.Top - ScrollY, bloqueo.Width, bloqueo.Height), null,
                            Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    }
                }
                if (lemming[actLEM].Bridge) // scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f
                {
                    framesale = (lemming[actLEM].Actualframe * 26);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Puente_nomas, new Vector2(lemming[actLEM].PosX - ScrollX - 7, lemming[actLEM].PosY - 7 - ScrollY), new Rectangle(0, framesale, 32, 26), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, (lemming[actLEM].Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Builder)  //scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f  builder builder draws
                {
                    if (lemming[actLEM].Numstairs >= 10) // chink draws
                    {
                        spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Chink, new Vector2(lemming[actLEM].PosX - ScrollX - 10, lemming[actLEM].PosY - 30 - ScrollY), new Rectangle(0, 0, LemmingsNetGame.Instance.Sprites.Chink.Width, LemmingsNetGame.Instance.Sprites.Chink.Height),
                            Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.builder_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Puente, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.builder_xpos, lemming[actLEM].PosY + SizeSprites.builder_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.builder_with, SizeSprites.builder_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.builder_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Miner)  //scale POSDraw x-5,y-2 at 1.2f x-9,y-7 at 1.35f pico pico miner miner
                {
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.pico_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Pico, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.pico_xpos + (lemming[actLEM].Right ? 0 : 10), lemming[actLEM].PosY + SizeSprites.pico_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.pico_with, SizeSprites.pico_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.pico_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Basher) //puto
                {           // scale basher RIGHT POSDRAW x-10,y+4 at 1.2f x-15,y+1 at 1.35f
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.basher_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Pared, new Vector2(lemming[actLEM].PosX - ScrollX + (lemming[actLEM].Right ? SizeSprites.basher_xpos : SizeSprites.basher_xposleft), lemming[actLEM].PosY + SizeSprites.basher_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.basher_with, SizeSprites.basher_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.basher_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Explode) // explotando explotando bomber bomber
                {
                    // bomber scale POSDraw x-5,y+4 at 1.2f x-9,y+2 at 1.35f
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.bomber_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Exploder, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.bomber_xpos, lemming[actLEM].PosY + SizeSprites.bomber_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.bomber_with, SizeSprites.bomber_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.bomber_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Lohno, new Vector2(lemming[actLEM].PosX - ScrollX - 20, lemming[actLEM].PosY - 25 - ScrollY), new Rectangle(0, 0, LemmingsNetGame.Instance.Sprites.Lohno.Width, LemmingsNetGame.Instance.Sprites.Lohno.Height),
                        Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Breakfloor) // scale POSDraw x-5,y+4 at 1.2f  x-9,y+2 at 1.35f breakfloor breakfloor
                {
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.floor_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Rompesuelo, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.floor_xpos, lemming[actLEM].PosY + SizeSprites.floor_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.floor_with, SizeSprites.floor_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floor_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    if (lemming[actLEM].Actualframe == SizeSprites.floor_frames - 1)
                    {
                        lemming[actLEM].Dead = true;
                        numlemnow--;
                        lemming[actLEM].Explode = false;
                        lemming[actLEM].Exploser = false;
                    }
                }
                if (lemming[actLEM].Falling) //umbrella paraguas falling with umbrella
                {
                    if (!lemming[actLEM].Framescut && lemming[actLEM].Actualframe == SizeSprites.floater_frames - 1)
                    {
                        lemming[actLEM].Framescut = true;
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Numframes = SizeSprites.floater_frames - 1 - 4;
                    }
                    if (!lemming[actLEM].Framescut)
                        framesale = (lemming[actLEM].Actualframe * SizeSprites.floater_with);
                    else
                        framesale = (lemming[actLEM].Actualframe + 4) * SizeSprites.floater_with; // scale floater POSDraw x-5,y-4 at 1.2f x-9,y-7 at 1.35f
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Paraguas, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.floater_xpos, lemming[actLEM].PosY + SizeSprites.floater_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.floater_with, SizeSprites.floater_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floater_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Fall) //fall cae
                {
                    framereal55 = (lemming[actLEM].Actualframe * SizeSprites.faller_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Falling, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.faller_xpos, lemming[actLEM].PosY - ScrollY + SizeSprites.faller_ypos), new Rectangle(framereal55, 0, SizeSprites.faller_with, SizeSprites.faller_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.faller_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Exit && !lemming[actLEM].Dead) //sale sale exit exit out out
                {
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.sale_with); // exit scale POSDraw  x-1,y+1 at 1.2f x-3,y-1 at 1.35f
                    spriteBatch.Draw(sale, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.sale_xpos, lemming[actLEM].PosY + SizeSprites.sale_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.sale_with, SizeSprites.sale_height), Color.White, 0f, Vector2.Zero, SizeSprites.sale_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                }
                if (lemming[actLEM].Digger)
                {
                    framereal55 = (lemming[actLEM].Actualframe * SizeSprites.digger_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Digger, new Vector2(lemming[actLEM].PosX - ScrollX + SizeSprites.digger_xpos, lemming[actLEM].PosY + 6 - ScrollY + SizeSprites.digger_ypos), new Rectangle(framereal55, 0, SizeSprites.digger_with, SizeSprites.digger_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.digger_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                }

                if (lemming[actLEM].Climbing) // scale POSDraw x-5,y+6 at 1.2f x-8.y+3 at 1.35f  //puto33
                {
                    framesale = (lemming[actLEM].Actualframe * SizeSprites.climber_with);
                    spriteBatch.Draw(LemmingsNetGame.Instance.Sprites.Climber, new Vector2(lemming[actLEM].PosX - ScrollX + (lemming[actLEM].Right ? SizeSprites.climber_xpos : SizeSprites.climber_xposleft), lemming[actLEM].PosY + SizeSprites.climber_ypos - ScrollY), new Rectangle(framesale, 0, SizeSprites.climber_with, SizeSprites.climber_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.climber_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
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
                    totalTime = 0;
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
            LemmingsNetGame.Instance.Fonts.TextLem("Out:" + string.Format("{0}", numLemmings) + "/" + string.Format("{0}", Numlems), vectorFill, Color.Magenta, 1f, 0.1f, spriteBatch);
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
            millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (Exploding && dibuja3 && !MyGame.Paused)  //logic explosions particles
            {
                int _totalExploding = actItem;
                for (int Qexplo = 0; Qexplo < actItem; Qexplo++)
                {
                    int TopY = MyGame.GameResolution.Y;
                    if (earth != null)
                        TopY = earth.Height - 2;
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
            if (!LevelEnded && ((_allBlow && numlemnow == 0) || zvTime < 0 || (numLemmings == Numlems && numlemnow == 0)))
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
                    numLemmings = 0;
                    fade = true;
                    millisecondsElapsed = 0;
                    doorOn = true;
                    Frame = 0;
                    Frame2 = 0;
                    Frame3 = 0;
                    frameDoor = 0;
                    frameExit = 0;
                    rest = 0;
                    LevelEnded = false;
                    ExitLevel = false;
                    _allBlow = false;
                    zvTime = 0;
                    ExitBad = false;
                    LemmingsNetGame.Instance.ReloadContent();
                    return; //next level
                }

                if (ExitBad) //repeat level
                {
                    LemmingsNetGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                    numSaved = 0;
                    numlemnow = 0;
                    numLemmings = 0;
                    fade = true;
                    millisecondsElapsed = 0;
                    doorOn = true;
                    Frame = 0;
                    Frame2 = 0;
                    Frame3 = 0;
                    frameDoor = 0;
                    frameExit = 0;
                    rest = 0;
                    LevelEnded = false;
                    ExitLevel = false;
                    _allBlow = false;
                    zvTime = 0;
                    ExitBad = false;
                    LemmingsNetGame.Instance.ReloadContent();
                    return;
                }
                CurrentMusic.Stop();
                LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose = 0;
                LevelEnded = false;
                ExitLevel = false;
                _allBlow = false;
                zvTime = 0;
                ExitBad = false;
                numLemmings = 0;
                LemmingsNetGame.Instance.ReloadContent();
                LemmingsNetGame.Instance.BackToMenu();
                return;
            }

            if (_allBlow && actualBlow < numLemmings) // crash crash TEST TEST
            {
                if (!lemming[actualBlow].Dead && !lemming[actualBlow].Explode)
                    lemming[actualBlow].Exploser = true;
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
                ScrollX + MyGame.GameResolution.X < earth.Width)
            {
                ScrollX += 5;
            }
            if (ScrollX + MyGame.GameResolution.X > earth.Width)
            {
                ScrollX = earth.Width - MyGame.GameResolution.X;
            }
            if (mousepos.X < -10 && ScrollX > 0)
            {
                ScrollX -= 5;
            }
            if (ScrollX < 0)
            {
                ScrollX = 0;
            }
            if (mousepos.Y + 20 > MyGame.GameResolution.Y && ScrollY + 512 < earth.Height)
            {
                ScrollY += 5;
            }
            if (ScrollY + 512 > earth.Height)
            {
                ScrollY = earth.Height - 512;
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
            if (draw2 && doorOn && Frame > 30)
            {
                totalTime = 0;
                int xx55 = varDoor[AllLevel[LemmingsNetGame.Instance.CurrentLevelNumber].TypeOfDoor].numFram - 1;
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
            float delayPercent = 27 - _inGameMenu.frequencyNumber * 0.26f; // see to fix speed of lemmings release on door only when change frecuency (not so good)
            if (dibuja && !doorOn)
            {
                exitFrame++;
                if (exitFrame >= (int)delayPercent)
                {
                    exitFrame = 0;
                    pullLemmings = true;
                }
            }
            //test to see difference with anterior process
            if (pullLemmings && numLemmings != Numlems && !_allBlow)
            {
                if (numTOTdoors > 1 && moreDoors != null) // more than 1 door is different calculation
                {
                    door1Y = (int)moreDoors[numACTdoor].doorMoreXY.Y;
                    door1X = (int)moreDoors[numACTdoor].doorMoreXY.X;
                    numACTdoor++;
                    if (numACTdoor >= numTOTdoors)
                        numACTdoor = 0;
                    lemming[numLemmings].PosY = door1Y;
                    lemming[numLemmings].PosX = door1X + 35;
                }
                else
                {
                    lemming[numLemmings].PosY = door1Y;
                    lemming[numLemmings].PosX = door1X + 35;
                }
                lemming[numLemmings].PosY = door1Y;
                lemming[numLemmings].PosX = door1X + 35;
                lemming[numLemmings].Numframes = 0;
                lemming[numLemmings].Right = true;
                lemming[numLemmings].Fall = true;
                lemming[numLemmings].Walker = false;
                lemming[numLemmings].Pixelscaida = 0;
                lemming[numLemmings].Numframes = SizeSprites.faller_frames;
                lemming[numLemmings].Actualframe = 0;
                lemming[numLemmings].Onmouse = false;
                lemming[numLemmings].Active = true;
                lemming[numLemmings].Exit = false;
                lemming[numLemmings].Dead = false;
                lemming[numLemmings].Digger = false;
                lemming[numLemmings].Climber = false;
                lemming[numLemmings].Climbing = false;
                lemming[numLemmings].Umbrella = false;
                lemming[numLemmings].Falling = false;
                lemming[numLemmings].Framescut = false;
                lemming[numLemmings].Breakfloor = false;
                lemming[numLemmings].Exploser = false;
                lemming[numLemmings].Explode = false;
                lemming[numLemmings].Time = 0;
                lemming[numLemmings].Blocker = false;
                lemming[numLemmings].Builder = false;
                lemming[numLemmings].Basher = false;
                lemming[numLemmings].Miner = false;
                lemming[numLemmings].Bridge = false;
                lemming[numLemmings].Burned = false;
                lemming[numLemmings].Drown = false;
                numLemmings++;
                numlemnow++;
            }

            for (actLEM2 = 0; actLEM2 < numLemmings; actLEM2++)
            {
                x.X = lemming[actLEM2].PosX + 14;
                x.Y = lemming[actLEM2].PosY + 25;
                if (lemming[actLEM2].Exit && lemming[actLEM2].Actualframe == 13) // change frame of yipee sound, old frame was init or 0 now different for frames
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
                    if (exit_rect.Contains(x) && !lemming[actLEM2].Exit && !lemming[actLEM2].Explode)
                    {
                        lemming[actLEM2].PosX = output1X - 19;
                        lemming[actLEM2].PosY = output1Y - 30;
                        lemming[actLEM2].Active = false;
                        lemming[actLEM2].Walker = false;
                        lemming[actLEM2].Fall = false;
                        lemming[actLEM2].Falling = false;
                        lemming[actLEM2].Exit = true;
                        lemming[actLEM2].Numframes = SizeSprites.sale_frames;
                        lemming[actLEM2].Actualframe = 0;
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
                        if (exit_rect.Contains(x) && !lemming[actLEM2].Exit && !lemming[actLEM2].Explode)
                        {
                            lemming[actLEM2].PosX = output1X - 19; //14+5 middle of the exit rect
                            lemming[actLEM2].PosY = output1Y - 30; //25+5
                            lemming[actLEM2].Active = false;
                            lemming[actLEM2].Walker = false;
                            lemming[actLEM2].Fall = false;
                            lemming[actLEM2].Falling = false;
                            lemming[actLEM2].Exit = true;
                            lemming[actLEM2].Numframes = SizeSprites.sale_frames;
                            lemming[actLEM2].Actualframe = 0; // break; //i'm not sure if it's necessary this break
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[17].doorX, AllLevel[17].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(AllLevel[17].doorX + 220, AllLevel[17].doorY);
                    moreDoors[2].doorMoreXY = new Vector2(AllLevel[17].doorX + 430, AllLevel[17].doorY);
                    moreDoors[3].doorMoreXY = new Vector2(AllLevel[17].doorX + 640, AllLevel[17].doorY);//IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
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
                    moreexits[0].exitMoreXY = new Vector2(AllLevel[23].exitX, AllLevel[23].exitY); //73,460 ----- LEVEL 23 TWO EXITS
                    moreexits[1].exitMoreXY = new Vector2(AllLevel[23].exitX, 180);//73,180 //IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[40].doorX, AllLevel[40].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(2240, AllLevel[40].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[59].doorX, AllLevel[59].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[62].doorX, AllLevel[62].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(1962, AllLevel[62].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[64].doorX, AllLevel[64].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(1174, AllLevel[64].doorY);
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
                    moreexits[0].exitMoreXY = new Vector2(AllLevel[65].exitX, AllLevel[65].exitY);
                    moreexits[1].exitMoreXY = new Vector2(AllLevel[65].exitX, 461);
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
                    moreexits[0].exitMoreXY = new Vector2(AllLevel[77].exitX, AllLevel[77].exitY);
                    moreexits[1].exitMoreXY = new Vector2(AllLevel[77].exitX, 180);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[86].doorX, AllLevel[86].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[108].doorX, AllLevel[108].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(1500, AllLevel[108].doorY);
                    moreDoors[2].doorMoreXY = new Vector2(AllLevel[108].doorX, 376);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[120].doorX, AllLevel[120].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(4094, AllLevel[120].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[134].doorX, AllLevel[134].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[138].doorX, AllLevel[138].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(AllLevel[138].doorX - 300, AllLevel[138].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[141].doorX, AllLevel[141].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(AllLevel[141].doorX + 400, AllLevel[141].doorY);
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
                    moreDoors[1].doorMoreXY = new Vector2(AllLevel[144].doorX, AllLevel[144].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[159].doorX, AllLevel[159].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[160].doorX, AllLevel[160].doorY);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[162].doorX, AllLevel[162].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(AllLevel[162].doorX + 180, AllLevel[162].doorY);
                    moreDoors[2].doorMoreXY = new Vector2(AllLevel[162].doorX, 345);
                    SteelON = true; numTOTsteel = 2;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(458, 0, 501 - 458, 319);
                    steel[1].area = new Rectangle(145, 269, 277 - 145, 320 - 269);
                    break;
                case 163:
                    numTOTdoors = 3;
                    numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[163].doorX, AllLevel[163].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(AllLevel[163].doorX, 220);
                    moreDoors[2].doorMoreXY = new Vector2(AllLevel[163].doorX, 382);
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
                    moreDoors[0].doorMoreXY = new Vector2(AllLevel[179].doorX, AllLevel[179].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(AllLevel[179].doorX + 100, AllLevel[179].doorY + 160);
                    moreDoors[2].doorMoreXY = new Vector2(AllLevel[179].doorX + 190, AllLevel[179].doorY + 330);
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

        internal void VariablesLevels()
        {
            varDoor[1].xWidth = 96; //96x500 -- 10 frames
            varDoor[1].yWidth = 50;
            varDoor[1].numFram = 10;
            varDoor[2].xWidth = 96;
            varDoor[2].yWidth = 50;
            varDoor[2].numFram = 10;
            varDoor[3].xWidth = 96;
            varDoor[3].yWidth = 50;
            varDoor[3].numFram = 10;
            varDoor[4].xWidth = 96; //96x432 -- 9 frames
            varDoor[4].yWidth = 48;
            varDoor[4].numFram = 9;
            varDoor[5].xWidth = 96; //96x432 -- 9 frames
            varDoor[5].yWidth = 50;
            varDoor[5].numFram = 10;
            varDoor[6].xWidth = 96; //96x432 -- 9 frames
            varDoor[6].yWidth = 50;
            varDoor[6].numFram = 10;
            varDoor[7].xWidth = 96; //96x432 -- 9 frames
            varDoor[7].yWidth = 50;
            varDoor[7].numFram = 10;
            varDoor[8].xWidth = 96; //96x432 -- 9 frames
            varDoor[8].yWidth = 48;
            varDoor[8].numFram = 8;

            // position of exit sprites are taken from level[].exitX & Y -- exit rectangle trap 10x10 size
            varExit[1].xWidth = 96;
            varExit[1].yWidth = 16;
            varExit[1].numFram = 6;
            varExit[1].moreX = 53;  // values x & y of the animation exit sprite
            varExit[1].moreY = 50;
            varExit[1].moreX2 = 53; // values x & y of exit principal sprite with no animation
            varExit[1].moreY2 = 34;
            varExit[2].xWidth = 64;
            varExit[2].yWidth = 26;
            varExit[2].numFram = 4;
            varExit[2].moreX = 37;
            varExit[2].moreY = 38;
            varExit[2].moreX2 = 37;
            varExit[2].moreY2 = 70;
            varExit[3].xWidth = 96;
            varExit[3].yWidth = 48;
            varExit[3].numFram = 6;
            varExit[3].moreX = 44;
            varExit[3].moreY = 104;
            varExit[3].moreX2 = 44;
            varExit[3].moreY2 = 56;
            varExit[4].xWidth = 96;
            varExit[4].yWidth = 16;
            varExit[4].numFram = 6;
            varExit[4].moreX = 53;
            varExit[4].moreY = 50;
            varExit[4].moreX2 = 53;
            varExit[4].moreY2 = 34;
            varExit[5].xWidth = 16;
            varExit[5].yWidth = 16;
            varExit[5].numFram = 14;
            varExit[5].moreX = -19;  // values x & y of the animation exit sprite
            varExit[5].moreY = 16;
            varExit[5].moreX2 = 35; // values x & y of exit principal sprite with no animation
            varExit[5].moreY2 = 64;
            varExit[6].xWidth = 16;
            varExit[6].yWidth = 16;
            varExit[6].numFram = 14;
            varExit[6].moreX = -19;  // values x & y of the animation exit sprite
            varExit[6].moreY = 76;
            varExit[6].moreX2 = 37; // values x & y of exit principal sprite with no animation
            varExit[6].moreY2 = 60;
            varExit[7].xWidth = 96;
            varExit[7].yWidth = 16;
            varExit[7].numFram = 6;
            varExit[7].moreX = 45;
            varExit[7].moreY = 47;
            varExit[7].moreX2 = 45;
            varExit[7].moreY2 = 31;
            varExit[8].xWidth = 32;
            varExit[8].yWidth = 16;
            varExit[8].numFram = 7;
            varExit[8].moreX = 18; //animation
            varExit[8].moreY = 79;
            varExit[8].moreX2 = 50;
            varExit[8].moreY2 = 63;
            varExit[9].xWidth = 96;
            varExit[9].yWidth = 16;
            varExit[9].numFram = 6;
            varExit[9].moreX = 53;
            varExit[9].moreY = 47;
            varExit[9].moreX2 = 53;
            varExit[9].moreY2 = 31;
            varExit[10].xWidth = 32;
            varExit[10].yWidth = 16;
            varExit[10].numFram = 6;
            varExit[10].moreX = 20; //Animation
            varExit[10].moreY = 135;
            varExit[10].moreX2 = 40;
            varExit[10].moreY2 = 120;
            // fun fun fun fun levels
            AllLevel[1].TotalLemmings = 10;
            AllLevel[1].InitPosX = 200;
            AllLevel[1].NameLev = "levels/fun/fun001"; // fun001
            AllLevel[1].nameOfLevel = "Just dig!";
            AllLevel[1].TypeOfDoor = 3; // 1 to 4
            AllLevel[1].doorX = 670;
            AllLevel[1].doorY = 130;
            AllLevel[1].TypeOfExit = 5; // 1 to 6 //1-4 animated too /5-6 alone for now... see sprite size too...
            AllLevel[1].exitX = 1185;
            AllLevel[1].exitY = 416;
            AllLevel[1].numberClimbers = 0;
            AllLevel[1].numberUmbrellas = 0;
            AllLevel[1].numberExploders = 0;
            AllLevel[1].numberBlockers = 0;
            AllLevel[1].numberBuilders = 0;
            AllLevel[1].numberBashers = 0;
            AllLevel[1].numberMiners = 0;
            AllLevel[1].numberDiggers = 10;
            AllLevel[1].MinFrequencyComming = 50;
            AllLevel[1].FrequencyComming = 50;
            AllLevel[1].NbLemmingsToSave = 1; // 10% OF 10 = 1
            AllLevel[1].totalTime = 5;
            AllLevel[2].TotalLemmings = 10;
            AllLevel[2].InitPosX = 200;
            AllLevel[2].NameLev = "levels/fun/fun002";
            AllLevel[2].nameOfLevel = "Only floaters can survive this!";
            AllLevel[2].TypeOfDoor = 1; // 1 to 4
            AllLevel[2].doorX = 310;
            AllLevel[2].doorY = 28;
            AllLevel[2].TypeOfExit = 1; // 1 to 6 //1-4 animated too /5-6 alone for now... see sprite size too...
            AllLevel[2].exitX = 1178;
            AllLevel[2].exitY = 477;
            AllLevel[2].numberClimbers = 0;
            AllLevel[2].numberUmbrellas = 10;
            AllLevel[2].numberExploders = 0;
            AllLevel[2].numberBlockers = 0;
            AllLevel[2].numberBuilders = 0;
            AllLevel[2].numberBashers = 0;
            AllLevel[2].numberMiners = 0;
            AllLevel[2].numberDiggers = 0;
            AllLevel[2].MinFrequencyComming = 50;
            AllLevel[2].FrequencyComming = 50;
            AllLevel[2].NbLemmingsToSave = 1; // 10% OF 10 = 1
            AllLevel[2].totalTime = 5;
            AllLevel[3].TotalLemmings = 50;
            AllLevel[3].InitPosX = 44; // Init xscroll
            AllLevel[3].NameLev = "levels/fun/fun003";
            AllLevel[3].nameOfLevel = "Taylor-made for blockers";
            AllLevel[3].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[3].doorX = 472;
            AllLevel[3].doorY = 39;
            AllLevel[3].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[3].exitX = 474;
            AllLevel[3].exitY = 489;
            AllLevel[3].numberClimbers = 0;
            AllLevel[3].numberUmbrellas = 0;
            AllLevel[3].numberExploders = 0;
            AllLevel[3].numberBlockers = 10;
            AllLevel[3].numberBuilders = 0;
            AllLevel[3].numberBashers = 0;
            AllLevel[3].numberMiners = 0;
            AllLevel[3].numberDiggers = 0;
            AllLevel[3].MinFrequencyComming = 50;
            AllLevel[3].FrequencyComming = 50;
            AllLevel[3].NbLemmingsToSave = 5; // 10% OF 50 = 5
            AllLevel[3].totalTime = 5;
            AllLevel[4].TotalLemmings = 10;
            AllLevel[4].InitPosX = 291; // Init xscroll
            AllLevel[4].NameLev = "levels/fun/fun004";
            AllLevel[4].nameOfLevel = "Now use miners && climbers";
            AllLevel[4].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[4].doorX = 627;
            AllLevel[4].doorY = 35;
            AllLevel[4].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[4].exitX = 1261;
            AllLevel[4].exitY = 153;
            AllLevel[4].numberClimbers = 10;
            AllLevel[4].numberUmbrellas = 0;
            AllLevel[4].numberExploders = 0;
            AllLevel[4].numberBlockers = 0;
            AllLevel[4].numberBuilders = 0;
            AllLevel[4].numberBashers = 0;
            AllLevel[4].numberMiners = 1;
            AllLevel[4].numberDiggers = 0;
            AllLevel[4].MinFrequencyComming = 1;
            AllLevel[4].FrequencyComming = 1;
            AllLevel[4].NbLemmingsToSave = 10; // 100% OF 10 = 10
            AllLevel[4].totalTime = 5;
            AllLevel[5].TotalLemmings = 50;
            AllLevel[5].InitPosX = 0; // Init xscroll
            AllLevel[5].NameLev = "levels/fun/fun005";
            AllLevel[5].nameOfLevel = "You need bashers this time";
            AllLevel[5].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[5].doorX = 419;
            AllLevel[5].doorY = 133;
            AllLevel[5].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[5].exitX = 2002;
            AllLevel[5].exitY = 427;
            AllLevel[5].numberClimbers = 0;
            AllLevel[5].numberUmbrellas = 0;
            AllLevel[5].numberExploders = 0;
            AllLevel[5].numberBlockers = 0;
            AllLevel[5].numberBuilders = 0;
            AllLevel[5].numberBashers = 50;
            AllLevel[5].numberMiners = 0;
            AllLevel[5].numberDiggers = 0;
            AllLevel[5].MinFrequencyComming = 50;
            AllLevel[5].FrequencyComming = 50;
            AllLevel[5].NbLemmingsToSave = 5; // 10% OF 50 = 5
            AllLevel[5].totalTime = 5;
            AllLevel[6].TotalLemmings = 50;
            AllLevel[6].InitPosX = 623; // Init xscroll
            AllLevel[6].NameLev = "levels/fun/fun006";
            AllLevel[6].nameOfLevel = "A task for blockers and bombers";
            AllLevel[6].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[6].doorX = 773;
            AllLevel[6].doorY = 72;
            AllLevel[6].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[6].exitX = 1609;
            AllLevel[6].exitY = 330;
            AllLevel[6].numberClimbers = 0;
            AllLevel[6].numberUmbrellas = 0;
            AllLevel[6].numberExploders = 5;
            AllLevel[6].numberBlockers = 5;
            AllLevel[6].numberBuilders = 0;
            AllLevel[6].numberBashers = 0;
            AllLevel[6].numberMiners = 0;
            AllLevel[6].numberDiggers = 0;
            AllLevel[6].MinFrequencyComming = 50;
            AllLevel[6].FrequencyComming = 50;
            AllLevel[6].NbLemmingsToSave = 10; // 20% OF 50 = 10
            AllLevel[6].totalTime = 5;
            AllLevel[7].TotalLemmings = 50;
            AllLevel[7].InitPosX = 170; // Init xscroll
            AllLevel[7].NameLev = "levels/fun/fun007";
            AllLevel[7].nameOfLevel = "Builders will help you here";
            AllLevel[7].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[7].doorX = 670;
            AllLevel[7].doorY = 290;
            AllLevel[7].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[7].exitX = 2485;
            AllLevel[7].exitY = 306;
            AllLevel[7].numberClimbers = 0;
            AllLevel[7].numberUmbrellas = 0;
            AllLevel[7].numberExploders = 0;
            AllLevel[7].numberBlockers = 0;
            AllLevel[7].numberBuilders = 20;
            AllLevel[7].numberBashers = 0;
            AllLevel[7].numberMiners = 0;
            AllLevel[7].numberDiggers = 0;
            AllLevel[7].MinFrequencyComming = 50;
            AllLevel[7].FrequencyComming = 50;
            AllLevel[7].NbLemmingsToSave = 25; // 50% OF 50 = 25
            AllLevel[7].totalTime = 5;
            AllLevel[8].TotalLemmings = 100;
            AllLevel[8].InitPosX = 854; // Init xscroll
            AllLevel[8].NameLev = "levels/fun/fun008";
            AllLevel[8].nameOfLevel = "Not as complicated as it looks";
            AllLevel[8].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[8].doorX = 1260;
            AllLevel[8].doorY = 150;
            AllLevel[8].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[8].exitX = 1155;
            AllLevel[8].exitY = 323;
            AllLevel[8].numberClimbers = 20;
            AllLevel[8].numberUmbrellas = 20;
            AllLevel[8].numberExploders = 20;
            AllLevel[8].numberBlockers = 20;
            AllLevel[8].numberBuilders = 20;
            AllLevel[8].numberBashers = 20;
            AllLevel[8].numberMiners = 20;
            AllLevel[8].numberDiggers = 20;
            AllLevel[8].MinFrequencyComming = 88;
            AllLevel[8].FrequencyComming = 88;
            AllLevel[8].NbLemmingsToSave = 95; // 95% OF 100 = 95
            AllLevel[8].totalTime = 5;
            AllLevel[9].TotalLemmings = 100;
            AllLevel[9].InitPosX = 0; // Init xscroll
            AllLevel[9].NameLev = "levels/fun/fun009";
            AllLevel[9].nameOfLevel = "As long as you try your best";
            AllLevel[9].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[9].doorX = 236;
            AllLevel[9].doorY = 281;
            AllLevel[9].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[9].exitX = 746;
            AllLevel[9].exitY = 399;
            AllLevel[9].numberClimbers = 20;
            AllLevel[9].numberUmbrellas = 20;
            AllLevel[9].numberExploders = 20;
            AllLevel[9].numberBlockers = 20;
            AllLevel[9].numberBuilders = 20;
            AllLevel[9].numberBashers = 20;
            AllLevel[9].numberMiners = 20;
            AllLevel[9].numberDiggers = 20;
            AllLevel[9].MinFrequencyComming = 99;
            AllLevel[9].FrequencyComming = 99;
            AllLevel[9].NbLemmingsToSave = 90; // 90% OF 100 = 90
            AllLevel[9].totalTime = 5;
            AllLevel[10].TotalLemmings = 20;
            AllLevel[10].InitPosX = 573; // Init xscroll
            AllLevel[10].NameLev = "levels/fun/fun010";
            AllLevel[10].nameOfLevel = "Smile if you love lemmings";
            AllLevel[10].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[10].doorX = 1130;
            AllLevel[10].doorY = 18;
            AllLevel[10].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[10].exitX = 957;
            AllLevel[10].exitY = 462;
            AllLevel[10].numberClimbers = 20;
            AllLevel[10].numberUmbrellas = 20;
            AllLevel[10].numberExploders = 20;
            AllLevel[10].numberBlockers = 20;
            AllLevel[10].numberBuilders = 20;
            AllLevel[10].numberBashers = 20;
            AllLevel[10].numberMiners = 20;
            AllLevel[10].numberDiggers = 20;
            AllLevel[10].MinFrequencyComming = 50;
            AllLevel[10].FrequencyComming = 50;
            AllLevel[10].NbLemmingsToSave = 10; // 50% OF 10 = 20
            AllLevel[10].totalTime = 5;
            AllLevel[11].TotalLemmings = 60;
            AllLevel[11].InitPosX = 87; // Init xscroll
            AllLevel[11].NameLev = "levels/fun/fun011";
            AllLevel[11].nameOfLevel = "Keep your hair on Mr. Lemming";
            AllLevel[11].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[11].doorX = 300;
            AllLevel[11].doorY = 300;
            AllLevel[11].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[11].exitX = 1040;
            AllLevel[11].exitY = 444;
            AllLevel[11].numberClimbers = 20;
            AllLevel[11].numberUmbrellas = 20;
            AllLevel[11].numberExploders = 20;
            AllLevel[11].numberBlockers = 20;
            AllLevel[11].numberBuilders = 20;
            AllLevel[11].numberBashers = 20;
            AllLevel[11].numberMiners = 20;
            AllLevel[11].numberDiggers = 20;
            AllLevel[11].MinFrequencyComming = 50;
            AllLevel[11].FrequencyComming = 50;
            AllLevel[11].NbLemmingsToSave = 50; // 83% OF 60 = 49'8=50
            AllLevel[11].totalTime = 5;
            AllLevel[12].TotalLemmings = 80;
            AllLevel[12].InitPosX = 0; // Init xscroll
            AllLevel[12].NameLev = "levels/fun/fun012";
            AllLevel[12].nameOfLevel = "Patience";
            AllLevel[12].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[12].doorX = 12;
            AllLevel[12].doorY = 206;
            AllLevel[12].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[12].exitX = 956;
            AllLevel[12].exitY = 387;
            AllLevel[12].numberClimbers = 20;
            AllLevel[12].numberUmbrellas = 20;
            AllLevel[12].numberExploders = 20;
            AllLevel[12].numberBlockers = 20;
            AllLevel[12].numberBuilders = 20;
            AllLevel[12].numberBashers = 20;
            AllLevel[12].numberMiners = 20;
            AllLevel[12].numberDiggers = 20;
            AllLevel[12].MinFrequencyComming = 99;
            AllLevel[12].FrequencyComming = 99;
            AllLevel[12].NbLemmingsToSave = 40; // 50% OF 80 = 40
            AllLevel[12].totalTime = 5;
            AllLevel[13].TotalLemmings = 20;
            AllLevel[13].InitPosX = 120; // Init xscroll
            AllLevel[13].NameLev = "levels/fun/fun013";
            AllLevel[13].nameOfLevel = "We all fall down";
            AllLevel[13].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[13].doorX = 556;
            AllLevel[13].doorY = 65;
            AllLevel[13].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[13].exitX = 2515;
            AllLevel[13].exitY = 383;
            AllLevel[13].numberClimbers = 0;
            AllLevel[13].numberUmbrellas = 0;
            AllLevel[13].numberExploders = 0;
            AllLevel[13].numberBlockers = 0;
            AllLevel[13].numberBuilders = 0;
            AllLevel[13].numberBashers = 0;
            AllLevel[13].numberMiners = 0;
            AllLevel[13].numberDiggers = 20;
            AllLevel[13].MinFrequencyComming = 1;
            AllLevel[13].FrequencyComming = 1;
            AllLevel[13].NbLemmingsToSave = 20; // 100%
            AllLevel[13].totalTime = 3;
            AllLevel[14].TotalLemmings = 80;
            AllLevel[14].InitPosX = 120; // Init xscroll
            AllLevel[14].NameLev = "levels/fun/fun014";
            AllLevel[14].nameOfLevel = "Origins and Lemmings";
            AllLevel[14].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[14].doorX = 299;
            AllLevel[14].doorY = 65;
            AllLevel[14].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[14].exitX = 2796;
            AllLevel[14].exitY = 330;
            AllLevel[14].numberClimbers = 20;
            AllLevel[14].numberUmbrellas = 20;
            AllLevel[14].numberExploders = 20;
            AllLevel[14].numberBlockers = 20;
            AllLevel[14].numberBuilders = 20;
            AllLevel[14].numberBashers = 20;
            AllLevel[14].numberMiners = 20;
            AllLevel[14].numberDiggers = 20;
            AllLevel[14].MinFrequencyComming = 20;
            AllLevel[14].FrequencyComming = 20;
            AllLevel[14].NbLemmingsToSave = 60; // 75% of 80 = 60 (75*80/100)
            AllLevel[14].totalTime = 6;
            AllLevel[15].TotalLemmings = 100;
            AllLevel[15].InitPosX = 0; // Init xscroll
            AllLevel[15].NameLev = "levels/fun/fun015";
            AllLevel[15].nameOfLevel = "Don't let youe eyes deceive you";
            AllLevel[15].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[15].doorX = 170;
            AllLevel[15].doorY = 65;
            AllLevel[15].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[15].exitX = 3667;
            AllLevel[15].exitY = 203;
            AllLevel[15].numberClimbers = 20;
            AllLevel[15].numberUmbrellas = 20;
            AllLevel[15].numberExploders = 20;
            AllLevel[15].numberBlockers = 20;
            AllLevel[15].numberBuilders = 20;
            AllLevel[15].numberBashers = 20;
            AllLevel[15].numberMiners = 20;
            AllLevel[15].numberDiggers = 20;
            AllLevel[15].MinFrequencyComming = 40;
            AllLevel[15].FrequencyComming = 40;
            AllLevel[15].NbLemmingsToSave = 80; // 80% of 100 = 80 (??*??/100)
            AllLevel[15].totalTime = 8;
            AllLevel[16].TotalLemmings = 80;
            AllLevel[16].InitPosX = 0; // Init xscroll
            AllLevel[16].NameLev = "levels/fun/fun016";
            AllLevel[16].nameOfLevel = "Don't do anything too hasty";
            AllLevel[16].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[16].doorX = 488;
            AllLevel[16].doorY = 275;
            AllLevel[16].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[16].exitX = 1709;
            AllLevel[16].exitY = 214;
            AllLevel[16].numberClimbers = 20;
            AllLevel[16].numberUmbrellas = 20;
            AllLevel[16].numberExploders = 20;
            AllLevel[16].numberBlockers = 20;
            AllLevel[16].numberBuilders = 20;
            AllLevel[16].numberBashers = 20;
            AllLevel[16].numberMiners = 20;
            AllLevel[16].numberDiggers = 20;
            AllLevel[16].MinFrequencyComming = 1;
            AllLevel[16].FrequencyComming = 1;
            AllLevel[16].NbLemmingsToSave = 50; // 62% of 80 = 49.6 (??*??/100)
            AllLevel[16].totalTime = 8;
            AllLevel[17].TotalLemmings = 50; // it has 4 doors SPECIAL ONE LEVEL SEE
            AllLevel[17].InitPosX = 139; // Init xscroll
            AllLevel[17].NameLev = "levels/fun/fun017";
            AllLevel[17].nameOfLevel = "Easy when you know how";
            AllLevel[17].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[17].doorX = 288;
            AllLevel[17].doorY = 145;
            AllLevel[17].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[17].exitX = 1358;
            AllLevel[17].exitY = 433;
            AllLevel[17].numberClimbers = 20;
            AllLevel[17].numberUmbrellas = 20;
            AllLevel[17].numberExploders = 20;
            AllLevel[17].numberBlockers = 20;
            AllLevel[17].numberBuilders = 20;
            AllLevel[17].numberBashers = 20;
            AllLevel[17].numberMiners = 20;
            AllLevel[17].numberDiggers = 20;
            AllLevel[17].MinFrequencyComming = 99;
            AllLevel[17].FrequencyComming = 99;
            AllLevel[17].NbLemmingsToSave = 20; // 40% of 50 = 20 (??*??/100)
            AllLevel[17].totalTime = 5;
            AllLevel[18].TotalLemmings = 70;
            AllLevel[18].InitPosX = 0; // Init xscroll
            AllLevel[18].NameLev = "levels/fun/fun018";
            AllLevel[18].nameOfLevel = "Let's block and blow";
            AllLevel[18].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[18].doorX = 60;
            AllLevel[18].doorY = 15;
            AllLevel[18].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[18].exitX = 1562;
            AllLevel[18].exitY = 452;
            AllLevel[18].numberClimbers = 0;
            AllLevel[18].numberUmbrellas = 0;
            AllLevel[18].numberExploders = 20;
            AllLevel[18].numberBlockers = 20;
            AllLevel[18].numberBuilders = 0;
            AllLevel[18].numberBashers = 0;
            AllLevel[18].numberMiners = 0;
            AllLevel[18].numberDiggers = 0;
            AllLevel[18].MinFrequencyComming = 80;
            AllLevel[18].FrequencyComming = 80;
            AllLevel[18].NbLemmingsToSave = 50; // 71% of 70 = 49.7 (??*??/100)
            AllLevel[18].totalTime = 5;
            AllLevel[19].TotalLemmings = 100;
            AllLevel[19].InitPosX = 300; // Init xscroll
            AllLevel[19].NameLev = "levels/fun/fun019";
            AllLevel[19].nameOfLevel = "Take good care of my lemmings";
            AllLevel[19].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[19].doorX = 740;
            AllLevel[19].doorY = 260;
            AllLevel[19].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[19].exitX = 4030;
            AllLevel[19].exitY = 263;
            AllLevel[19].numberClimbers = 20;
            AllLevel[19].numberUmbrellas = 20;
            AllLevel[19].numberExploders = 20;
            AllLevel[19].numberBlockers = 20;
            AllLevel[19].numberBuilders = 20;
            AllLevel[19].numberBashers = 20;
            AllLevel[19].numberMiners = 20;
            AllLevel[19].numberDiggers = 20;
            AllLevel[19].MinFrequencyComming = 20;
            AllLevel[19].FrequencyComming = 20;
            AllLevel[19].NbLemmingsToSave = 70; // 70% of 100 = 70 (??*??/100)
            AllLevel[19].totalTime = 5;
            AllLevel[20].TotalLemmings = 50;
            AllLevel[20].InitPosX = 875; // Init xscroll
            AllLevel[20].NameLev = "levels/fun/fun020";
            AllLevel[20].nameOfLevel = "We are now at LEMCON ONE";
            AllLevel[20].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[20].doorX = 1160;
            AllLevel[20].doorY = 130;
            AllLevel[20].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[20].exitX = 2298;
            AllLevel[20].exitY = 334;
            AllLevel[20].numberClimbers = 20;
            AllLevel[20].numberUmbrellas = 20;
            AllLevel[20].numberExploders = 20;
            AllLevel[20].numberBlockers = 20;
            AllLevel[20].numberBuilders = 20;
            AllLevel[20].numberBashers = 20;
            AllLevel[20].numberMiners = 20;
            AllLevel[20].numberDiggers = 20;
            AllLevel[20].MinFrequencyComming = 10;
            AllLevel[20].FrequencyComming = 10;
            AllLevel[20].NbLemmingsToSave = 30; // 60% of 50 = 30 (??*??/100)
            AllLevel[20].totalTime = 5;
            AllLevel[21].TotalLemmings = 100;
            AllLevel[21].InitPosX = 0; // Init xscroll
            AllLevel[21].NameLev = "levels/fun/fun021";
            AllLevel[21].nameOfLevel = "You Live and Lem";
            AllLevel[21].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[21].doorX = 260;
            AllLevel[21].doorY = 160;
            AllLevel[21].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[21].exitX = 3486;
            AllLevel[21].exitY = 459;
            AllLevel[21].numberClimbers = 20;
            AllLevel[21].numberUmbrellas = 20;
            AllLevel[21].numberExploders = 20;
            AllLevel[21].numberBlockers = 20;
            AllLevel[21].numberBuilders = 20;
            AllLevel[21].numberBashers = 20;
            AllLevel[21].numberMiners = 20;
            AllLevel[21].numberDiggers = 20;
            AllLevel[21].MinFrequencyComming = 50;
            AllLevel[21].FrequencyComming = 50;
            AllLevel[21].NbLemmingsToSave = 60; // 60% of 100 = 60 (??*??/100)
            AllLevel[21].totalTime = 8;
            AllLevel[22].TotalLemmings = 100;
            AllLevel[22].InitPosX = 0; // Init xscroll
            AllLevel[22].NameLev = "levels/fun/fun022";
            AllLevel[22].nameOfLevel = "A Beast of a level";
            AllLevel[22].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[22].doorX = 260;
            AllLevel[22].doorY = 280;
            AllLevel[22].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[22].exitX = 2566;
            AllLevel[22].exitY = 225;
            AllLevel[22].numberClimbers = 20;
            AllLevel[22].numberUmbrellas = 20;
            AllLevel[22].numberExploders = 20;
            AllLevel[22].numberBlockers = 20;
            AllLevel[22].numberBuilders = 20;
            AllLevel[22].numberBashers = 20;
            AllLevel[22].numberMiners = 20;
            AllLevel[22].numberDiggers = 20;
            AllLevel[22].MinFrequencyComming = 50;
            AllLevel[22].FrequencyComming = 50;
            AllLevel[22].NbLemmingsToSave = 80; // 80% of 100 = 80 (??*??/100)
            AllLevel[22].totalTime = 5;
            AllLevel[23].TotalLemmings = 80;  //two exits special one level SEE SEE SEE
            AllLevel[23].InitPosX = 1833; // Init xscroll
            AllLevel[23].NameLev = "levels/fun/fun023";
            AllLevel[23].nameOfLevel = "I've lost that Lemming feeling";
            AllLevel[23].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[23].doorX = 2027;
            AllLevel[23].doorY = 247;
            AllLevel[23].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[23].exitX = 73;
            AllLevel[23].exitY = 460;
            AllLevel[23].numberClimbers = 20;
            AllLevel[23].numberUmbrellas = 20;
            AllLevel[23].numberExploders = 20;
            AllLevel[23].numberBlockers = 20;
            AllLevel[23].numberBuilders = 20;
            AllLevel[23].numberBashers = 20;
            AllLevel[23].numberMiners = 20;
            AllLevel[23].numberDiggers = 20;
            AllLevel[23].MinFrequencyComming = 50;
            AllLevel[23].FrequencyComming = 50;
            AllLevel[23].NbLemmingsToSave = 20; // 25% of 80 = 20 (??*??/100)
            AllLevel[23].totalTime = 8;
            AllLevel[24].TotalLemmings = 30;  //two exits special one level SEE SEE SEE
            AllLevel[24].InitPosX = 294; // Init xscroll
            AllLevel[24].NameLev = "levels/fun/fun024";
            AllLevel[24].nameOfLevel = "Konbanwa Lemming san";
            AllLevel[24].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[24].doorX = 450;
            AllLevel[24].doorY = 38;
            AllLevel[24].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[24].exitX = 1393;
            AllLevel[24].exitY = 458;
            AllLevel[24].numberClimbers = 20;
            AllLevel[24].numberUmbrellas = 20;
            AllLevel[24].numberExploders = 20;
            AllLevel[24].numberBlockers = 20;
            AllLevel[24].numberBuilders = 20;
            AllLevel[24].numberBashers = 20;
            AllLevel[24].numberMiners = 20;
            AllLevel[24].numberDiggers = 20;
            AllLevel[24].MinFrequencyComming = 99;
            AllLevel[24].FrequencyComming = 99;
            AllLevel[24].NbLemmingsToSave = 20; // 66% of 30 = 19.8 (??*??/100)
            AllLevel[24].totalTime = 5;
            AllLevel[25].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[25].InitPosX = 0; // Init xscroll
            AllLevel[25].NameLev = "levels/fun/fun025";
            AllLevel[25].nameOfLevel = "Lemmings Lemmings everywhere";
            AllLevel[25].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[25].doorX = 260;
            AllLevel[25].doorY = 300;
            AllLevel[25].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock 7-crystal
            AllLevel[25].exitX = 1460;
            AllLevel[25].exitY = 495;
            AllLevel[25].numberClimbers = 20;
            AllLevel[25].numberUmbrellas = 20;
            AllLevel[25].numberExploders = 20;
            AllLevel[25].numberBlockers = 20;
            AllLevel[25].numberBuilders = 20;
            AllLevel[25].numberBashers = 20;
            AllLevel[25].numberMiners = 20;
            AllLevel[25].numberDiggers = 20;
            AllLevel[25].MinFrequencyComming = 99;
            AllLevel[25].FrequencyComming = 99;
            AllLevel[25].NbLemmingsToSave = 50; // 50% of 100 = 50 (??*??/100)
            AllLevel[25].totalTime = 5;
            AllLevel[26].TotalLemmings = 2;  //two exits special one level SEE SEE SEE
            AllLevel[26].InitPosX = 1500; // Init xscroll
            AllLevel[26].NameLev = "levels/fun/fun026";
            AllLevel[26].nameOfLevel = "Nightmare on Lem Street";
            AllLevel[26].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[26].doorX = 1652;
            AllLevel[26].doorY = 50;
            AllLevel[26].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[26].exitX = 2587;
            AllLevel[26].exitY = 309;
            AllLevel[26].numberClimbers = 20;
            AllLevel[26].numberUmbrellas = 20;
            AllLevel[26].numberExploders = 20;
            AllLevel[26].numberBlockers = 20;
            AllLevel[26].numberBuilders = 20;
            AllLevel[26].numberBashers = 20;
            AllLevel[26].numberMiners = 20;
            AllLevel[26].numberDiggers = 20;
            AllLevel[26].MinFrequencyComming = 30;
            AllLevel[26].FrequencyComming = 30;
            AllLevel[26].NbLemmingsToSave = 2; // 100% of 2 = 2 (??*??/100)
            AllLevel[26].totalTime = 5;
            AllLevel[27].TotalLemmings = 50;  //two exits special one level SEE SEE SEE
            AllLevel[27].InitPosX = 520; // Init xscroll
            AllLevel[27].NameLev = "levels/fun/fun027";
            AllLevel[27].nameOfLevel = "Let's be careful out there";
            AllLevel[27].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[27].doorX = 812;
            AllLevel[27].doorY = 128;
            AllLevel[27].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[27].exitX = 1545;
            AllLevel[27].exitY = 482;
            AllLevel[27].numberClimbers = 20;
            AllLevel[27].numberUmbrellas = 20;
            AllLevel[27].numberExploders = 20;
            AllLevel[27].numberBlockers = 20;
            AllLevel[27].numberBuilders = 20;
            AllLevel[27].numberBashers = 20;
            AllLevel[27].numberMiners = 20;
            AllLevel[27].numberDiggers = 20;
            AllLevel[27].MinFrequencyComming = 1;
            AllLevel[27].FrequencyComming = 1;
            AllLevel[27].NbLemmingsToSave = 25; // 50% of 50 = 25 (??*??/100)
            AllLevel[27].totalTime = 5;
            AllLevel[28].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[28].InitPosX = 1075; // Init xscroll
            AllLevel[28].NameLev = "levels/fun/fun028";
            AllLevel[28].nameOfLevel = "If only they could fly";
            AllLevel[28].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[28].doorX = 1828;
            AllLevel[28].doorY = 10;
            AllLevel[28].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[28].exitX = 822;
            AllLevel[28].exitY = 203;
            AllLevel[28].numberClimbers = 30;
            AllLevel[28].numberUmbrellas = 30;
            AllLevel[28].numberExploders = 30;
            AllLevel[28].numberBlockers = 30;
            AllLevel[28].numberBuilders = 30;
            AllLevel[28].numberBashers = 30;
            AllLevel[28].numberMiners = 30;
            AllLevel[28].numberDiggers = 30;
            AllLevel[28].MinFrequencyComming = 80;
            AllLevel[28].FrequencyComming = 80;
            AllLevel[28].NbLemmingsToSave = 60; // 60% of 100 = 60 (??*??/100)
            AllLevel[28].totalTime = 5;
            AllLevel[29].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[29].InitPosX = 1274; // Init xscroll
            AllLevel[29].NameLev = "levels/fun/fun029";
            AllLevel[29].nameOfLevel = "worra lorra Lemmings";
            AllLevel[29].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[29].doorX = 1810;
            AllLevel[29].doorY = 249;
            AllLevel[29].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[29].exitX = 83;
            AllLevel[29].exitY = 198;
            AllLevel[29].numberClimbers = 30;
            AllLevel[29].numberUmbrellas = 30;
            AllLevel[29].numberExploders = 30;
            AllLevel[29].numberBlockers = 30;
            AllLevel[29].numberBuilders = 30;
            AllLevel[29].numberBashers = 30;
            AllLevel[29].numberMiners = 30;
            AllLevel[29].numberDiggers = 30;
            AllLevel[29].MinFrequencyComming = 90;
            AllLevel[29].FrequencyComming = 90;
            AllLevel[29].NbLemmingsToSave = 60; // 60% of 100 = 60 (??*??/100)
            AllLevel[29].totalTime = 8;
            AllLevel[30].TotalLemmings = 60;  //two exits special one level SEE SEE SEE
            AllLevel[30].InitPosX = 372; // Init xscroll
            AllLevel[30].NameLev = "levels/fun/fun030";
            AllLevel[30].nameOfLevel = "Lock up your Lemmings";
            AllLevel[30].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[30].doorX = 722;
            AllLevel[30].doorY = 53;
            AllLevel[30].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[30].exitX = 594;
            AllLevel[30].exitY = 473;
            AllLevel[30].numberClimbers = 20;
            AllLevel[30].numberUmbrellas = 20;
            AllLevel[30].numberExploders = 20;
            AllLevel[30].numberBlockers = 20;
            AllLevel[30].numberBuilders = 20;
            AllLevel[30].numberBashers = 20;
            AllLevel[30].numberMiners = 20;
            AllLevel[30].numberDiggers = 20;
            AllLevel[30].MinFrequencyComming = 10;
            AllLevel[30].FrequencyComming = 10;
            AllLevel[30].NbLemmingsToSave = 40; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[30].totalTime = 5;
            //tricky levels tricky levels////////////////////////////////////////////////////////////////////////////////////
            AllLevel[31].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[31].InitPosX = 0; // Init xscroll
            AllLevel[31].NameLev = "levels/tri/tri001";
            AllLevel[31].nameOfLevel = "This must be easy";
            AllLevel[31].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[31].doorX = 325;
            AllLevel[31].doorY = 258;
            AllLevel[31].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[31].exitX = 2491;
            AllLevel[31].exitY = 375;
            AllLevel[31].numberClimbers = 10;
            AllLevel[31].numberUmbrellas = 10;
            AllLevel[31].numberExploders = 10;
            AllLevel[31].numberBlockers = 10;
            AllLevel[31].numberBuilders = 10;
            AllLevel[31].numberBashers = 10;
            AllLevel[31].numberMiners = 10;
            AllLevel[31].numberDiggers = 10;
            AllLevel[31].MinFrequencyComming = 50;
            AllLevel[31].FrequencyComming = 50;
            AllLevel[31].NbLemmingsToSave = 50; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[31].totalTime = 6;
            AllLevel[32].TotalLemmings = 15;  //two exits special one level SEE SEE SEE
            AllLevel[32].InitPosX = 0; // Init xscroll
            AllLevel[32].NameLev = "levels/tri/tri002";
            AllLevel[32].nameOfLevel = "All we end falling";
            AllLevel[32].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[32].doorX = 542;
            AllLevel[32].doorY = 77;
            AllLevel[32].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[32].exitX = 2528;
            AllLevel[32].exitY = 384;
            AllLevel[32].numberClimbers = 0;
            AllLevel[32].numberUmbrellas = 0;
            AllLevel[32].numberExploders = 0;
            AllLevel[32].numberBlockers = 0;
            AllLevel[32].numberBuilders = 0;
            AllLevel[32].numberBashers = 0;
            AllLevel[32].numberMiners = 0;
            AllLevel[32].numberDiggers = 40;
            AllLevel[32].MinFrequencyComming = 1;
            AllLevel[32].FrequencyComming = 1;
            AllLevel[32].NbLemmingsToSave = 15; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[32].totalTime = 4;
            AllLevel[33].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[33].InitPosX = 0; // Init xscroll
            AllLevel[33].NameLev = "levels/tri/tri003";
            AllLevel[33].nameOfLevel = "A stair would be great";
            AllLevel[33].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[33].doorX = 354;
            AllLevel[33].doorY = 37;
            AllLevel[33].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[33].exitX = 2086;
            AllLevel[33].exitY = 163;
            AllLevel[33].numberClimbers = 20;
            AllLevel[33].numberUmbrellas = 20;
            AllLevel[33].numberExploders = 20;
            AllLevel[33].numberBlockers = 20;
            AllLevel[33].numberBuilders = 40;
            AllLevel[33].numberBashers = 20;
            AllLevel[33].numberMiners = 20;
            AllLevel[33].numberDiggers = 20;
            AllLevel[33].MinFrequencyComming = 50;
            AllLevel[33].FrequencyComming = 50;
            AllLevel[33].NbLemmingsToSave = 50; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[33].totalTime = 8;
            AllLevel[34].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[34].InitPosX = 0; // Init xscroll
            AllLevel[34].NameLev = "levels/tri/tri004";
            AllLevel[34].nameOfLevel = "This i study before";
            AllLevel[34].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[34].doorX = 112;
            AllLevel[34].doorY = 140;
            AllLevel[34].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[34].exitX = 2669;
            AllLevel[34].exitY = 364;
            AllLevel[34].numberClimbers = 40;
            AllLevel[34].numberUmbrellas = 40;
            AllLevel[34].numberExploders = 40;
            AllLevel[34].numberBlockers = 40;
            AllLevel[34].numberBuilders = 40;
            AllLevel[34].numberBashers = 40;
            AllLevel[34].numberMiners = 40;
            AllLevel[34].numberDiggers = 40;
            AllLevel[34].MinFrequencyComming = 55;
            AllLevel[34].FrequencyComming = 55;
            AllLevel[34].NbLemmingsToSave = 20; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[34].totalTime = 11;
            AllLevel[35].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[35].InitPosX = 1162; // Init xscroll
            AllLevel[35].NameLev = "levels/tri/tri005";
            AllLevel[35].nameOfLevel = "Go crazy can cost lives";
            AllLevel[35].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[35].doorX = 1357;
            AllLevel[35].doorY = 192;
            AllLevel[35].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[35].exitX = 3290;
            AllLevel[35].exitY = 249;
            AllLevel[35].numberClimbers = 20;
            AllLevel[35].numberUmbrellas = 20;
            AllLevel[35].numberExploders = 20;
            AllLevel[35].numberBlockers = 20;
            AllLevel[35].numberBuilders = 20;
            AllLevel[35].numberBashers = 20;
            AllLevel[35].numberMiners = 20;
            AllLevel[35].numberDiggers = 20;
            AllLevel[35].MinFrequencyComming = 20;
            AllLevel[35].FrequencyComming = 20;
            AllLevel[35].NbLemmingsToSave = 20; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[35].totalTime = 7;
            AllLevel[36].TotalLemmings = 5;  //two exits special one level SEE SEE SEE
            AllLevel[36].InitPosX = 0; // Init xscroll
            AllLevel[36].NameLev = "levels/tri/tri006";
            AllLevel[36].nameOfLevel = "Lemminglogogy";
            AllLevel[36].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[36].doorX = 570;
            AllLevel[36].doorY = 169;
            AllLevel[36].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[36].exitX = 1628;
            AllLevel[36].exitY = 431;
            AllLevel[36].numberClimbers = 20;
            AllLevel[36].numberUmbrellas = 20;
            AllLevel[36].numberExploders = 20;
            AllLevel[36].numberBlockers = 20;
            AllLevel[36].numberBuilders = 20;
            AllLevel[36].numberBashers = 20;
            AllLevel[36].numberMiners = 20;
            AllLevel[36].numberDiggers = 20;
            AllLevel[36].MinFrequencyComming = 50;
            AllLevel[36].FrequencyComming = 50;
            AllLevel[36].NbLemmingsToSave = 4; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[36].totalTime = 7;
            AllLevel[37].TotalLemmings = 75;  //two exits special one level SEE SEE SEE
            AllLevel[37].InitPosX = 1595; // Init xscroll
            AllLevel[37].NameLev = "levels/tri/tri007";
            AllLevel[37].nameOfLevel = "Veni, vidi, vici";
            AllLevel[37].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[37].doorX = 1970;
            AllLevel[37].doorY = 325;
            AllLevel[37].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[37].exitX = 369;
            AllLevel[37].exitY = 451;
            AllLevel[37].numberClimbers = 20;
            AllLevel[37].numberUmbrellas = 20;
            AllLevel[37].numberExploders = 20;
            AllLevel[37].numberBlockers = 20;
            AllLevel[37].numberBuilders = 20;
            AllLevel[37].numberBashers = 20;
            AllLevel[37].numberMiners = 20;
            AllLevel[37].numberDiggers = 20;
            AllLevel[37].MinFrequencyComming = 20;
            AllLevel[37].FrequencyComming = 20;
            AllLevel[37].NbLemmingsToSave = 55; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[37].totalTime = 7;
            AllLevel[38].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[38].InitPosX = 180; // Init xscroll
            AllLevel[38].NameLev = "levels/tri/tri008";
            AllLevel[38].nameOfLevel = "Lemming Sanctuary at sight";
            AllLevel[38].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[38].doorX = 1019;
            AllLevel[38].doorY = 370;
            AllLevel[38].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[38].exitX = 217;
            AllLevel[38].exitY = 164;
            AllLevel[38].numberClimbers = 0;
            AllLevel[38].numberUmbrellas = 0;
            AllLevel[38].numberExploders = 0;
            AllLevel[38].numberBlockers = 20;
            AllLevel[38].numberBuilders = 50;
            AllLevel[38].numberBashers = 0;
            AllLevel[38].numberMiners = 0;
            AllLevel[38].numberDiggers = 0;
            AllLevel[38].MinFrequencyComming = 40;
            AllLevel[38].FrequencyComming = 40;
            AllLevel[38].NbLemmingsToSave = 60; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[38].totalTime = 11;
            AllLevel[39].TotalLemmings = 75;  //two exits special one level SEE SEE SEE
            AllLevel[39].InitPosX = 900; // Init xscroll
            AllLevel[39].NameLev = "levels/tri/tri009";
            AllLevel[39].nameOfLevel = "They don't left to arrive";
            AllLevel[39].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[39].doorX = 1585;
            AllLevel[39].doorY = 90;
            AllLevel[39].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[39].exitX = 1677;
            AllLevel[39].exitY = 458;
            AllLevel[39].numberClimbers = 20;
            AllLevel[39].numberUmbrellas = 20;
            AllLevel[39].numberExploders = 20;
            AllLevel[39].numberBlockers = 20;
            AllLevel[39].numberBuilders = 20;
            AllLevel[39].numberBashers = 20;
            AllLevel[39].numberMiners = 20;
            AllLevel[39].numberDiggers = 20;
            AllLevel[39].MinFrequencyComming = 40;
            AllLevel[39].FrequencyComming = 40;
            AllLevel[39].NbLemmingsToSave = 70;
            AllLevel[39].totalTime = 7;
            AllLevel[40].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[40].InitPosX = 100; // Init xscroll
            AllLevel[40].NameLev = "levels/tri/tri010";
            AllLevel[40].nameOfLevel = "There's a lot of them about";
            AllLevel[40].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[40].doorX = 388;
            AllLevel[40].doorY = 225;
            AllLevel[40].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[40].exitX = 1432;
            AllLevel[40].exitY = 216;
            AllLevel[40].numberClimbers = 20;
            AllLevel[40].numberUmbrellas = 20;
            AllLevel[40].numberExploders = 20;
            AllLevel[40].numberBlockers = 20;
            AllLevel[40].numberBuilders = 20;
            AllLevel[40].numberBashers = 20;
            AllLevel[40].numberMiners = 20;
            AllLevel[40].numberDiggers = 20;
            AllLevel[40].MinFrequencyComming = 60;
            AllLevel[40].FrequencyComming = 60;
            AllLevel[40].NbLemmingsToSave = 94;
            AllLevel[40].totalTime = 8;
            AllLevel[41].TotalLemmings = 50;  //two exits special one level SEE SEE SEE
            AllLevel[41].InitPosX = 0; // Init xscroll
            AllLevel[41].NameLev = "levels/tri/tri011";
            AllLevel[41].nameOfLevel = "Lemmings on the attic";
            AllLevel[41].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[41].doorX = 754;
            AllLevel[41].doorY = 294;
            AllLevel[41].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[41].exitX = 1452;
            AllLevel[41].exitY = 218;
            AllLevel[41].numberClimbers = 20;
            AllLevel[41].numberUmbrellas = 20;
            AllLevel[41].numberExploders = 20;
            AllLevel[41].numberBlockers = 20;
            AllLevel[41].numberBuilders = 20;
            AllLevel[41].numberBashers = 20;
            AllLevel[41].numberMiners = 20;
            AllLevel[41].numberDiggers = 20;
            AllLevel[41].MinFrequencyComming = 20;
            AllLevel[41].FrequencyComming = 20;
            AllLevel[41].NbLemmingsToSave = 42;
            AllLevel[41].totalTime = 11;
            AllLevel[42].TotalLemmings = 50;  //two exits special one level SEE SEE SEE
            AllLevel[42].InitPosX = 0; // Init xscroll
            AllLevel[42].NameLev = "levels/tri/tri012";
            AllLevel[42].nameOfLevel = "Lemmings bittersweet";
            AllLevel[42].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[42].doorX = 403;
            AllLevel[42].doorY = 14;
            AllLevel[42].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[42].exitX = 1270;
            AllLevel[42].exitY = 356;
            AllLevel[42].numberClimbers = 50;
            AllLevel[42].numberUmbrellas = 50;
            AllLevel[42].numberExploders = 20;
            AllLevel[42].numberBlockers = 20;
            AllLevel[42].numberBuilders = 50;
            AllLevel[42].numberBashers = 20;
            AllLevel[42].numberMiners = 20;
            AllLevel[42].numberDiggers = 20;
            AllLevel[42].MinFrequencyComming = 20;
            AllLevel[42].FrequencyComming = 20;
            AllLevel[42].NbLemmingsToSave = 40;
            AllLevel[42].totalTime = 11;
            AllLevel[43].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[43].InitPosX = 0; // Init xscroll
            AllLevel[43].NameLev = "levels/tri/tri013";
            AllLevel[43].nameOfLevel = "Free fall";
            AllLevel[43].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[43].doorX = 237;
            AllLevel[43].doorY = 127;
            AllLevel[43].TypeOfExit = 9; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[43].exitX = 1247;
            AllLevel[43].exitY = 284;
            AllLevel[43].numberClimbers = 20;
            AllLevel[43].numberUmbrellas = 20;
            AllLevel[43].numberExploders = 20;
            AllLevel[43].numberBlockers = 20;
            AllLevel[43].numberBuilders = 50;
            AllLevel[43].numberBashers = 20;
            AllLevel[43].numberMiners = 20;
            AllLevel[43].numberDiggers = 20;
            AllLevel[43].MinFrequencyComming = 20;
            AllLevel[43].FrequencyComming = 20;
            AllLevel[43].NbLemmingsToSave = 70;
            AllLevel[43].totalTime = 11;
            AllLevel[44].TotalLemmings = 80;  //two exits special one level SEE SEE SEE
            AllLevel[44].InitPosX = 0; // Init xscroll
            AllLevel[44].NameLev = "levels/tri/tri014";
            AllLevel[44].nameOfLevel = "This is Hell";
            AllLevel[44].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[44].doorX = 48;
            AllLevel[44].doorY = 348;
            AllLevel[44].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[44].exitX = 2889;
            AllLevel[44].exitY = 269;
            AllLevel[44].numberClimbers = 10;
            AllLevel[44].numberUmbrellas = 10;
            AllLevel[44].numberExploders = 15;
            AllLevel[44].numberBlockers = 10;
            AllLevel[44].numberBuilders = 15;
            AllLevel[44].numberBashers = 15;
            AllLevel[44].numberMiners = 15;
            AllLevel[44].numberDiggers = 15;
            AllLevel[44].MinFrequencyComming = 50;
            AllLevel[44].FrequencyComming = 50;
            AllLevel[44].NbLemmingsToSave = 70;
            AllLevel[44].totalTime = 11;
            AllLevel[45].TotalLemmings = 10;  //two exits special one level SEE SEE SEE
            AllLevel[45].InitPosX = 0; // Init xscroll
            AllLevel[45].NameLev = "levels/tri/tri015";
            AllLevel[45].nameOfLevel = "Ecologist Lemmings";
            AllLevel[45].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[45].doorX = 690;
            AllLevel[45].doorY = 123;
            AllLevel[45].TypeOfExit = 5; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[45].exitX = 1184;
            AllLevel[45].exitY = 416;
            AllLevel[45].numberClimbers = 0;
            AllLevel[45].numberUmbrellas = 0;
            AllLevel[45].numberExploders = 5;
            AllLevel[45].numberBlockers = 0;
            AllLevel[45].numberBuilders = 0;
            AllLevel[45].numberBashers = 0;
            AllLevel[45].numberMiners = 0;
            AllLevel[45].numberDiggers = 0;
            AllLevel[45].MinFrequencyComming = 50;
            AllLevel[45].FrequencyComming = 50;
            AllLevel[45].NbLemmingsToSave = 6;
            AllLevel[45].totalTime = 7;
            AllLevel[46].TotalLemmings = 50;  //two exits special one level SEE SEE SEE
            AllLevel[46].InitPosX = 0; // Init xscroll
            AllLevel[46].NameLev = "levels/tri/tri016";
            AllLevel[46].nameOfLevel = "Bichillos adorables";
            AllLevel[46].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[46].doorX = 441;
            AllLevel[46].doorY = 123;
            AllLevel[46].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[46].exitX = 2028;
            AllLevel[46].exitY = 427;
            AllLevel[46].numberClimbers = 0;
            AllLevel[46].numberUmbrellas = 0;
            AllLevel[46].numberExploders = 10;
            AllLevel[46].numberBlockers = 0;
            AllLevel[46].numberBuilders = 0;
            AllLevel[46].numberBashers = 1;
            AllLevel[46].numberMiners = 1;
            AllLevel[46].numberDiggers = 0;
            AllLevel[46].MinFrequencyComming = 76;
            AllLevel[46].FrequencyComming = 76;
            AllLevel[46].NbLemmingsToSave = 40;
            AllLevel[46].totalTime = 7;
            AllLevel[47].TotalLemmings = 50;  //two exits special one level SEE SEE SEE
            AllLevel[47].InitPosX = 440; // Init xscroll
            AllLevel[47].NameLev = "levels/tri/tri017";
            AllLevel[47].nameOfLevel = "Lemmingola light";
            AllLevel[47].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[47].doorX = 755;
            AllLevel[47].doorY = 100;
            AllLevel[47].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[47].exitX = 1697;
            AllLevel[47].exitY = 330;
            AllLevel[47].numberClimbers = 0;
            AllLevel[47].numberUmbrellas = 0;
            AllLevel[47].numberExploders = 2;
            AllLevel[47].numberBlockers = 0;
            AllLevel[47].numberBuilders = 0;
            AllLevel[47].numberBashers = 0;
            AllLevel[47].numberMiners = 0;
            AllLevel[47].numberDiggers = 0;
            AllLevel[47].MinFrequencyComming = 50;
            AllLevel[47].FrequencyComming = 50;
            AllLevel[47].NbLemmingsToSave = 48;
            AllLevel[47].totalTime = 7;
            AllLevel[48].TotalLemmings = 10;  //two exits special one level SEE SEE SEE
            AllLevel[48].InitPosX = 0; // Init xscroll
            AllLevel[48].NameLev = "levels/tri/tri018";
            AllLevel[48].nameOfLevel = "Elemmingtal, Dear Watson";
            AllLevel[48].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[48].doorX = 298;
            AllLevel[48].doorY = 46;
            AllLevel[48].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[48].exitX = 1175;
            AllLevel[48].exitY = 478;
            AllLevel[48].numberClimbers = 0;
            AllLevel[48].numberUmbrellas = 1;
            AllLevel[48].numberExploders = 1;
            AllLevel[48].numberBlockers = 0;
            AllLevel[48].numberBuilders = 2;
            AllLevel[48].numberBashers = 0;
            AllLevel[48].numberMiners = 0;
            AllLevel[48].numberDiggers = 1;
            AllLevel[48].MinFrequencyComming = 1;
            AllLevel[48].FrequencyComming = 1;
            AllLevel[48].NbLemmingsToSave = 9;
            AllLevel[48].totalTime = 7;
            AllLevel[49].TotalLemmings = 50;  //two exits special one level SEE SEE SEE
            AllLevel[49].InitPosX = 100; // Init xscroll
            AllLevel[49].NameLev = "levels/tri/tri019";
            AllLevel[49].nameOfLevel = "Card from Lemmingland";
            AllLevel[49].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[49].doorX = 667;
            AllLevel[49].doorY = 178;
            AllLevel[49].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[49].exitX = 2484;
            AllLevel[49].exitY = 307;
            AllLevel[49].numberClimbers = 10;
            AllLevel[49].numberUmbrellas = 10;
            AllLevel[49].numberExploders = 0;
            AllLevel[49].numberBlockers = 0;
            AllLevel[49].numberBuilders = 1;
            AllLevel[49].numberBashers = 1;
            AllLevel[49].numberMiners = 1;
            AllLevel[49].numberDiggers = 1;
            AllLevel[49].MinFrequencyComming = 50;
            AllLevel[49].FrequencyComming = 50;
            AllLevel[49].NbLemmingsToSave = 50;
            AllLevel[49].totalTime = 7;
            AllLevel[50].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[50].InitPosX = 0; // Init xscroll
            AllLevel[50].NameLev = "levels/tri/tri020";
            AllLevel[50].nameOfLevel = "One tunnel for Freedom";
            AllLevel[50].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[50].doorX = 463;
            AllLevel[50].doorY = 237;
            AllLevel[50].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[50].exitX = 2287;
            AllLevel[50].exitY = 393;
            AllLevel[50].numberClimbers = 3;
            AllLevel[50].numberUmbrellas = 3;
            AllLevel[50].numberExploders = 6;
            AllLevel[50].numberBlockers = 2;
            AllLevel[50].numberBuilders = 2;
            AllLevel[50].numberBashers = 4;
            AllLevel[50].numberMiners = 2;
            AllLevel[50].numberDiggers = 2;
            AllLevel[50].MinFrequencyComming = 60;
            AllLevel[50].FrequencyComming = 60;
            AllLevel[50].NbLemmingsToSave = 95;
            AllLevel[50].totalTime = 6;
            AllLevel[51].TotalLemmings = 66;  //two exits special one level SEE SEE SEE
            AllLevel[51].InitPosX = 422; // Init xscroll
            AllLevel[51].NameLev = "levels/tri/tri021";
            AllLevel[51].nameOfLevel = "SaTaN";
            AllLevel[51].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[51].doorX = 622;
            AllLevel[51].doorY = 267;
            AllLevel[51].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[51].exitX = 1614;
            AllLevel[51].exitY = 384;
            AllLevel[51].numberClimbers = 66;
            AllLevel[51].numberUmbrellas = 66;
            AllLevel[51].numberExploders = 66;
            AllLevel[51].numberBlockers = 66;
            AllLevel[51].numberBuilders = 66;
            AllLevel[51].numberBashers = 66;
            AllLevel[51].numberMiners = 66;
            AllLevel[51].numberDiggers = 66;
            AllLevel[51].MinFrequencyComming = 66;
            AllLevel[51].FrequencyComming = 66;
            AllLevel[51].NbLemmingsToSave = 44;
            AllLevel[51].totalTime = 8;
            AllLevel[52].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[52].InitPosX = 422; // Init xscroll
            AllLevel[52].NameLev = "levels/tri/tri022";
            AllLevel[52].nameOfLevel = "Turn around, young Lemmings";
            AllLevel[52].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[52].doorX = 1234;
            AllLevel[52].doorY = 141;
            AllLevel[52].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[52].exitX = 1126;
            AllLevel[52].exitY = 325;
            AllLevel[52].numberClimbers = 0;
            AllLevel[52].numberUmbrellas = 1;
            AllLevel[52].numberExploders = 15;
            AllLevel[52].numberBlockers = 0;
            AllLevel[52].numberBuilders = 20;
            AllLevel[52].numberBashers = 1;
            AllLevel[52].numberMiners = 0;
            AllLevel[52].numberDiggers = 0;
            AllLevel[52].MinFrequencyComming = 60;
            AllLevel[52].FrequencyComming = 60;
            AllLevel[52].NbLemmingsToSave = 90;
            AllLevel[52].totalTime = 7;
            AllLevel[53].TotalLemmings = 100;
            AllLevel[53].InitPosX = 422; // Init xscroll
            AllLevel[53].NameLev = "levels/tri/tri023";
            AllLevel[53].nameOfLevel = "At limit";
            AllLevel[53].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[53].doorX = 1;
            AllLevel[53].doorY = 212;
            AllLevel[53].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[53].exitX = 962;
            AllLevel[53].exitY = 387;
            AllLevel[53].numberClimbers = 10;
            AllLevel[53].numberUmbrellas = 10;
            AllLevel[53].numberExploders = 0;
            AllLevel[53].numberBlockers = 0;
            AllLevel[53].numberBuilders = 1;
            AllLevel[53].numberBashers = 2;
            AllLevel[53].numberMiners = 1;
            AllLevel[53].numberDiggers = 1;
            AllLevel[53].MinFrequencyComming = 96; // 99 is so fast for this, so close
            AllLevel[53].FrequencyComming = 96;
            //here i need to rescue 10 less than original(60).My basher speed is similar than dos version
            // but i need to basher more bites than original does. ALways return back 10 more lemmings.
            AllLevel[53].NbLemmingsToSave = 50;
            AllLevel[53].totalTime = 7;
            AllLevel[54].TotalLemmings = 80;
            AllLevel[54].InitPosX = 0; // Init xscroll
            AllLevel[54].NameLev = "levels/tri/tri024";
            AllLevel[54].nameOfLevel = "City CuerdaFloja";
            AllLevel[54].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[54].doorX = 1;
            AllLevel[54].doorY = 212;
            AllLevel[54].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[54].exitX = 1793;
            AllLevel[54].exitY = 238;
            AllLevel[54].numberClimbers = 5;
            AllLevel[54].numberUmbrellas = 5;
            AllLevel[54].numberExploders = 5;
            AllLevel[54].numberBlockers = 5;
            AllLevel[54].numberBuilders = 3;
            AllLevel[54].numberBashers = 5;
            AllLevel[54].numberMiners = 5;
            AllLevel[54].numberDiggers = 5;
            AllLevel[54].MinFrequencyComming = 85;
            AllLevel[54].FrequencyComming = 85;
            AllLevel[54].NbLemmingsToSave = 75;
            AllLevel[54].totalTime = 3;
            AllLevel[55].TotalLemmings = 100;
            AllLevel[55].InitPosX = 400; // Init xscroll
            AllLevel[55].NameLev = "levels/tri/tri025";
            AllLevel[55].nameOfLevel = "Waterfall";
            AllLevel[55].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 1--7puerta
            AllLevel[55].doorX = 1063;
            AllLevel[55].doorY = 302;
            AllLevel[55].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 1--9
            AllLevel[55].exitX = 2453;
            AllLevel[55].exitY = 335;
            AllLevel[55].numberClimbers = 1;
            AllLevel[55].numberUmbrellas = 10;
            AllLevel[55].numberExploders = 5;
            AllLevel[55].numberBlockers = 0;
            AllLevel[55].numberBuilders = 10;
            AllLevel[55].numberBashers = 1;
            AllLevel[55].numberMiners = 1;
            AllLevel[55].numberDiggers = 5;
            AllLevel[55].MinFrequencyComming = 99;
            AllLevel[55].FrequencyComming = 99;
            AllLevel[55].NbLemmingsToSave = 10;
            AllLevel[55].totalTime = 7;
            AllLevel[56].TotalLemmings = 100;
            AllLevel[56].InitPosX = 400; // Init xscroll
            AllLevel[56].NameLev = "levels/tri/tri026";
            AllLevel[56].nameOfLevel = "A smart plann";
            AllLevel[56].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[56].doorX = 176;
            AllLevel[56].doorY = 281;
            AllLevel[56].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[56].exitX = 746;
            AllLevel[56].exitY = 399;
            AllLevel[56].numberClimbers = 2;
            AllLevel[56].numberUmbrellas = 0;
            AllLevel[56].numberExploders = 5;
            AllLevel[56].numberBlockers = 10;
            AllLevel[56].numberBuilders = 20;
            AllLevel[56].numberBashers = 2;
            AllLevel[56].numberMiners = 2;
            AllLevel[56].numberDiggers = 2;
            AllLevel[56].MinFrequencyComming = 99;
            AllLevel[56].FrequencyComming = 99;
            AllLevel[56].NbLemmingsToSave = 100;
            // i can't save 100 like original by the way builders do-reduce speed o less bites to draw
            // my best result without pauses was 99 saved but with MyGame.Paused game 100% saved
            AllLevel[56].totalTime = 7;
            AllLevel[57].TotalLemmings = 100;
            AllLevel[57].InitPosX = 350; // Init xscroll
            AllLevel[57].NameLev = "levels/tri/tri027";
            AllLevel[57].nameOfLevel = "Lemmings Island";
            AllLevel[57].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[57].doorX = 772;
            AllLevel[57].doorY = 5;
            AllLevel[57].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[57].exitX = 1951;
            AllLevel[57].exitY = 154;
            AllLevel[57].numberClimbers = 5;
            AllLevel[57].numberUmbrellas = 0;
            AllLevel[57].numberExploders = 5;
            AllLevel[57].numberBlockers = 5;
            AllLevel[57].numberBuilders = 5;
            AllLevel[57].numberBashers = 5;
            AllLevel[57].numberMiners = 5;
            AllLevel[57].numberDiggers = 5;
            AllLevel[57].MinFrequencyComming = 60;
            AllLevel[57].FrequencyComming = 60;
            AllLevel[57].NbLemmingsToSave = 90;
            AllLevel[57].totalTime = 7;
            AllLevel[58].TotalLemmings = 100;
            AllLevel[58].InitPosX = 878; // Init xscroll
            AllLevel[58].NameLev = "levels/tri/tri028";
            AllLevel[58].nameOfLevel = "Do you lost something?";
            AllLevel[58].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[58].doorX = 1336;
            AllLevel[58].doorY = 256;
            AllLevel[58].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[58].exitX = 1893;
            AllLevel[58].exitY = 239;
            AllLevel[58].DoorExitDepth = 0.6f;
            AllLevel[58].numberClimbers = 10;
            AllLevel[58].numberUmbrellas = 10;
            AllLevel[58].numberExploders = 10;
            AllLevel[58].numberBlockers = 10;
            AllLevel[58].numberBuilders = 10;
            AllLevel[58].numberBashers = 10;
            AllLevel[58].numberMiners = 10;
            AllLevel[58].numberDiggers = 10;
            AllLevel[58].MinFrequencyComming = 70;
            AllLevel[58].FrequencyComming = 70;
            AllLevel[58].NbLemmingsToSave = 90;
            AllLevel[58].totalTime = 7;
            AllLevel[59].TotalLemmings = 100; //100 test 500 for bombers ALL OK
            AllLevel[59].InitPosX = 0; // Init xscroll
            AllLevel[59].NameLev = "levels/tri/tri029";
            AllLevel[59].nameOfLevel = "Rainbow Island";
            AllLevel[59].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[59].doorX = 159;
            AllLevel[59].doorY = 303;
            AllLevel[59].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[59].exitX = 3179;
            AllLevel[59].exitY = 486;
            AllLevel[59].numberClimbers = 10;
            AllLevel[59].numberUmbrellas = 10;
            AllLevel[59].numberExploders = 10;
            AllLevel[59].numberBlockers = 10;
            AllLevel[59].numberBuilders = 10;
            AllLevel[59].numberBashers = 10;
            AllLevel[59].numberMiners = 10;
            AllLevel[59].numberDiggers = 10;
            AllLevel[59].MinFrequencyComming = 70;
            AllLevel[59].FrequencyComming = 70;
            AllLevel[59].NbLemmingsToSave = 99;
            AllLevel[59].totalTime = 6;
            AllLevel[60].TotalLemmings = 100;
            AllLevel[60].InitPosX = 0; // Init xscroll
            AllLevel[60].NameLev = "levels/tri/tri030";
            AllLevel[60].nameOfLevel = "Egypt mistery";
            AllLevel[60].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[60].doorX = 310;
            AllLevel[60].doorY = 99;
            AllLevel[60].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[60].exitX = 2790;
            AllLevel[60].exitY = 330;
            AllLevel[60].numberClimbers = 10;
            AllLevel[60].numberUmbrellas = 10;
            AllLevel[60].numberExploders = 10;
            AllLevel[60].numberBlockers = 10;
            AllLevel[60].numberBuilders = 10;
            AllLevel[60].numberBashers = 0;
            AllLevel[60].numberMiners = 10;
            AllLevel[60].numberDiggers = 10;
            AllLevel[60].MinFrequencyComming = 70;
            AllLevel[60].FrequencyComming = 70;
            AllLevel[60].NbLemmingsToSave = 90;
            AllLevel[60].totalTime = 6;
            // TAXING LEVELS TAXING //////////////////////////////////////////////////////////////////////////
            AllLevel[61].TotalLemmings = 100;
            AllLevel[61].InitPosX = 0; // Init xscroll
            AllLevel[61].NameLev = "levels/tax/exi001";
            AllLevel[61].nameOfLevel = "If you don't do it at first...";
            AllLevel[61].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[61].doorX = 155;
            AllLevel[61].doorY = 99;
            AllLevel[61].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[61].exitX = 3671;
            AllLevel[61].exitY = 203;
            AllLevel[61].numberClimbers = 2;
            AllLevel[61].numberUmbrellas = 2;
            AllLevel[61].numberExploders = 2;
            AllLevel[61].numberBlockers = 2;
            AllLevel[61].numberBuilders = 8;
            AllLevel[61].numberBashers = 2;
            AllLevel[61].numberMiners = 2;
            AllLevel[61].numberDiggers = 2;
            AllLevel[61].MinFrequencyComming = 40;
            AllLevel[61].FrequencyComming = 40;
            AllLevel[61].NbLemmingsToSave = 99;
            AllLevel[61].totalTime = 6;
            AllLevel[62].TotalLemmings = 100;
            AllLevel[62].InitPosX = 1222; // Init xscroll
            AllLevel[62].NameLev = "levels/tax/exi002";
            AllLevel[62].nameOfLevel = "Be careful with traps";
            AllLevel[62].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[62].doorX = 1400;
            AllLevel[62].doorY = 65;
            AllLevel[62].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[62].exitX = 462;
            AllLevel[62].exitY = 305;
            AllLevel[62].numberClimbers = 10;
            AllLevel[62].numberUmbrellas = 5;
            AllLevel[62].numberExploders = 5;
            AllLevel[62].numberBlockers = 10;
            AllLevel[62].numberBuilders = 10;
            AllLevel[62].numberBashers = 5;
            AllLevel[62].numberMiners = 5;
            AllLevel[62].numberDiggers = 5;
            AllLevel[62].MinFrequencyComming = 70;
            AllLevel[62].FrequencyComming = 80;
            AllLevel[62].NbLemmingsToSave = 99;
            AllLevel[62].totalTime = 7;
            AllLevel[63].TotalLemmings = 80;
            AllLevel[63].InitPosX = 145; // Init xscroll
            AllLevel[63].NameLev = "levels/tax/exi003";
            AllLevel[63].nameOfLevel = "Heaven can wait";
            AllLevel[63].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[63].doorX = 488;
            AllLevel[63].doorY = 275;
            AllLevel[63].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[63].exitX = 1709;
            AllLevel[63].exitY = 214;
            AllLevel[63].numberClimbers = 30;
            AllLevel[63].numberUmbrellas = 30;
            AllLevel[63].numberExploders = 30;
            AllLevel[63].numberBlockers = 0;
            AllLevel[63].numberBuilders = 30;
            AllLevel[63].numberBashers = 30;
            AllLevel[63].numberMiners = 30;
            AllLevel[63].numberDiggers = 30;
            AllLevel[63].MinFrequencyComming = 1;
            AllLevel[63].FrequencyComming = 1;
            AllLevel[63].NbLemmingsToSave = 80;
            AllLevel[63].totalTime = 3;
            AllLevel[64].TotalLemmings = 40;
            AllLevel[64].InitPosX = 712; // Init xscroll
            AllLevel[64].NameLev = "levels/tax/exi004";
            AllLevel[64].nameOfLevel = "Give me a hand";
            AllLevel[64].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[64].doorX = 1316;
            AllLevel[64].doorY = 321;
            AllLevel[64].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[64].exitX = 1228;
            AllLevel[64].exitY = 80;
            AllLevel[64].numberClimbers = 0;
            AllLevel[64].numberUmbrellas = 0;
            AllLevel[64].numberExploders = 5;
            AllLevel[64].numberBlockers = 5;
            AllLevel[64].numberBuilders = 22;
            AllLevel[64].numberBashers = 2;
            AllLevel[64].numberMiners = 2;
            AllLevel[64].numberDiggers = 2;
            AllLevel[64].MinFrequencyComming = 50;
            AllLevel[64].FrequencyComming = 50;
            AllLevel[64].NbLemmingsToSave = 25;
            AllLevel[64].totalTime = 10;
            AllLevel[65].TotalLemmings = 60;
            AllLevel[65].InitPosX = 712; // Init xscroll
            AllLevel[65].NameLev = "levels/tax/exi005";
            AllLevel[65].nameOfLevel = "The jail";
            AllLevel[65].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[65].doorX = 99;
            AllLevel[65].doorY = 190;
            AllLevel[65].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[65].exitX = 969;
            AllLevel[65].exitY = 190;
            AllLevel[65].numberClimbers = 0;
            AllLevel[65].numberUmbrellas = 0;
            AllLevel[65].numberExploders = 5;
            AllLevel[65].numberBlockers = 4;
            AllLevel[65].numberBuilders = 20;
            AllLevel[65].numberBashers = 10;
            AllLevel[65].numberMiners = 0;
            AllLevel[65].numberDiggers = 3;
            AllLevel[65].MinFrequencyComming = 50;
            AllLevel[65].FrequencyComming = 50;
            AllLevel[65].NbLemmingsToSave = 45;
            AllLevel[65].totalTime = 7;
            AllLevel[66].TotalLemmings = 50;
            AllLevel[66].InitPosX = 0; // Init xscroll
            AllLevel[66].NameLev = "levels/tax/exi006";
            AllLevel[66].nameOfLevel = "Compression method";
            AllLevel[66].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[66].doorX = 288;
            AllLevel[66].doorY = 145;
            AllLevel[66].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[66].exitX = 1358;
            AllLevel[66].exitY = 433;
            AllLevel[66].numberClimbers = 0;
            AllLevel[66].numberUmbrellas = 0;
            AllLevel[66].numberExploders = 3;
            AllLevel[66].numberBlockers = 3;
            AllLevel[66].numberBuilders = 0;
            AllLevel[66].numberBashers = 5;
            AllLevel[66].numberMiners = 1;
            AllLevel[66].numberDiggers = 0;
            AllLevel[66].MinFrequencyComming = 99;
            AllLevel[66].FrequencyComming = 99;
            AllLevel[66].NbLemmingsToSave = 30;
            AllLevel[66].totalTime = 4;
            AllLevel[67].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[67].InitPosX = 0; // Init xscroll
            AllLevel[67].NameLev = "levels/tax/exi007";
            AllLevel[67].nameOfLevel = "All lemmings are yours";
            AllLevel[67].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[67].doorX = 112;
            AllLevel[67].doorY = 140;
            AllLevel[67].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[67].exitX = 2669;
            AllLevel[67].exitY = 364;
            AllLevel[67].numberClimbers = 1;
            AllLevel[67].numberUmbrellas = 0;
            AllLevel[67].numberExploders = 5;
            AllLevel[67].numberBlockers = 1;
            AllLevel[67].numberBuilders = 6;
            AllLevel[67].numberBashers = 1;
            AllLevel[67].numberMiners = 0;
            AllLevel[67].numberDiggers = 0;
            AllLevel[67].MinFrequencyComming = 55;
            AllLevel[67].FrequencyComming = 55;
            AllLevel[67].NbLemmingsToSave = 98; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[67].totalTime = 4;
            AllLevel[68].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[68].InitPosX = 850; // Init xscroll
            AllLevel[68].NameLev = "levels/tax/exi008";
            AllLevel[68].nameOfLevel = "The art gallery";
            AllLevel[68].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[68].doorX = 1375;
            AllLevel[68].doorY = 183;
            AllLevel[68].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[68].exitX = 3291;
            AllLevel[68].exitY = 249;
            AllLevel[68].numberClimbers = 10;
            AllLevel[68].numberUmbrellas = 10;
            AllLevel[68].numberExploders = 10;
            AllLevel[68].numberBlockers = 0;
            AllLevel[68].numberBuilders = 10;
            AllLevel[68].numberBashers = 10;
            AllLevel[68].numberMiners = 0;
            AllLevel[68].numberDiggers = 0;
            AllLevel[68].MinFrequencyComming = 20;
            AllLevel[68].FrequencyComming = 20;
            AllLevel[68].NbLemmingsToSave = 100; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[68].totalTime = 6;
            AllLevel[69].TotalLemmings = 20;  //two exits special one level SEE SEE SEE
            AllLevel[69].InitPosX = 608; // Init xscroll
            AllLevel[69].NameLev = "levels/tax/exi009";
            AllLevel[69].nameOfLevel = "Perseverance";
            AllLevel[69].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[69].doorX = 1115;
            AllLevel[69].doorY = 5;
            AllLevel[69].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[69].exitX = 900;
            AllLevel[69].exitY = 462;
            AllLevel[69].numberClimbers = 2;
            AllLevel[69].numberUmbrellas = 1;
            AllLevel[69].numberExploders = 1;
            AllLevel[69].numberBlockers = 2;
            AllLevel[69].numberBuilders = 2;
            AllLevel[69].numberBashers = 1;
            AllLevel[69].numberMiners = 1;
            AllLevel[69].numberDiggers = 2;
            AllLevel[69].MinFrequencyComming = 50;
            AllLevel[69].FrequencyComming = 50;
            AllLevel[69].NbLemmingsToSave = 20; // 66% of 60 = 39.6 (??*??/100)
            AllLevel[69].totalTime = 6;
            AllLevel[70].TotalLemmings = 5;  //two exits special one level SEE SEE SEE
            AllLevel[70].InitPosX = 0; // Init xscroll
            AllLevel[70].NameLev = "levels/tax/exi010";
            AllLevel[70].nameOfLevel = "Crazy lemming on course";
            AllLevel[70].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[70].doorX = 550;
            AllLevel[70].doorY = 127;
            AllLevel[70].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[70].exitX = 1683;
            AllLevel[70].exitY = 431;
            AllLevel[70].numberClimbers = 0;
            AllLevel[70].numberUmbrellas = 0;
            AllLevel[70].numberExploders = 5;
            AllLevel[70].numberBlockers = 5;
            AllLevel[70].numberBuilders = 15;
            AllLevel[70].numberBashers = 5;
            AllLevel[70].numberMiners = 5;
            AllLevel[70].numberDiggers = 5;
            AllLevel[70].MinFrequencyComming = 50;
            AllLevel[70].FrequencyComming = 50;
            AllLevel[70].NbLemmingsToSave = 5;
            AllLevel[70].totalTime = 7;
            AllLevel[71].TotalLemmings = 50;
            AllLevel[71].InitPosX = 1500;
            AllLevel[71].NameLev = "levels/tax/exi011";
            AllLevel[71].nameOfLevel = "Pillar ascendant";
            AllLevel[71].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[71].doorX = 1960;
            AllLevel[71].doorY = 327;
            AllLevel[71].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[71].exitX = 354;
            AllLevel[71].exitY = 451;
            AllLevel[71].numberClimbers = 2;
            AllLevel[71].numberUmbrellas = 1;
            AllLevel[71].numberExploders = 0;
            AllLevel[71].numberBlockers = 0;
            AllLevel[71].numberBuilders = 20;
            AllLevel[71].numberBashers = 5;
            AllLevel[71].numberMiners = 5;
            AllLevel[71].numberDiggers = 5;
            AllLevel[71].MinFrequencyComming = 10;
            AllLevel[71].FrequencyComming = 10;
            AllLevel[71].NbLemmingsToSave = 50;
            AllLevel[71].totalTime = 6;
            AllLevel[72].TotalLemmings = 10;
            AllLevel[72].InitPosX = 400;
            AllLevel[72].NameLev = "levels/tax/exi012";
            AllLevel[72].nameOfLevel = "Between two lands";
            AllLevel[72].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[72].doorX = 860;
            AllLevel[72].doorY = 292;
            AllLevel[72].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[72].exitX = 1938;
            AllLevel[72].exitY = 180;
            AllLevel[72].numberClimbers = 10;
            AllLevel[72].numberUmbrellas = 10;
            AllLevel[72].numberExploders = 10;
            AllLevel[72].numberBlockers = 2;
            AllLevel[72].numberBuilders = 10;
            AllLevel[72].numberBashers = 10;
            AllLevel[72].numberMiners = 10;
            AllLevel[72].numberDiggers = 10;
            AllLevel[72].MinFrequencyComming = 1;
            AllLevel[72].FrequencyComming = 1;
            AllLevel[72].NbLemmingsToSave = 8;
            AllLevel[72].totalTime = 7;
            AllLevel[73].TotalLemmings = 100;
            AllLevel[73].InitPosX = 1900;
            AllLevel[73].NameLev = "levels/tax/exi013";
            AllLevel[73].nameOfLevel = "UpsideDown World";
            AllLevel[73].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[73].doorX = 2228;
            AllLevel[73].doorY = 270;
            AllLevel[73].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[73].exitX = 138;
            AllLevel[73].exitY = 311;
            AllLevel[73].numberClimbers = 1;
            AllLevel[73].numberUmbrellas = 0;
            AllLevel[73].numberExploders = 10;
            AllLevel[73].numberBlockers = 1;
            AllLevel[73].numberBuilders = 2;
            AllLevel[73].numberBashers = 3;
            AllLevel[73].numberMiners = 4;
            AllLevel[73].numberDiggers = 2;
            AllLevel[73].MinFrequencyComming = 40;
            AllLevel[73].FrequencyComming = 40;
            AllLevel[73].NbLemmingsToSave = 99;
            AllLevel[73].totalTime = 8;
            AllLevel[74].TotalLemmings = 100;
            AllLevel[74].InitPosX = 0;
            AllLevel[74].NameLev = "levels/tax/exi014";
            AllLevel[74].nameOfLevel = "The hunt for Nessy Monster";
            AllLevel[74].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[74].doorX = 394;
            AllLevel[74].doorY = 281;
            AllLevel[74].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[74].exitX = 3804;
            AllLevel[74].exitY = 317;
            AllLevel[74].numberClimbers = 0;
            AllLevel[74].numberUmbrellas = 0;
            AllLevel[74].numberExploders = 10;
            AllLevel[74].numberBlockers = 10;
            AllLevel[74].numberBuilders = 30;
            AllLevel[74].numberBashers = 2;
            AllLevel[74].numberMiners = 1;
            AllLevel[74].numberDiggers = 1;
            AllLevel[74].MinFrequencyComming = 30;
            AllLevel[74].FrequencyComming = 30;
            AllLevel[74].NbLemmingsToSave = 95;
            AllLevel[74].totalTime = 13;
            AllLevel[75].TotalLemmings = 100;
            AllLevel[75].InitPosX = 0;
            AllLevel[75].NameLev = "levels/tax/exi015";
            AllLevel[75].nameOfLevel = "A alucinant trip";
            AllLevel[75].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[75].doorX = 122;
            AllLevel[75].doorY = 68;
            AllLevel[75].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[75].exitX = 2850;
            AllLevel[75].exitY = 96;
            AllLevel[75].numberClimbers = 20;
            AllLevel[75].numberUmbrellas = 20;
            AllLevel[75].numberExploders = 20;
            AllLevel[75].numberBlockers = 20;
            AllLevel[75].numberBuilders = 20;
            AllLevel[75].numberBashers = 20;
            AllLevel[75].numberMiners = 20;
            AllLevel[75].numberDiggers = 20;
            AllLevel[75].MinFrequencyComming = 50;
            AllLevel[75].FrequencyComming = 50;
            AllLevel[75].NbLemmingsToSave = 80;
            AllLevel[75].totalTime = 8;
            AllLevel[76].TotalLemmings = 100;
            AllLevel[76].InitPosX = 0;
            AllLevel[76].NameLev = "levels/tax/exi016";
            AllLevel[76].nameOfLevel = "Mary Poppins world";
            AllLevel[76].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[76].doorX = 50;
            AllLevel[76].doorY = 3;
            AllLevel[76].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[76].exitX = 2036;
            AllLevel[76].exitY = 427;
            AllLevel[76].numberClimbers = 0;
            AllLevel[76].numberUmbrellas = 4;
            AllLevel[76].numberExploders = 3;
            AllLevel[76].numberBlockers = 3;
            AllLevel[76].numberBuilders = 20;
            AllLevel[76].numberBashers = 0;
            AllLevel[76].numberMiners = 0;
            AllLevel[76].numberDiggers = 0;
            AllLevel[76].MinFrequencyComming = 50;
            AllLevel[76].FrequencyComming = 50;
            AllLevel[76].NbLemmingsToSave = 97;
            AllLevel[76].totalTime = 5;
            AllLevel[77].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[77].InitPosX = 1833; // Init xscroll
            AllLevel[77].NameLev = "levels/tax/exi017";
            AllLevel[77].nameOfLevel = "The X is the point";
            AllLevel[77].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[77].doorX = 2027;
            AllLevel[77].doorY = 247;
            AllLevel[77].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[77].exitX = 73;
            AllLevel[77].exitY = 460;
            AllLevel[77].numberClimbers = 5;
            AllLevel[77].numberUmbrellas = 0;
            AllLevel[77].numberExploders = 5;
            AllLevel[77].numberBlockers = 0;
            AllLevel[77].numberBuilders = 20;
            AllLevel[77].numberBashers = 5;
            AllLevel[77].numberMiners = 0;
            AllLevel[77].numberDiggers = 6;
            AllLevel[77].MinFrequencyComming = 50;
            AllLevel[77].FrequencyComming = 50;
            AllLevel[77].NbLemmingsToSave = 90; // 25% of 80 = 20 (??*??/100)
            AllLevel[77].totalTime = 10;
            AllLevel[78].DoorExitDepth = 0.6f; // prefer the stairs on the doors for better seeing
            AllLevel[78].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[78].InitPosX = 0; // Init xscroll
            AllLevel[78].NameLev = "levels/tax/exi018";
            AllLevel[78].nameOfLevel = "Oskar Award Level";
            AllLevel[78].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[78].doorX = 300;
            AllLevel[78].doorY = 300;
            AllLevel[78].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[78].exitX = 1040;
            AllLevel[78].exitY = 444;
            AllLevel[78].numberClimbers = 0;
            AllLevel[78].numberUmbrellas = 1;
            AllLevel[78].numberExploders = 15;
            AllLevel[78].numberBlockers = 0;
            AllLevel[78].numberBuilders = 15;
            AllLevel[78].numberBashers = 1;
            AllLevel[78].numberMiners = 0;
            AllLevel[78].numberDiggers = 0;
            AllLevel[78].MinFrequencyComming = 50;
            AllLevel[78].FrequencyComming = 50;
            AllLevel[78].NbLemmingsToSave = 65; // 25% of 80 = 20 (??*??/100)
            AllLevel[78].totalTime = 7;
            AllLevel[79].TotalLemmings = 70;
            AllLevel[79].InitPosX = 0; // Init xscroll
            AllLevel[79].NameLev = "levels/tax/exi019";
            AllLevel[79].nameOfLevel = "Badabum";
            AllLevel[79].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[79].doorX = 60;
            AllLevel[79].doorY = 15;
            AllLevel[79].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[79].exitX = 1562;
            AllLevel[79].exitY = 452;
            AllLevel[79].numberClimbers = 0;
            AllLevel[79].numberUmbrellas = 0;
            AllLevel[79].numberExploders = 15;
            AllLevel[79].numberBlockers = 0;
            AllLevel[79].numberBuilders = 0;
            AllLevel[79].numberBashers = 0;
            AllLevel[79].numberMiners = 0;
            AllLevel[79].numberDiggers = 0;
            AllLevel[79].MinFrequencyComming = 80;
            AllLevel[79].FrequencyComming = 80;
            AllLevel[79].NbLemmingsToSave = 64;
            AllLevel[79].totalTime = 3;
            AllLevel[80].TotalLemmings = 90;
            AllLevel[80].InitPosX = 0; // Init xscroll
            AllLevel[80].NameLev = "levels/tax/exi020";
            AllLevel[80].nameOfLevel = "Don't lose the way";
            AllLevel[80].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[80].doorX = 60;
            AllLevel[80].doorY = 300;
            AllLevel[80].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[80].exitX = 3528;
            AllLevel[80].exitY = 497;
            AllLevel[80].numberClimbers = 20;
            AllLevel[80].numberUmbrellas = 20;
            AllLevel[80].numberExploders = 20;
            AllLevel[80].numberBlockers = 20;
            AllLevel[80].numberBuilders = 20;
            AllLevel[80].numberBashers = 20;
            AllLevel[80].numberMiners = 20;
            AllLevel[80].numberDiggers = 20;
            AllLevel[80].MinFrequencyComming = 50;
            AllLevel[80].FrequencyComming = 50;
            AllLevel[80].NbLemmingsToSave = 80;
            AllLevel[80].totalTime = 7;
            AllLevel[81].TotalLemmings = 20;
            AllLevel[81].InitPosX = 0; // Init xscroll
            AllLevel[81].NameLev = "levels/tax/exi021";
            AllLevel[81].nameOfLevel = "Feel the hot";
            AllLevel[81].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[81].doorX = 87;
            AllLevel[81].doorY = 235;
            AllLevel[81].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[81].exitX = 736;
            AllLevel[81].exitY = 391;
            AllLevel[81].numberClimbers = 3;
            AllLevel[81].numberUmbrellas = 3;
            AllLevel[81].numberExploders = 3;
            AllLevel[81].numberBlockers = 3;
            AllLevel[81].numberBuilders = 4;
            AllLevel[81].numberBashers = 3;
            AllLevel[81].numberMiners = 3;
            AllLevel[81].numberDiggers = 3;
            AllLevel[81].MinFrequencyComming = 70;
            AllLevel[81].FrequencyComming = 70;
            AllLevel[81].NbLemmingsToSave = 20;
            AllLevel[81].totalTime = 3;
            AllLevel[82].TotalLemmings = 50;
            AllLevel[82].InitPosX = 300; // Init xscroll
            AllLevel[82].NameLev = "levels/tax/exi022";
            AllLevel[82].nameOfLevel = "Come to visit me";
            AllLevel[82].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[82].doorX = 873;
            AllLevel[82].doorY = 143;
            AllLevel[82].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[82].exitX = 1516;
            AllLevel[82].exitY = 483;
            AllLevel[82].numberClimbers = 0;
            AllLevel[82].numberUmbrellas = 1;
            AllLevel[82].numberExploders = 0;
            AllLevel[82].numberBlockers = 1;
            AllLevel[82].numberBuilders = 10;
            AllLevel[82].numberBashers = 1;
            AllLevel[82].numberMiners = 1;
            AllLevel[82].numberDiggers = 2;
            AllLevel[82].MinFrequencyComming = 1;
            AllLevel[82].FrequencyComming = 1;
            AllLevel[82].NbLemmingsToSave = 50;
            AllLevel[82].totalTime = 4;
            AllLevel[83].TotalLemmings = 100;
            AllLevel[83].InitPosX = 180; // Init xscroll
            AllLevel[83].NameLev = "levels/tax/exi023";
            AllLevel[83].nameOfLevel = "King of the castle";
            AllLevel[83].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[83].doorX = 998;
            AllLevel[83].doorY = 367;
            AllLevel[83].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[83].exitX = 228;
            AllLevel[83].exitY = 164;
            AllLevel[83].numberClimbers = 0;
            AllLevel[83].numberUmbrellas = 0;
            AllLevel[83].numberExploders = 0;
            AllLevel[83].numberBlockers = 0;
            AllLevel[83].numberBuilders = 22;
            AllLevel[83].numberBashers = 0;
            AllLevel[83].numberMiners = 0;
            AllLevel[83].numberDiggers = 0;
            AllLevel[83].MinFrequencyComming = 20;
            AllLevel[83].FrequencyComming = 20;
            AllLevel[83].NbLemmingsToSave = 95;
            AllLevel[83].totalTime = 7;
            AllLevel[84].TotalLemmings = 100;
            AllLevel[84].InitPosX = 0; // Init xscroll
            AllLevel[84].NameLev = "levels/tax/exi024";
            AllLevel[84].nameOfLevel = "Jump by the race";
            AllLevel[84].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[84].doorX = 357;
            AllLevel[84].doorY = 107;
            AllLevel[84].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[84].exitX = 1288;
            AllLevel[84].exitY = 395;
            AllLevel[84].numberClimbers = 0;
            AllLevel[84].numberUmbrellas = 0;
            AllLevel[84].numberExploders = 0;
            AllLevel[84].numberBlockers = 0;
            AllLevel[84].numberBuilders = 30;
            AllLevel[84].numberBashers = 0;
            AllLevel[84].numberMiners = 0;
            AllLevel[84].numberDiggers = 0;
            AllLevel[84].MinFrequencyComming = 50;
            AllLevel[84].FrequencyComming = 50;
            AllLevel[84].NbLemmingsToSave = 99;
            AllLevel[84].totalTime = 4;
            AllLevel[85].TotalLemmings = 100;
            AllLevel[85].InitPosX = 0; // Init xscroll
            AllLevel[85].NameLev = "levels/tax/exi025";
            AllLevel[85].nameOfLevel = "Follow the leader";
            AllLevel[85].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[85].doorX = 650;
            AllLevel[85].doorY = 327;
            AllLevel[85].TypeOfExit = 9; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[85].exitX = 4030;
            AllLevel[85].exitY = 264;
            AllLevel[85].numberClimbers = 0;
            AllLevel[85].numberUmbrellas = 0;
            AllLevel[85].numberExploders = 10;
            AllLevel[85].numberBlockers = 0;
            AllLevel[85].numberBuilders = 7;
            AllLevel[85].numberBashers = 5;
            AllLevel[85].numberMiners = 0;
            AllLevel[85].numberDiggers = 0;
            AllLevel[85].MinFrequencyComming = 20;
            AllLevel[85].FrequencyComming = 20;
            AllLevel[85].NbLemmingsToSave = 90;
            AllLevel[85].totalTime = 6;
            AllLevel[86].TotalLemmings = 100;
            AllLevel[86].InitPosX = 0; // Init xscroll
            AllLevel[86].NameLev = "levels/tax/exi026";
            AllLevel[86].nameOfLevel = "Triple problem";
            AllLevel[86].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[86].doorX = 84;
            AllLevel[86].doorY = 382;
            AllLevel[86].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[86].exitX = 561;
            AllLevel[86].exitY = 324;
            AllLevel[86].numberClimbers = 10;
            AllLevel[86].numberUmbrellas = 10;
            AllLevel[86].numberExploders = 10;
            AllLevel[86].numberBlockers = 10;
            AllLevel[86].numberBuilders = 12;
            AllLevel[86].numberBashers = 10;
            AllLevel[86].numberMiners = 10;
            AllLevel[86].numberDiggers = 10;
            AllLevel[86].MinFrequencyComming = 80;
            AllLevel[86].FrequencyComming = 80;
            AllLevel[86].NbLemmingsToSave = 99;
            AllLevel[86].totalTime = 7;
            AllLevel[87].TotalLemmings = 100;
            AllLevel[87].InitPosX = 370; // Init xscroll
            AllLevel[87].NameLev = "levels/tax/exi027";
            AllLevel[87].nameOfLevel = "Call the bombers";
            AllLevel[87].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[87].doorX = 760;
            AllLevel[87].doorY = 330;
            AllLevel[87].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[87].exitX = 1326;
            AllLevel[87].exitY = 208;
            AllLevel[87].numberClimbers = 0;
            AllLevel[87].numberUmbrellas = 0;
            AllLevel[87].numberExploders = 10;
            AllLevel[87].numberBlockers = 10;
            AllLevel[87].numberBuilders = 10;
            AllLevel[87].numberBashers = 0;
            AllLevel[87].numberMiners = 0;
            AllLevel[87].numberDiggers = 0;
            AllLevel[87].MinFrequencyComming = 10;
            AllLevel[87].FrequencyComming = 10;
            AllLevel[87].NbLemmingsToSave = 60;
            AllLevel[87].totalTime = 7;
            AllLevel[88].TotalLemmings = 100;
            AllLevel[88].InitPosX = 350; // Init xscroll
            AllLevel[88].NameLev = "levels/tax/exi028";
            AllLevel[88].nameOfLevel = "Poor Lemmings";
            AllLevel[88].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[88].doorX = 650;
            AllLevel[88].doorY = 70;
            AllLevel[88].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[88].exitX = 2613;
            AllLevel[88].exitY = 239;
            AllLevel[88].numberClimbers = 1;
            AllLevel[88].numberUmbrellas = 1;
            AllLevel[88].numberExploders = 4;
            AllLevel[88].numberBlockers = 4;
            AllLevel[88].numberBuilders = 9; // here needs 1 more than psp for saved the water width
            AllLevel[88].numberBashers = 3;
            AllLevel[88].numberMiners = 1;
            AllLevel[88].numberDiggers = 0;
            AllLevel[88].MinFrequencyComming = 1;
            AllLevel[88].FrequencyComming = 1;
            AllLevel[88].NbLemmingsToSave = 70;
            AllLevel[88].totalTime = 7;
            AllLevel[89].TotalLemmings = 100;
            AllLevel[89].InitPosX = 0; // Init xscroll
            AllLevel[89].NameLev = "levels/tax/exi029";
            AllLevel[89].nameOfLevel = "How to clean the road?";
            AllLevel[89].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[89].doorX = 310;
            AllLevel[89].doorY = 30;
            AllLevel[89].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[89].exitX = 2086;
            AllLevel[89].exitY = 163;
            AllLevel[89].numberClimbers = 10;
            AllLevel[89].numberUmbrellas = 10;
            AllLevel[89].numberExploders = 10;
            AllLevel[89].numberBlockers = 10;
            AllLevel[89].numberBuilders = 5;
            AllLevel[89].numberBashers = 10;
            AllLevel[89].numberMiners = 10;
            AllLevel[89].numberDiggers = 10;
            AllLevel[89].MinFrequencyComming = 80;
            AllLevel[89].FrequencyComming = 80;
            AllLevel[89].NbLemmingsToSave = 95;
            AllLevel[89].totalTime = 6;
            AllLevel[90].TotalLemmings = 30;
            AllLevel[90].InitPosX = 0; // Init xscroll
            AllLevel[90].NameLev = "levels/tax/exi030";
            AllLevel[90].nameOfLevel = "All we finish falling";
            AllLevel[90].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[90].doorX = 547;
            AllLevel[90].doorY = 30;
            AllLevel[90].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[90].exitX = 2525;
            AllLevel[90].exitY = 383;
            AllLevel[90].numberClimbers = 0;
            AllLevel[90].numberUmbrellas = 0;
            AllLevel[90].numberExploders = 0;
            AllLevel[90].numberBlockers = 0;
            AllLevel[90].numberBuilders = 0;
            AllLevel[90].numberBashers = 0;
            AllLevel[90].numberMiners = 0;
            AllLevel[90].numberDiggers = 60;
            AllLevel[90].MinFrequencyComming = 1;
            AllLevel[90].FrequencyComming = 1;
            AllLevel[90].NbLemmingsToSave = 30;
            AllLevel[90].totalTime = 4;
            // MAYHEM LEVELS MEYHEM LEVELS ////////////////////////////////////////////////////////////////////////////
            AllLevel[91].TotalLemmings = 100;
            AllLevel[91].InitPosX = 300; // Init xscroll
            AllLevel[91].NameLev = "levels/may/cao001";
            AllLevel[91].nameOfLevel = "Steel nerves";
            AllLevel[91].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[91].doorX = 734;
            AllLevel[91].doorY = 46;
            AllLevel[91].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[91].exitX = 2900;
            AllLevel[91].exitY = 78;
            AllLevel[91].numberClimbers = 0;
            AllLevel[91].numberUmbrellas = 5;
            AllLevel[91].numberExploders = 10;
            AllLevel[91].numberBlockers = 5;
            AllLevel[91].numberBuilders = 30;
            AllLevel[91].numberBashers = 0;
            AllLevel[91].numberMiners = 0;
            AllLevel[91].numberDiggers = 5;
            AllLevel[91].MinFrequencyComming = 15;
            AllLevel[91].FrequencyComming = 15;
            AllLevel[91].NbLemmingsToSave = 90;
            AllLevel[91].totalTime = 11;
            AllLevel[92].TotalLemmings = 100;
            AllLevel[92].InitPosX = 1077; // Init xscroll
            AllLevel[92].NameLev = "levels/may/cao002";
            AllLevel[92].nameOfLevel = "The Boiler Room";
            AllLevel[92].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[92].doorX = 1542;
            AllLevel[92].doorY = 241;
            AllLevel[92].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[92].exitX = 649;
            AllLevel[92].exitY = 198;
            AllLevel[92].numberClimbers = 10;
            AllLevel[92].numberUmbrellas = 5;
            AllLevel[92].numberExploders = 10;
            AllLevel[92].numberBlockers = 10;
            AllLevel[92].numberBuilders = 30;
            AllLevel[92].numberBashers = 0;
            AllLevel[92].numberMiners = 0;
            AllLevel[92].numberDiggers = 0;
            AllLevel[92].MinFrequencyComming = 30;
            AllLevel[92].FrequencyComming = 30;
            AllLevel[92].NbLemmingsToSave = 90;
            AllLevel[92].totalTime = 8;
            AllLevel[93].TotalLemmings = 30;
            AllLevel[93].InitPosX = 200; // Init xscroll
            AllLevel[93].NameLev = "levels/may/cao003";
            AllLevel[93].nameOfLevel = "Heroic Times";
            AllLevel[93].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[93].doorX = 460;
            AllLevel[93].doorY = 10;
            AllLevel[93].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[93].exitX = 1381;
            AllLevel[93].exitY = 458;
            AllLevel[93].numberClimbers = 1;
            AllLevel[93].numberUmbrellas = 1;
            AllLevel[93].numberExploders = 1;
            AllLevel[93].numberBlockers = 1;
            AllLevel[93].numberBuilders = 1;
            AllLevel[93].numberBashers = 1;
            AllLevel[93].numberMiners = 1;
            AllLevel[93].numberDiggers = 1;
            AllLevel[93].MinFrequencyComming = 50;
            AllLevel[93].FrequencyComming = 50;
            AllLevel[93].NbLemmingsToSave = 30;
            AllLevel[93].totalTime = 2;
            AllLevel[94].TotalLemmings = 50;
            AllLevel[94].InitPosX = 0; // Init xscroll
            AllLevel[94].NameLev = "levels/may/cao004";
            AllLevel[94].nameOfLevel = "Crossroads";
            AllLevel[94].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[94].doorX = 232;
            AllLevel[94].doorY = 372;
            AllLevel[94].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[94].exitX = 1443;
            AllLevel[94].exitY = 495;
            AllLevel[94].numberClimbers = 0;
            AllLevel[94].numberUmbrellas = 0;
            AllLevel[94].numberExploders = 10;
            AllLevel[94].numberBlockers = 0;
            AllLevel[94].numberBuilders = 0;
            AllLevel[94].numberBashers = 10;
            AllLevel[94].numberMiners = 0;
            AllLevel[94].numberDiggers = 0;
            AllLevel[94].MinFrequencyComming = 99;
            AllLevel[94].FrequencyComming = 99;
            AllLevel[94].NbLemmingsToSave = 40;
            AllLevel[94].totalTime = 2;
            AllLevel[95].TotalLemmings = 80;
            AllLevel[95].InitPosX = 1362; // Init xscroll
            AllLevel[95].NameLev = "levels/may/cao005";
            AllLevel[95].nameOfLevel = "Down, Forward, Up in order";
            AllLevel[95].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[95].doorX = 1828;
            AllLevel[95].doorY = 10;
            AllLevel[95].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[95].exitX = 822;
            AllLevel[95].exitY = 203;
            AllLevel[95].numberClimbers = 2;
            AllLevel[95].numberUmbrellas = 2;
            AllLevel[95].numberExploders = 10;
            AllLevel[95].numberBlockers = 10;
            AllLevel[95].numberBuilders = 5;
            AllLevel[95].numberBashers = 1;
            AllLevel[95].numberMiners = 0;
            AllLevel[95].numberDiggers = 5;
            AllLevel[95].MinFrequencyComming = 80;
            AllLevel[95].FrequencyComming = 80;
            AllLevel[95].NbLemmingsToSave = 60;
            AllLevel[95].totalTime = 7;
            AllLevel[96].TotalLemmings = 75;
            AllLevel[96].InitPosX = 1362; // Init xscroll
            AllLevel[96].NameLev = "levels/may/cao006";
            AllLevel[96].nameOfLevel = "In one way or another";
            AllLevel[96].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[96].doorX = 1590;
            AllLevel[96].doorY = 78;
            AllLevel[96].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[96].exitX = 1684;
            AllLevel[96].exitY = 458;
            AllLevel[96].numberClimbers = 0;
            AllLevel[96].numberUmbrellas = 0;
            AllLevel[96].numberExploders = 10;
            AllLevel[96].numberBlockers = 0;
            AllLevel[96].numberBuilders = 15;
            AllLevel[96].numberBashers = 0;
            AllLevel[96].numberMiners = 5;
            AllLevel[96].numberDiggers = 5;
            AllLevel[96].MinFrequencyComming = 50;
            AllLevel[96].FrequencyComming = 50;
            AllLevel[96].NbLemmingsToSave = 75;
            AllLevel[96].totalTime = 6;
            AllLevel[97].TotalLemmings = 50;
            AllLevel[97].InitPosX = 826; // Init xscroll
            AllLevel[97].NameLev = "levels/may/cao007";
            AllLevel[97].nameOfLevel = "Opossite forces";
            AllLevel[97].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[97].doorX = 1160;
            AllLevel[97].doorY = 130;
            AllLevel[97].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[97].exitX = 2298;
            AllLevel[97].exitY = 334;
            AllLevel[97].numberClimbers = 1;
            AllLevel[97].numberUmbrellas = 10;
            AllLevel[97].numberExploders = 0;
            AllLevel[97].numberBlockers = 0;
            AllLevel[97].numberBuilders = 6;
            AllLevel[97].numberBashers = 4;
            AllLevel[97].numberMiners = 0;
            AllLevel[97].numberDiggers = 4;
            AllLevel[97].MinFrequencyComming = 50;
            AllLevel[97].FrequencyComming = 50;
            AllLevel[97].NbLemmingsToSave = 45;
            AllLevel[97].totalTime = 7;
            AllLevel[98].TotalLemmings = 100;
            AllLevel[98].InitPosX = 0; // Init xscroll
            AllLevel[98].NameLev = "levels/may/cao008";
            AllLevel[98].nameOfLevel = "Silly the last";
            AllLevel[98].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[98].doorX = 410;
            AllLevel[98].doorY = 243;
            AllLevel[98].TypeOfExit = 5; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[98].exitX = 1512;
            AllLevel[98].exitY = 480;
            AllLevel[98].numberClimbers = 5;
            AllLevel[98].numberUmbrellas = 0;
            AllLevel[98].numberExploders = 5;
            AllLevel[98].numberBlockers = 5;
            AllLevel[98].numberBuilders = 10;
            AllLevel[98].numberBashers = 0;
            AllLevel[98].numberMiners = 5;
            AllLevel[98].numberDiggers = 5;
            AllLevel[98].MinFrequencyComming = 55;
            AllLevel[98].FrequencyComming = 55;
            AllLevel[98].NbLemmingsToSave = 90;
            AllLevel[98].totalTime = 7;
            AllLevel[99].TotalLemmings = 100;
            AllLevel[99].InitPosX = MyGame.GameResolution.X; // Init xscroll
            AllLevel[99].NameLev = "levels/may/cao009";
            AllLevel[99].nameOfLevel = "Maldition of the Pharaons";
            AllLevel[99].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[99].doorX = 1815;
            AllLevel[99].doorY = 233;
            AllLevel[99].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[99].exitX = 77;
            AllLevel[99].exitY = 198;
            AllLevel[99].numberClimbers = 0;
            AllLevel[99].numberUmbrellas = 0;
            AllLevel[99].numberExploders = 20;
            AllLevel[99].numberBlockers = 1;
            AllLevel[99].numberBuilders = 12;
            AllLevel[99].numberBashers = 5;
            AllLevel[99].numberMiners = 0;
            AllLevel[99].numberDiggers = 1;
            AllLevel[99].MinFrequencyComming = 90;
            AllLevel[99].FrequencyComming = 90;
            AllLevel[99].NbLemmingsToSave = 99;
            AllLevel[99].totalTime = 6;
            AllLevel[100].TotalLemmings = 75;
            AllLevel[100].InitPosX = 614; // Init xscroll
            AllLevel[100].NameLev = "levels/may/cao010";
            AllLevel[100].nameOfLevel = "Pillars of Hercules";
            AllLevel[100].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[100].doorX = 1045;
            AllLevel[100].doorY = 2;
            AllLevel[100].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[100].exitX = 2004;
            AllLevel[100].exitY = 176;
            AllLevel[100].numberClimbers = 2;
            AllLevel[100].numberUmbrellas = 3;
            AllLevel[100].numberExploders = 4;
            AllLevel[100].numberBlockers = 2;
            AllLevel[100].numberBuilders = 20;
            AllLevel[100].numberBashers = 4;
            AllLevel[100].numberMiners = 0;
            AllLevel[100].numberDiggers = 2;
            AllLevel[100].MinFrequencyComming = 1;
            AllLevel[100].FrequencyComming = 1;
            AllLevel[100].NbLemmingsToSave = 50;
            AllLevel[100].totalTime = 7;
            AllLevel[101].TotalLemmings = 60;
            AllLevel[101].InitPosX = 0; // Init xscroll
            AllLevel[101].NameLev = "levels/may/cao011";
            AllLevel[101].nameOfLevel = "All we finish falling again";
            AllLevel[101].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[101].doorX = 561;
            AllLevel[101].doorY = 82;
            AllLevel[101].TypeOfExit = 5; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[101].exitX = 2483;
            AllLevel[101].exitY = 382;
            AllLevel[101].numberClimbers = 0;
            AllLevel[101].numberUmbrellas = 0;
            AllLevel[101].numberExploders = 0;
            AllLevel[101].numberBlockers = 0;
            AllLevel[101].numberBuilders = 0;
            AllLevel[101].numberBashers = 0;
            AllLevel[101].numberMiners = 0;
            AllLevel[101].numberDiggers = 80;
            AllLevel[101].MinFrequencyComming = 1;
            AllLevel[101].FrequencyComming = 1;
            AllLevel[101].NbLemmingsToSave = 60;
            AllLevel[101].totalTime = 4;
            AllLevel[102].TotalLemmings = 75;
            AllLevel[102].InitPosX = 0; // Init xscroll
            AllLevel[102].NameLev = "levels/may/cao012";
            AllLevel[102].nameOfLevel = "Faraway";
            AllLevel[102].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[102].doorX = 778;
            AllLevel[102].doorY = 99;
            AllLevel[102].TypeOfExit = 5; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[102].exitX = 1095;
            AllLevel[102].exitY = 230;
            AllLevel[102].numberClimbers = 2;
            AllLevel[102].numberUmbrellas = 1;
            AllLevel[102].numberExploders = 0;
            AllLevel[102].numberBlockers = 0;
            AllLevel[102].numberBuilders = 20;
            AllLevel[102].numberBashers = 5;
            AllLevel[102].numberMiners = 5;
            AllLevel[102].numberDiggers = 5;
            AllLevel[102].MinFrequencyComming = 50;
            AllLevel[102].FrequencyComming = 50;
            AllLevel[102].NbLemmingsToSave = 75;
            AllLevel[102].totalTime = 6;
            AllLevel[103].TotalLemmings = 2;
            AllLevel[103].InitPosX = 1500; // Init xscroll
            AllLevel[103].NameLev = "levels/may/cao013";
            AllLevel[103].nameOfLevel = "The big joke";
            AllLevel[103].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[103].doorX = 1652;
            AllLevel[103].doorY = 50;
            AllLevel[103].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[103].exitX = 2587;
            AllLevel[103].exitY = 309;
            AllLevel[103].numberClimbers = 1;
            AllLevel[103].numberUmbrellas = 1;
            AllLevel[103].numberExploders = 2;
            AllLevel[103].numberBlockers = 2;
            AllLevel[103].numberBuilders = 2;
            AllLevel[103].numberBashers = 10;
            AllLevel[103].numberMiners = 10;
            AllLevel[103].numberDiggers = 10;
            AllLevel[103].MinFrequencyComming = 50;
            AllLevel[103].FrequencyComming = 50;
            AllLevel[103].NbLemmingsToSave = 2;
            AllLevel[103].totalTime = 7;
            AllLevel[104].TotalLemmings = 80;
            AllLevel[104].InitPosX = 0; // Init xscroll
            AllLevel[104].NameLev = "levels/may/cao014";
            AllLevel[104].nameOfLevel = "Guisantes soup";
            AllLevel[104].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[104].doorX = 210;
            AllLevel[104].doorY = 350;
            AllLevel[104].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[104].exitX = 1947;
            AllLevel[104].exitY = 468;
            AllLevel[104].numberClimbers = 0;
            AllLevel[104].numberUmbrellas = 0;
            AllLevel[104].numberExploders = 10;
            AllLevel[104].numberBlockers = 10;
            AllLevel[104].numberBuilders = 30;
            AllLevel[104].numberBashers = 0;
            AllLevel[104].numberMiners = 0;
            AllLevel[104].numberDiggers = 21;
            AllLevel[104].MinFrequencyComming = 70;
            AllLevel[104].FrequencyComming = 70;
            AllLevel[104].NbLemmingsToSave = 75;
            AllLevel[104].totalTime = 7;
            AllLevel[105].TotalLemmings = 100;
            AllLevel[105].InitPosX = 0; // Init xscroll
            AllLevel[105].NameLev = "levels/may/cao015";
            AllLevel[105].nameOfLevel = "Fried Chicken";
            AllLevel[105].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[105].doorX = 388;
            AllLevel[105].doorY = 225;
            AllLevel[105].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[105].exitX = 1432;
            AllLevel[105].exitY = 216;
            AllLevel[105].numberClimbers = 10;
            AllLevel[105].numberUmbrellas = 10;
            AllLevel[105].numberExploders = 10;
            AllLevel[105].numberBlockers = 10;
            AllLevel[105].numberBuilders = 10;
            AllLevel[105].numberBashers = 10;
            AllLevel[105].numberMiners = 10;
            AllLevel[105].numberDiggers = 10;
            AllLevel[105].MinFrequencyComming = 60;
            AllLevel[105].FrequencyComming = 60;
            AllLevel[105].NbLemmingsToSave = 96;
            AllLevel[105].totalTime = 3;
            AllLevel[106].TotalLemmings = 50;
            AllLevel[106].InitPosX = 0; // Init xscroll
            AllLevel[106].NameLev = "levels/may/cao016";
            AllLevel[106].nameOfLevel = "Just a minute";
            AllLevel[106].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[106].doorX = 500;
            AllLevel[106].doorY = 225;
            AllLevel[106].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[106].exitX = 1294;
            AllLevel[106].exitY = 358;
            AllLevel[106].numberClimbers = 0;
            AllLevel[106].numberUmbrellas = 1;
            AllLevel[106].numberExploders = 5;
            AllLevel[106].numberBlockers = 0;
            AllLevel[106].numberBuilders = 0;
            AllLevel[106].numberBashers = 5;
            AllLevel[106].numberMiners = 0;
            AllLevel[106].numberDiggers = 5;
            AllLevel[106].MinFrequencyComming = 10;
            AllLevel[106].FrequencyComming = 10;
            AllLevel[106].NbLemmingsToSave = 50;
            AllLevel[106].totalTime = 1;
            AllLevel[107].TotalLemmings = 80;
            AllLevel[107].InitPosX = 0; // Init xscroll
            AllLevel[107].NameLev = "levels/may/cao017";
            AllLevel[107].nameOfLevel = "Rolling stones";
            AllLevel[107].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[107].doorX = 396;
            AllLevel[107].doorY = 80;
            AllLevel[107].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[107].exitX = 1658;
            AllLevel[107].exitY = 214;
            AllLevel[107].numberClimbers = 5;
            AllLevel[107].numberUmbrellas = 2;
            AllLevel[107].numberExploders = 0;
            AllLevel[107].numberBlockers = 2;
            AllLevel[107].numberBuilders = 12;
            AllLevel[107].numberBashers = 5;
            AllLevel[107].numberMiners = 5;
            AllLevel[107].numberDiggers = 5;
            AllLevel[107].MinFrequencyComming = 50;
            AllLevel[107].FrequencyComming = 50;
            AllLevel[107].NbLemmingsToSave = 70;
            AllLevel[107].totalTime = 5;
            AllLevel[108].TotalLemmings = 100;
            AllLevel[108].InitPosX = 930; // Init xscroll
            AllLevel[108].NameLev = "levels/may/cao018";
            AllLevel[108].nameOfLevel = "And then they were four";
            AllLevel[108].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[108].doorX = 1240;
            AllLevel[108].doorY = 75;
            AllLevel[108].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[108].exitX = 1413;
            AllLevel[108].exitY = 292;
            AllLevel[108].numberClimbers = 2;
            AllLevel[108].numberUmbrellas = 0;
            AllLevel[108].numberExploders = 20;
            AllLevel[108].numberBlockers = 10;
            AllLevel[108].numberBuilders = 30;
            AllLevel[108].numberBashers = 1;
            AllLevel[108].numberMiners = 2;
            AllLevel[108].numberDiggers = 1;
            AllLevel[108].MinFrequencyComming = 1;
            AllLevel[108].FrequencyComming = 1;
            AllLevel[108].NbLemmingsToSave = 90;
            AllLevel[108].totalTime = 12;
            AllLevel[109].TotalLemmings = 50;
            AllLevel[109].InitPosX = 0; // Init xscroll
            AllLevel[109].NameLev = "levels/may/cao019";
            AllLevel[109].nameOfLevel = "Time to wake up";
            AllLevel[109].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[109].doorX = 715;
            AllLevel[109].doorY = 400;
            AllLevel[109].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[109].exitX = 1444;
            AllLevel[109].exitY = 218;
            AllLevel[109].numberClimbers = 2;
            AllLevel[109].numberUmbrellas = 0;
            AllLevel[109].numberExploders = 4;
            AllLevel[109].numberBlockers = 0;
            AllLevel[109].numberBuilders = 20;
            AllLevel[109].numberBashers = 0;
            AllLevel[109].numberMiners = 0;
            AllLevel[109].numberDiggers = 0;
            AllLevel[109].MinFrequencyComming = 20;
            AllLevel[109].FrequencyComming = 20;
            AllLevel[109].NbLemmingsToSave = 46;
            AllLevel[109].totalTime = 7;
            AllLevel[110].TotalLemmings = 50;
            AllLevel[110].InitPosX = 0; // Init xscroll
            AllLevel[110].NameLev = "levels/may/cao020";
            AllLevel[110].nameOfLevel = "No artificial colors";
            AllLevel[110].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[110].doorX = 630;
            AllLevel[110].doorY = 5;
            AllLevel[110].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[110].exitX = 1261;
            AllLevel[110].exitY = 153;
            AllLevel[110].numberClimbers = 2;
            AllLevel[110].numberUmbrellas = 0;
            AllLevel[110].numberExploders = 0;
            AllLevel[110].numberBlockers = 1;
            AllLevel[110].numberBuilders = 1;
            AllLevel[110].numberBashers = 1;
            AllLevel[110].numberMiners = 2;
            AllLevel[110].numberDiggers = 0;
            AllLevel[110].MinFrequencyComming = 85;
            AllLevel[110].FrequencyComming = 85;
            AllLevel[110].NbLemmingsToSave = 50;
            AllLevel[110].totalTime = 7;
            AllLevel[111].TotalLemmings = 20;
            AllLevel[111].InitPosX = 0; // Init xscroll
            AllLevel[111].NameLev = "levels/may/cao021";
            AllLevel[111].nameOfLevel = "With a little bit of lemming please";
            AllLevel[111].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[111].doorX = 403;
            AllLevel[111].doorY = 14;
            AllLevel[111].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[111].exitX = 1270;
            AllLevel[111].exitY = 356;
            AllLevel[111].numberClimbers = 1;
            AllLevel[111].numberUmbrellas = 20;
            AllLevel[111].numberExploders = 0;
            AllLevel[111].numberBlockers = 0;
            AllLevel[111].numberBuilders = 4;
            AllLevel[111].numberBashers = 1;
            AllLevel[111].numberMiners = 0;
            AllLevel[111].numberDiggers = 1;
            AllLevel[111].MinFrequencyComming = 1;
            AllLevel[111].FrequencyComming = 1;
            AllLevel[111].NbLemmingsToSave = 20;
            AllLevel[111].totalTime = 7;
            AllLevel[112].TotalLemmings = 100;
            AllLevel[112].InitPosX = 0; // Init xscroll
            AllLevel[112].NameLev = "levels/may/cao022";
            AllLevel[112].nameOfLevel = "Bestial level two";
            AllLevel[112].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[112].doorX = 25;
            AllLevel[112].doorY = 276;
            AllLevel[112].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[112].exitX = 2507;
            AllLevel[112].exitY = 412;
            AllLevel[112].numberClimbers = 10;
            AllLevel[112].numberUmbrellas = 10;
            AllLevel[112].numberExploders = 10;
            AllLevel[112].numberBlockers = 10;
            AllLevel[112].numberBuilders = 15;
            AllLevel[112].numberBashers = 10;
            AllLevel[112].numberMiners = 10;
            AllLevel[112].numberDiggers = 10;
            AllLevel[112].MinFrequencyComming = 75;
            AllLevel[112].FrequencyComming = 75;
            AllLevel[112].NbLemmingsToSave = 85;
            AllLevel[112].totalTime = 8;
            AllLevel[113].TotalLemmings = 100;
            AllLevel[113].InitPosX = 2500; // Init xscroll
            AllLevel[113].NameLev = "levels/may/cao023";
            AllLevel[113].nameOfLevel = "On your feet";
            AllLevel[113].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[113].doorX = 2967;
            AllLevel[113].doorY = 366;
            AllLevel[113].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[113].exitX = 234;
            AllLevel[113].exitY = 463;
            AllLevel[113].numberClimbers = 0;
            AllLevel[113].numberUmbrellas = 1;
            AllLevel[113].numberExploders = 20;
            AllLevel[113].numberBlockers = 20;
            AllLevel[113].numberBuilders = 30;
            AllLevel[113].numberBashers = 5;
            AllLevel[113].numberMiners = 0;
            AllLevel[113].numberDiggers = 0;
            AllLevel[113].MinFrequencyComming = 70;
            AllLevel[113].FrequencyComming = 70;
            AllLevel[113].NbLemmingsToSave = 80;
            AllLevel[113].totalTime = 12;
            AllLevel[114].TotalLemmings = 50;
            AllLevel[114].InitPosX = 372; // Init xscroll
            AllLevel[114].NameLev = "levels/may/cao024";
            AllLevel[114].nameOfLevel = "All or Nothing";
            AllLevel[114].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[114].doorX = 722;
            AllLevel[114].doorY = 53;
            AllLevel[114].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[114].exitX = 594;
            AllLevel[114].exitY = 473;
            AllLevel[114].numberClimbers = 0;
            AllLevel[114].numberUmbrellas = 0;
            AllLevel[114].numberExploders = 0;
            AllLevel[114].numberBlockers = 0;
            AllLevel[114].numberBuilders = 0;
            AllLevel[114].numberBashers = 3;
            AllLevel[114].numberMiners = 0;
            AllLevel[114].numberDiggers = 0;
            AllLevel[114].MinFrequencyComming = 10;
            AllLevel[114].FrequencyComming = 10;
            AllLevel[114].NbLemmingsToSave = 50;
            AllLevel[114].totalTime = 2;
            AllLevel[115].TotalLemmings = 100;
            AllLevel[115].InitPosX = 0; // Init xscroll
            AllLevel[115].NameLev = "levels/may/cao025";
            AllLevel[115].nameOfLevel = "Good Time";
            AllLevel[115].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[115].doorX = 237;
            AllLevel[115].doorY = 127;
            AllLevel[115].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[115].exitX = 1247;
            AllLevel[115].exitY = 284;
            AllLevel[115].numberClimbers = 2;
            AllLevel[115].numberUmbrellas = 2;
            AllLevel[115].numberExploders = 0;
            AllLevel[115].numberBlockers = 2;
            AllLevel[115].numberBuilders = 25;
            AllLevel[115].numberBashers = 1;
            AllLevel[115].numberMiners = 1;
            AllLevel[115].numberDiggers = 1;
            AllLevel[115].MinFrequencyComming = 1;
            AllLevel[115].FrequencyComming = 1;
            AllLevel[115].NbLemmingsToSave = 90;
            AllLevel[115].totalTime = 7;
            AllLevel[116].TotalLemmings = 100;
            AllLevel[116].InitPosX = 0; // Init xscroll
            AllLevel[116].NameLev = "levels/may/cao026";
            AllLevel[116].nameOfLevel = "Kessel mines of steel";
            AllLevel[116].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[116].doorX = 260;
            AllLevel[116].doorY = 160;
            AllLevel[116].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[116].exitX = 3486;
            AllLevel[116].exitY = 459;
            AllLevel[116].numberClimbers = 0;
            AllLevel[116].numberUmbrellas = 0;
            AllLevel[116].numberExploders = 15; // original was 10 of all this three
            AllLevel[116].numberBlockers = 15;
            AllLevel[116].numberBuilders = 15;
            AllLevel[116].numberBashers = 0;
            AllLevel[116].numberMiners = 0;
            AllLevel[116].numberDiggers = 0;
            AllLevel[116].MinFrequencyComming = 50;
            AllLevel[116].FrequencyComming = 50;
            AllLevel[116].NbLemmingsToSave = 75;
            AllLevel[116].totalTime = 11;
            AllLevel[117].TotalLemmings = 50;
            AllLevel[117].InitPosX = 0; // Init xscroll
            AllLevel[117].NameLev = "levels/may/cao027";
            AllLevel[117].nameOfLevel = "Just a minute (part two)";
            AllLevel[117].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[117].doorX = 500;
            AllLevel[117].doorY = 225;
            AllLevel[117].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[117].exitX = 1294;
            AllLevel[117].exitY = 358;
            AllLevel[117].numberClimbers = 1;
            AllLevel[117].numberUmbrellas = 1;
            AllLevel[117].numberExploders = 5;
            AllLevel[117].numberBlockers = 0;
            AllLevel[117].numberBuilders = 0;
            AllLevel[117].numberBashers = 5;
            AllLevel[117].numberMiners = 0;
            AllLevel[117].numberDiggers = 5;
            AllLevel[117].MinFrequencyComming = 10;
            AllLevel[117].FrequencyComming = 10;
            AllLevel[117].NbLemmingsToSave = 50;
            AllLevel[117].totalTime = 2;
            AllLevel[118].TotalLemmings = 1;
            AllLevel[118].InitPosX = 2700; // Init xscroll
            AllLevel[118].NameLev = "levels/may/cao028";
            AllLevel[118].nameOfLevel = "Be careful with the step";
            AllLevel[118].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[118].doorX = 3058;
            AllLevel[118].doorY = 119;
            AllLevel[118].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[118].exitX = 1246;
            AllLevel[118].exitY = 383;
            AllLevel[118].numberClimbers = 0;
            AllLevel[118].numberUmbrellas = 0;
            AllLevel[118].numberExploders = 0;
            AllLevel[118].numberBlockers = 0;
            AllLevel[118].numberBuilders = 25;
            AllLevel[118].numberBashers = 10;
            AllLevel[118].numberMiners = 0;
            AllLevel[118].numberDiggers = 15;
            AllLevel[118].MinFrequencyComming = 1;
            AllLevel[118].FrequencyComming = 1;
            AllLevel[118].NbLemmingsToSave = 1;
            AllLevel[118].totalTime = 10;
            AllLevel[119].TotalLemmings = 100;
            AllLevel[119].InitPosX = 2800; // Init xscroll
            AllLevel[119].NameLev = "levels/may/cao029";
            AllLevel[119].nameOfLevel = "Save me";
            AllLevel[119].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[119].doorX = 3250;
            AllLevel[119].doorY = 341;
            AllLevel[119].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[119].exitX = 466;
            AllLevel[119].exitY = 451;
            AllLevel[119].numberClimbers = 0;
            AllLevel[119].numberUmbrellas = 0;
            AllLevel[119].numberExploders = 0;
            AllLevel[119].numberBlockers = 6;
            AllLevel[119].numberBuilders = 15;
            AllLevel[119].numberBashers = 2;
            AllLevel[119].numberMiners = 0;
            AllLevel[119].numberDiggers = 2;
            AllLevel[119].MinFrequencyComming = 50;
            AllLevel[119].FrequencyComming = 50;
            AllLevel[119].NbLemmingsToSave = 80;
            AllLevel[119].totalTime = 10;
            AllLevel[120].TotalLemmings = 80;
            AllLevel[120].InitPosX = 0; // Init xscroll
            AllLevel[120].NameLev = "levels/may/cao030";
            AllLevel[120].nameOfLevel = "Date on the mountain";
            AllLevel[120].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[120].doorX = 99;
            AllLevel[120].doorY = 328;
            AllLevel[120].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[120].exitX = 2084;
            AllLevel[120].exitY = 108;
            AllLevel[120].numberClimbers = 10;
            AllLevel[120].numberUmbrellas = 1;
            AllLevel[120].numberExploders = 10;
            AllLevel[120].numberBlockers = 10;
            AllLevel[120].numberBuilders = 30;
            AllLevel[120].numberBashers = 10;
            AllLevel[120].numberMiners = 10;
            AllLevel[120].numberDiggers = 4;
            AllLevel[120].MinFrequencyComming = 20;
            AllLevel[120].FrequencyComming = 20;
            AllLevel[120].NbLemmingsToSave = 60;
            AllLevel[120].totalTime = 12;
            // BONUS LEVELS 121-156
            AllLevel[121].TotalLemmings = 5;
            AllLevel[121].InitPosX = 0; // Init xscroll
            AllLevel[121].NameLev = "levels/sp/sp001";
            AllLevel[121].nameOfLevel = "Ascend to victory";
            AllLevel[121].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[121].doorX = 245;
            AllLevel[121].doorY = 212;
            AllLevel[121].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[121].exitX = 719;
            AllLevel[121].exitY = 423;
            AllLevel[121].numberClimbers = 5;
            AllLevel[121].numberUmbrellas = 0;
            AllLevel[121].numberExploders = 0;
            AllLevel[121].numberBlockers = 0;
            AllLevel[121].numberBuilders = 0;
            AllLevel[121].numberBashers = 0;
            AllLevel[121].numberMiners = 0;
            AllLevel[121].numberDiggers = 0;
            AllLevel[121].MinFrequencyComming = 50;
            AllLevel[121].FrequencyComming = 50;
            AllLevel[121].NbLemmingsToSave = 1; // 20% of 5 = 1 (??*??/100)
            AllLevel[121].totalTime = 8;
            AllLevel[122].TotalLemmings = 5;
            AllLevel[122].InitPosX = 0; // Init xscroll
            AllLevel[122].NameLev = "levels/sp/sp002";
            AllLevel[122].nameOfLevel = "Saved by floaters";
            AllLevel[122].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[122].doorX = 90;
            AllLevel[122].doorY = 221;
            AllLevel[122].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[122].exitX = 1195;
            AllLevel[122].exitY = 392;
            AllLevel[122].numberClimbers = 5;
            AllLevel[122].numberUmbrellas = 5;
            AllLevel[122].numberExploders = 0;
            AllLevel[122].numberBlockers = 0;
            AllLevel[122].numberBuilders = 0;
            AllLevel[122].numberBashers = 0;
            AllLevel[122].numberMiners = 0;
            AllLevel[122].numberDiggers = 0;
            AllLevel[122].MinFrequencyComming = 50;
            AllLevel[122].FrequencyComming = 50;
            AllLevel[122].NbLemmingsToSave = 1; // 20% of 5 = 1 (??*??/100)
            AllLevel[122].totalTime = 10;
            AllLevel[123].TotalLemmings = 5;
            AllLevel[123].InitPosX = 0; // Init xscroll
            AllLevel[123].NameLev = "levels/sp/sp003";
            AllLevel[123].nameOfLevel = "Dig and Knock";
            AllLevel[123].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[123].doorX = 89;
            AllLevel[123].doorY = 5;
            AllLevel[123].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[123].exitX = 1132;
            AllLevel[123].exitY = 388;
            AllLevel[123].numberClimbers = 5;
            AllLevel[123].numberUmbrellas = 0;
            AllLevel[123].numberExploders = 0;
            AllLevel[123].numberBlockers = 0;
            AllLevel[123].numberBuilders = 0;
            AllLevel[123].numberBashers = 0;
            AllLevel[123].numberMiners = 5;
            AllLevel[123].numberDiggers = 5;
            AllLevel[123].MinFrequencyComming = 50;
            AllLevel[123].FrequencyComming = 50;
            AllLevel[123].NbLemmingsToSave = 1; // 20% of 5 = 1 (??*??/100)
            AllLevel[123].totalTime = 10;
            AllLevel[124].TotalLemmings = 20;
            AllLevel[124].InitPosX = 0; // Init xscroll
            AllLevel[124].NameLev = "levels/sp/sp004";
            AllLevel[124].nameOfLevel = "First block then explode";
            AllLevel[124].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[124].doorX = 310;
            AllLevel[124].doorY = 20;
            AllLevel[124].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[124].exitX = 1475;
            AllLevel[124].exitY = 436;
            AllLevel[124].numberClimbers = 0;
            AllLevel[124].numberUmbrellas = 0;
            AllLevel[124].numberExploders = 10;
            AllLevel[124].numberBlockers = 10;
            AllLevel[124].numberBuilders = 0;
            AllLevel[124].numberBashers = 0;
            AllLevel[124].numberMiners = 0;
            AllLevel[124].numberDiggers = 0;
            AllLevel[124].MinFrequencyComming = 80;
            AllLevel[124].FrequencyComming = 80;
            AllLevel[124].NbLemmingsToSave = 5; // 25% of 20 = 5 (??*??/100)
            AllLevel[124].totalTime = 10;
            AllLevel[125].TotalLemmings = 10;
            AllLevel[125].InitPosX = 0; // Init xscroll
            AllLevel[125].NameLev = "levels/sp/sp005";
            AllLevel[125].nameOfLevel = "Make a build and use miner";
            AllLevel[125].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[125].doorX = 183;
            AllLevel[125].doorY = 30;
            AllLevel[125].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[125].exitX = 1442;
            AllLevel[125].exitY = 406;
            AllLevel[125].numberClimbers = 0;
            AllLevel[125].numberUmbrellas = 0;
            AllLevel[125].numberExploders = 0;
            AllLevel[125].numberBlockers = 0;
            AllLevel[125].numberBuilders = 10;
            AllLevel[125].numberBashers = 0;
            AllLevel[125].numberMiners = 10;
            AllLevel[125].numberDiggers = 0;
            AllLevel[125].MinFrequencyComming = 60;
            AllLevel[125].FrequencyComming = 60;
            AllLevel[125].NbLemmingsToSave = 5;
            AllLevel[125].totalTime = 10;
            AllLevel[126].TotalLemmings = 10;
            AllLevel[126].InitPosX = 255; // Init xscroll
            AllLevel[126].NameLev = "levels/sp/sp006";
            AllLevel[126].nameOfLevel = "Bash in the right way";
            AllLevel[126].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[126].doorX = 452;
            AllLevel[126].doorY = 89;
            AllLevel[126].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[126].exitX = 885;
            AllLevel[126].exitY = 249;
            AllLevel[126].numberClimbers = 0;
            AllLevel[126].numberUmbrellas = 0;
            AllLevel[126].numberExploders = 0;
            AllLevel[126].numberBlockers = 0;
            AllLevel[126].numberBuilders = 10;
            AllLevel[126].numberBashers = 10;
            AllLevel[126].numberMiners = 0;
            AllLevel[126].numberDiggers = 0;
            AllLevel[126].MinFrequencyComming = 60;
            AllLevel[126].FrequencyComming = 60;
            AllLevel[126].NbLemmingsToSave = 5;
            AllLevel[126].totalTime = 10;
            AllLevel[127].TotalLemmings = 10;
            AllLevel[127].InitPosX = 1048; // Init xscroll
            AllLevel[127].NameLev = "levels/sp/sp007";
            AllLevel[127].nameOfLevel = "Crystals";
            AllLevel[127].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[127].doorX = 1965;
            AllLevel[127].doorY = 23;
            AllLevel[127].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[127].exitX = 880;
            AllLevel[127].exitY = 160;
            AllLevel[127].numberClimbers = 0;
            AllLevel[127].numberUmbrellas = 0;
            AllLevel[127].numberExploders = 2;
            AllLevel[127].numberBlockers = 2;
            AllLevel[127].numberBuilders = 4;
            AllLevel[127].numberBashers = 4;
            AllLevel[127].numberMiners = 1;
            AllLevel[127].numberDiggers = 1;
            AllLevel[127].MinFrequencyComming = 50;
            AllLevel[127].FrequencyComming = 50;
            AllLevel[127].NbLemmingsToSave = 9;
            AllLevel[127].totalTime = 10;
            AllLevel[128].TotalLemmings = 5;
            AllLevel[128].InitPosX = 277; // Init xscroll
            AllLevel[128].NameLev = "levels/sp/sp008";
            AllLevel[128].nameOfLevel = "So close, So far";
            AllLevel[128].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[128].doorX = 700;
            AllLevel[128].doorY = 246;
            AllLevel[128].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[128].exitX = 527;
            AllLevel[128].exitY = 319;
            AllLevel[128].numberClimbers = 5;
            AllLevel[128].numberUmbrellas = 0;
            AllLevel[128].numberExploders = 0;
            AllLevel[128].numberBlockers = 0;
            AllLevel[128].numberBuilders = 3;
            AllLevel[128].numberBashers = 3;
            AllLevel[128].numberMiners = 0;
            AllLevel[128].numberDiggers = 1;
            AllLevel[128].MinFrequencyComming = 50;
            AllLevel[128].FrequencyComming = 50;
            AllLevel[128].NbLemmingsToSave = 5;
            AllLevel[128].totalTime = 4;
            AllLevel[129].TotalLemmings = 10;
            AllLevel[129].InitPosX = 1060; // Init xscroll
            AllLevel[129].NameLev = "levels/sp/sp009";
            AllLevel[129].nameOfLevel = "Let's talk about pillar";
            AllLevel[129].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[129].doorX = 1365;
            AllLevel[129].doorY = 190;
            AllLevel[129].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[129].exitX = 516;
            AllLevel[129].exitY = 376;
            AllLevel[129].numberClimbers = 0;
            AllLevel[129].numberUmbrellas = 0;
            AllLevel[129].numberExploders = 2;
            AllLevel[129].numberBlockers = 2;
            AllLevel[129].numberBuilders = 9;
            AllLevel[129].numberBashers = 0;
            AllLevel[129].numberMiners = 4;
            AllLevel[129].numberDiggers = 4;
            AllLevel[129].MinFrequencyComming = 25;
            AllLevel[129].FrequencyComming = 25;
            AllLevel[129].NbLemmingsToSave = 8;
            AllLevel[129].totalTime = 7;
            AllLevel[130].TotalLemmings = 18;
            AllLevel[130].InitPosX = 1060; // Init xscroll
            AllLevel[130].NameLev = "levels/sp/sp010";
            AllLevel[130].nameOfLevel = "Only SevenTeen";
            AllLevel[130].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[130].doorX = 1864;
            AllLevel[130].doorY = 280;
            AllLevel[130].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[130].exitX = 355;
            AllLevel[130].exitY = 292;
            AllLevel[130].numberClimbers = 1;
            AllLevel[130].numberUmbrellas = 1;
            AllLevel[130].numberExploders = 1;
            AllLevel[130].numberBlockers = 1;
            AllLevel[130].numberBuilders = 5;
            AllLevel[130].numberBashers = 3;
            AllLevel[130].numberMiners = 1;
            AllLevel[130].numberDiggers = 1;
            AllLevel[130].MinFrequencyComming = 25;
            AllLevel[130].FrequencyComming = 25;
            AllLevel[130].NbLemmingsToSave = 17;
            AllLevel[130].totalTime = 5;
            AllLevel[131].TotalLemmings = 10;
            AllLevel[131].InitPosX = 0; // Init xscroll
            AllLevel[131].NameLev = "levels/sp/sp011";
            AllLevel[131].nameOfLevel = "Under the line";
            AllLevel[131].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[131].doorX = 160;
            AllLevel[131].doorY = 45;
            AllLevel[131].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[131].exitX = 896;
            AllLevel[131].exitY = 489;
            AllLevel[131].numberClimbers = 0;
            AllLevel[131].numberUmbrellas = 0;
            AllLevel[131].numberExploders = 0;
            AllLevel[131].numberBlockers = 0;
            AllLevel[131].numberBuilders = 0;
            AllLevel[131].numberBashers = 2;
            AllLevel[131].numberMiners = 0;
            AllLevel[131].numberDiggers = 1;
            AllLevel[131].MinFrequencyComming = 30;
            AllLevel[131].FrequencyComming = 30;
            AllLevel[131].NbLemmingsToSave = 10;
            AllLevel[131].totalTime = 4;
            AllLevel[132].TotalLemmings = 2;
            AllLevel[132].InitPosX = 357; // Init xscroll
            AllLevel[132].NameLev = "levels/sp/sp012";
            AllLevel[132].nameOfLevel = "From A to B";
            AllLevel[132].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[132].doorX = 1021;
            AllLevel[132].doorY = 290;
            AllLevel[132].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[132].exitX = 2716;
            AllLevel[132].exitY = 377;
            AllLevel[132].numberClimbers = 10;
            AllLevel[132].numberUmbrellas = 10;
            AllLevel[132].numberExploders = 1;
            AllLevel[132].numberBlockers = 1;
            AllLevel[132].numberBuilders = 3;
            AllLevel[132].numberBashers = 3;
            AllLevel[132].numberMiners = 0;
            AllLevel[132].numberDiggers = 0;
            AllLevel[132].MinFrequencyComming = 5;
            AllLevel[132].FrequencyComming = 5;
            AllLevel[132].NbLemmingsToSave = 2;
            AllLevel[132].totalTime = 1;
            AllLevel[133].TotalLemmings = 25;
            AllLevel[133].InitPosX = 880; // Init xscroll
            AllLevel[133].NameLev = "levels/sp/sp013";
            AllLevel[133].nameOfLevel = "Chain Reaction";
            AllLevel[133].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[133].doorX = 1317;
            AllLevel[133].doorY = 161;
            AllLevel[133].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[133].exitX = 211;
            AllLevel[133].exitY = 331;
            AllLevel[133].numberClimbers = 0;
            AllLevel[133].numberUmbrellas = 0;
            AllLevel[133].numberExploders = 10;
            AllLevel[133].numberBlockers = 0;
            AllLevel[133].numberBuilders = 0;
            AllLevel[133].numberBashers = 0;
            AllLevel[133].numberMiners = 0;
            AllLevel[133].numberDiggers = 0;
            AllLevel[133].MinFrequencyComming = 1;
            AllLevel[133].FrequencyComming = 1;
            AllLevel[133].NbLemmingsToSave = 15;
            AllLevel[133].totalTime = 5;
            AllLevel[134].TotalLemmings = 8;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[134].InitPosX = 83; // Init xscroll
            AllLevel[134].NameLev = "levels/sp/sp014";
            AllLevel[134].nameOfLevel = "Tunnel Vision";
            AllLevel[134].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[134].doorX = 970;
            AllLevel[134].doorY = 381;
            AllLevel[134].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[134].exitX = 628;
            AllLevel[134].exitY = 324;
            AllLevel[134].numberClimbers = 8;
            AllLevel[134].numberUmbrellas = 0;
            AllLevel[134].numberExploders = 0;
            AllLevel[134].numberBlockers = 0;
            AllLevel[134].numberBuilders = 1;
            AllLevel[134].numberBashers = 2;
            AllLevel[134].numberMiners = 2;
            AllLevel[134].numberDiggers = 0;
            AllLevel[134].MinFrequencyComming = 60;
            AllLevel[134].FrequencyComming = 60;
            AllLevel[134].NbLemmingsToSave = 8;
            AllLevel[134].totalTime = 5;
            AllLevel[135].TotalLemmings = 2;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[135].InitPosX = 0; // Init xscroll
            AllLevel[135].NameLev = "levels/sp/sp015";
            AllLevel[135].nameOfLevel = "Two good friends";
            AllLevel[135].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[135].doorX = 27;
            AllLevel[135].doorY = 150;
            AllLevel[135].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[135].exitX = 1617;
            AllLevel[135].exitY = 400;
            AllLevel[135].numberClimbers = 1;
            AllLevel[135].numberUmbrellas = 0;
            AllLevel[135].numberExploders = 0;
            AllLevel[135].numberBlockers = 0;
            AllLevel[135].numberBuilders = 0;
            AllLevel[135].numberBashers = 2;
            AllLevel[135].numberMiners = 0;
            AllLevel[135].numberDiggers = 2;
            AllLevel[135].MinFrequencyComming = 1;
            AllLevel[135].FrequencyComming = 1;
            AllLevel[135].NbLemmingsToSave = 2;
            AllLevel[135].totalTime = 8;
            AllLevel[136].TotalLemmings = 5;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[136].InitPosX = 0; // Init xscroll
            AllLevel[136].NameLev = "levels/sp/sp016";
            AllLevel[136].nameOfLevel = "Only one work";
            AllLevel[136].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[136].doorX = 325;
            AllLevel[136].doorY = 104;
            AllLevel[136].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[136].exitX = 1398;
            AllLevel[136].exitY = 359;
            AllLevel[136].numberClimbers = 0;
            AllLevel[136].numberUmbrellas = 0;
            AllLevel[136].numberExploders = 0;
            AllLevel[136].numberBlockers = 0;
            AllLevel[136].numberBuilders = 2;
            AllLevel[136].numberBashers = 2;
            AllLevel[136].numberMiners = 0;
            AllLevel[136].numberDiggers = 1;
            AllLevel[136].MinFrequencyComming = 30;
            AllLevel[136].FrequencyComming = 30;
            AllLevel[136].NbLemmingsToSave = 5;
            AllLevel[136].totalTime = 5;
            AllLevel[137].TotalLemmings = 8;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[137].InitPosX = 300; // Init xscroll
            AllLevel[137].NameLev = "levels/sp/sp017";
            AllLevel[137].nameOfLevel = "Freedom is the Word";
            AllLevel[137].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[137].doorX = 1064;
            AllLevel[137].doorY = 63;
            AllLevel[137].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[137].exitX = 943;
            AllLevel[137].exitY = 462;
            AllLevel[137].numberClimbers = 0;
            AllLevel[137].numberUmbrellas = 8;
            AllLevel[137].numberExploders = 0;
            AllLevel[137].numberBlockers = 0;
            AllLevel[137].numberBuilders = 3;
            AllLevel[137].numberBashers = 0;
            AllLevel[137].numberMiners = 1;
            AllLevel[137].numberDiggers = 1;
            AllLevel[137].MinFrequencyComming = 1;
            AllLevel[137].FrequencyComming = 1;
            AllLevel[137].NbLemmingsToSave = 6;
            AllLevel[137].totalTime = 2;
            AllLevel[138].TotalLemmings = 2;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[138].InitPosX = 0; // Init xscroll
            AllLevel[138].NameLev = "levels/sp/sp018";
            AllLevel[138].nameOfLevel = "Return to play";
            AllLevel[138].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[138].doorX = 383;
            AllLevel[138].doorY = 237;
            AllLevel[138].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[138].exitX = 1242;
            AllLevel[138].exitY = 415;
            AllLevel[138].numberClimbers = 1;
            AllLevel[138].numberUmbrellas = 0;
            AllLevel[138].numberExploders = 0;
            AllLevel[138].numberBlockers = 0;
            AllLevel[138].numberBuilders = 4;
            AllLevel[138].numberBashers = 3;
            AllLevel[138].numberMiners = 0;
            AllLevel[138].numberDiggers = 0;
            AllLevel[138].MinFrequencyComming = 1;
            AllLevel[138].FrequencyComming = 1;
            AllLevel[138].NbLemmingsToSave = 2;
            AllLevel[138].totalTime = 5;
            AllLevel[139].TotalLemmings = 20;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[139].InitPosX = 1840; // Init xscroll
            AllLevel[139].NameLev = "levels/sp/sp019";
            AllLevel[139].nameOfLevel = "3 feets to Heaven";
            AllLevel[139].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[139].doorX = 2478;
            AllLevel[139].doorY = 218;
            AllLevel[139].TypeOfExit = 7; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[139].exitX = 1101;
            AllLevel[139].exitY = 278;
            AllLevel[139].numberClimbers = 5;
            AllLevel[139].numberUmbrellas = 5;
            AllLevel[139].numberExploders = 0;
            AllLevel[139].numberBlockers = 0;
            AllLevel[139].numberBuilders = 15;
            AllLevel[139].numberBashers = 5;
            AllLevel[139].numberMiners = 5;
            AllLevel[139].numberDiggers = 5;
            AllLevel[139].MinFrequencyComming = 20;
            AllLevel[139].FrequencyComming = 20;
            AllLevel[139].NbLemmingsToSave = 20;
            AllLevel[139].totalTime = 5;
            AllLevel[140].TotalLemmings = 1;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[140].InitPosX = 0; // Init xscroll
            AllLevel[140].NameLev = "levels/sp/sp020";
            AllLevel[140].nameOfLevel = "Toast Lemming";
            AllLevel[140].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[140].doorX = 280;
            AllLevel[140].doorY = 28;
            AllLevel[140].TypeOfExit = 5; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[140].exitX = 237;
            AllLevel[140].exitY = 437;
            AllLevel[140].numberClimbers = 1;
            AllLevel[140].numberUmbrellas = 0;
            AllLevel[140].numberExploders = 0;
            AllLevel[140].numberBlockers = 0;
            AllLevel[140].numberBuilders = 3;
            AllLevel[140].numberBashers = 1;
            AllLevel[140].numberMiners = 1;
            AllLevel[140].numberDiggers = 1;
            AllLevel[140].MinFrequencyComming = 1;
            AllLevel[140].FrequencyComming = 1;
            AllLevel[140].NbLemmingsToSave = 1;
            AllLevel[140].totalTime = 4;
            AllLevel[141].TotalLemmings = 12;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[141].InitPosX = 133; // Init xscroll
            AllLevel[141].NameLev = "levels/sp/sp021";
            AllLevel[141].nameOfLevel = "Seeing double";
            AllLevel[141].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[141].doorX = 358;
            AllLevel[141].doorY = 32;
            AllLevel[141].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[141].exitX = 683;
            AllLevel[141].exitY = 449;
            AllLevel[141].numberClimbers = 5;
            AllLevel[141].numberUmbrellas = 0;
            AllLevel[141].numberExploders = 1;
            AllLevel[141].numberBlockers = 2;
            AllLevel[141].numberBuilders = 2;
            AllLevel[141].numberBashers = 4;
            AllLevel[141].numberMiners = 0;
            AllLevel[141].numberDiggers = 0;
            AllLevel[141].MinFrequencyComming = 10;
            AllLevel[141].FrequencyComming = 10;
            AllLevel[141].NbLemmingsToSave = 8;
            AllLevel[141].totalTime = 5;
            AllLevel[142].TotalLemmings = 10;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[142].InitPosX = 0; // Init xscroll
            AllLevel[142].NameLev = "levels/sp/sp022";
            AllLevel[142].nameOfLevel = "The WormHole";
            AllLevel[142].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[142].doorX = 359;
            AllLevel[142].doorY = 358;
            AllLevel[142].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[142].exitX = 303;
            AllLevel[142].exitY = 143;
            AllLevel[142].numberClimbers = 0;
            AllLevel[142].numberUmbrellas = 0;
            AllLevel[142].numberExploders = 0;
            AllLevel[142].numberBlockers = 0;
            AllLevel[142].numberBuilders = 15;
            AllLevel[142].numberBashers = 0;
            AllLevel[142].numberMiners = 15;
            AllLevel[142].numberDiggers = 0;
            AllLevel[142].MinFrequencyComming = 10;
            AllLevel[142].FrequencyComming = 10;
            AllLevel[142].NbLemmingsToSave = 5;
            AllLevel[142].totalTime = 5;
            AllLevel[143].TotalLemmings = 100;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[143].InitPosX = 0; // Init xscroll
            AllLevel[143].NameLev = "levels/sp/sp023";
            AllLevel[143].nameOfLevel = "Waterflow";
            AllLevel[143].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[143].doorX = 2;
            AllLevel[143].doorY = 95;
            AllLevel[143].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[143].exitX = 1864;
            AllLevel[143].exitY = 264;
            AllLevel[143].numberClimbers = 1;
            AllLevel[143].numberUmbrellas = 0;
            AllLevel[143].numberExploders = 0;
            AllLevel[143].numberBlockers = 0;
            AllLevel[143].numberBuilders = 10;
            AllLevel[143].numberBashers = 5;
            AllLevel[143].numberMiners = 1;
            AllLevel[143].numberDiggers = 0;
            AllLevel[143].MinFrequencyComming = 1;
            AllLevel[143].FrequencyComming = 1;
            AllLevel[143].NbLemmingsToSave = 100;
            AllLevel[143].totalTime = 6;
            AllLevel[144].TotalLemmings = 2;  // TWO 222222 DOOOOOOOOOOOOOOOORRRRRSSSS
            AllLevel[144].InitPosX = 0; // Init xscroll
            AllLevel[144].NameLev = "levels/sp/sp024";
            AllLevel[144].nameOfLevel = "...with a little help from my Lemmings";
            AllLevel[144].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[144].doorX = 464;
            AllLevel[144].doorY = 3;
            AllLevel[144].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[144].exitX = 804;
            AllLevel[144].exitY = 463;
            AllLevel[144].numberClimbers = 2;
            AllLevel[144].numberUmbrellas = 0;
            AllLevel[144].numberExploders = 0;
            AllLevel[144].numberBlockers = 0;
            AllLevel[144].numberBuilders = 0;
            AllLevel[144].numberBashers = 3;
            AllLevel[144].numberMiners = 0;
            AllLevel[144].numberDiggers = 2;
            AllLevel[144].MinFrequencyComming = 80;
            AllLevel[144].FrequencyComming = 80;
            AllLevel[144].NbLemmingsToSave = 2;
            AllLevel[144].totalTime = 5;
            AllLevel[145].TotalLemmings = 20;
            AllLevel[145].InitPosX = 0; // Init xscroll
            AllLevel[145].NameLev = "levels/sp/sp025";
            AllLevel[145].nameOfLevel = "Not so easy as it looks";
            AllLevel[145].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[145].doorX = 464;
            AllLevel[145].doorY = 3;
            AllLevel[145].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[145].exitX = 538;
            AllLevel[145].exitY = 487;
            AllLevel[145].numberClimbers = 0;
            AllLevel[145].numberUmbrellas = 0;
            AllLevel[145].numberExploders = 0;
            AllLevel[145].numberBlockers = 1;
            AllLevel[145].numberBuilders = 0;
            AllLevel[145].numberBashers = 0;
            AllLevel[145].numberMiners = 0;
            AllLevel[145].numberDiggers = 3;
            AllLevel[145].MinFrequencyComming = 99;
            AllLevel[145].FrequencyComming = 99;
            AllLevel[145].NbLemmingsToSave = 19;
            AllLevel[145].totalTime = 5;
            AllLevel[146].TotalLemmings = 20;
            AllLevel[146].InitPosX = 1000; // Init xscroll
            AllLevel[146].NameLev = "levels/sp/sp026";
            AllLevel[146].nameOfLevel = "On the Web";
            AllLevel[146].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[146].doorX = 1302;
            AllLevel[146].doorY = 202;
            AllLevel[146].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[146].exitX = 2070;
            AllLevel[146].exitY = 275;
            AllLevel[146].numberClimbers = 2;
            AllLevel[146].numberUmbrellas = 2;
            AllLevel[146].numberExploders = 1;
            AllLevel[146].numberBlockers = 1;
            AllLevel[146].numberBuilders = 2;
            AllLevel[146].numberBashers = 2;
            AllLevel[146].numberMiners = 0;
            AllLevel[146].numberDiggers = 0;
            AllLevel[146].MinFrequencyComming = 50;
            AllLevel[146].FrequencyComming = 50;
            AllLevel[146].NbLemmingsToSave = 19;
            AllLevel[146].totalTime = 2;
            AllLevel[147].TotalLemmings = 20;
            AllLevel[147].InitPosX = 0; // Init xscroll
            AllLevel[147].NameLev = "levels/sp/sp027";
            AllLevel[147].nameOfLevel = "Erroneous points of view";
            AllLevel[147].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[147].doorX = 141;
            AllLevel[147].doorY = 13;
            AllLevel[147].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[147].exitX = 2337;
            AllLevel[147].exitY = 259;
            AllLevel[147].numberClimbers = 5;
            AllLevel[147].numberUmbrellas = 20;
            AllLevel[147].numberExploders = 5;
            AllLevel[147].numberBlockers = 0;
            AllLevel[147].numberBuilders = 8;
            AllLevel[147].numberBashers = 5;
            AllLevel[147].numberMiners = 0;
            AllLevel[147].numberDiggers = 0;
            AllLevel[147].MinFrequencyComming = 50;
            AllLevel[147].FrequencyComming = 50;
            AllLevel[147].NbLemmingsToSave = 15;
            AllLevel[147].totalTime = 5;
            AllLevel[148].TotalLemmings = 3;
            AllLevel[148].InitPosX = 142; // Init xscroll
            AllLevel[148].NameLev = "levels/sp/sp028";
            AllLevel[148].nameOfLevel = "Fly Lemmings, fly";
            AllLevel[148].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[148].doorX = 671;
            AllLevel[148].doorY = 145;
            AllLevel[148].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[148].exitX = 1287;
            AllLevel[148].exitY = 375;
            AllLevel[148].numberClimbers = 5;
            AllLevel[148].numberUmbrellas = 7;
            AllLevel[148].numberExploders = 2;
            AllLevel[148].numberBlockers = 2;
            AllLevel[148].numberBuilders = 5;
            AllLevel[148].numberBashers = 0;
            AllLevel[148].numberMiners = 1;
            AllLevel[148].numberDiggers = 0;
            AllLevel[148].MinFrequencyComming = 1;
            AllLevel[148].FrequencyComming = 1;
            AllLevel[148].NbLemmingsToSave = 1;
            AllLevel[148].totalTime = 4;
            AllLevel[149].TotalLemmings = 10;
            AllLevel[149].InitPosX = 580; // Init xscroll
            AllLevel[149].NameLev = "levels/sp/sp029";
            AllLevel[149].nameOfLevel = "Bridge over Lemming river";
            AllLevel[149].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[149].doorX = 1313;
            AllLevel[149].doorY = 3;
            AllLevel[149].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[149].exitX = 277;
            AllLevel[149].exitY = 478;
            AllLevel[149].numberClimbers = 1;
            AllLevel[149].numberUmbrellas = 1;
            AllLevel[149].numberExploders = 0;
            AllLevel[149].numberBlockers = 0;
            AllLevel[149].numberBuilders = 7;
            AllLevel[149].numberBashers = 3;
            AllLevel[149].numberMiners = 1;
            AllLevel[149].numberDiggers = 2;
            AllLevel[149].MinFrequencyComming = 50;
            AllLevel[149].FrequencyComming = 50;
            AllLevel[149].NbLemmingsToSave = 9;
            AllLevel[149].totalTime = 5;
            AllLevel[150].TotalLemmings = 25;
            AllLevel[150].InitPosX = 177; // Init xscroll
            AllLevel[150].NameLev = "levels/sp/sp030";
            AllLevel[150].nameOfLevel = "Stairs without floor";
            AllLevel[150].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[150].doorX = 216;
            AllLevel[150].doorY = 7;
            AllLevel[150].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[150].exitX = 364;
            AllLevel[150].exitY = 491;
            AllLevel[150].numberClimbers = 0;
            AllLevel[150].numberUmbrellas = 1;
            AllLevel[150].numberExploders = 1;
            AllLevel[150].numberBlockers = 1;
            AllLevel[150].numberBuilders = 7;
            AllLevel[150].numberBashers = 3;
            AllLevel[150].numberMiners = 2;
            AllLevel[150].numberDiggers = 1;
            AllLevel[150].MinFrequencyComming = 1;
            AllLevel[150].FrequencyComming = 1;
            AllLevel[150].NbLemmingsToSave = 24;
            AllLevel[150].totalTime = 4;
            AllLevel[151].TotalLemmings = 30;
            AllLevel[151].InitPosX = 142; // Init xscroll
            AllLevel[151].NameLev = "levels/sp/sp031";
            AllLevel[151].nameOfLevel = "Yet and Now";
            AllLevel[151].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[151].doorX = 295;
            AllLevel[151].doorY = 5;
            AllLevel[151].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[151].exitX = 345;
            AllLevel[151].exitY = 228;
            AllLevel[151].numberClimbers = 0;
            AllLevel[151].numberUmbrellas = 13;
            AllLevel[151].numberExploders = 0;
            AllLevel[151].numberBlockers = 0;
            AllLevel[151].numberBuilders = 20;
            AllLevel[151].numberBashers = 1;
            AllLevel[151].numberMiners = 0;
            AllLevel[151].numberDiggers = 0;
            AllLevel[151].MinFrequencyComming = 1;
            AllLevel[151].FrequencyComming = 1;
            AllLevel[151].NbLemmingsToSave = 15;
            AllLevel[151].totalTime = 5;
            AllLevel[152].TotalLemmings = 25;
            AllLevel[152].InitPosX = 0; // Init xscroll
            AllLevel[152].NameLev = "levels/sp/sp032";
            AllLevel[152].nameOfLevel = "Get together and dispers";
            AllLevel[152].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[152].doorX = 240;
            AllLevel[152].doorY = 2;
            AllLevel[152].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[152].exitX = 736;
            AllLevel[152].exitY = 88;
            AllLevel[152].numberClimbers = 0;
            AllLevel[152].numberUmbrellas = 0;
            AllLevel[152].numberExploders = 4;
            AllLevel[152].numberBlockers = 4;
            AllLevel[152].numberBuilders = 25;
            AllLevel[152].numberBashers = 2;
            AllLevel[152].numberMiners = 0;
            AllLevel[152].numberDiggers = 0;
            AllLevel[152].MinFrequencyComming = 50;
            AllLevel[152].FrequencyComming = 50;
            AllLevel[152].NbLemmingsToSave = 20;
            AllLevel[152].totalTime = 7;
            AllLevel[153].TotalLemmings = 11;
            AllLevel[153].InitPosX = 0; // Init xscroll
            AllLevel[153].NameLev = "levels/sp/sp033";
            AllLevel[153].nameOfLevel = "Reservoir dogs";
            AllLevel[153].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[153].doorX = 30;
            AllLevel[153].doorY = 44;
            AllLevel[153].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[153].exitX = 890;
            AllLevel[153].exitY = 485;
            AllLevel[153].numberClimbers = 0;
            AllLevel[153].numberUmbrellas = 1;
            AllLevel[153].numberExploders = 0;
            AllLevel[153].numberBlockers = 0;
            AllLevel[153].numberBuilders = 7; //need two more builders for the solution by the height of the stairs (dos was 5)
            AllLevel[153].numberBashers = 2;
            AllLevel[153].numberMiners = 0;
            AllLevel[153].numberDiggers = 0;
            AllLevel[153].MinFrequencyComming = 70;
            AllLevel[153].FrequencyComming = 70;
            AllLevel[153].NbLemmingsToSave = 11;
            AllLevel[153].totalTime = 8;
            AllLevel[154].TotalLemmings = 10;
            AllLevel[154].InitPosX = 0; // Init xscroll
            AllLevel[154].NameLev = "levels/sp/sp034";
            AllLevel[154].nameOfLevel = "I was born blocker and i'll die this";
            AllLevel[154].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[154].doorX = 30;
            AllLevel[154].doorY = 44;
            AllLevel[154].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[154].exitX = 1975;
            AllLevel[154].exitY = 461;
            AllLevel[154].numberClimbers = 10;
            AllLevel[154].numberUmbrellas = 0;
            AllLevel[154].numberExploders = 0;
            AllLevel[154].numberBlockers = 9;
            AllLevel[154].numberBuilders = 0;
            AllLevel[154].numberBashers = 0;
            AllLevel[154].numberMiners = 0;
            AllLevel[154].numberDiggers = 0;
            AllLevel[154].MinFrequencyComming = 95;
            AllLevel[154].FrequencyComming = 95;
            AllLevel[154].NbLemmingsToSave = 1;
            AllLevel[154].totalTime = 8;
            AllLevel[155].TotalLemmings = 20;
            AllLevel[155].InitPosX = 400; // Init xscroll
            AllLevel[155].NameLev = "levels/sp/sp035";
            AllLevel[155].nameOfLevel = "No justice for Hero";
            AllLevel[155].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[155].doorX = 995;
            AllLevel[155].doorY = 6;
            AllLevel[155].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[155].exitX = 795;
            AllLevel[155].exitY = 475;
            AllLevel[155].numberClimbers = 0;
            AllLevel[155].numberUmbrellas = 0;
            AllLevel[155].numberExploders = 0;
            AllLevel[155].numberBlockers = 0;
            AllLevel[155].numberBuilders = 1;
            AllLevel[155].numberBashers = 0;
            AllLevel[155].numberMiners = 1;
            AllLevel[155].numberDiggers = 1;
            AllLevel[155].MinFrequencyComming = 95;
            AllLevel[155].FrequencyComming = 95;
            AllLevel[155].NbLemmingsToSave = 19;
            AllLevel[155].totalTime = 8;
            AllLevel[156].TotalLemmings = 20;
            AllLevel[156].InitPosX = 0; // Init xscroll
            AllLevel[156].NameLev = "levels/sp/sp036";
            AllLevel[156].nameOfLevel = "Is not what you think";
            AllLevel[156].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[156].doorX = 140;
            AllLevel[156].doorY = 44;
            AllLevel[156].TypeOfExit = 2; // 1-egypt 2-greek 3-hell 4-rock 7-crystal 5-rock2 6-greek2
            AllLevel[156].exitX = 1198;
            AllLevel[156].exitY = 393;
            AllLevel[156].numberClimbers = 0;
            AllLevel[156].numberUmbrellas = 1;
            AllLevel[156].numberExploders = 1;
            AllLevel[156].numberBlockers = 0;
            AllLevel[156].numberBuilders = 1;
            AllLevel[156].numberBashers = 0;
            AllLevel[156].numberMiners = 0;
            AllLevel[156].numberDiggers = 1;
            AllLevel[156].MinFrequencyComming = 1;
            AllLevel[156].FrequencyComming = 1;
            AllLevel[156].NbLemmingsToSave = 19;
            AllLevel[156].totalTime = 4;
            // USER LEVELS 157-------eTc...
            AllLevel[157].TotalLemmings = 80;  //two exits special one level SEE SEE SEE
            AllLevel[157].InitPosX = 0; // Init xscroll
            AllLevel[157].NameLev = "levels/user/user001";
            AllLevel[157].nameOfLevel = "The Apple Computer Level";
            AllLevel[157].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[157].doorX = 36;
            AllLevel[157].doorY = 290;
            AllLevel[157].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[157].exitX = 2053;
            AllLevel[157].exitY = 420;
            AllLevel[157].numberClimbers = 21;
            AllLevel[157].numberUmbrellas = 21;
            AllLevel[157].numberExploders = 21;
            AllLevel[157].numberBlockers = 21;
            AllLevel[157].numberBuilders = 21;
            AllLevel[157].numberBashers = 21;
            AllLevel[157].numberMiners = 21;
            AllLevel[157].numberDiggers = 21;
            AllLevel[157].MinFrequencyComming = 50;
            AllLevel[157].FrequencyComming = 50;
            AllLevel[157].NbLemmingsToSave = 40; // 50% of 80 = 40 (??*??/100)
            AllLevel[157].totalTime = 9;
            AllLevel[158].TotalLemmings = 30;  //two exits special one level SEE SEE SEE
            AllLevel[158].InitPosX = 1000; // Init xscroll
            AllLevel[158].NameLev = "levels/user/user002";
            AllLevel[158].nameOfLevel = "Dragon Ball Level";
            AllLevel[158].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt
            AllLevel[158].doorX = 1070;
            AllLevel[158].doorY = 10;
            AllLevel[158].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[158].exitX = 79;
            AllLevel[158].exitY = 437;
            AllLevel[158].numberClimbers = 0;
            AllLevel[158].numberUmbrellas = 0;
            AllLevel[158].numberExploders = 2;
            AllLevel[158].numberBlockers = 2;
            AllLevel[158].numberBuilders = 15;
            AllLevel[158].numberBashers = 0;
            AllLevel[158].numberMiners = 3;
            AllLevel[158].numberDiggers = 0;
            AllLevel[158].MinFrequencyComming = 1;
            AllLevel[158].FrequencyComming = 1;
            AllLevel[158].NbLemmingsToSave = 20; // ??% of ?? = ?? (??*??/100)
            AllLevel[158].totalTime = 4;
            AllLevel[159].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[159].InitPosX = 0; // Init xscroll
            AllLevel[159].NameLev = "levels/user/user003";
            AllLevel[159].nameOfLevel = "Lemmings 2 Beach level 1";
            AllLevel[159].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[159].doorX = 136;
            AllLevel[159].doorY = 14;
            AllLevel[159].TypeOfExit = 10; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[159].exitX = 539;
            AllLevel[159].exitY = 583;
            AllLevel[159].numberClimbers = 99;
            AllLevel[159].numberUmbrellas = 99;
            AllLevel[159].numberExploders = 99;
            AllLevel[159].numberBlockers = 99;
            AllLevel[159].numberBuilders = 99;
            AllLevel[159].numberBashers = 99;
            AllLevel[159].numberMiners = 99;
            AllLevel[159].numberDiggers = 99;
            AllLevel[159].MinFrequencyComming = 1;
            AllLevel[159].FrequencyComming = 1;
            AllLevel[159].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[159].totalTime = 11;
            AllLevel[160].TotalLemmings = 100;  //two exits special one level SEE SEE SEE
            AllLevel[160].InitPosX = 0; // Init xscroll
            AllLevel[160].NameLev = "levels/user/user004";
            AllLevel[160].nameOfLevel = "Dream Team";
            AllLevel[160].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[160].doorX = 136;
            AllLevel[160].doorY = 154;
            AllLevel[160].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[160].exitX = 976;
            AllLevel[160].exitY = 1036;
            AllLevel[160].numberClimbers = 0;
            AllLevel[160].numberUmbrellas = 1;
            AllLevel[160].numberExploders = 30;
            AllLevel[160].numberBlockers = 30;
            AllLevel[160].numberBuilders = 5;
            AllLevel[160].numberBashers = 0;
            AllLevel[160].numberMiners = 0;
            AllLevel[160].numberDiggers = 1;
            AllLevel[160].MinFrequencyComming = 1;
            AllLevel[160].FrequencyComming = 1;
            AllLevel[160].NbLemmingsToSave = 50; // ??% of ?? = ?? (??*??/100)
            AllLevel[160].totalTime = 8;
            AllLevel[161].TotalLemmings = 100;
            AllLevel[161].InitPosX = 0; // Init xscroll
            AllLevel[161].NameLev = "levels/user/user005";
            AllLevel[161].nameOfLevel = "Lemmings 2 Beach level 2";
            AllLevel[161].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[161].doorX = 200;
            AllLevel[161].doorY = 14;
            AllLevel[161].TypeOfExit = 10; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[161].exitX = 100;
            AllLevel[161].exitY = 559;
            AllLevel[161].numberClimbers = 99;
            AllLevel[161].numberUmbrellas = 99;
            AllLevel[161].numberExploders = 99;
            AllLevel[161].numberBlockers = 99;
            AllLevel[161].numberBuilders = 99;
            AllLevel[161].numberBashers = 99;
            AllLevel[161].numberMiners = 99;
            AllLevel[161].numberDiggers = 99;
            AllLevel[161].MinFrequencyComming = 1;
            AllLevel[161].FrequencyComming = 1;
            AllLevel[161].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[161].totalTime = 11;
            AllLevel[162].TotalLemmings = 100;
            AllLevel[162].InitPosX = 0; // Init xscroll
            AllLevel[162].NameLev = "levels/user/user006";
            AllLevel[162].nameOfLevel = "Lemmings 2 Egypt Level 1";
            AllLevel[162].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[162].doorX = 158;
            AllLevel[162].doorY = 99;
            AllLevel[162].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[162].exitX = 633;
            AllLevel[162].exitY = 267;
            AllLevel[162].numberClimbers = 99;
            AllLevel[162].numberUmbrellas = 99;
            AllLevel[162].numberExploders = 99;
            AllLevel[162].numberBlockers = 99;
            AllLevel[162].numberBuilders = 99;
            AllLevel[162].numberBashers = 99;
            AllLevel[162].numberMiners = 99;
            AllLevel[162].numberDiggers = 99;
            AllLevel[162].MinFrequencyComming = 1;
            AllLevel[162].FrequencyComming = 1;
            AllLevel[162].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[162].totalTime = 11;
            AllLevel[163].TotalLemmings = 100;
            AllLevel[163].InitPosX = 0; // Init xscroll
            AllLevel[163].NameLev = "levels/user/user007";
            AllLevel[163].nameOfLevel = "Lemmings 2 Egypt Level 2";
            AllLevel[163].TypeOfDoor = 1; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[163].doorX = 54;
            AllLevel[163].doorY = 39;
            AllLevel[163].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[163].exitX = 1295;
            AllLevel[163].exitY = 160;
            AllLevel[163].numberClimbers = 99;
            AllLevel[163].numberUmbrellas = 99;
            AllLevel[163].numberExploders = 99;
            AllLevel[163].numberBlockers = 99;
            AllLevel[163].numberBuilders = 99;
            AllLevel[163].numberBashers = 99;
            AllLevel[163].numberMiners = 99;
            AllLevel[163].numberDiggers = 99;
            AllLevel[163].MinFrequencyComming = 1;
            AllLevel[163].FrequencyComming = 1;
            AllLevel[163].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[163].totalTime = 11;
            AllLevel[164].TotalLemmings = 100;
            AllLevel[164].InitPosX = 0; // Init xscroll
            AllLevel[164].NameLev = "levels/user/user008";
            AllLevel[164].nameOfLevel = "Lemmings 2 Classic Level 1";
            AllLevel[164].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[164].doorX = 260;
            AllLevel[164].doorY = 50;
            AllLevel[164].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[164].exitX = 875;
            AllLevel[164].exitY = 349;
            AllLevel[164].numberClimbers = 99;
            AllLevel[164].numberUmbrellas = 99;
            AllLevel[164].numberExploders = 99;
            AllLevel[164].numberBlockers = 99;
            AllLevel[164].numberBuilders = 99;
            AllLevel[164].numberBashers = 99;
            AllLevel[164].numberMiners = 99;
            AllLevel[164].numberDiggers = 99;
            AllLevel[164].MinFrequencyComming = 1;
            AllLevel[164].FrequencyComming = 1;
            AllLevel[164].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[164].totalTime = 11;
            AllLevel[165].TotalLemmings = 100;
            AllLevel[165].InitPosX = 0; // Init xscroll
            AllLevel[165].NameLev = "levels/user/user009";
            AllLevel[165].nameOfLevel = "Lemmings 2 Classic Level 2";
            AllLevel[165].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[165].doorX = 200;
            AllLevel[165].doorY = 260;
            AllLevel[165].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[165].exitX = 875;
            AllLevel[165].exitY = 349;
            AllLevel[165].numberClimbers = 99;
            AllLevel[165].numberUmbrellas = 99;
            AllLevel[165].numberExploders = 99;
            AllLevel[165].numberBlockers = 99;
            AllLevel[165].numberBuilders = 99;
            AllLevel[165].numberBashers = 99;
            AllLevel[165].numberMiners = 99;
            AllLevel[165].numberDiggers = 99;
            AllLevel[165].MinFrequencyComming = 1;
            AllLevel[165].FrequencyComming = 1;
            AllLevel[165].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[165].totalTime = 11;
            AllLevel[166].TotalLemmings = 100;
            AllLevel[166].InitPosX = 0; // Init xscroll
            AllLevel[166].NameLev = "levels/user/user010";
            AllLevel[166].nameOfLevel = "Lemmings 2 Classic Level 3";
            AllLevel[166].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[166].doorX = 136;
            AllLevel[166].doorY = 194;
            AllLevel[166].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[166].exitX = 1693;
            AllLevel[166].exitY = 320;
            AllLevel[166].numberClimbers = 99;
            AllLevel[166].numberUmbrellas = 99;
            AllLevel[166].numberExploders = 99;
            AllLevel[166].numberBlockers = 99;
            AllLevel[166].numberBuilders = 99;
            AllLevel[166].numberBashers = 99;
            AllLevel[166].numberMiners = 99;
            AllLevel[166].numberDiggers = 99;
            AllLevel[166].MinFrequencyComming = 1;
            AllLevel[166].FrequencyComming = 1;
            AllLevel[166].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[166].totalTime = 11;
            AllLevel[167].TotalLemmings = 100;
            AllLevel[167].InitPosX = 0; // Init xscroll
            AllLevel[167].NameLev = "levels/user/user011";
            AllLevel[167].nameOfLevel = "Lemmings 2 Classic Level 4";
            AllLevel[167].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[167].doorX = 136;
            AllLevel[167].doorY = 92;
            AllLevel[167].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[167].exitX = 1520;
            AllLevel[167].exitY = 322;
            AllLevel[167].numberClimbers = 99;
            AllLevel[167].numberUmbrellas = 99;
            AllLevel[167].numberExploders = 99;
            AllLevel[167].numberBlockers = 99;
            AllLevel[167].numberBuilders = 99;
            AllLevel[167].numberBashers = 99;
            AllLevel[167].numberMiners = 99;
            AllLevel[167].numberDiggers = 99;
            AllLevel[167].MinFrequencyComming = 1;
            AllLevel[167].FrequencyComming = 1;
            AllLevel[167].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[167].totalTime = 11;
            AllLevel[168].TotalLemmings = 100;
            AllLevel[168].InitPosX = 0; // Init xscroll
            AllLevel[168].NameLev = "levels/user/user012";
            AllLevel[168].nameOfLevel = "Lemmings 2 Classic Level 5";
            AllLevel[168].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[168].doorX = 130;
            AllLevel[168].doorY = 28;
            AllLevel[168].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[168].exitX = 2233;
            AllLevel[168].exitY = 509;
            AllLevel[168].numberClimbers = 99;
            AllLevel[168].numberUmbrellas = 99;
            AllLevel[168].numberExploders = 99;
            AllLevel[168].numberBlockers = 99;
            AllLevel[168].numberBuilders = 99;
            AllLevel[168].numberBashers = 99;
            AllLevel[168].numberMiners = 99;
            AllLevel[168].numberDiggers = 99;
            AllLevel[168].MinFrequencyComming = 1;
            AllLevel[168].FrequencyComming = 1;
            AllLevel[168].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[168].totalTime = 11;
            AllLevel[169].TotalLemmings = 100;
            AllLevel[169].InitPosX = 0; // Init xscroll
            AllLevel[169].NameLev = "levels/user/user013";
            AllLevel[169].nameOfLevel = "Lemmings 2 Classic Level 6";
            AllLevel[169].TypeOfDoor = 3; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[169].doorX = 527;
            AllLevel[169].doorY = 76;
            AllLevel[169].TypeOfExit = 1; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[169].exitX = 2785;
            AllLevel[169].exitY = 483;
            AllLevel[169].numberClimbers = 99;
            AllLevel[169].numberUmbrellas = 99;
            AllLevel[169].numberExploders = 99;
            AllLevel[169].numberBlockers = 99;
            AllLevel[169].numberBuilders = 99;
            AllLevel[169].numberBashers = 99;
            AllLevel[169].numberMiners = 99;
            AllLevel[169].numberDiggers = 99;
            AllLevel[169].MinFrequencyComming = 1;
            AllLevel[169].FrequencyComming = 1;
            AllLevel[169].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[169].totalTime = 11;
            AllLevel[170].TotalLemmings = 100;
            AllLevel[170].InitPosX = 0; // Init xscroll
            AllLevel[170].NameLev = "levels/user/user014";
            AllLevel[170].nameOfLevel = "Lemmings 2 Beach Level 3";
            AllLevel[170].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[170].doorX = 47;
            AllLevel[170].doorY = 56;
            AllLevel[170].TypeOfExit = 10; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[170].exitX = 54;
            AllLevel[170].exitY = 380;
            AllLevel[170].numberClimbers = 99;
            AllLevel[170].numberUmbrellas = 99;
            AllLevel[170].numberExploders = 99;
            AllLevel[170].numberBlockers = 99;
            AllLevel[170].numberBuilders = 99;
            AllLevel[170].numberBashers = 99;
            AllLevel[170].numberMiners = 99;
            AllLevel[170].numberDiggers = 99;
            AllLevel[170].MinFrequencyComming = 1;
            AllLevel[170].FrequencyComming = 1;
            AllLevel[170].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[170].totalTime = 11;
            AllLevel[171].TotalLemmings = 256;
            AllLevel[171].InitPosX = 0; // Init xscroll
            AllLevel[171].NameLev = "levels/user/user015";
            AllLevel[171].nameOfLevel = "Lemmings miracles!!!";
            AllLevel[171].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[171].doorX = 77;
            AllLevel[171].doorY = 10;
            AllLevel[171].TypeOfExit = 10; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[171].exitX = 135;
            AllLevel[171].exitY = 759;
            AllLevel[171].numberClimbers = 99;
            AllLevel[171].numberUmbrellas = 99;
            AllLevel[171].numberExploders = 99;
            AllLevel[171].numberBlockers = 99;
            AllLevel[171].numberBuilders = 99;
            AllLevel[171].numberBashers = 99;
            AllLevel[171].numberMiners = 99;
            AllLevel[171].numberDiggers = 99;
            AllLevel[171].MinFrequencyComming = 1;
            AllLevel[171].FrequencyComming = 1;
            AllLevel[171].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[171].totalTime = 11;
            AllLevel[172].TotalLemmings = 25;
            AllLevel[172].InitPosX = 0; // Init xscroll
            AllLevel[172].NameLev = "levels/user/user016";
            AllLevel[172].nameOfLevel = "Is this a Vita Level???";
            AllLevel[172].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[172].doorX = 290 + 16;
            AllLevel[172].doorY = 10 + 16;
            AllLevel[172].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[172].exitX = 876 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[172].exitY = 276 + 16;
            AllLevel[172].numberClimbers = 45;
            AllLevel[172].numberUmbrellas = 50;
            AllLevel[172].numberExploders = 10;
            AllLevel[172].numberBlockers = 10;
            AllLevel[172].numberBuilders = 30;
            AllLevel[172].numberBashers = 20;
            AllLevel[172].numberMiners = 20;
            AllLevel[172].numberDiggers = 30;
            AllLevel[172].MinFrequencyComming = 1;
            AllLevel[172].FrequencyComming = 1;
            AllLevel[172].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[172].totalTime = 11;
            AllLevel[173].TotalLemmings = 25;
            AllLevel[173].InitPosX = 0; // Init xscroll
            AllLevel[173].NameLev = "levels/user/user017";
            AllLevel[173].nameOfLevel = "Is this a error???";
            AllLevel[173].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[173].doorX = 455 + 16;
            AllLevel[173].doorY = 12 + 16;
            AllLevel[173].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[173].exitX = 2802 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[173].exitY = 127 + 16;
            AllLevel[173].numberClimbers = 45;
            AllLevel[173].numberUmbrellas = 50;
            AllLevel[173].numberExploders = 10;
            AllLevel[173].numberBlockers = 10;
            AllLevel[173].numberBuilders = 30;
            AllLevel[173].numberBashers = 20;
            AllLevel[173].numberMiners = 20;
            AllLevel[173].numberDiggers = 30;
            AllLevel[173].MinFrequencyComming = 1;
            AllLevel[173].FrequencyComming = 1;
            AllLevel[173].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[173].totalTime = 11;
            AllLevel[174].TotalLemmings = 25;
            AllLevel[174].InitPosX = 0; // Init xscroll
            AllLevel[174].NameLev = "levels/user/user018";
            AllLevel[174].nameOfLevel = "This is not Vita!!!";
            AllLevel[174].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[174].doorX = 660 + 16;
            AllLevel[174].doorY = 12 + 16;
            AllLevel[174].TypeOfExit = 3; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[174].exitX = 1479 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[174].exitY = 218 + 16;
            AllLevel[174].numberClimbers = 45;
            AllLevel[174].numberUmbrellas = 50;
            AllLevel[174].numberExploders = 10;
            AllLevel[174].numberBlockers = 10;
            AllLevel[174].numberBuilders = 30;
            AllLevel[174].numberBashers = 20;
            AllLevel[174].numberMiners = 20;
            AllLevel[174].numberDiggers = 30;
            AllLevel[174].MinFrequencyComming = 1;
            AllLevel[174].FrequencyComming = 1;
            AllLevel[174].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[174].totalTime = 11;
            AllLevel[175].TotalLemmings = 25;
            AllLevel[175].InitPosX = 0; // Init xscroll
            AllLevel[175].NameLev = "levels/user/user019";
            AllLevel[175].nameOfLevel = "This is a user level";
            AllLevel[175].TypeOfDoor = 4; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[175].doorX = 240 + 16;
            AllLevel[175].doorY = 149 + 16;
            AllLevel[175].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[175].exitX = 2333 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[175].exitY = 144 + 16;
            AllLevel[175].numberClimbers = 45;
            AllLevel[175].numberUmbrellas = 50;
            AllLevel[175].numberExploders = 10;
            AllLevel[175].numberBlockers = 10;
            AllLevel[175].numberBuilders = 30;
            AllLevel[175].numberBashers = 20;
            AllLevel[175].numberMiners = 20;
            AllLevel[175].numberDiggers = 30;
            AllLevel[175].MinFrequencyComming = 1;
            AllLevel[175].FrequencyComming = 1;
            AllLevel[175].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[175].totalTime = 11;
            AllLevel[176].TotalLemmings = 25;
            AllLevel[176].InitPosX = 0; // Init xscroll
            AllLevel[176].NameLev = "levels/user/user020";
            AllLevel[176].nameOfLevel = "Limit is your imagination";
            AllLevel[176].TypeOfDoor = 5; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[176].doorX = 240 + 16;
            AllLevel[176].doorY = 249 + 16;
            AllLevel[176].TypeOfExit = 8; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[176].exitX = 2511 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[176].exitY = 227 + 16;
            AllLevel[176].numberClimbers = 45;
            AllLevel[176].numberUmbrellas = 50;
            AllLevel[176].numberExploders = 10;
            AllLevel[176].numberBlockers = 10;
            AllLevel[176].numberBuilders = 30;
            AllLevel[176].numberBashers = 20;
            AllLevel[176].numberMiners = 20;
            AllLevel[176].numberDiggers = 30;
            AllLevel[176].MinFrequencyComming = 1;
            AllLevel[176].FrequencyComming = 1;
            AllLevel[176].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[176].totalTime = 11;
            AllLevel[177].TotalLemmings = 25;
            AllLevel[177].InitPosX = 0; // Init xscroll
            AllLevel[177].NameLev = "levels/user/user021";
            AllLevel[177].nameOfLevel = "Use Vita levels whatever";
            AllLevel[177].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[177].doorX = 20 + 16;
            AllLevel[177].doorY = 209 + 16;
            AllLevel[177].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[177].exitX = 980 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[177].exitY = 370 + 16;
            AllLevel[177].numberClimbers = 45;
            AllLevel[177].numberUmbrellas = 50;
            AllLevel[177].numberExploders = 10;
            AllLevel[177].numberBlockers = 10;
            AllLevel[177].numberBuilders = 30;
            AllLevel[177].numberBashers = 20;
            AllLevel[177].numberMiners = 20;
            AllLevel[177].numberDiggers = 30;
            AllLevel[177].MinFrequencyComming = 1;
            AllLevel[177].FrequencyComming = 1;
            AllLevel[177].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[177].totalTime = 11;

            AllLevel[178].TotalLemmings = 25;
            AllLevel[178].InitPosX = 0; // Init xscroll
            AllLevel[178].NameLev = "levels/user/user022";
            AllLevel[178].nameOfLevel = "Try yourself";
            AllLevel[178].TypeOfDoor = 6; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[178].doorX = 137 + 16;
            AllLevel[178].doorY = 17 + 16;
            AllLevel[178].TypeOfExit = 4; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[178].exitX = 890 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[178].exitY = 414 + 16;
            AllLevel[178].numberClimbers = 45;
            AllLevel[178].numberUmbrellas = 50;
            AllLevel[178].numberExploders = 10;
            AllLevel[178].numberBlockers = 10;
            AllLevel[178].numberBuilders = 30;
            AllLevel[178].numberBashers = 20;
            AllLevel[178].numberMiners = 20;
            AllLevel[178].numberDiggers = 30;
            AllLevel[178].MinFrequencyComming = 1;
            AllLevel[178].FrequencyComming = 1;
            AllLevel[178].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[178].totalTime = 11;
            AllLevel[179].TotalLemmings = 25;
            AllLevel[179].InitPosX = 0; // Init xscroll
            AllLevel[179].NameLev = "levels/user/user023";
            AllLevel[179].nameOfLevel = "End";
            AllLevel[179].TypeOfDoor = 7; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[179].doorX = 140 + 16;
            AllLevel[179].doorY = 25 + 16;
            AllLevel[179].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[179].exitX = 1970 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[179].exitY = 290 + 16;
            AllLevel[179].numberClimbers = 45;
            AllLevel[179].numberUmbrellas = 50;
            AllLevel[179].numberExploders = 10;
            AllLevel[179].numberBlockers = 10;
            AllLevel[179].numberBuilders = 30;
            AllLevel[179].numberBashers = 20;
            AllLevel[179].numberMiners = 20;
            AllLevel[179].numberDiggers = 30;
            AllLevel[179].MinFrequencyComming = 1;
            AllLevel[179].FrequencyComming = 1;
            AllLevel[179].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[179].totalTime = 11;

            AllLevel[180].TotalLemmings = 25;
            AllLevel[180].InitPosX = 0; // Init xscroll
            AllLevel[180].NameLev = "levels/user/user024";
            AllLevel[180].nameOfLevel = "PS3???";
            AllLevel[180].TypeOfDoor = 8; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[180].doorX = 100 + 16;
            AllLevel[180].doorY = 20 + 16;
            AllLevel[180].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[180].exitX = 1330 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[180].exitY = 449 + 16;
            AllLevel[180].numberClimbers = 45;
            AllLevel[180].numberUmbrellas = 50;
            AllLevel[180].numberExploders = 10;
            AllLevel[180].numberBlockers = 10;
            AllLevel[180].numberBuilders = 30;
            AllLevel[180].numberBashers = 20;
            AllLevel[180].numberMiners = 20;
            AllLevel[180].numberDiggers = 30;
            AllLevel[180].MinFrequencyComming = 1;
            AllLevel[180].FrequencyComming = 1;
            AllLevel[180].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[180].totalTime = 11;

            AllLevel[181].TotalLemmings = 25;
            AllLevel[181].InitPosX = 0; // Init xscroll
            AllLevel[181].NameLev = "levels/user/user025";
            AllLevel[181].nameOfLevel = "Really???";
            AllLevel[181].TypeOfDoor = 2; // 1-egypt 2-hell 3-greek 4-l2 egypt 8-beach
            AllLevel[181].doorX = 100 + 16;
            AllLevel[181].doorY = 20 + 16;
            AllLevel[181].TypeOfExit = 6; // 1-egypt 2-greek 3-hell 4-rock
            AllLevel[181].exitX = 856 + 16; // See exact pos with "F1" ->  +16 (half of 32x32 mouse sprite) = center of mouse sprite.
            AllLevel[181].exitY = 386 + 16;
            AllLevel[181].numberClimbers = 45;
            AllLevel[181].numberUmbrellas = 50;
            AllLevel[181].numberExploders = 10;
            AllLevel[181].numberBlockers = 10;
            AllLevel[181].numberBuilders = 30;
            AllLevel[181].numberBashers = 20;
            AllLevel[181].numberMiners = 20;
            AllLevel[181].numberDiggers = 30;
            AllLevel[181].MinFrequencyComming = 1;
            AllLevel[181].FrequencyComming = 1;
            AllLevel[181].NbLemmingsToSave = 10; // ??% of ?? = ?? (??*??/100)
            AllLevel[181].totalTime = 11;
        }
    }
}
