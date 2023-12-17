using System;
using System.IO;

namespace ComputeLemmingsLevel;

internal static class ProgramTraps
{
    internal static void Main(string[] args)
    {
        string[] listeLignes;
        string[] splitter;
        int numLevel = 0, lastNumTrap = -1, currentNumTrap;
        string ligneCourante, typeTrap, lastTypeTrap = "", nomPropriete, valeurPropriete;
        listeLignes = File.ReadAllLines($"c:\\tmp\\traps.txt");
        foreach (string ligne in listeLignes)
        {
            ligneCourante = ligne.Trim();
            if (ligneCourante.Contains("case "))
            {
                numLevel = int.Parse(ligneCourante.Replace("case ", "").Replace(":", ""));
                lastNumTrap = -1;
                lastTypeTrap = "";
            }
            else if (ligneCourante.Contains("MyGame.Instance.ScreenInGame.") &&
                !ligneCourante.Contains(" new ") &&
                !ligneCourante.ToLower().Contains("numtot") &&
                !ligneCourante.ToLower().Contains("numactdoor") &&
                !ligneCourante.ToLower().Contains("arrowson") &&
                !ligneCourante.ToLower().Contains("steelon") &&
                !ligneCourante.ToLower().Contains("trapson") &&
                !ligneCourante.ToLower().Contains("platson") &&
                !ligneCourante.ToLower().Contains("addson"))
            {
                ligneCourante = ligneCourante.Replace("MyGame.Instance.ScreenInGame.", "");
                currentNumTrap = int.Parse(ligneCourante.Substring(ligneCourante.IndexOf("[") + 1, ligneCourante.IndexOf("]") - ligneCourante.IndexOf("[") - 1)) + 1;
                typeTrap = ligneCourante.Substring(0, ligneCourante.IndexOf("["));
                splitter = ligneCourante.Substring(ligneCourante.IndexOf(".") + 1).Split('=');
                nomPropriete = splitter[0].Trim();
                valeurPropriete = splitter[1].Trim().Replace(";", "");
                if (valeurPropriete.IndexOf("//") >= 0)
                    valeurPropriete = valeurPropriete.Substring(0, valeurPropriete.IndexOf("//")).Trim();
                if (valeurPropriete.EndsWith("f"))
                    valeurPropriete = valeurPropriete.Substring(0, valeurPropriete.Length - 1);
                if (currentNumTrap != lastNumTrap || lastTypeTrap != typeTrap)
                {
                    File.AppendAllText(Program.repLevels + @"level" + numLevel.ToString() + ".ini", Environment.NewLine);
                    File.AppendAllText(Program.repLevels + @"level" + numLevel.ToString() + ".ini", "[" + typeTrap + currentNumTrap + "]" + Environment.NewLine);
                }
                File.AppendAllText(Program.repLevels + @"level" + numLevel.ToString() + ".ini", nomPropriete + "=" + valeurPropriete + Environment.NewLine);
                lastNumTrap = currentNumTrap;
                lastTypeTrap = typeTrap;
            }
        }
    }
}
