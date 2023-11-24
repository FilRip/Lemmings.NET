using System;
using System.IO;

using Lemmings.NET.Constants;
using Lemmings.NET.Datatables;
using Lemmings.NET.Models;
using Lemmings.NET.Screens;
using Lemmings.NET.Structs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET
{
    public partial class LemmingsNetGame : Game
    {
        #region Fields
        private Color letterboxingColor = new(0, 0, 0);
        public RenderTarget2D renderTarget;
        private Rectangle renderTargetDestination;
        public bool Scaled { get; set; }
        public static LemmingsNetGame Instance { get; private set; }
        // Datatables
        private Sprites _sprites;
        private Music _music;
        private Sfx _sfx;
        private Fonts _fonts;
        private MouseManager _mouse;
        private Gfx _gfx;
        private Vfx _vfx;
        private InGameMenuGfx _inGameMenuGfx;
        // Screens
        private MainMenu _screenMainMenu;
        private InGame _screenInGame;
        private DebugOsd _debugOsd;
        private Vector2 vectorFill;
        #endregion

        #region Properties
        internal Sfx Sfx
        {
            get { return _sfx; }
        }
        internal Music Music
        {
            get { return _music; }
        }
        internal Fonts Fonts
        {
            get { return _fonts; }
        }
        internal Gfx Gfx
        {
            get { return _gfx; }
        }
        internal Sprites Sprites
        {
            get { return _sprites; }
        }
        internal MainMenu ScreenMainMenu
        {
            get { return _screenMainMenu; }
        }
        internal InGame ScreenInGame
        {
            get { return _screenInGame; }
        }
        internal MouseManager MouseManager
        {
            get { return _mouse; }
        }
        internal DebugOsd DebugOsd
        {
            get { return _debugOsd; }
        }
        internal Vfx Vfx
        {
            get { return _vfx; }
        }
        internal InGameMenuGfx InGameMenuGfx
        {
            get { return _inGameMenuGfx; }
        }
        internal int CurrentLevelNumber { get; set; }
        internal ECurrentScreen CurrentScreen { get; set; }
        #endregion

        internal void BackToMenu()
        {
            if (_music.MenuMusic.State == SoundState.Stopped)
                _music.MenuMusic.Play();
            _screenMainMenu.BackToMenu();
            CurrentScreen = ECurrentScreen.MainMenu;
        }

        public Texture2D text;
        public bool MustReadFile { get; set; } = true;
        public bool LevelEnded { get; set; } = false;
        public bool ExitLevel { get; set; } = false;
        public bool ExitBad { get; set; } = false;
        // Paused Lemmings update vars,time counter,door open,bombers countdown,traps
        // 38 * 53 size of mask exploder BE CAREFUL WITH NEW() FOR MASK SYSTEM-OUT OF MEMORY CRASHES
        // this three color mask are for the arrows for now 500*512 size is enough
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////explosions particles data and definitions
        public bool Exploding { get; set; } = false; //always start as false - true is when are exploding or active
        internal Particle[,] Explosion { get; set; } = new Particle[MyGame.totalExplosions, 24];
        private int xExp, yExp;
        public Structs.Particles[] particle;
        private float rparticle1;
        private bool rightparticle;
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        //vita touch textures

        Vector2 direction_sprite;
        int[] terrainContour;
        int ssi, px, py, ancho, amount, positioYOrig, y55, x55, startposy, framepos, yypos99, cantidad99, yy99, xx99;
        int xz, yypos888, yy88, xx88, y4, x4, posy456, posx456, arriba, b, ti, pixx;
        int pos_real, wer3, width2, top2, yypos555, yy33, xx33, valX, valY, y, ykk, xkk, abajo2, pixx2, pos_real2, py2, px2, valorx, valory;
        public int varParticle, tYheight, vv444, spY, xx55, yy55, swidth, sheight, sx1, sy1, xxAnim, w, h, x2, yy66, ex22;
        int rest2, width, xwe, xqw, mmx, mmy, mmKX, mmKY, mmKplusY, mmKindX, mmKindY, mmPlusy;

        public LemmingsNetGame()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8,
                PreferredBackBufferWidth = MyGame.GameResolution.X,
                PreferredBackBufferHeight = MyGame.GameResolution.Y,
            };
            //// this.IsMouseVisible = true;  //WINDOWS MOUSE VISIBLE OR NOT
            Content.RootDirectory = "Content";
            Window.Title = "Lemmings.NET";
            Window.AllowUserResizing = false;
        }

        protected override void Initialize()
        {
            _screenInGame = new InGame();
            _screenInGame.VariablesLevels();
            SoundEffect.MasterVolume = 1.0f;
            _screenMainMenu = new MainMenu();
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

        internal void ReloadContent()
        {
            LoadContent();
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
                _mouse = new MouseManager();
                _mouse.LoadContent(Content);
            }
            if (_gfx == null)
            {
                _gfx = new Gfx();
                _gfx.Load(GraphicsDevice, Content);
            }
            if (_debugOsd == null)
            {
                _debugOsd = new DebugOsd();
            }
            if (_inGameMenuGfx == null)
            {
                _inGameMenuGfx = new InGameMenuGfx();
                _inGameMenuGfx.Load(Content);
            }
            if (_vfx == null)
            {
                _vfx = new Vfx();
                _vfx.Load(Content);
            }
            Mouse.SetPosition(0, 0);
            renderTarget = new RenderTarget2D(GraphicsDevice, MyGame.GameResolution.X, MyGame.GameResolution.Y);
            renderTargetDestination = GetRenderTargetDestination(MyGame.GameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            //background_02  logo  llama  fondos/nubes  crateN
            text = Content.Load<Texture2D>("crate");
            //Crate = Content.Load<Texture2D>("crate");
            _screenMainMenu.LoadGfx();

            if (CurrentScreen == ECurrentScreen.MainMenu)
            {
                //mainMenuLogo = Content.Load<Texture2D>("lem1/logo_mainmenu");
                _music.MenuMusic.Play();
                if (MustReadFile)
                {
                    if (File.Exists(MyGame.SaveGameFileName))
                    {
                        BinaryReader reader = new(File.Open(MyGame.SaveGameFileName, FileMode.Open));
                        for (int i = 0; i < MyGame.NumTotalLevels; i++)
                        {
                            _screenInGame.LevelEnd[i] = reader.ReadBoolean();
                        }
                        reader.Close();
                        MustReadFile = false;
                    }
                    else
                    {
                        BinaryWriter writer = new(File.Open(MyGame.SaveGameFileName, FileMode.Create));
                        for (int i = 0; i < MyGame.NumTotalLevels; i++)
                        {
                            writer.Write(_screenInGame.LevelEnd[i]);
                        }
                        writer.Write("(c) 2023 FilRip from CoolBytes");
                        writer.Close();
                        MustReadFile = false;
                    }
                }

            }
            if (CurrentScreen == ECurrentScreen.InGame) //when level starts all the vars and reset all
            {
                _screenInGame.LoadLevel(CurrentLevelNumber, Content);

                //backmenu1 = Content.Load<Texture2D>("background_01");
                //backmenu3 = Content.Load<Texture2D>("background_02");
                //lucesfondo = Content.Load<Texture2D>("fondos/luces de fondo guays");
                //explode = Content.Load<Texture2D>("explode");
                _screenInGame.mascarapared = Content.Load<Texture2D>("mascara_pared");
                //mascarapico = Content.Load<Texture2D>("mascara_pico");
                //mascarapared_left = Content.Load<Texture2D>("mascara_pared_left");
                //lsplat = Content.Load<Texture2D>("sprite/splat");
                //lyipie = Content.Load<Texture2D>("sprite/yipie");
                //lglup = Content.Load<Texture2D>("sprite/glub");
                _screenInGame.CurrentMusic = _music.GetMusic(CurrentLevelNumber % 19);
            }
        }
#pragma warning restore S125 // Sections of code should not be commented out

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.SetKeyboardState(Keyboard.GetState());
            Input.SetMouseState(Mouse.GetState());
            Input.CurrentGameTime = gameTime;
            if (Input.PreviousKeyState.IsKeyDown(Keys.F12) && Input.CurrentKeyState.IsKeyUp(Keys.F12))
            {
                ToggleScale();
            }
            else if (Input.PreviousKeyState.IsKeyDown(Keys.F10) && Input.CurrentKeyState.IsKeyUp(Keys.F10))
            {
                ToggleFullScreen();
            }
            else if (Input.PreviousKeyState.IsKeyDown(Keys.M) && Input.CurrentKeyState.IsKeyUp(Keys.M))
            {
                if (CurrentScreen == ECurrentScreen.InGame)
                {
                    if (_screenInGame.CurrentMusic.State == SoundState.Playing)
                        _screenInGame.CurrentMusic.Pause();
                    else if (_screenInGame.CurrentMusic.State == SoundState.Paused)
                        _screenInGame.CurrentMusic.Resume();
                }
                else
                {
                    if (_music.MenuMusic.State == SoundState.Playing)
                        _music.MenuMusic.Pause();
                    else
                        _music.MenuMusic.Play();
                }
            }
            else if (Input.PreviousKeyState.IsKeyDown(Keys.PageDown) && Input.CurrentKeyState.IsKeyUp(Keys.PageDown) && SoundEffect.MasterVolume > 0.0f)
                try
                {
                    SoundEffect.MasterVolume -= 0.1f;
                }
                catch (Exception)
                {
                    SoundEffect.MasterVolume = 0;
                }
            else if (Input.PreviousKeyState.IsKeyDown(Keys.PageUp) && Input.CurrentKeyState.IsKeyUp(Keys.PageUp) && SoundEffect.MasterVolume < 1.0f)
                try
                {
                    SoundEffect.MasterVolume += 0.1f;
                }
                catch (Exception)
                {
                    SoundEffect.MasterVolume = 1.0f;
                }

            if (CurrentScreen == ECurrentScreen.MainMenu)
                _screenMainMenu.Update();
            else if (CurrentScreen == ECurrentScreen.InGame)
                _screenInGame.Update(gameTime);
            //take care of numerodentro for the save file when exit with 
            // right button does not save cos if is false always

            _debugOsd.Update(gameTime);
            if (CurrentScreen == ECurrentScreen.MainMenu)
            {
                _screenMainMenu.Update();
            }
            // particle test test test right button mouse
            if ((Input.PreviousMouseState.RightButton == ButtonState.Released && Input.CurrentMouseState.RightButton == ButtonState.Pressed) && !LevelEnded)
            {
                if (particle != null)
                    particle = null;
                else
                {
                    rightparticle = false;
                    rparticle1 = MyGame.Rnd.Next(0, 1);
                    if (rparticle1 == 0)
                        rightparticle = false;
                    else
                        rightparticle = true;
                    particle = new Structs.Particles[MyGame.numParticles];
                    for (varParticle = 0; varParticle < MyGame.numParticles; varParticle++)
                    {
                        vectorFill.X = MyGame.Rnd.Next(20, 1080);
                        vectorFill.Y = MyGame.Rnd.Next(5, 650) - 660;
                        particle[varParticle].Pos = vectorFill;
                        vectorFill.X = 1;
                        vectorFill.Y = 2;
                        particle[varParticle].Direction = vectorFill;
                        particle[varParticle].Sprite = Content.Load<Texture2D>("sprite/particle");
                        rparticle1 = (float)MyGame.Rnd.NextDouble() * 3;
                        particle[varParticle].DirectionTime = rparticle1;
                    }
                }

            }
            if (particle != null)
            {
                for (varParticle = 0; varParticle < MyGame.numParticles; varParticle++)
                {
                    particle[varParticle].DirectionTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    particle[varParticle].Lifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    particle[varParticle].Pos += particle[0].Direction;
                    if (rightparticle)
                        particle[varParticle].SetPosX(particle[varParticle].DirectionTime);
                    else
                        particle[varParticle].SetPosX(particle[varParticle].Pos.X - particle[varParticle].DirectionTime);
                    particle[varParticle].SetPosY(particle[varParticle].Pos.Y - (float)MyGame.Rnd.NextDouble());
                    if (particle[varParticle].DirectionTime < 0)
                    {
                        rightparticle = false;
                        rparticle1 = MyGame.Rnd.Next(0, 1);
                        if (rparticle1 == 0)
                            rightparticle = false;
                        else
                            rightparticle = true;
                        rparticle1 = (float)MyGame.Rnd.NextDouble() * 3;
                        particle[varParticle].DirectionTime = rparticle1;
                    }
                    if (particle[varParticle].Pos.Y > MyGame.GameResolution.Y)
                        particle[varParticle].SetPosY(0);
                }

            }
            base.Update(gameTime);
        }

        private void ToggleScale()
        {
            Scaled = !Scaled;

            _graphics.PreferredBackBufferWidth = MyGame.GameResolution.X * (Scaled ? 2 : 1);
            _graphics.PreferredBackBufferHeight = MyGame.GameResolution.Y * (Scaled ? 2 : 1);

            _graphics.ApplyChanges();

            renderTargetDestination = GetRenderTargetDestination(MyGame.GameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        private void ToggleFullScreen()
        {
            if (_graphics.IsFullScreen)
            {
                _graphics.PreferredBackBufferWidth = MyGame.GameResolution.X;
                _graphics.PreferredBackBufferHeight = MyGame.GameResolution.Y;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            renderTargetDestination = GetRenderTargetDestination(MyGame.GameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _graphics.ToggleFullScreen();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(letterboxingColor);

            if (CurrentScreen == ECurrentScreen.InGame)
            {
                _screenInGame.Draw(GraphicsDevice, _spriteBatch);
            }
            else if (CurrentScreen == ECurrentScreen.MainMenu)
            {
                _screenMainMenu.Draw(GraphicsDevice, _spriteBatch);
            }

            _spriteBatch.Begin();
            _debugOsd.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(letterboxingColor);

            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget, renderTargetDestination, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

