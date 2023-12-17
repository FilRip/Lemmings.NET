using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputeLemmingsLevel;

internal static class Program
{
    internal const string repLevels = @"c:\tmp\levels\";

    internal static void Main(string[] args)
    {
        foreach (string file in Directory.GetFiles(repLevels, "*.ini"))
        {
            string[] lignes = File.ReadAllLines(file);
            List<string> listLignes = lignes.ToList();
            listLignes.Insert(0, "[Level]");
            File.Delete(file);
            File.WriteAllLines(file, listLignes);
        }
    }
}
