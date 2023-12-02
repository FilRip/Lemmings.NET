using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lemmings.NET.Models;

[XmlRoot()]
public class LemmingsSaveGame
{
    [XmlElement()]
    public List<OneSavedLevel> FinishedLevels { get; set; }
}
