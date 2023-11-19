using System;
using System.IO;

using Lemmings.NET.Constants;
using Lemmings.NET.Datatables;
using Lemmings.NET.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using static Lemmings.NET.Constants.SizeSprites;

namespace Lemmings.NET
{
    public partial class LemmingsNetGame : Game
    {
        Point gameResolution = new(1100, 700);
        Color letterboxingColor = new(0, 0, 0);
        RenderTarget2D renderTarget;
        Rectangle renderTargetDestination;
        bool scaled;
        private bool _lockMouse;
        private ELevelCategory _levelCategory;
        private Sprites _sprites;
        private Music _music;
        private Sfx _sfx;
        private Fonts _fonts;
        private Datatables.Mouse _mouse;

        double actWaves444 = 0, actWaves333 = 0, frameWaves = 0, actWaves = 0;
        private bool initON = false;
        RenderTarget2D colors88, normals;
        Effect efecto;
        int loopcolor = 0;
        private Texture2D crateNormals;
        private Texture2D text;
        float peakheight = 25;
        float frameWater = 0;

        private Texture2D rainbowpic;
        public Color[] Looplogo { get; set; } = new Color[100 * 100];
        public Color[] Looplogo2 { get; set; } = new Color[100 * 100];

        const string fileName = "LevelStats.txt"; // savegame
        public int Qexplo { get; set; }
        public int Iexplo { get; set; }
        public int TopY { get; set; }
        public int NumberAlive { get; set; }
        public int TotalExploding { get; set; }
        public int LifeCount { get; set; }
        public float DoorExitDepth { get; set; } = 0.403f;  // default value--bigger than 0.5f is behind the terrain (0.6f level 58 for example)
        public bool MustReadFile { get; set; } = true;
        public bool MainMenu { get; set; } = true;
        public bool LevelOn { get; set; } = false;
        public bool Paused { get; set; } = false;
        public bool Updown { get; set; } = true;
        public bool LevelEnded { get; set; } = false;
        public bool ExitLevel { get; set; } = false;
        public bool ExitBad { get; set; } = false;
        private bool _endSongPlayed;
        // Paused Lemmings update vars,time counter,door open,bombers countdown,traps
        public string LemSkill { get; set; } = "";
        // 38 * 53 size of mask exploder BE CAREFUL WITH NEW() FOR MASK SYSTEM-OUT OF MEMORY CRASHES
        public Color[] Colorsobre33 { get; set; } = new Color[38 * 53];
        public Color[] Colormask33 { get; set; } = new Color[38 * 53]; // explode mask 38*53
        public Color[] Colorsobre2 { get; set; } = new Color[20 * 20];
        public Color[] Colormask2 { get; set; } = new Color[20 * 20];  // miner mask 20*20 && basher too 20*20
        public Color[] C25 { get; set; } = new Color[4096 * 4096]; // Maximun size of a color array used for mask all the level
        // this three color mask are for the arrows for now 500*512 size is enough
        public Color[] Colorsobre22 { get; set; } = new Color[500 * 512];
        public Color[] Colormask22 { get; set; } = new Color[500 * 512];
        public Color[] Colormasktotal { get; set; } = new Color[500 * 512];
        KeyboardState oldK, actK;
        public float SizeL { get; set; } = 1.35f; //1.2f was default in the beggining
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////explosions particles data and definitions
        public int PARTICLE_NUM { get; set; } = 24; //24
        public double MAX_DX { get; set; } = 3.5; //1.5
        public double MIN_DX { get; set; } = -3.5; //-1.5
        public double MAX_DY { get; set; } = 2; //1
        public double MIN_DY { get; set; } = -8; //-4
        public int LIFE_COUNTER { get; set; } = 74; //64 original value
        public int LIFE_VARIANCE { get; set; } = 16; //16
        public double GRAVITY { get; set; } = 0.1; //0.1
        public bool Exploding { get; set; } = false; //always start as false - true is when are exploding or active
        public const int totalExplosions = 256;  // be careful with the number of lemmings per level -- this is the size of elements for explosions 500*24
        Particle[,] Explosion { get; set; } = new Particle[totalExplosions, 24];
        private int xExp, yExp, actItem = 0;
        Models.Particles[] particle;
        private SoundEffect song, strap;
        private SoundEffectInstance songInstance, strapInstance;
        private bool doorWaveOn = false;
        private float rparticle1;
        private bool rightparticle;
        private int numParticles = 300, sx, sy, actualBlow = 0, rest = 0, _currentLevelNumber = 1, numTOTdoors = 1, numACTdoor = 0, numTOTexits = 1,
             numTOTsteel = 0, percent = 0, numTOTadds = 0, numTOTplats = 0;
        Random rnd = new();
        private int walker_frame = 0, builder_frame = 0, builder_frame_second = 1;
        private bool _decreaseOn, _increaseOn;
        private int _numSaved = 0;
        public int Contador { get; set; } = 1;
        public int Contador2 { get; set; } = 0;
        public int Frente { get; set; } = 0;
        public int Frente2 { get; set; } = 0;
        public float Lem_depth { get; set; } = 0.300f;
        public float Contadortime { get; set; } = 0;
        public float Contadortime2 { get; set; } = 0;
        public int Lemsneeded { get; set; } = 1;
        public int Numlems { get; set; } = 1;
        private int numlemnow = 0;
        private int z1 = 0;
        private int z2 = 0;
        private int z3 = 0;
        private int mmlevchoose = 0;
        public int Framesecond { get; set; } = 6;
        public int Framesecond2 { get; set; } = 2;
        public int Framesecond3 { get; set; } = 1;  // frame speed less all go crazy 6->ok framesecond=6 default framesecond2=3 default
        Lem[] lemming;
        Varsprites[] sprite;
        Vartraps[] trap;
        Varadds[] adds;
        Varplat[] plats;
        Vararrows[] arrow;
        Varsteel[] steel;
        Varmoredoors[] moreDoors;
        Varmoreexits[] moreexits;
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        //vita touch textures
        private int maxnumberfalling = 210, useumbrella = 100, NumTotTraps = 0, NumTotArrow = 0, dibujaloop = 1;
        private bool dibuja = true, luzmas = true, luzmas2 = true, draw2 = true, dibuja3 = false, draw_walker = false, draw_builder = false;
        private bool rayLigths;
        private bool mouseOnLem = false;
        private bool fade = true, blink1on = false, blink2on = false, blink3on = false, TrapsON = false, ArrowsON = false, AddsON = false, PlatsON = false;
        private bool SteelON = false;
        private double totalTime, millisecondsElapsed = 0;
        private int Frame = 0;
        private int Frame2 = 0, Frame3 = 0;
        private Texture2D mascaraexplosion, mascarapared, explosion_particle;
        private Texture2D lohno, squemado, lhiss, lchink;
        private Texture2D earth;
        private Texture2D foregroundTexture;
        private Texture2D mainMenuSign, mainMenuSign2, ranksign1, ranksign2, ranksign3, ranksign5, ranksign6;
        private Texture2D mas, menos, paraguas, puente, pausa, pared, pico, bomba, rompesuelo;
        private Texture2D puente_nomas;
        private Texture2D myTexture, circulo_led;
        private Texture2D puerta_ani;
        private Texture2D salida_ani1, salida_ani1_1, sale;
        private Texture2D backmenu2, backlogo;
        private Texture2D avanzar, cuadrado_menu, logo_fondo, nubes_2, nubes, agua2;
        
        private string strPositionMouse;
        private int _scrollX = 0;  // scroll X of the entire level
        private int _scrollY = 0;
        int frameCounter = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;
        Vector2 mousepos = Vector2.Zero;
        Vector2 direction_sprite;
        MouseState mouseActState, mouseAntState;
        float delayPercent = 1f;
        bool nobasher;
        Rectangle bloqueo, arrowLem;
        Point poslem;
        int[] terrainContour;
        int ssi, px, py, ancho, amount, alto, positioYOrig, y55, x55, startposy, framepos, yypos99, cantidad99, yy99, xx99, s, maxluz;
        int maxluz2, xx66, xz, cantidad22, alto66, ancho66, yypos888, yy88, xx88, y4, x4, posy456, posx456, r, actLEM, arriba, _below, medx, medy, b, ti, pixx;
        int pos_real, wer3, width2, top2, yypos555, yy33, xx33, xEmpty, xErase, valX, valY, y, posi_real, ykk, xkk, abajo2, pixx2, pos_real2, py2, px2, valorx, valory;
        int varParticle, tYheight, vv444, spY, framereal565, xx55, yy55, swidth, sheight, sx1, sy1, xxAnim, w, h, x2, yy66, frameact, ex22, actLEM2, crono, framereal55, framesale;
        int rest2, mmstartx, mmstarty, mmX, width, xwe, xqw, mmx, mmy, mmKX, mmKY, mmKplusY, levelACT, mmKindX, mmKindY, mmPlusy;

        private void Update_level()
        {
            frameCounter++;
            if (_elapsed_time > 1)
            {
                _fps = frameCounter;
                frameCounter = 0;
                _elapsed_time = 0;
            }
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
            if (walker_frame > walker_framesecond)
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
                if (!Paused)
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
            ActualizeMouse();
            // stop all things for exit prepare
            if (LevelEnded)
            {
                Paused = true;
            }
            MoverLemming();
            if (sprite != null) //sprites logic if necessary puto77
            {
                for (ssi = 0; ssi < sprite.Length; ssi++)
                {
                    sprite[ssi].frame++;
                    if (sprite[ssi].sprite.Name == "touch/fire_sprites_other" && sprite[ssi].frame > sprite[ssi].framesecond)
                    {
                        sprite[ssi].frame = 0;
                        if (sprite[ssi].minus)
                            sprite[ssi].actFrame -= 2;
                        else
                            sprite[ssi].actFrame++; // 2 frames less to return to zero better effect i think
                        if (sprite[ssi].actFrame > 14 && !sprite[ssi].minus)
                        {
                            sprite[ssi].actFrame = 15;
                            sprite[ssi].minus = true;
                        }
                        if (sprite[ssi].actFrame < 0 && sprite[ssi].minus)
                        {
                            sprite[ssi].minus = false;
                            sprite[ssi].actFrame = 1;
                        }
                        continue;
                    }
                    if (sprite[ssi].frame > sprite[ssi].framesecond)
                    {
                        sprite[ssi].frame = 0;
                        sprite[ssi].actFrame++;
                        if (sprite[ssi].actFrame > (sprite[ssi].axisX * sprite[ssi].axisY) - 1)
                            sprite[ssi].actFrame = 0;
                    }
                    if (sprite[ssi].speed != 0)  // spider destination puto puto puto
                    {
                        if (sprite[ssi].calc)
                        {
                            sprite[ssi].calc = false;
                            if (!sprite[ssi].minus)
                            {
                                sprite[ssi].pos.X = sprite[ssi].path[sprite[ssi].actVect].X;
                                sprite[ssi].pos.Y = sprite[ssi].path[sprite[ssi].actVect].Y;
                                sprite[ssi].speed = sprite[ssi].path[sprite[ssi].actVect].Z;
                                sprite[ssi].dest.X = sprite[ssi].path[sprite[ssi].actVect + 1].X;
                                sprite[ssi].dest.Y = sprite[ssi].path[sprite[ssi].actVect + 1].Y;
                            }
                            else
                            {
                                sprite[ssi].dest.X = sprite[ssi].path[sprite[ssi].actVect].X;
                                sprite[ssi].dest.Y = sprite[ssi].path[sprite[ssi].actVect].Y;
                                sprite[ssi].speed = sprite[ssi].path[sprite[ssi].actVect].Z;
                                sprite[ssi].pos.X = sprite[ssi].path[sprite[ssi].actVect + 1].X;
                                sprite[ssi].pos.Y = sprite[ssi].path[sprite[ssi].actVect + 1].Y;
                            }
                            if (!sprite[ssi].minus)
                            {
                                sprite[ssi].actVect++;
                            }
                            else
                            {
                                sprite[ssi].actVect--;
                            }
                            if (sprite[ssi].actVect > sprite[ssi].path.Length - 2 && !sprite[ssi].minus)
                            {
                                sprite[ssi].actVect--; sprite[ssi].minus = true;
                            }
                            if (sprite[ssi].actVect < 0 && sprite[ssi].minus)
                            {
                                sprite[ssi].actVect++; sprite[ssi].minus = false;
                            }

                            continue; // control when arrive to LAST destination point actvect
                        }
                        direction_sprite = Vector2.Normalize(sprite[ssi].dest - sprite[ssi].pos);
                        sprite[ssi].pos = sprite[ssi].pos + direction_sprite * sprite[ssi].speed;
                        float distance = Vector2.Distance(sprite[ssi].pos, sprite[ssi].dest);
                        if (distance < 1)
                        {
                            sprite[ssi].calc = true;
                            continue; // control when arrive to destination point
                        }
                        sprite[ssi].rotation = (float)Math.Atan2(direction_sprite.X, direction_sprite.Y) * -1;
                    }
                }
            }
            if (PlatsON && !Paused)
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
                        px = plats[i].areaDraw.X - (plats[i].areaDraw.Width / 2);
                        py = plats[i].areaDraw.Y;
                        ancho = plats[i].areaDraw.Width;
                        amount = ancho * 1; // *height
                        alto = plats[i].step * plats[i].numSteps;
                        positioYOrig = plats[i].areaDraw.Y + (plats[i].actStep * plats[i].step);
                        bool realLine = false;
                        for (y55 = 0; y55 < alto; y55++)
                        {
                            for (x55 = 0; x55 < plats[i].areaDraw.Width; x55++)
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
                        if (debug)
                            earth.SetData(C25, 0, earth.Width * earth.Height); //set this only for debugger and see the real c25 redraw
                    }
                    plats[i].frame++;
                }
            }

