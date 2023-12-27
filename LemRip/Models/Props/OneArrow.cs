using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props;

internal class OneArrow : OneBaseProp
{
    internal Rectangle Area;
    internal bool Right;
    internal Texture2D Arrow, EnvelopArrow;
    internal int Moving, Transparency;
    private int amount22;

    internal override ETypeProp TypeTrap
    {
        get { return ETypeProp.Arrow; }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(EnvelopArrow, new Vector2(Area.X - MyGame.Instance.ScreenInGame.ScrollX, Area.Y - MyGame.Instance.ScreenInGame.ScrollY),
            new Rectangle(0, 0, EnvelopArrow.Width, EnvelopArrow.Height),
            new Color(255, 255, 255, Transparency), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.499f);
    }

    internal override void Update()
    {
        amount22 = Area.Width * Area.Height;
        Arrow.GetData(MyGame.Instance.ScreenInGame.Colormask22, 0, Arrow.Height * Arrow.Width);
        int py = Area.Y;
        int px = Area.X;
        int alto66 = Area.Height;
        int ancho66 = Area.Width;
        amount22 = 0;
        for (int yy88 = 0; yy88 < alto66; yy88++)
        {
            int yypos888 = (yy88 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
            for (int xx88 = 0; xx88 < ancho66; xx88++)
            {
                MyGame.Instance.ScreenInGame.Colorsobre22[amount22].PackedValue = MyGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                amount22++;
            }
        }
        if (Right)
        {
            Moving--;
            if (Moving < 0)
            {
                Moving = Arrow.Width - 1;
            }
            for (int y4 = 0; y4 < Area.Height; y4++)
            {
                for (int x4 = 0; x4 < Area.Width; x4++)
                {
                    int posy456 = y4 % Arrow.Height;
                    int posx456 = x4 % Arrow.Width;
                    posx456 = ((posx456 + Moving) % Arrow.Width);  //Left okok
                    MyGame.Instance.ScreenInGame.Colormasktotal[(y4 * Area.Width) + x4].PackedValue = MyGame.Instance.ScreenInGame.Colormask22[(posy456 * Arrow.Width) + posx456].PackedValue;
                }
            }
            for (int r = 0; r < amount22; r++)
            {
                if (MyGame.Instance.ScreenInGame.Colorsobre22[r].R > 0 || MyGame.Instance.ScreenInGame.Colorsobre22[r].G > 0 || MyGame.Instance.ScreenInGame.Colorsobre22[r].B > 0)
                {
                    MyGame.Instance.ScreenInGame.Colorsobre22[r].PackedValue = MyGame.Instance.ScreenInGame.Colormasktotal[r].PackedValue;
                }
            }
            EnvelopArrow.SetData(MyGame.Instance.ScreenInGame.Colorsobre22, 0, EnvelopArrow.Height * EnvelopArrow.Width);
        }
        else
        {
            Moving++;
            if (Moving < 0)
            {
                Moving = Arrow.Width - 1;
            }
            for (int y4 = 0; y4 < Area.Height; y4++)
            {
                for (int x4 = 0; x4 < Area.Width; x4++)
                {
                    int posy456 = y4 % Arrow.Height;
                    int posx456 = x4 % Arrow.Width;
                    posx456 = (Arrow.Width - 1) - ((posx456 + Moving) % Arrow.Width); // left perfecto
                    MyGame.Instance.ScreenInGame.Colormasktotal[(y4 * Area.Width) + x4].PackedValue = MyGame.Instance.ScreenInGame.Colormask22[(posy456 * Arrow.Width) + posx456].PackedValue;
                }
            }
            for (int r = 0; r < amount22; r++)
            {
                if (MyGame.Instance.ScreenInGame.Colorsobre22[r].R > 0 || MyGame.Instance.ScreenInGame.Colorsobre22[r].G > 0 || MyGame.Instance.ScreenInGame.Colorsobre22[r].B > 0)
                {
                    MyGame.Instance.ScreenInGame.Colorsobre22[r].PackedValue = MyGame.Instance.ScreenInGame.Colormasktotal[r].PackedValue;
                }
            }
            EnvelopArrow.SetData(MyGame.Instance.ScreenInGame.Colorsobre22, 0, EnvelopArrow.Height * EnvelopArrow.Width);
        }
    }
}
