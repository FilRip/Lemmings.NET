using Microsoft.Xna.Framework.Audio;

namespace Lemmings.NET.Helpers;

internal static class Extensions
{
    internal static void Replay(this SoundEffectInstance sound)
    {
        if (sound == null)
            return;
        try
        {
            MyGame.Instance.Sfx.ChangeOp.Pitch = 0;
            MyGame.Instance.Sfx.ChangeOp.Volume = 1f;
            if (sound.State == SoundState.Playing)
                sound.Stop();
            sound.Play();
        }
        catch { /* Ignore errors */ }
    }
}
