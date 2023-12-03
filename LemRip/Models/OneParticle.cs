using Microsoft.Xna.Framework;

namespace Lemmings.NET.Models;

internal class OneParticle
{
    internal double X { get; set; }
    internal double Y { get; set; }
    internal double Dx { get; set; }
    internal double Dy { get; set; }
    internal Color Color { get; set; }
    internal void SetColorA(byte a)
    {
        Color = new Color(Color, a);
    }
    internal int LifeCtr { get; set; }
    internal int MaxCounter { get; set; }
    internal int Counter { get; set; }
    internal float Rotation { get; set; }
    internal float Size { get; set; }
}
