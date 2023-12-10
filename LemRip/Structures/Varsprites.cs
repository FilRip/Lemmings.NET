using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Structs;

internal struct Varsprites
{
    internal int AxisX, AxisY, ActFrame, Transparency, R, G, B, Framesecond, Frame, ActVect;
    internal Vector2 Pos, Dest, Center;
    internal float Depth, Rotation, Scale, Typescroll, Speed;
    internal bool MinusScrollX, Minus, Calc;
    internal Texture2D Sprite;
    internal Vector3[] Path;
    internal void SetPosX(float x)
    {
        Pos.X = x;
    }
    internal void SetPosY(float y)
    {
        Pos.Y = y;
    }
    internal void SetPos(Vector2 newPos)
    {
        Pos = newPos;
    }
    internal void SetDestX(float x)
    {
        Dest.X = x;
    }
    internal void SetDestY(float y)
    {
        Dest.Y = y;
    }
    internal void SetFrame(int newFrame)
    {
        Frame = newFrame;
    }
    internal void SetActVect(int newActVect)
    {
        ActVect = newActVect;
    }
    internal void SetActFrame(int newActFrame)
    {
        ActFrame = newActFrame;
    }
    internal void SetMinus(bool newMinus)
    {
        Minus = newMinus;
    }
    internal void SetCalc(bool newCalc)
    {
        Calc = newCalc;
    }
    internal void SetSpeed(float speed)
    {
        Speed = speed;
    }
    internal void SetRotation(float newRotation)
    {
        Rotation = newRotation;
    }
}
