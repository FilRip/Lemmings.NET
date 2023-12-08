using Lemmings.NET.Constants;
using Lemmings.NET.Models;
using Lemmings.NET.Structs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens;

internal class SnowOverlay
{
    internal Particles[] ParticleTab { get; set; }
    private Texture2D _flake;
    internal void Load(ContentManager content)
    {
        _flake = content.Load<Texture2D>("sprite/particle");
    }

    internal void UpdateSnow(GameTime gameTime)
    {
        if ((Input.PreviousKeyState.IsKeyDown(Keys.F2) && Input.CurrentKeyState.IsKeyUp(Keys.F2)) && !MyGame.Instance.LevelEnded)
        {
            if (ParticleTab != null)
                ParticleTab = null;
            else
            {
                ParticleTab = new Particles[GlobalConst.NumParticles];
                Vector2 vectorFill;
                for (int varParticle = 0; varParticle < GlobalConst.NumParticles; varParticle++)
                {
                    vectorFill = new Vector2()
                    {
                        X = GlobalConst.Rnd.Next(20, 1080),
                        Y = GlobalConst.Rnd.Next(5, 650) - 660,
                    };
                    ParticleTab[varParticle].Pos = vectorFill;
                    vectorFill.X = 1;
                    vectorFill.Y = 2;
                    ParticleTab[varParticle].Direction = vectorFill;
                    ParticleTab[varParticle].Sprite = _flake;
                    ParticleTab[varParticle].DirectionTime = (float)GlobalConst.Rnd.NextDouble() * 3;
                }
            }
        }
        if (ParticleTab != null)
        {
            for (int varParticle = 0; varParticle < GlobalConst.NumParticles; varParticle++)
            {
                ParticleTab[varParticle].DirectionTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                ParticleTab[varParticle].Lifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                ParticleTab[varParticle].Pos += ParticleTab[0].Direction;
                ParticleTab[varParticle].SetPosX(ParticleTab[varParticle].Pos.X - ParticleTab[varParticle].DirectionTime);
                ParticleTab[varParticle].SetPosY(ParticleTab[varParticle].Pos.Y - (float)GlobalConst.Rnd.NextDouble());
                if (ParticleTab[varParticle].DirectionTime < 0)
                {
                    ParticleTab[varParticle].DirectionTime = (float)GlobalConst.Rnd.NextDouble() * 3;
                }
                if (ParticleTab[varParticle].Pos.Y > GlobalConst.GameResolution.Y)
                    ParticleTab[varParticle].SetPosY(0);
            }
        }
    }

    internal void DrawSnow(SpriteBatch spriteBatch)
    {
        if (ParticleTab != null)
        {
            foreach (Particles particle in ParticleTab)
            {
                spriteBatch.Draw(particle.Sprite, particle.Pos, new Rectangle(0, 0, 10, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.50001f);
            }
        }
    }
}
