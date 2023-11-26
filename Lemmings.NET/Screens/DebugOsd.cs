using Lemmings.NET.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens
{
    internal class DebugOsd
    {
        private float _elapsed_time;
        private string strPositionMouse;
        public bool debug; // ACTIVE DEBUG MODE //be careful with spritebacht begin---end debug mode fails
        private int _fps;
        private int frameCounter;

        internal void Init()
        {
            _elapsed_time = 0.0f;
            debug = false;
            _fps = 0;
            frameCounter = 0;
        }

        internal void Update(GameTime gameTime)
        {
            frameCounter++;
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            strPositionMouse = Input.CurrentMouseState.X.ToString() + ", " + Input.CurrentMouseState.Y.ToString();
            if (Input.PreviousKeyState.IsKeyDown(Keys.F1) && Input.CurrentKeyState.IsKeyUp(Keys.F1)) // f1 de-activate debug mode this is only for test BETTER OFF
            {
                debug = !debug;
            }
            if (_elapsed_time > 1)
            {
                _fps = frameCounter;
                frameCounter = 0;
                _elapsed_time = 0;
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                if (LemmingsNetGame.Instance.CurrentScreen == Constants.ECurrentScreen.MainMenu)
                    spriteBatch.DrawString(LemmingsNetGame.Instance.Fonts.Standard, string.Format("numero={0}", LemmingsNetGame.Instance.ScreenMainMenu.MouseLevelChoose), new Vector2(960, 100), Color.White);
                spriteBatch.DrawString(LemmingsNetGame.Instance.Fonts.Standard, string.Format("FPS={0}", _fps), new Vector2(960, 50), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(LemmingsNetGame.Instance.Fonts.Standard, strPositionMouse, new Vector2(960, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

                if (LemmingsNetGame.Instance.ScreenInGame.SteelON)
                {
                    for (int xz = 0; xz < LemmingsNetGame.Instance.ScreenInGame.numTOTsteel; xz++)
                    {
                        Rectangle rectangleFill = new()
                        {
                            X = LemmingsNetGame.Instance.ScreenInGame.steel[xz].area.Left - LemmingsNetGame.Instance.ScreenInGame.ScrollX,
                            Y = LemmingsNetGame.Instance.ScreenInGame.steel[xz].area.Top - LemmingsNetGame.Instance.ScreenInGame.ScrollY,
                            Width = LemmingsNetGame.Instance.ScreenInGame.steel[xz].area.Width,
                            Height = LemmingsNetGame.Instance.ScreenInGame.steel[xz].area.Height,
                        };
                        // magenta r:255,g:0,b:255
                        Color colorFill = new()
                        {
                            R = 255,
                            G = 0,
                            B = 255,
                            A = 140,
                        };
                        spriteBatch.Draw(LemmingsNetGame.Instance.Gfx.Texture1pixel, rectangleFill, null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                    }
                }
            }
        }
    }
}
