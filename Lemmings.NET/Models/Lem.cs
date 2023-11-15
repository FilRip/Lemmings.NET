namespace Lemmings.NET.Models
{
    public struct Lem //puto
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Numstairs { get; set; }
        public int Numframes { get; set; }
        public int Actualframe { get; set; }
        public bool Active { get; set; }
        public bool Right { get; set; }
        public bool Left
        {
            get { return !Right; }
        }
        public int Pixelscaida { get; set; }
        public bool Walker { get; set; }
        public bool Blocker { get; set; }
        public bool Fall { get; set; }
        public bool Builder { get; set; }
        public bool Basher { get; set; }
        public bool Miner { get; set; }
        public bool Bridge { get; set; }
        public bool Digger { get; set; }
        public bool Onmouse { get; set; }
        public bool Exit { get; set; }
        public bool Exploser { get; set; }
        public bool Explode { get; set; }
        public bool Dead { get; set; }
        public bool Breakfloor { get; set; }
        public bool Climber { get; set; }
        public bool Climbing { get; set; }
        public bool Umbrella { get; set; }
        public bool Falling { get; set; }
        public bool Framescut { get; set; }
        public bool Burned { get; set; }
        public bool Drown { get; set; }
        public double Time { get; set; }
    }
}
