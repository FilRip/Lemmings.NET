using System;

using Microsoft.Xna.Framework;

namespace Lemmings.NET.Constants;

internal static class MyGame
{
    public static Point GameResolution { get; } = new(1100, 700);
    public static int NumParticles { get; } = 300;
    public static bool Paused { get; set; }
    public static Random Rnd { get; } = new(Environment.TickCount);
    public const string SaveGameFileName = "LevelStats.txt";
    public const int NumTotalLevels = 182;
    public const int totalExplosions = 256;  // be careful with the number of lemmings per level -- this is the size of elements for explosions 500*24
    public const int PARTICLE_NUM = 24; //24
    public const int LIFE_COUNTER = 74; //64 original value
    public const int LIFE_VARIANCE = 16; //16
    public const int useumbrella = 100;
    public const int maxnumberfalling = 210;
}
