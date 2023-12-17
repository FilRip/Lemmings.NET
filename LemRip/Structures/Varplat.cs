using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs;

internal struct Varplat
{
    public int Frame, Framesecond, NumSteps, ActStep, Step;
    public bool Up;
    public Rectangle AreaDraw;
    public Texture2D Sprite;

    internal void SetFrame(int newFrame)
    {
        Frame = newFrame;
    }
    internal void SetUp(bool newUp)
    {
        Up = newUp;
    }
    internal void SetActStep(int newActStep)
    {
        ActStep = newActStep;
    }
    internal void SetAreaDrawX(int x)
    {
        AreaDraw.X = x;
    }
    internal void SetAreaDrawY(int y)
    {
        AreaDraw.Y = y;
    }
}
