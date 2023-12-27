using System;
using System.Diagnostics;

using Lemmings.NET.Constants;
using Lemmings.NET.Datatables;
using Lemmings.NET.Helpers;
using Lemmings.NET.Models;
using Lemmings.NET.Screens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET;

public partial class MyGame : Game
{
    #region Fields
    private Color letterboxingColor = new(0, 0, 0);
    private Rectangle renderTargetDestination;
    // Datatables
    private Sprites _sprites;
    private Music _music;
    private Sfx _sfx;
    private Fonts _fonts;
    private MouseManager _mouse;
    private Gfx _gfx;
    private InGameMenuGfx _inGameMenuGfx;
    private Props _props;
    // Screens
    private MainMenu _screenMainMenu;
    private InGame _screenInGame;
    private DebugOsd _debugOsd;
    private SnowOverlay _snowOverlay;
    private readonly Stopwatch _showVolume;
    private readonly Stopwatch _showMusic;
    // Graphics
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    #endregion

    #region Properties
    public RenderTarget2D MainRenderTarget { get; private set; }
    public bool Scaled { get; set; }
    public static MyGame Instance { get; private set; }
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
    internal InGameMenuGfx InGameMenuGfx
    {
        get { return _inGameMenuGfx; }
    }
    internal Props Props
    {
        get { return _props; }
    }
    internal int CurrentLevelNumber { get; set; }
    internal ECurrentScreen CurrentScreen { get; set; }
    public bool LevelEnded { get; set; } = false;
    #endregion

    internal void BackToMenu()
    {
        if (_music.MenuMusic.State == SoundState.Stopped && !SaveGame.MuteMusic)
            _music.MenuMusic.Play();
        _screenMainMenu.BackToMenu();
        CurrentScreen = ECurrentScreen.MainMenu;
    }

