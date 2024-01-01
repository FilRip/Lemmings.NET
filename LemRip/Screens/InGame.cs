using System;
using System.Collections.Generic;
using System.Linq;

using Lemmings.NET.Constants;
using Lemmings.NET.Datatables;
using Lemmings.NET.Helpers;
using Lemmings.NET.Models;
using Lemmings.NET.Models.Particles;
using Lemmings.NET.Models.Props;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens;

internal class InGame
{
    #region Properties

    internal int ScrollX { get; set; }
    internal int ScrollY { get; set; }
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
    internal int R1 { get; set; }
    internal int R2 { get; set; }
    internal int R3 { get; set; }
    internal int ZvTime { get; set; }
    internal bool Fade { get; set; } = true;
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
    internal int Front2 { get; set; }
    internal int Front { get; set; }
    internal bool MouseOnLem { get; set; }
    internal bool Draw_walker { get; set; }
    internal bool Draw_builder { get; set; }
    internal Color[] ColorExploderMask { get; set; } = new Color[38 * 53]; // explode mask 38*53
    internal Color[] ColorBasherMask { get; set; } = new Color[20 * 20];  // miner mask 20*20 && basher too 20*20
    internal Color[] ColorOver { get; set; } = new Color[20 * 20];
    internal Color[] LevelOverlay { get; set; } = new Color[4096 * 4096]; // Maximun size of a color array used for mask all the level
    internal Color[] Colormask22 { get; set; } = new Color[500 * 512];
    internal int NumSaved
    {
        get
        {
            return AllLemmings?.Count(l => l.Exit) ?? 0;
        }
    }
    internal Color[] ClownLevelOverlay { get; set; } = new Color[38 * 53];
    internal OneLevel CurrentLevel { get; set; }
    internal bool ExitBad { get; set; }
    internal int Numlemnow { get; set; }
    internal int Lemsneeded { get; set; } = 1;
    internal EndLevel EndLevelScreen { get; set; }
    internal Dictionary<int, List<OneExplosion>> Explosion { get; set; }
    internal int FrameReal565 { get; set; }
    internal float DoorExitDepth { get; set; } = 0.403f;  // default value--bigger than 0.5f is behind the terrain (0.6f level 58 for example)
    internal Color[] Colorsobre22 { get; set; } = new Color[500 * 512];
    internal Color[] Colormasktotal { get; set; } = new Color[500 * 512];
    internal Texture2D Exit1Animation { get; set; }
    internal Texture2D Exit2Animation { get; set; }
    internal Texture2D AnimatedDoor { get; set; }
    internal int Z1 { get; set; }

    #endregion

    #region Fields

    private int _numActiveDoor;
    private float _counterTime2;
    private double _backgroundWave, _backgroundWave2, _backgroundWave3;
    private bool _drawing3, _levelEnded, _exitLevel, _backToMainMenu;
    private int _rest = 0, _counter2, _counter = 1;
    private bool _doorOn = true;
    private double _frameWaves;
    private int _walker_frame;
    private int _builder_frame;
    private readonly int _builder_frame_second = 1;
    private int _frame3;
    private readonly int _frameSecond = 6;
    private readonly int _frameSecond2 = 2;
    private readonly int _frameSecond3 = 1;  // frame speed less all go crazy 6->ok framesecond=6 default framesecond2=3 default
    private int _door1X, _door1Y;
    private int _output1X, _output1Y;
    private int _frameDoor, _frameExit; // 0--10   0--6
    private int _exitFrame = 999, _actualBlow; // frecuency lemmings go in
    private bool _initOn = false;
    private int _z2;
    private int _z3;
    private bool _luzmas = true, _luzmas2 = true;
    private int _totalNumLemmings = 1;
    private bool _doorWaveOn;
    private int _frameAct;
    private readonly bool _lockMouse;
    private readonly InGameMenu _inGameMenu;

    #endregion

    internal InGame()
    {
        _inGameMenu = new InGameMenu(this);
        EndLevelScreen = new EndLevel();
        _lockMouse = false;
    }

