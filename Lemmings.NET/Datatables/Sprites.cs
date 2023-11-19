using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables
{
    internal class Sprites
    {
        public Texture2D Climber { get; private set; }
        public Texture2D Builder { get; private set; }
        public Texture2D Walker { get; private set; }
        public Texture2D Blocker { get; private set; }
        public Texture2D Digger { get; private set; }
        public Texture2D Falling { get; private set; }
        public Texture2D Exploder { get; private set; }
        public Texture2D Drowner { get; private set; }
        public Texture2D EyeBlink1 { get; private set; }
        public Texture2D EyeBlink2 { get; private set; }
        public Texture2D EyeBlink3 { get; private set; }

        internal void LoadContent(ContentManager Content)
        {
            Walker = Content.Load<Texture2D>("walker_ok");
            Falling = Content.Load<Texture2D>("cae_ok");
            Digger = Content.Load<Texture2D>("cavar_ok");
            Climber = Content.Load<Texture2D>("escala");
            Exploder = Content.Load<Texture2D>("explota");
            Blocker = Content.Load<Texture2D>("blocker");
            Drowner = Content.Load<Texture2D>("ahoga");
            EyeBlink1 = Content.Load<Texture2D>("lem1/blink1");
            EyeBlink2 = Content.Load<Texture2D>("lem1/blink2");
            EyeBlink3 = Content.Load<Texture2D>("lem1/blink3");
        }
    }
}
