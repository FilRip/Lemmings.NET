using Microsoft.Xna.Framework;

namespace Lemmings.NET.Models
{
    public struct Particle
    {
        internal double x, y, dx, dy;
        public Color Color { get; set; }
        public void SetColorA(byte a)
        {
            Color = new Color(Color, a);
        }
        public int LifeCtr { get; set; }
        public int MaxCounter { get; set; }
        public int Counter { get; set; }
        public float Rotation { get; set; }
        public float Size { get; set; }
    }
}
