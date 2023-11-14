using System;
using System.IO;

using Lemmings.NET.Constants;
using Lemmings.NET.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET
{
    public partial class LemmingsNetGame : Game
    {
        Point gameResolution = new Point(1100, 700);
        Color letterboxingColor = new Color(0, 0, 0);
        RenderTarget2D renderTarget;
        Rectangle renderTargetDestination;
        bool scaled;
        private bool _lockMouse;
        private SoundEffectInstance initInstance;
        double actWaves444 = 0, actWaves333 = 0, frameWaves = 0, actWaves = 0;
        private bool initON = false;
        RenderTarget2D colors88, normals;
        Effect lightEffect, efecto;
        Vector3 lightPosition;
        Color lightColor;
        int loopcolor = 0;
        float lightIntensity, lightRadius;
        private Texture2D crateNormals;
        private Texture2D text;
        float peakheight = 25;
        float frameWater = 0;

        RenderTarget2D lighting;
        private Texture2D rainbowpic;
        public Color[] Looplogo { get; set; } = new Color[100 * 100];
        public Color[] Looplogo2 { get; set; } = new Color[100 * 100];

        const string fileName = "LevelStats.txt"; // savegame
        public int Qexplo { get; set; }
        public int Iexplo { get; set; }
        public int Za { get; set; } = 0;
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
        Particles[] particle;
        private SoundEffect init, doorwav, oing, die, song, splat, ohno, explo, chink, strap, sfire, sglug, sting, smousepre, schangeop, winSong;
        private SoundEffectInstance doorInstance, oingInstance, dieInstance, songInstance, splatInstance, ohnoInstance, exploInstance,
            chinkInstance, strapInstance, fireInstance, glugInstance, tingInstance, mousepreInstance, changeopInstance, winSongInstance;
        private bool doorwavOn = false;
        private float rparticle1;
        private bool rightparticle;
        private int numParticles = 300, sx, sy, actualBlow = 0, rest = 0, levelNumber = 1, numTOTdoors = 1, numACTdoor = 0, numTOTexits = 1,
             numTOTsteel = 0, percent = 0, numTOTadds = 0, numTOTplats = 0;
        Random rnd = new Random();
        const int walker_frames = 25, walker_height = 120, walker_with = 4000 / 25, walker_ypos = -15, walker_xpos = -20, walker_framesecond = 3;
        private int walker_frame = 0, builder_frame = 0, builder_frame_second = 1;
        const float walker_size = 0.45f;
        const int blocker_frames = 55, blocker_height = 120, blocker_with = 8800 / 55, blocker_ypos = -15, blocker_xpos = -19;
        const float blocker_size = 0.45f;
        const int basher_frames = 54, basher_height = 120, basher_with = 8640 / basher_frames, basher_ypos = -13, basher_xpos = -25, basher_xposleft = -15;
        const float basher_size = 0.45f;
        const int climber_frames = 24, climber_height = 120, climber_with = 3840 / climber_frames, climber_ypos = -10, climber_xpos = -25, climber_xposleft = -18;
        const float climber_size = 0.45f;
        const int floater_frames = 31, floater_height = 120, floater_with = 4960 / floater_frames, floater_ypos = -30, floater_xpos = -35;
        const float floater_size = 0.65f;
        const int digger_frames = 29, digger_height = 120, digger_with = 4640 / digger_frames, digger_ypos = -18, digger_xpos = -20;
        const float digger_size = 0.45f;
        const int builder_frames = 56, builder_height = 120, builder_with = 8960 / builder_frames, builder_ypos = -16, builder_xpos = -20;
        const float builder_size = 0.45f;
        const int bomber_frames = 47, bomber_height = 120, bomber_with = 7520 / bomber_frames, bomber_ypos = -35, bomber_xpos = -40;
        const float bomber_size = 0.65f;
        const int water_frames = 71, water_height = 120, water_with = 11360 / water_frames, water_ypos = -17, water_xpos = -35;
        const float water_size = 0.65f;
        const int faller_frames = 29, faller_height = 120, faller_with = 4640 / faller_frames, faller_ypos = -15, faller_xpos = -20;
        const float faller_size = 0.45f;
        const int floor_frames = 20, floor_height = 120, floor_with = 3200 / floor_frames, floor_ypos = -28, floor_xpos = -40;
        const float floor_size = 0.65f;
        const int sale_frames = 26, sale_height = 120, sale_with = 4160 / sale_frames, sale_ypos = -35, sale_xpos = -33;
        const float sale_size = 0.65f;
        const int pico_frames = 67, pico_height = 120, pico_with = 10720 / pico_frames, pico_ypos = -13, pico_xpos = -25;
        const float pico_size = 0.45f;
        private bool _decreaseOn, _increaseOn;
        public int Numerodentro { get; set; } = 0;
        public int Contador { get; set; } = 1;
        public int Contador2 { get; set; } = 0;
        public int Frente { get; set; } = 0;
        public int Frente2 { get; set; } = 0;
        public int A { get; set; } = 0;
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
        public int Frec { get; set; } = 10;
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
        Varmoredoors[] moredoors;
        Varmoreexits[] moreexits;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //vita touch textures
        private int maxnumberfalling = 210, useumbrella = 100, NumTotTraps = 0, NumTotArrow = 0, dibujaloop = 1;
        private bool dibuja = true, luzmas = true, luzmas2 = true, dibuja2 = true, dibuja3 = false, dibuja_walker = false, dibuja_builder = false;
        private bool rayLigths;
        private bool mouseOnLem = false, mmop1 = false, mmop2 = false, mmop3 = false, mmop4 = false, mmop5 = false, mmop6 = false;
        private bool fade = true, blink1on = false, blink2on = false, blink3on = false, TrapsON = false, ArrowsON = false, AddsON = false, PlatsON = false;
        private bool SteelON = false;
        private double tiempototal, milisegundos = 0;
        private int Frame = 0;
        private int Frame2 = 0, Frame3 = 0;
        private Texture2D walker2;
        private Texture2D mascaraexplosion, mascarapared, explosion_particle;
        private Texture2D lohno, sahoga, squemado, lhiss, lchink;
        private Texture2D earth;
        private Texture2D foregroundTexture;
        private Texture2D cae;
        private Texture2D digger, mainMenuSign, mainMenuSign2, ranksign1, ranksign2, ranksign3, ranksign5, ranksign6;
        private Texture2D ratonon;
        private Texture2D ratonoff, mas, menos, escala, paraguas, blocker, puente, pausa, explota, pared, pico, bomba, rompesuelo;
        private Texture2D puente_nomas;
        private Texture2D blink1, blink2, blink3;
        private Texture2D myTexture, circulo_led;
        private Texture2D puerta_ani;
        private Texture2D salida_ani1, salida_ani1_1, sale;
        private Texture2D lemfont, backmenu2, backlogo;
        private Texture2D avanzar, cuadrado_menu, logo_fondo, nubes_2, nubes, agua2;
        SpriteFont Font1;
        private string sposicionMouse;
        private int xscroll = 0;  // scroll X of the entire level
        private int yscroll = 0;
        int frameCounter = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;
        Vector2 mousepos = Vector2.Zero;
        Vector2 direction_sprite;
        MouseState mouseActState, mouseAntState;
        float retardoporcien = 1f;
        bool nobasher;
        Rectangle bloqueo, arrowLem;
        Point poslem;
        int[] terrainContour;
        int ssi, px, py, ancho, cantidad, alto, positioYOrig, y55, x55, startposy, framepos, yypos99, cantidad99, yy99, xx99, s, maxluz;
        int maxluz2, xx66, xz, cantidad22, alto66, ancho66, yypos888, yy88, xx88, y4, x4, posy456, posx456, r, actLEM, arriba, abajo, medx, medy, b, ti, pixx;
        int pos_real, wer3, ancho2, alto2, yypos555, yy33, xx33, xEmpty, xErase, valX, valY, y, posi_real, ykk, xkk, abajo2, pixx2, pos_real2, py2, px2, valorx, valory, numSong;
        int varParticle, tYheight, vv444, spY, framereal565, xx55, yy55, swidth, sheight, sx1, sy1, xxAnim, w, h, x2, yy66, frameact, ex22, actLEM2, crono, framereal55, framesale;
        int rest2, mmstartx, mmstarty, mmX, width, xwe, xqw, mmx, mmy, mmKX, mmKY, mmKplusY, levelACT, mmKindX, mmKindY, mmPlusy;
        public void Update_level()
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
            dibuja2 = false;
            dibuja3 = false;
            dibuja_walker = false;
            dibuja_builder = false;
            if (walker_frame > walker_framesecond)
            {
                walker_frame = 0;
                dibuja_walker = true;
            }
            if (builder_frame > builder_frame_second)
            {
                builder_frame = 0;
                dibuja_builder = true;
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
                dibuja2 = true;
            }
            if (frameWaves > Framesecond3)
            {
                frameWaves = 0;
                dibuja3 = true;
                actWaves++;
            } // change add of actwaves to see differences in speed  +=2,+=5
            ActualizarMouse();
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
                for (A = 0; A < numTOTplats; A++)
                {
                    if (plats[A].frame > plats[A].framesecond)
                    {
                        bool goUP = plats[A].up;
                        plats[A].frame = 0;
                        if (goUP)
                            plats[A].actStep++;
                        else
                            plats[A].actStep--;
                        if (goUP)
                            plats[A].areaDraw.Y -= plats[A].step;
                        else
                            plats[A].areaDraw.Y += plats[A].step;
                        if (plats[A].actStep >= plats[A].numSteps - 1)
                            plats[A].up = false;
                        if (plats[A].actStep < 1)
                            plats[A].up = true;
                        px = plats[A].areaDraw.X - (plats[A].areaDraw.Width / 2);
                        py = plats[A].areaDraw.Y;
                        ancho = plats[A].areaDraw.Width;
                        cantidad = ancho * 1; // *height
                        alto = plats[A].step * plats[A].numSteps;
                        positioYOrig = plats[A].areaDraw.Y + (plats[A].actStep * plats[A].step);
                        bool realLine = false;
                        for (y55 = 0; y55 < alto; y55++)
                        {
                            for (x55 = 0; x55 < plats[A].areaDraw.Width; x55++)
                            {
                                if (y55 == (alto - 1) - plats[A].actStep * plats[A].step)
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
                    plats[A].frame++;
                }
            }

            if (AddsON && !Paused)
            {
                startposy = adds[0].sprite.Height / adds[0].numFrames; // height of each frame inside the whole sprite
                framepos = startposy * adds[0].actFrame; // actual y position of the frame
                ancho = adds[0].sprite.Width;
                cantidad = ancho * startposy; // height frame
                rectangleFill.X = 0;
                rectangleFill.Y = framepos;
                rectangleFill.Width = ancho;
                rectangleFill.Height = startposy;
                adds[0].sprite.GetData(0, rectangleFill, Colormask22, 0, cantidad);
                rectangleFill.X = adds[0].areaDraw.X;
                rectangleFill.Y = adds[0].areaDraw.Y;
                rectangleFill.Width = ancho;
                rectangleFill.Height = startposy;
                earth.SetData(0, rectangleFill, Colormask22, 0, cantidad);
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
            tiempototal = Contadortime / 60; //real time of the level see to stop when finish or zvtime<0
            if (puertaon)
            {
                Contadortime = 0;
                tiempototal = 0;
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
                xx66 = varExit[level[levelNumber].typeOfExit].numFram - 1;
                framesalida++;
                if (framesalida > xx66)
                {
                    framesalida = 0;
                }
            }
            if (!Paused)
                Puerta();
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
                xscroll + gameResolution.X < earth.Width)
            {
                xscroll += 5;
            }
            if (xscroll + gameResolution.X > earth.Width)
            {
                xscroll = earth.Width - gameResolution.X;
            }
            if (mousepos.X < -10 && xscroll > 0)
            {
                xscroll -= 5;
            }
            if (xscroll < 0)
            {
                xscroll = 0;
            }
            if (mousepos.Y + 20 > gameResolution.Y && yscroll + 512 < earth.Height)
            {
                yscroll += 5;
            }
            if (yscroll + 512 > earth.Height)
            {
                yscroll = earth.Height - 512;
            }
            if (mousepos.Y < -10 && yscroll > 0)
            {
                yscroll -= 5;
            }
            if (yscroll < 0)
            {
                yscroll = 0;
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
                Mouse.SetPosition((int)mousepos.X, (int)mousepos.Y); // setposition //this is for my son kids don't know move mouse so good  
        }

        public void MoverLemming() //lemmings logic called every update
        {
            mouseOnLem = false;  // scroll mouse on level landscape
            Scrolling();
            for (actLEM = 0; actLEM < numerosaca; actLEM++)
            {
                if (puertaon)
                    break; // start when door finish opening
                if (lemming[actLEM].Dead)
                    continue;
                // LOGIC BLOCKER BLOCKER BLOQUEO LOGIC bbbbbbbbbbbbbbbbbbbbllllllllloooooooccccccccccckkkkkkkkkkkeeeeeeeeeeeeedddddddddddddddddd
                medx = 14;
                medy = 14;
                for (b = 0; b < numerosaca; b++)
                {
                    if (lemming[b].Blocker && b != actLEM)
                    {
                        bloqueo.X = lemming[b].Posx;
                        bloqueo.Y = lemming[b].Posy;
                        bloqueo.Width = 28;
                        bloqueo.Height = 28;
                        if (lemming[actLEM].Miner)
                        {
                            bloqueo.X = lemming[b].Posx + 10;
                            bloqueo.Y = lemming[b].Posy;
                            bloqueo.Width = 9;
                            bloqueo.Height = 28;
                        }
                        poslem.X = lemming[actLEM].Posx + medx;
                        poslem.Y = lemming[actLEM].Posy + medy;
                        if (bloqueo.Contains(poslem))
                        {
                            if (lemming[actLEM].Right)
                            {
                                if (lemming[actLEM].Posx < lemming[b].Posx)
                                {
                                    lemming[actLEM].Right = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (lemming[actLEM].Posx > lemming[b].Posx - 1)
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
                if ((mousepos.X + 16 >= lemming[actLEM].Posx - xscroll && mousepos.X + 16 <= lemming[actLEM].Posx - xscroll + 28
                        && mousepos.Y + 16 >= lemming[actLEM].Posy - yscroll && mousepos.Y + 16 <= lemming[actLEM].Posy + 28 - yscroll) && !mouseOnLem)
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
                    if (lemming[actLEM].Escalar)
                        LemSkill += ",C";
                    if (lemming[actLEM].Umbrella)
                        LemSkill += ",F";
                    if (lemming[actLEM].Escalando)
                        LemSkill = "Climber";
                    if (lemming[actLEM].Escalando && lemming[actLEM].Umbrella)
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
                        x.X = lemming[actLEM].Posx + 14;
                        x.Y = lemming[actLEM].Posy + 25;
                        if (trap[ti].areaTrap.Contains(x) && !trap[ti].isOn && trap[ti].type == 666)
                        {
                            trap[ti].isOn = true;
                            lemming[actLEM].Activo = false;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Dead = true;
                            numlemnow--;
                            lemming[actLEM].Explotando = false;
                            lemming[actLEM].Explota = false;
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
                                    if (dieInstance.State == SoundState.Playing)
                                    {
                                        dieInstance.Stop();
                                    }
                                    try
                                    {
                                        dieInstance.Play();
                                    }
                                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                                    break;
                            }
                            break;
                        }
                        rectangleFill.X = lemming[actLEM].Posx + 14;
                        rectangleFill.Y = lemming[actLEM].Posy;
                        rectangleFill.Width = 1;
                        rectangleFill.Height = 28;
                        if (trap[ti].areaTrap.Intersects(rectangleFill) && !lemming[actLEM].Quemado && !lemming[actLEM].Ahoga && trap[ti].type != 666)
                        {
                            switch (trap[ti].sprite.Name)
                            {
                                case "traps/dead_spin":
                                case "traps/fuego1":
                                case "traps/fuego2":
                                case "traps/fuego3":
                                case "traps/fuego4":
                                    if (fireInstance.State == SoundState.Playing)
                                    {
                                        fireInstance.Stop();
                                    }
                                    try
                                    {
                                        fireInstance.Play();
                                    }
                                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                                    lemming[actLEM].Quemado = true;
                                    lemming[actLEM].Ahoga = false;
                                    lemming[actLEM].Explotando = false;
                                    lemming[actLEM].Explota = false;
                                    lemming[actLEM].Numframes = 14;
                                    lemming[actLEM].Actualframe = 0;
                                    lemming[actLEM].Activo = false;
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
                                    if (glugInstance.State == SoundState.Playing)
                                    {
                                        glugInstance.Stop();
                                    }
                                    try
                                    {
                                        glugInstance.Play();
                                    }
                                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                                    lemming[actLEM].Ahoga = true;
                                    lemming[actLEM].Quemado = false;
                                    lemming[actLEM].Explotando = false;
                                    lemming[actLEM].Explota = false;
                                    lemming[actLEM].Falling = false;
                                    lemming[actLEM].Fall = false;
                                    lemming[actLEM].Numframes = water_frames;
                                    lemming[actLEM].Actualframe = 0;
                                    lemming[actLEM].Activo = false;
                                    lemming[actLEM].Walker = false;
                                    break;
                                default:
                                    if (dieInstance.State == SoundState.Playing)
                                    {
                                        dieInstance.Stop();
                                    }
                                    try
                                    {
                                        dieInstance.Play();
                                    }
                                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                                    lemming[actLEM].Explotando = false;
                                    lemming[actLEM].Explota = false;
                                    lemming[actLEM].Activo = false;
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
                        numerocavan--;
                        if (numerocavan < 0)
                        {
                            numerocavan = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
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
                    if (_currentSelectedSkill == ECurrentSkill.CLIMBER && lemming[actLEM].Onmouse && !lemming[actLEM].Escalar) //CLIMBER
                    {
                        numeroescalan--;
                        if (numeroescalan < 0)
                        {
                            numeroescalan = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
                            lemming[actLEM].Escalar = true;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.FLOATER && lemming[actLEM].Onmouse && !lemming[actLEM].Umbrella && !lemming[actLEM].Breakfloor) //FLOATER
                    {
                        numeroparaguas--;
                        if (numeroparaguas < 0)
                        {
                            numeroparaguas = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
                            lemming[actLEM].Umbrella = true;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.EXPLODER && lemming[actLEM].Onmouse && !lemming[actLEM].Explota) //BOMBER
                    {
                        numeroexplotan--;
                        if (numeroexplotan < 0)
                        {
                            numeroexplotan = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
                            lemming[actLEM].Explota = true;
                            continue;
                        }
                    }
                    if (_currentSelectedSkill == ECurrentSkill.BLOCKER && lemming[actLEM].Onmouse && !lemming[actLEM].Blocker //BLOCKER
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Builder || lemming[actLEM].Basher || lemming[actLEM].Miner))
                    {
                        numeroblockers--;
                        if (numeroblockers < 0)
                        {
                            numeroblockers = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
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
                        && (lemming[actLEM].Walker || lemming[actLEM].Digger || lemming[actLEM].Basher || lemming[actLEM].Miner || lemming[actLEM].Puentenomas))
                    {
                        numeropuentes--;
                        if (numeropuentes < 0)
                        {
                            numeropuentes = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
                            lemming[actLEM].Puentenomas = false;
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
                        numeropared--;
                        if (numeropared < 0)
                        {
                            numeropared = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
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
                        numeropico--;
                        if (numeropico < 0)
                        {
                            numeropico = 0;
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                        }
                        else
                        {
                            if (mousepreInstance.State == SoundState.Playing)
                            {
                                mousepreInstance.Stop();
                            }
                            mousepreInstance.Play();
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
                if (dibuja_builder && lemming[actLEM].Builder)
                {
                    lemming[actLEM].Actualframe++;
                    if (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1 && !lemming[actLEM].Explotando)
                    {
                        lemming[actLEM].Actualframe = 0;
                    }
                }
                if (dibuja_walker && !lemming[actLEM].Builder && !lemming[actLEM].Basher && !lemming[actLEM].Miner
                    && !lemming[actLEM].Quemado && !lemming[actLEM].Ahoga)
                {
                    lemming[actLEM].Actualframe++;
                    if (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1 && !lemming[actLEM].Explotando)
                    {
                        lemming[actLEM].Actualframe = 0;
                    }
                    //be carefull with bomber frames actualization
                }
                if (dibuja2 && (lemming[actLEM].Basher || lemming[actLEM].Miner
                    || lemming[actLEM].Quemado || lemming[actLEM].Ahoga)) // see careful frames
                {
                    lemming[actLEM].Actualframe++;
                    if ((lemming[actLEM].Quemado || lemming[actLEM].Ahoga) &&
                        (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1))
                    {
                        lemming[actLEM].Quemado = false;
                        lemming[actLEM].Ahoga = false;
                        lemming[actLEM].Dead = true;
                        lemming[actLEM].Explotando = false;
                        lemming[actLEM].Explota = false;
                        numlemnow--;
                    }
                    if (lemming[actLEM].Actualframe > lemming[actLEM].Numframes - 1 && !lemming[actLEM].Explotando)
                    {
                        lemming[actLEM].Actualframe = 0;
                    }
                }
                if (lemming[actLEM].Exit)
                {
                    if (lemming[actLEM].Actualframe == lemming[actLEM].Numframes - 1)
                    {
                        lemming[actLEM].Dead = true;
                        lemming[actLEM].Explotando = false;
                        lemming[actLEM].Explota = false;
                        numlemnow--;
                        Numerodentro++;  // here is where the lemming go inside after door animation
                    }
                    continue;
                }
                arriba = 0;
                abajo = 0;
                pixx = lemming[actLEM].Posx + medx;
                ancho = earth.Width;
                for (x55 = 0; x55 <= 8; x55++)
                {
                    pos_real = lemming[actLEM].Posy + x55 + medy + medy;  ///////////// pixel por debajo -> beneath.............
                    if (pos_real == earth.Height)
                    {
                        abajo = 9;
                        break;
                    }
                    if (pos_real < 0)
                        pos_real = 0;
                    if (pos_real > earth.Height)
                    {
                        lemming[actLEM].Dead = true;
                        abajo = 9;
                        lemming[actLEM].Activo = false;
                        numlemnow--;
                        lemming[actLEM].Explotando = false;
                        lemming[actLEM].Explota = false;
                        if (dieInstance.State == SoundState.Playing)
                        {
                            dieInstance.Stop();
                        }
                        try
                        {
                            dieInstance.Play();
                        }
                        catch (InstancePlayLimitException) { /* Ignore errors */ }
                        break;
                    }
                    if (C25[(pos_real * earth.Width) + pixx].R == 0 && C25[(pos_real * earth.Width) + pixx].G == 0 && C25[(pos_real * earth.Width) + pixx].B == 0)
                    {
                        abajo++;
                    }
                    else
                    {
                        break;
                    }
                }
                // very important to check digger and miner before change to falling
                if (lemming[actLEM].Pixelscaida > useumbrella && !lemming[actLEM].Falling && lemming[actLEM].Umbrella
                    && (!lemming[actLEM].Digger && !lemming[actLEM].Miner && !lemming[actLEM].Builder) && lemming[actLEM].Activo)
                {
                    lemming[actLEM].Pixelscaida = 11;
                    lemming[actLEM].Falling = true;
                    lemming[actLEM].Fall = false;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = floater_frames;
                }
                if ((abajo > 8 && !lemming[actLEM].Fall && (!lemming[actLEM].Digger || !lemming[actLEM].Miner)) && !lemming[actLEM].Falling
                    && !lemming[actLEM].Explotando && lemming[actLEM].Activo)
                {
                    lemming[actLEM].Fall = true;
                    lemming[actLEM].Pixelscaida = 0;
                    lemming[actLEM].Escalando = false;
                    lemming[actLEM].Walker = false;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = faller_frames;
                    lemming[actLEM].Basher = false;
                    lemming[actLEM].Builder = false;
                    lemming[actLEM].Puentenomas = false;
                    lemming[actLEM].Miner = false;
                    continue; // lemming fall when there's no floor on feet and fall down
                }
                if ((abajo == 0) && (lemming[actLEM].Fall || lemming[actLEM].Falling) && (!lemming[actLEM].Digger && !lemming[actLEM].Miner)) //OJO LOCO A VECES AL CAVAR Y SIGUE WALKER
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
                        if (splatInstance.State == SoundState.Playing)
                        {
                            splatInstance.Stop();
                        }
                        try
                        {
                            splatInstance.Play();
                        }
                        catch (InstancePlayLimitException) { /* Ignore errors */ }
                        lemming[actLEM].Fall = false;
                        lemming[actLEM].Walker = false;
                        lemming[actLEM].Falling = false;
                        lemming[actLEM].Explotando = false;
                        lemming[actLEM].Explota = false;
                        lemming[actLEM].Activo = false;
                        lemming[actLEM].Breakfloor = true;
                        lemming[actLEM].Umbrella = false;
                        lemming[actLEM].Numframes = floor_frames;
                        lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
                if ((abajo == 0) && lemming[actLEM].Walker && (!lemming[actLEM].Digger && !lemming[actLEM].Miner))
                {
                    lemming[actLEM].Pixelscaida = 0;
                }
                for (x55 = 0; x55 <= 20; x55++)
                {
                    pos_real = lemming[actLEM].Posy + medy + medy - x55;
                    if (pos_real == earth.Height)    // rompe los calculos si sale de la pantalla o se cuelga AARRIBBBAAAA
                    {
                        lemming[actLEM].Activo = false;
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
                if (lemming[actLEM].Blocker && abajo > 2)
                {
                    lemming[actLEM].Blocker = false;
                    lemming[actLEM].Fall = true;
                    lemming[actLEM].Pixelscaida = 0;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = faller_frames;
                    continue;
                }
                if (lemming[actLEM].Miner && dibuja2 && lemming[actLEM].Actualframe == 42)  // miner logic pico logic
                {
                    if (ArrowsON) // miner arrows logic areaTrap Intersects
                    {
                        bool nominer = false;
                        arrowLem.X = lemming[actLEM].Posx;
                        arrowLem.Y = lemming[actLEM].Posy;
                        arrowLem.Width = 28;
                        arrowLem.Height = 28;
                        for (wer3 = 0; wer3 < NumTotArrow; wer3++)
                        {
                            if (arrow[wer3].area.Intersects(arrowLem) && lemming[actLEM].Right && !arrow[wer3].right)
                            {
                                nominer = true;
                                continue;
                            }
                            if (arrow[wer3].area.Intersects(arrowLem) && !lemming[actLEM].Right && arrow[wer3].right)
                            {
                                nominer = true;
                            }
                        }
                        if (nominer)
                        {
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = walker_frames;
                            continue;
                        }
                    }
                    if (lemming[actLEM].Right)
                    {
                        ancho2 = 20;
                        alto2 = 20;
                        px = lemming[actLEM].Posx + 12;
                        py = lemming[actLEM].Posy + 14;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + ancho2 >= earth.Width)
                        {
                            ancho2 = earth.Width - px;
                        }
                        if (py + alto2 >= earth.Height)
                        {
                            alto2 = earth.Height - py;
                        }
                        cantidad = ancho2 * alto2;
                        mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                        cantidad = 0;
                        yypos888 = 0;
                        yy88 = 0;
                        xx88 = 0;
                        for (yy88 = 0; yy88 < alto2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < ancho2; xx88++)
                            {
                                Colorsobre2[cantidad].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                cantidad++;
                            }
                        }
                        for (r = 0; r < cantidad; r++)
                        {
                            if (SteelON)
                            {
                                sx = r % ancho2;
                                sy = r / ancho2;
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
                        rectangleFill.Width = ancho2;
                        rectangleFill.Height = alto2;
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, cantidad);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        cantidad = 0;
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < alto2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < ancho2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[cantidad].PackedValue;
                                cantidad++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        lemming[actLEM].Posx += 12;
                        lemming[actLEM].Posy++;
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
                        ancho2 = 20;
                        alto2 = 20;
                        px = lemming[actLEM].Posx - 4;
                        if (px < 0)
                        {
                            px = 0;
                        }
                        py = lemming[actLEM].Posy + 14;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + ancho2 >= earth.Width)
                        {
                            ancho2 = earth.Width - px;
                        }
                        if (py + alto2 >= earth.Height)
                        {
                            alto2 = earth.Height - py;
                        }
                        cantidad = ancho2 * alto2;
                        mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                        cantidad = 0; yypos888 = 0; yy88 = 0; xx88 = 0;
                        for (yy88 = 0; yy88 < alto2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < ancho2; xx88++)
                            {
                                Colorsobre2[cantidad].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                cantidad++;
                            }
                        }
                        for (r = 0; r < cantidad; r++)
                        {
                            if (SteelON)
                            {
                                sx = r % ancho2;
                                sy = r / ancho2;
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
                            if (Colorsobre2[cantidad - 1 - r].R > 0 || Colorsobre2[cantidad - 1 - r].G > 0 || Colorsobre2[cantidad - 1 - r].B > 0)
                            {
                                Frente2++;
                            }
                            if (Colormask2[cantidad - 1 - r].R > 0 || Colormask2[cantidad - 1 - r].G > 0 || Colormask2[cantidad - 1 - r].B > 0)
                            {
                                Colorsobre2[r].PackedValue = 0;
                            }
                        }
                        rectangleFill.X = px;
                        rectangleFill.Y = py;
                        rectangleFill.Width = ancho2;
                        rectangleFill.Height = alto2;
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, cantidad);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        cantidad = 0;
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < alto2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < ancho2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[cantidad].PackedValue;
                                cantidad++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        lemming[actLEM].Posx -= 12;
                        lemming[actLEM].Posy++;
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

                if (lemming[actLEM].Basher && (lemming[actLEM].Actualframe == 10 || lemming[actLEM].Actualframe == 37) && dibuja2)
                {
                    if (ArrowsON) // basher arrows logic areaTrap Intersects
                    {
                        nobasher = false;
                        arrowLem.X = lemming[actLEM].Posx;
                        arrowLem.Y = lemming[actLEM].Posy;
                        arrowLem.Width = 28;
                        arrowLem.Height = 28;
                        for (wer3 = 0; wer3 < NumTotArrow; wer3++)
                        {
                            if (arrow[wer3].area.Intersects(arrowLem) && lemming[actLEM].Right && !arrow[wer3].right)
                            {
                                nobasher = true;
                                continue;
                            }
                            if (arrow[wer3].area.Intersects(arrowLem) && !lemming[actLEM].Right && arrow[wer3].right)
                            {
                                nobasher = true;
                            }
                        }
                        if (nobasher)
                        {
                            if (tingInstance.State == SoundState.Playing)
                            {
                                tingInstance.Stop();
                            }
                            tingInstance.Play();
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = walker_frames;
                            continue;
                        }
                    }
                    if (lemming[actLEM].Right)
                    {
                        ancho2 = 20;
                        alto2 = 20;
                        px = lemming[actLEM].Posx + 14;
                        py = lemming[actLEM].Posy + 8;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + ancho2 >= earth.Width)
                        {
                            ancho2 = earth.Width - px;
                        }
                        if (py + alto2 >= earth.Height)
                        {
                            alto2 = earth.Height - py;
                        }
                        cantidad = ancho2 * alto2;
                        mascarapared.GetData(Colormask2);
                        //////// optimized for hd3000 laptop
                        cantidad = 0;
                        yypos888 = 0;
                        yy88 = 0;
                        xx88 = 0;
                        for (yy88 = 0; yy88 < alto2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < ancho2; xx88++)
                            {
                                Colorsobre2[cantidad].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                cantidad++;
                            }
                        }
                        xEmpty = 0;
                        xErase = ancho2;
                        Frente = 0;
                        sx = 0;
                        for (valX = 0; valX < ancho2; valX++)
                        {
                            Frente = 0;
                            for (valY = 0; valY < alto2; valY++)
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
                                if ((Colormask2[(valY * ancho2) + valX].R > 0 || Colormask2[(valY * ancho2) + valX].G > 0 || Colormask2[(valY * ancho2) + valX].B > 0) &&
                                    (Colorsobre2[(valY * ancho2) + valX].R > 0 || Colorsobre2[(valY * ancho2) + valX].G > 0 || Colorsobre2[(valY * ancho2) + valX].B > 0))
                                {
                                    Colorsobre2[(valY * ancho2) + valX].PackedValue = 0;
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
                        rectangleFill.Width = ancho2;
                        rectangleFill.Height = alto2;
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, cantidad);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        cantidad = 0;
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < alto2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < ancho2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[cantidad].PackedValue;
                                cantidad++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        if (xEmpty < xErase)
                            lemming[actLEM].Posx += 14;
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
                        ancho2 = 20;
                        alto2 = 20;
                        px = lemming[actLEM].Posx - 5;
                        if (px < 0)
                        {
                            px = 0;
                        }
                        py = lemming[actLEM].Posy + 8;
                        if (py < 0) // top of the level
                        {
                            py = 0;
                        }
                        if (px < 0) // left of the level
                        {
                            px = 0;
                        }
                        if (px + ancho2 >= earth.Width)
                        {
                            ancho2 = earth.Width - px;
                        }
                        if (py + alto2 >= earth.Height)
                        {
                            alto2 = earth.Height - py;
                        }
                        cantidad = ancho2 * alto2;
                        //////// optimized for hd3000 laptop
                        cantidad = 0;
                        yypos888 = 0;
                        yy88 = 0;
                        xx88 = 0;
                        for (yy88 = 0; yy88 < alto2; yy88++)
                        {
                            yypos888 = (yy88 + py) * earth.Width;
                            for (xx88 = 0; xx88 < ancho2; xx88++)
                            {
                                Colorsobre2[cantidad].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                                cantidad++;
                            }
                        }
                        xEmpty = ancho2;
                        xErase = 0;
                        Frente = 0;
                        sx = 0;
                        for (valX = ancho2 - 1; valX >= 0; valX--)
                        {
                            Frente = 0;
                            for (valY = 0; valY < alto2; valY++)
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
                                if ((Colormask2[valY * ancho2 + valX].R > 0 || Colormask2[valY * ancho2 + valX].G > 0 || Colormask2[valY * ancho2 + valX].B > 0) &&
                                    (Colorsobre2[valY * ancho2 + valX].R > 0 || Colorsobre2[valY * ancho2 + valX].G > 0 || Colorsobre2[valY * ancho2 + valX].B > 0))
                                {
                                    Colorsobre2[valY * ancho2 + valX].PackedValue = 0;
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
                        rectangleFill.Width = ancho2;
                        rectangleFill.Height = alto2;
                        earth.SetData(0, rectangleFill, Colorsobre2, 0, cantidad);
                        // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                        cantidad = 0;
                        yypos555 = 0;
                        yy33 = 0;
                        xx33 = 0;
                        for (yy33 = 0; yy33 < alto2; yy33++)
                        {
                            yypos555 = (yy33 + py) * earth.Width;
                            for (xx33 = 0; xx33 < ancho2; xx33++)
                            {
                                C25[yypos555 + px + xx33].PackedValue = Colorsobre2[cantidad].PackedValue;
                                cantidad++;
                            }
                        }
                        if (sx == -777)
                            continue;
                        if (xEmpty > xErase)
                            lemming[actLEM].Posx -= 14;
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
                if (lemming[actLEM].Basher && abajo > 3)
                {
                    lemming[actLEM].Basher = false;
                    lemming[actLEM].Walker = true;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = walker_frames;
                    continue;
                }
                if (lemming[actLEM].Builder && dibuja_builder) // BUILDER LOGIC HERE chink sound see limits tooo FIX FIX FIX
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
                                lemming[actLEM].Posy += 6;
                                lemming[actLEM].Posx -= 14;
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].Right = false;
                                continue;

                            }
                            if (lemming[actLEM].Posy < -24) //see ok was -24 but sometimes fails the u-turn
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].Posy += 3;
                                lemming[actLEM].Posx -= 6;
                                continue;
                            }
                            for (y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                posi_real = (lemming[actLEM].Posy + 24 + y) * earth.Width + lemming[actLEM].Posx;
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
                            lemming[actLEM].Posy -= 3;
                            lemming[actLEM].Posx += 6;
                            if (lemming[actLEM].Numstairs >= 10)
                            {
                                if (chinkInstance.State == SoundState.Playing)
                                {
                                    chinkInstance.Stop();
                                }
                                chinkInstance.Play();
                            }
                            cantidad = 0;
                            for (ykk = 27; ykk < 31; ykk++)
                            {
                                posi_real = (lemming[actLEM].Posy + ykk) * earth.Width + lemming[actLEM].Posx;
                                for (xkk = 0; xkk < 28; xkk++)
                                {
                                    Colormask22[cantidad] = C25[posi_real + xkk];
                                    cantidad++;
                                }
                            }
                            rectangleFill.X = lemming[actLEM].Posx;
                            rectangleFill.Y = lemming[actLEM].Posy + 27;
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
                                lemming[actLEM].Posx -= 7;
                                lemming[actLEM].Right = false;
                            }
                            continue;
                        }
                        else
                        {
                            if (arriba > 1)
                            {
                                lemming[actLEM].Posy += 6;
                                lemming[actLEM].Posx += 15;
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].Numstairs = 0;
                                lemming[actLEM].Right = true;
                                continue;

                            }
                            if (lemming[actLEM].Posy < -24) //see ok was -24
                            {
                                lemming[actLEM].Builder = false;
                                lemming[actLEM].Walker = true;
                                lemming[actLEM].Actualframe = 0;
                                lemming[actLEM].Numframes = walker_frames;
                                lemming[actLEM].Posy += 3;
                                lemming[actLEM].Posx += 6;
                                continue;
                            }
                            for (y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                posi_real = (lemming[actLEM].Posy + 24 + y) * earth.Width + lemming[actLEM].Posx;
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
                            lemming[actLEM].Posy -= 3;
                            lemming[actLEM].Posx -= 6;
                            if (lemming[actLEM].Numstairs >= 10)
                            {
                                if (chinkInstance.State == SoundState.Playing)
                                {
                                    chinkInstance.Stop();
                                }
                                chinkInstance.Play();
                            }
                            //earth.SetData<Color>(c25); //OPTIMIZED BUILDER SETDATA
                            cantidad = 0;
                            px = lemming[actLEM].Posx;
                            if (px < 0)
                                px = 0;
                            for (ykk = 27; ykk < 31; ykk++)
                            {
                                posi_real = (lemming[actLEM].Posy + ykk) * earth.Width + px;
                                for (xkk = 0; xkk < 28; xkk++)
                                {
                                    Colormask22[cantidad] = C25[posi_real + xkk];
                                    cantidad++;
                                }
                            }
                            rectangleFill.X = px;
                            rectangleFill.Y = lemming[actLEM].Posy + 27;
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
                                lemming[actLEM].Posx += 8;
                                lemming[actLEM].Right = true;
                            }
                            continue;
                        }
                    }
                    if (lemming[actLEM].Numstairs >= 12 &&
                        !lemming[actLEM].Puentenomas)
                    {
                        lemming[actLEM].Builder = false;
                        lemming[actLEM].Puentenomas = true;
                        lemming[actLEM].Pixelscaida = 0;
                        if (lemming[actLEM].Right)
                        {
                            lemming[actLEM].Posx -= 6;
                        }
                        else
                        {
                            lemming[actLEM].Posx += 6;
                        }
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Numframes = walker_frames;
                    }
                }
                if (lemming[actLEM].Puentenomas && lemming[actLEM].Actualframe == 7 && lemming[actLEM].Puentenomas)
                {
                    lemming[actLEM].Puentenomas = false;
                    lemming[actLEM].Walker = true;
                    lemming[actLEM].Actualframe = 0;
                    lemming[actLEM].Numframes = walker_frames;
                    lemming[actLEM].Numstairs = 0;
                    continue;
                }
                if (lemming[actLEM].Digger) ///// DIGGER DIGGER WARNING WARNING
                {
                    if (abajo == 0 || abajo == 1) // 5 ok que no se aceleren a digger si hay mas de 2 juntos antes era <9 los pixeles debajo de sus pies
                    {
                        abajo2 = 0;
                        pixx2 = lemming[actLEM].Posx + 14;
                        for (xx88 = 0; xx88 <= 4; xx88++)
                        {
                            pos_real2 = lemming[actLEM].Posy + xx88 + 28;  ///////////// pixel por debajo.............
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
                        if ((lemming[actLEM].Actualframe == 11 || lemming[actLEM].Actualframe == 26) && dibuja_walker)
                        {
                            sx = 0;
                            for (y = 9; y <= 18; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                            {
                                posi_real = (lemming[actLEM].Posy + 14 + y) * earth.Width + lemming[actLEM].Posx;
                                if (lemming[actLEM].Posy + 14 + y > earth.Height)
                                {
                                    break;
                                } // cortar si esta en el limite por debajo 512=earth.height
                                for (xx88 = 4; xx88 <= 24; xx88++)
                                {
                                    if (SteelON)
                                    {
                                        x.X = lemming[actLEM].Posx + xx88;
                                        x.Y = lemming[actLEM].Posy + 14 + y;
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
                            cantidad = 0;
                            for (ykk = 9; ykk <= 18; ykk++)
                            {
                                posi_real = (lemming[actLEM].Posy + 14 + ykk) * earth.Width + lemming[actLEM].Posx;
                                for (xkk = 0; xkk < 28; xkk++)
                                {
                                    Colormask22[cantidad] = C25[posi_real + xkk];
                                    cantidad++;
                                }
                            }
                            rectangleFill.X = lemming[actLEM].Posx;
                            rectangleFill.Y = lemming[actLEM].Posy + 23;
                            rectangleFill.Width = 28;
                            rectangleFill.Height = 10;
                            earth.SetData(0, rectangleFill, Colormask22, 0, 28 * 10);
                            if (sx == -777)
                                continue;
                            lemming[actLEM].Posy += abajo2;
                            continue;
                        }
                    }
                    else
                    {
                        if (lemming[actLEM].Posy + 28 >= earth.Height) // erase draws bottom when die and exit level height 21x10
                        {
                            for (ykk = 0; ykk < 210; ykk++)
                            {
                                Colormask22[ykk].PackedValue = 0;
                            }
                            rectangleFill.Y = 502;
                            rectangleFill.X = lemming[actLEM].Posx + 4;
                            rectangleFill.Width = 21;
                            rectangleFill.Height = 10;
                            earth.SetData(0, rectangleFill, Colormask22, 0, 210);
                        }
                        lemming[actLEM].Basher = false;
                        lemming[actLEM].Builder = false;
                        lemming[actLEM].Miner = false;
                        lemming[actLEM].Escalando = false;
                        lemming[actLEM].Digger = false;
                        lemming[actLEM].Fall = true;
                        lemming[actLEM].Walker = false;
                        lemming[actLEM].Pixelscaida = 0;
                        lemming[actLEM].Actualframe = 0;
                        lemming[actLEM].Numframes = faller_frames;
                        continue; //break o continue DUNNO I DON'T KNOW WHICH IS BETTER
                    }

                }
                if (lemming[actLEM].Escalando)
                {
                    if (lemming[actLEM].Posy <= -28) // top of level -- out of limits 28 size sprite lemming 28x28
                    {
                        lemming[actLEM].Escalando = false;
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
                        pos_real2 = lemming[actLEM].Posy + 27;
                        if (C25[(pos_real2 * earth.Width) + pixx - 2].R > 0 || C25[(pos_real2 * earth.Width) + pixx - 2].G > 0 || C25[(pos_real2 * earth.Width) + pixx - 2].B > 0)
                        {
                            lemming[actLEM].Right = false;
                            lemming[actLEM].Posx -= 2;   // 1 o 2 LOOK
                            lemming[actLEM].Escalando = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Numframes = walker_frames;
                            lemming[actLEM].Actualframe = 0;
                            continue;
                        }
                    }
                    else
                    {
                        pos_real2 = lemming[actLEM].Posy + 27;
                        if (C25[(pos_real2 * earth.Width) + pixx + 2].R > 0 || C25[(pos_real2 * earth.Width) + pixx + 2].G > 0 || C25[(pos_real2 * earth.Width) + pixx + 2].B > 0)
                        {
                            lemming[actLEM].Right = true;
                            lemming[actLEM].Posx += 2; // 1 o 2 LOOK
                            lemming[actLEM].Escalando = false;
                            lemming[actLEM].Walker = true;
                            lemming[actLEM].Numframes = walker_frames;
                            lemming[actLEM].Actualframe = 0;
                            continue;
                        }
                    }
                    if (arriba > 0 && dibuja)
                    {
                        lemming[actLEM].Posy--;
                    }
                    if (arriba == 0)
                    {
                        if (lemming[actLEM].Right)
                        {
                            lemming[actLEM].Posx++;
                        }
                        else
                        {
                            lemming[actLEM].Posx--;
                        }
                        lemming[actLEM].Escalando = false;
                        lemming[actLEM].Walker = true;
                        lemming[actLEM].Numframes = walker_frames;
                        lemming[actLEM].Actualframe = 0;
                        continue;
                    }
                }
                if (lemming[actLEM].Walker)
                {
                    if (abajo < 3 && lemming[actLEM].Right)
                    {
                        lemming[actLEM].Posx++;
                        if (arriba < 16)
                        {
                            lemming[actLEM].Posy -= arriba;
                        }
                    }  //// <6 o <8 falla cava
                    if (abajo < 3 && !lemming[actLEM].Right)
                    {
                        lemming[actLEM].Posx--;
                        if (arriba < 16)
                        {
                            lemming[actLEM].Posy -= arriba;
                        }
                    }
                    if (arriba >= 16)
                    {
                        if (!lemming[actLEM].Escalar)
                        {
                            if (lemming[actLEM].Right && arriba >= 16)
                            {
                                lemming[actLEM].Right = false;
                                lemming[actLEM].Posx -= 2;  // 1 o 2 LOOK
                            }
                            else
                            {
                                lemming[actLEM].Right = true;
                                lemming[actLEM].Posx += 2;  // 1 o 2 LOOK
                            }
                        }
                        else
                        {
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Escalando = true;
                            lemming[actLEM].Numframes = climber_frames;
                            lemming[actLEM].Pixelscaida = 0;
                            lemming[actLEM].Actualframe = 0;
                            continue;
                        }
                    }
                }
                if (lemming[actLEM].Explotando && lemming[actLEM].Actualframe >= 47)
                {
                    ////////////////////////////////////////////////////////////////////////////////////// EXPLODE MASK
                    ///////////////// EXPLODING MASK LIMITS -- SIZE OF AREA ERASEABLE
                    ancho66 = 38;
                    alto66 = 53;
                    px = lemming[actLEM].Posx - 5; //center the big explosion to 28x28 lemming sprite
                    py = lemming[actLEM].Posy - 2;
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
                    cantidad = ancho66 * alto66;
                    rectangleFill.X = px2;
                    rectangleFill.Y = py2;
                    rectangleFill.Width = ancho66;
                    rectangleFill.Height = alto66;
                    mascaraexplosion.GetData(0, rectangleFill, Colormask33, 0, cantidad);
                    cantidad = 0;
                    yypos888 = 0;
                    yy88 = 0;
                    xx88 = 0;
                    for (yy88 = 0; yy88 < alto66; yy88++)
                    {
                        yypos888 = (yy88 + py) * earth.Width;
                        for (xx88 = 0; xx88 < ancho66; xx88++)
                        {
                            Colorsobre33[cantidad].PackedValue = C25[yypos888 + px + xx88].PackedValue;
                            cantidad++;
                        }
                    }
                    for (r = 0; r < cantidad; r++)
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
                    earth.SetData(0, rectangleFill, Colorsobre33, 0, cantidad);
                    // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                    cantidad = 0;
                    yypos555 = 0;
                    yy33 = 0;
                    xx33 = 0;
                    for (yy33 = 0; yy33 < alto66; yy33++)
                    {
                        yypos555 = (yy33 + py) * earth.Width;
                        for (xx33 = 0; xx33 < ancho66; xx33++)
                        {
                            C25[yypos555 + px + xx33].PackedValue = Colorsobre33[cantidad].PackedValue;
                            cantidad++;
                        }
                    }
                    lemming[actLEM].Dead = true;
                    numlemnow--;
                    lemming[actLEM].Explotando = false;
                    lemming[actLEM].Explota = false;
                    // luto luto sound fix
                    if (exploInstance.State == SoundState.Playing)
                    {
                        exploInstance.Stop();
                    }
                    try
                    {
                        exploInstance.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                    //explosions addons emitter - particles logic add
                    xExp = lemming[actLEM].Posx + 14;
                    yExp = lemming[actLEM].Posy + 14;
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
                if (!lemming[actLEM].Falling && lemming[actLEM].Activo)
                {
                    if (abajo >= 3)
                    {
                        lemming[actLEM].Posy += 3;
                        lemming[actLEM].Pixelscaida += 3;
                    }
                    else
                    {
                        lemming[actLEM].Posy += abajo;
                        lemming[actLEM].Pixelscaida += abajo;
                    } // fall 3 MAX---MAX 3 FALL PIXELS
                }
                else
                {
                    if (!lemming[actLEM].Ahoga && dibuja)
                    {
                        if (abajo >= 3)
                        {
                            lemming[actLEM].Posy += 3;
                        }
                        else
                        {
                            lemming[actLEM].Posy += abajo;
                        }
                    }
                }
                if (lemming[actLEM].Posy < -27) // walker top of the screen
                {
                    if (lemming[actLEM].Right)
                    {
                        lemming[actLEM].Right = false;
                        lemming[actLEM].Posx -= 3;
                        lemming[actLEM].Posy++;
                    }
                    else
                    {
                        lemming[actLEM].Right = true;
                        lemming[actLEM].Posx += 3;
                        lemming[actLEM].Posy++;
                    }
                }
                if (lemming[actLEM].Posx < -16)// limits of the screen from LEFT
                {
                    lemming[actLEM].Activo = false;
                    lemming[actLEM].Dead = true;
                    numlemnow--;
                    lemming[actLEM].Explotando = false;
                    lemming[actLEM].Explota = false;
                    if (dieInstance.State == SoundState.Playing)
                    {
                        dieInstance.Stop();
                    }
                    try
                    {
                        dieInstance.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                }
                if (lemming[actLEM].Posx + 14 > earth.Width)// limits of the screen from RIGHT
                {
                    lemming[actLEM].Activo = false;
                    lemming[actLEM].Dead = true;
                    numlemnow--;
                    lemming[actLEM].Explotando = false;
                    lemming[actLEM].Explota = false;
                    if (dieInstance.State == SoundState.Playing)
                    {
                        dieInstance.Stop();
                    }
                    try
                    {
                        dieInstance.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                }

            }
        }

        public LemmingsNetGame()
        {
            _lockMouse = false;
            graphics = new GraphicsDeviceManager(this)
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

        protected void ActualizarMouse()
        {
            mouseAntState = mouseActState;
            mouseActState = Mouse.GetState();
            valorx = mouseActState.X;
            valorx += xscroll;
            valory = mouseActState.Y;
            valory += yscroll;
            mousepos.X = mouseActState.X;
            mousepos.Y = mouseActState.Y;
            sposicionMouse = valorx.ToString() + " " + valory.ToString();
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
            Point bounds = new Point(preferredBackBufferWidth, preferredBackBufferHeight);
            screenRatio = (float)bounds.X / bounds.Y;
            float scale;
            Rectangle rectangle = new Rectangle();

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

        internal static Rectangle CenterRectangle(Rectangle outerRectangle, Rectangle innerRectangle)
        {
            Point delta = outerRectangle.Center - innerRectangle.Center;
            innerRectangle.Offset(delta);
            return innerRectangle;
        }

#pragma warning disable S125 // Sections of code should not be commented out
        //private Texture2D mascarapico, lyipie, lglup, lsplat, walker, mainMenuLogo, lucesfondo, backmenu3, explode, backmenu1, numfont, Crate;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            renderTarget = new RenderTarget2D(GraphicsDevice, gameResolution.X, gameResolution.Y);
            renderTargetDestination = GetRenderTargetDestination(gameResolution, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
    
            texture1pixel = new Texture2D(GraphicsDevice, 1, 1);
            texture1pixel.SetData(new Color[] { Color.White });  // texture for DRAWLINE 1x1
            mainMenuSign2 = Content.Load<Texture2D>("cubo");
            blink1 = Content.Load<Texture2D>("lem1/blink1");
            blink2 = Content.Load<Texture2D>("lem1/blink2");
            blink3 = Content.Load<Texture2D>("lem1/blink3");

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
            lightEffect = Content.Load<Effect>("lightEffect");
            efecto = Content.Load<Effect>("efecto");
            lighting = new RenderTarget2D(GraphicsDevice, widthl, height);

            // Lighting parameters
            lightColor = Color.Cyan;
            lightRadius = 350.0f; //250
            lightIntensity = 0.8f;  //1
            lightPosition = Vector3.Zero;

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
                        BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));
                        for (Za = 0; Za < numTotalLevels; Za++)
                        {
                            LevelEnd[Za] = reader.ReadBoolean();
                        }
                        reader.Close();
                        MustReadFile = false;
                    }
                    else
                    {
                        BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
                        for (Za = 0; Za < numTotalLevels; Za++)
                        {
                            writer.Write(LevelEnd[Za]);
                        }
                        writer.Write("(c) 2016 Oskar Oskar LEMMINGS c#");
                        writer.Close();
                        MustReadFile = false;
                    }
                }

            }
            if (LevelOn) //when level starts all the vars and reset all
            {
                songInstance.Stop();
                LemSkill = "";
                Paused = false;
                zvTime = 0;
                allBlow = false;
                actualBlow = 0;
                exitFrame = 999;
                _currentSelectedSkill = ECurrentSkill.NONE;
                op12 = false;
                moreexits = null;
                moredoors = null;
                trap = null;
                arrow = null;
                sprite = null;
                numTOTexits = 1;
                numTOTdoors = 1;
                NumTotTraps = 0;
                numTOTadds = 0;
                NumTotArrow = 0;
                doorwavOn = false;
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
                earth = Content.Load<Texture2D>(level[levelNumber].nameLev);
                earth.GetData(C25, 0, earth.Height * earth.Width); //better here than moverlemming() for performance see issues 
                                                                   //see differences with old getdata, see size important (x * y)
                puerta1x = level[levelNumber].doorX;
                puerta1y = level[levelNumber].doorY;
                salida1x = level[levelNumber].exitX;
                salida1y = level[levelNumber].exitY;
                // this is the depth of the exit and doors animated sprites -- See level 58 the exit is behind the mountain (0.6f)
                if (level[levelNumber].DoorExitDepth != 0)
                {
                    DoorExitDepth = level[levelNumber].DoorExitDepth;
                }
                else
                {
                    DoorExitDepth = 0.403f;
                }
                numeroescalan = level[levelNumber].numberClimbers;
                numeroparaguas = level[levelNumber].numberUmbrellas;
                numeroexplotan = level[levelNumber].numberExploders;
                numeroblockers = level[levelNumber].numberBlockers;
                numeropuentes = level[levelNumber].numberBuilders;
                numeropared = level[levelNumber].numberBashers;
                numeropico = level[levelNumber].numberMiners;
                numerocavan = level[levelNumber].numberDiggers;
                if (numeroescalan > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.CLIMBER;
                }
                else if (numeroparaguas > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.FLOATER;
                }
                else if (numeroexplotan > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.EXPLODER;
                }
                else if (numeroblockers > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.BLOCKER;
                }
                else if (numeropuentes > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.BUILDER;
                }
                else if (numeropared > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.BASHER;
                }
                else if (numeropico > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.MINER;
                }
                else if (numerocavan > 0)
                {
                    _currentSelectedSkill = ECurrentSkill.DIGGER;
                }
                numerofrecuencia = level[levelNumber].NumberFrecuency;
                numerominfrecuencia = level[levelNumber].minNumberFrecuency;
                Numlems = level[levelNumber].numTotalLem;
                Lemsneeded = level[levelNumber].lemsToSave;
                xscroll = level[levelNumber].initXpos;
                yscroll = 0;
                lemming = new Lem[Numlems];
                VariablesTraps();

                //walker = Content.Load<Texture2D>("walker");
                walker2 = Content.Load<Texture2D>("walker_ok");
                ratonon = Content.Load<Texture2D>("raton_on1");
                if (debug)
                {
                    ratonoff = Content.Load<Texture2D>("raton_off1_debugon");
                }
                else
                {
                    ratonoff = Content.Load<Texture2D>("raton_off1");
                }
                cae = Content.Load<Texture2D>("cae_ok");
                digger = Content.Load<Texture2D>("cavar_ok");
                circulo_led = Content.Load<Texture2D>("circulo_brillante");
                puerta_ani = Content.Load<Texture2D>("puerta" + string.Format("{0}", level[levelNumber].typeOfDoor)); // type of door puerta1-2-3-4 etc.
                string xx455 = string.Format("{0}", level[levelNumber].typeOfExit);
                salida_ani1 = Content.Load<Texture2D>("salida" + xx455);
                salida_ani1_1 = Content.Load<Texture2D>("salida" + xx455 + "_1");
                sale = Content.Load<Texture2D>("sale");
                agua2 = Content.Load<Texture2D>("Animations/water2");
                lemfont = Content.Load<Texture2D>("lemmfont");
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
                escala = Content.Load<Texture2D>("escala");
                explota = Content.Load<Texture2D>("explota");
                blocker = Content.Load<Texture2D>("blocker");
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
                init = Content.Load<SoundEffect>("soundfx/letsgo");
                lohno = Content.Load<Texture2D>("sprite/ohno");
                //lsplat = Content.Load<Texture2D>("sprite/splat");
                explosion_particle = Content.Load<Texture2D>("sprite/stater");  //stater nice with rotation too
                //lyipie = Content.Load<Texture2D>("sprite/yipie");
                //lglup = Content.Load<Texture2D>("sprite/glub");
                lhiss = Content.Load<Texture2D>("sprite/hiss");
                lchink = Content.Load<Texture2D>("sprite/chink");
                sahoga = Content.Load<Texture2D>("ahoga");
                squemado = Content.Load<Texture2D>("quemado");
                doorwav = Content.Load<SoundEffect>("soundfx/door");
                initInstance = init.CreateInstance();
                doorInstance = doorwav.CreateInstance();
                oing = Content.Load<SoundEffect>("soundfx/yippee");
                oingInstance = oing.CreateInstance();
                die = Content.Load<SoundEffect>("soundfx/die");
                dieInstance = die.CreateInstance();
                splat = Content.Load<SoundEffect>("soundfx/splat");
                splatInstance = splat.CreateInstance();
                ohno = Content.Load<SoundEffect>("soundfx/ohno");
                ohnoInstance = ohno.CreateInstance();
                chink = Content.Load<SoundEffect>("soundfx/chink");
                chinkInstance = chink.CreateInstance();
                explo = Content.Load<SoundEffect>("soundfx/explode");
                exploInstance = explo.CreateInstance();
                sfire = Content.Load<SoundEffect>("soundfx/fire");
                fireInstance = sfire.CreateInstance();
                sglug = Content.Load<SoundEffect>("soundfx/glug");
                glugInstance = sglug.CreateInstance();
                sting = Content.Load<SoundEffect>("soundfx/ting");
                tingInstance = sting.CreateInstance();
                smousepre = Content.Load<SoundEffect>("soundfx/mousepre");
                mousepreInstance = smousepre.CreateInstance();
                schangeop = Content.Load<SoundEffect>("soundfx/changeop");
                changeopInstance = schangeop.CreateInstance();
                numSong = levelNumber % 19; // 19 song files on music
                switch (numSong)
                {
                    case 1:
                        song = Content.Load<SoundEffect>("music/lem_intro");
                        break; //lem_intro
                    case 2:
                        song = Content.Load<SoundEffect>("music/lemming1");
                        break;
                    case 3:
                        song = Content.Load<SoundEffect>("music/tim2");
                        break;
                    case 4:
                    case 146:
                        song = Content.Load<SoundEffect>("music/lemming2");
                        break;
                    case 5:
                        song = Content.Load<SoundEffect>("music/tim8");
                        break;
                    case 6:
                        song = Content.Load<SoundEffect>("music/tim3");
                        break;
                    case 7:
                        song = Content.Load<SoundEffect>("music/tim5");
                        break;
                    case 8:
                        song = Content.Load<SoundEffect>("music/doggie");
                        break;
                    case 9:
                        song = Content.Load<SoundEffect>("music/tim6");
                        break;
                    case 10:
                        song = Content.Load<SoundEffect>("music/lemming3");
                        break;
                    case 11:
                        song = Content.Load<SoundEffect>("music/tim7");
                        break;
                    case 12:
                        song = Content.Load<SoundEffect>("music/tim9");
                        break;
                    case 13:
                        song = Content.Load<SoundEffect>("music/tim1");
                        break;
                    case 14:
                        song = Content.Load<SoundEffect>("music/tim10");
                        break;
                    case 15:
                        song = Content.Load<SoundEffect>("music/tim4");
                        break;
                    case 16:
                        song = Content.Load<SoundEffect>("music/tenlemms");
                        break;
                    case 17:
                        song = Content.Load<SoundEffect>("music/mountain");
                        break;
                    case 18:
                        song = Content.Load<SoundEffect>("music/cancan");
                        break;
                    case 0:
                        song = Content.Load<SoundEffect>("music/tim1");
                        break;
                    default:
                        song = Content.Load<SoundEffect>("music/lem_intro");
                        break;
                }
                songInstance = song.CreateInstance();
                winSong = Content.Load<SoundEffect>("music/title");
                winSongInstance = winSong.CreateInstance();
                winSongInstance.IsLooped = true;
            }
            lemfont = Content.Load<Texture2D>("lemmfont");
            //numfont = Content.Load<Texture2D>("nummfont");
            Font1 = Content.Load<SpriteFont>("spriteFont1");
        }
#pragma warning restore S125 // Sections of code should not be commented out

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            lightPosition = new Vector3(Mouse.GetState().X, Mouse.GetState().Y, 10.0f);
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
            if (!LevelEnded && ((allBlow && numlemnow == 0) || zvTime < 0 || (numerosaca == Numlems && numlemnow == 0)))
            {
                if (!Paused)
                    rest++;  // var to wait until menu appears gives this way 4 seconds plus more
                if (rest > 190)
                {
                    Exploding = false;
                    actItem = 0;  //see when finish time and are more particles ON
                    LevelEnded = true;
                    Paused = true;
                    if (Numerodentro < Lemsneeded)
                        ExitBad = true;
                }
            }
            if (oldK.IsKeyDown(Keys.F1) && actK.IsKeyUp(Keys.F1)) // f1 de-activate debug mode this is only for test BETTER OFF
            {
                debug = !debug;
            }
            if (oldK.IsKeyDown(Keys.F12) && actK.IsKeyUp(Keys.F12))
            {
                ToggleScale();
            }
            if (oldK.IsKeyDown(Keys.M) && actK.IsKeyUp(Keys.M) && songInstance != null)
            {
                if (songInstance.State == SoundState.Playing)
                    songInstance.Pause();
                else if (songInstance.State == SoundState.Paused)
                    songInstance.Resume();
            }
            if (oldK.IsKeyDown(Keys.Left))
            {
                xscroll -= 5;
                Scrolling();
            }
            if (oldK.IsKeyDown(Keys.Right))
            {
                xscroll += 5;
                Scrolling();
            }
            if (oldK.IsKeyDown(Keys.Up))
            {
                yscroll -= 5;
                Scrolling();
            }
            if (oldK.IsKeyDown(Keys.Down))
            {
                yscroll += 5;
                Scrolling();
            }
            if (oldK.IsKeyDown(Keys.D1))
            {
                _decreaseOn = true;
            }
            else if (oldK.IsKeyUp(Keys.D1))
                _decreaseOn = false;
            if (oldK.IsKeyDown(Keys.D2))
            {
                _increaseOn = true;
            }
            else if (oldK.IsKeyUp(Keys.D2))
                _increaseOn = false;

            if (numeroescalan > 0 && oldK.IsKeyDown(Keys.D3) && actK.IsKeyUp(Keys.D3))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.CLIMBER;
            }
            else if (numeroparaguas > 0 && oldK.IsKeyDown(Keys.D4) && actK.IsKeyUp(Keys.D4))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.FLOATER;
            }
            else if (numeroexplotan > 0 && oldK.IsKeyDown(Keys.D5) && actK.IsKeyUp(Keys.D5))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.EXPLODER;
            }
            else if (numeroblockers > 0 && oldK.IsKeyDown(Keys.D6) && actK.IsKeyUp(Keys.D6))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BLOCKER;
            }
            else if (numeropuentes > 0 && oldK.IsKeyDown(Keys.D7) && actK.IsKeyUp(Keys.D7))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BUILDER;
            }
            else if (numeropared > 0 && oldK.IsKeyDown(Keys.D8) && actK.IsKeyUp(Keys.D8))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BASHER;
            }
            else if (numeropico > 0 && oldK.IsKeyDown(Keys.D9) && actK.IsKeyUp(Keys.D9))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.MINER;
            }
            else if (numerocavan > 0 && oldK.IsKeyDown(Keys.D0) && actK.IsKeyUp(Keys.D0))
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
                    else if (Numerodentro >= Lemsneeded && LevelEnded)
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
                Numerodentro = 0;
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
                if (Numerodentro >= Lemsneeded)
                    ExitLevel = true;
            }
            if (ExitLevel)
            {
                if (Numerodentro >= Lemsneeded) //see here if level is finished or not
                {
                    LevelEnd[mmlevchoose] = true;
                    BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
                    for (Za = 0; Za < numTotalLevels; Za++)
                    {
                        //LevelEnd[za] = false; // first time create all the levels vars to false --> not finished
                        writer.Write(LevelEnd[Za]);
                    }
                    writer.Write("(c) 2016-2023 Oskar Oskar LEMMINGS c#. 2023 FilRip from CoolBytes");
                    writer.Close();
                    MustReadFile = true;
                    LevelOn = true;
                    levelNumber++;
                    if (levelNumber >= numTotalLevels - 1)
                        levelNumber = numTotalLevels - 1;
                    mmlevchoose = levelNumber;
                    MainMenu = false;
                    this.IsMouseVisible = false;
                    Numerodentro = 0;
                    numlemnow = 0;
                    numerosaca = 0;
                    fade = true;
                    milisegundos = 0;
                    puertaon = true;
                    Frame = 0;
                    Frame2 = 0;
                    Frame3 = 0;
                    framepuerta = 0;
                    framesalida = 0;
                    rest = 0;
                    LevelEnded = false;
                    ExitLevel = false;
                    allBlow = false;
                    zvTime = 0;
                    ExitBad = false;
                    Content.Unload();
                    base.LoadContent();
                    base.Initialize();
                    return; //next level
                }

                if (ExitBad) //repeat level
                {
                    LevelOn = true;
                    MainMenu = false;
                    this.IsMouseVisible = false;
                    Numerodentro = 0;
                    numlemnow = 0;
                    numerosaca = 0;
                    fade = true;
                    milisegundos = 0;
                    puertaon = true;
                    Frame = 0;
                    Frame2 = 0;
                    Frame3 = 0;
                    framepuerta = 0;
                    framesalida = 0;
                    rest = 0;
                    LevelEnded = false;
                    ExitLevel = false;
                    allBlow = false;
                    zvTime = 0;
                    ExitBad = false;
                    Content.Unload(); // UnloadContent all sprites slows 3 - 4 seconds
                    base.LoadContent();
                    base.Initialize();
                    return;
                }
                songInstance.Stop();
                MainMenu = true;
                LevelOn = false;
                mmop1 = false;
                mmop2 = false;
                mmop3 = false;
                mmop4 = false;
                mmop5 = false;
                mmop6 = false;
                mmlevchoose = 0;
                this.IsMouseVisible = false; //true without shader
                LevelEnded = false;
                ExitLevel = false;
                allBlow = false;
                zvTime = 0;
                ExitBad = false;
                numerosaca = 0;
                Content.Unload();
                base.LoadContent();
                base.Initialize();
                return;
            }

            if (oldK.IsKeyDown(Keys.P) && actK.IsKeyUp(Keys.P))
            {
                PlaySoundMenu();
                Paused = !Paused;
            }
            if (allBlow && actualBlow < numerosaca) // crash crash TEST TEST
            {
                if (!lemming[actualBlow].Dead && !lemming[actualBlow].Explotando)
                    lemming[actualBlow].Explota = true;
                actualBlow++;
            }
            milisegundos += gameTime.ElapsedGameTime.Milliseconds;
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
                ActualizarMouse();
                if (mmlevchoose != 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
                {
                    LevelOn = true;
                    levelNumber = mmlevchoose;
                    MainMenu = false;
                    this.IsMouseVisible = false;
                    Numerodentro = 0;
                    numlemnow = 0;
                    numerosaca = 0;
                    fade = true;
                    milisegundos = 0;
                    puertaon = true;
                    Frame = 0;
                    Frame2 = 0;
                    Frame3 = 0;
                    framepuerta = 0;
                    framesalida = 0;
                    Content.Unload(); // UnloadContent all sprites slows 3 - 4 seconds
                    base.LoadContent();
                    base.Initialize();
                    /// GC.Collect();GC.WaitForPendingFinalizers();
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
                    particle = new Particles[numParticles];
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

        void ToggleScale()
        {
            scaled = !scaled;

            graphics.PreferredBackBufferWidth = gameResolution.X * (scaled ? 2 : 1);
            graphics.PreferredBackBufferHeight = gameResolution.Y * (scaled ? 2 : 1);

            graphics.ApplyChanges();

            renderTargetDestination = GetRenderTargetDestination(gameResolution, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (LevelOn)
            {
                GraphicsDevice.SetRenderTarget(renderTarget);
                GraphicsDevice.Clear(letterboxingColor);

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null, null);
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
                        spriteBatch.Draw(particle[varParticle].Sprite, particle[varParticle].Pos, rectangleFill, colorFill, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.50001f);
                    }
                }
                rayLigths = true;
                // logic of background stars moving from -50 to 50
                actWaves333 = 50 * Math.Sin(actWaves / 60);  // 50 height of the wave  // 60 length of it
                actWaves444 = -70 * Math.Sin(actWaves / -80); // 10,100 -70,100
                if (levelNumber != 159)
                {
                    rectangleFill.X = 0;
                    rectangleFill.Y = 0;
                    rectangleFill.Width = gameResolution.X;
                    rectangleFill.Height = (int)(gameResolution.Y * 0.732);
                    colorFill.R = 150;
                    colorFill.G = 150;
                    colorFill.B = 150;
                    colorFill.A = 160;
                    spriteBatch.Draw(logo_fondo, rectangleFill, rectangleFill, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
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
                    spriteBatch.Draw(logo666, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.8091f);
                    Texture2D logo555 = Content.Load<Texture2D>("fondos/ice outttt");
                    rectangleFill2.X = 0 + (int)actWaves444;
                    rectangleFill2.Y = 0 + (int)actWaves444;
                    rectangleFill2.Width = gameResolution.X;
                    rectangleFill2.Height = gameResolution.Y - 188;
                    colorFill.R = 150;
                    colorFill.G = 150;
                    colorFill.B = 150;
                    colorFill.A = 120;
                    spriteBatch.Draw(logo555, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
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
                            rectangleFill.X = trap[r].areaDraw.X - xscroll;
                            rectangleFill.Y = trap[r].areaDraw.Y - yscroll;
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
                            spY = trap[r].sprite.Height / trap[r].numFrames;
                            rectangleFill.X = (int)trap[r].pos.X - xscroll - trap[r].vvX;
                            rectangleFill.Y = (int)trap[r].pos.Y - trap[r].vvY - yscroll;
                            rectangleFill.Width = trap[r].sprite.Width;
                            rectangleFill.Height = spY;
                            rectangleFill2.X = 0;
                            rectangleFill2.Y = spY * trap[r].actFrame;
                            rectangleFill2.Width = trap[r].sprite.Width;
                            rectangleFill2.Height = spY;
                            spriteBatch.Draw(trap[r].sprite, rectangleFill, rectangleFill2, colorFill, 0f, Vector2.Zero, SpriteEffects.None, trap[r].depth);
                        }
                        if (debug)
                        {
                            spriteBatch.Draw(texture1pixel, new Rectangle(trap[r].areaTrap.Left - xscroll, trap[r].areaTrap.Top - yscroll, trap[r].areaTrap.Width, trap[r].areaTrap.Height),
                                null, new Color(255, 255, 255, 140), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                        }

                    }
                }
                if (SteelON && debug) // and debug show magenta steel areas
                {
                    for (xz = 0; xz < numTOTsteel; xz++)
                    {
                        rectangleFill.X = steel[xz].area.Left - xscroll;
                        rectangleFill.Y = steel[xz].area.Top - yscroll;
                        rectangleFill.Width = steel[xz].area.Width;
                        rectangleFill.Height = steel[xz].area.Height;
                        // magenta r:255,g:0,b:255
                        colorFill.R = 255;
                        colorFill.G = 0;
                        colorFill.B = 255;
                        colorFill.A = 140;
                        spriteBatch.Draw(texture1pixel, rectangleFill, null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    }
                }
                switch (levelNumber)  // effect draws water cascade,stars,etc...
                {
                    case 1:
                        spriteBatch.Draw(agua2, new Rectangle(1560 - xscroll, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                        rayLigths = false;
                        break;
                    case 4:
                        spriteBatch.Draw(agua2, new Rectangle(1530 - xscroll, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f
                        spriteBatch.Draw(agua2, new Rectangle(1560 - xscroll, -80, 260, 750), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.803f); //0.802f  
                        rayLigths = false;
                        break;
                    case 5:
                        spriteBatch.Draw(agua2, new Rectangle(760 - xscroll, -80, 260, 650), new Rectangle(0 + z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                            Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                        break;
                    case 6:
                        spriteBatch.Draw(agua2, new Rectangle(2000 - xscroll, -80, 260, 680), new Rectangle(0 + z3 * 192, 0, 192, 192),
                            new Color(255, 50, 80, 170), 0f, Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f                            
                        break;
                    default:
                        break;
                }

                if (levelNumber != 159) //nubes clouds moving in background
                {
                    if (rayLigths)
                    {
                        spriteBatch.Draw(myTexture, new Vector2(gameResolution.X / 2, (gameResolution.Y - 188) / 2), new Rectangle(0, 0, myTexture.Width, myTexture.Height), new Color(255, 255, 255, 10 + Contador * 2),
                            0.4f + Contador2 * 0.001f, new Vector2(myTexture.Width / 2, myTexture.Height / 2), 3f, SpriteEffects.FlipHorizontally, 0.805f); // okokok
                    }
                    // rayligts effect
                    spriteBatch.Draw(nubes_2, new Rectangle(0, 50 - (int)actWaves444, gameResolution.X, nubes_2.Height), new Rectangle(z1, 0, gameResolution.X, nubes_2.Height),
                        new Color(255, 255, 255, 110), 0f, Vector2.Zero, SpriteEffects.None, 0.804f);

                    spriteBatch.Draw(nubes, new Rectangle(0, 220, gameResolution.X, nubes.Height), new Rectangle(z2, 0, gameResolution.X, nubes.Height), new Color(255, 255, 255, 110), 0f,
                        Vector2.Zero, SpriteEffects.None, 0.803f);
                }
                spriteBatch.Draw(earth, new Vector2(0, 0), new Rectangle(xscroll, yscroll, gameResolution.X, gameResolution.Y - 188), //512 size of window draw
                    Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.500f);
                if (NumTotArrow > 0)
                {
                    for (xz = 0; xz < NumTotArrow; xz++)
                    {
                        spriteBatch.Draw(arrow[xz].flechassobre, new Vector2(arrow[xz].area.X - xscroll, arrow[xz].area.Y - yscroll),
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
                        if (ExitBad && ohnoInstance.State != SoundState.Playing)
                            ohnoInstance.Play();
                        else if (!ExitBad && winSongInstance.State != SoundState.Playing)
                            winSongInstance.Play();
                    }
                    _endSongPlayed = true;
                    colorFill.R = 0; //color.black for this change to see differents options
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 150;
                    spriteBatch.Draw(texture1pixel, new Rectangle(45, 32, 1005, 600), null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.001f);
                    spriteBatch.Draw(mainMenuSign2, new Rectangle(-200, -120, 1500, 900), null,
                       Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.00005f);
                    percent = (100 * Numerodentro) / level[mmlevchoose].numTotalLem;
                    TextLem("All lemmings accounted for:", new Vector2(150, 100), Color.Cyan, 1.5f, 0.0000000001f);
                    TextLem("You rescued " + string.Format("{0}", percent) + "%",
                         new Vector2(270, 160), Color.Violet, 1.5f, 0.0000000001f);
                    percent = (100 * Lemsneeded) / level[mmlevchoose].numTotalLem;
                    TextLem("You needed " + string.Format("{0}", percent) + "%",
                         new Vector2(300, 220), Color.DodgerBlue, 1.5f, 0.0000000001f);
                    TextLem("Press <ESC> or <Left Mouse Button>", new Vector2(70, 400), Color.LightCyan, 1.3f, 0.0000000001f);
                    if (ExitBad)
                        TextLem("to retry level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f);
                    else if (Numerodentro >= Lemsneeded)
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
                xx55 = varDoor[level[levelNumber].typeOfDoor].xWidth;
                yy55 = varDoor[level[levelNumber].typeOfDoor].yWidth;
                framereal565 = (framepuerta * yy55);
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
                            spriteBatch.Draw(sprite[ssi].sprite, new Vector2(sprite[ssi].pos.X, sprite[ssi].pos.Y - yscroll),
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
                                    xxAnim = (int)sprite[ssi].pos.X - xscroll + 32;
                                }
                                else
                                {
                                    xxAnim = (int)sprite[ssi].pos.X + 32;
                                }
                                spriteBatch.Draw(sprite[ssi].sprite, new Vector2(xxAnim, sprite[ssi].pos.Y - yscroll - 32),
                                    new Rectangle(sx1, sy1, swidth, sheight), new Color(sprite[ssi].R, sprite[ssi].G, sprite[ssi].B, sprite[ssi].transparency),
                                    sprite[ssi].rotation, sprite[ssi].center, sprite[ssi].scale, SpriteEffects.None, sprite[ssi].depth);
                            }
                            else
                            {
                                xxAnim = 0;
                                if (sprite[ssi].minusScrollx)
                                {
                                    xxAnim = (int)sprite[ssi].pos.X - xscroll;
                                }
                                else
                                {
                                    xxAnim = (int)sprite[ssi].pos.X;
                                }
                                spriteBatch.Draw(sprite[ssi].sprite, new Vector2(xxAnim, sprite[ssi].pos.Y - yscroll),
                                    new Rectangle(sx1, sy1, swidth, sheight), new Color(sprite[ssi].R, sprite[ssi].G, sprite[ssi].B, sprite[ssi].transparency),
                                    sprite[ssi].rotation, Vector2.Zero, sprite[ssi].scale, SpriteEffects.None, sprite[ssi].depth);
                            }
                        }
                    }
                }
                if (PlatsON)
                {
                    for (A = 0; A < numTOTplats; A++)
                    {
                        x2 = plats[A].areaDraw.X - plats[A].areaDraw.Width / 2;
                        y = plats[A].areaDraw.Y;
                        w = plats[A].sprite.Width;
                        h = plats[A].sprite.Height;
                        spriteBatch.Draw(plats[A].sprite, new Rectangle(x2 - xscroll, y - yscroll - 5, plats[A].areaDraw.Width, plats[A].areaDraw.Height),
                            new Rectangle(0, 0, w, h), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.56f);
                    }
                }
                if (moredoors == null)
                {
                    spriteBatch.Draw(puerta_ani, new Vector2(puerta1x - xscroll, puerta1y - yscroll), new Rectangle(0, framereal565, xx55, yy55),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                }
                else
                {
                    for (A = 0; A < numTOTdoors; A++)
                    {
                        puerta1x = (int)moredoors[A].doormorexy.X;
                        puerta1y = (int)moredoors[A].doormorexy.Y;
                        spriteBatch.Draw(puerta_ani, new Vector2(puerta1x - xscroll, puerta1y - yscroll), new Rectangle(0, framereal565, xx55, yy55),
                            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    }
                }
                xx66 = varExit[level[levelNumber].typeOfExit].xWidth;
                yy66 = varExit[level[levelNumber].typeOfExit].yWidth;
                xx88 = varExit[level[levelNumber].typeOfExit].moreX;
                xx99 = varExit[level[levelNumber].typeOfExit].moreX2;
                yy88 = varExit[level[levelNumber].typeOfExit].moreY;
                yy99 = varExit[level[levelNumber].typeOfExit].moreY2;
                frameact = (framesalida * yy66);
                if (moreexits == null)
                {
                    spriteBatch.Draw(salida_ani1_1, new Vector2(salida1x - xscroll - xx88, salida1y - yy88 - yscroll), new Rectangle(0, frameact, xx66, yy66), Color.White,
                        0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    spriteBatch.Draw(salida_ani1, new Vector2(salida1x - xscroll - xx99, salida1y - yy99 - yscroll), new Rectangle(0, 0, salida_ani1.Width, salida_ani1.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                    if (debug) //exits debug
                    {
                        salida_rect = new Rectangle(salida1x - 5, salida1y - 5, 10, 10);
                        spriteBatch.Draw(texture1pixel, new Rectangle(salida_rect.Left - xscroll, salida_rect.Top - yscroll, salida_rect.Width, salida_rect.Height), null,
                            Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    }
                }
                else
                {
                    for (ex22 = 0; ex22 < numTOTexits; ex22++)
                    {
                        salida1x = (int)moreexits[ex22].exitmorexy.X;
                        salida1y = (int)moreexits[ex22].exitmorexy.Y;
                        spriteBatch.Draw(salida_ani1_1, new Vector2(salida1x - xscroll - xx88, salida1y - yy88 - yscroll), new Rectangle(0, frameact, xx66, yy66), Color.White,
                            0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                        spriteBatch.Draw(salida_ani1, new Vector2(salida1x - xscroll - xx99, salida1y - yy99 - yscroll), new Rectangle(0, 0, salida_ani1.Width, salida_ani1.Height),
                            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
                        if (debug) //exits debug
                        {
                            salida_rect = new Rectangle(salida1x - 5, salida1y - 5, 10, 10);
                            spriteBatch.Draw(texture1pixel, new Rectangle(salida_rect.Left - xscroll, salida_rect.Top - yscroll, salida_rect.Width, salida_rect.Height), null,
                                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                        }
                    }
                }
                // infos various for test only
                if (debug)
                {
                    spriteBatch.DrawString(Font1, string.Format("FPS={0}", _fps), new Vector2(960, 50), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                    spriteBatch.DrawString(Font1, sposicionMouse, new Vector2(940, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                }

                for (actLEM = 0; actLEM < numerosaca; actLEM++) //si lo hace de 100 a cero dibujara los primeros encima y mejorara el aspecto
                {
                    if (puertaon)
                        break;
                    if (lemming[actLEM].Dead)
                        continue;
                    if (lemming[actLEM].Explota && !lemming[actLEM].Explotando)
                    {
                        if (lemming[actLEM].Time == 0)
                            lemming[actLEM].Time = tiempototal;
                        double timez = tiempototal - lemming[actLEM].Time;
                        crono = (int)(6f - (float)timez);
                        TextLem(string.Format("{0}", crono), new Vector2(lemming[actLEM].Posx + 3 - xscroll, lemming[actLEM].Posy - 10 - yscroll), Color.White, 0.4f, 0.000000000004f);
                        if (crono <= 0)
                        {
                            // luto luto sound monogame 3.2 works ok without catch exception
                            if (ohnoInstance.State == SoundState.Playing)
                            {
                                ohnoInstance.Stop();
                            }
                            try
                            {
                                ohnoInstance.Play();
                            }
                            catch (InstancePlayLimitException) { /* Ignore errors */ }
                            lemming[actLEM].Explotando = true;
                            lemming[actLEM].Activo = false;
                            lemming[actLEM].Umbrella = false;
                            lemming[actLEM].Walker = false;
                            lemming[actLEM].Digger = false;
                            lemming[actLEM].Escalar = false;
                            lemming[actLEM].Fall = false;
                            lemming[actLEM].Falling = false;
                            lemming[actLEM].Escalando = false;
                            lemming[actLEM].Exit = false;
                            lemming[actLEM].Blocker = false;
                            lemming[actLEM].Builder = false;
                            lemming[actLEM].Puentenomas = false;
                            lemming[actLEM].Basher = false;
                            lemming[actLEM].Miner = false;
                            lemming[actLEM].Actualframe = 0;
                            lemming[actLEM].Numframes = bomber_frames;
                        }
                    }
                    framereal55 = (lemming[actLEM].Actualframe * 118);
                    if (lemming[actLEM].Quemado) // scale POSDraw x+0,y+0 at 1.2f x-5,y+0 at 1.35f
                    {
                        spriteBatch.Draw(squemado, new Vector2(lemming[actLEM].Posx - xscroll - 5, lemming[actLEM].Posy - yscroll), new Rectangle(0, lemming[actLEM].Actualframe * 28, 32, 28),
                            (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        spriteBatch.Draw(lhiss, new Vector2(lemming[actLEM].Posx - xscroll, lemming[actLEM].Posy - 20 - yscroll), new Rectangle(0, 0, lhiss.Width, lhiss.Height),
                            Color.White, 0f, Vector2.Zero, (0.5f + (0.01f * lemming[actLEM].Actualframe)), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Ahoga) // scale POSDraw x+0,y+10 at 1.2f x-8,y+7 at 1.35f  //puto ahoga
                    {
                        spriteBatch.Draw(sahoga, new Vector2(lemming[actLEM].Posx - xscroll + water_xpos, lemming[actLEM].Posy + water_ypos - yscroll), new Rectangle(lemming[actLEM].Actualframe * water_with, 0, water_with, water_height),
                            (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, water_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Walker)
                    {
                        framereal55 = (lemming[actLEM].Actualframe * walker_with);
                        spriteBatch.Draw(walker2, new Vector2((lemming[actLEM].Posx - xscroll + walker_xpos), lemming[actLEM].Posy - yscroll + walker_ypos), new Rectangle(framereal55, 0, walker_with, walker_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, walker_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Blocker) // blocker scale POSDraw x-5 y+4 at 1.2f x-7 y+1 at 1.35f  //puto
                    {
                        framesale = (lemming[actLEM].Actualframe * blocker_with);
                        spriteBatch.Draw(blocker, new Vector2(lemming[actLEM].Posx - xscroll + blocker_xpos, lemming[actLEM].Posy + blocker_ypos - yscroll), new Rectangle(framesale, 0, blocker_with, blocker_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, blocker_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        if (debug)
                        {
                            bloqueo = new Rectangle(lemming[actLEM].Posx, lemming[actLEM].Posy, 28, 28);
                            spriteBatch.Draw(texture1pixel, new Rectangle(bloqueo.Left - xscroll, bloqueo.Top - yscroll, bloqueo.Width, bloqueo.Height), null,
                                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                        }
                    }
                    if (lemming[actLEM].Puentenomas) // scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f
                    {
                        framesale = (lemming[actLEM].Actualframe * 26);
                        spriteBatch.Draw(puente_nomas, new Vector2(lemming[actLEM].Posx - xscroll - 7, lemming[actLEM].Posy - 7 - yscroll), new Rectangle(0, framesale, 32, 26), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeL, (lemming[actLEM].Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Builder)  //scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f  builder builder draws
                    {
                        if (lemming[actLEM].Numstairs >= 10) // chink draws
                        {
                            spriteBatch.Draw(lchink, new Vector2(lemming[actLEM].Posx - xscroll - 10, lemming[actLEM].Posy - 30 - yscroll), new Rectangle(0, 0, lchink.Width, lchink.Height),
                                Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        }
                        framesale = (lemming[actLEM].Actualframe * builder_with);
                        spriteBatch.Draw(puente, new Vector2(lemming[actLEM].Posx - xscroll + builder_xpos, lemming[actLEM].Posy + builder_ypos - yscroll), new Rectangle(framesale, 0, builder_with, builder_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, builder_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Miner)  //scale POSDraw x-5,y-2 at 1.2f x-9,y-7 at 1.35f pico pico miner miner
                    {
                        framesale = (lemming[actLEM].Actualframe * pico_with);
                        spriteBatch.Draw(pico, new Vector2(lemming[actLEM].Posx - xscroll + pico_xpos + (lemming[actLEM].Right ? 0 : 10), lemming[actLEM].Posy + pico_ypos - yscroll), new Rectangle(framesale, 0, pico_with, pico_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, pico_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Basher) //puto
                    {           // scale basher RIGHT POSDRAW x-10,y+4 at 1.2f x-15,y+1 at 1.35f
                        framesale = (lemming[actLEM].Actualframe * basher_with);
                        spriteBatch.Draw(pared, new Vector2(lemming[actLEM].Posx - xscroll + (lemming[actLEM].Right ? basher_xpos : basher_xposleft), lemming[actLEM].Posy + basher_ypos - yscroll), new Rectangle(framesale, 0, basher_with, basher_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, basher_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Explotando) // explotando explotando bomber bomber
                    {
                        // bomber scale POSDraw x-5,y+4 at 1.2f x-9,y+2 at 1.35f
                        framesale = (lemming[actLEM].Actualframe * bomber_with);
                        spriteBatch.Draw(explota, new Vector2(lemming[actLEM].Posx - xscroll + bomber_xpos, lemming[actLEM].Posy + bomber_ypos - yscroll), new Rectangle(framesale, 0, bomber_with, bomber_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, bomber_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        spriteBatch.Draw(lohno, new Vector2(lemming[actLEM].Posx - xscroll - 20, lemming[actLEM].Posy - 25 - yscroll), new Rectangle(0, 0, lohno.Width, lohno.Height),
                            Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * lemming[actLEM].Actualframe), SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Breakfloor) // scale POSDraw x-5,y+4 at 1.2f  x-9,y+2 at 1.35f breakfloor breakfloor
                    {
                        framesale = (lemming[actLEM].Actualframe * floor_with);
                        spriteBatch.Draw(rompesuelo, new Vector2(lemming[actLEM].Posx - xscroll + floor_xpos, lemming[actLEM].Posy + floor_ypos - yscroll), new Rectangle(framesale, 0, floor_with, floor_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, floor_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                        if (lemming[actLEM].Actualframe == floor_frames - 1)
                        {
                            lemming[actLEM].Dead = true;
                            numlemnow--;
                            lemming[actLEM].Explotando = false;
                            lemming[actLEM].Explota = false;
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
                        spriteBatch.Draw(paraguas, new Vector2(lemming[actLEM].Posx - xscroll + floater_xpos, lemming[actLEM].Posy + floater_ypos - yscroll), new Rectangle(framesale, 0, floater_with, floater_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, floater_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Fall) //fall cae
                    {
                        framereal55 = (lemming[actLEM].Actualframe * faller_with);
                        spriteBatch.Draw(cae, new Vector2(lemming[actLEM].Posx - xscroll + faller_xpos, lemming[actLEM].Posy - yscroll + faller_ypos), new Rectangle(framereal55, 0, faller_with, faller_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, faller_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Exit && !lemming[actLEM].Dead) //sale sale exit exit out out
                    {
                        framesale = (lemming[actLEM].Actualframe * sale_with); // exit scale POSDraw  x-1,y+1 at 1.2f x-3,y-1 at 1.35f
                        spriteBatch.Draw(sale, new Vector2(lemming[actLEM].Posx - xscroll + sale_xpos, lemming[actLEM].Posy + sale_ypos - yscroll), new Rectangle(framesale, 0, sale_with, sale_height), Color.White, 0f, Vector2.Zero, sale_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                    if (lemming[actLEM].Digger)
                    {
                        framereal55 = (lemming[actLEM].Actualframe * digger_with);
                        spriteBatch.Draw(digger, new Vector2(lemming[actLEM].Posx - xscroll + digger_xpos, lemming[actLEM].Posy + 6 - yscroll + digger_ypos), new Rectangle(framereal55, 0, digger_with, digger_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, digger_size, SpriteEffects.None, Lem_depth + (actLEM * 0.00001f));
                    }

                    if (lemming[actLEM].Escalando) // scale POSDraw x-5,y+6 at 1.2f x-8.y+3 at 1.35f  //puto33
                    {
                        framesale = (lemming[actLEM].Actualframe * climber_with);
                        spriteBatch.Draw(escala, new Vector2(lemming[actLEM].Posx - xscroll + (lemming[actLEM].Right ? climber_xpos : climber_xposleft), lemming[actLEM].Posy + climber_ypos - yscroll), new Rectangle(framesale, 0, climber_with, climber_height), (lemming[actLEM].Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, climber_size, (lemming[actLEM].Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), Lem_depth + (actLEM * 0.00001f));
                    }
                }
                if (fade)
                {
                    rest++;
                    rest2 = rest * 7;
                    if (rest2 < 70)
                        rest2 = 0;
                    DrawLine(spriteBatch, new Vector2(0, 0), new Vector2(gameResolution.X, 0), new Color(0, 0, 0, 255 - rest2), gameResolution.Y, 0f);
                    if (Frame > 19)
                    {
                        fade = false;
                        rest = 0;
                        tiempototal = 0;
                        if (initInstance.State == SoundState.Stopped && !initON)
                        {
                            initInstance.Play();
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

                            vectorFill.X = (float)Explosion[Qexplo, Iexplo].x - xscroll;
                            vectorFill.Y = (float)Explosion[Qexplo, Iexplo].y - yscroll;
                            spriteBatch.Draw(explosion_particle, vectorFill, new Rectangle(0, 0, explosion_particle.Width, explosion_particle.Height), Explosion[Qexplo, Iexplo].Color,
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
                spriteBatch.Draw((mouseOnLem ? ratonon : ratonoff), new Vector2(mousepos.X, mousepos.Y), new Rectangle(0, 0, 34, 34), Color.White, 0f, Vector2.Zero,
                    1f, SpriteEffects.None, 0f);
                spriteBatch.End();

                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(letterboxingColor);

                spriteBatch.Begin();
                spriteBatch.Draw(renderTarget, renderTargetDestination, Color.White);
                spriteBatch.End();
            }
            if (MainMenu)
            {
                // rainbow over lemmings logo text into rendertarget
                GraphicsDevice.SetRenderTarget(colors88);
                GraphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 1.0f, 0);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
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
                spriteBatch.Draw(text, new Vector2(0, 0), Color.White);
                spriteBatch.End();
                GraphicsDevice.SetRenderTarget(null);

                // light NMAP effect over lemmings logo with mouse pos into other rendertarget
                Vector2 cratePosition = new Vector2(215, 20);
                // Draw all the normals, in the same place as the textures
                GraphicsDevice.SetRenderTarget(normals);
                //GraphicsDevice.Clear(ClearOptions.Target, new Color(128, 128, 255, 255), 1.0f, 0); // Clear the target with the default normal, pointing up (0, 0, 1)
                GraphicsDevice.Clear(ClearOptions.Target,
                    new Color(128, 128, 255, 255), 1.0f, 0); // Clear the target with the default normal, pointing up (0, 0, 1)
                spriteBatch.Begin();
                spriteBatch.Draw(crateNormals, cratePosition, Color.White);
                spriteBatch.End();
                // Draw the lighting
                GraphicsDevice.SetRenderTarget(lighting);
                GraphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 1.0f, 0);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                // Set the basic transform matrix from XNA
                Matrix projection = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1);
                Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);
                lightEffect.Parameters["MatrixTransform"].SetValue(halfPixelOffset * projection);
                // Set parameters
                lightEffect.Parameters["screenSize"].SetValue(new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));
                lightEffect.Parameters["InverseVP"].SetValue(Matrix.Identity); // Since we have no view matrix, set it to identity
                lightEffect.Parameters["LightPosition"].SetValue(lightPosition);
                lightEffect.Parameters["LightRadius"].SetValue(lightRadius);
                lightEffect.Parameters["LightColor"].SetValue(lightColor.ToVector4());
                lightEffect.Parameters["LightIntensity"].SetValue(lightIntensity);
                // Apply the effect
                lightEffect.CurrentTechnique.Passes[0].Apply();
                // Create a rectangle around the lightposition
                Rectangle rect22 = new Rectangle((int)lightPosition.X - (int)lightRadius, (int)lightPosition.Y - (int)lightRadius, (int)lightRadius * 2, (int)lightRadius * 2);
                // Draw the normal buffer with every pointlight 
                // Possible optimization: set the buffer only once, since they all use the same effect.
                spriteBatch.Draw(normals, rect22, lightColor);
                spriteBatch.End();
                GraphicsDevice.SetRenderTarget(null);

                //normal target
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, null, null);
                GraphicsDevice.Clear(Color.Black); //new Color(255, 0, 255, 255)
                mmstartx = 5;
                mmstarty = 80;
                mmX = 135;
                x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                Rectangle mm1 = new Rectangle(mmstartx, mmstarty, mainMenuSign.Width, mainMenuSign.Height);
                if (mm1.Contains(x))
                {
                    mmop1 = true;
                    mmop2 = false;
                    mmop3 = false;
                    mmop4 = false;
                    mmop5 = false;
                    mmop6 = false;
                    mmlevchoose = 0;
                }
                Rectangle mm2 = new Rectangle(mmstartx, mmstarty + 100, mainMenuSign.Width, mainMenuSign.Height);
                if (mm2.Contains(x))
                {
                    mmop1 = false;
                    mmop2 = true;
                    mmop3 = false;
                    mmop4 = false;
                    mmop5 = false;
                    mmop6 = false;
                    mmlevchoose = 0;
                }
                Rectangle mm3 = new Rectangle(mmstartx, mmstarty + 200, mainMenuSign.Width, mainMenuSign.Height);
                if (mm3.Contains(x))
                {
                    mmop1 = false;
                    mmop2 = false;
                    mmop3 = true;
                    mmop4 = false;
                    mmop5 = false;
                    mmop6 = false;
                    mmlevchoose = 0;
                }
                Rectangle mm4 = new Rectangle(mmstartx, mmstarty + 300, mainMenuSign.Width, mainMenuSign.Height);
                if (mm4.Contains(x))
                {
                    mmop1 = false;
                    mmop2 = false;
                    mmop3 = false;
                    mmop4 = true;
                    mmop5 = false;
                    mmop6 = false;
                    mmlevchoose = 0;
                }
                Rectangle mm5 = new Rectangle(mmstartx, mmstarty + 400, mainMenuSign.Width, mainMenuSign.Height);
                if (mm5.Contains(x))
                {
                    mmop1 = false;
                    mmop2 = false;
                    mmop3 = false;
                    mmop4 = false;
                    mmop5 = true;
                    mmop6 = false;
                    mmlevchoose = 0;
                }
                Rectangle mm6 = new Rectangle(mmstartx, mmstarty + 500, mainMenuSign.Width, mainMenuSign.Height);
                if (mm6.Contains(x))
                {
                    mmop1 = false;
                    mmop2 = false;
                    mmop3 = false;
                    mmop4 = false;
                    mmop5 = false;
                    mmop6 = true;
                    mmlevchoose = 0;
                }
                spriteBatch.Draw(logo_fondo, new Rectangle(0, 0, gameResolution.X, gameResolution.Y), new Rectangle(0, 0, gameResolution.X, gameResolution.Y), new Color(255, 255, 255, 100));
                if (debug)
                {
                    spriteBatch.DrawString(Font1, string.Format("numero={0}", mmlevchoose), new Vector2(960, 50), Color.White);
                    spriteBatch.DrawString(Font1, sposicionMouse, new Vector2(940, 10), Color.White);
                }
                spriteBatch.Draw(backlogo, new Vector2(215, 20), Color.White);
                spriteBatch.Draw(blink1, new Vector2(239, 58), new Rectangle(0, framblink1 * 12, blink1.Width, 12), Color.White,
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
                spriteBatch.Draw(blink2, new Vector2(463, 58), new Rectangle(0, framblink2 * 12, blink2.Width, 12), Color.White,
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 0.104f);
                spriteBatch.Draw(blink3, new Vector2(703, 50), new Rectangle(0, framblink3 * 12, blink3.Width, 12), Color.White,
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
                spriteBatch.Draw(foregroundTexture, rectangleFill, rectangleFill2, colorFill);
                rectangleFill2.X = 0 - (int)frameWater;
                rectangleFill2.Y = 100;
                rectangleFill2.Width = gameResolution.X;
                rectangleFill2.Height = gameResolution.Y;
                colorFill.A = 80;
                spriteBatch.Draw(foregroundTexture, rectangleFill, rectangleFill2, colorFill); // second wave position depth by order of draw
                if (particle != null)
                {
                    rectangleFill.X = 0;
                    rectangleFill.Y = 0;
                    rectangleFill.Width = 10;
                    rectangleFill.Height = 10;
                    for (varParticle = 0; varParticle < numParticles; varParticle++)
                    {
                        spriteBatch.Draw(particle[varParticle].Sprite, particle[varParticle].Pos, rectangleFill, Color.Magenta, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.90001f);
                    }
                }
                if (mmop1)
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty), new Color(255, 255, 255, 255));
                }
                else
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty), new Color(80, 80, 80, 255));
                }
                if (mmop2)
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 100), new Color(255, 255, 255, 255));
                    spriteBatch.Draw(ranksign3, new Vector2(mmstartx + 34, mmstarty + 125), new Color(255, 255, 255, 255));
                }
                else
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 100), new Color(80, 80, 80, 255));
                    spriteBatch.Draw(ranksign3, new Vector2(mmstartx + 34, mmstarty + 125), new Color(80, 80, 80, 255));
                }
                if (mmop3)
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 200), new Color(255, 255, 255, 255));
                    spriteBatch.Draw(ranksign2, new Vector2(mmstartx + 34, mmstarty + 225), new Color(255, 255, 255, 255));
                }
                else
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 200), new Color(80, 80, 80, 255));
                    spriteBatch.Draw(ranksign2, new Vector2(mmstartx + 34, mmstarty + 225), new Color(80, 80, 80, 255));
                }
                if (mmop4)
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 300), new Color(255, 255, 255, 255));
                    spriteBatch.Draw(ranksign1, new Vector2(mmstartx + 34, mmstarty + 325), new Color(255, 255, 255, 255));
                }
                else
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 300), new Color(80, 80, 80, 255));
                    spriteBatch.Draw(ranksign1, new Vector2(mmstartx + 34, mmstarty + 325), new Color(80, 80, 80, 255));
                }
                if (mmop5)
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 400), new Color(255, 255, 255, 255));
                    spriteBatch.Draw(ranksign5, new Vector2(mmstartx + 34, mmstarty + 425), new Color(255, 255, 255, 255));
                }
                else
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 400), new Color(80, 80, 80, 255));
                    spriteBatch.Draw(ranksign5, new Vector2(mmstartx + 34, mmstarty + 425), new Color(80, 80, 80, 255));
                }
                if (mmop6)
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 500), new Color(255, 255, 255, 255));
                    spriteBatch.Draw(ranksign6, new Vector2(mmstartx + 34, mmstarty + 525), new Color(255, 255, 255, 255));
                }
                else
                {
                    spriteBatch.Draw(mainMenuSign, new Vector2(mmstartx, mmstarty + 500), new Color(80, 80, 80, 255));
                    spriteBatch.Draw(ranksign6, new Vector2(mmstartx + 34, mmstarty + 525), new Color(80, 80, 80, 255));
                }
                if (mmop1)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 31; s++)
                    {
                        Rectangle mmlev = new Rectangle(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                    myTexture = Content.Load<Texture2D>("levels/mini_levels1");
                    spriteBatch.Draw(myTexture, new Vector2(mmX, 130), Color.White);
                }
                if (mmop2)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX; mmy = 130; x = new Point(mouseActState.Position.X, mouseActState.Position.Y); mmlevchoose = 0;
                    for (s = 1; s < 31; s++)
                    {
                        Rectangle mmlev = new Rectangle(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = 30 + s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
                                colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                            break;
                        }
                        mmx += 135;
                        if (s % 7 == 0)
                        {
                            mmx = mmX; mmy += 70;
                        }
                    }
                    myTexture = Content.Load<Texture2D>("levels/mini_levels2");
                    spriteBatch.Draw(myTexture, new Vector2(mmX, 130), Color.White);
                }
                if (mmop3)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 31; s++)
                    {
                        Rectangle mmlev = new Rectangle(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = 60 + s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                    myTexture = Content.Load<Texture2D>("levels/mini_levels3");
                    spriteBatch.Draw(myTexture, new Vector2(mmX, 130), Color.White);
                }
                if (mmop4)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 31; s++)
                    {
                        Rectangle mmlev = new Rectangle(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = 90 + s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                    myTexture = Content.Load<Texture2D>("levels/mini_levels4");
                    spriteBatch.Draw(myTexture, new Vector2(mmX, 130), Color.White);
                }
                if (mmop5)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 37; s++)
                    {
                        Rectangle mmlev = new Rectangle(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = 120 + s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                    spriteBatch.Draw(myTexture, new Vector2(mmX, 130), Color.White);
                }
                if (mmop6)
                {
                    colorFill.R = 0;  // black with transparency at 170
                    colorFill.G = 0;
                    colorFill.B = 0;
                    colorFill.A = 170;
                    spriteBatch.Draw(texture1pixel, new Rectangle(mmX - 10, 130, 955, 420), null, // 7 x 6 mini levels maps
                        colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(mainMenuSign2, new Rectangle(-110, 15, 1429, 638), null, // 7 x 6 mini levels maps
                        Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    mmx = mmX;
                    mmy = 130;
                    x = new Point(mouseActState.Position.X, mouseActState.Position.Y);
                    mmlevchoose = 0;
                    for (s = 1; s < 26; s++) //number user levels to show okok be careful
                    {
                        Rectangle mmlev = new Rectangle(mmx, mmy, 130, 55);
                        if (mmlev.Contains(x))
                        {
                            mmlevchoose = 156 + s;
                            if (LevelEnd[mmlevchoose])
                                colorFill = Color.ForestGreen;
                            else
                                colorFill = Color.Red;
                            spriteBatch.Draw(texture1pixel, new Rectangle(mmx, mmy, 130, 55), null, // 7 x 6 mini levels maps
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
                }
                if (mmlevchoose != 0 && mmlevchoose <= numTotalLevels - 1) // MENU SHOW LEVELS DETAILS
                {
                    mmKX = 100;
                    mmKY = 555;
                    mmKplusY = 27;
                    levelACT = mmlevchoose;
                    if (levelACT > 30 && levelACT <= 60)
                        levelACT -= 30;
                    if (levelACT > 60 && levelACT <= 90)
                        levelACT -= 60;
                    if (levelACT > 90 && levelACT <= 120)
                        levelACT -= 90;
                    if (levelACT > 120 && levelACT <= 156)
                        levelACT -= 120;
                    if (levelACT > 156)
                        levelACT -= 156;
                    TextLem("Level " + string.Format("{0}", levelACT), new Vector2(mmKX, mmKY), Color.Red, 1f, 0.1f);
                    TextLem(level[mmlevchoose].nameOfLevel, new Vector2(mmKX + 200, mmKY), Color.Red, 1f, 0.1f);
                    TextLem("Number of Lemmings " + string.Format("{0}", level[mmlevchoose].numTotalLem), new Vector2(mmKX, mmKY + mmKplusY), Color.Blue, 1f, 0.1f);
                    TextLem(string.Format("{0}", level[mmlevchoose].lemsToSave) + " to be saved", new Vector2(mmKX, mmKY + mmKplusY * 2), Color.Green, 1f, 0.1f);
                    TextLem("Release Rate " + string.Format("{0}", level[mmlevchoose].minNumberFrecuency), new Vector2(mmKX, mmKY + mmKplusY * 3), Color.Yellow, 1f, 0.1f);
                    TextLem("Time " + string.Format("{0}", level[mmlevchoose].totalTime) + " Minutes", new Vector2(mmKX, mmKY + mmKplusY * 4), Color.Cyan, 1f, 0.1f);
                    if (mmlevchoose <= 30)
                    {
                        TextLem("Rating FUN", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    if (mmlevchoose > 30 && mmlevchoose <= 60)
                    {
                        TextLem("Rating TRICKY", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    if (mmlevchoose > 60 && mmlevchoose <= 90)
                    {
                        TextLem("Rating TAXING", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    if (mmlevchoose > 90 && mmlevchoose <= 120)
                    {
                        TextLem("Rating MAYHEM", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    if (mmlevchoose > 120 && mmlevchoose <= 156)
                    {
                        TextLem("Rating BONUS", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    if (mmlevchoose > 156)
                    {
                        TextLem("Rating USER", new Vector2(mmKX + 470, mmKY + mmKplusY * 4), Color.Magenta, 1f, 0.1f);
                    }
                    mmKindX = 960;
                    mmKindY = 580;
                    mmPlusy = 15;
                    TextLem("Climbers: " + string.Format("{0}", level[mmlevchoose].numberClimbers), new Vector2(mmKindX, mmKindY), Color.Linen, 0.5f, 0.1f);
                    TextLem("Floaters: " + string.Format("{0}", level[mmlevchoose].numberUmbrellas), new Vector2(mmKindX, mmKindY + mmPlusy), Color.LimeGreen, 0.5f, 0.1f);
                    TextLem(" Bombers: " + string.Format("{0}", level[mmlevchoose].numberExploders), new Vector2(mmKindX, mmKindY + mmPlusy * 2), Color.SteelBlue, 0.5f, 0.1f);
                    TextLem("Blockers: " + string.Format("{0}", level[mmlevchoose].numberBlockers), new Vector2(mmKindX, mmKindY + mmPlusy * 3), Color.Red, 0.5f, 0.1f);
                    TextLem("Builders: " + string.Format("{0}", level[mmlevchoose].numberBuilders), new Vector2(mmKindX, mmKindY + mmPlusy * 4), Color.Orange, 0.5f, 0.1f);
                    TextLem(" Bashers: " + string.Format("{0}", level[mmlevchoose].numberBashers), new Vector2(mmKindX, mmKindY + mmPlusy * 5), Color.Violet, 0.5f, 0.1f);
                    TextLem("  Miners: " + string.Format("{0}", level[mmlevchoose].numberMiners), new Vector2(mmKindX, mmKindY + mmPlusy * 6), Color.Turquoise, 0.5f, 0.1f);
                    TextLem(" Diggers: " + string.Format("{0}", level[mmlevchoose].numberDiggers), new Vector2(mmKindX, mmKindY + mmPlusy * 7), Color.Tomato, 0.5f, 0.1f);
                }

                if (mmop6) //6 seconds to load all 30 images  better make a full png image of all mini levels and then use it...
                {          // the first load is slow then it remains on gpu memory cache and goes really fast
                    mmx = mmX;
                    mmy = 130;
                    for (s = 1; s < 26; s++) //number user levels to show okok be careful
                    {
                        myTexture = Content.Load<Texture2D>("levels/user/user" + string.Format("{0,3:D3}", s));
                        spriteBatch.Draw(myTexture, new Rectangle(mmx, mmy, 130, 55), new Rectangle(0, 0, myTexture.Width, myTexture.Height), Color.White);
                        mmx += 135;
                        if (s % 7 == 0)
                        {
                            mmx = mmX;
                            mmy += 70;
                        }
                    }
                }
                spriteBatch.Draw(colors88, new Vector2(560, 480), new Rectangle(0, 0, colors88.Width, colors88.Height), Color.White, 0f, Vector2.Zero, .8f,
                    SpriteEffects.None, 0.0001f);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                spriteBatch.Draw(lighting, new Vector2(0, 0), new Color(1, 1, 1, 0.01f));
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}