    public MyGame()
    {
        Instance = this;
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8,
            PreferredBackBufferWidth = GlobalConst.GameResolution.X,
            PreferredBackBufferHeight = GlobalConst.GameResolution.Y,
            GraphicsProfile = GraphicsProfile.HiDef,
        };
        Content.RootDirectory = "Content";
        Window.Title = "Lemmings.NET";
        Window.AllowUserResizing = false;
        _showVolume = new Stopwatch();
        _showMusic = new Stopwatch();
        SaveGame.LoadSavedGame();
    }

    protected override void Initialize()
    {
        _screenInGame = new InGame();
        SoundEffect.MasterVolume = 1.0f;
        if (SaveGame.FullScreen)
            ToggleFullScreen();
        else if (SaveGame.Scale)
            ToggleScale();
        _screenMainMenu = new MainMenu();
        base.Initialize();
    }

    internal void ReloadContent()
    {
        LoadContent();
    }

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
            _debugOsd.Init();
        }
        if (_inGameMenuGfx == null)
        {
            _inGameMenuGfx = new InGameMenuGfx();
            _inGameMenuGfx.Load(Content);
        }
        if (_props == null)
        {
            _props = new Props();
            _props.Load();
        }
        if (_snowOverlay == null)
        {
            _snowOverlay = new SnowOverlay();
            _snowOverlay.Load(Content);
        }
        Mouse.SetPosition(GlobalConst.GameResolution.X / 2, GlobalConst.GameResolution.Y / 2);
        MainRenderTarget = new RenderTarget2D(GraphicsDevice, GlobalConst.GameResolution.X, GlobalConst.GameResolution.Y);
        renderTargetDestination = Graphics.GetRenderTargetDestination(GlobalConst.GameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        _screenMainMenu.LoadGfx();

        if (CurrentScreen == ECurrentScreen.MainMenu)
        {
            SaveGame.LoadSavedGame();
            SoundEffect.MasterVolume = SaveGame.SoundVolume;
            if (!SaveGame.MuteMusic)
                _music.MenuMusic.Play();
        }
        else if (CurrentScreen == ECurrentScreen.InGame) //when level starts all the vars and reset all
        {
            _screenInGame.LoadLevel(CurrentLevelNumber, Content);
            _screenInGame.CurrentMusic = _music.GetMusic(CurrentLevelNumber % 19);
        }
    }

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
            SaveGame.MuteMusic = !SaveGame.MuteMusic;
            if (CurrentScreen == ECurrentScreen.InGame)
            {
                if (_screenInGame.CurrentMusic.State == SoundState.Playing)
                    _screenInGame.CurrentMusic.Pause();
                else if (_screenInGame.CurrentMusic.State == SoundState.Paused)
                    _screenInGame.CurrentMusic.Resume();
                else if (_screenInGame.CurrentMusic.State == SoundState.Stopped)
                    _screenInGame.CurrentMusic.Play();
            }
            else
            {
                if (_music.MenuMusic.State == SoundState.Playing)
                    _music.MenuMusic.Pause();
                else
                    _music.MenuMusic.Play();
            }
            _showMusic.Restart();
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.PageDown) && Input.CurrentKeyState.IsKeyUp(Keys.PageDown) && SoundEffect.MasterVolume > 0.0f)
        {
            try
            {
                SoundEffect.MasterVolume -= 0.1f;
            }
            catch (Exception)
            {
                SoundEffect.MasterVolume = 0;
            }
            _showVolume.Restart();
            SaveGame.SoundVolume = SoundEffect.MasterVolume;
        }
        else if (Input.PreviousKeyState.IsKeyDown(Keys.PageUp) && Input.CurrentKeyState.IsKeyUp(Keys.PageUp) && SoundEffect.MasterVolume < 1.0f)
        {
            try
            {
                SoundEffect.MasterVolume += 0.1f;
            }
            catch (Exception)
            {
                SoundEffect.MasterVolume = 1.0f;
            }
            _showVolume.Restart();
        }

        if (CurrentScreen == ECurrentScreen.MainMenu)
            _screenMainMenu.Update();
        else if (CurrentScreen == ECurrentScreen.InGame)
            _screenInGame.Update(gameTime);
        //take care of numerodentro for the save file when exit with 
        // right button does not save cos if is false always

        _debugOsd.Update(gameTime);

        _snowOverlay.UpdateSnow(gameTime);

        base.Update(gameTime);
    }

    private void ToggleScale()
    {
        Scaled = !Scaled;
        SaveGame.Scale = Scaled;
        _graphics.PreferredBackBufferWidth = GlobalConst.GameResolution.X * (Scaled ? 2 : 1);
        _graphics.PreferredBackBufferHeight = GlobalConst.GameResolution.Y * (Scaled ? 2 : 1);

        _graphics.ApplyChanges();

        renderTargetDestination = Graphics.GetRenderTargetDestination(GlobalConst.GameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
    }

    private void ToggleFullScreen()
    {
        if (_graphics.IsFullScreen)
        {
            _graphics.PreferredBackBufferWidth = GlobalConst.GameResolution.X;
            _graphics.PreferredBackBufferHeight = GlobalConst.GameResolution.Y;
        }
        else
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        SaveGame.FullScreen = _graphics.IsFullScreen;
        renderTargetDestination = Graphics.GetRenderTargetDestination(GlobalConst.GameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _graphics.ToggleFullScreen();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(MainRenderTarget);
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
        if (_showVolume.IsRunning)
        {
            if (_showVolume.ElapsedMilliseconds <= 3000)
                _fonts.TextLem($"Volume : {(SoundEffect.MasterVolume * 100):00}%", new Vector2(800, 665), Color.White, 1, 0.1f, _spriteBatch);
            else
                _showVolume.Stop();
        }
        else if (_showMusic.IsRunning)
        {
            if (_showMusic.ElapsedMilliseconds <= 3000)
                _fonts.TextLem($"Music : {(SaveGame.MuteMusic ? "Off" : "On")}", new Vector2(800, 665), Color.White, 1, 0.1f, _spriteBatch);
            else
                _showMusic.Stop();
        }
        _snowOverlay.DrawSnow(_spriteBatch);
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(letterboxingColor);

        _spriteBatch.Begin();
        _spriteBatch.Draw(MainRenderTarget, renderTargetDestination, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
