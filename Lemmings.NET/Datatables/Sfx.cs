using Lemmings.NET.Helpers;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Lemmings.NET.Datatables;

internal class Sfx
{
    internal SoundEffectInstance Letsgo { get; private set; }
    internal SoundEffectInstance EntryLemmings { get; private set; }
    internal SoundEffectInstance Yippe { get; private set; }
    internal SoundEffectInstance Die { get; private set; }
    internal SoundEffectInstance Splat { get; private set; }
    internal SoundEffectInstance OhNo { get; private set; }
    internal SoundEffectInstance Explode { get; private set; }
    internal SoundEffectInstance Chink { get; private set; }
    internal SoundEffectInstance Fire { get; private set; }
    internal SoundEffectInstance Glup { get; private set; }
    internal SoundEffectInstance Ting { get; private set; }
    internal SoundEffectInstance MousePre { get; private set; }
    internal SoundEffectInstance ChangeOp { get; private set; }
    internal SoundEffectInstance StrapTenton { get; private set; }
    internal SoundEffectInstance StrapMan { get; private set; }
    internal SoundEffectInstance StrapChain { get; private set; }
    internal SoundEffectInstance StrapChupar { get; private set; }
    internal SoundEffectInstance StrapTenTonnes { get; private set; }

    internal void LoadContent(ContentManager content)
    {
        SoundEffect doorwav = content.Load<SoundEffect>("soundfx/door");
        EntryLemmings = doorwav.CreateInstance();
        SoundEffect init = content.Load<SoundEffect>("soundfx/letsgo");
        Letsgo = init.CreateInstance();
        SoundEffect oing = content.Load<SoundEffect>("soundfx/yippee");
        Yippe = oing.CreateInstance();
        SoundEffect die = content.Load<SoundEffect>("soundfx/die");
        Die = die.CreateInstance();
        SoundEffect splat = content.Load<SoundEffect>("soundfx/splat");
        Splat = splat.CreateInstance();
        SoundEffect ohno = content.Load<SoundEffect>("soundfx/ohno");
        OhNo = ohno.CreateInstance();
        SoundEffect chink = content.Load<SoundEffect>("soundfx/chink");
        Chink = chink.CreateInstance();
        SoundEffect explo = content.Load<SoundEffect>("soundfx/explode");
        Explode = explo.CreateInstance();
        SoundEffect sfire = content.Load<SoundEffect>("soundfx/fire");
        Fire = sfire.CreateInstance();
        SoundEffect sglug = content.Load<SoundEffect>("soundfx/glug");
        Glup = sglug.CreateInstance();
        SoundEffect sting = content.Load<SoundEffect>("soundfx/ting");
        Ting = sting.CreateInstance();
        SoundEffect smousepre = content.Load<SoundEffect>("soundfx/mousepre");
        MousePre = smousepre.CreateInstance();
        SoundEffect schangeop = content.Load<SoundEffect>("soundfx/changeop");
        ChangeOp = schangeop.CreateInstance();
        SoundEffect tenton = content.Load<SoundEffect>("soundfx/tenton");
        StrapTenton = tenton.CreateInstance();
        SoundEffect manTrap = content.Load<SoundEffect>("soundfx/mantrap");
        StrapMan = manTrap.CreateInstance();
        SoundEffect chain = content.Load<SoundEffect>("soundfx/chain");
        StrapChain = chain.CreateInstance();
        SoundEffect chupar = content.Load<SoundEffect>("soundfx/chupar");
        StrapChupar = chupar.CreateInstance();
        SoundEffect tenTonnes = content.Load<SoundEffect>("soundfx/10tones");
        StrapTenTonnes = tenTonnes.CreateInstance();
    }
}
