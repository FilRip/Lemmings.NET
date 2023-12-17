using System;
using System.IO;

namespace ComputeLemmingsLevel;

internal static class ProgramLevels
{
    internal static void Main(string[] args)
    {
        string[] listeLignes;
        string[] splitter;
        string valeur;
        string numLevel;
        listeLignes = File.ReadAllLines($"c:\\tmp\\levels.txt");
        foreach (string ligne in listeLignes)
        {
            splitter = ligne.Split('.');
            numLevel = splitter[0].Replace("AllLevel[", "").Replace("]", "").Trim();
            valeur = splitter[1].Split(';')[0].Trim().Replace("\"", "").Replace(" = ", "=").TrimStart().TrimEnd();
            File.AppendAllText(Program.repLevels + @"level" + numLevel + ".ini", valeur + Environment.NewLine);
        }
    }
}
