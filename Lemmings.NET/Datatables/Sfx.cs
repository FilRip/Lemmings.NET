using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Lemmings.NET.Datatables
{
    internal class Sfx
    {
        public SoundEffectInstance Letsgo { get; private set; }
        public SoundEffectInstance EntryLemmings { get; private set; }
        public SoundEffectInstance Yippe { get; private set; }
        public SoundEffectInstance Die { get; private set; }
        public SoundEffectInstance Splat { get; private set; }
        public SoundEffectInstance OhNo { get; private set; }
        public SoundEffectInstance Explode { get; private set; }
        public SoundEffectInstance Chink { get; private set; }
        public SoundEffectInstance Fire { get; private set; }
        public SoundEffectInstance Glup { get; private set; }
        public SoundEffectInstance Ting { get; private set; }
        public SoundEffectInstance MousePre { get; private set; }
        public SoundEffectInstance ChangeOp { get; private set; }

        internal void LoadContent(ContentManager Content)
        {
            SoundEffect doorwav = Content.Load<SoundEffect>("soundfx/door");
            EntryLemmings = doorwav.CreateInstance();
            SoundEffect init = Content.Load<SoundEffect>("soundfx/letsgo");
            Letsgo = init.CreateInstance();
            SoundEffect oing = Content.Load<SoundEffect>("soundfx/yippee");
            Yippe = oing.CreateInstance();
            SoundEffect die = Content.Load<SoundEffect>("soundfx/die");
            Die = die.CreateInstance();
            SoundEffect splat = Content.Load<SoundEffect>("soundfx/splat");
            Splat = splat.CreateInstance();
            SoundEffect ohno = Content.Load<SoundEffect>("soundfx/ohno");
            OhNo = ohno.CreateInstance();
            SoundEffect chink = Content.Load<SoundEffect>("soundfx/chink");
            Chink = chink.CreateInstance();
            SoundEffect explo = Content.Load<SoundEffect>("soundfx/explode");
            Explode = explo.CreateInstance();
            SoundEffect sfire = Content.Load<SoundEffect>("soundfx/fire");
            Fire = sfire.CreateInstance();
            SoundEffect sglug = Content.Load<SoundEffect>("soundfx/glug");
            Glup = sglug.CreateInstance();
            SoundEffect sting = Content.Load<SoundEffect>("soundfx/ting");
            Ting = sting.CreateInstance();
            SoundEffect smousepre = Content.Load<SoundEffect>("soundfx/mousepre");
            MousePre = smousepre.CreateInstance();
            SoundEffect schangeop = Content.Load<SoundEffect>("soundfx/changeop");
            ChangeOp = schangeop.CreateInstance();
        }
    }
}
