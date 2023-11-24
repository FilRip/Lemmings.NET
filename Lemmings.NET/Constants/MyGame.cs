using System;

using Microsoft.Xna.Framework;

namespace Lemmings.NET.Constants
{
    internal static class MyGame
    {
        public static Point GameResolution { get; } = new(1100, 700);
        public static int numParticles { get; } = 300;

        public static Random Rnd { get; } = new(Environment.TickCount);
        public const string SaveGameFileName = "LevelStats.txt";
        public const int NumTotalLevels = 182;
        public const int totalExplosions = 256;  // be careful with the number of lemmings per level -- this is the size of elements for explosions 500*24
    }
}
