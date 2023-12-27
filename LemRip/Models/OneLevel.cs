using System.Collections.Generic;
using System.Linq;

namespace Lemmings.NET.Models;

internal class OneLevel
{
    internal OneLevel() : base() { }

    internal OneLevel(int numLevel) : this()
    {
        NumLevel = numLevel;
    }

    internal int TotalLemmings { get; set; }
    internal int InitPosX { get; set; }
    internal int NumberClimbers { get; set; }
    internal int NumberUmbrellas { get; set; }
    internal int NumberExploders { get; set; }
    internal int NumberBuilders { get; set; }
    internal int NumberBashers { get; set; }
    internal int NumberMiners { get; set; }
    internal int NumberDiggers { get; set; }
    internal int NumberBlockers { get; set; }
    internal int MinFrequencyComming { get; set; }
    internal int FrequencyComming { get; set; }
    internal int NbLemmingsToSave { get; set; }
    internal int TotalTime { get; set; }
    internal int NumLevel { get; set; }

    internal string NameLev { get; set; }
    internal string NameOfLevel { get; set; }

    internal int DoorX { get; set; }
    internal int DoorY { get; set; }
    internal int TypeOfDoor { get; set; }
    internal int ExitX { get; set; }
    internal int ExitY { get; set; }
    internal int TypeOfExit { get; set; }
    internal float DoorExitDepth { get; set; }
    internal List<OneBaseProp> ListAllProps { get; set; } = [];
    internal IEnumerable<T> ListProps<T>() where T : OneBaseProp
    {
        return ListAllProps.OfType<T>();
    }
}
