using System.Collections.Generic;

using Lemmings.NET.Constants;
using Lemmings.NET.Models;
using Lemmings.NET.Models.Particles;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens;

internal class SnowOverlay
{
    private List<OneSnow> ParticleTab;
    private Texture2D _flake;

    internal void Load(ContentManager content)
    {
        _flake = content.Load<Texture2D>("sprite/particle");
    }

    internal void UpdateSnow(GameTime gameTime)
    {
        // Enable/Disable snowflake
        if ((Input.PreviousKeyState.IsKeyDown(Keys.F2) && Input.CurrentKeyState.IsKeyUp(Keys.F2)) && !MyGame.Instance.LevelEnded)
        {
            if (ParticleTab != null)
                ParticleTab = null;
            else
            {
                ParticleTab = [];
                OneSnow snow;
                for (int varParticle = 0; varParticle < GlobalConst.NumParticles; varParticle++)
                {
                    snow = new OneSnow();
                    snow.Pos.X = GlobalConst.Rnd.Next(20, 1080);
                    snow.Pos.Y = GlobalConst.Rnd.Next(5, 650) - 660;
                    snow.Direction = new Vector2(1, 2);
                    snow.Sprite = _flake;
                    snow.DirectionTime = (float)GlobalConst.Rnd.NextDouble() * 3;
                    ParticleTab.Add(snow);
                }
            }
        }

        if (ParticleTab != null)
        {
            foreach (OneSnow snow in ParticleTab)
            {
                snow.Update(gameTime);
            }
        }
    }

    internal void DrawSnow(SpriteBatch spriteBatch)
    {
        if (ParticleTab != null)
        {
            foreach (OneSnow particle in ParticleTab)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
