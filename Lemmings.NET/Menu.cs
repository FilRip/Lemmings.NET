using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET
{
    partial class LemmingsNetGame : Game
    {
        private readonly int posm = 742;
        private readonly int posy = 572;
        int i;
        float clickTimer2 = 0, clickTimer1 = 0, angle, lemxscale, lemyscale, mousexscale, mouseyscale, max, min;
        float mxscale, myscale;
        bool click1 = false;
        Vector2 edge;
        float mmscale, mmscale2, xscale, yscale, mmscaley, mmscaley2;
        private int framecae = 0; // 0--3
        private int framecava = 0; //0--15
        private int frameescala = 0; //0--7
        private int frameparaguas = 0; //0--9
        private int frameexplota = 0; //0--15
        private int frameblocker = 0; //0--15
        private int framepuente = 0; //0--15
        private int framepared = 0; //0--31
        private int framepico = 0; //0--23
        private int framblink1 = 0, framblink2 = 0, framblink3 = 0;
        private bool op1 = false, op2 = false, op3 = false, op4 = false, op5 = false, op6 = false, op7 = false, op8 = false, op9 = false, op10 = false, op11 = false, op12 = false, op13 = false;
        private int numeroescalan = 99, numeroparaguas = 88, numeroexplotan = 77, numeroblockers = 66,
            numeropuentes = 55, numeropared = 44, numeropico = 33, numerocavan = 99, r1 = 0, r2 = 0, r3 = 0;
        private float cosa = 0f;
        int zvTime = 0, zv = 0;
        private readonly int posymenu = 575;
        Color sombramenu = Color.SlateGray;
        Color backmenu = Color.MediumSlateBlue;
        private Texture2D texture1pixel;
        const int rry = 581;
        const int rrx = 20;
        const int rhy = 51;
        const int rhx = 50;
        private Rectangle rectop1 = new Rectangle(rrx, rry, rhx, rhy);
        private Rectangle rectop2 = new Rectangle(rrx + 55, rry, rhx, rhy);
        private Rectangle rectop3 = new Rectangle(rrx + 2 * 55, rry, rhx, rhy);
        private Rectangle rectop4 = new Rectangle(rrx + 3 * 55, rry, rhx, rhy);
        private Rectangle rectop5 = new Rectangle(rrx + 4 * 55, rry, rhx, rhy);
        private Rectangle rectop6 = new Rectangle(rrx + 5 * 55, rry, rhx, rhy);
        private Rectangle rectop7 = new Rectangle(rrx + 6 * 55, rry, rhx, rhy);
        private Rectangle rectop8 = new Rectangle(rrx + 7 * 55, rry, rhx, rhy);
        private Rectangle rectop9 = new Rectangle(rrx + 8 * 55, rry, rhx, rhy);
        private Rectangle rectop10 = new Rectangle(rrx + 9 * 55, rry, rhx, rhy);
        private Rectangle rectop11 = new Rectangle(rrx + 10 * 55, rry, rhx, rhy);
        private Rectangle rectop12 = new Rectangle(rrx + 11 * 55, rry, rhx, rhy);
        private Rectangle rectop13 = new Rectangle(rrx + 12 * 55, rry, rhx, rhy);
        private Rectangle rectminimenu = new Rectangle(742, 572, 336, 84);

        private Vector2 vectorFill, vectorFill2;
        private Rectangle rectangleFill, rectangleFill2;
        private Color colorFill;
        private bool _alreadyPlayed;

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color pintado, Int32 grosor, float layer)
        {
            edge = end - start;// calculate angle to rotate line
            angle = (float)Math.Atan2(edge.Y, edge.X);
            rectangleFill.X = (int)start.X;
            rectangleFill.Y = (int)start.Y;
            rectangleFill.Width = (int)edge.Length();
            rectangleFill.Height = grosor;
            sb.Draw(texture1pixel, rectangleFill, null, pintado, angle, Vector2.Zero, SpriteEffects.None, layer);
        }
        void PlaySoundMenu()
        {
            if (changeopInstance.State == SoundState.Playing)
            {
                changeopInstance.Stop();
            }
            try
            {
                changeopInstance.Play();
            }
            catch (InstancePlayLimitException) { /* Ignore errors */ }
        }

        void TextLem(string txt, Vector2 start, Color pinta, float size, float layer)
        {
            for (i = 0; i <= txt.Length - 1; i++)
            {
                A = Convert.ToInt32(txt[i]);
                switch (A)
                {
                    case 57:
                        A = 26 * 24;
                        break;
                    case 56:
                        A = 26 * 23;
                        break;
                    case 55:
                        A = 26 * 22;
                        break;
                    case 54:
                        A = 26 * 21;
                        break;
                    case 53:
                        A = 26 * 20;
                        break;
                    case 52:
                        A = 26 * 19;
                        break;
                    case 51:
                        A = 26 * 18;
                        break;
                    case 50:
                        A = 26 * 17;
                        break;
                    case 49:
                        A = 26 * 16;
                        break;
                    case 48:
                        A = 26 * 15;
                        break;
                    case 65:
                        A = 26 * 32;
                        break;
                    case 66:
                        A = 26 * 33;
                        break;
                    case 67:
                        A = 26 * 34;
                        break;
                    case 68:
                        A = 26 * 35;
                        break;
                    case 69:
                        A = 26 * 36;
                        break;
                    case 70:
                        A = 26 * 37;
                        break;
                    case 71:
                        A = 26 * 38;
                        break;
                    case 72:
                        A = 26 * 39;
                        break;
                    case 73:
                        A = 26 * 40;
                        break;
                    case 74:
                        A = 26 * 41;
                        break;
                    case 75:
                        A = 26 * 42;
                        break;
                    case 76:
                        A = 26 * 43;
                        break;
                    case 77:
                        A = 26 * 44;
                        break;
                    case 78:
                        A = 26 * 45;
                        break;
                    case 79:
                        A = 26 * 46;
                        break;
                    case 80:
                        A = 26 * 47;
                        break;
                    case 81:
                        A = 26 * 48;
                        break;
                    case 82:
                        A = 26 * 49;
                        break;
                    case 83:
                        A = 26 * 50;
                        break;
                    case 84:
                        A = 26 * 51;
                        break;
                    case 85:
                        A = 26 * 52;
                        break;
                    case 86:
                        A = 26 * 53;
                        break;
                    case 87:
                        A = 26 * 54;
                        break;
                    case 88:
                        A = 26 * 55;
                        break;
                    case 89:
                        A = 26 * 56;
                        break;
                    case 90:
                        A = 26 * 57;
                        break;
                    case 97:
                        A = 26 * 64;
                        break;
                    case 98:
                        A = 26 * 65;
                        break;
                    case 99:
                        A = 26 * 66;
                        break;
                    case 100:
                        A = 26 * 67;
                        break;
                    case 101:
                        A = 26 * 68;
                        break;
                    case 102:
                        A = 26 * 69;
                        break;
                    case 103:
                        A = 26 * 70;
                        break;
                    case 104:
                        A = 26 * 71;
                        break;
                    case 105:
                        A = 26 * 72;
                        break;
                    case 106:
                        A = 26 * 73;
                        break;
                    case 107:
                        A = 26 * 74;
                        break;
                    case 108:
                        A = 26 * 75;
                        break;
                    case 109:
                        A = 26 * 76;
                        break;
                    case 110:
                        A = 26 * 77;
                        break;
                    case 111:
                        A = 26 * 78;
                        break;
                    case 112:
                        A = 26 * 79;
                        break;
                    case 113:
                        A = 26 * 80;
                        break;
                    case 114:
                        A = 26 * 81;
                        break;
                    case 115:
                        A = 26 * 82;
                        break;
                    case 116:
                        A = 26 * 83;
                        break;
                    case 117:
                        A = 26 * 84;
                        break;
                    case 118:
                        A = 26 * 85;
                        break;
                    case 119:
                        A = 26 * 86;
                        break;
                    case 120:
                        A = 26 * 87;
                        break;
                    case 121:
                        A = 26 * 88;
                        break;
                    case 122:
                        A = 26 * 89;
                        break;
                    case 33:
                        A = 0;
                        break;
                    case 34:
                        A = 26;
                        break;
                    case 35:
                        A = 26 * 2;
                        break;
                    case 36:
                        A = 26 * 3;
                        break;
                    case 37:
                        A = 26 * 4;
                        break;
                    case 38:
                        A = 26 * 5;
                        break;
                    case 39:
                        A = 26 * 6;
                        break;
                    case 40:
                        A = 26 * 7;
                        break;
                    case 41:
                        A = 26 * 8;
                        break;
                    case 42:
                        A = 26 * 9;
                        break;
                    case 43:
                        A = 26 * 10;
                        break;
                    case 44:
                        A = 26 * 11;
                        break;
                    case 45:
                        A = 26 * 12;
                        break;
                    case 46:
                        A = 26 * 13;
                        break;
                    case 47:
                        A = 26 * 14;
                        break;
                    case 58:
                        A = 26 * 25;
                        break;
                    case 59:
                        A = 26 * 26;
                        break;
                    case 60:
                        A = 26 * 27;
                        break;
                    case 61:
                        A = 26 * 28;
                        break;
                    case 62:
                        A = 26 * 29;
                        break;
                    case 63:
                        A = 26 * 30;
                        break;
                    case 64:
                        A = 26 * 31;
                        break;
                    case 91:
                        A = 26 * 58;
                        break;
                    case 92:
                        A = 26 * 59;
                        break;
                    case 93:
                        A = 26 * 60;
                        break;
                    case 94:
                        A = 26 * 61;
                        break;
                    case 95:
                        A = 26 * 62;
                        break;
                    case 180:
                        A = 26 * 63;
                        break;
                    case 123:
                        A = 26 * 90;
                        break;
                    case 124:
                        A = 26 * 91;
                        break;
                    case 125:
                        A = 26 * 92;
                        break;
                    case 126:
                        A = 26 * 93;
                        break;
                    default:
                        break;
                }
                start.X += 19 * size;  // ancho de lemfont (18X26) 18+1 para dejar espacio entre chars
                rectangleFill.X = 0;
                rectangleFill.Y = A;
                rectangleFill.Width = 18;
                rectangleFill.Height = 26;
                if (A != 32)
                    spriteBatch.Draw(lemfont, start, rectangleFill, pinta, 0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }
        }

        partial void Menu_draw()
        {
            rectangleFill.X = 0;
            rectangleFill.Y = 513;
            rectangleFill.Width = 1100;
            rectangleFill.Height = 184;
            rectangleFill2.X = 0;
            rectangleFill2.Y = 0;
            rectangleFill2.Width = 1100;
            rectangleFill2.Height = 184;
            spriteBatch.Draw(backmenu2, rectangleFill, rectangleFill2, backmenu, 0f, Vector2.Zero, SpriteEffects.None, 0.251f);
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = backlogo.Width;
            rectangleFill.Height = backlogo.Height;
            vectorFill.X = 130;
            vectorFill.Y = 622;
            colorFill.R = 100;
            colorFill.G = 100;
            colorFill.B = 100;
            colorFill.A = 200;
            spriteBatch.Draw(backlogo, vectorFill, rectangleFill, colorFill, 0f, Vector2.Zero, 0.78f, SpriteEffects.None, 0.215f);  // logo del menu
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = backlogo.Width;
            rectangleFill.Height = backlogo.Height;
            vectorFill.X = 140;
            vectorFill.Y = 625;
            spriteBatch.Draw(backlogo, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.115f);  // logo del menu
            rectangleFill.X = 0;
            rectangleFill.Y = framblink1 * 12;
            rectangleFill.Width = blink1.Width;
            rectangleFill.Height = 12;
            vectorFill.X = 158;
            vectorFill.Y = 654;
            spriteBatch.Draw(blink1, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.104f);
            rectangleFill.X = 0;
            rectangleFill.Y = framblink2 * 12;
            rectangleFill.Width = blink2.Width;
            rectangleFill.Height = 12;
            vectorFill.X = 329;
            vectorFill.Y = 654;
            spriteBatch.Draw(blink2, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.104f);
            rectangleFill.X = 0;
            rectangleFill.Y = framblink3 * 12;
            rectangleFill.Width = blink3.Width;
            rectangleFill.Height = 12;
            vectorFill.X = 506;
            vectorFill.Y = 648;
            spriteBatch.Draw(blink3, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.104f);
            rectangleFill.X = 735;
            rectangleFill.Y = 564;
            rectangleFill.Width = 350;
            rectangleFill.Height = 100;
            rectangleFill2.X = 0;
            rectangleFill2.Y = 0;
            rectangleFill2.Width = cuadrado_menu.Width;
            rectangleFill2.Height = cuadrado_menu.Height;
            spriteBatch.Draw(cuadrado_menu, rectangleFill, rectangleFill2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            vectorFill.X = 0;
            vectorFill.Y = graphics.PreferredBackBufferHeight - 188;
            vectorFill2.X = graphics.PreferredBackBufferWidth;
            vectorFill2.Y = graphics.PreferredBackBufferHeight - 188;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.White, 2, 0.1f);
            vectorFill.X = 0;
            vectorFill.Y = graphics.PreferredBackBufferHeight - 2;
            vectorFill2.X = graphics.PreferredBackBufferWidth;
            vectorFill2.Y = graphics.PreferredBackBufferHeight - 2;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.White, 2, 0.1f);
            vectorFill.X = 741;
            vectorFill.Y = 572;
            vectorFill2.X = 741 + 338;
            vectorFill2.Y = 572;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Black, 84, 0.1f);
            vectorFill.X = posm;
            vectorFill.Y = 572;
            colorFill.R = 70;
            colorFill.G = 70;
            colorFill.B = 70;
            colorFill.A = 255;
            vectorFill2.X = xscale;
            vectorFill2.Y = yscale;
            spriteBatch.Draw(earth, vectorFill, null, colorFill, 0f, Vector2.Zero, vectorFill2, /*scale is the real proportion*/ SpriteEffects.None, 0.090f);
            //draw yellow lines on the mini menu
            vectorFill.X = posm + mmscale;
            vectorFill.Y = posy + mmscaley;
            vectorFill2.X = posm + mmscale;
            vectorFill2.Y = posy + mmscaley2 + 1;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
            vectorFill.X = posm + mmscale2;
            vectorFill.Y = posy + mmscaley;
            vectorFill2.X = posm + mmscale2;
            vectorFill2.Y = posy + mmscaley2 + 1;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
            vectorFill.X = posm + mmscale;
            vectorFill.Y = posy + mmscaley;
            vectorFill2.X = posm + mmscale2;
            vectorFill2.Y = posy + mmscaley;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
            vectorFill.X = posm + mmscale;
            vectorFill.Y = posy + mmscaley2;
            vectorFill2.X = posm + mmscale2;
            vectorFill2.Y = posy + mmscaley2;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
            for (i = 0; i < numerosaca; i++)
            {
                if (!lemming[i].Dead)
                {
                    lemxscale = (float)(lemming[i].Posx + 12) * xscale;
                    lemyscale = (float)(lemming[i].Posy + 20) * yscale;
                    vectorFill.X = posm + lemxscale;
                    vectorFill.Y = 572 + lemyscale;
                    vectorFill2.X = posm + lemxscale + 2;
                    vectorFill2.Y = 572 + lemyscale;
                    DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Magenta, 2, 0.001f);
                }
            }
            vectorFill.X = 80 - 55;
            vectorFill.Y = 560;
            TextLem(string.Format("{0,2:D2}", numerominfrecuencia), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80;
            TextLem(string.Format("{0,2:D2}", numerofrecuencia), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 55;
            TextLem(string.Format("{0,2:D2}", numeroescalan), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 2 * 55;
            TextLem(string.Format("{0,2:D2}", numeroparaguas), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 3 * 55;
            TextLem(string.Format("{0,2:D2}", numeroexplotan), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 4 * 55;
            TextLem(string.Format("{0,2:D2}", numeroblockers), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 5 * 55;
            TextLem(string.Format("{0,2:D2}", numeropuentes), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 6 * 55;
            TextLem(string.Format("{0,2:D2}", numeropared), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 7 * 55;
            TextLem(string.Format("{0,2:D2}", numeropico), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 80 + 8 * 55;
            TextLem(string.Format("{0,2:D2}", numerocavan), vectorFill, Color.LimeGreen, 0.5f, 0.1f);
            vectorFill.X = 890;
            vectorFill.Y = 518;
            TextLem("Time", vectorFill, Color.Yellow, 1f, 0.1f);
            zvTime = (level[levelNumber].totalTime * 60) - (int)tiempototal;
            if (zvTime < 0)
                zvTime = -1; //see that -1 is necesary for end by time -- it test (zvtime < 0)
            vectorFill.X = 982;
            vectorFill.Y = 518;
            TextLem(string.Format("{0,2:D2}", zvTime / 60) + ":" + string.Format("{0,2:D2}", zvTime % 60), vectorFill, Color.Lime, 1f, 0.1f);
            vectorFill.X = 1016;
            vectorFill.Y = 547;
            TextLem(string.Format("{0,2:D2}", zv / 60) + ":" + string.Format("{0,2:D2}", zv % 60), vectorFill, Color.Green, 0.5f, 0.1f);
            vectorFill.X = 650;
            vectorFill.Y = 518;
            TextLem("Home:" + string.Format("{0}", Numerodentro) + "/" + string.Format("{0}", Lemsneeded), vectorFill, Color.Cyan, 1f, 0.1f);
            vectorFill.X = 320;
            vectorFill.Y = 518;
            TextLem("Out:" + string.Format("{0}", numerosaca) + "/" + string.Format("{0}", Numlems), vectorFill, Color.Magenta, 1f, 0.1f);
            vectorFill.X = 530;
            vectorFill.Y = 518;
            TextLem("In:" + string.Format("{0}", numlemnow), vectorFill, Color.AliceBlue, 1f, 0.1f);
            // Kind of lemming SKILLS SKILL SKILLS
            vectorFill.X = 0;
            vectorFill.Y = 518;
            TextLem(LemSkill, vectorFill, Color.GreenYellow, 1f, 0.1f);
            for (i = 0; i <= 12; i++)
            {
                vectorFill.X = 12 + i * 55;
                vectorFill.Y = posymenu;
                rectangleFill.X = 0;
                rectangleFill.Y = 0;
                rectangleFill.Width = circulo_led.Width;
                rectangleFill.Height = circulo_led.Height;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
            } //todas casillas desactivadas
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = circulo_led.Width;
            rectangleFill.Height = circulo_led.Height;
            vectorFill2.X = circulo_led.Width / 2.0f;
            vectorFill2.Y = circulo_led.Height / 2.0f;
            if (op1)
            {
                vectorFill.X = 45;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 40;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(menos, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            else
            {
                vectorFill.X = 45;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(menos, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            if (op2)
            {
                vectorFill.X = 45 + 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 40;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(mas, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            else
            {
                vectorFill.X = 45 + 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(mas, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            if (op3)  //climber
            {
                vectorFill.X = 45 + 2 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 10 + 2 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = frameescala * climber_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = climber_with;
                rectangleFill2.Height = climber_height;
                spriteBatch.Draw(escala, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, climber_size, SpriteEffects.None, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 2 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 10 + 2 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = climber_with;
                rectangleFill2.Height = climber_height;
                spriteBatch.Draw(escala, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, climber_size, SpriteEffects.None, 0.1f);
            }
            if (op4)
            {
                vectorFill.X = 45 + 3 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 5 + 3 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = frameparaguas * floater_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = floater_with;
                rectangleFill2.Height = floater_height;
                spriteBatch.Draw(paraguas, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, 0.55f, SpriteEffects.None, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 3 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 5 + 3 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = floater_with * 4;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = floater_with;
                rectangleFill2.Height = floater_height;
                spriteBatch.Draw(paraguas, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, 0.55f, SpriteEffects.None, 0.1f);
            }
            if (op5)
            {
                vectorFill.X = 45 + 4 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = -5 + 4 * 55;
                vectorFill.Y = posymenu - 20;
                rectangleFill2.X = frameexplota * bomber_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = bomber_with;
                rectangleFill2.Height = bomber_height;
                spriteBatch.Draw(explota, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, bomber_size, SpriteEffects.None, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 4 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = -5 + 4 * 55;
                vectorFill.Y = posymenu - 20;
                rectangleFill2.X = bomber_with * 7;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = bomber_with;
                rectangleFill2.Height = bomber_height;
                spriteBatch.Draw(explota, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, bomber_size, SpriteEffects.None, 0.1f);
            }
            if (op6)
            {
                vectorFill.X = 45 + 5 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 10 + 5 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = frameblocker * blocker_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = blocker_with;
                rectangleFill2.Height = blocker_height;
                spriteBatch.Draw(blocker, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, blocker_size, SpriteEffects.None, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 5 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 10 + 5 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = blocker_with;
                rectangleFill2.Height = blocker_height;
                spriteBatch.Draw(blocker, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, blocker_size, SpriteEffects.None, 0.1f);
            }
            if (op7)
            {
                vectorFill.X = 45 + 6 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 6 + 6 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = framepuente * builder_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = builder_with;
                rectangleFill2.Height = builder_height;
                spriteBatch.Draw(puente, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, builder_size, SpriteEffects.None, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 6 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 6 + 6 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = builder_with * 12;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = builder_with;
                rectangleFill2.Height = builder_height;
                spriteBatch.Draw(puente, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, builder_size, SpriteEffects.None, 0.1f);
            }
            if (op8)
            {
                vectorFill.X = 45 + 7 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 10 + 7 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = framepared * basher_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = basher_with;
                rectangleFill2.Height = basher_height;
                spriteBatch.Draw(pared, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, basher_size, SpriteEffects.FlipHorizontally, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 7 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 20 + 7 * 55;
                vectorFill.Y = posymenu;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = basher_with;
                rectangleFill2.Height = basher_height;
                spriteBatch.Draw(pared, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, basher_size, SpriteEffects.FlipHorizontally, 0.1f);
            }
            if (op9) //PICO PICO
            {
                vectorFill.X = 45 + 8 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 10 + 8 * 55;
                vectorFill.Y = posymenu + 7;
                rectangleFill2.X = framepico * pico_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = pico_with;
                rectangleFill2.Height = pico_height;
                spriteBatch.Draw(pico, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, pico_size, SpriteEffects.None, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 8 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 10 + 8 * 55;
                vectorFill.Y = posymenu + 7;
                rectangleFill2.X = pico_with * 30;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = pico_with;
                rectangleFill2.Height = pico_height;
                spriteBatch.Draw(pico, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, pico_size, SpriteEffects.None, 0.1f);
            }
            if (op10)
            {
                vectorFill.X = 45 + 9 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 505;
                vectorFill.Y = posymenu;
                rectangleFill2.X = framecava * digger_with;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = digger_with;
                rectangleFill2.Height = digger_height;
                spriteBatch.Draw(digger, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, digger_size, SpriteEffects.None, 0.1f);
            }
            else
            {
                vectorFill.X = 45 + 9 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.11f);
                vectorFill.X = 505;
                vectorFill.Y = posymenu;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = digger_with;
                rectangleFill2.Height = digger_height;
                spriteBatch.Draw(digger, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, digger_size, SpriteEffects.None, 0.1f);
            }
            if (op11)
            {
                vectorFill.X = 45 + 10 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 10 * 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 40;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(pausa, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            else
            {
                vectorFill.X = 45 + 10 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 10 * 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(pausa, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            if (op12)
            {
                vectorFill.X = 45 + 11 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 11 * 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 40;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(bomba, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            else
            {
                vectorFill.X = 45 + 11 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 11 * 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(bomba, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            if (op13)
            {
                vectorFill.X = 45 + 12 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, Color.White, cosa, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 12 * 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 40;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(avanzar, vectorFill, rectangleFill2, Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
            else
            {
                vectorFill.X = 45 + 12 * 55;
                vectorFill.Y = posymenu + 31;
                spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, sombramenu, 0f, vectorFill2, 1f, SpriteEffects.None, 0.1f);
                vectorFill.X = 24 + 12 * 55;
                vectorFill.Y = posymenu + 6;
                rectangleFill2.X = 0;
                rectangleFill2.Y = 0;
                rectangleFill2.Width = 32;
                rectangleFill2.Height = 40;
                spriteBatch.Draw(avanzar, vectorFill, rectangleFill2, sombramenu, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
            }
        }
        partial void Menu_logic()
        {
            if (r1 == 0)
            {
                r1 = rnd.Next(30, 90);
            }
            if (r2 == 0)
            {
                r2 = rnd.Next(60, 120);
            }
            if (r3 == 0)
            {
                r3 = rnd.Next(40, 90);
            }
            if (earth.Width > 1100)
                xscale = (float)336 / earth.Width;
            else
                xscale = (float)336 / 1100;
            if (earth.Height > graphics.PreferredBackBufferHeight - 188)
                yscale = (float)84 / earth.Height;
            else
                yscale = (float)84 / (graphics.PreferredBackBufferHeight - 188);
            // float scale = Math.Min(xscale, yscale);  // scale from voth axis for real size
            mmscale = (xscroll) * xscale;
            mmscale2 = (xscroll + 1100) * xscale;
            mmscaley = (yscroll) * yscale;
            mmscaley2 = (yscroll + graphics.PreferredBackBufferHeight - 188) * yscale;
            mmscale = (int)mmscale;
            mmscale2 = (int)mmscale2;
            mmscaley = (int)mmscaley;
            mmscaley2 = (int)mmscaley2; //let decimals out for better mini-map size
            if (Frame % r1 == 0 && !blink1on)
            {
                framblink1 = 0;
                blink1on = true;
            }  // bbbbbbbbbbbbbbllllllllllllllblinking eyes menu 1-2-3
            if (blink1on && dibuja)
            {
                framblink1++;
                if (framblink1 > 8)
                {
                    blink1on = false;
                    r1 = 0;
                }
            }
            if (Frame % r2 == 0 && !blink2on)
            {
                framblink2 = 0;
                blink2on = true;
            }
            if (blink2on && dibuja)
            {
                framblink2++;
                if (framblink2 > 8)
                {
                    blink2on = false;
                    r2 = 0;
                }
            }
            if (Frame % r3 == 0 && !blink3on)
            {
                framblink3 = 0;
                blink3on = true;
            }
            if (blink3on && dibuja)
            {
                framblink3++;
                if (framblink3 > 8)
                {
                    blink3on = false;
                    r3 = 0;
                }
            }
            zv = (int)tiempototal;
            if (fade)
            {
                zv = 0;
                Contadortime = 0;
            }
            if (dibuja2)
            {
                framecae++;
                if (framecae > 3)
                {
                    framecae = 0;
                }
                framecava++;
                if (framecava > 28)
                {
                    framecava = 0;
                }
                frameescala++;
                if (frameescala > climber_frames - 1)
                {
                    frameescala = 0;
                }
                frameparaguas++;
                if (frameparaguas > floater_frames - 1)
                {
                    frameparaguas = 0;
                }
                frameexplota++;
                if (frameexplota > bomber_frames - 1)
                {
                    frameexplota = 0;
                }
                frameblocker++;
                if (frameblocker > blocker_frames - 1)
                {
                    frameblocker = 0;
                }
                framepuente++;
                if (framepuente > builder_frames - 1)
                {
                    framepuente = 0;
                }
                framepared++;
                if (framepared > basher_frames - 1)
                {
                    framepared = 0;
                }
                framepico++;
                if (framepico > pico_frames - 1)
                {
                    framepico = 0;
                }
            }
            if (changeopInstance.State == SoundState.Playing && (op1 || op2))
            {
                changeopInstance.Stop();
                changeopInstance.Pitch = -1f + numerofrecuencia * 0.02f;
                changeopInstance.Volume = 0.25f + numerofrecuencia * 0.005f;
            }
            else
            {
                changeopInstance.Pitch = 0;
                changeopInstance.Volume = 1f;
            }
            cosa += 0.05f;
            if (cosa > 12.5)
            {
                cosa = 0;
            } // menu selection rotation speed
            x.X = mouseActState.Position.X + 14;
            x.Y = mouseActState.Position.Y + 14;
            // medium position for bucle medx medy
            if (rectop1.Contains(x) && mouseActState.LeftButton == ButtonState.Pressed)
            {
                changeopInstance.Pitch = -1f + numerofrecuencia * 0.02f;
                changeopInstance.Volume = 0.25f + numerofrecuencia * 0.005f;
                if (changeopInstance.State == SoundState.Stopped)
                    try
                    {
                        changeopInstance.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                if (numerofrecuencia == numerominfrecuencia)
                {
                    changeopInstance.Stop();
                }
                op1 = true;
                if (dibuja2)
                    numerofrecuencia -= 1; // on monogame 3.6 crash if frecuencia -1 only puto puto
                if (numerofrecuencia < numerominfrecuencia)
                    numerofrecuencia = numerominfrecuencia;
            }
            else
            {
                op1 = false;
            }
            if (rectop2.Contains(x) && mouseActState.LeftButton == ButtonState.Pressed)
            {
                changeopInstance.Pitch = -1f + numerofrecuencia * 0.02f;
                changeopInstance.Volume = 0.25f + numerofrecuencia * 0.005f;
                if (changeopInstance.State == SoundState.Stopped)
                    try
                    {
                        changeopInstance.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                if (numerofrecuencia == 99)
                {
                    changeopInstance.Stop();
                }
                op2 = true;
                if (dibuja2)
                    numerofrecuencia += 1; // on monogame 3.6 crash if frecuencia +1 only
                if (numerofrecuencia > 99)
                    numerofrecuencia = 99;
            }
            else
            {
                op2 = false;
            }
            if (rectop3.Contains(x) && numeroescalan > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                op3 = true;
                op4 = false;
                op5 = false;
                op6 = false;
                op7 = false;
                op8 = false;
                op9 = false;
                op10 = false;
            }
            if (rectop4.Contains(x) && numeroparaguas > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                op3 = false;
                op4 = true;
                op5 = false;
                op6 = false;
                op7 = false;
                op8 = false;
                op9 = false;
                op10 = false;
            }
            if (rectop5.Contains(x) && numeroexplotan > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                op3 = false;
                op4 = false;
                op5 = true;
                op6 = false;
                op7 = false;
                op8 = false;
                op9 = false;
                op10 = false;
            }
            if (rectop6.Contains(x) && numeroblockers > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                op3 = false;
                op4 = false;
                op5 = false;
                op6 = true;
                op7 = false;
                op8 = false;
                op9 = false;
                op10 = false;
            }
            if (rectop7.Contains(x) && numeropuentes > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            { PlaySoundMenu(); op3 = false; op4 = false; op5 = false; op6 = false; op7 = true; op8 = false; op9 = false; op10 = false; }
            if (rectop8.Contains(x) && numeropared > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            { PlaySoundMenu(); op3 = false; op4 = false; op5 = false; op6 = false; op7 = false; op8 = true; op9 = false; op10 = false; }
            if (rectop9.Contains(x) && numeropico > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            { PlaySoundMenu(); op3 = false; op4 = false; op5 = false; op6 = false; op7 = false; op8 = false; op9 = true; op10 = false; }
            if (rectop10.Contains(x) && numerocavan > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            { PlaySoundMenu(); op3 = false; op4 = false; op5 = false; op6 = false; op7 = false; op8 = false; op9 = false; op10 = true; }
            if (rectop11.Contains(x) && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            { PlaySoundMenu(); if (op11) { op11 = false; Paused = false; } else { op11 = true; Paused = true; } }
            if (rectop12.Contains(x) && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed) && !op12)
            {
                if (!click1)
                {
                    click1 = true;
                    clickTimer1 = (float)milisegundos;
                }
                else
                {
                    click1 = false;
                    clickTimer2 = (float)milisegundos;
                }
                max = MathHelper.Max(clickTimer1, clickTimer2);
                min = MathHelper.Min(clickTimer1, clickTimer2);
                if (max - min < 300)
                {
                    PlaySoundMenu();
                    op12 = true; allBlow = true;
                }
            } // BOMBERS ALL
            if (mouseActState.LeftButton == ButtonState.Released)
                _alreadyPlayed = false;
            if (rectop13.Contains(x) && mouseActState.LeftButton == ButtonState.Pressed)  //FAST FORWARD
            {
                if (changeopInstance.State == SoundState.Playing)
                {
                    changeopInstance.Resume();
                }
                try
                {
                    if (!_alreadyPlayed)
                    {
                        changeopInstance.Play();
                        _alreadyPlayed = true;
                    }
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
                op13 = true; this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 180.0f);
            } // 120--240 van ok mas no lo se depende creo
            else
            {
                op13 = false;
                this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f);
            }
            if (rectminimenu.Contains(x) && mouseActState.LeftButton == ButtonState.Pressed)
            {
                mmscale = (float)(xscroll) * xscale;
                mmscale2 = (float)(xscroll + 1100) * xscale;
                mmscaley = (float)(yscroll) * yscale;
                mmscaley2 = (float)(yscroll + graphics.PreferredBackBufferHeight - 188) * yscale;
                mxscale = (float)earth.Width / 336;
                myscale = (float)earth.Height / 84;
                mousexscale = ((mouseActState.Position.X - posm + 14) * mxscale) - (graphics.PreferredBackBufferWidth / 2); // center x axis in minimap (xscroll)
                mouseyscale = ((mouseActState.Position.Y - posy) + 28) * myscale;
                xscroll = (int)mousexscale;
                if (xscroll + graphics.PreferredBackBufferWidth > earth.Width)
                {
                    xscroll = earth.Width - graphics.PreferredBackBufferWidth;
                }
                if (xscroll < 0)
                {
                    xscroll = 0;
                }
                yscroll = (int)mouseyscale - graphics.PreferredBackBufferHeight - 188;
                if (yscroll + graphics.PreferredBackBufferHeight - 188 > earth.Height)
                {
                    yscroll = earth.Height - graphics.PreferredBackBufferHeight - 188;
                }
                if (yscroll < 0)
                {
                    yscroll = 0;
                }
                mmscale = (int)mmscale;
                mmscale2 = (int)mmscale2;
                mmscaley = (int)mmscaley;
                mmscaley2 = (int)mmscaley2; //let decimals out for better mini-map size
            }
        }
    }
}
