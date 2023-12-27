using System;

using Microsoft.Xna.Framework;

namespace Lemmings.NET.Constants;

internal static class GlobalConst
{
    internal static Point GameResolution { get; } = new(1100, 700);
    internal static int NumParticles { get; } = 300;
    internal static bool Paused { get; set; }
    internal static Random Rnd { get; } = new(Environment.TickCount);
    internal const string SaveGameFileName = "LevelStats.txt";
    internal const int NumTotalLevels = 182;
    internal const int TotalExplosions = 256;  // be careful with the number of lemmings per level -- this is the size of elements for explosions 500*24
    internal const int PARTICLE_NUM = 24; //24
    internal const int LIFE_COUNTER = 74; //64 original value
    internal const int LIFE_VARIANCE = 16; //16
    internal const int UseUmbrella = 100;
    internal const int MaxNumberFalling = 210;
    internal const float SizeL = 1.35f; //1.2f was default in the beggining
    internal const float Lem_depth = 0.300f;
    internal const double GRAVITY = 0.1; //0.1
}
