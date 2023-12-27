using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneAdd : OneBaseProp
{
    internal int Framesecond, NumFrames;
    internal Rectangle AreaDraw;
    internal Texture2D Sprite;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.Add; }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    internal override void Update()
    {
        int startposy = Sprite.Height / NumFrames; // height of each frame inside the whole sprite
        int framepos = startposy * ActFrame; // actual y position of the frame
        int ancho = Sprite.Width;
        int amount = ancho * startposy; // height frame
        Rectangle rectangleFill;
        rectangleFill.X = 0;
        rectangleFill.Y = framepos;
        rectangleFill.Width = ancho;
        rectangleFill.Height = startposy;
        Sprite.GetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colormask22, 0, amount);
        rectangleFill.X = AreaDraw.X;
        rectangleFill.Y = AreaDraw.Y;
        rectangleFill.Width = ancho;
        rectangleFill.Height = startposy;
        MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colormask22, 0, amount);
        int py = AreaDraw.Y;
        int px = AreaDraw.X;
        int cantidad99 = 0;
        for (int yy99 = 0; yy99 < startposy; yy99++)
        {
            int yypos99 = (yy99 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
            for (int xx99 = 0; xx99 < ancho; xx99++)
            {
                MyGame.Instance.ScreenInGame.C25[yypos99 + px + xx99].PackedValue = MyGame.Instance.ScreenInGame.Colormask22[cantidad99].PackedValue;
                cantidad99++;
            }
        }
        if (Frame > Framesecond)
        {
            Frame = 0;
            ActFrame++;
            if (ActFrame >= NumFrames)
                ActFrame = 0;
        }
        Frame++;
    }
}
