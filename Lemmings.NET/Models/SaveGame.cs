using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using Lemmings.NET.Constants;

namespace Lemmings.NET.Models
{
    internal static class SaveGame
    {
        private static LemmingsSaveGame MySaveGameFile { get; set; } = new LemmingsSaveGame() { FinishedLevels = [] };

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
                    NumLevel = numLevel,
                    NbSecondsToComplete = nbSeconds,
                    NumLemmingSaved = numLemmingSaved,
                });
                mustSaved = true;
            }
            else if (numLemmingSaved > lvl.NumLemmingSaved || nbSeconds.CompareTo(lvl.NbSecondsToComplete) < 0)
            {
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

    [XmlRoot()]
    public class LemmingsSaveGame
    {
        [XmlElement()]
        public List<OneSavedLevel> FinishedLevels { get; set; }
    }

    [XmlRoot()]
    public class OneSavedLevel
    {
        [XmlElement()]
        public int NumLevel { get; set; }
        [XmlElement()]
        public int NumLemmingSaved { get; set; }
        [XmlElement()]
        public int NbSecondsToComplete { get; set; }
    }
}
