using System;

using Lemmings.NET.Constants;
using Lemmings.NET.Helpers;
using Lemmings.NET.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Screens;

internal class InGameMenu
{
    private readonly InGame _inGame;

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
    private const int rry = 581;
    private const int rrx = 20;
    private const int rhy = 51;
    private const int rhx = 50;
    private Rectangle rectop1 = new(rrx, rry, rhx, rhy);
    private Rectangle rectop2 = new(rrx + 55, rry, rhx, rhy);
    private Rectangle rectop3 = new(rrx + 2 * 55, rry, rhx, rhy);
    private Rectangle rectop4 = new(rrx + 3 * 55, rry, rhx, rhy);
    private Rectangle rectop5 = new(rrx + 4 * 55, rry, rhx, rhy);
    private Rectangle rectop6 = new(rrx + 5 * 55, rry, rhx, rhy);
    private Rectangle rectop7 = new(rrx + 6 * 55, rry, rhx, rhy);
    private Rectangle rectop8 = new(rrx + 7 * 55, rry, rhx, rhy);
    private Rectangle rectop9 = new(rrx + 8 * 55, rry, rhx, rhy);
    private Rectangle rectop10 = new(rrx + 9 * 55, rry, rhx, rhy);
    private Rectangle rectop11 = new(rrx + 10 * 55, rry, rhx, rhy);
    private Rectangle rectop12 = new(rrx + 11 * 55, rry, rhx, rhy);
    private Rectangle rectop13 = new(rrx + 12 * 55, rry, rhx, rhy);
    private Rectangle rectminimenu = new(742, 572, 336, 84);
    private bool blink1on = false, blink2on = false, blink3on = false;
    public ECurrentSkill CurrentSelectedSkill { get; set; }
    private readonly int posm = 742;
    private readonly int posy = 572;
    private bool op1 = false, op2 = false, op13 = false;
    public int FrequencyNumber { get; set; } = 50;
    private int numerominfrecuencia = 50, zv = 0;
    private float mmscale2, mmscaley, mmscaley2, xscale, yscale, mmscale;
    private readonly int posymenu = 575;
    private double clickTimer1 = 0;
    private Color backmenu = Color.MediumSlateBlue;
    private Color sombramenu = Color.SlateGray;
    private bool _alreadyPlayed;
    private float cosa = 0f;
    public bool _decreaseOn, _increaseOn;

    internal InGameMenu(InGame inGame)
    {
        _inGame = inGame;
    }

    internal void Init()
    {
        numerominfrecuencia = _inGame.CurrentLevel.MinFrequencyComming;
        FrequencyNumber = _inGame.CurrentLevel.FrequencyComming;
    }

