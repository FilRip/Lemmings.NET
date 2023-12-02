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
            if (sound.State == SoundState.Playing)
                sound.Stop();
            sound.Play();
        }
        catch { /* Ignore errors */ }
    }
}