            if (AddsON && !Paused)
            {
                startposy = adds[0].sprite.Height / adds[0].numFrames; // height of each frame inside the whole sprite
                framepos = startposy * adds[0].actFrame; // actual y position of the frame
                ancho = adds[0].sprite.Width;
                amount = ancho * startposy; // height frame
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
                py = adds[0].areaDraw.Y;
                px = adds[0].areaDraw.X;
                yypos99 = 0;
                cantidad99 = 0;
                for (yy99 = 0; yy99 < startposy; yy99++)
                {
                    yypos99 = (yy99 + py) * earth.Width;
                    for (xx99 = 0; xx99 < ancho; xx99++)
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
            if (TrapsON && dibuja && !Paused)
            {
                for (s = 0; s < NumTotTraps; s++)
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
            if (!Paused)
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
            maxluz = 14; // numero de ciclos de variar el rectangle del EFECTO DE LUCES 50 normalmente
            maxluz2 = 200;
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
                xx66 = varExit[_level[_currentLevelNumber].TypeOfExit].numFram - 1;
                frameExit++;
                if (frameExit > xx66)
                {
                    frameExit = 0;
                }
            }
            if (!Paused)
                Door();
            Menu_logic();
            myTexture = Content.Load<Texture2D>("luces/" + Contador);// okokokokokokokok

            if (dibuja && NumTotArrow > 0) // dibuja or dibuja2 test performance-- this is the worst part of the code NEED OPTIMIZATION
            {
                for (xz = 0; xz < NumTotArrow; xz++)
                {
                    cantidad22 = arrow[xz].area.Width * arrow[xz].area.Height;
                    arrow[xz].flechas.GetData(Colormask22, 0, arrow[xz].flechas.Height * arrow[xz].flechas.Width);
                    //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                    py = arrow[xz].area.Y;
                    px = arrow[xz].area.X;
                    alto66 = arrow[xz].area.Height;
                    ancho66 = arrow[xz].area.Width;
                    cantidad22 = 0;
                    yypos888 = 0;
                    yy88 = 0;
                    xx88 = 0;
                    for (yy88 = 0; yy88 < alto66; yy88++)
                    {
                        yypos888 = (yy88 + py) * earth.Width;
                        for (xx88 = 0; xx88 < ancho66; xx88++)
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
                        for (y4 = 0; y4 < arrow[xz].area.Height; y4++)
                        {
                            for (x4 = 0; x4 < arrow[xz].area.Width; x4++)
                            {
                                posy456 = y4 % arrow[xz].flechas.Height;
                                posx456 = x4 % arrow[xz].flechas.Width;
                                posx456 = (arrow[xz].flechas.Width - 1) - ((posx456 + arrow[xz].desplaza) % arrow[xz].flechas.Width); // left perfecto
                                Colormasktotal[(y4 * arrow[xz].area.Width) + x4].PackedValue = Colormask22[(posy456 * arrow[xz].flechas.Width) + posx456].PackedValue;
                            }
                        }
                        for (r = 0; r < cantidad22; r++)
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
                        for (y4 = 0; y4 < arrow[xz].area.Height; y4++)
                        {
                            for (x4 = 0; x4 < arrow[xz].area.Width; x4++)
                            {
                                posy456 = y4 % arrow[xz].flechas.Height;
                                posx456 = x4 % arrow[xz].flechas.Width;
                                posx456 = ((posx456 + arrow[xz].desplaza) % arrow[xz].flechas.Width);  //Left okok
                                Colormasktotal[(y4 * arrow[xz].area.Width) + x4].PackedValue = Colormask22[(posy456 * arrow[xz].flechas.Width) + posx456].PackedValue;
                            }
                        }
                        for (r = 0; r < cantidad22; r++)
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

        private void Scrolling()
        {
            if (mousepos.X + 20 > gameResolution.X &&
                _scrollX + gameResolution.X < earth.Width)
            {
                _scrollX += 5;
            }
            if (_scrollX + gameResolution.X > earth.Width)
            {
                _scrollX = earth.Width - gameResolution.X;
            }
            if (mousepos.X < -10 && _scrollX > 0)
            {
                _scrollX -= 5;
            }
            if (_scrollX < 0)
            {
                _scrollX = 0;
            }
            if (mousepos.Y + 20 > gameResolution.Y && _scrollY + 512 < earth.Height)
            {
                _scrollY += 5;
            }
            if (_scrollY + 512 > earth.Height)
            {
                _scrollY = earth.Height - 512;
            }
            if (mousepos.Y < -10 && _scrollY > 0)
            {
                _scrollY -= 5;
            }
            if (_scrollY < 0)
            {
                _scrollY = 0;
            }
            if (mousepos.Y < -14)
                mousepos.Y = -14;
            if (mousepos.Y > gameResolution.Y * (scaled ? 2 : 1))
                mousepos.Y = gameResolution.Y * (scaled ? 2 : 1);
            if (mousepos.X < -14)
                mousepos.X = -14;
            if (mousepos.X > gameResolution.X * (scaled ? 2 : 1))
                mousepos.X = gameResolution.X * (scaled ? 2 : 1);
            if (_lockMouse)
                Microsoft.Xna.Framework.Input.Mouse.SetPosition((int)mousepos.X, (int)mousepos.Y); // setposition //this is for my son kids don't know move mouse so good  
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
                medx = 14;
                medy = 14;
                for (b = 0; b < numLemmings; b++)
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
                if ((mousepos.X + 16 >= lemming[actLEM].PosX - _scrollX && mousepos.X + 16 <= lemming[actLEM].PosX - _scrollX + 28
                        && mousepos.Y + 16 >= lemming[actLEM].PosY - _scrollY && mousepos.Y + 16 <= lemming[actLEM].PosY + 28 - _scrollY) && !mouseOnLem)
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
                if (TrapsON && !Paused) //Traps logic and sounds
                {
                    for (ti = 0; ti < NumTotTraps; ti++)
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
                                    strap = Content.Load<SoundEffect>("soundfx/tenton");
                                    strapInstance = strap.CreateInstance();
                                    if (strapInstance.State == SoundState.Playing)
                                    {
                                        strapInstance.Stop();
                                    }
                                    strapInstance.Play();
                                    break;
                                case "traps/dead_trampa":
                                    strap = Content.Load<SoundEffect>("soundfx/mantrap");
                                    strapInstance = strap.CreateInstance();
                                    if (strapInstance.State == SoundState.Playing)
                                    {
                                        strapInstance.Stop();
                                    }
                                    strapInstance.Play();
                                    break;
                                case "traps/dead_soga":
                                    strap = Content.Load<SoundEffect>("soundfx/chain");
                                    strapInstance = strap.CreateInstance();
                                    if (strapInstance.State == SoundState.Playing)
                                    {
                                        strapInstance.Stop();
                                    }
                                    strapInstance.Play();
                                    break;
                                case "traps/dead_bombona":
                                    strap = Content.Load<SoundEffect>("soundfx/chupar");
                                    strapInstance = strap.CreateInstance();
                                    if (strapInstance.State == SoundState.Playing)
                                    {
                                        strapInstance.Stop();
                                    }
                                    strapInstance.Play();
                                    break;
                                case "traps/dead_10":
                                    strap = Content.Load<SoundEffect>("soundfx/10tones");
                                    strapInstance = strap.CreateInstance();
                                    if (strapInstance.State == SoundState.Playing)
                                    {
                                        strapInstance.Stop();
                                    }
                                    strapInstance.Play();
                                    break;
                                default:
                                    if (_sfx.Die.State == SoundState.Playing)
                                    {
                                        _sfx.Die.Stop();
                                    }
                                    try
                                    {
                                        _sfx.Die.Play();
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
                                    if (_sfx.Fire.State == SoundState.Playing)
                                    {
                                        _sfx.Fire.Stop();
                                    }
                                    try
                                    {
                                        _sfx.Fire.Play();
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
                                    if (_sfx.Glup.State == SoundState.Playing)
                                    {
                                        _sfx.Glup.Stop();
                                    }
                                    try
                                    {
                                        _sfx.Glup.Play();
                                    }
                                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                                    lemming[actLEM].Drown = true;
                                    lemming[actLEM].Burned = false;
                                    lemming[actLEM].Explode = false;
                                    lemming[actLEM].Exploser = false;
                                    lemming[actLEM].Falling = false;
                                    lemming[actLEM].Fall = false;
                                    lemming[actLEM].Numframes = water_frames;
                                    lemming[actLEM].Actualframe = 0;
                                    lemming[actLEM].Active = false;
                                    lemming[actLEM].Walker = false;
                                    break;
                                default:
                                    if (_sfx.Die.State == SoundState.Playing)
                                    {
                                        _sfx.Die.Stop();
                                    }
                                    try
                                    {
                                        _sfx.Die.Play();
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
                if (mouseOnLem && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
                {
                    if (_currentSelectedSkill == ECurrentSkill.DIGGER && !lemming[actLEM].Digger && lemming[actLEM].Onmouse //DIGGER
                        && (lemming[actLEM].Walker || lemming[actLEM].Builder || lemming[actLEM].Basher || lemming[actLEM].Miner))
                    {
                        _nbDiggerRemaining--;
                        if (_nbDiggerRemaining < 0)
                        {
                            _nbDiggerRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Digger = true;
                            lemming[actLEM].Fall = false;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = digger_frames;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.CLIMBER && lemming[actLEM].Onmouse && !lemming[actLEM].Climber) //CLIMBER
                    {
                        _nbClimberRemaining--;
                        if (_nbClimberRemaining < 0)
                        {
                            _nbClimberRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Climber = true;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.FLOATER && lemming[actLEM].Onmouse && !lemming[actLEM].Umbrella && !lemming[actLEM].Breakfloor) //FLOATER
                    {
                        _nbFloaterRemaining--;
                        if (_nbFloaterRemaining < 0)
                        {
                            _nbFloaterRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Umbrella = true;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.EXPLODER && lemming[actLEM].Onmouse && !lemming[actLEM].Exploser) //BOMBER
                    {
                        _nbExploderRemaining--;
                        if (_nbExploderRemaining < 0)
                        {
                            _nbExploderRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Exploser = true;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.BLOCKER && lemming[actLEM].Onmouse && !lemming[actLEM].Blocker //BLOCKER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Builder || lemming[actLEM].Basher || lemming[actLEM].Miner))
                    {
                        _nbBlockerRemaining--;
                        if (_nbBlockerRemaining < 0)
                        {
                            _nbBlockerRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Blocker = true;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = blocker_frames;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.BUILDER && lemming[actLEM].Onmouse && !lemming[actLEM].Builder //BUILDER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Basher || lemming[actLEM].Miner || lemming[actLEM].Bridge))
                    {
                        _nbBuilderRemaining--;
                        if (_nbBuilderRemaining < 0)
                        {
                            _nbBuilderRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Bridge = false;
                            lemming[actLEM].Builder = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Numstairs = 0;
                            lemming[actLEM].Numframes = builder_frames;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.BASHER && lemming[actLEM].Onmouse && !lemming[actLEM].Basher //BASHER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Builder || lemming[actLEM].Miner))
                    {
                        _nbBasherRemaining--;
                        if (_nbBasherRemaining < 0)
                        {
                            _nbBasherRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Basher = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Numframes = basher_frames;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.MINER && lemming[actLEM].Onmouse && !lemming[actLEM].Miner //MINER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Basher || lemming[actLEM].Builder))
                    {
                        _nbMinerRemaining--;
                        if (_nbMinerRemaining < 0)
                        {
                            _nbMinerRemaining = 0;
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                        }
                        else
                        {
                            if (_sfx.MousePre.State == SoundState.Playing)
                            {
                                _sfx.MousePre.Stop();
                            }
                            _sfx.MousePre.Play();
                            lemming[actLEM].Miner = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Numframes = pico_frames;
                            continue;
                        }
                    }

                }
                if (Paused)
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
                        _numSaved++;  // here is where the lemming go inside after door animation
                    }
                    continue;
                }
                arriba = 0;
                _below = 0;
                pixx = lemming[actLEM].PosX + medx;
                ancho = earth.Width;
                for (x55 = 0; x55 <= 8; x55++)
                {
                    pos_real = lemming[actLEM].PosY + x55 + medy + medy;  ///////////// pixel por debajo -> beneath.............
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
                        if (_sfx.Die.State == SoundState.Playing)
                        {
                            _sfx.Die.Stop();
                        }
                        try
                        {
                            _sfx.Die.Play();
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
                    lemming[actLEM].Numframes = floater_frames;
                }
                if ((_below > 8 && !lemming[actLEM].Fall && (!lemming[actLEM].Digger || !lemming[actLEM].Miner)) && !lemming[actLEM].Falling
                    && !lemming[actLEM].Explode && lemming[actLEM].Active)
                {
                    lemming[actLEM].Fall = true;
                    lemming[actLEM].Pixelscaida = 0;
                    lemming[actLEM].Climbing = false;
                    lemming[actLEM].Walker = false;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = faller_frames;
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
                        lemming[actLEM].Numframes = walker_frames;  //8 walker;4 fall;16 digger;breakfloor 16;escala ...
                    }
                    else
                    {
                        if (_sfx.Splat.State == SoundState.Playing)
                        {
                            _sfx.Splat.Stop();
                        }
                        try
                        {
                            _sfx.Splat.Play();
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
                        lemming[actLEM].Numframes = floor_frames;
                        lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
                if ((_below == 0) && lemming[actLEM].Walker && (!lemming[actLEM].Digger && !lemming[actLEM].Miner))
                {
                    lemming[actLEM].Pixelscaida = 0;
                }
                for (x55 = 0; x55 <= 20; x55++)
                {
                    pos_real = lemming[actLEM].PosY + medy + medy - x55;
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
                    lemming[actLEM].Numframes = faller_frames;
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
                        for (wer3 = 0; wer3 < NumTotArrow; wer3++)
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
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = walker_frames;
                            continue;
                        }
                    }
                    if (lemming[actLEM].Right)
                    {
                        width2 = 20;
                        top2 = 20;
                        px = lemming[actLEM].PosX + 12;
                        py = lemming[actLEM].PosY + 14;
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
                        amount = width2 * top2;
                        mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                        amount = 0;
                        yypos888 = 0;
                        yy88 = 0;
                        xx88 = 0;
                        for (yy88 = 0; yy88 < top2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < width2; xx88++)
                            {
                                Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                amount++;
                            }
                        }
                        for (r = 0; r < amount; r++)
                        {
                            if (SteelON)
                            {
                                sx = r % width2;
                                sy = r / width2;
                                x.X = px + sx;
                                x.Y = py + sy;
                                for (xz = 0; xz < numTOTsteel; xz++)
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
                                    lemming[actLEM].Numframes = walker_frames;
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
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < top2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < width2; xx33++)
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
                            lemming[actLEM].Numframes = walker_frames;
                            continue;
                        }
                    }
                    else
                    {
                        width2 = 20;
                        top2 = 20;
                        px = lemming[actLEM].PosX - 4;
                        if (px < 0)
                        {
                            px = 0;
                        }
                        py = lemming[actLEM].PosY + 14;
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
                        amount = width2 * top2;
                        mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                        amount = 0; yypos888 = 0; yy88 = 0; xx88 = 0;
                        for (yy88 = 0; yy88 < top2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < width2; xx88++)
                            {
                                Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                amount++;
                            }
                        }
                        for (r = 0; r < amount; r++)
                        {
                            if (SteelON)
                            {
                                sx = r % width2;
                                sy = r / width2;
                                x.X = px + sx;
                                x.Y = py + sy;
                                for (xz = 0; xz < numTOTsteel; xz++)
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
                                    lemming[actLEM].Numframes = walker_frames;
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
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < top2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < width2; xx33++)
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
                            lemming[actLEM].Numframes = walker_frames;
                            continue;
                        }
                    }
                    Frente2 = 0;  /////// PPPPPPPPIIIIIIIIIICCCCCCCCCCCCCCCCCOOOOOOOOOOOOOOOOOOO  BASHER LOGIC puto33
                }

                if (lemming[actLEM].Basher && (lemming[actLEM].Actualframe == 10 || lemming[actLEM].Actualframe == 37) && draw2)
                {
                    if (ArrowsON) // basher arrows logic areaTrap Intersects
                    {
                        nobasher = false;
                        arrowLem.X = lemming[actLEM].PosX;
                        arrowLem.Y = lemming[actLEM].PosY;
                        arrowLem.Width = 28;
                        arrowLem.Height = 28;
                        for (wer3 = 0; wer3 < NumTotArrow; wer3++)
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
                            if (_sfx.Ting.State == SoundState.Playing)
                            {
                                _sfx.Ting.Stop();
                            }
                            _sfx.Ting.Play();
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = walker_frames;
                            continue;
                        }
                    }
                    if (lemming[actLEM].Right)
                    {
                        width2 = 20;
                        top2 = 20;
                        px = lemming[actLEM].PosX + 14;
                        py = lemming[actLEM].PosY + 8;
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
                        amount = width2 * top2;
                        mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop
                        amount = 0;
                        yypos888 = 0;
                        yy88 = 0;
                        xx88 = 0;
                        for (yy88 = 0; yy88 < top2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < width2; xx88++)
                            {
                                Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                amount++;
                            }
                        }
                        xEmpty = 0;
                        xErase = width2;
                        Frente = 0;
                        sx = 0;
                        for (valX = 0; valX < width2; valX++)
                        {
                            Frente = 0;
                            for (valY = 0; valY < top2; valY++)
                            {
                                if (SteelON)
                                {
                                    x.X = px + valX;
                                    x.Y = py + valY;
                                    for (xz = 0; xz < numTOTsteel; xz++)
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
                                        lemming[actLEM].Numframes = walker_frames;
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
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < top2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < width2; xx33++)
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
                            lemming[actLEM].Numframes = walker_frames;
                            continue;
                        }
                    }
                    else
                    {
                        width2 = 20;
                        top2 = 20;
                        px = lemming[actLEM].PosX - 5;
                        if (px < 0)
                        {
                            px = 0;
                        }
                        py = lemming[actLEM].PosY + 8;
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
                        amount = width2 * top2;
                        //////// optimized for hd3000 laptop
                        amount = 0;
                        yypos888 = 0;
                        yy88 = 0;
                        xx88 = 0;
                        for (yy88 = 0; yy88 < top2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < width2; xx88++)
                            {
                                Colorsobre2[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                amount++;
                            }
                        }
                        xEmpty = width2;
                        xErase = 0;
                        Frente = 0;
                        sx = 0;
                        for (valX = width2 - 1; valX >= 0; valX--)
                        {
                            Frente = 0;
                            for (valY = 0; valY < top2; valY++)
                            {
                                if (SteelON)
                                {
                                    x.X = px + valX;
                                    x.Y = py + valY;
                                    for (xz = 0; xz < numTOTsteel; xz++)
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
                                        lemming[actLEM].Numframes = walker_frames;
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
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < top2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < width2; xx33++)
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
                            lemming[actLEM].Numframes = walker_frames;
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
                    lemming[actLEM].Numframes = walker_frames;
                    continue;
                }
                if (lemming[actLEM].Builder && draw_builder) // BUILDER LOGIC HERE chink sound see limits tooo FIX FIX FIX
                {
                    if (lemming[actLEM].Actualframe >= 48 && lemming[actLEM].Numstairs < 12) // >=33 old with dibuja2
                    // i need to cut on frame 33 of 56 because speed problems timings and x & y axis, see later to fix speed making stairs and fix positioning for get real 56 frames
                    {
                        Frente = 0;
                        lemming[actLEM].Actualframe = builder_frames + 1;  // erase with // no compiling//  to see full frames
                        if (lemming[actLEM].Right)
                        {
                            if (arriba > 1)
                            {
                                lemming[actLEM].PosY += 6;
                                lemming[actLEM].PosX -= 14;
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].Right = false;
                                continue;

                            }
                            if (lemming[actLEM].PosY < -24) //see ok was -24 but sometimes fails the u-turn
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].PosY += 3;
                                lemming[actLEM].PosX -= 6;
                                continue;
                            }
                            for (y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                posi_real = (lemming[actLEM].PosY + 24 + y) * earth.Width + lemming[actLEM].PosX;
                                for (xx88 = 14; xx88 <= 28; xx88++)
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
                                if (_sfx.Chink.State == SoundState.Playing)
                                {
                                    _sfx.Chink.Stop();
                                }
                                _sfx.Chink.Play();
                            }
                            amount = 0;
                            for (ykk = 27; ykk < 31; ykk++)
                            {
                                posi_real = (lemming[actLEM].PosY + ykk) * earth.Width + lemming[actLEM].PosX;
                                for (xkk = 0; xkk < 28; xkk++)
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
                                lemming[actLEM].Numframes = walker_frames;
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
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].Right = true;
                                continue;

                            }
                            if (lemming[actLEM].PosY < -24) //see ok was -24
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].PosY += 3;
                                lemming[actLEM].PosX += 6;
                                continue;
                            }
                            for (y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                posi_real = (lemming[actLEM].PosY + 24 + y) * earth.Width + lemming[actLEM].PosX;
                                for (xx88 = 0; xx88 <= 14; xx88++)
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
                                if (_sfx.Chink.State == SoundState.Playing)
                                {
                                    _sfx.Chink.Stop();
                                }
                                _sfx.Chink.Play();
                            }
                            //earth.SetData<Color>(c25); //OPTIMIZED BUILDER SETDATA
                            amount = 0;
                            px = lemming[actLEM].PosX;
                            if (px < 0)
                                px = 0;
                            for (ykk = 27; ykk < 31; ykk++)
                            {
                                posi_real = (lemming[actLEM].PosY + ykk) * earth.Width + px;
                                for (xkk = 0; xkk < 28; xkk++)
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
                                lemming[actLEM].Numframes = walker_frames;
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
                        lemming[actLEM].Numframes = walker_frames;
                    }
                }
                if (lemming[actLEM].Bridge && lemming[actLEM].Actualframe == 7 && lemming[actLEM].Bridge)
                {
                    lemming[actLEM].Bridge = false;
                    lemming[actLEM].Walker = true;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = walker_frames;
                    lemming[actLEM].Numstairs = 0;
                    continue;
                }
                if (lemming[actLEM].Digger) ///// DIGGER DIGGER WARNING WARNING
                {
                    if (_below == 0 || _below == 1) // 5 ok que no se aceleren a digger si hay mas de 2 juntos antes era <9 los pixeles debajo de sus pies
                    {
                        abajo2 = 0;
                        pixx2 = lemming[actLEM].PosX + 14;
                        for (xx88 = 0; xx88 <= 4; xx88++)
                        {
                            pos_real2 = lemming[actLEM].PosY + xx88 + 28;  ///////////// pixel por debajo.............
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
                            for (y = 9; y <= 18; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                posi_real = (lemming[actLEM].PosY + 14 + y) * earth.Width + lemming[actLEM].PosX;
                                if (lemming[actLEM].PosY + 14 + y > earth.Height)
                                {
                                    break;
                                } // cortar si esta en el limite por debajo 512=earth.height
                                for (xx88 = 4; xx88 <= 24; xx88++)
                                {
                                    if (SteelON)
                                    {
                                        x.X = lemming[actLEM].PosX + xx88;
                                        x.Y = lemming[actLEM].PosY + 14 + y;
                                        for (xz = 0; xz < numTOTsteel; xz++)
                                        {
                                            if (steel[xz].area.Contains(x))
                                            {
                                                sx = -777; break;
                                            }
                                        }
                                        if (sx == -777)
                                        {
                                            lemming[actLEM].Walker = true;
                                            lemming[actLEM].Numframes = walker_frames;
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
                            amount = 0;
                            for (ykk = 9; ykk <= 18; ykk++)
                            {
                                posi_real = (lemming[actLEM].PosY + 14 + ykk) * earth.Width + lemming[actLEM].PosX;
                                for (xkk = 0; xkk < 28; xkk++)
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
                            for (ykk = 0; ykk < 210; ykk++)
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
                        lemming[actLEM].Numframes = faller_frames;
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
                        lemming[actLEM].Numframes = faller_frames;
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Builder = false;
                        lemming[actLEM].Right = !lemming[actLEM].Right;
                        continue;
                    }
                    if (lemming[actLEM].Right)
                    {
                        pos_real2 = lemming[actLEM].PosY + 27;
                        if (C25[(pos_real2 * earth.Width) + pixx - 2].R > 0 || C25[(pos_real2 * earth.Width) + pixx - 2].G > 0 || C25[(pos_real2 * earth.Width) + pixx - 2].B > 0)
                        {
                            lemming[actLEM].Right = false;
                            lemming[actLEM].PosX -= 2;   // 1 o 2 LOOK
                            lemming[actLEM].Climbing = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Numframes = walker_frames;
                            lemming[actLEM].Actualframe = 0;
                            continue;
                        }
                    }
                    else
                    {
                        pos_real2 = lemming[actLEM].PosY + 27;
                        if (C25[(pos_real2 * earth.Width) + pixx + 2].R > 0 || C25[(pos_real2 * earth.Width) + pixx + 2].G > 0 || C25[(pos_real2 * earth.Width) + pixx + 2].B > 0)
                        {
                            lemming[actLEM].Right = true;
                            lemming[actLEM].PosX += 2; // 1 o 2 LOOK
                            lemming[actLEM].Climbing = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Numframes = walker_frames;
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
                        lemming[actLEM].Numframes = walker_frames;
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
                            lemming[actLEM].Numframes = climber_frames;
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
                    ancho66 = 38;
                    alto66 = 53;
                    px = lemming[actLEM].PosX - 5; //center the big explosion to 28x28 lemming sprite
                    py = lemming[actLEM].PosY - 2;
                    py2 = 0;
                    px2 = 0;
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
                    amount = ancho66 * alto66;
                    rectangleFill.X = px2;
                    rectangleFill.Y = py2;
                    rectangleFill.Width = ancho66;
                    rectangleFill.Height = alto66;
                    mascaraexplosion.GetData(0, rectangleFill, Colormask33, 0, amount);
                    amount = 0;
                    yypos888 = 0;
                    yy88 = 0;
                    xx88 = 0;
                    for (yy88 = 0; yy88 < alto66; yy88++)
                    {
                        yypos888 = (yy88 + py) * earth.Width;
                        for (xx88 = 0; xx88 < ancho66; xx88++)
                        {
                            Colorsobre33[amount].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                            amount++;
                        }
                    }
                    for (r = 0; r < amount; r++)
                    {
                        if (SteelON)
                        {
                            sx = r % ancho66;
                            sy = r / ancho66;
                            x.X = px + sx;
                            x.Y = py + sy;
                            for (xz = 0; xz < numTOTsteel; xz++)
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
                    yypos555 = 0;
                    yy33 = 0;
                    xx33 = 0;
                    for (yy33 = 0; yy33 < alto66; yy33++)
                    {
                        yypos555 = (yy33 + py) * earth.Width;
                        for (xx33 = 0; xx33 < ancho66; xx33++)
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
                    if (_sfx.Explode.State == SoundState.Playing)
                    {
                        _sfx.Explode.Stop();
                    }
                    try
                    {
                        _sfx.Explode.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                    //explosions addons emitter - particles logic add
                    xExp = lemming[actLEM].PosX + 14;
                    yExp = lemming[actLEM].PosY + 14;
                    Explosion[actItem, 0].MaxCounter = 0;
                    Explosion[actItem, 0].Counter = 0;
                    for (Iexplo = 0; Iexplo < PARTICLE_NUM; Iexplo++)
                    {
                        Explosion[actItem, Iexplo].MaxCounter = 0;
                        byte colorr = (byte)rnd.Next(255);
                        byte colorg = (byte)rnd.Next(255);
                        byte colorb = (byte)rnd.Next(255);
                        colorFill.R = colorr;
                        colorFill.G = colorg;
                        colorFill.B = colorb;
                        colorFill.A = 255;
                        LifeCount = LIFE_COUNTER + (int)(rnd.NextDouble() * 2 * LIFE_VARIANCE) - LIFE_VARIANCE;
                        if (LifeCount > Explosion[actItem, 0].MaxCounter)
                            Explosion[0, 0].MaxCounter = LifeCount;
                        Explosion[actItem, Iexplo].dx = (rnd.NextDouble() * (MAX_DX - MIN_DX) + MIN_DX);
                        Explosion[actItem, Iexplo].dy = (rnd.NextDouble() * (MAX_DY - MIN_DY) + MIN_DY);
                        Explosion[actItem, Iexplo].x = xExp;
                        Explosion[actItem, Iexplo].y = yExp;
                        Explosion[actItem, Iexplo].Color = colorFill;
                        Explosion[actItem, Iexplo].LifeCtr = LifeCount;
                        Explosion[actItem, Iexplo].Rotation = (float)rnd.NextDouble();
                        Explosion[actItem, Iexplo].Size = (float)(rnd.NextDouble() / 2);
                    }
                    Exploding = true;
                    actItem++;
                    if (actItem > totalExplosions - 1)
                        actItem = totalExplosions - 1;
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
                    if (_sfx.Die.State == SoundState.Playing)
                    {
                        _sfx.Die.Stop();
                    }
                    try
                    {
                        _sfx.Die.Play();
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
                    if (_sfx.Die.State == SoundState.Playing)
                    {
                        _sfx.Die.Stop();
                    }
                    try
                    {
                        _sfx.Die.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                }

            }
        }

        public LemmingsNetGame()
        {
            _lockMouse = false;
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8,
                PreferredBackBufferWidth = gameResolution.X,
                PreferredBackBufferHeight = gameResolution.Y,
            };
            //// this.IsMouseVisible = true;  //WINDOWS MOUSE VISIBLE OR NOT
            Content.RootDirectory = "Content";
            Window.Title = "Lemmings.NET";
            Window.AllowUserResizing = false;
        }

        private void ActualizeMouse()
        {
            mouseAntState = mouseActState;
            mouseActState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            valorx = mouseActState.X;
            valorx += _scrollX;
            valory = mouseActState.Y;
            valory += _scrollY;
            mousepos.X = mouseActState.X;
            mousepos.Y = mouseActState.Y;
            strPositionMouse = valorx.ToString() + ", " + valory.ToString();
        }

        protected override void Initialize()
        {
            VariablesLevels();
            if (MainMenu)
                this.IsMouseVisible = false;
            base.Initialize();
        }

        private Rectangle GetRenderTargetDestination(Point resolution, int preferredBackBufferWidth, int preferredBackBufferHeight)
        {
            float resolutionRatio = (float)resolution.X / resolution.Y;
            float screenRatio;
            Point bounds = new(preferredBackBufferWidth, preferredBackBufferHeight);
            screenRatio = (float)bounds.X / bounds.Y;
            float scale;
            Rectangle rectangle = new();

            if (resolutionRatio < screenRatio)
                scale = (float)bounds.Y / resolution.Y;
            else if (resolutionRatio > screenRatio)
                scale = (float)bounds.X / resolution.X;
            else
            {
                // Resolution and window/screen share aspect ratio
                rectangle.Size = bounds;
                return rectangle;
            }
            rectangle.Width = (int)(resolution.X * scale);
            rectangle.Height = (int)(resolution.Y * scale);
            return CenterRectangle(new Rectangle(Point.Zero, bounds), rectangle);
        }

        private Rectangle CenterRectangle(Rectangle outerRectangle, Rectangle innerRectangle)
        {
            Point delta = outerRectangle.Center - innerRectangle.Center;
            innerRectangle.Offset(delta);
            return innerRectangle;
        }

        private void LoadLevel(int newLevel)
        {
            if (_music.Music20.State == SoundState.Playing)
                _music.Music20.Stop();
            _currentLevelNumber = newLevel;
            songInstance.Stop();
            LemSkill = "";
            Paused = false;
            zvTime = 0;
            _allBlow = false;
            actualBlow = 0;
            exitFrame = 999;
            _currentSelectedSkill = ECurrentSkill.NONE;
            moreexits = null;
            moreDoors = null;
            trap = null;
            arrow = null;
            sprite = null;
            numTOTexits = 1;
            numTOTdoors = 1;
            NumTotTraps = 0;
            numTOTadds = 0;
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

            Texture2D level = Content.Load<Texture2D>(_level[_currentLevelNumber].NameLev);
            earth = new Texture2D(GraphicsDevice, level.Width, level.Height);
            Color[] pixels = new Color[level.Width * level.Height];
            level.GetData(pixels);
            earth.SetData(pixels);
            earth.GetData(C25, 0, earth.Height * earth.Width); //better here than moverlemming() for performance see issues 
                                                               //see differences with old getdata, see size important (x * y)
            door1X = _level[_currentLevelNumber].doorX;
            door1Y = _level[_currentLevelNumber].doorY;
            output1X = _level[_currentLevelNumber].exitX;
            output1Y = _level[_currentLevelNumber].exitY;
            // this is the depth of the exit and doors animated sprites -- See level 58 the exit is behind the mountain (0.6f)
            if (_level[_currentLevelNumber].DoorExitDepth != 0)
            {
                DoorExitDepth = _level[_currentLevelNumber].DoorExitDepth;
            }
            else
            {
                DoorExitDepth = 0.403f;
            }
            _nbClimberRemaining = _level[_currentLevelNumber].numberClimbers;
            _nbFloaterRemaining = _level[_currentLevelNumber].numberUmbrellas;
            _nbExploderRemaining = _level[_currentLevelNumber].numberExploders;
            _nbBlockerRemaining = _level[_currentLevelNumber].numberBlockers;
            _nbBuilderRemaining = _level[_currentLevelNumber].numberBuilders;
            _nbBasherRemaining = _level[_currentLevelNumber].numberBashers;
            _nbMinerRemaining = _level[_currentLevelNumber].numberMiners;
            _nbDiggerRemaining = _level[_currentLevelNumber].numberDiggers;
            if (_nbClimberRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.CLIMBER;
            }
            else if (_nbFloaterRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.FLOATER;
            }
            else if (_nbExploderRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.EXPLODER;
            }
            else if (_nbBlockerRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.BLOCKER;
            }
            else if (_nbBuilderRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.BUILDER;
            }
            else if (_nbBasherRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.BASHER;
            }
            else if (_nbMinerRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.MINER;
            }
            else if (_nbDiggerRemaining > 0)
            {
                _currentSelectedSkill = ECurrentSkill.DIGGER;
            }
            frequencyNumber = _level[_currentLevelNumber].FrequencyComming;
            numerominfrecuencia = _level[_currentLevelNumber].MinFrequencyComming;
            Numlems = _level[_currentLevelNumber].TotalLemmings;
            Lemsneeded = _level[_currentLevelNumber].NbLemmingsToSave;
            _scrollX = _level[_currentLevelNumber].InitPosX;
            _scrollY = 0;
            lemming = new Lem[Numlems];
            VariablesTraps();
        }

#pragma warning disable S125 // Sections of code should not be commented out
        //private Texture2D mascarapico, lyipie, lglup, lsplat, walker, mainMenuLogo, lucesfondo, backmenu3, explode, backmenu1, numfont, Crate;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if (_sprites == null)
            {
                _sprites = new Sprites();
                _sprites.LoadContent(Content);
            }
            if (_music == null)
            {
                _music = new Music();
                _music.LoadContent(Content);
            }
            if (_sfx == null)
            {
                _sfx = new Sfx();
                _sfx.LoadContent(Content);
            }
            if (_fonts == null)
            {
                _fonts = new Fonts();
                _fonts.LoadContent(Content);
            }
            if (_mouse == null)
            {
                _mouse = new Datatables.Mouse();
                _mouse.LoadContent(Content);
            }
            Microsoft.Xna.Framework.Input.Mouse.SetPosition(0, 0);
            renderTarget = new RenderTarget2D(GraphicsDevice, gameResolution.X, gameResolution.Y);
            renderTargetDestination = GetRenderTargetDestination(gameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
    
            texture1pixel = new Texture2D(GraphicsDevice, 1, 1);
            texture1pixel.SetData(new Color[] { Color.White });  // texture for DRAWLINE 1x1
            mainMenuSign2 = Content.Load<Texture2D>("cubo");

            rainbowpic = Content.Load<Texture2D>("surge-rainbow"); // texture to the effect shine shader // test -> surge-rainbow2 - surge-rainbow3 - surge-rainbow
            rainbowpic.GetData(Looplogo, 0, rainbowpic.Width * rainbowpic.Height);
            //background_02  logo  llama  fondos/nubes  crateN
            text = Content.Load<Texture2D>("crate");
            //Crate = Content.Load<Texture2D>("crate");
            crateNormals = Content.Load<Texture2D>("craten");
            int widthl = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            colors88 = new RenderTarget2D(GraphicsDevice, widthl, height);
            normals = new RenderTarget2D(GraphicsDevice, widthl, height);
            efecto = Content.Load<Effect>("efecto");

            if (MainMenu)
            {
                mainMenuSign = Content.Load<Texture2D>("lem1/menusign_04");
                backlogo = Content.Load<Texture2D>("lemlogo_01");
                //mainMenuLogo = Content.Load<Texture2D>("lem1/logo_mainmenu");
                ranksign1 = Content.Load<Texture2D>("lem1/ranksign_01");
                ranksign2 = Content.Load<Texture2D>("lem1/ranksign_02");
                ranksign3 = Content.Load<Texture2D>("lem1/ranksign_03");
                ranksign6 = Content.Load<Texture2D>("lem1/ranksign_06");
                logo_fondo = Content.Load<Texture2D>("logo");
                ranksign5 = Content.Load<Texture2D>("lem1/ranksign_05");
                song = Content.Load<SoundEffect>("music/lem_menu");
                songInstance = song.CreateInstance();
                songInstance.Stop();
                songInstance.IsLooped = true;
                songInstance.Play();
                if (MustReadFile)
                {
                    if (File.Exists(fileName))
                    {
                        BinaryReader reader = new(File.Open(fileName, FileMode.Open));
                        for (int i = 0; i < numTotalLevels; i++)
                        {
                            LevelEnd[i] = reader.ReadBoolean();
                        }
                        reader.Close();
                        MustReadFile = false;
                    }
                    else
                    {
                        BinaryWriter writer = new(File.Open(fileName, FileMode.Create));
                        for (int i = 0; i < numTotalLevels; i++)
                        {
                            writer.Write(LevelEnd[i]);
                        }
                        writer.Write("(c) 2016 Oskar Oskar LEMMINGS c#. (c) 2023 FilRip from CoolBytes");
                        writer.Close();
                        MustReadFile = false;
                    }
                }

            }
            if (LevelOn) //when level starts all the vars and reset all
            {
                LoadLevel(_currentLevelNumber);

                circulo_led = Content.Load<Texture2D>("circulo_brillante");
                puerta_ani = Content.Load<Texture2D>("puerta" + string.Format("{0}", _level[_currentLevelNumber].TypeOfDoor)); // type of door puerta1-2-3-4 etc.
                string xx455 = string.Format("{0}", _level[_currentLevelNumber].TypeOfExit);
                salida_ani1 = Content.Load<Texture2D>("salida" + xx455);
                salida_ani1_1 = Content.Load<Texture2D>("salida" + xx455 + "_1");
                sale = Content.Load<Texture2D>("sale");
                agua2 = Content.Load<Texture2D>("Animations/water2");
                //backmenu1 = Content.Load<Texture2D>("background_01");
                backmenu2 = Content.Load<Texture2D>("background_012");
                //backmenu3 = Content.Load<Texture2D>("background_02");
                backlogo = Content.Load<Texture2D>("lemlogo_01");
                avanzar = Content.Load<Texture2D>("avanzar");
                cuadrado_menu = Content.Load<Texture2D>("border");
                logo_fondo = Content.Load<Texture2D>("logo");
                nubes_2 = Content.Load<Texture2D>("fondos/nubes2");
                nubes = Content.Load<Texture2D>("fondos/nubes");
                //lucesfondo = Content.Load<Texture2D>("fondos/luces de fondo guays");
                mas = Content.Load<Texture2D>("mas");
                menos = Content.Load<Texture2D>("menos");
                paraguas = Content.Load<Texture2D>("paraguas");
                puente = Content.Load<Texture2D>("puente");
                pared = Content.Load<Texture2D>("pared");
                rompesuelo = Content.Load<Texture2D>("rompesuelo");
                //explode = Content.Load<Texture2D>("explode");
                mascaraexplosion = Content.Load<Texture2D>("mascara_explode");
                puente_nomas = Content.Load<Texture2D>("puente_nomas");
                mascarapared = Content.Load<Texture2D>("mascara_pared");
                //mascarapico = Content.Load<Texture2D>("mascara_pico");
                //mascarapared_left = Content.Load<Texture2D>("mascara_pared_left");
                pico = Content.Load<Texture2D>("pico");
                pausa = Content.Load<Texture2D>("pausa");
                bomba = Content.Load<Texture2D>("bomba");
                lohno = Content.Load<Texture2D>("sprite/ohno");
                //lsplat = Content.Load<Texture2D>("sprite/splat");
                explosion_particle = Content.Load<Texture2D>("sprite/stater");  //stater nice with rotation too
                //lyipie = Content.Load<Texture2D>("sprite/yipie");
                //lglup = Content.Load<Texture2D>("sprite/glub");
                lhiss = Content.Load<Texture2D>("sprite/hiss");
                lchink = Content.Load<Texture2D>("sprite/chink");
                squemado = Content.Load<Texture2D>("quemado");
                songInstance = _music.GetMusic(_currentLevelNumber % 19);
            }
        }
#pragma warning restore S125 // Sections of code should not be commented out

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            dibujaloop++;

            oldK = actK;
            actK = Keyboard.GetState();  // fail level-> no more lemmings or timeout or all blowed (exitBad = true)
            if (Exploding && dibuja3 && !Paused)  //logic explosions particles
            {
                TotalExploding = actItem;
                for (Qexplo = 0; Qexplo < actItem; Qexplo++)
                {
                    TopY = gameResolution.Y;
                    if (earth != null && LevelOn)
                        TopY = earth.Height - 2;
                    NumberAlive = 0;
                    for (Iexplo = 0; Iexplo < PARTICLE_NUM; Iexplo++)
                    {
                        if (Explosion[Qexplo, Iexplo].LifeCtr == -100)
                            NumberAlive++;
                        if (Explosion[Qexplo, Iexplo].LifeCtr > 0)
                        {
                            //this change alpha channel from half life and fade out every particle
                            xx33 = Explosion[Qexplo, Iexplo].LifeCtr;
                            yy33 = Explosion[Qexplo, 0].Counter;
                            xx55 = (xx33 + yy33) / 2;
                            if (yy33 > xx55)
                            {
                                yy33 -= xx55;
                                yy55 = yy33 * 100 / xx55;
                                yy55 *= 2;
                                if (yy55 > 255)
                                    yy55 = 255;
                                Explosion[Qexplo, Iexplo].SetColorA(Convert.ToByte(255 - yy55)); //total alpha - % of death value
                            }
                            //calculate new position
                            Explosion[Qexplo, Iexplo].x += Explosion[Qexplo, Iexplo].dx;
                            Explosion[Qexplo, Iexplo].y += Explosion[Qexplo, Iexplo].dy + Explosion[Qexplo, 0].Counter * GRAVITY;
                            if (Explosion[Qexplo, Iexplo].y > TopY)
                            {
                                //explosion[qexplo, iexplo].y = topY;  //bottom of drawable sets y to max
                                Explosion[Qexplo, Iexplo].LifeCtr = -100;  //bottom of drawable area kills particle
                            }
                            // check life counter
                            if (Explosion[Qexplo, Iexplo].LifeCtr > 0)
                                Explosion[Qexplo, Iexplo].LifeCtr--;
                            if (Explosion[Qexplo, Iexplo].LifeCtr == 0)
                                Explosion[Qexplo, Iexplo].LifeCtr = -100;

                        }
                    }
                    Explosion[Qexplo, 0].Counter++;
                    if (NumberAlive >= PARTICLE_NUM)
                    {
                        TotalExploding--;
                    }
                }
                if (TotalExploding == 0)  // no more particles[0....?][24] are ON
                {
                    Exploding = false;
                    actItem = 0;
                }
            }
            if (!LevelEnded && ((_allBlow && numlemnow == 0) || zvTime < 0 || (numLemmings == Numlems && numlemnow == 0)))
            {
                if (!Paused)
                    rest++;  // var to wait until menu appears gives this way 4 seconds plus more
                if (rest > 190)
                {
                    Exploding = false;
                    actItem = 0;  //see when finish time and are more particles ON
                    LevelEnded = true;
                    Paused = true;
                    if (_numSaved < Lemsneeded)
                        ExitBad = true;
                }
            }
            if (oldK.IsKeyDown(Keys.F1) && actK.IsKeyUp(Keys.F1)) // f1 de-activate debug mode this is only for test BETTER OFF
            {
                debug = !debug;
            }
            else if (oldK.IsKeyDown(Keys.F12) && actK.IsKeyUp(Keys.F12))
            {
                ToggleScale();
            }
            else if (oldK.IsKeyDown(Keys.M) && actK.IsKeyUp(Keys.M) && songInstance != null)
            {
                if (songInstance.State == SoundState.Playing)
                    songInstance.Pause();
                else if (songInstance.State == SoundState.Paused)
                    songInstance.Resume();
            }
            else if (oldK.IsKeyDown(Keys.Left))
            {
                _scrollX -= 5;
                if (oldK.IsKeyDown(Keys.LeftShift) || oldK.IsKeyDown(Keys.RightShift))
                    _scrollX -= 10;
                Scrolling();
            }
            else if (oldK.IsKeyDown(Keys.Right))
            {
                _scrollX += 5;
                if (oldK.IsKeyDown(Keys.LeftShift) || oldK.IsKeyDown(Keys.RightShift))
                    _scrollX += 10;
                Scrolling();
            }
            else if (oldK.IsKeyDown(Keys.Up))
            {
                _scrollY -= 5;
                if (oldK.IsKeyDown(Keys.LeftShift) || oldK.IsKeyDown(Keys.RightShift))
                    _scrollY -= 10;
                Scrolling();
            }
            else if (oldK.IsKeyDown(Keys.Down))
            {
                _scrollY += 5;
                if (oldK.IsKeyDown(Keys.LeftShift) || oldK.IsKeyDown(Keys.RightShift))
                    _scrollY += 10;
                Scrolling();
            }
            else if (oldK.IsKeyDown(Keys.D1))
            {
                _decreaseOn = true;
            }
            else if (oldK.IsKeyUp(Keys.D1))
                _decreaseOn = false;
            else if (oldK.IsKeyDown(Keys.D2))
                _increaseOn = true;
            else if (oldK.IsKeyUp(Keys.D2))
                _increaseOn = false;

            if (_nbClimberRemaining > 0 && oldK.IsKeyDown(Keys.D3) && actK.IsKeyUp(Keys.D3))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.CLIMBER;
            }
            else if (_nbFloaterRemaining > 0 && oldK.IsKeyDown(Keys.D4) && actK.IsKeyUp(Keys.D4))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.FLOATER;
            }
            else if (_nbExploderRemaining > 0 && oldK.IsKeyDown(Keys.D5) && actK.IsKeyUp(Keys.D5))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.EXPLODER;
            }
            else if (_nbBlockerRemaining > 0 && oldK.IsKeyDown(Keys.D6) && actK.IsKeyUp(Keys.D6))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BLOCKER;
            }
            else if (_nbBuilderRemaining > 0 && oldK.IsKeyDown(Keys.D7) && actK.IsKeyUp(Keys.D7))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BUILDER;
            }
            else if (_nbBasherRemaining > 0 && oldK.IsKeyDown(Keys.D8) && actK.IsKeyUp(Keys.D8))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BASHER;
            }
            else if (_nbMinerRemaining > 0 && oldK.IsKeyDown(Keys.D9) && actK.IsKeyUp(Keys.D9))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.MINER;
            }
            else if (_nbDiggerRemaining > 0 && oldK.IsKeyDown(Keys.D0) && actK.IsKeyUp(Keys.D0))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.DIGGER;
            }
            else if (oldK.IsKeyDown(Keys.Escape) && actK.IsKeyUp(Keys.Escape))
            {
                if (MainMenu)
                    Exit();
                if (LevelOn)
                {
                    if (ExitBad && LevelEnded)
                        ExitLevel = true;
                    else if (_numSaved >= Lemsneeded && LevelEnded)
                        ExitLevel = true;
                    else
                    {
                        if (!LevelEnded)
                        {
                            ExitBad = true;
                            LevelEnded = true;
                            Paused = true;
                        }
                        else
                        {
                            Paused = false;
                            LevelEnded = false;
                        }
                    }
                }
            }
            if (((oldK.IsKeyDown(Keys.Enter) && actK.IsKeyUp(Keys.Enter)) ||
                (mouseAntState.RightButton == ButtonState.Released && mouseActState.RightButton == ButtonState.Pressed))
                && LevelEnded)
            {
                ExitLevel = true;
                ExitBad = false;
                _numSaved = 0;
            }
            //take care of numerodentro for the save file when exit with 
            // right button does not save cos if is false always
            if ((mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed) && LevelEnded)
            {
                if (!ExitBad)
                {
                    Paused = false;
                    LevelEnded = false;
                }
                else
                    ExitLevel = true;
                if (_numSaved >= Lemsneeded)
                    ExitLevel = true;
            }
            if (ExitLevel)
            {
                if (_numSaved >= Lemsneeded) //see here if level is finished or not
                {
                    LevelEnd[mmlevchoose] = true;
                    BinaryWriter writer = new(File.Open(fileName, FileMode.Create));
                    for (int i = 0; i < numTotalLevels; i++)
                    {
                        //LevelEnd[za] = false; // first time create all the levels vars to false --> not finished
                        writer.Write(LevelEnd[i]);
                    }
                    writer.Write("(c) 2016 Oskar Oskar LEMMINGS c#. 2023 FilRip from CoolBytes");
                    writer.Close();
                    MustReadFile = true;
                    LevelOn = true;
                    _currentLevelNumber++;
                    if (_currentLevelNumber >= numTotalLevels - 1)
                        _currentLevelNumber = numTotalLevels - 1;
                    mmlevchoose = _currentLevelNumber;
                    MainMenu = false;
                    this.IsMouseVisible = false;
                    _numSaved = 0;
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
                    LoadContent();
                    return; //next level
                }

                if (ExitBad) //repeat level
                {
                    LevelOn = true;
                    MainMenu = false;
                    this.IsMouseVisible = false;
                    _numSaved = 0;
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
                    LoadContent();
                    return;
                }
                songInstance.Stop();
                MainMenu = true;
                LevelOn = false;
                _levelCategory = ELevelCategory.None;
                mmlevchoose = 0;
                this.IsMouseVisible = false; //true without shader
                LevelEnded = false;
                ExitLevel = false;
                _allBlow = false;
                zvTime = 0;
                ExitBad = false;
                numLemmings = 0;
                LoadContent();
                return;
            }

            if (oldK.IsKeyDown(Keys.P) && actK.IsKeyUp(Keys.P))
            {
                PlaySoundMenu();
                Paused = !Paused;
            }
            if (_allBlow && actualBlow < numLemmings) // crash crash TEST TEST
            {
                if (!lemming[actualBlow].Dead && !lemming[actualBlow].Explode)
                    lemming[actualBlow].Exploser = true;
                actualBlow++;
            }
            millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (LevelOn)
                Update_level();
            if (MainMenu)
            {
                Frame2++;
                dibuja = false;
                if (Frame2 > 6)
                {
                    Frame2 = 0;
                    Frame++;
                    dibuja = true;
                }
                if (r1 == 0)
                {
                    r1 = rnd.Next(1, 30);
                }
                if (r2 == 0)
                {
                    r2 = rnd.Next(1, 45);
                }
                if (r3 == 0)
                {
                    r3 = rnd.Next(1, 35);
                }
                if (Frame % r1 == 0 && !blink1on)
                {
                    framblink1 = 0;
                    blink1on = true;
                }  // bbbbbbbbbbbbbbllllllllllllllblinking eyes menu 1-2-3
                if (blink1on && dibuja)
                {
                    framblink1++;
                    if (framblink1 > 8)
                    {
                        blink1on = false;
                        r1 = 0;
                    }
                }
                if (Frame % r2 == 0 && !blink2on)
                {
                    framblink2 = 0;
                    blink2on = true;
                }
                if (blink2on && dibuja)
                {
                    framblink2++;
                    if (framblink2 > 8)
                    {
                        blink2on = false;
                        r2 = 0;
                    }
                }
                if (Frame % r3 == 0 && !blink3on)
                {
                    framblink3 = 0;
                    blink3on = true;
                }
                if (blink3on && dibuja)
                {
                    framblink3++;
                    if (framblink3 > 8)
                    {
                        blink3on = false; r3 = 0;
                    }
                }
                ActualizeMouse();
                if (mmlevchoose != 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
                {
                    LevelOn = true;
                    _currentLevelNumber = mmlevchoose;
                    MainMenu = false;
                    this.IsMouseVisible = false;
                    _numSaved = 0;
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
                    LoadContent();
                }
            }
            // particle test test test right button mouse
            if ((mouseAntState.RightButton == ButtonState.Released && mouseActState.RightButton == ButtonState.Pressed) && !LevelEnded)
            {
                if (particle != null)
                    particle = null;
                else
                {
                    rightparticle = false;
                    rparticle1 = rnd.Next(0, 1);
                    if (rparticle1 == 0)
                        rightparticle = false;
                    else
                        rightparticle = true;
                    particle = new Models.Particles[numParticles];
                    for (varParticle = 0; varParticle < numParticles; varParticle++)
                    {
                        vectorFill.X = rnd.Next(20, 1080);
                        vectorFill.Y = rnd.Next(5, 650) - 660;
                        particle[varParticle].Pos = vectorFill;
                        vectorFill.X = 1;
                        vectorFill.Y = 2;
                        particle[varParticle].Direction = vectorFill;
                        particle[varParticle].Sprite = Content.Load<Texture2D>("sprite/particle");
                        rparticle1 = (float)rnd.NextDouble() * 3;
                        particle[varParticle].DirectionTime = rparticle1;
                    }
                }

            }
            if (particle != null)
            {
                for (varParticle = 0; varParticle < numParticles; varParticle++)
                {
                    particle[varParticle].DirectionTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    particle[varParticle].Lifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    particle[varParticle].Pos += particle[0].Direction;
                    if (rightparticle)
                        particle[varParticle].SetPosX(particle[varParticle].DirectionTime);
                    else
                        particle[varParticle].SetPosX(particle[varParticle].Pos.X - particle[varParticle].DirectionTime);
                    particle[varParticle].SetPosY(particle[varParticle].Pos.Y - (float)rnd.NextDouble());
                    if (particle[varParticle].DirectionTime < 0)
                    {
                        rightparticle = false;
                        rparticle1 = rnd.Next(0, 1);
                        if (rparticle1 == 0)
                            rightparticle = false;
                        else
                            rightparticle = true;
                        rparticle1 = (float)rnd.NextDouble() * 3;
                        particle[varParticle].DirectionTime = rparticle1;
                    }
                    if (particle[varParticle].Pos.Y > gameResolution.Y)
                        particle[varParticle].SetPosY(0);
                }

            }
            base.Update(gameTime);
        }

        private void ToggleScale()
        {
            scaled = !scaled;

            _graphics.PreferredBackBufferWidth = gameResolution.X * (scaled ? 2 : 1);
            _graphics.PreferredBackBufferHeight = gameResolution.Y * (scaled ? 2 : 1);

            _graphics.ApplyChanges();

            renderTargetDestination = GetRenderTargetDestination(gameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(letterboxingColor);

            if (LevelOn)
            {
                _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null, null);
                GraphicsDevice.Clear(Color.Black);  //BACKGROUND COLOR darkslategray,cornblue,dimgray,black,gray,lighslategray
                                                    //draws back image for all the level
                if (particle != null)
                {
                    rectangleFill.X = 0;
                    rectangleFill.Y = 0;
                    rectangleFill.Width = 10;
                    rectangleFill.Height = 10;
                    colorFill.R = 255;
                    colorFill.G = 255;
                    colorFill.B = 255;
                    colorFill.A = 150;
                    for (varParticle = 0; varParticle < numParticles; varParticle++)
                    {
                        _spriteBatch.Draw(particle[varParticle].Sprite, particle[varParticle].Pos, rectangleFill, colorFill, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.50001f);
                    }
                }
                rayLigths = true;
                // logic of background stars moving from -50 to 50
                actWaves333 = 50 * Math.Sin(actWaves / 60);  // 50 height of the wave  // 60 length of it
                actWaves444 = -70 * Math.Sin(actWaves / -80); // 10,100 -70,100
                if (_currentLevelNumber != 159)
                {
                    rectangleFill.X = 0;
                    rectangleFill.Y = 0;
                    rectangleFill.Width = gameResolution.X;
                    rectangleFill.Height = (int)(gameResolution.Y * 0.732);
                    colorFill.R = 150;
                    colorFill.G = 150;
                    colorFill.B = 150;
                    colorFill.A = 160;
                    _spriteBatch.Draw(logo_fondo, rectangleFill, rectangleFill, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
                }
                else
                {
                    Texture2D logo666 = Content.Load<Texture2D>("fondos/star2");
                    rectangleFill.X = 0;
                    rectangleFill.Y = 0;
                    rectangleFill.Width = gameResolution.X;
                    rectangleFill.Height = (int)(gameResolution.Y * 0.732);
                    colorFill.R = 255;
                    colorFill.G = 255;
                    colorFill.B = 255;
                    colorFill.A = 250;
                    rectangleFill2.X = 0 + z1;
                    rectangleFill2.Y = 0 - (int)actWaves333;
                    rectangleFill2.Width = gameResolution.X;
                    rectangleFill2.Height = gameResolution.Y - 188;
                    _spriteBatch.Draw(logo666, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.8091f);
                    Texture2D logo555 = Content.Load<Texture2D>("fondos/ice outttt");
                    rectangleFill2.X = 0 + (int)actWaves444;
                    rectangleFill2.Y = 0 + (int)actWaves444;
                    rectangleFill2.Width = gameResolution.X;
                    rectangleFill2.Height = gameResolution.Y - 188;
                    colorFill.R = 150;
                    colorFill.G = 150;
                    colorFill.B = 150;
                    colorFill.A = 120;
                    _spriteBatch.Draw(logo555, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
                }
                if (TrapsON) //draw traps
                {
                    for (r = 0; r < NumTotTraps; r++)
                    {
                        tYheight = trap[r].sprite.Height / trap[r].numFrames;
                        if (trap[r].type != 555 && trap[r].type != 666)
                        {
                            vv444 = 0;
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
                            rectangleFill.X = trap[r].areaDraw.X - _scrollX;
                            rectangleFill.Y = trap[r].areaDraw.Y - _scrollY;
                            rectangleFill.Width = trap[r].areaDraw.Width;
                            rectangleFill.Height = tYheight;
                            rectangleFill2.X = 0 + vv444;
                            rectangleFill2.Y = tYheight * trap[r].actFrame;
                            rectangleFill2.Width = trap[r].areaDraw.Width;
                            rectangleFill2.Height = tYheight;
                            _spriteBatch.Draw(trap[r].sprite, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, trap[r].depth);
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
                            spY = trap[r].sprite.Height / trap[r].numFrames;
                            rectangleFill.X = (int)trap[r].pos.X - _scrollX - trap[r].vvX;
                            rectangleFill.Y = (int)trap[r].pos.Y - trap[r].vvY - _scrollY;
                            rectangleFill.Width = trap[r].sprite.Width;
                            rectangleFill.Height = spY;
                            rectangleFill2.X = 0;
                            rectangleFill2.Y = spY * trap[r].actFrame;
                            rectangleFill2.Width = trap[r].sprite.Width;
                            rectangleFill2.Height = spY;
                            _spriteBatch.Draw(trap[r].sprite, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, trap[r].depth);
                        }
                        if (debug)
                        {
                            _spriteBatch.Draw(texture1pixel, new Rectangle(trap[r].areaTrap.Left - _scrollX, trap[r].areaTrap.Top - _scrollY, trap[r].areaTrap.Width, trap[r].areaTrap.Height),
                                null, new Color(255, 255, 255, 140), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                        }

                    }
                }
                if (SteelON && debug) // and debug show magenta steel areas
                {
                    for (xz = 0; xz < numTOTsteel; xz++)
                    {
                        rectangleFill.X = steel[xz].area.Left - _scrollX;
                        rectangleFill.Y = steel[xz].area.Top - _scrollY;
                        rectangleFill.Width = steel[xz].area.Width;
                        rectangleFill.Height = steel[xz].area.Height;
                        // magenta r:255,g:0,b:255
                        colorFill.R = 255;
                        colorFill.G = 0;
                        colorFill.B = 255;
                        colorFill.A = 140;
                        _spriteBatch.Draw(texture1pixel, rectangleFill, null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    }
                }
                switch (_currentLevelNumber)  // effect draws water cascade,stars,etc...
                {
                    case 1:
                        _spriteBatch.Draw(agua2, new Rectangle(1560 - _scrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                        rayLigths = false;
                        break;
                    case 4:
                        _spriteBatch.Draw(agua2, new Rectangle(1530 - _scrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f
                        _spriteBatch.Draw(agua2, new Rectangle(1560 - _scrollX, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.803f); //0.802f  
                        rayLigths = false;
                        break;
                    case 5:
                        _spriteBatch.Draw(agua2, new Rectangle(760 - _scrollX, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                        break;
                    case 6:
                        _spriteBatch.Draw(agua2, new Rectangle(2000 - _scrollX, -80, 260, 680), new Rectangle(0 + z3 * 192, 0, 192, 192),
                            new Color(255, 50, 80, 170), 0f, Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f                            
                        break;
                    default:
                        break;
                }

                if (_currentLevelNumber != 159) //nubes clouds moving in background
                {
                    if (rayLigths)
                    {
                        _spriteBatch.Draw(myTexture, new Vector2(gameResolution.X / 2, (gameResolution.Y - 188) / 2), new Rectangle(0, 0, myTexture.Width, myTexture.Height), new Color(255, 255, 255, 10 + Contador * 2),
                            0.4f + Contador2 * 0.001f, new Vector2(myTexture.Width / 2, myTexture.Height / 2), 3f, SpriteEffects.FlipHorizontally, 0.805f); // okokok
                    }
                    // rayligts effect
                    _spriteBatch.Draw(nubes_2, new Rectangle(0, 50 - (int)actWaves444, gameResolution.X, nubes_2.Height), new Rectangle(z1, 0, gameResolution.X, nubes_2.Height),
                        new Color(255, 255, 255, 110), 0f, Vector2.Zero, SpriteEffects.None, 0.804f);

                    _spriteBatch.Draw(nubes, new Rectangle(0, 220, gameResolution.X, nubes.Height), new Rectangle(z2, 0, gameResolution.X, nubes.Height), new Color(255, 255, 255, 110), 0f,
                        Vector2.Zero, SpriteEffects.None, 0.803f);
                }
                _spriteBatch.Draw(earth, new Vector2(0, 0), new Rectangle(_scrollX, _scrollY, gameResolution.X, gameResolution.Y - 188), //512 size of window draw
                    Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.500f);
                if (NumTotArrow > 0)
                {
                    for (xz = 0; xz < NumTotArrow; xz++)
                    {
                        _spriteBatch.Draw(arrow[xz].flechassobre, new Vector2(arrow[xz].area.X - _scrollX, arrow[xz].area.Y - _scrollY),
                            new Rectangle(0, 0, arrow[xz].flechassobre.Width, arrow[xz].flechassobre.Height),
                            new Color(255, 255, 255, arrow[xz].transparency), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.499f);
                    }
                }
                Menu_draw();
                //menu for ending level or not
                if (LevelEnded)
                {
                    if (!_endSongPlayed)
                    {
                        if (songInstance.State == SoundState.Playing)
                            songInstance.Stop();
                        if (ExitBad && _sfx.OhNo.State != SoundState.Playing)
                            _sfx.OhNo.Play();
                        else if (!ExitBad && _music.Music20.State != SoundState.Playing)
                            _music.Music20.Play();
                    }
                    _endSongPlayed = true;
                    colorFill.R = 0; //color.black for this change to see differents options
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 150;
                    _spriteBatch.Draw(texture1pixel, new Rectangle(45, 32, 1005, 600), null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.001f);
                    _spriteBatch.Draw(mainMenuSign2, new Rectangle(-200, -120, 1500, 900), null,
                       Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.00005f);
                    percent = (100 * _numSaved) / _level[mmlevchoose].TotalLemmings;
                    TextLem("All lemmings accounted for:", new Vector2(150, 100), Color.Cyan, 1.5f, 0.0000000001f);
                    TextLem("You rescued " + string.Format("{0}", percent) + "%",
                         new Vector2(270, 160), Color.Violet, 1.5f, 0.0000000001f);
                    percent = (100 * Lemsneeded) / _level[mmlevchoose].TotalLemmings;
                    TextLem("You needed " + string.Format("{0}", percent) + "%",
                         new Vector2(300, 220), Color.DodgerBlue, 1.5f, 0.0000000001f);
                    TextLem("Press <ESC> or <Left Mouse Button>", new Vector2(70, 400), Color.LightCyan, 1.3f, 0.0000000001f);
                    if (ExitBad)
                        TextLem("to retry level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f);
                    else if (_numSaved >= Lemsneeded)
                    {
                        TextLem("to next level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f);
                    }
                    else
                    {
                        TextLem("to continue...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f);
                    }
                    TextLem("Press <Enter> or <Right Mouse Button>", new Vector2(70, 520), Color.Yellow, 1.3f, 0.0000000001f);
                    TextLem("to Main Menu...", new Vector2(100, 560), Color.Yellow, 1.3f, 0.0000000001f);
                }
                xx55 = varDoor[_level[_currentLevelNumber].TypeOfDoor].xWidth;
                yy55 = varDoor[_level[_currentLevelNumber].TypeOfDoor].yWidth;
                framereal565 = (frameDoor * yy55);
                if (sprite != null) //draw sprites
                {
                    for (ssi = 0; ssi < sprite.Length; ssi++)
                    {
                        swidth = sprite[ssi].sprite.Width / sprite[ssi].axisX;
                        sheight = sprite[ssi].sprite.Height / sprite[ssi].axisY;
                        sx1 = 0;
                        sy1 = 0;
                        if (sprite[ssi].actFrame != 0)
                        {
                            sx1 = swidth * (sprite[ssi].actFrame % sprite[ssi].axisX);
                            sy1 = sheight * (sprite[ssi].actFrame / sprite[ssi].axisX);
                        }
                        if (sprite[ssi].typescroll > 0)
                        {
                            sprite[ssi].pos.X -= sprite[ssi].typescroll;
                            if (sprite[ssi].pos.X < 0 - (sprite[ssi].sprite.Width * sprite[ssi].scale))
                                sprite[ssi].pos.X = gameResolution.X;
                            if (sprite[ssi].pos.X > gameResolution.X)
                                sprite[ssi].pos.X = -100;
                            _spriteBatch.Draw(sprite[ssi].sprite, new Vector2(sprite[ssi].pos.X, sprite[ssi].pos.Y - _scrollY),
                                new Rectangle(sx1, sy1, swidth, sheight), new Color(sprite[ssi].R, sprite[ssi].G, sprite[ssi].B, sprite[ssi].transparency),
                                sprite[ssi].rotation, Vector2.Zero, sprite[ssi].scale, SpriteEffects.None, sprite[ssi].depth);
                        }
                        else
                        {
                            if (sprite[ssi].sprite.Name == "touch/arana") // 64x64 sprite frame size
                            {
                                xxAnim = 0;
                                if (sprite[ssi].minusScrollx)
                                {
                                    xxAnim = (int)sprite[ssi].pos.X - _scrollX + 32;
                                }
                                else
                                {
                                    xxAnim = (int)sprite[ssi].pos.X + 32;
                                }
                                _spriteBatch.Draw(sprite[ssi].sprite, new Vector2(xxAnim, sprite[ssi].pos.Y - _scrollY - 32),
                                    new Rectangle(sx1, sy1, swidth, sheight), new Color(sprite[ssi].R, sprite[ssi].G, sprite[ssi].B, sprite[ssi].transparency),
                                    sprite[ssi].rotation, sprite[ssi].center, sprite[ssi].scale, SpriteEffects.None, sprite[ssi].depth);
                            }
                            else
                            {
                                xxAnim = 0;
                                if (sprite[ssi].minusScrollx)
                                {
                                    xxAnim = (int)sprite[ssi].pos.X - _scrollX;
                                }
                                else
                                {
                                    xxAnim = (int)sprite[ssi].pos.X;
                                }
                                _spriteBatch.Draw(sprite[ssi].sprite, new Vector2(xxAnim, sprite[ssi].pos.Y - _scrollY),
                                    new Rectangle(sx1, sy1, swidth, sheight), new Color(sprite[ssi].R, sprite[ssi].G, sprite[ssi].B, sprite[ssi].transparency),
                                    sprite[ssi].rotation, Vector2.Zero, sprite[ssi].scale, SpriteEffects.None, sprite[ssi].depth);
                            }
                        }
                    }
                }
                if (PlatsON)
                {
                    for (int i = 0; i < numTOTplats; i++)
                    {
                        x2 = plats[i].areaDraw.X - plats[i].areaDraw.Width / 2;
                        y = plats[i].areaDraw.Y;
                        w = plats[i].sprite.Width;
                        h = plats[i].sprite.Height;
                        _spriteBatch.Draw(plats[i].sprite, new Rectangle(x2 - _scrollX, y - _scrollY - 5, plats[i].areaDraw.Width, plats[i].areaDraw.Height),
                            new Rectangle(0, 0, w, h), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.56f);
                    }
                }
                if (moreDoors == null)
                {
                    _spriteBatch.Draw(puerta_ani, new Vector2(door1X - _scrollX, door1Y - _scrollY), new Rectangle(0, framereal565, xx55, yy55),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                }
                else
                {
                    for (int i = 0; i < numTOTdoors; i++)
                    {
                        door1X = (int)moreDoors[i].doorMoreXY.X;
                        door1Y = (int)moreDoors[i].doorMoreXY.Y;
                        _spriteBatch.Draw(puerta_ani, new Vector2(door1X - _scrollX, door1Y - _scrollY), new Rectangle(0, framereal565, xx55, yy55),
                            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    }
                }
                xx66 = varExit[_level[_currentLevelNumber].TypeOfExit].xWidth;
                yy66 = varExit[_level[_currentLevelNumber].TypeOfExit].yWidth;
                xx88 = varExit[_level[_currentLevelNumber].TypeOfExit].moreX;
                xx99 = varExit[_level[_currentLevelNumber].TypeOfExit].moreX2;
                yy88 = varExit[_level[_currentLevelNumber].TypeOfExit].moreY;
                yy99 = varExit[_level[_currentLevelNumber].TypeOfExit].moreY2;
                frameact = (frameExit * yy66);
                if (moreexits == null)
                {
                    _spriteBatch.Draw(salida_ani1_1, new Vector2(output1X - _scrollX - xx88, output1Y - yy88 - _scrollY), new Rectangle(0, frameact, xx66, yy66), Color.White,
                        0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    _spriteBatch.Draw(salida_ani1, new Vector2(output1X - _scrollX - xx99, output1Y - yy99 - _scrollY), new Rectangle(0, 0, salida_ani1.Width, salida_ani1.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    if (debug) //exits debug
                    {
                        exit_rect = new Rectangle(output1X - 5, output1Y - 5, 10, 10);
                        _spriteBatch.Draw(texture1pixel, new Rectangle(exit_rect.Left - _scrollX, exit_rect.Top - _scrollY, exit_rect.Width, exit_rect.Height), null,
                            Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    }
                }
                else
                {
                    for (ex22 = 0; ex22 < numTOTexits; ex22++)
                    {
                        output1X = (int)moreexits[ex22].exitMoreXY.X;
                        output1Y = (int)moreexits[ex22].exitMoreXY.Y;
                        _spriteBatch.Draw(salida_ani1_1, new Vector2(output1X - _scrollX - xx88, output1Y - yy88 - _scrollY), new Rectangle(0, frameact, xx66, yy66), Color.White,
                            0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                        _spriteBatch.Draw(salida_ani1, new Vector2(output1X - _scrollX - xx99, output1Y - yy99 - _scrollY), new Rectangle(0, 0, salida_ani1.Width, salida_ani1.Height),
                            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                        if (debug) //exits debug
                        {
                            exit_rect = new Rectangle(output1X - 5, output1Y - 5, 10, 10);
                            _spriteBatch.Draw(texture1pixel, new Rectangle(exit_rect.Left - _scrollX, exit_rect.Top - _scrollY, exit_rect.Width, exit_rect.Height), null,
                                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                        }
                    }
                }
                // infos various for test only
                if (debug)
                {
                    _spriteBatch.DrawString(_fonts.Standard, string.Format("FPS={0}", _fps), new Vector2(960, 50), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                    _spriteBatch.DrawString(_fonts.Standard, strPositionMouse, new Vector2(940, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                }

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
                        crono = (int)(6f - (float)timez);
                        TextLem(string.Format("{0}", crono), new Vector2(lemming[actLEM].PosX + 3 - _scrollX, lemming[actLEM].PosY - 10 - _scrollY), Color.White, 0.4f, 0.000000000004f);
                        if (crono <= 0)
                        {
                            // luto luto sound monogame 3.2 works ok without catch exception
                            if (_sfx.OhNo.State == SoundState.Playing)
                            {
                                _sfx.OhNo.Stop();
                            }
                            try
                            {
                                _sfx.OhNo.Play();
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
                            lemming[actLEM].Numframes = bomber_frames;
                        }
                    }
                    framereal55 = (lemming[actLEM].Actualframe * 118);
                    if (lemming[actLEM].Burned) // scale POSDraw x+0,y+0 at 1.2f x-5,y+0 at 1.35f
                    {
                        _spriteBatch.Draw(squemado, new Vector2(lemming[actLEM].PosX - _scrollX - 5, lemming[actLEM].PosY - _scrollY), new Rectangle(0, lemming[actLEM].Actualframe * 28, 32, 28),
                            (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        _spriteBatch.Draw(lhiss, new Vector2(lemming[actLEM].PosX - _scrollX, lemming[actLEM].PosY - 20 - _scrollY), new Rectangle(0, 0, lhiss.Width, lhiss.Height),
                            Color.White, 0f, Vector2.Zero, (0.5f + (0.01f * lemming[actLEM].Actualframe)), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Drown) // scale POSDraw x+0,y+10 at 1.2f x-8,y+7 at 1.35f  //puto ahoga
                    {
                        _spriteBatch.Draw(_sprites.Drowner, new Vector2(lemming[actLEM].PosX - _scrollX + water_xpos, lemming[actLEM].PosY + water_ypos - _scrollY), new Rectangle(lemming[actLEM].Actualframe * water_with, 0, water_with, water_height),
                            (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, water_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Walker)
                    {
                        framereal55 = (lemming[actLEM].Actualframe * walker_with);
                        _spriteBatch.Draw(_sprites.Walker, new Vector2((lemming[actLEM].PosX - _scrollX + walker_xpos), lemming[actLEM].PosY - _scrollY + walker_ypos), new Rectangle(framereal55, 0, walker_with, walker_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, walker_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Blocker) // blocker scale POSDraw x-5 y+4 at 1.2f x-7 y+1 at 1.35f  //puto
                    {
                        framesale = (lemming[actLEM].Actualframe * blocker_with);
                        _spriteBatch.Draw(_sprites.Blocker, new Vector2(lemming[actLEM].PosX - _scrollX + blocker_xpos, lemming[actLEM].PosY + blocker_ypos - _scrollY), new Rectangle(framesale, 0, blocker_with, blocker_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, blocker_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        if (debug)
                        {
                            bloqueo = new Rectangle(lemming[actLEM].PosX, lemming[actLEM].PosY, 28, 28);
                            _spriteBatch.Draw(texture1pixel, new Rectangle(bloqueo.Left - _scrollX, bloqueo.Top - _scrollY, bloqueo.Width, bloqueo.Height), null,
                                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                        }
                    }
                    if (lemming[actLEM].Bridge) // scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f
                    {
                        framesale = (lemming[actLEM].Actualframe * 26);
                        _spriteBatch.Draw(puente_nomas, new Vector2(lemming[actLEM].PosX - _scrollX - 7, lemming[actLEM].PosY - 7 - _scrollY), new Rectangle(0, framesale, 32, 26), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, (lemming[actLEM].Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Builder)  //scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f  builder builder draws
                    {
                        if (lemming[actLEM].Numstairs >= 10) // chink draws
                        {
                            _spriteBatch.Draw(lchink, new Vector2(lemming[actLEM].PosX - _scrollX - 10, lemming[actLEM].PosY - 30 - _scrollY), new Rectangle(0, 0, lchink.Width, lchink.Height),
                                Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        }
                        framesale = (lemming[actLEM].Actualframe * builder_with);
                        _spriteBatch.Draw(puente, new Vector2(lemming[actLEM].PosX - _scrollX + builder_xpos, lemming[actLEM].PosY + builder_ypos - _scrollY), new Rectangle(framesale, 0, builder_with, builder_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, builder_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Miner)  //scale POSDraw x-5,y-2 at 1.2f x-9,y-7 at 1.35f pico pico miner miner
                    {
                        framesale = (lemming[actLEM].Actualframe * pico_with);
                        _spriteBatch.Draw(pico, new Vector2(lemming[actLEM].PosX - _scrollX + pico_xpos + (lemming[actLEM].Right ? 0 : 10), lemming[actLEM].PosY + pico_ypos - _scrollY), new Rectangle(framesale, 0, pico_with, pico_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, pico_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Basher) //puto
                    {           // scale basher RIGHT POSDRAW x-10,y+4 at 1.2f x-15,y+1 at 1.35f
                        framesale = (lemming[actLEM].Actualframe * basher_with);
                        _spriteBatch.Draw(pared, new Vector2(lemming[actLEM].PosX - _scrollX + (lemming[actLEM].Right ? basher_xpos : basher_xposleft), lemming[actLEM].PosY + basher_ypos - _scrollY), new Rectangle(framesale, 0, basher_with, basher_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, basher_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Explode) // explotando explotando bomber bomber
                    {
                        // bomber scale POSDraw x-5,y+4 at 1.2f x-9,y+2 at 1.35f
                        framesale = (lemming[actLEM].Actualframe * bomber_with);
                        _spriteBatch.Draw(_sprites.Exploder, new Vector2(lemming[actLEM].PosX - _scrollX + bomber_xpos, lemming[actLEM].PosY + bomber_ypos - _scrollY), new Rectangle(framesale, 0, bomber_with, bomber_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, bomber_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        _spriteBatch.Draw(lohno, new Vector2(lemming[actLEM].PosX - _scrollX - 20, lemming[actLEM].PosY - 25 - _scrollY), new Rectangle(0, 0, lohno.Width, lohno.Height),
                            Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Breakfloor) // scale POSDraw x-5,y+4 at 1.2f  x-9,y+2 at 1.35f breakfloor breakfloor
                    {
                        framesale = (lemming[actLEM].Actualframe * floor_with);
                        _spriteBatch.Draw(rompesuelo, new Vector2(lemming[actLEM].PosX - _scrollX + floor_xpos, lemming[actLEM].PosY + floor_ypos - _scrollY), new Rectangle(framesale, 0, floor_with, floor_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, floor_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        if (lemming[actLEM].Actualframe == floor_frames - 1)
                        {
                            lemming[actLEM].Dead = true;
                            numlemnow--;
                            lemming[actLEM].Explode = false;
                            lemming[actLEM].Exploser = false;
                        }
                    }
                    if (lemming[actLEM].Falling) //umbrella paraguas falling with umbrella
                    {
                        if (!lemming[actLEM].Framescut && lemming[actLEM].Actualframe == floater_frames - 1)
                        {
                            lemming[actLEM].Framescut = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = floater_frames - 1 - 4;
                        }
                        if (!lemming[actLEM].Framescut)
                            framesale = (lemming[actLEM].Actualframe * floater_with);
                        else
                            framesale = (lemming[actLEM].Actualframe + 4) * floater_with; // scale floater POSDraw x-5,y-4 at 1.2f x-9,y-7 at 1.35f
                        _spriteBatch.Draw(paraguas, new Vector2(lemming[actLEM].PosX - _scrollX + floater_xpos, lemming[actLEM].PosY + floater_ypos - _scrollY), new Rectangle(framesale, 0, floater_with, floater_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, floater_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Fall) //fall cae
                    {
                        framereal55 = (lemming[actLEM].Actualframe * faller_with);
                        _spriteBatch.Draw(_sprites.Falling, new Vector2(lemming[actLEM].PosX - _scrollX + faller_xpos, lemming[actLEM].PosY - _scrollY + faller_ypos), new Rectangle(framereal55, 0, faller_with, faller_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, faller_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Exit && !lemming[actLEM].Dead) //sale sale exit exit out out
                    {
                        framesale = (lemming[actLEM].Actualframe * sale_with); // exit scale POSDraw  x-1,y+1 at 1.2f x-3,y-1 at 1.35f
                        _spriteBatch.Draw(sale, new Vector2(lemming[actLEM].PosX - _scrollX + sale_xpos, lemming[actLEM].PosY + sale_ypos - _scrollY), new Rectangle(framesale, 0, sale_with, sale_height), Color.White, 0f, Vector2.Zero, sale_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Digger)
                    {
                        framereal55 = (lemming[actLEM].Actualframe * digger_with);
                        _spriteBatch.Draw(_sprites.Digger, new Vector2(lemming[actLEM].PosX - _scrollX + digger_xpos, lemming[actLEM].PosY + 6 - _scrollY + digger_ypos), new Rectangle(framereal55, 0, digger_with, digger_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, digger_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }

                    if (lemming[actLEM].Climbing) // scale POSDraw x-5,y+6 at 1.2f x-8.y+3 at 1.35f  //puto33
                    {
                        framesale = (lemming[actLEM].Actualframe * climber_with);
                        _spriteBatch.Draw(_sprites.Climber, new Vector2(lemming[actLEM].PosX - _scrollX + (lemming[actLEM].Right ? climber_xpos : climber_xposleft), lemming[actLEM].PosY + climber_ypos - _scrollY), new Rectangle(framesale, 0, climber_with, climber_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, climber_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                }
                if (fade)
                {
                    rest++;
                    rest2 = rest * 7;
                    if (rest2 < 70)
                        rest2 = 0;
                    DrawLine(_spriteBatch, new Vector2(0, 0), new Vector2(gameResolution.X, 0), new Color(0, 0, 0, 255 - rest2), gameResolution.Y, 0f);
                    if (Frame > 19)
                    {
                        fade = false;
                        rest = 0;
                        totalTime = 0;
                        if (_sfx.Letsgo.State == SoundState.Stopped && !initON)
                        {
                            _sfx.Letsgo.Play();
                            initON = true;
                        }
                    }

                }
                if (Exploding) // draws explosions particles explosion_particle
                {
                    for (Qexplo = 0; Qexplo < actItem; Qexplo++)
                    {
                        for (Iexplo = 0; Iexplo < PARTICLE_NUM; Iexplo++)
                        {
                            if (Explosion[Qexplo, Iexplo].LifeCtr < 0)
                                continue;

                            vectorFill.X = (float)Explosion[Qexplo, Iexplo].x - _scrollX;
                            vectorFill.Y = (float)Explosion[Qexplo, Iexplo].y - _scrollY;
                            _spriteBatch.Draw(explosion_particle, vectorFill, new Rectangle(0, 0, explosion_particle.Width, explosion_particle.Height), Explosion[Qexplo, Iexplo].Color,
                                Explosion[Qexplo, Iexplo].Rotation, Vector2.Zero, Explosion[Qexplo, Iexplo].Size, SpriteEffects.None, 0.300f);
                            Explosion[Qexplo, Iexplo].Rotation += 0.03f;
                            Explosion[Qexplo, Iexplo].Size += 0.01f;
                        }
                    }

                }
                if (mouseOnLem)
                {
                    LemSkill = "";
                }
                _spriteBatch.Draw((mouseOnLem ? _mouse.MouseOverLemmings : _mouse.MouseCross), new Vector2(mousepos.X, mousepos.Y), new Rectangle(0, 0, 34, 34), Color.White, 0f, Vector2.Zero,
                    1f, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }

            else if (MainMenu)
            {
                if (_music.Music20.State == SoundState.Playing)
                    _music.Music20.Stop();
                // rainbow over lemmings logo text into rendertarget
                GraphicsDevice.SetRenderTarget(colors88);
                GraphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 1.0f, 0);
                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                if (dibujaloop % 5 == 0) //surge-rainbowpic is 45x75 px
                {
                    loopcolor++;
                    if (loopcolor > 44)
                        loopcolor = 1;
                    rainbowpic.SetData(Looplogo, 0, 45 * 75); // init full logo to apply second mask
                    rainbowpic.GetData(0, new Rectangle(loopcolor, 0, 45 - loopcolor, 75), Looplogo2, 0, (45 - loopcolor) * 75);
                    rainbowpic.SetData(0, new Rectangle(0, 0, 45 - loopcolor, 75), Looplogo2, 0, (45 - loopcolor) * 75);

                    rainbowpic.GetData(0, new Rectangle(0, 0, loopcolor, 75), Looplogo2, 0, loopcolor * 75);
                    rainbowpic.SetData(0, new Rectangle(45 - loopcolor, 0, loopcolor, 75), Looplogo2, 0, loopcolor * 75);
                }
                efecto.Parameters["rainbow"].SetValue(rainbowpic); //rainbowpic
                efecto.CurrentTechnique.Passes[0].Apply();
                _spriteBatch.Draw(text, new Vector2(0, 0), Color.White);
                _spriteBatch.End();

                // light NMAP effect over lemmings logo with mouse pos into other rendertarget
                Vector2 cratePosition = new(215, 20);
                // Draw all the normals, in the same place as the textures
                GraphicsDevice.SetRenderTarget(normals);
                GraphicsDevice.Clear(ClearOptions.Target, new Color(128, 128, 255, 255), 1.0f, 0); // Clear the target with the default normal, pointing up (0, 0, 1)
                GraphicsDevice.Clear(ClearOptions.Target,
                    new Color(128, 128, 255, 255), 1.0f, 0); // Clear the target with the default normal, pointing up (0, 0, 1)
                _spriteBatch.Begin();
                _spriteBatch.Draw(crateNormals, cratePosition, Color.White);
                _spriteBatch.End();
                GraphicsDevice.SetRenderTarget(renderTarget);

                //normal target
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null);
                GraphicsDevice.Clear(Color.Black); //new Color(255, 0, 255, 255)
                mmstartx = 5;
                mmstarty = 80;
                mmX = 135;
                x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                Rectangle mm1 = new(mmstartx, mmstarty, mainMenuSign.Width, mainMenuSign.Height);
                Rectangle mm2 = new(mmstartx, mmstarty + 100, mainMenuSign.Width, mainMenuSign.Height);
                Rectangle mm3 = new(mmstartx, mmstarty + 200, mainMenuSign.Width, mainMenuSign.Height);
                Rectangle mm4 = new(mmstartx, mmstarty + 300, mainMenuSign.Width, mainMenuSign.Height);
                Rectangle mm5 = new(mmstartx, mmstarty + 400, mainMenuSign.Width, mainMenuSign.Height);
                Rectangle mm6 = new(mmstartx, mmstarty + 500, mainMenuSign.Width, mainMenuSign.Height);
                if (mm1.Contains(x))
                {
                    _levelCategory = ELevelCategory.Fun;
                    mmlevchoose = 0;
                }
                else if (mm2.Contains(x))
                {
                    _levelCategory = ELevelCategory.Tricky;
                    mmlevchoose = 0;
                }
                else if (mm3.Contains(x))
                {
                    _levelCategory = ELevelCategory.Taxing;
                    mmlevchoose = 0;
                }
                else if (mm4.Contains(x))
                {
                    _levelCategory = ELevelCategory.Mayhem;
                    mmlevchoose = 0;
                }
                else if (mm5.Contains(x))
                {
                    _levelCategory = ELevelCategory.Bonus;
                    mmlevchoose = 0;
                }
                else if (mm6.Contains(x))
                {
                    _levelCategory = ELevelCategory.User;
                    mmlevchoose = 0;
                }
                _spriteBatch.Draw(logo_fondo, new Rectangle(0, 0, gameResolution.X, gameResolution.Y), new Rectangle(0, 0, gameResolution.X, gameResolution.Y), new Color(255, 255, 255, 100));
                if (debug)
                {
                    _spriteBatch.DrawString(_fonts.Standard, string.Format("numero={0}", mmlevchoose), new Vector2(960, 50), Color.White);
                    _spriteBatch.DrawString(_fonts.Standard, strPositionMouse, new Vector2(940, 10), Color.White);
                }
                _spriteBatch.Draw(backlogo, new Vector2(215, 20), Color.White);
                _spriteBatch.Draw(_sprites.EyeBlink1, new Vector2(239, 58), new Rectangle(0, framblink1 * 12, _sprites.EyeBlink1.Width, 12), Color.White,
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
                _spriteBatch.Draw(_sprites.EyeBlink2, new Vector2(463, 58), new Rectangle(0, framblink2 * 12, _sprites.EyeBlink2.Width, 12), Color.White,
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
                _spriteBatch.Draw(_sprites.EyeBlink3, new Vector2(703, 50), new Rectangle(0, framblink3 * 12, _sprites.EyeBlink3.Width, 12), Color.White,
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
                //water effect waves okokok mainMenu
                frameWater++;
                width = 628; // ok for wave wrapping with flatness 50,100
                terrainContour = new int[width];
                float offset = width / 3.0f;
                float flatness = 100; //wave length
                for (xwe = 0; xwe < width; xwe++)
                {
                    double height = peakheight * Math.Sin(xwe / flatness) + offset;
                    terrainContour[xwe] = (int)height;
                }
                Color[] foregroundColors = new Color[width * 512];

                for (xqw = 0; xqw < width; xqw++)
                {
                    for (y = 0; y < 512; y++)
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
                foregroundTexture = new Texture2D(GraphicsDevice, width, 512, false, SurfaceFormat.Color);
                foregroundTexture.SetData(foregroundColors);
                rectangleFill.X = 0;
                rectangleFill.Y = 0;
                rectangleFill.Width = gameResolution.X;
                rectangleFill.Height = gameResolution.Y;
                rectangleFill2.X = 0 + (int)frameWater * 4;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = gameResolution.X;
                rectangleFill2.Height = gameResolution.Y;
                colorFill.R = 255;
                colorFill.G = 255;
                colorFill.B = 255;
                colorFill.A = 100;
                _spriteBatch.Draw(foregroundTexture, rectangleFill, rectangleFill2, colorFill);
                rectangleFill2.X = 0 - (int)frameWater;
                rectangleFill2.Y = 100;
                rectangleFill2.Width = gameResolution.X;
                rectangleFill2.Height = gameResolution.Y;
                colorFill.A = 80;
                _spriteBatch.Draw(foregroundTexture, rectangleFill, rectangleFill2, colorFill); // second wave position depth by order of draw
                if (particle != null)
                {
                    rectangleFill.X = 0;
                    rectangleFill.Y = 0;
                    rectangleFill.Width = 10;
                    rectangleFill.Height = 10;
                    for (varParticle = 0; varParticle < numParticles; varParticle++)
                    {
                        _spriteBatch.Draw(particle[varParticle].Sprite, particle[varParticle].Pos, rectangleFill, Color.Magenta, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.90001f);
                    }
                }
                if (_levelCategory == ELevelCategory.Fun)
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty), new Color(255, 255, 255, 255));
                }
                else
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty), new Color(80, 80, 80, 255));
                }
                if (_levelCategory == ELevelCategory.Tricky)
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 100), new Color(255, 255, 255, 255));
                    _spriteBatch.Draw(ranksign3, new Vector2(mmstartx + 34, mmstarty + 125), new Color(255, 255, 255, 255));
                }
                else
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 100), new Color(80, 80, 80, 255));
                    _spriteBatch.Draw(ranksign3, new Vector2(mmstartx + 34, mmstarty + 125), new Color(80, 80, 80, 255));
                }
                if (_levelCategory == ELevelCategory.Taxing)
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 200), new Color(255, 255, 255, 255));
                    _spriteBatch.Draw(ranksign2, new Vector2(mmstartx + 34, mmstarty + 225), new Color(255, 255, 255, 255));
                }
                else
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 200), new Color(80, 80, 80, 255));
                    _spriteBatch.Draw(ranksign2, new Vector2(mmstartx + 34, mmstarty + 225), new Color(80, 80, 80, 255));
                }
                if (_levelCategory == ELevelCategory.Mayhem)
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 300), new Color(255, 255, 255, 255));
                    _spriteBatch.Draw(ranksign1, new Vector2(mmstartx + 34, mmstarty + 325), new Color(255, 255, 255, 255));
                }
                else
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 300), new Color(80, 80, 80, 255));
                    _spriteBatch.Draw(ranksign1, new Vector2(mmstartx + 34, mmstarty + 325), new Color(80, 80, 80, 255));
                }
                if (_levelCategory == ELevelCategory.Bonus)
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 400), new Color(255, 255, 255, 255));
                    _spriteBatch.Draw(ranksign5, new Vector2(mmstartx + 34, mmstarty + 425), new Color(255, 255, 255, 255));
                }
                else
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 400), new Color(80, 80, 80, 255));
                    _spriteBatch.Draw(ranksign5, new Vector2(mmstartx + 34, mmstarty + 425), new Color(80, 80, 80, 255));
                }
                if (_levelCategory == ELevelCategory.User)
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 500), new Color(255, 255, 255, 255));
                    _spriteBatch.Draw(ranksign6, new Vector2(mmstartx + 34, mmstarty + 525), new Color(255, 255, 255, 255));
                }
                else
                {
                    _spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 500), new Color(80, 80, 80, 255));
                    _spriteBatch.Draw(ranksign6, new Vector2(mmstartx + 34, mmstarty + 525), new Color(80, 80, 80, 255));
                }
                if ((int)_levelCategory <= (int)ELevelCategory.Mayhem && _levelCategory != ELevelCategory.None)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    _spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    _spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 31; s++)
                    {
                        Rectangle mmlev = new(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = s + (30 * (((int)_levelCategory) - 1));
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            _spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                    if (myTexture == null || myTexture.Name != "levels/mini_levels" + ((int)_levelCategory).ToString())
                    {
                        Console.WriteLine("Load texture " + (rnd.Next(65535)).ToString());
                        myTexture = Content.Load<Texture2D>("levels/mini_levels" + ((int)_levelCategory).ToString());
                    }
                    _spriteBatch.Draw(myTexture, new Vector2(mmX, 130), Color.White);
                }
                else if (_levelCategory == ELevelCategory.Bonus)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    _spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    _spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 37; s++)
                    {
                        Rectangle mmlev = new(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = 120 + s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            _spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                    myTexture = Content.Load<Texture2D>("levels/mini_levels5");
                    _spriteBatch.Draw(myTexture, new Vector2(mmX, 130), Color.White);
                }
                else if (_levelCategory == ELevelCategory.User)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    _spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    _spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 26; s++) //number user levels to show okok be careful
                    {
                        Rectangle mmlev = new(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = 156 + s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            _spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                    for (s = 1; s < 26; s++) //number user levels to show okok be careful
                    {
                        myTexture = Content.Load<Texture2D>("levels/user/user" + string.Format("{0,3:D3}", s));
                        _spriteBatch.Draw(myTexture, new Rectangle(mmx, mmy, 130, 55), new Rectangle(0, 0, myTexture.Width, myTexture.Height), Color.White);
                        mmx += 135;
                        if (s % 7 == 0)
                        {
                            mmx = mmX;
                            mmy += 70;
                        }
                    }
                }
                if (mmlevchoose != 0 && mmlevchoose <= numTotalLevels - 1) // MENU SHOW LEVELS DETAILS
                {
                    mmKX = 100;
                    mmKY = 555;
                    mmKplusY = 27;
                    levelACT = mmlevchoose;
                    if ((int)_levelCategory <= (int)ELevelCategory.Mayhem && _levelCategory != ELevelCategory.None)
                        levelACT -= 30 * (((int)_levelCategory) - 1);
                    else if (levelACT > 120 && levelACT <= 156)
                        levelACT -= 120;
                    else if (levelACT > 156)
                        levelACT -= 156;
                    TextLem("Level " + string.Format("{0}", levelACT), new Vector2(mmKX, mmKY), Color.Red, 1f, 0.1f);
                    TextLem(_level[mmlevchoose].nameOfLevel, new Vector2(mmKX + 200, mmKY), Color.Red, 1f, 0.1f);
                    TextLem("Number of Lemmings " + string.Format("{0}", _level[mmlevchoose].TotalLemmings), new Vector2(mmKX, mmKY + mmKplusY), Color.Blue, 1f, 0.1f);
                    TextLem(string.Format("{0}", _level[mmlevchoose].NbLemmingsToSave) + " to be saved", new Vector2(mmKX, mmKY + mmKplusY * 2), Color.Green, 1f, 0.1f);
                    TextLem("Release Rate " + string.Format("{0}", _level[mmlevchoose].MinFrequencyComming), new Vector2(mmKX, mmKY + mmKplusY * 3), Color.Yellow, 1f, 0.1f);
                    TextLem("Time " + string.Format("{0}", _level[mmlevchoose].totalTime) + " Minutes", new Vector2(mmKX, mmKY + mmKplusY * 4), Color.Cyan, 1f, 0.1f);
                    if (_levelCategory == ELevelCategory.Fun)
                    {
                        TextLem("Rating FUN", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    else if (_levelCategory == ELevelCategory.Tricky)
                    {
                        TextLem("Rating TRICKY", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    else if (_levelCategory == ELevelCategory.Taxing)
                    {
                        TextLem("Rating TAXING", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    else if (_levelCategory == ELevelCategory.Mayhem)
                    {
                        TextLem("Rating MAYHEM", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    else if (mmlevchoose > 120 && mmlevchoose <= 156)
                    {
                        TextLem("Rating BONUS", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    else if (mmlevchoose > 156)
                    {
                        TextLem("Rating USER", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    mmKindX = 960;
                    mmKindY = 580;
                    mmPlusy = 15;
                    TextLem("Climbers: " + string.Format("{0}", _level[mmlevchoose].numberClimbers), new Vector2(mmKindX, mmKindY), Color.Linen, 0.5f, 0.1f);
                    TextLem("Floaters: " + string.Format("{0}", _level[mmlevchoose].numberUmbrellas), new Vector2(mmKindX, mmKindY + mmPlusy), Color.LimeGreen, 0.5f, 0.1f);
                    TextLem(" Bombers: " + string.Format("{0}", _level[mmlevchoose].numberExploders), new Vector2(mmKindX, mmKindY + mmPlusy * 2), Color.SteelBlue, 0.5f, 0.1f);
                    TextLem("Blockers: " + string.Format("{0}", _level[mmlevchoose].numberBlockers), new Vector2(mmKindX, mmKindY + mmPlusy * 3), Color.Red, 0.5f, 0.1f);
                    TextLem("Builders: " + string.Format("{0}", _level[mmlevchoose].numberBuilders), new Vector2(mmKindX, mmKindY + mmPlusy * 4), Color.Orange, 0.5f, 0.1f);
                    TextLem(" Bashers: " + string.Format("{0}", _level[mmlevchoose].numberBashers), new Vector2(mmKindX, mmKindY + mmPlusy * 5), Color.Violet, 0.5f, 0.1f);
                    TextLem("  Miners: " + string.Format("{0}", _level[mmlevchoose].numberMiners), new Vector2(mmKindX, mmKindY + mmPlusy * 6), Color.Turquoise, 0.5f, 0.1f);
                    TextLem(" Diggers: " + string.Format("{0}", _level[mmlevchoose].numberDiggers), new Vector2(mmKindX, mmKindY + mmPlusy * 7), Color.Tomato, 0.5f, 0.1f);
                }

                _spriteBatch.Draw(colors88, new Vector2(560, 480), new Rectangle(0, 0, colors88.Width, colors88.Height), Color.White, 0f, Vector2.Zero, .8f,
                    SpriteEffects.None, 0.0001f);
                _spriteBatch.End();

                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                ActualizeMouse();
                _spriteBatch.Draw(_mouse.MouseCross, new Vector2(mousepos.X - 17, mousepos.Y - 17), new Rectangle(0, 0, 34, 34), Color.White, 0f, Vector2.Zero,
                    1f, SpriteEffects.None, 0f);

                _spriteBatch.End();
            }

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(letterboxingColor);

            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget, renderTargetDestination, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

