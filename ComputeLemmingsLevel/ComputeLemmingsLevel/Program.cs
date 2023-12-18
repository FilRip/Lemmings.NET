using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputeLemmingsLevel;

internal static class Program
{
    internal const string repLevels = @"c:\tmp\levels\";

    internal static void Main(string[] args)
    {
        /*foreach (string file in Directory.GetFiles(repLevels, "*.ini"))
        {
            string[] lignes = File.ReadAllLines(file);
            List<string> listLignes = lignes.ToList();
            listLignes.Insert(0, "[Level]");
            File.Delete(file);
            File.WriteAllLines(file, listLignes);
        }*/
        foreach (string file in Directory.GetFiles(@"D:\Code\FilRip\Lemmings.NET\LemRip\Content\levels", "*.ini"))
        {
            string[] lignes = File.ReadAllLines(file);
            bool changement = false;
            for (int i = 0; i < lignes.Length; i++)
            {
                string ligne = lignes[i];
                if (ligne.Contains("Path["))
                {
                    int j = int.Parse(ligne.Split('=')[0].Replace("Path[", "").Replace("]", "")) + 1;
                    lignes[i] = $"Path{j}=" + ligne.Split('=')[1];
                    changement = true;
                }
            }
            if (changement)
            {
                File.Delete(file);
                File.WriteAllLines(file, lignes);
            }
        }
    }
}
