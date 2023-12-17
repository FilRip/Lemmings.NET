using System;

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

    public static T[] Insert<T>(this T[] list, object itemToInsert, int position)
    {
        ArgumentNullException.ThrowIfNull(itemToInsert);

        if (position > list.Length - 1) position = list.Length;
        if ((typeof(T) == itemToInsert.GetType()) || (itemToInsert.GetType().IsSubclassOf(typeof(T))))
        {
            Array.Resize(ref list, list.Length + 1);
            for (int i = list.Length - 1; i > position; i--)
                list[i] = list[i - 1];
            list[position] = (T)itemToInsert;
        }
        return list;
    }

    public static T[] RemoveAt<T>(this T[] list, int index)
    {
        ArgumentNullException.ThrowIfNull(list);

#pragma warning disable S112
        if (index >= list.Length)
            throw new IndexOutOfRangeException();
        if (list.Length == 0)
            throw new IndexOutOfRangeException();
#pragma warning restore S112

        T[] newList = new T[list.Length - 1];
        if (list.Length > 0)
            for (int i = 0; i < list.Length; i++)
                if (i != index) newList[(i > index ? i - 1 : i)] = list[i];

        return newList;
    }

    public static T[] Add<T>(this T[] list, object itemToAdd)
    {
        ArgumentNullException.ThrowIfNull(itemToAdd);

        if ((typeof(T) == itemToAdd.GetType()) || (itemToAdd.GetType().IsSubclassOf(typeof(T))))
        {
            Array.Resize(ref list, list.Length + 1);
            list[^1] = (T)itemToAdd;
        }
        return list;
    }
}
