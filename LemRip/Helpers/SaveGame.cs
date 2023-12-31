﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using Lemmings.NET.Constants;
using Lemmings.NET.Models;

namespace Lemmings.NET.Helpers;

internal static class SaveGame
{
    private static LemmingsSaveGame MySaveGameFile { get; set; } = new LemmingsSaveGame() { FinishedLevels = [] };

    internal static bool MuteMusic
    {
        get { return MySaveGameFile.MuteMusic; }
        set
        {
            MySaveGameFile.MuteMusic = value;
            SaveSavedGame();
        }
    }

    internal static float SoundVolume
    {
        get { return MySaveGameFile.Volume; }
        set
        {
            MySaveGameFile.Volume = value;
            SaveSavedGame();
        }
    }

    internal static bool Scale
    {
        get { return MySaveGameFile.Scale; }
        set
        {
            MySaveGameFile.Scale = value;
            SaveSavedGame();
        }
    }

    internal static bool FullScreen
    {
        get { return MySaveGameFile.FullScreen; }
        set
        {
            MySaveGameFile.FullScreen = value;
            SaveSavedGame();
        }
    }

    internal static void LoadSavedGame()
    {
        if (File.Exists(GlobalConst.SaveGameFileName))
        {
            try
            {
                XmlSerializer serializer = new(typeof(LemmingsSaveGame));
                using FileStream fs = File.OpenRead(GlobalConst.SaveGameFileName);
                MySaveGameFile = (LemmingsSaveGame)serializer.Deserialize(fs);
            }
            catch (Exception)
            {
                SaveSavedGame();
            }
        }
        else
            SaveSavedGame();
    }

    internal static void AddFinishedGame(int numLevel, int nbSeconds, int numLemmingSaved)
    {
        OneSavedLevel lvl = GetLevel(numLevel);
        bool mustSaved = false;
        if (lvl == null)
        {
            MySaveGameFile.FinishedLevels.Add(new OneSavedLevel()
            {
                Finished = true,
                NumLevel = numLevel,
                NbSecondsToComplete = nbSeconds,
                NumLemmingSaved = numLemmingSaved,
            });
            mustSaved = true;
        }
        else if (numLemmingSaved > lvl.NumLemmingSaved || nbSeconds.CompareTo(lvl.NbSecondsToComplete) < 0)
        {
            lvl.Finished = true;
            lvl.NumLemmingSaved = numLemmingSaved;
            lvl.NbSecondsToComplete = nbSeconds;
            mustSaved = true;
        }

        if (mustSaved)
            SaveSavedGame();
    }

    internal static OneSavedLevel GetLevel(int level)
    {
        MySaveGameFile.FinishedLevels ??= [];
        return MySaveGameFile?.FinishedLevels?.SingleOrDefault(l => l.NumLevel == level);
    }

    private static void SaveSavedGame()
    {
        XmlSerializer serializer = new(typeof(LemmingsSaveGame));
        using FileStream fs = File.Create(GlobalConst.SaveGameFileName);
        serializer.Serialize(fs, MySaveGameFile);
    }
}