    internal void LoadLevel(int newLevel, ContentManager content)
    {
        if (MyGame.Instance.Music.WinMusic.State == SoundState.Playing)
            MyGame.Instance.Music.WinMusic.Stop();
        if (MyGame.Instance.Music.MenuMusic.State == SoundState.Playing)
            MyGame.Instance.Music.MenuMusic.Stop();
        CurrentLevel = Levels.GetLevel(newLevel);
        Numlemnow = 0;
        _frameDoor = 0;
        _frameExit = 0;
        _frame3 = 0;
        Fade = true;
        _doorOn = true;
        MillisecondsElapsed = 0;
        AnimatedDoor = content.Load<Texture2D>("puerta" + string.Format("{0}", CurrentLevel.TypeOfDoor)); // type of door puerta1-2-3-4 etc.
        string exitName = string.Format("{0}", CurrentLevel.TypeOfExit);
        Exit1Animation = content.Load<Texture2D>("salida" + exitName);
        Exit2Animation = content.Load<Texture2D>("salida" + exitName + "_1");
        MyGame.Instance.CurrentLevelNumber = newLevel;
        LemSkill = "";
        GlobalConst.Paused = false;
        ZvTime = 0;
        AllBlow = false;
        _actualBlow = 0;
        _exitFrame = 999;
        _inGameMenu.CurrentSelectedSkill = ECurrentSkill.NONE;
        _doorWaveOn = false;
        _initOn = false;
        _levelEnded = false;
        EndLevelScreen.EndSongPlayed = false;
        _exitLevel = false;
        _backToMainMenu = false;
        ExitBad = false;

        Texture2D level = MyGame.Instance.Content.Load<Texture2D>(CurrentLevel.NameLev);
        Earth = new Texture2D(MyGame.Instance.GraphicsDevice, level.Width, level.Height);
        Color[] pixels = new Color[level.Width * level.Height];
        level.GetData(pixels);
        Earth.SetData(pixels);
        Earth.GetData(LevelOverlay, 0, Earth.Height * Earth.Width); //better here than moverlemming() for performance see issues 
                                                                    //see differences with old getdata, see size important (x * y)
        _door1X = CurrentLevel.DoorX;
        _door1Y = CurrentLevel.DoorY;
        _output1X = CurrentLevel.ExitX;
        _output1Y = CurrentLevel.ExitY;
        // this is the depth of the exit and doors animated sprites -- See level 58 the exit is behind the mountain (0.6f)
        if (CurrentLevel.DoorExitDepth != 0)
        {
            DoorExitDepth = CurrentLevel.DoorExitDepth;
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
        _totalNumLemmings = CurrentLevel.TotalLemmings;
        Lemsneeded = CurrentLevel.NbLemmingsToSave;
        ScrollX = CurrentLevel.InitPosX;
        ScrollY = 0;
        AllLemmings = [];
        Explosion = [];
        for (int i = 0; i < GlobalConst.TotalExplosions; i++)
        {
            Explosion.Add(i, []);
            for (int j = 0; j < GlobalConst.PARTICLE_NUM; j++)
                Explosion[i].Add(new OneExplosion());
        }
    }

    private void Update_level()
    {
        _builder_frame++;
        _walker_frame++;
        _frameWaves++;
        Frame2++;
        _frame3++;
        Drawing = false;
        Draw2 = false;
        _drawing3 = false;
        Draw_walker = false;
        Draw_builder = false;
        if (_walker_frame > SizeSprites.walker_framesecond)
        {
            _walker_frame = 0;
            Draw_walker = true;
        }
        if (_builder_frame > _builder_frame_second)
        {
            _builder_frame = 0;
            Draw_builder = true;
        }
        if (Frame2 > _frameSecond)
        {
            Frame2 = 0;
            Drawing = true;
            if (!GlobalConst.Paused)
                Frame++;
        } //without this Frame affects door speed exit
        if (_frame3 > _frameSecond2)
        {
            _frame3 = 0;
            Draw2 = true;
        }
        if (_frameWaves > _frameSecond3)
        {
            _frameWaves = 0;
            _drawing3 = true;
            _backgroundWave3++;
        } // change add of actwaves to see differences in speed  +=2,+=5

        // stop all things for exit prepare
        if (_levelEnded)
        {
            GlobalConst.Paused = true;
        }

        MoverLemming();

        if (CurrentLevel.ListProps<OneScreenSprite>().Any())
        {
            foreach (OneScreenSprite spr in CurrentLevel.ListProps<OneScreenSprite>())
            {
                spr.Update();
            }
        }

        if (!GlobalConst.Paused &&
            CurrentLevel.ListProps<OnePlat>().Any())
        {
            foreach (OnePlat plat in CurrentLevel.ListProps<OnePlat>())
            {
                plat.Update();
            }
        }

        if (!GlobalConst.Paused && CurrentLevel.ListProps<OneAdd>().Any())
        {
            foreach (OneAdd add in CurrentLevel.ListProps<OneAdd>())
            {
                add.Update();
            }
        }
        if (CurrentLevel.ListProps<OneTrap>().Any() && Drawing && !GlobalConst.Paused)
        {
            foreach (OneTrap trap in CurrentLevel.ListProps<OneTrap>())
            {
                trap.Update();
            }
        }
        if (!GlobalConst.Paused)
        {
            Countertime++;
        }
        _counterTime2++;
        TotalTime = Countertime / 60; //real time of the level see to stop when finish or zvtime<0
        if (_doorOn)
        {
            Countertime = 0;
            TotalTime = 0;
        }
        int maxluz = 14; // numero de ciclos de variar el rectangle del EFECTO DE LUCES 50 normalmente
        int maxluz2 = 200;
        if (_luzmas2)
        {
            _counter2++;
            if (_counter2 >= maxluz2)
            {
                _counter2 = maxluz2 - 2;
                _luzmas2 = false;
            }
        }
        else
        {
            _counter2--;
            if (_counter2 <= 0)
            {
                _counter2 = 2;
                _luzmas2 = true;
            }
        }
        if ((_counterTime2 / 4) % 2 == 0) //velocidad del refresco efecto de luces
        {
            if (_luzmas)
            {
                _counter++;
                if (_counter >= maxluz)
                {
                    _counter = maxluz - 2;
                    _luzmas = false;
                }
            }
            else
            {
                _counter--;
                if (_counter <= 0)
                {
                    _counter = 2;
                    _luzmas = true;
                }
            }
        }// abajo calculos nubes nubes2 y waterfall
        Z1 = (int)_counterTime2 / 3;
        _z2 = (int)_counterTime2 / 10;
        _z3 = (int)_counterTime2 / 9;
        _z3 %= 4; // mumero de frames del agua a ver 4 de 5 que tiene la ultima esta vacia nose porque
        if (Drawing)
        {
            int xx66 = MyGame.Instance.Props.GetExit(CurrentLevel.TypeOfExit).NumFrame - 1;
            _frameExit++;
            if (_frameExit > xx66)
            {
                _frameExit = 0;
            }
        }
        if (!GlobalConst.Paused)
            Door();
        _inGameMenu.Update();
        MyTexture = MyGame.Instance.Content.Load<Texture2D>("luces/" + _counter);// okokokokokokokok

        if (Drawing && CurrentLevel.ListProps<OneArrow>().Any()) // dibuja or dibuja2 test performance-- this is the worst part of the code NEED OPTIMIZATION
        {
            foreach (OneArrow arrow in CurrentLevel.ListProps<OneArrow>())
            {
                arrow.Update();
            }
        }
    }

    private void MoverLemming() //lemmings logic called every update
    {
        MouseOnLem = false;  // scroll mouse on level landscape
        Scrolling();
        if (_doorOn)
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
        _backgroundWave2 = 50 * Math.Sin(_backgroundWave3 / 60);  // 50 height of the wave  // 60 length of it
        _backgroundWave = -70 * Math.Sin(_backgroundWave3 / -80); // 10,100 -70,100
        Rectangle rectFill = new(0, 0, GlobalConst.GameResolution.X, (int)(GlobalConst.GameResolution.Y * 0.732));
        if (MyGame.Instance.CurrentLevelNumber != 159)
        {
            spriteBatch.Draw(MyGame.Instance.Gfx.Logo_fondo, rectFill, rectFill, new Color(150, 150, 150, 160), 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
        }
        else
        {
            Rectangle rectSrc = new(0 + Z1, 0 - (int)_backgroundWave2, GlobalConst.GameResolution.X, GlobalConst.GameResolution.Y - 188);
            spriteBatch.Draw(MyGame.Instance.InGameMenuGfx.Logo666, rectFill, rectSrc, new Color(255, 255, 255, 250), 0f, Vector2.Zero, SpriteEffects.None, 0.8091f);
            Texture2D logo555 = MyGame.Instance.Content.Load<Texture2D>("fondos/ice outttt");
            rectSrc = new((int)_backgroundWave, (int)_backgroundWave, GlobalConst.GameResolution.X, GlobalConst.GameResolution.Y - 188);
            spriteBatch.Draw(logo555, rectFill, rectSrc, new Color(150, 150, 150, 120), 0f, Vector2.Zero, SpriteEffects.None, 0.806f);
        }
        if (CurrentLevel.ListProps<OneTrap>().Any()) //draw traps
        {
            foreach (OneTrap trap in CurrentLevel.ListProps<OneTrap>())
            {
                trap.Draw(spriteBatch);
            }
        }
        switch (MyGame.Instance.CurrentLevelNumber)  // effect draws water cascade,stars,etc...
        {
            case 1:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + _z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                rayLigths = false;
                break;
            case 4:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(1530 - ScrollX, -80, 260, 650), new Rectangle(0 + _z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(1560 - ScrollX, -80, 260, 750), new Rectangle(0 + _z3 * 192, 0, 192, 192), new Color(230, 50, 255, 160), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.803f); //0.802f  
                rayLigths = false;
                break;
            case 5:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(760 - ScrollX, -80, 260, 650), new Rectangle(0 + _z3 * 192, 0, 192, 192), new Color(50, 255, 240, 100), 0f,
                    Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f  
                break;
            case 6:
                spriteBatch.Draw(MyGame.Instance.Sprites.Water2, new Rectangle(2000 - ScrollX, -80, 260, 680), new Rectangle(0 + _z3 * 192, 0, 192, 192),
                    new Color(255, 50, 80, 170), 0f, Vector2.Zero, SpriteEffects.None, 0.802f); //0.802f                            
                break;
            default:
                break;
        }

        if (MyGame.Instance.CurrentLevelNumber != 159) //nubes clouds moving in background
        {
            if (rayLigths)
            {
                spriteBatch.Draw(MyTexture, new Vector2(GlobalConst.GameResolution.X / 2, (GlobalConst.GameResolution.Y - 188) / 2), new Rectangle(0, 0, MyTexture.Width, MyTexture.Height), new Color(255, 255, 255, 10 + _counter * 2),
                    0.4f + _counter2 * 0.001f, new Vector2(MyTexture.Width / 2, MyTexture.Height / 2), 3f, SpriteEffects.FlipHorizontally, 0.805f); // okokok
            }
            // rayligts effect
            spriteBatch.Draw(MyGame.Instance.Sprites.Nubes_2, new Rectangle(0, 50 - (int)_backgroundWave, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes_2.Height), new Rectangle(Z1, 0, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes_2.Height),
                new Color(255, 255, 255, 110), 0f, Vector2.Zero, SpriteEffects.None, 0.804f);

            spriteBatch.Draw(MyGame.Instance.Sprites.Nubes, new Rectangle(0, 220, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes.Height), new Rectangle(_z2, 0, GlobalConst.GameResolution.X, MyGame.Instance.Sprites.Nubes.Height), new Color(255, 255, 255, 110), 0f,
                Vector2.Zero, SpriteEffects.None, 0.803f);
        }
        spriteBatch.Draw(Earth, new Vector2(0, 0), new Rectangle(ScrollX, ScrollY, GlobalConst.GameResolution.X, GlobalConst.GameResolution.Y - 188), //512 size of window draw
            Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.500f);
        if (CurrentLevel.ListProps<OneArrow>().Any())
        {
            foreach (OneArrow arrow in CurrentLevel.ListProps<OneArrow>())
            {
                arrow.Draw(spriteBatch);
            }
        }

        //menu for ending level or not
        if (_levelEnded)
        {
            EndLevelScreen.Draw(spriteBatch);
        }

        OneEntry entry = MyGame.Instance.Props.GetEntry(CurrentLevel.TypeOfDoor);
        FrameReal565 = (_frameDoor * entry.Height);

        if (CurrentLevel.ListProps<OneScreenSprite>().Any())
        {
            foreach (OneScreenSprite spr in CurrentLevel.ListProps<OneScreenSprite>())
            {
                spr.Draw(spriteBatch);
            }
        }

        if (CurrentLevel.ListProps<OnePlat>().Any())
        {
            foreach (OnePlat plat in CurrentLevel.ListProps<OnePlat>())
            {
                plat.Draw(spriteBatch);
            }
        }
        if (CurrentLevel.ListProps<OneMoreDoor>().Any())
        {
            foreach (OneMoreDoor moreDoor in CurrentLevel.ListProps<OneMoreDoor>())
            {
                moreDoor.Draw(spriteBatch, entry.Width, entry.Height);
            }
        }
        else
        {
            spriteBatch.Draw(AnimatedDoor, new Vector2(_door1X - ScrollX, _door1Y - ScrollY), new Rectangle(0, FrameReal565, entry.Width, entry.Height),
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
        }
        OneExit exit = MyGame.Instance.Props.GetExit(CurrentLevel.TypeOfExit);
        int x1 = exit.Width;
        int y1 = exit.Height;
        int x2 = exit.MoreX;
        int y2 = exit.MoreY;
        int x3 = exit.MoreX2;
        int y3 = exit.MoreY2;
        _frameAct = (_frameExit * y1);
        if (CurrentLevel.ListProps<OneMoreExit>().Any())
        {
            foreach (OneMoreExit moreExit in CurrentLevel.ListProps<OneMoreExit>())
            {
                moreExit.Draw(spriteBatch, x1, y1, x2, y2, x3, y3, _frameAct);
            }
        }
        else
        {
            spriteBatch.Draw(Exit2Animation, new Vector2(_output1X - ScrollX - x2, _output1Y - y2 - ScrollY), new Rectangle(0, _frameAct, x1, y1), Color.White,
                0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            spriteBatch.Draw(Exit1Animation, new Vector2(_output1X - ScrollX - x3, _output1Y - y3 - ScrollY), new Rectangle(0, 0, Exit1Animation.Width, Exit1Animation.Height),
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, DoorExitDepth);
            if (MyGame.Instance.DebugOsd.Debug) //exits debug
            {
                Rectangle exitRect = new(_output1X - 5, _output1Y - 5, 10, 10);
                spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(exitRect.Left - ScrollX, exitRect.Top - ScrollY, exitRect.Width, exitRect.Height),
                    null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            }
        }
        // infos various for test only

        if (!_doorOn)
            foreach (OneLemming lemming in AllLemmings) //si lo hace de 100 a cero dibujara los primeros encima y mejorara el aspecto
                lemming.Draw(spriteBatch);

        if (Fade)
        {
            _rest++;
            int rest2 = _rest * 7;
            if (rest2 < 70)
                rest2 = 0;
            MyGame.Instance.Gfx.DrawLine(spriteBatch, new Vector2(0, 0), new Vector2(GlobalConst.GameResolution.X, 0), new Color(0, 0, 0, 255 - rest2), GlobalConst.GameResolution.Y, 0f);
            if (Frame > 19)
            {
                Fade = false;
                _rest = 0;
                TotalTime = 0;
                if (MyGame.Instance.Sfx.Letsgo.State == SoundState.Stopped && !_initOn)
                {
                    MyGame.Instance.Sfx.Letsgo.Play();
                    _initOn = true;
                }
            }

        }
        if (Exploding) // draws explosions particles explosion_particle
        {
            foreach (List<OneExplosion> listExplosions in Explosion.Values)
            {
                foreach (OneExplosion explosion in listExplosions)
                {
                    explosion.Draw(spriteBatch);
                }
            }
        }
        if (!MouseOnLem)
        {
            LemSkill = "";
        }

        MyGame.Instance.Fonts.TextLem("Home:" + string.Format("{0}", NumSaved) + "/" + string.Format("{0}", Lemsneeded), new Vector2(650, 518), Color.Cyan, 1f, 0.1f, spriteBatch);
        MyGame.Instance.Fonts.TextLem("Out:" + string.Format("{0}", AllLemmings.Count) + "/" + string.Format("{0}", _totalNumLemmings), new Vector2(320, 518), Color.Magenta, 1f, 0.1f, spriteBatch);
        MyGame.Instance.Fonts.TextLem("In:" + string.Format("{0}", Numlemnow), new Vector2(530, 518), Color.AliceBlue, 1f, 0.1f, spriteBatch);

        _inGameMenu.Draw(spriteBatch);

        MyGame.Instance.MouseManager.Draw(spriteBatch, MouseOnLem);

        spriteBatch.End();
    }

    internal void Update(GameTime gameTime)
    {
        MillisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
        if (Exploding && _drawing3 && !GlobalConst.Paused)  //logic explosions particles
        {
            int _totalExploding = ActItem;
            foreach (List<OneExplosion> listExplosions in Explosion.Values)
            {
                int TopY = GlobalConst.GameResolution.Y;
                if (Earth != null)
                    TopY = Earth.Height - 2;
                int NumberAlive = 0;
                foreach (OneExplosion explosion in listExplosions)
                {
                    bool result = explosion.Update(gameTime, TopY);
                    if (result)
                        NumberAlive++;
                }
                listExplosions[0].Counter++;
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
        if (!_levelEnded && ((AllBlow && Numlemnow == 0) || ZvTime < 0 || (AllLemmings.Count == _totalNumLemmings && Numlemnow == 0)))
        {
            if (!GlobalConst.Paused)
                _rest++;  // var to wait until menu appears gives this way 4 seconds plus more
            if (_rest > 180)
            {
                Exploding = false;
                ActItem = 0;  //see when finish time and are more particles ON
                _levelEnded = true;
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
            if (ExitBad && _levelEnded)
                _exitLevel = true;
            else if (NumSaved >= Lemsneeded && _levelEnded)
                _exitLevel = true;
            else
            {
                if (!_levelEnded)
                {
                    ExitBad = true;
                    _levelEnded = true;
                    GlobalConst.Paused = true;
                }
                else
                {
                    GlobalConst.Paused = false;
                    _levelEnded = false;
                }
            }
        }
        if (((Input.PreviousKeyState.IsKeyDown(Keys.Enter) && Input.CurrentKeyState.IsKeyUp(Keys.Enter)) ||
            (Input.PreviousMouseState.RightButton == ButtonState.Released && Input.CurrentMouseState.RightButton == ButtonState.Pressed))
            && _levelEnded)
        {
            _exitLevel = true;
            ExitBad = false;
            _backToMainMenu = true;
        }
        if ((Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed) && _levelEnded)
        {
            if (!ExitBad)
            {
                GlobalConst.Paused = false;
                _levelEnded = false;
            }
            else
                _exitLevel = true;
            if (NumSaved >= Lemsneeded)
                _exitLevel = true;
        }
        if (_exitLevel)
        {
            if (ExitBad) //repeat level
            {
                MyGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                Numlemnow = 0;
                Fade = true;
                MillisecondsElapsed = 0;
                _doorOn = true;
                Frame = 0;
                Frame2 = 0;
                _frame3 = 0;
                _frameDoor = 0;
                _frameExit = 0;
                _rest = 0;
                _levelEnded = false;
                _exitLevel = false;
                AllBlow = false;
                ZvTime = 0;
                ExitBad = false;
                MyGame.Instance.ReloadContent();
                return;
            }

            if (NumSaved >= Lemsneeded) //see here if level is finished or not
            {
                SaveGame.AddFinishedGame(MyGame.Instance.CurrentLevelNumber, 0, AllLemmings.Count(l => l.Exit));
                if (!_backToMainMenu)
                {
                    MyGame.Instance.CurrentLevelNumber++;
                    if (MyGame.Instance.CurrentLevelNumber >= GlobalConst.NumTotalLevels - 1)
                        MyGame.Instance.CurrentLevelNumber = GlobalConst.NumTotalLevels - 1;
                    MyGame.Instance.ScreenMainMenu.MouseLevelChoose = MyGame.Instance.CurrentLevelNumber;
                    MyGame.Instance.CurrentScreen = ECurrentScreen.InGame;
                    Numlemnow = 0;
                    Fade = true;
                    MillisecondsElapsed = 0;
                    _doorOn = true;
                    Frame = 0;
                    Frame2 = 0;
                    _frame3 = 0;
                    _frameDoor = 0;
                    _frameExit = 0;
                    _rest = 0;
                    _levelEnded = false;
                    _exitLevel = false;
                    AllBlow = false;
                    ZvTime = 0;
                    ExitBad = false;
                    MyGame.Instance.ReloadContent();
                    return; //next level
                }
            }

            CurrentMusic.Stop();
            MyGame.Instance.ScreenMainMenu.MouseLevelChoose = 0;
            _levelEnded = false;
            _exitLevel = false;
            AllBlow = false;
            ZvTime = 0;
            ExitBad = false;
            _backToMainMenu = false;
            MyGame.Instance.ReloadContent();
            MyGame.Instance.BackToMenu();
            return;
        }

        if (AllBlow && _actualBlow < AllLemmings.Count) // crash crash TEST TEST
        {
            if (!AllLemmings[_actualBlow].Dead && !AllLemmings[_actualBlow].Explode)
                AllLemmings[_actualBlow].Exploser = true;
            _actualBlow++;
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
        if (_lockMouse)
            Mouse.SetPosition(mousepos.X, mousepos.Y); // setposition //this is for my son kids don't know move mouse so good  
    }

    private void Door()
    {
        Rectangle exitRect = new(_output1X - 5, _output1Y - 5, 10, 10);
        if (Draw2 && _doorOn && Frame > 30)
        {
            TotalTime = 0;
            int xx55 = MyGame.Instance.Props.GetEntry(CurrentLevel.TypeOfDoor).NumFrame - 1;
            _frameDoor++;
            if (_frameDoor == 1 && MyGame.Instance.Sfx.EntryLemmings.State == SoundState.Stopped && !_doorWaveOn)
            {
                MyGame.Instance.Sfx.EntryLemmings.Play();
                _doorWaveOn = true;
            }
            if (_frameDoor > xx55)
            {
                CurrentMusic.IsLooped = true;
                if (!SaveGame.MuteMusic)
                    CurrentMusic.Play();
                _doorOn = false;
                _frameDoor = xx55;
            }
        }
        bool pullLemmings = false;
        float delayPercent = 27 - _inGameMenu.FrequencyNumber * 0.26f; // see to fix speed of lemmings release on door only when change frecuency (not so good)
        if (Drawing && !_doorOn)
        {
            _exitFrame++;
            if (_exitFrame >= (int)delayPercent)
            {
                _exitFrame = 0;
                pullLemmings = true;
            }
        }
        //test to see difference with anterior process
        if (pullLemmings && AllLemmings.Count != _totalNumLemmings && !AllBlow)
        {
            if (CurrentLevel.ListProps<OneMoreDoor>().Any()) // more than 1 door is different calculation
            {
                _door1Y = (int)CurrentLevel.ListProps<OneMoreDoor>().ElementAt(_numActiveDoor).DoorMoreXY.Y;
                _door1X = (int)CurrentLevel.ListProps<OneMoreDoor>().ElementAt(_numActiveDoor).DoorMoreXY.X;
                _numActiveDoor++;
                if (_numActiveDoor >= CurrentLevel.ListProps<OneMoreDoor>().Count())
                    _numActiveDoor = 0;
            }
            AllLemmings.Add(new OneLemming()
            {
                NumLemming = AllLemmings.Count,
                PosY = _door1Y,
                PosX = _door1X + 35,
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
            Point x;
            x.X = lemming.PosX + 14;
            x.Y = lemming.PosY + 25;
            if (lemming.Exit && lemming.Actualframe == 13) // change frame of yipee sound, old frame was init or 0 now different for frames
            {
                if (MyGame.Instance.Sfx.Yippe.State == SoundState.Playing && Draw2)
                    MyGame.Instance.Sfx.Yippe.Stop();
                if (MyGame.Instance.Sfx.Yippe.State == SoundState.Stopped)
                    MyGame.Instance.Sfx.Yippe.Play();
            }
            if (CurrentLevel.ListProps<OneMoreExit>().Any())
            {
                foreach (Vector2 moreExitPos in CurrentLevel.ListProps<OneMoreExit>().Select(me => me.ExitMoreXY)) // more than one EXIT place
                {
                    _output1X = (int)moreExitPos.X;
                    _output1Y = (int)moreExitPos.Y;
                    exitRect = new(_output1X - 5, _output1Y - 5, 10, 10);
                    if (exitRect.Contains(x) && !lemming.Exit && !lemming.Explode)
                    {
                        lemming.PosX = _output1X - 19; //14+5 middle of the exit rect
                        lemming.PosY = _output1Y - 30; //25+5
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
            else
            {
                if (exitRect.Contains(x) && !lemming.Exit && !lemming.Explode)
                {
                    lemming.PosX = _output1X - 19;
                    lemming.PosY = _output1Y - 30;
                    lemming.Active = false;
                    lemming.Walker = false;
                    lemming.Fall = false;
                    lemming.Falling = false;
                    lemming.Exit = true;
                    lemming.Numframes = SizeSprites.sale_frames;
                    lemming.Actualframe = 0;
                }
            }
        }
    }
}