    internal void Update()
    {
        if (_inGame.R1 == 0)
        {
            _inGame.R1 = GlobalConst.Rnd.Next(30, 90);
        }
        if (_inGame.R2 == 0)
        {
            _inGame.R2 = GlobalConst.Rnd.Next(60, 120);
        }
        if (_inGame.R3 == 0)
        {
            _inGame.R3 = GlobalConst.Rnd.Next(40, 90);
        }
        if (_inGame.Earth.Width > GlobalConst.GameResolution.X)
            xscale = (float)336 / _inGame.Earth.Width;
        else
            xscale = (float)336 / GlobalConst.GameResolution.X;
        if (_inGame.Earth.Height > GlobalConst.GameResolution.Y - 188)
            yscale = (float)84 / _inGame.Earth.Height;
        else
            yscale = (float)84 / (GlobalConst.GameResolution.Y - 188);
        // float scale = Math.Min(xscale, yscale);  // scale from voth axis for real size
        mmscale = _inGame.ScrollX * xscale;
        mmscale2 = (_inGame.ScrollX + GlobalConst.GameResolution.X) * xscale;
        mmscaley = _inGame.ScrollY * yscale;
        mmscaley2 = (_inGame.ScrollY + GlobalConst.GameResolution.Y - 188) * yscale;
        mmscale = (int)mmscale;
        mmscale2 = (int)mmscale2;
        mmscaley = (int)mmscaley;
        mmscaley2 = (int)mmscaley2; //let decimals out for better mini-map size
        if (_inGame.Frame % _inGame.R1 == 0 && !blink1on)
        {
            framblink1 = 0;
            blink1on = true;
        }  // bbbbbbbbbbbbbbllllllllllllllblinking eyes menu 1-2-3
        if (blink1on && _inGame.Drawing)
        {
            framblink1++;
            if (framblink1 > 8)
            {
                blink1on = false;
                _inGame.R1 = 0;
            }
        }
        if (_inGame.Frame % _inGame.R2 == 0 && !blink2on)
        {
            framblink2 = 0;
            blink2on = true;
        }
        if (blink2on && _inGame.Drawing)
        {
            framblink2++;
            if (framblink2 > 8)
            {
                blink2on = false;
                _inGame.R2 = 0;
            }
        }
        if (_inGame.Frame % _inGame.R3 == 0 && !blink3on)
        {
            framblink3 = 0;
            blink3on = true;
        }
        if (blink3on && _inGame.Drawing)
        {
            framblink3++;
            if (framblink3 > 8)
            {
                blink3on = false;
                _inGame.R3 = 0;
            }
        }
        zv = (int)_inGame.TotalTime;
        if (_inGame.Fade)
        {
            zv = 0;
            _inGame.Contadortime = 0;
        }
        if (_inGame.Draw2)
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
            if (frameescala > SizeSprites.climber_frames - 1)
            {
                frameescala = 0;
            }
            frameparaguas++;
            if (frameparaguas > SizeSprites.floater_frames - 1)
            {
                frameparaguas = 0;
            }
            frameexplota++;
            if (frameexplota > SizeSprites.bomber_frames - 1)
            {
                frameexplota = 0;
            }
            frameblocker++;
            if (frameblocker > SizeSprites.blocker_frames - 1)
            {
                frameblocker = 0;
            }
            framepuente++;
            if (framepuente > SizeSprites.builder_frames - 1)
            {
                framepuente = 0;
            }
            framepared++;
            if (framepared > SizeSprites.basher_frames - 1)
            {
                framepared = 0;
            }
            framepico++;
            if (framepico > SizeSprites.pico_frames - 1)
            {
                framepico = 0;
            }
        }
        if (MyGame.Instance.Sfx.ChangeOp.State == SoundState.Playing && (op1 || op2))
        {
            MyGame.Instance.Sfx.ChangeOp.Stop();
            MyGame.Instance.Sfx.ChangeOp.Pitch = -1f + FrequencyNumber * 0.02f;
            MyGame.Instance.Sfx.ChangeOp.Volume = 0.25f + FrequencyNumber * 0.005f;
        }
        else
        {
            MyGame.Instance.Sfx.ChangeOp.Pitch = 0;
            MyGame.Instance.Sfx.ChangeOp.Volume = 1f;
        }
        cosa += 0.05f;
        if (cosa > 12.5)
        {
            cosa = 0;
        } // menu selection rotation speed
        // medium position for bucle medx medy
        if ((rectop1.Contains(Input.CurrentMouseState.Position) && Input.CurrentMouseState.LeftButton == ButtonState.Pressed) || _decreaseOn)
        {
            MyGame.Instance.Sfx.ChangeOp.Pitch = -1f + FrequencyNumber * 0.02f;
            MyGame.Instance.Sfx.ChangeOp.Volume = 0.25f + FrequencyNumber * 0.005f;
            if (MyGame.Instance.Sfx.ChangeOp.State == SoundState.Stopped)
                try
                {
                    MyGame.Instance.Sfx.ChangeOp.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
            if (FrequencyNumber == numerominfrecuencia)
            {
                MyGame.Instance.Sfx.ChangeOp.Stop();
            }
            op1 = true;
            if (_inGame.Draw2)
                FrequencyNumber -= 1; // on monogame 3.6 crash if frecuencia -1 only puto puto
            if (FrequencyNumber < numerominfrecuencia)
                FrequencyNumber = numerominfrecuencia;
        }
        else
        {
            op1 = false;
        }
        if ((rectop2.Contains(Input.CurrentMouseState.Position) && Input.CurrentMouseState.LeftButton == ButtonState.Pressed) || _increaseOn)
        {
            MyGame.Instance.Sfx.ChangeOp.Pitch = -1f + FrequencyNumber * 0.02f;
            MyGame.Instance.Sfx.ChangeOp.Volume = 0.25f + FrequencyNumber * 0.005f;
            if (MyGame.Instance.Sfx.ChangeOp.State == SoundState.Stopped)
                try
                {
                    MyGame.Instance.Sfx.ChangeOp.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
            if (FrequencyNumber == 99)
            {
                MyGame.Instance.Sfx.ChangeOp.Stop();
            }
            op2 = true;
            if (_inGame.Draw2)
                FrequencyNumber += 1; // on monogame 3.6 crash if frecuencia +1 only
            if (FrequencyNumber > 99)
                FrequencyNumber = 99;
        }
        else
        {
            op2 = false;
        }
        if (rectop3.Contains(Input.CurrentMouseState.Position) && _inGame.NbClimberRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.CLIMBER;
        }
        if (rectop4.Contains(Input.CurrentMouseState.Position) && _inGame.NbFloaterRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.FLOATER;
        }
        if (rectop5.Contains(Input.CurrentMouseState.Position) && _inGame.NbExploderRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.EXPLODER;
        }
        if (rectop6.Contains(Input.CurrentMouseState.Position) && _inGame.NbBlockerRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.BLOCKER;
        }
        if (rectop7.Contains(Input.CurrentMouseState.Position) && _inGame.NbBuilderRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.BUILDER;
        }
        if (rectop8.Contains(Input.CurrentMouseState.Position) && _inGame.NbBasherRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.BASHER;
        }
        if (rectop9.Contains(Input.CurrentMouseState.Position) && _inGame.NbMinerRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.MINER;
        }
        if (rectop10.Contains(Input.CurrentMouseState.Position) && _inGame.NbDiggerRemaining > 0 && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            CurrentSelectedSkill = ECurrentSkill.DIGGER;
        }
        if (rectop11.Contains(Input.CurrentMouseState.Position) && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            MyGame.Instance.Sfx.ChangeOp.Replay();
            GlobalConst.Paused = !GlobalConst.Paused;
        }
        if (rectop12.Contains(Input.CurrentMouseState.Position) && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed) && !_inGame.AllBlow)
        {
            if (clickTimer1 > 0 && _inGame.MillisecondsElapsed - clickTimer1 < 300)
            {
                MyGame.Instance.Sfx.ChangeOp.Replay();
                clickTimer1 = 0;
                _inGame.AllBlow = true;
            }
            else
                clickTimer1 = _inGame.MillisecondsElapsed;
        } // BOMBERS ALL
        if (Input.CurrentMouseState.LeftButton == ButtonState.Released)
            _alreadyPlayed = false;
        if (rectop13.Contains(Input.CurrentMouseState.Position) && Input.CurrentMouseState.LeftButton == ButtonState.Pressed)  //FAST FORWARD
        {
            if (MyGame.Instance.Sfx.ChangeOp.State == SoundState.Playing)
            {
                MyGame.Instance.Sfx.ChangeOp.Resume();
            }
            try
            {
                if (!_alreadyPlayed)
                {
                    MyGame.Instance.Sfx.ChangeOp.Play();
                    _alreadyPlayed = true;
                }
            }
            catch (InstancePlayLimitException) { /* Ignore errors */ }
            op13 = true;
            MyGame.Instance.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 180.0f);
        } // 120--240 van ok mas no lo se depende creo
        else
        {
            op13 = false;
            MyGame.Instance.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f);
        }
        if (rectminimenu.Contains(Input.CurrentMouseState.Position) && Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
        {
            mmscale = _inGame.ScrollX * xscale;
            mmscale2 = (_inGame.ScrollX + GlobalConst.GameResolution.X) * xscale;
            mmscaley = _inGame.ScrollY * yscale;
            mmscaley2 = (_inGame.ScrollY + GlobalConst.GameResolution.Y - 188) * yscale;
            float mxscale = (float)_inGame.Earth.Width / 336;
            float myscale = (float)_inGame.Earth.Height / 84;
            float mousexscale = ((Input.CurrentMouseState.Position.X - posm + 14) * mxscale) - (GlobalConst.GameResolution.X / 2); // center x axis in minimap (xscroll)
            float mouseyscale = ((Input.CurrentMouseState.Position.Y - posy) + 28) * myscale;
            _inGame.ScrollX = (int)mousexscale;
            if (_inGame.ScrollX + GlobalConst.GameResolution.X > _inGame.Earth.Width)
            {
                _inGame.ScrollX = _inGame.Earth.Width - GlobalConst.GameResolution.X;
            }
            if (_inGame.ScrollX < 0)
            {
                _inGame.ScrollX = 0;
            }
            _inGame.ScrollY = (int)mouseyscale - GlobalConst.GameResolution.Y - 188;
            if (_inGame.ScrollY + GlobalConst.GameResolution.Y - 188 > _inGame.Earth.Height)
            {
                _inGame.ScrollY = _inGame.Earth.Height - GlobalConst.GameResolution.Y - 188;
            }
            if (_inGame.ScrollY < 0)
            {
                _inGame.ScrollY = 0;
            }
            mmscale = (int)mmscale;
            mmscale2 = (int)mmscale2;
            mmscaley = (int)mmscaley;
            mmscaley2 = (int)mmscaley2; //let decimals out for better mini-map size
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle rectangleFill, rectangleFill2;
        Vector2 vectorFill, vectorFill2;
        Color colorFill = new();
        rectangleFill.X = 0;
        rectangleFill.Y = 513;
        rectangleFill.Width = GlobalConst.GameResolution.X;
        rectangleFill.Height = 184;
        rectangleFill2.X = 0;
        rectangleFill2.Y = 0;
        rectangleFill2.Width = GlobalConst.GameResolution.X;
        rectangleFill2.Height = 184;
        spriteBatch.Draw(MyGame.Instance.Gfx.Backmenu2, rectangleFill, rectangleFill2, backmenu, 0f, Vector2.Zero, SpriteEffects.None, 0.251f);
        rectangleFill.X = 0;
        rectangleFill.Y = 0;
        rectangleFill.Width = MyGame.Instance.Gfx.Backlogo.Width;
        rectangleFill.Height = MyGame.Instance.Gfx.Backlogo.Height;
        vectorFill.X = 130;
        vectorFill.Y = 622;
        colorFill.R = 100;
        colorFill.G = 100;
        colorFill.B = 100;
        colorFill.A = 200;
        spriteBatch.Draw(MyGame.Instance.Gfx.Backlogo, vectorFill, rectangleFill, colorFill, 0f, Vector2.Zero, 0.78f, SpriteEffects.None, 0.215f);  // logo del menu
        rectangleFill.X = 0;
        rectangleFill.Y = 0;
        rectangleFill.Width = MyGame.Instance.Gfx.Backlogo.Width;
        rectangleFill.Height = MyGame.Instance.Gfx.Backlogo.Height;
        vectorFill.X = 140;
        vectorFill.Y = 625;
        spriteBatch.Draw(MyGame.Instance.Gfx.Backlogo, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.115f);  // logo del menu
        rectangleFill.X = 0;
        rectangleFill.Y = framblink1 * 12;
        rectangleFill.Width = MyGame.Instance.Sprites.EyeBlink1.Width;
        rectangleFill.Height = 12;
        vectorFill.X = 158;
        vectorFill.Y = 654;
        spriteBatch.Draw(MyGame.Instance.Sprites.EyeBlink1, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.104f);
        rectangleFill.X = 0;
        rectangleFill.Y = framblink2 * 12;
        rectangleFill.Width = MyGame.Instance.Sprites.EyeBlink2.Width;
        rectangleFill.Height = 12;
        vectorFill.X = 329;
        vectorFill.Y = 654;
        spriteBatch.Draw(MyGame.Instance.Sprites.EyeBlink2, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.104f);
        rectangleFill.X = 0;
        rectangleFill.Y = framblink3 * 12;
        rectangleFill.Width = MyGame.Instance.Sprites.EyeBlink3.Width;
        rectangleFill.Height = 12;
        vectorFill.X = 506;
        vectorFill.Y = 648;
        spriteBatch.Draw(MyGame.Instance.Sprites.EyeBlink3, vectorFill, rectangleFill, Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.104f);
        rectangleFill.X = 735;
        rectangleFill.Y = 564;
        rectangleFill.Width = 350;
        rectangleFill.Height = 100;
        rectangleFill2.X = 0;
        rectangleFill2.Y = 0;
        rectangleFill2.Width = MyGame.Instance.Sprites.Cuadrado_menu.Width;
        rectangleFill2.Height = MyGame.Instance.Sprites.Cuadrado_menu.Height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Cuadrado_menu, rectangleFill, rectangleFill2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
        vectorFill.X = 0;
        vectorFill.Y = GlobalConst.GameResolution.Y - 188;
        vectorFill2.X = GlobalConst.GameResolution.X;
        vectorFill2.Y = GlobalConst.GameResolution.Y - 188;
        MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.White, 2, 0.1f);
        vectorFill.X = 0;
        vectorFill.Y = GlobalConst.GameResolution.Y - 2;
        vectorFill2.X = GlobalConst.GameResolution.X;
        vectorFill2.Y = GlobalConst.GameResolution.Y - 2;
        MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.White, 2, 0.1f);
        vectorFill.X = 741;
        vectorFill.Y = 572;
        vectorFill2.X = 741 + 338;
        vectorFill2.Y = 572;
        MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Black, 84, 0.1f);
        vectorFill.X = posm;
        vectorFill.Y = 572;
        colorFill.R = 70;
        colorFill.G = 70;
        colorFill.B = 70;
        colorFill.A = 255;
        vectorFill2.X = xscale;
        vectorFill2.Y = yscale;
        spriteBatch.Draw(_inGame.Earth, vectorFill, null, colorFill, 0f, Vector2.Zero, vectorFill2, /*scale is the real proportion*/ SpriteEffects.None, 0.090f);
        //draw yellow lines on the mini menu
        vectorFill.X = posm + mmscale;
        vectorFill.Y = posy + mmscaley;
        vectorFill2.X = posm + mmscale;
        vectorFill2.Y = posy + mmscaley2 + 1;
        MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
        vectorFill.X = posm + mmscale2;
        vectorFill.Y = posy + mmscaley;
        vectorFill2.X = posm + mmscale2;
        vectorFill2.Y = posy + mmscaley2 + 1;
        MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
        vectorFill.X = posm + mmscale;
        vectorFill.Y = posy + mmscaley;
        vectorFill2.X = posm + mmscale2;
        vectorFill2.Y = posy + mmscaley;
        MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
        vectorFill.X = posm + mmscale;
        vectorFill.Y = posy + mmscaley2;
        vectorFill2.X = posm + mmscale2;
        vectorFill2.Y = posy + mmscaley2;
        MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Yellow, 1, 0.007f);
        foreach (OneLemming lemming in _inGame.AllLemmings)
        {
            if (!lemming.Dead)
            {
                float lemxscale = (lemming.PosX + 12) * xscale;
                float lemyscale = (lemming.PosY + 20) * yscale;
                vectorFill.X = posm + lemxscale;
                vectorFill.Y = 572 + lemyscale;
                vectorFill2.X = posm + lemxscale + 2;
                vectorFill2.Y = 572 + lemyscale;
                MyGame.Instance.Gfx.DrawLine(spriteBatch, vectorFill, vectorFill2, Color.Magenta, 2, 0.001f);
            }
        }
        vectorFill.X = 80 - 55;
        vectorFill.Y = 560;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", numerominfrecuencia), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", FrequencyNumber), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbClimberRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 2 * 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbFloaterRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 3 * 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbExploderRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 4 * 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbBlockerRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 5 * 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbBuilderRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 6 * 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbBasherRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 7 * 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbMinerRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 80 + 8 * 55;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.NbDiggerRemaining), vectorFill, Color.LimeGreen, 0.5f, 0.1f, spriteBatch);
        vectorFill.X = 890;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem("Time", vectorFill, Color.Yellow, 1f, 0.1f, spriteBatch);
        _inGame.ZvTime = (_inGame.CurrentLevel.TotalTime * 60) - (int)_inGame.TotalTime;
        if (_inGame.ZvTime < 0)
            _inGame.ZvTime = -1; //see that -1 is necesary for end by time -- it test (zvtime < 0)
        vectorFill.X = 982;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", _inGame.ZvTime / 60) + ":" + string.Format("{0,2:D2}", _inGame.ZvTime % 60), vectorFill, Color.Lime, 1f, 0.1f, spriteBatch);
        vectorFill.X = 1016;
        vectorFill.Y = 547;
        MyGame.Instance.Fonts.TextLem(string.Format("{0,2:D2}", zv / 60) + ":" + string.Format("{0,2:D2}", zv % 60), vectorFill, Color.Green, 0.5f, 0.1f, spriteBatch);
        // Kind of lemming SKILLS SKILL SKILLS
        vectorFill.X = 0;
        vectorFill.Y = 518;
        MyGame.Instance.Fonts.TextLem(_inGame.LemSkill, vectorFill, Color.GreenYellow, 1f, 0.1f, spriteBatch);
        for (int i = 0; i <= 12; i++)
        {
            vectorFill.X = 12 + i * 55;
            vectorFill.Y = posymenu;
            rectangleFill.X = 0;
            rectangleFill.Y = 0;
            rectangleFill.Width = MyGame.Instance.Sprites.Circulo_led.Width;
            rectangleFill.Height = MyGame.Instance.Sprites.Circulo_led.Height;
            spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, sombramenu, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
        } //todas casillas desactivadas
        rectangleFill.X = 0;
        rectangleFill.Y = 0;
        rectangleFill.Width = MyGame.Instance.Sprites.Circulo_led.Width;
        rectangleFill.Height = MyGame.Instance.Sprites.Circulo_led.Height;
        vectorFill2.X = MyGame.Instance.Sprites.Circulo_led.Width / 2.0f;
        vectorFill2.Y = MyGame.Instance.Sprites.Circulo_led.Height / 2.0f;

        vectorFill.X = 45;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (op1 ? Color.White : sombramenu), (op1 ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
        vectorFill.X = 24;
        vectorFill.Y = posymenu + 6;
        rectangleFill2.X = 0;
        rectangleFill2.Y = 40;
        rectangleFill2.Width = 32;
        rectangleFill2.Height = 40;
        spriteBatch.Draw(MyGame.Instance.Sprites.Menos, vectorFill, rectangleFill2, (op1 ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

        vectorFill.X = 45 + 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (op2 ? Color.White : sombramenu), (op2 ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
        vectorFill.X = 24 + 55;
        vectorFill.Y = posymenu + 6;
        rectangleFill2.X = 0;
        rectangleFill2.Y = 40;
        rectangleFill2.Width = 32;
        rectangleFill2.Height = 40;
        spriteBatch.Draw(MyGame.Instance.Sprites.Mas, vectorFill, rectangleFill2, (op2 ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

        vectorFill.X = 45 + 2 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.CLIMBER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.CLIMBER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = 10 + 2 * 55;
        vectorFill.Y = posymenu;
        rectangleFill2.X = frameescala * (CurrentSelectedSkill == ECurrentSkill.CLIMBER ? SizeSprites.climber_with : 0);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.climber_with;
        rectangleFill2.Height = SizeSprites.climber_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Climber, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.CLIMBER ? Color.White : sombramenu), 0, Vector2.Zero, SizeSprites.climber_size, SpriteEffects.None, 0.1f);

        vectorFill.X = 45 + 3 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.FLOATER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.FLOATER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = 5 + 3 * 55;
        vectorFill.Y = posymenu;
        rectangleFill2.X = SizeSprites.floater_with * (CurrentSelectedSkill == ECurrentSkill.FLOATER ? frameparaguas : 4);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.floater_with;
        rectangleFill2.Height = SizeSprites.floater_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Paraguas, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.FLOATER ? Color.White : sombramenu), 0f, Vector2.Zero, 0.55f, SpriteEffects.None, 0.1f);

        vectorFill.X = 45 + 4 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.EXPLODER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.EXPLODER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = -5 + 4 * 55;
        vectorFill.Y = posymenu - 20;
        rectangleFill2.X = SizeSprites.bomber_with * (CurrentSelectedSkill == ECurrentSkill.EXPLODER ? frameexplota : 7);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.bomber_with;
        rectangleFill2.Height = SizeSprites.bomber_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Exploder, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.EXPLODER ? Color.White : sombramenu), 0f, Vector2.Zero, SizeSprites.bomber_size, SpriteEffects.None, 0.1f);

        vectorFill.X = 45 + 5 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.BLOCKER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.BLOCKER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = 10 + 5 * 55;
        vectorFill.Y = posymenu;
        rectangleFill2.X = SizeSprites.blocker_with * (CurrentSelectedSkill == ECurrentSkill.BLOCKER ? frameblocker : 0);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.blocker_with;
        rectangleFill2.Height = SizeSprites.blocker_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Blocker, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.BLOCKER ? Color.White : sombramenu), 0f, Vector2.Zero, SizeSprites.blocker_size, SpriteEffects.None, 0.1f);

        vectorFill.X = 45 + 6 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.BUILDER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.BUILDER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = 6 + 6 * 55;
        vectorFill.Y = posymenu;
        rectangleFill2.X = SizeSprites.builder_with * (CurrentSelectedSkill == ECurrentSkill.BUILDER ? framepuente : 12);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.builder_with;
        rectangleFill2.Height = SizeSprites.builder_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Puente, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.BUILDER ? Color.White : sombramenu), 0f, Vector2.Zero, SizeSprites.builder_size, SpriteEffects.None, 0.1f);

        vectorFill.X = 45 + 7 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.BASHER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.BASHER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = 10 + 7 * 55;
        vectorFill.Y = posymenu;
        rectangleFill2.X = SizeSprites.basher_with * (CurrentSelectedSkill == ECurrentSkill.BASHER ? framepared : 0);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.basher_with;
        rectangleFill2.Height = SizeSprites.basher_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Pared, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.BASHER ? Color.White : sombramenu), 0f, Vector2.Zero, SizeSprites.basher_size, SpriteEffects.FlipHorizontally, 0.1f);

        vectorFill.X = 45 + 8 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.MINER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.MINER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = 10 + 8 * 55;
        vectorFill.Y = posymenu + 7;
        rectangleFill2.X = SizeSprites.pico_with * (CurrentSelectedSkill == ECurrentSkill.MINER ? framepico : 30);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.pico_with;
        rectangleFill2.Height = SizeSprites.pico_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Pico, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.MINER ? Color.White : sombramenu), 0f, Vector2.Zero, SizeSprites.pico_size, SpriteEffects.None, 0.1f);

        vectorFill.X = 45 + 9 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (CurrentSelectedSkill == ECurrentSkill.DIGGER ? Color.White : sombramenu), (CurrentSelectedSkill == ECurrentSkill.DIGGER ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.11f);
        vectorFill.X = 505;
        vectorFill.Y = posymenu;
        rectangleFill2.X = framecava * (CurrentSelectedSkill == ECurrentSkill.DIGGER ? SizeSprites.digger_with : 0);
        rectangleFill2.Y = 0;
        rectangleFill2.Width = SizeSprites.digger_with;
        rectangleFill2.Height = SizeSprites.digger_height;
        spriteBatch.Draw(MyGame.Instance.Sprites.Digger, vectorFill, rectangleFill2, (CurrentSelectedSkill == ECurrentSkill.DIGGER ? Color.White : sombramenu), 0f, Vector2.Zero, SizeSprites.digger_size, SpriteEffects.None, 0.1f);

        vectorFill.X = 45 + 10 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (GlobalConst.Paused ? Color.White : sombramenu), (GlobalConst.Paused ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
        vectorFill.X = 24 + 10 * 55;
        vectorFill.Y = posymenu + 6;
        rectangleFill2.X = 0;
        rectangleFill2.Y = (GlobalConst.Paused ? 40 : 0);
        rectangleFill2.Width = 32;
        rectangleFill2.Height = 40;
        spriteBatch.Draw(MyGame.Instance.Sprites.Pausa, vectorFill, rectangleFill2, (GlobalConst.Paused ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

        vectorFill.X = 45 + 11 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (_inGame.AllBlow ? Color.White : sombramenu), (_inGame.AllBlow ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
        vectorFill.X = 24 + 11 * 55;
        vectorFill.Y = posymenu + 6;
        rectangleFill2.X = 0;
        rectangleFill2.Y = (_inGame.AllBlow ? 40 : 0);
        rectangleFill2.Width = 32;
        rectangleFill2.Height = 40;
        spriteBatch.Draw(MyGame.Instance.Sprites.Bomba, vectorFill, rectangleFill2, (_inGame.AllBlow ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);

        vectorFill.X = 45 + 12 * 55;
        vectorFill.Y = posymenu + 31;
        spriteBatch.Draw(MyGame.Instance.Sprites.Circulo_led, vectorFill, rectangleFill, (op13 ? Color.White : sombramenu), (op13 ? cosa : 0), vectorFill2, 1f, SpriteEffects.None, 0.1f);
        vectorFill.X = 24 + 12 * 55;
        vectorFill.Y = posymenu + 6;
        rectangleFill2.X = 0;
        rectangleFill2.Y = (op13 ? 40 : 0);
        rectangleFill2.Width = 32;
        rectangleFill2.Height = 40;
        spriteBatch.Draw(MyGame.Instance.Sprites.Avanzar, vectorFill, rectangleFill2, (op13 ? Color.White : sombramenu), 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0.11f);
    }
}
