using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Lemmings.NET.Datatables;

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
    public SoundEffectInstance StrapTenton { get; private set; }
    public SoundEffectInstance StrapMan { get; private set; }
    public SoundEffectInstance StrapChain { get; private set; }
    public SoundEffectInstance StrapChupar { get; private set; }
    public SoundEffectInstance StrapTenTonnes { get; private set; }

    public void PlaySoundMenu()
    {
        if (ChangeOp.State == SoundState.Playing)
        {
            ChangeOp.Stop();
        }
        try
        {
            ChangeOp.Play();
        }
        catch (InstancePlayLimitException) { /* Ignore errors */ }
    }

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
