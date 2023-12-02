using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Lemmings.NET.Datatables;

internal class Music
{
    internal SoundEffectInstance Music1 { get; private set; }
    internal SoundEffectInstance Music2 { get; private set; }
    internal SoundEffectInstance Music3 { get; private set; }
    internal SoundEffectInstance Music4 { get; private set; }
    internal SoundEffectInstance Music5 { get; private set; }
    internal SoundEffectInstance Music6 { get; private set; }
    internal SoundEffectInstance Music7 { get; private set; }
    internal SoundEffectInstance Music8 { get; private set; }
    internal SoundEffectInstance Music9 { get; private set; }
    internal SoundEffectInstance Music10 { get; private set; }
    internal SoundEffectInstance Music11 { get; private set; }
    internal SoundEffectInstance Music12 { get; private set; }
    internal SoundEffectInstance Music13 { get; private set; }
    internal SoundEffectInstance MenuMusic { get; private set; }
    internal SoundEffectInstance Music15 { get; private set; }
    internal SoundEffectInstance Music16 { get; private set; }
    internal SoundEffectInstance Music17 { get; private set; }
    internal SoundEffectInstance Music18 { get; private set; }
    internal SoundEffectInstance Music19 { get; private set; }
    internal SoundEffectInstance WinMusic { get; private set; }

    internal SoundEffectInstance GetMusic(int numMusic)
    {
        return numMusic switch
        {
            1 => Music2,
            2 => Music3,
            3 => Music4,
            4 => Music5,
            5 => Music6,
            6 => Music7,
            7 => Music8,
            8 => Music9,
            9 => Music10,
            10 => Music11,
            11 => Music12,
            12 => Music13,
            14 => Music15,
            15 => Music16,
            16 => Music17,
            17 => Music18,
            18 => Music19,
            _ => Music1,
        };
    }

    internal void LoadContent(ContentManager content)
    {
        SoundEffect _music1 = content.Load<SoundEffect>("music/tim1");
        Music1 = _music1.CreateInstance();
        Music1.IsLooped = true;
        SoundEffect _music2 = content.Load<SoundEffect>("music/lem_intro");
        Music2 = _music2.CreateInstance();
        Music2.IsLooped = true;
        SoundEffect _music3 = content.Load<SoundEffect>("music/lemming1");
        Music3 = _music3.CreateInstance();
        Music3.IsLooped = true;
        SoundEffect _music4 = content.Load<SoundEffect>("music/tim2");
        Music4 = _music4.CreateInstance();
        Music4.IsLooped = true;
        SoundEffect _music5 = content.Load<SoundEffect>("music/lemming2");
        Music5 = _music5.CreateInstance();
        Music5.IsLooped = true;
        SoundEffect _music6 = content.Load<SoundEffect>("music/tim8");
        Music6 = _music6.CreateInstance();
        Music6.IsLooped = true;
        SoundEffect _music7 = content.Load<SoundEffect>("music/tim3");
        Music7 = _music7.CreateInstance();
        Music7.IsLooped = true;
        SoundEffect _music8 = content.Load<SoundEffect>("music/tim5");
        Music8 = _music8.CreateInstance();
        Music8.IsLooped = true;
        SoundEffect _music9 = content.Load<SoundEffect>("music/doggie");
        Music9 = _music9.CreateInstance();
        Music9.IsLooped = true;
        SoundEffect _music10 = content.Load<SoundEffect>("music/tim6");
        Music10 = _music10.CreateInstance();
        Music10.IsLooped = true;
        SoundEffect _music11 = content.Load<SoundEffect>("music/lemming3");
        Music11 = _music11.CreateInstance();
        Music11.IsLooped = true;
        SoundEffect _music12 = content.Load<SoundEffect>("music/tim7");
        Music12 = _music12.CreateInstance();
        Music12.IsLooped = true;
        SoundEffect _music13 = content.Load<SoundEffect>("music/tim9");
        Music13 = _music13.CreateInstance();
        Music13.IsLooped = true;
        SoundEffect _music15 = content.Load<SoundEffect>("music/tim10");
        Music15 = _music15.CreateInstance();
        Music15.IsLooped = true;
        SoundEffect _music16 = content.Load<SoundEffect>("music/tim4");
        Music16 = _music16.CreateInstance();
        Music16.IsLooped = true;
        SoundEffect _music17 = content.Load<SoundEffect>("music/tenlemms");
        Music17 = _music17.CreateInstance();
        Music17.IsLooped = true;
        SoundEffect _music18 = content.Load<SoundEffect>("music/mountain");
        Music18 = _music18.CreateInstance();
        Music18.IsLooped = true;
        SoundEffect _music19 = content.Load<SoundEffect>("music/cancan");
        Music19 = _music19.CreateInstance();
        Music19.IsLooped = true;

        SoundEffect _music20 = content.Load<SoundEffect>("music/title");
        WinMusic = _music20.CreateInstance();
        WinMusic.IsLooped = true;
        SoundEffect _music14 = content.Load<SoundEffect>("music/lem_menu");
        MenuMusic = _music14.CreateInstance();
        MenuMusic.IsLooped = true;
    }
}
