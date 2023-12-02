using System.Xml.Serialization;

namespace Lemmings.NET.Models;

[XmlRoot()]
public class OneSavedLevel
{
    [XmlElement()]
    public int NumLevel { get; set; }

    [XmlElement()]
    public bool Finished { get; set; }

    [XmlElement()]
    public int NumLemmingSaved { get; set; }

    [XmlElement()]
    public int NbSecondsToComplete { get; set; }
}
