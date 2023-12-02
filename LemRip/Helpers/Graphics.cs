using Microsoft.Xna.Framework;

namespace Lemmings.NET.Helpers;

internal static class Graphics
{
    public static Rectangle GetRenderTargetDestination(Point resolution, int preferredBackBufferWidth, int preferredBackBufferHeight)
    {
        float resolutionRatio = (float)resolution.X / resolution.Y;
        float screenRatio;
        Point bounds = new(preferredBackBufferWidth, preferredBackBufferHeight);
        screenRatio = (float)bounds.X / bounds.Y;
        float scale;
        Rectangle rectangle = new();

        if (resolutionRatio < screenRatio)
            scale = (float)bounds.Y / resolution.Y;
        else if (resolutionRatio > screenRatio)
            scale = (float)bounds.X / resolution.X;
        else
        {
            // Resolution and window/screen share aspect ratio
            rectangle.Size = bounds;
            return rectangle;
        }
        rectangle.Width = (int)(resolution.X * scale);
        rectangle.Height = (int)(resolution.Y * scale);
        return CenterRectangle(new Rectangle(Point.Zero, bounds), rectangle);
    }

    public static Rectangle CenterRectangle(Rectangle outerRectangle, Rectangle innerRectangle)
    {
        Point delta = outerRectangle.Center - innerRectangle.Center;
        innerRectangle.Offset(delta);
        return innerRectangle;
    }
}
