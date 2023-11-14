using System;

using Lemmings.NET.Constants;

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
        float angle, lemxscale, lemyscale, mousexscale, mouseyscale;
        private double clickTimer1 = 0;
        float mxscale, myscale;
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
        private ECurrentSkill _currentSelectedSkill;
        private bool op1 = false, op2 = false, op12 = false, op13 = false;
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

        private void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color pintado, Int32 grosor, float layer)
        {
            edge = end - start;// calculate angle to rotate line
            angle = (float)Math.Atan2(edge.Y, edge.X);
            rectangleFill.X = (int)start.X;
            rectangleFill.Y = (int)start.Y;
            rectangleFill.Width = (int)edge.Length();
            rectangleFill.Height = grosor;
            sb.Draw(texture1pixel, rectangleFill, null, pintado, angle, Vector2.Zero, SpriteEffects.None, layer);
        }
        private void PlaySoundMenu()
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

        private void TextLem(string txt, Vector2 start, Color pinta, float size, float layer)
        {
            for (i = 0; i <= txt.Length - 1; i++)
            {
                A = Convert.ToInt32(txt[i]);
                start.X += 19 * size;  // ancho de lemfont (18X26) 18+1 para dejar espacio entre chars
                if (A == 32)
                    continue;
                rectangleFill.X = 0;
                rectangleFill.Y = 26 * (A - 33);
                rectangleFill.Width = 18;
                rectangleFill.Height = 26;
                spriteBatch.Draw(lemfont, start, rectangleFill, pinta, 0f, Vector2.Zero, size, SpriteEffects.None, layer);
            }
        }

        private void Menu_draw()
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
            vectorFill.Y = gameResolution.Y - 188;
            vectorFill2.X = gameResolution.X;
            vectorFill2.Y = gameResolution.Y - 188;
            DrawLine(spriteBatch, vectorFill, vectorFill2, Color.White, 2, 0.1f);
            vectorFill.X = 0;
            vectorFill.Y = gameResolution.Y - 2;
            vectorFill2.X = gameResolution.X;
            vectorFill2.Y = gameResolution.Y - 2;
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
                    lemxscale = (lemming[i].Posx + 12) * xscale;
                    lemyscale = (lemming[i].Posy + 20) * yscale;
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

            vectorFill.X = 45;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (op1 ? Color.White : sombramenu), (op1 ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
            vectorFill.X = 24;
            vectorFill.Y = posymenu + 6;
            rectangleFill2.X = 0;
            rectangleFill2.Y = 40;
            rectangleFill2.Width = 32;
            rectangleFill2.Height = 40;
            spriteBatch.Draw(menos, vectorFill, rectangleFill2, (op1 ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

            vectorFill.X = 45 + 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (op2 ? Color.White : sombramenu), (op2 ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
            vectorFill.X = 24 + 55;
            vectorFill.Y = posymenu + 6;
            rectangleFill2.X = 0;
            rectangleFill2.Y = 40;
            rectangleFill2.Width = 32;
            rectangleFill2.Height = 40;
            spriteBatch.Draw(mas, vectorFill, rectangleFill2, (op2 ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

            vectorFill.X = 45 + 2 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.CLIMBER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.CLIMBER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = 10 + 2 * 55;
            vectorFill.Y = posymenu;
            rectangleFill2.X = frameescala * (_currentSelectedSkill == ECurrentSkill.CLIMBER ? climber_with : 0);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = climber_with;
            rectangleFill2.Height = climber_height;
            spriteBatch.Draw(escala, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.CLIMBER ? Color.White : sombramenu), 0, Vector2.Zero, climber_size, SpriteEffects.None, 0.1f);

            vectorFill.X = 45 + 3 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.FLOATER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.FLOATER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = 5 + 3 * 55;
            vectorFill.Y = posymenu;
            rectangleFill2.X = floater_with * (_currentSelectedSkill == ECurrentSkill.FLOATER ? frameparaguas : 4);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = floater_with;
            rectangleFill2.Height = floater_height;
            spriteBatch.Draw(paraguas, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.FLOATER ? Color.White : sombramenu), 0f, Vector2.Zero, 0.55f, SpriteEffects.None, 0.1f);

            vectorFill.X = 45 + 4 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.EXPLODER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.EXPLODER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = -5 + 4 * 55;
            vectorFill.Y = posymenu - 20;
            rectangleFill2.X = bomber_with * (_currentSelectedSkill == ECurrentSkill.EXPLODER ? frameexplota : 7);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = bomber_with;
            rectangleFill2.Height = bomber_height;
            spriteBatch.Draw(explota, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.EXPLODER ? Color.White : sombramenu), 0f, Vector2.Zero, bomber_size, SpriteEffects.None, 0.1f);

            vectorFill.X = 45 + 5 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.BLOCKER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.BLOCKER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = 10 + 5 * 55;
            vectorFill.Y = posymenu;
            rectangleFill2.X = blocker_with * (_currentSelectedSkill == ECurrentSkill.BLOCKER ? frameblocker : 0);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = blocker_with;
            rectangleFill2.Height = blocker_height;
            spriteBatch.Draw(blocker, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.BLOCKER ? Color.White : sombramenu), 0f, Vector2.Zero, blocker_size, SpriteEffects.None, 0.1f);

            vectorFill.X = 45 + 6 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.BUILDER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.BUILDER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = 6 + 6 * 55;
            vectorFill.Y = posymenu;
            rectangleFill2.X = builder_with * (_currentSelectedSkill == ECurrentSkill.BUILDER ? framepuente : 12);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = builder_with;
            rectangleFill2.Height = builder_height;
            spriteBatch.Draw(puente, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.BUILDER ? Color.White : sombramenu), 0f, Vector2.Zero, builder_size, SpriteEffects.None, 0.1f);

            vectorFill.X = 45 + 7 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.BASHER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.BASHER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = 10 + 7 * 55;
            vectorFill.Y = posymenu;
            rectangleFill2.X = basher_with * (_currentSelectedSkill == ECurrentSkill.BASHER ? framepared : 0);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = basher_with;
            rectangleFill2.Height = basher_height;
            spriteBatch.Draw(pared, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.BASHER ? Color.White : sombramenu), 0f, Vector2.Zero, basher_size, SpriteEffects.FlipHorizontally, 0.1f);

            vectorFill.X = 45 + 8 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.MINER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.MINER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = 10 + 8 * 55;
            vectorFill.Y = posymenu + 7;
            rectangleFill2.X = pico_with * (_currentSelectedSkill == ECurrentSkill.MINER ? framepico : 30);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = pico_with;
            rectangleFill2.Height = pico_height;
            spriteBatch.Draw(pico, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.MINER ? Color.White : sombramenu), 0f, Vector2.Zero, pico_size, SpriteEffects.None, 0.1f);

            vectorFill.X = 45 + 9 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (_currentSelectedSkill == ECurrentSkill.DIGGER ? Color.White : sombramenu), (_currentSelectedSkill == ECurrentSkill.DIGGER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
            vectorFill.X = 505;
            vectorFill.Y = posymenu;
            rectangleFill2.X = framecava * (_currentSelectedSkill == ECurrentSkill.DIGGER ? digger_with : 0);
            rectangleFill2.Y = 0;
            rectangleFill2.Width = digger_with;
            rectangleFill2.Height = digger_height;
            spriteBatch.Draw(digger, vectorFill, rectangleFill2, (_currentSelectedSkill == ECurrentSkill.DIGGER ? Color.White : sombramenu), 0f, Vector2.Zero, digger_size, SpriteEffects.None, 0.1f);

            vectorFill.X = 45 + 10 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (Paused ? Color.White : sombramenu), (Paused ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
            vectorFill.X = 24 + 10 * 55;
            vectorFill.Y = posymenu + 6;
            rectangleFill2.X = 0;
            rectangleFill2.Y = (Paused ? 40 : 0);
            rectangleFill2.Width = 32;
            rectangleFill2.Height = 40;
            spriteBatch.Draw(pausa, vectorFill, rectangleFill2, (Paused ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

            vectorFill.X = 45 + 11 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (allBlow ? Color.White : sombramenu), (allBlow ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
            vectorFill.X = 24 + 11 * 55;
            vectorFill.Y = posymenu + 6;
            rectangleFill2.X = 0;
            rectangleFill2.Y = (op12 ? 40 : 0);
            rectangleFill2.Width = 32;
            rectangleFill2.Height = 40;
            spriteBatch.Draw(bomba, vectorFill, rectangleFill2, (allBlow ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

            vectorFill.X = 45 + 12 * 55;
            vectorFill.Y = posymenu + 31;
            spriteBatch.Draw(circulo_led, vectorFill, rectangleFill, (op13 ? Color.White : sombramenu), (op13 ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
            vectorFill.X = 24 + 12 * 55;
            vectorFill.Y = posymenu + 6;
            rectangleFill2.X = 0;
            rectangleFill2.Y = (op13 ? 40 : 0);
            rectangleFill2.Width = 32;
            rectangleFill2.Height = 40;
            spriteBatch.Draw(avanzar, vectorFill, rectangleFill2, (op13 ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
        }

        private void Menu_logic()
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
            if (earth.Height > gameResolution.Y - 188)
                yscale = (float)84 / earth.Height;
            else
                yscale = (float)84 / (gameResolution.Y - 188);
            // float scale = Math.Min(xscale, yscale);  // scale from voth axis for real size
            mmscale = (xscroll) * xscale;
            mmscale2 = (xscroll + 1100) * xscale;
            mmscaley = (yscroll) * yscale;
            mmscaley2 = (yscroll + gameResolution.Y - 188) * yscale;
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
            if ((rectop1.Contains(x) && mouseActState.LeftButton == ButtonState.Pressed) || _decreaseOn)
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
            if ((rectop2.Contains(x) && mouseActState.LeftButton == ButtonState.Pressed) || _increaseOn)
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
                _currentSelectedSkill = ECurrentSkill.CLIMBER;
            }
            if (rectop4.Contains(x) && numeroparaguas > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.FLOATER;
            }
            if (rectop5.Contains(x) && numeroexplotan > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.EXPLODER;
            }
            if (rectop6.Contains(x) && numeroblockers > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BLOCKER;
            }
            if (rectop7.Contains(x) && numeropuentes > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BUILDER;
            }
            if (rectop8.Contains(x) && numeropared > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.BASHER;
            }
            if (rectop9.Contains(x) && numeropico > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.MINER;
            }
            if (rectop10.Contains(x) && numerocavan > 0 && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                _currentSelectedSkill = ECurrentSkill.DIGGER;
            }
            if (rectop11.Contains(x) && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed))
            {
                PlaySoundMenu();
                Paused = !Paused;
            }
            if (rectop12.Contains(x) && (mouseAntState.LeftButton == ButtonState.Released && mouseActState.LeftButton == ButtonState.Pressed) && !op12)
            {
                if (clickTimer1 > 0 && milisegundos - clickTimer1 < 300)
                {
                    PlaySoundMenu();
                    clickTimer1 = 0;
                    allBlow = true;
                }
                else
                    clickTimer1 = milisegundos;
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
                op13 = true;
                this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 180.0f);
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
                mmscaley2 = (float)(yscroll + gameResolution.Y - 188) * yscale;
                mxscale = (float)earth.Width / 336;
                myscale = (float)earth.Height / 84;
                mousexscale = ((mouseActState.Position.X - posm + 14) * mxscale) - (gameResolution.X / 2); // center x axis in minimap (xscroll)
                mouseyscale = ((mouseActState.Position.Y - posy) + 28) * myscale;
                xscroll = (int)mousexscale;
                if (xscroll + gameResolution.X > earth.Width)
                {
                    xscroll = earth.Width - gameResolution.X;
                }
                if (xscroll < 0)
                {
                    xscroll = 0;
                }
                yscroll = (int)mouseyscale - gameResolution.Y - 188;
                if (yscroll + gameResolution.Y - 188 > earth.Height)
                {
                    yscroll = earth.Height - gameResolution.Y - 188;
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
