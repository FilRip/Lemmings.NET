using System.IO;

using Lemmings.NET.Constants;

namespace Lemmings.NET.Models
{
    internal static class SaveGame
    {
        internal static bool[] FinishedLevel { get; } = new bool[GlobalConst.NumTotalLevels];

        internal static void LoadSavedGame()
        {
            if (File.Exists(GlobalConst.SaveGameFileName))
            {
                BinaryReader reader = new(File.Open(GlobalConst.SaveGameFileName, FileMode.Open));
                for (int i = 0; i < GlobalConst.NumTotalLevels; i++)
                {
                    FinishedLevel[i] = reader.ReadBoolean();
                }
                reader.Close();
            }
            else
                SaveSavedGame();
        }

        internal static void SaveSavedGame()
        {
            BinaryWriter writer = new(File.Open(GlobalConst.SaveGameFileName, FileMode.Create));
            for (int i = 0; i < GlobalConst.NumTotalLevels; i++)
            {
                writer.Write(FinishedLevel[i]);
            }
            writer.Write("(C) 2023 FilRip from CoolBytes");
            writer.Close();
        }
    }
}
