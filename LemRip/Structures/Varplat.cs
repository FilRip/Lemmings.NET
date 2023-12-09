using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs;

internal struct Varplat
{
    public int frame, framesecond, numSteps, actStep, step;
    public bool up;
    public Rectangle areaDraw;
    public Texture2D sprite;
    internal void SetFrame(int newFrame)
    {
        frame = newFrame;
    }
    internal void SetUp(bool newUp)
    {
        up = newUp;
    }
    internal void SetActStep(int newActStep)
    {
        actStep = newActStep;
    }
    internal void SetAreaDrawX(int x)
    {
        areaDraw.X = x;
    }
    internal void SetAreaDrawY(int y)
    {
        areaDraw.Y = y;
    }
}
