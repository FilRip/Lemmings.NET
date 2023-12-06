using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lemmings.NET.Models;

[XmlRoot()]
public class LemmingsSaveGame
{
    [XmlElement()]
    public List<OneSavedLevel> FinishedLevels { get; set; }

    [XmlElement()]
    public float Volume { get; set; } = 1.0f;

    [XmlElement()]
    public bool MuteMusic { get; set; }
}
