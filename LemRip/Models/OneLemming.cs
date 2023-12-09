using System;
using System.Linq;

using Lemmings.NET.Constants;
using Lemmings.NET.Helpers;
using Lemmings.NET.Structs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.NET.Models;

internal class OneLemming
{
    public int NumLemming { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int Numstairs { get; set; }
    public int Numframes { get; set; }
    public int Actualframe { get; set; }
    public bool Active { get; set; }
    public bool Right { get; set; }
    public bool Left
    {
        get { return !Right; }
    }
    public int PixelsDrop { get; set; }
    public bool Walker { get; set; }
    public bool Blocker { get; set; }
    public bool Fall { get; set; }
    public bool Builder { get; set; }
    public bool Basher { get; set; }
    public bool Miner { get; set; }
    public bool Bridge { get; set; }
    public bool Digger { get; set; }
    public bool Onmouse { get; set; }
    public bool Exit { get; set; }
    public bool Exploser { get; set; }
    public bool Explode { get; set; }
    public bool Dead { get; set; }
    public bool Breakfloor { get; set; }
    public bool Climber { get; set; }
    public bool Climbing { get; set; }
    public bool Umbrella { get; set; }
    public bool Falling { get; set; }
    public bool Framescut { get; set; }
    public bool Burned { get; set; }
    public bool Drown { get; set; }
    public double Time { get; set; }

    public void Moving()
    {
        Rectangle bloqueo, rectangleFill;
        Point poslem, x;
        int sx = 0;

        if (Dead)
            return;
        // LOGIC BLOCKER BLOCKER BLOQUEO LOGIC bbbbbbbbbbbbbbbbbbbbllllllllloooooooccccccccccckkkkkkkkkkkeeeeeeeeeeeeedddddddddddddddddd
        int medx = 14;
        int medy = 14;
        foreach (OneLemming lemming in MyGame.Instance.ScreenInGame.AllLemmings.Where(l => l.Blocker && l != this))
        {
            bloqueo.X = lemming.PosX;
            bloqueo.Y = lemming.PosY;
            bloqueo.Width = 28;
            bloqueo.Height = 28;
            if (Miner)
            {
                bloqueo.X = lemming.PosX + 10;
                bloqueo.Y = lemming.PosY;
                bloqueo.Width = 9;
                bloqueo.Height = 28;
            }
            poslem.X = PosX + medx;
            poslem.Y = PosY + medy;
            if (bloqueo.Contains(poslem))
            {
                if (Right)
                {
                    if (PosX < lemming.PosX)
                    {
                        Right = false;
                        break;
                    }
                }
                else
                {
                    if (PosX > lemming.PosX - 1)
                    {
                        Right = true;
                        break;
                    }
                }
                break;
            }
        }
        Onmouse = false; //LEMMING SKILL STRING MOUSE ON
        if ((Input.CurrentMouseState.X >= PosX - MyGame.Instance.ScreenInGame.ScrollX && Input.CurrentMouseState.X <= PosX - MyGame.Instance.ScreenInGame.ScrollX + 28
            && Input.CurrentMouseState.Y >= PosY - MyGame.Instance.ScreenInGame.ScrollY && Input.CurrentMouseState.Y <= PosY + 28 - MyGame.Instance.ScreenInGame.ScrollY) && !MyGame.Instance.ScreenInGame.MouseOnLem)
        {
            if (Walker)
                MyGame.Instance.ScreenInGame.LemSkill = "Walker";
            else if (Builder)
                MyGame.Instance.ScreenInGame.LemSkill = "Builder";
            else if (Basher)
                MyGame.Instance.ScreenInGame.LemSkill = "Basher";
            else if (Blocker)
                MyGame.Instance.ScreenInGame.LemSkill = "Blocker";
            else if (Miner)
                MyGame.Instance.ScreenInGame.LemSkill = "Miner";
            else if (Digger)
                MyGame.Instance.ScreenInGame.LemSkill = "Digger";
            if (Climber)
                MyGame.Instance.ScreenInGame.LemSkill += ",C";
            if (Umbrella)
                MyGame.Instance.ScreenInGame.LemSkill += ",F";
            if (Climbing)
                MyGame.Instance.ScreenInGame.LemSkill = "Climber";
            else if (Climbing && Umbrella)
                MyGame.Instance.ScreenInGame.LemSkill = "Climber,F";
            else if ((Fall || Falling) && !Umbrella)
                MyGame.Instance.ScreenInGame.LemSkill = "Faller";
            else if ((Fall || Falling) && Umbrella)
                MyGame.Instance.ScreenInGame.LemSkill = "Floater";
            MyGame.Instance.ScreenInGame.MouseOnLem = true;
            Onmouse = true;
        } //  inside the mouse rectangle lemming ON
        if (MyGame.Instance.ScreenInGame.TrapsON &&
            !GlobalConst.Paused &&
            MyGame.Instance.ScreenInGame.Trap != null) //Traps logic and sounds
        {
            foreach (Vartraps trap in MyGame.Instance.ScreenInGame.Trap)
            {
                x.X = PosX + 14;
                x.Y = PosY + 25;
                if (trap.areaTrap.Contains(x) && !trap.isOn && trap.type == 666)
                {
                    trap.SetIsOn(true);
                    Active = false;
                    Walker = false;
                    Dead = true;
                    MyGame.Instance.ScreenInGame.Numlemnow--;
                    Explode = false;
                    Exploser = false;
                    switch (trap.sprite.Name)
                    {
                        case "traps/dead_marble":
                        case "traps/dead_marble2_fix":
                            MyGame.Instance.Sfx.StrapTenton.Replay();
                            break;
                        case "traps/dead_trampa":
                            MyGame.Instance.Sfx.StrapMan.Replay();
                            break;
                        case "traps/dead_soga":
                            MyGame.Instance.Sfx.StrapChain.Replay();
                            break;
                        case "traps/dead_bombona":
                            MyGame.Instance.Sfx.StrapChupar.Replay();
                            break;
                        case "traps/dead_10":
                            MyGame.Instance.Sfx.StrapTenTonnes.Replay();
                            break;
                        default:
                            MyGame.Instance.Sfx.Die.Replay();
                            break;
                    }
                    break;
                }
                rectangleFill.X = PosX + 14;
                rectangleFill.Y = PosY;
                rectangleFill.Width = 1;
                rectangleFill.Height = 28;
                if (trap.areaTrap.Intersects(rectangleFill) && !Burned && !Drown && trap.type != 666)
                {
                    switch (trap.sprite.Name)
                    {
                        case "traps/dead_spin":
                        case "traps/fuego1":
                        case "traps/fuego2":
                        case "traps/fuego3":
                        case "traps/fuego4":
                            MyGame.Instance.Sfx.Fire.Replay();
                            Burned = true;
                            Drown = false;
                            Explode = false;
                            Exploser = false;
                            Numframes = 14;
                            Actualframe = 0;
                            Active = false;
                            Walker = false;
                            Falling = false;
                            Fall = false;
                            break;
                        case "traps/acid":
                        case "traps/acid2":
                        case "traps/ice_water":
                        case "traps/ice_water2":
                        case "traps/water_blue":
                        case "traps/water_bubbles":
                            MyGame.Instance.Sfx.Glup.Replay();
                            Drown = true;
                            Burned = false;
                            Explode = false;
                            Exploser = false;
                            Falling = false;
                            Fall = false;
                            Numframes = SizeSprites.water_frames;
                            Actualframe = 0;
                            Active = false;
                            Walker = false;
                            break;
                        default:
                            MyGame.Instance.Sfx.Die.Replay();
                            Explode = false;
                            Exploser = false;
                            Active = false;
                            Walker = false;
                            Dead = true;
                            MyGame.Instance.ScreenInGame.Numlemnow--;
                            break;
                    }
                }
            }
        }
        // assign skills to lemmings //////////////////////////////////////////////
        if (MyGame.Instance.ScreenInGame.MouseOnLem && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.DIGGER && !Digger && Onmouse //DIGGER
                && (Walker || Builder || Basher || Miner))
            {
                MyGame.Instance.ScreenInGame.NbDiggerRemaining--;
                if (MyGame.Instance.ScreenInGame.NbDiggerRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbDiggerRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Digger = true;
                    Fall = false;
                    Builder = false;
                    Walker = false;
                    Basher = false;
                    Miner = false;
                    Actualframe = 0;
                    Numframes = SizeSprites.digger_frames;
                    return;
                }
            }
            else if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.CLIMBER && Onmouse && !Climber) //CLIMBER
            {
                MyGame.Instance.ScreenInGame.NbClimberRemaining--;
                if (MyGame.Instance.ScreenInGame.NbClimberRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbClimberRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Climber = true;
                    return;
                }
            }
            else if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.FLOATER && Onmouse && !Umbrella && !Breakfloor) //FLOATER
            {
                MyGame.Instance.ScreenInGame.NbFloaterRemaining--;
                if (MyGame.Instance.ScreenInGame.NbFloaterRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbFloaterRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Umbrella = true;
                    return;
                }
            }
            else if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.EXPLODER && Onmouse && !Exploser) //BOMBER
            {
                MyGame.Instance.ScreenInGame.NbExploderRemaining--;
                if (MyGame.Instance.ScreenInGame.NbExploderRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbExploderRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Exploser = true;
                    return;
                }
            }
            else if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.BLOCKER && Onmouse && !Blocker //BLOCKER
                && (Walker || Digger || Builder || Basher || Miner))
            {
                MyGame.Instance.ScreenInGame.NbBlockerRemaining--;
                if (MyGame.Instance.ScreenInGame.NbBlockerRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbBlockerRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Blocker = true;
                    Builder = false;
                    Basher = false;
                    Miner = false;
                    Walker = false;
                    Digger = false;
                    Actualframe = 0;
                    Numframes = SizeSprites.blocker_frames;
                    return;
                }
            }
            else if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.BUILDER && Onmouse && !Builder //BUILDER
                && (Walker || Digger || Basher || Miner || Bridge))
            {
                MyGame.Instance.ScreenInGame.NbBuilderRemaining--;
                if (MyGame.Instance.ScreenInGame.NbBuilderRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbBuilderRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Bridge = false;
                    Builder = true;
                    Actualframe = 0;
                    Walker = false;
                    Digger = false;
                    Basher = false;
                    Miner = false;
                    Numstairs = 0;
                    Numframes = SizeSprites.builder_frames;
                    return;
                }
            }
            else if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.BASHER && Onmouse && !Basher //BASHER
                && (Walker || Digger || Builder || Miner))
            {
                MyGame.Instance.ScreenInGame.NbBasherRemaining--;
                if (MyGame.Instance.ScreenInGame.NbBasherRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbBasherRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Basher = true;
                    Actualframe = 0;
                    Walker = false;
                    Digger = false;
                    Builder = false;
                    Miner = false;
                    Numframes = SizeSprites.basher_frames;
                    return;
                }
            }
            else if (MyGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.MINER && Onmouse && !Miner //MINER
                && (Walker || Digger || Basher || Builder))
            {
                MyGame.Instance.ScreenInGame.NbMinerRemaining--;
                if (MyGame.Instance.ScreenInGame.NbMinerRemaining < 0)
                {
                    MyGame.Instance.ScreenInGame.NbMinerRemaining = 0;
                    MyGame.Instance.Sfx.Ting.Replay();
                }
                else
                {
                    MyGame.Instance.Sfx.MousePre.Replay();
                    Miner = true;
                    Actualframe = 0;
                    Walker = false;
                    Digger = false;
                    Basher = false;
                    Builder = false;
                    Numframes = SizeSprites.miner_frames;
                    return;
                }
            }

        }
        if (GlobalConst.Paused)
            return;
        if (MyGame.Instance.ScreenInGame.Draw_builder && Builder)
        {
            Actualframe++;
            if (Actualframe > Numframes - 1 && !Explode)
            {
                Actualframe = 0;
            }
        }
        if (MyGame.Instance.ScreenInGame.Draw_walker && !Builder && !Basher && !Miner
            && !Burned && !Drown)
        {
            Actualframe++;
            if (Actualframe > Numframes - 1 && !Explode)
            {
                Actualframe = 0;
            }
            //be carefull with bomber frames actualization
        }
        if (MyGame.Instance.ScreenInGame.Draw2 && (Basher || Miner
            || Burned || Drown)) // see careful frames
        {
            Actualframe++;
            if ((Burned || Drown) &&
                (Actualframe > Numframes - 1))
            {
                Burned = false;
                Drown = false;
                Dead = true;
                Explode = false;
                Exploser = false;
                MyGame.Instance.ScreenInGame.Numlemnow--;
            }
            if (Actualframe > Numframes - 1 && !Explode)
            {
                Actualframe = 0;
            }
        }
        if (Exit)
        {
            if (Actualframe == Numframes - 1)
            {
                Dead = true;
                Explode = false;
                Exploser = false;
                MyGame.Instance.ScreenInGame.Numlemnow--;
            }
            return;
        }
        int above = 0;
        int below = 0;
        int pixx = PosX + medx;
        for (int x55 = 0; x55 <= 8; x55++)
        {
            int pos_real = PosY + x55 + medy + medy;  ///////////// pixel por debajo -> beneath.............
            if (pos_real == MyGame.Instance.ScreenInGame.Earth.Height)
            {
                below = 9;
                break;
            }
            if (pos_real < 0)
                pos_real = 0;
            if (pos_real > MyGame.Instance.ScreenInGame.Earth.Height)
            {
                Dead = true;
                below = 9;
                Active = false;
                MyGame.Instance.ScreenInGame.Numlemnow--;
                Explode = false;
                Exploser = false;
                MyGame.Instance.Sfx.Die.Replay();
                break;
            }
            if (MyGame.Instance.ScreenInGame.C25[(pos_real * MyGame.Instance.ScreenInGame.Earth.Width) + pixx].R == 0 && MyGame.Instance.ScreenInGame.C25[(pos_real * MyGame.Instance.ScreenInGame.Earth.Width) + pixx].G == 0 && MyGame.Instance.ScreenInGame.C25[(pos_real * MyGame.Instance.ScreenInGame.Earth.Width) + pixx].B == 0)
            {
                below++;
            }
            else
            {
                break;
            }
        }
        // very important to check digger and miner before change to falling
        if (PixelsDrop > GlobalConst.useumbrella && !Falling && Umbrella
            && (!Digger && !Miner && !Builder) && Active)
        {
            PixelsDrop = 11;
            Falling = true;
            Fall = false;
            Actualframe = 0;
            Numframes = SizeSprites.floater_frames;
        }
        if ((below > 8 && !Fall && (!Digger || !Miner)) && !Falling
            && !Explode && Active)
        {
            Fall = true;
            PixelsDrop = 0;
            Climbing = false;
            Walker = false;
            Actualframe = 0;
            Numframes = SizeSprites.faller_frames;
            Basher = false;
            Builder = false;
            Bridge = false;
            Miner = false;
            return; // lemming fall when there's no floor on feet and fall down
        }
        if ((below == 0) && (Fall || Falling) && (!Digger && !Miner)) //OJO LOCO A VECES AL CAVAR Y SIGUE WALKER
        {
            if (PixelsDrop <= GlobalConst.maxnumberfalling)
            {
                PixelsDrop = 0;
                Framescut = false;
                Falling = false;
                Walker = true;
                Fall = false;
                Actualframe = 0;
                Numframes = SizeSprites.walker_frames;  //8 walker;4 fall;16 digger;breakfloor 16;escala ...
            }
            else
            {
                MyGame.Instance.Sfx.Splat.Replay();
                Fall = false;
                Walker = false;
                Falling = false;
                Explode = false;
                Exploser = false;
                Active = false;
                Breakfloor = true;
                Umbrella = false;
                Numframes = SizeSprites.floor_frames;
                Actualframe = 0;
                return;
            }
        }
        if ((below == 0) && Walker && (!Digger && !Miner))
        {
            PixelsDrop = 0;
        }
        for (int x55 = 0; x55 <= 20; x55++)
        {
            int pos_real = PosY + medy + medy - x55;
            if (pos_real == MyGame.Instance.ScreenInGame.Earth.Height)    // rompe los calculos si sale de la pantalla o se cuelga AARRIBBBAAAA
            {
                Active = false;
                break;
            }
            if (pos_real < MyGame.Instance.ScreenInGame.Earth.Height && pos_real > 0)
            {
                if (MyGame.Instance.ScreenInGame.C25[(pos_real * MyGame.Instance.ScreenInGame.Earth.Width) + pixx].R > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real * MyGame.Instance.ScreenInGame.Earth.Width) + pixx].G > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real * MyGame.Instance.ScreenInGame.Earth.Width) + pixx].B > 0)
                {
                    above++;
                }
                else
                {
                    break;
                }
            }
        }
        if (Blocker && below > 2)
        {
            Blocker = false;
            Fall = true;
            PixelsDrop = 0;
            Actualframe = 0;
            Numframes = SizeSprites.faller_frames;
            return;
        }
        if (Miner && MyGame.Instance.ScreenInGame.Draw2 && Actualframe == 42)  // miner logic pico logic
        {
            if (MyGame.Instance.ScreenInGame.ArrowsON) // miner arrows logic areaTrap Intersects
            {
                bool nominer = false;
                Rectangle arrowLem;
                arrowLem.X = PosX;
                arrowLem.Y = PosY;
                arrowLem.Width = 28;
                arrowLem.Height = 28;
                if (MyGame.Instance.ScreenInGame.Arrow != null)
                {
                    foreach (Vararrows arrow in MyGame.Instance.ScreenInGame.Arrow)
                    {
                        if (arrow.area.Intersects(arrowLem) && Right && !arrow.right)
                        {
                            nominer = true;
                            continue;
                        }
                        if (arrow.area.Intersects(arrowLem) && Left && arrow.right)
                        {
                            nominer = true;
                        }
                    }
                }
                if (nominer)
                {
                    MyGame.Instance.Sfx.Ting.Replay();
                    Miner = false;
                    Walker = true;
                    Actualframe = 0;
                    Numframes = SizeSprites.walker_frames;
                    return;
                }
            }
            if (Right)
            {
                int width2 = 20;
                int top2 = 20;
                int px = PosX + 12;
                int py = PosY + 14;
                if (py < 0) // top of the level
                {
                    py = 0;
                }
                if (px < 0) // left of the level
                {
                    px = 0;
                }
                if (px + width2 >= MyGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = MyGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= MyGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = MyGame.Instance.ScreenInGame.Earth.Height - py;
                }
                MyGame.Instance.Gfx.Mascarapared.GetData(MyGame.Instance.ScreenInGame.Colormask2);
                //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = MyGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                for (int r = 0; r < amount; r++)
                {
                    if (MyGame.Instance.ScreenInGame.SteelON)
                    {
                        sx = r % width2;
                        int sy = r / width2;
                        x.X = px + sx;
                        x.Y = py + sy;
                        if (MyGame.Instance.ScreenInGame.Steel != null &&
                            Array.Exists(MyGame.Instance.ScreenInGame.Steel, s => s.area.Contains(x)))
                        {
                            sx = -777;
                        }
                        if (sx == -777)
                        {
                            Walker = true;
                            Numframes = SizeSprites.walker_frames;
                            Actualframe = 0;
                            Miner = false;
                            break;
                        }
                    }
                    if (MyGame.Instance.ScreenInGame.Colorsobre2[r].R > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[r].G > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[r].B > 0)
                    {
                        MyGame.Instance.ScreenInGame.Frente2++;
                    }
                    if (MyGame.Instance.ScreenInGame.Colormask2[r].R > 0 || MyGame.Instance.ScreenInGame.Colormask2[r].G > 0 || MyGame.Instance.ScreenInGame.Colormask2[r].B > 0)
                    {
                        MyGame.Instance.ScreenInGame.Colorsobre2[r].PackedValue = 0;
                    }
                }
                rectangleFill.X = px;
                rectangleFill.Y = py;
                rectangleFill.Width = width2;
                rectangleFill.Height = top2;
                MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        MyGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
                        amount++;
                    }
                }
                if (sx == -777)
                    return;
                PosX += 12;
                PosY++;
                if (MyGame.Instance.ScreenInGame.Frente2 == 0)
                {
                    Miner = false;
                    Walker = true;
                    Actualframe = 0;
                    Numframes = SizeSprites.walker_frames;
                    return;
                }
            }
            else
            {
                int width2 = 20;
                int top2 = 20;
                int px = PosX - 4;
                if (px < 0)
                {
                    px = 0;
                }
                int py = PosY + 14;
                if (py < 0) // top of the level
                {
                    py = 0;
                }
                if (px < 0) // left of the level
                {
                    px = 0;
                }
                if (px + width2 >= MyGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = MyGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= MyGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = MyGame.Instance.ScreenInGame.Earth.Height - py;
                }
                MyGame.Instance.Gfx.Mascarapared.GetData(MyGame.Instance.ScreenInGame.Colormask2);
                //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = MyGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                for (int r = 0; r < amount; r++)
                {
                    if (MyGame.Instance.ScreenInGame.SteelON)
                    {
                        sx = r % width2;
                        int sy = r / width2;
                        x.X = px + sx;
                        x.Y = py + sy;
                        if (Array.Exists(MyGame.Instance.ScreenInGame.Steel, s => s.area.Contains(x)))
                            sx = -777;
                        if (sx == -777)
                        {
                            Walker = true;
                            Numframes = SizeSprites.walker_frames;
                            Actualframe = 0;
                            Miner = false;
                            break;
                        }
                    }
                    if (MyGame.Instance.ScreenInGame.Colorsobre2[amount - 1 - r].R > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[amount - 1 - r].G > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[amount - 1 - r].B > 0)
                    {
                        MyGame.Instance.ScreenInGame.Frente2++;
                    }
                    if (MyGame.Instance.ScreenInGame.Colormask2[amount - 1 - r].R > 0 || MyGame.Instance.ScreenInGame.Colormask2[amount - 1 - r].G > 0 || MyGame.Instance.ScreenInGame.Colormask2[amount - 1 - r].B > 0)
                    {
                        MyGame.Instance.ScreenInGame.Colorsobre2[r].PackedValue = 0;
                    }
                }
                rectangleFill.X = px;
                rectangleFill.Y = py;
                rectangleFill.Width = width2;
                rectangleFill.Height = top2;
                MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        MyGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
                        amount++;
                    }
                }
                if (sx == -777)
                    return;
                PosX -= 12;
                PosY++;
                if (MyGame.Instance.ScreenInGame.Frente2 == 0)
                {
                    Basher = false;
                    Walker = true;
                    Actualframe = 0;
                    Numframes = SizeSprites.walker_frames;
                    return;
                }
            }
            MyGame.Instance.ScreenInGame.Frente2 = 0;  /////// PPPPPPPPIIIIIIIIIICCCCCCCCCCCCCCCCCOOOOOOOOOOOOOOOOOOO  BASHER LOGIC puto33
        }

        if (Basher && (Actualframe == 10 || Actualframe == 37) && MyGame.Instance.ScreenInGame.Draw2)
        {
            if (MyGame.Instance.ScreenInGame.ArrowsON) // basher arrows logic areaTrap Intersects
            {
                bool nobasher = false;
                Rectangle arrowLem;
                arrowLem.X = PosX;
                arrowLem.Y = PosY;
                arrowLem.Width = 28;
                arrowLem.Height = 28;
                if (MyGame.Instance.ScreenInGame.Arrow != null)
                {
                    foreach (Vararrows arrow in MyGame.Instance.ScreenInGame.Arrow)
                    {
                        if (arrow.area.Intersects(arrowLem) && Right && !arrow.right)
                        {
                            nobasher = true;
                            continue;
                        }
                        if (arrow.area.Intersects(arrowLem) && Left && arrow.right)
                        {
                            nobasher = true;
                        }
                    }
                }
                if (nobasher)
                {
                    MyGame.Instance.Sfx.Ting.Replay();
                    Basher = false;
                    Walker = true;
                    Actualframe = 0;
                    Numframes = SizeSprites.walker_frames;
                    return;
                }
            }
            if (Right)
            {
                int width2 = 20;
                int top2 = 20;
                int px = PosX + 14;
                int py = PosY + 8;
                if (py < 0) // top of the level
                {
                    py = 0;
                }
                if (px < 0) // left of the level
                {
                    px = 0;
                }
                if (px + width2 >= MyGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = MyGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= MyGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = MyGame.Instance.ScreenInGame.Earth.Height - py;
                }
                MyGame.Instance.Gfx.Mascarapared.GetData(MyGame.Instance.ScreenInGame.Colormask2);
                //////// optimized for hd3000 laptop
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = MyGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                int xEmpty = 0;
                int xErase = width2;
                MyGame.Instance.ScreenInGame.Frente = 0;
                sx = 0;
                for (int valX = 0; valX < width2; valX++)
                {
                    MyGame.Instance.ScreenInGame.Frente = 0;
                    for (int valY = 0; valY < top2; valY++)
                    {
                        if (MyGame.Instance.ScreenInGame.SteelON)
                        {
                            x.X = px + valX;
                            x.Y = py + valY;
                            if (Array.Exists(MyGame.Instance.ScreenInGame.Steel, s => s.area.Contains(x)))
                                sx = -777;
                            if (sx == -777)
                            {
                                Walker = true;
                                Numframes = SizeSprites.walker_frames;
                                Actualframe = 0;
                                Basher = false;
                                break;
                            }
                        }
                        if ((MyGame.Instance.ScreenInGame.Colormask2[(valY * width2) + valX].R > 0 || MyGame.Instance.ScreenInGame.Colormask2[(valY * width2) + valX].G > 0 || MyGame.Instance.ScreenInGame.Colormask2[(valY * width2) + valX].B > 0) &&
                            (MyGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].R > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].G > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].B > 0))
                        {
                            MyGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].PackedValue = 0;
                            MyGame.Instance.ScreenInGame.Frente++;
                        }
                    }
                    if (sx == -777)
                        break;
                    if (MyGame.Instance.ScreenInGame.Frente == 0)
                        xEmpty = valX;
                    if (MyGame.Instance.ScreenInGame.Frente > 0)
                    {
                        xErase = valX;
                    }
                    if (xEmpty > xErase)
                        break;
                }
                xEmpty++;
                xErase++;
                rectangleFill.X = px;
                rectangleFill.Y = py;
                rectangleFill.Width = width2;
                rectangleFill.Height = top2;
                MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        MyGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
                        amount++;
                    }
                }
                if (sx == -777)
                    return;
                if (xEmpty < xErase)
                    PosX += 14;
                if (xEmpty > xErase || xErase == 21)
                {
                    Basher = false;
                    Walker = true;
                    Actualframe = 0;
                    Numframes = SizeSprites.walker_frames;
                    return;
                }
            }
            else
            {
                int width2 = 20;
                int top2 = 20;
                int px = PosX - 5;
                if (px < 0)
                {
                    px = 0;
                }
                int py = PosY + 8;
                if (py < 0) // top of the level
                {
                    py = 0;
                }
                if (px < 0) // left of the level
                {
                    px = 0;
                }
                if (px + width2 >= MyGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = MyGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= MyGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = MyGame.Instance.ScreenInGame.Earth.Height - py;
                }
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = MyGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                int xEmpty = width2;
                int xErase = 0;
                MyGame.Instance.ScreenInGame.Frente = 0;
                sx = 0;
                for (int valX = width2 - 1; valX >= 0; valX--)
                {
                    MyGame.Instance.ScreenInGame.Frente = 0;
                    for (int valY = 0; valY < top2; valY++)
                    {
                        if (MyGame.Instance.ScreenInGame.SteelON)
                        {
                            x.X = px + valX;
                            x.Y = py + valY;
                            if (Array.Exists(MyGame.Instance.ScreenInGame.Steel, s => s.area.Contains(x)))
                                sx = -777;
                            if (sx == -777)
                            {
                                Walker = true;
                                Numframes = SizeSprites.walker_frames;
                                Actualframe = 0;
                                Basher = false;
                                break;
                            }
                        }
                        if ((MyGame.Instance.ScreenInGame.Colormask2[valY * width2 + valX].R > 0 || MyGame.Instance.ScreenInGame.Colormask2[valY * width2 + valX].G > 0 || MyGame.Instance.ScreenInGame.Colormask2[valY * width2 + valX].B > 0) &&
                            (MyGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].R > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].G > 0 || MyGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].B > 0))
                        {
                            MyGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].PackedValue = 0;
                            MyGame.Instance.ScreenInGame.Frente++;
                        }
                    }
                    if (sx == -777)
                        break;
                    if (MyGame.Instance.ScreenInGame.Frente == 0)
                        xEmpty = valX;
                    if (MyGame.Instance.ScreenInGame.Frente > 0)
                    {
                        xErase = valX;
                    }
                    if (xEmpty < xErase)
                        break;
                }
                xEmpty++;
                xErase++;
                rectangleFill.X = px;
                rectangleFill.Y = py;
                rectangleFill.Width = width2;
                rectangleFill.Height = top2;
                MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        MyGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = MyGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
                        amount++;
                    }
                }
                if (sx == -777)
                    return;
                if (xEmpty > xErase)
                    PosX -= 14;
                if (xEmpty < xErase || xEmpty == 1) // xerase==20 nothing erases
                {
                    Basher = false;
                    Walker = true;
                    Actualframe = 0;
                    Numframes = SizeSprites.walker_frames;
                    return;
                }
            }
            MyGame.Instance.ScreenInGame.Frente2 = 0;
            ////////////////////////////////////////////////////////////////////// PPPPPPPPPAAAAAAARRRRRRRRRRRRRRRREEEEEEEDDDDDDDDD
        }
        if (Basher && below > 3)
        {
            Basher = false;
            Walker = true;
            Actualframe = 0;
            Numframes = SizeSprites.walker_frames;
            return;
        }
        if (Builder && MyGame.Instance.ScreenInGame.Draw_builder) // BUILDER LOGIC HERE chink sound see limits tooo FIX FIX FIX
        {
            if (Actualframe >= 48 && Numstairs < 12) // >=33 old with dibuja2
            {
                MyGame.Instance.ScreenInGame.Frente = 0;
                Actualframe = SizeSprites.builder_frames + 1;  // erase with // no compiling//  to see full frames
                if (Right)
                {
                    if (above > 1)
                    {
                        PosY += 6;
                        PosX -= 14;
                        Builder = false;
                        Walker = true;
                        Actualframe = 0;
                        Numframes = SizeSprites.walker_frames;
                        Numstairs = 0;
                        Right = false;
                        return;
                    }
                    if (PosY < -24) //see ok was -24 but sometimes fails the u-turn
                    {
                        Builder = false;
                        Walker = true;
                        Actualframe = 0;
                        Numframes = SizeSprites.walker_frames;
                        PosY += 3;
                        PosX -= 6;
                        return;
                    }
                    for (int y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                    {
                        int posi_real = (PosY + 24 + y) * MyGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xx88 = 14; xx88 <= 28; xx88++)
                        {
                            if (MyGame.Instance.ScreenInGame.C25[posi_real + xx88].R == 0 && MyGame.Instance.ScreenInGame.C25[posi_real + xx88].G == 0 && MyGame.Instance.ScreenInGame.C25[posi_real + xx88].B == 0)
                            {
                                Color colorFill = new()
                                {
                                    R = Convert.ToByte(255 - (Numstairs * 5)),
                                    G = 0,
                                    B = Convert.ToByte(255 - (Numstairs * 10)),
                                    A = 255,
                                };
                                MyGame.Instance.ScreenInGame.C25[posi_real + xx88] = colorFill;
                            } //steps color stairs
                            else
                            {
                                if (xx88 == 19)
                                    MyGame.Instance.ScreenInGame.Frente++;
                            }
                        }
                    }
                    Numstairs++;
                    PosY -= 3;
                    PosX += 6;
                    if (Numstairs >= 10)
                    {
                        MyGame.Instance.Sfx.Chink.Replay();
                    }
                    int amount = 0;
                    for (int ykk = 27; ykk < 31; ykk++)
                    {
                        int posi_real = (PosY + ykk) * MyGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xkk = 0; xkk < 28; xkk++)
                        {
                            MyGame.Instance.ScreenInGame.Colormask22[amount] = MyGame.Instance.ScreenInGame.C25[posi_real + xkk];
                            amount++;
                        }
                    }
                    rectangleFill.X = PosX;
                    rectangleFill.Y = PosY + 27;
                    rectangleFill.Width = 28;
                    rectangleFill.Height = 4;
                    MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colormask22, 0, 28 * 4);
                    if (MyGame.Instance.ScreenInGame.Frente == 3)
                    {
                        Builder = false;
                        Walker = true;
                        Actualframe = 0;
                        Numframes = SizeSprites.walker_frames;
                        Numstairs = 0;
                        PosX -= 7;
                        Right = false;
                    }
                    return;
                }
                else
                {
                    if (above > 1)
                    {
                        PosY += 6;
                        PosX += 15;
                        Builder = false;
                        Walker = true;
                        Actualframe = 0;
                        Numframes = SizeSprites.walker_frames;
                        Numstairs = 0;
                        Right = true;
                        return;
                    }
                    if (PosY < -24) //see ok was -24
                    {
                        Builder = false;
                        Walker = true;
                        Actualframe = 0;
                        Numframes = SizeSprites.walker_frames;
                        PosY += 3;
                        PosX += 6;
                        return;
                    }
                    for (int y = 1; y <= 3; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                    {
                        int posi_real = (PosY + 24 + y) * MyGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xx88 = 0; xx88 <= 14; xx88++)
                        {
                            if (MyGame.Instance.ScreenInGame.C25[posi_real + xx88].R == 0 && MyGame.Instance.ScreenInGame.C25[posi_real + xx88].G == 0 && MyGame.Instance.ScreenInGame.C25[posi_real + xx88].B == 0)
                            {
                                Color colorFill = new()
                                {
                                    R = Convert.ToByte(255 - (Numstairs * 5)),
                                    G = 0,
                                    B = Convert.ToByte(255 - (Numstairs * 10)),
                                    A = 255,
                                };
                                MyGame.Instance.ScreenInGame.C25[posi_real + xx88] = colorFill;
                            }//magenta stairs
                            else
                            {
                                if (xx88 == 9)
                                    MyGame.Instance.ScreenInGame.Frente++;
                            }
                        }
                    }
                    Numstairs++;
                    PosY -= 3;
                    PosX -= 6;
                    if (Numstairs >= 10)
                    {
                        MyGame.Instance.Sfx.Chink.Replay();
                    }
                    //earth.SetData<Color>(c25); //OPTIMIZED BUILDER SETDATA
                    int amount = 0;
                    int px = PosX;
                    if (px < 0)
                        px = 0;
                    for (int ykk = 27; ykk < 31; ykk++)
                    {
                        int posi_real = (PosY + ykk) * MyGame.Instance.ScreenInGame.Earth.Width + px;
                        for (int xkk = 0; xkk < 28; xkk++)
                        {
                            MyGame.Instance.ScreenInGame.Colormask22[amount] = MyGame.Instance.ScreenInGame.C25[posi_real + xkk];
                            amount++;
                        }
                    }
                    rectangleFill.X = px;
                    rectangleFill.Y = PosY + 27;
                    rectangleFill.Width = 28;
                    rectangleFill.Height = 4;
                    MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colormask22, 0, 28 * 4);
                    if (MyGame.Instance.ScreenInGame.Frente == 3)
                    {
                        Builder = false;
                        Walker = true;
                        Actualframe = 0;
                        Numframes = SizeSprites.walker_frames;
                        Numstairs = 0;
                        PosX += 8;
                        Right = true;
                    }
                    return;
                }
            }
            if (Numstairs >= 12 &&
                !Bridge)
            {
                Builder = false;
                Bridge = true;
                PixelsDrop = 0;
                if (Right)
                {
                    PosX -= 6;
                }
                else
                {
                    PosX += 6;
                }
                Actualframe = 0;
                Numframes = SizeSprites.walker_frames;
            }
        }
        if (Bridge && Actualframe == 7 && Bridge)
        {
            Bridge = false;
            Walker = true;
            Actualframe = 0;
            Numframes = SizeSprites.walker_frames;
            Numstairs = 0;
            return;
        }
        if (Digger) ///// DIGGER DIGGER WARNING WARNING
        {
            if (below == 0 || below == 1) // 5 ok que no se aceleren a digger si hay mas de 2 juntos antes era <9 los pixeles debajo de sus pies
            {
                int abajo2 = 0;
                int pixx2 = PosX + 14;
                for (int xx88 = 0; xx88 <= 4; xx88++)
                {
                    int pos_real2 = PosY + xx88 + 28;  ///////////// pixel por debajo.............
                    if (pos_real2 == MyGame.Instance.ScreenInGame.Earth.Height)
                    {
                        abajo2 = 9;
                        break;
                    }
                    if (pos_real2 < 0)
                        pos_real2 = 0;
                    if (pos_real2 > MyGame.Instance.ScreenInGame.Earth.Height)
                    {
                        pos_real2 = MyGame.Instance.ScreenInGame.Earth.Height;
                    }
                    if (MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx2].R > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx2].G > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx2].B > 0)
                    {
                        abajo2++;
                    }
                    else
                    {
                        break;
                    }
                }
                if ((Actualframe == 11 || Actualframe == 26) && MyGame.Instance.ScreenInGame.Draw_walker)
                {
                    sx = 0;
                    for (int y = 9; y <= 18; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                    {
                        int posi_real = (PosY + 14 + y) * MyGame.Instance.ScreenInGame.Earth.Width + PosX;
                        if (PosY + 14 + y > MyGame.Instance.ScreenInGame.Earth.Height)
                        {
                            break;
                        } // cortar si esta en el limite por debajo 512=earth.height
                        for (int xx88 = 4; xx88 <= 24; xx88++)
                        {
                            if (MyGame.Instance.ScreenInGame.SteelON)
                            {
                                x.X = PosX + xx88;
                                x.Y = PosY + 14 + y;
                                if (Array.Exists(MyGame.Instance.ScreenInGame.Steel, s => s.area.Contains(x)))
                                    sx = -777;
                                if (sx == -777)
                                {
                                    Walker = true;
                                    Numframes = SizeSprites.walker_frames;
                                    Actualframe = 0;
                                    Digger = false;
                                    break;
                                }
                            }
                            if (sx == -777)
                                break;
                            Color colorFill = new()
                            {
                                R = 0,
                                G = 0,
                                B = 0,
                                A = 0,
                            };
                            MyGame.Instance.ScreenInGame.C25[posi_real + xx88] = colorFill; // Color.TransparentBlack is the same i think. 0,0,0,0.
                        }
                    }
                    //earth.SetData<Color>(c25); //OPTIMIZED digger SETDATA
                    int amount = 0;
                    for (int ykk = 9; ykk <= 18; ykk++)
                    {
                        int posi_real = (PosY + 14 + ykk) * MyGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xkk = 0; xkk < 28; xkk++)
                        {
                            MyGame.Instance.ScreenInGame.Colormask22[amount] = MyGame.Instance.ScreenInGame.C25[posi_real + xkk];
                            amount++;
                        }
                    }
                    rectangleFill.X = PosX;
                    rectangleFill.Y = PosY + 23;
                    rectangleFill.Width = 28;
                    rectangleFill.Height = 10;
                    MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colormask22, 0, 28 * 10);
                    if (sx == -777)
                        return;
                    PosY += abajo2;
                    return;
                }
            }
            else
            {
                if (PosY + 28 >= MyGame.Instance.ScreenInGame.Earth.Height) // erase draws bottom when die and exit level height 21x10
                {
                    for (int ykk = 0; ykk < 210; ykk++)
                    {
                        MyGame.Instance.ScreenInGame.Colormask22[ykk].PackedValue = 0;
                    }
                    rectangleFill.Y = 502;
                    rectangleFill.X = PosX + 4;
                    rectangleFill.Width = 21;
                    rectangleFill.Height = 10;
                    MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colormask22, 0, 210);
                }
                Basher = false;
                Builder = false;
                Miner = false;
                Climbing = false;
                Digger = false;
                Fall = true;
                Walker = false;
                PixelsDrop = 0;
                Actualframe = 0;
                Numframes = SizeSprites.faller_frames;
                return; //break o continue DUNNO I DON'T KNOW WHICH IS BETTER
            }

        }
        if (Climbing)
        {
            if (PosY <= -28) // top of level -- out of limits 28 size sprite lemming 28x28
            {
                Climbing = false;
                Fall = true;
                Walker = false;
                PixelsDrop = 0;
                Numframes = SizeSprites.faller_frames;
                Actualframe = 0;
                Builder = false;
                Right = !Right;
                return;
            }
            if (Right)
            {
                int pos_real2 = PosY + 27;
                if (MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx - 2].R > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx - 2].G > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx - 2].B > 0)
                {
                    Right = false;
                    PosX -= 2;   // 1 o 2 LOOK
                    Climbing = false;
                    Walker = true;
                    Numframes = SizeSprites.walker_frames;
                    Actualframe = 0;
                    return;
                }
            }
            else
            {
                int pos_real2 = PosY + 27;
                if (MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx + 2].R > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx + 2].G > 0 || MyGame.Instance.ScreenInGame.C25[(pos_real2 * MyGame.Instance.ScreenInGame.Earth.Width) + pixx + 2].B > 0)
                {
                    Right = true;
                    PosX += 2; // 1 o 2 LOOK
                    Climbing = false;
                    Walker = true;
                    Numframes = SizeSprites.walker_frames;
                    Actualframe = 0;
                    return;
                }
            }
            if (above > 0 && MyGame.Instance.ScreenInGame.Drawing)
            {
                PosY--;
            }
            if (above == 0)
            {
                if (Right)
                {
                    PosX++;
                }
                else
                {
                    PosX--;
                }
                Climbing = false;
                Walker = true;
                Numframes = SizeSprites.walker_frames;
                Actualframe = 0;
                return;
            }
        }
        if (Walker)
        {
            if (below < 3 && Right)
            {
                PosX++;
                if (above < 16)
                {
                    PosY -= above;
                }
            }  //// <6 o <8 falla cava
            if (below < 3 && Left)
            {
                PosX--;
                if (above < 16)
                {
                    PosY -= above;
                }
            }
            if (above >= 16)
            {
                if (!Climber)
                {
                    if (Right && above >= 16)
                    {
                        Right = false;
                        PosX -= 2;  // 1 o 2 LOOK
                    }
                    else
                    {
                        Right = true;
                        PosX += 2;  // 1 o 2 LOOK
                    }
                }
                else
                {
                    Walker = false;
                    Climbing = true;
                    Numframes = SizeSprites.climber_frames;
                    PixelsDrop = 0;
                    Actualframe = 0;
                    return;
                }
            }
        }
        if (Explode && Actualframe >= 47)
        {
            ////////////////////////////////////////////////////////////////////////////////////// EXPLODE MASK
            ///////////////// EXPLODING MASK LIMITS -- SIZE OF AREA ERASEABLE
            int ancho66 = 38;
            int alto66 = 53;
            int px = PosX - 5; //center the big explosion to 28x28 lemming sprite
            int py = PosY - 2;
            int py2 = 0;
            int px2 = 0;
            if (py < 0) // top of the level
            {
                py2 = py * -1;
                alto66 -= py2;
                py = 0;
            }
            if (px < 0) // left of the level
            {
                px2 = px * -1;
                ancho66 -= px2;
                px = 0;
            }
            if (px + ancho66 >= MyGame.Instance.ScreenInGame.Earth.Width)  // right of the level
            {
                ancho66 = MyGame.Instance.ScreenInGame.Earth.Width - px;
            }
            if (py + alto66 >= MyGame.Instance.ScreenInGame.Earth.Height) //bottom of the level
            {
                alto66 = MyGame.Instance.ScreenInGame.Earth.Height - py;
            }
            int amount = ancho66 * alto66;
            rectangleFill.X = px2;
            rectangleFill.Y = py2;
            rectangleFill.Width = ancho66;
            rectangleFill.Height = alto66;
            MyGame.Instance.ScreenMainMenu.MainMenuGfx.Mascaraexplosion.GetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colormask33, 0, amount);
            amount = 0;
            for (int yy88 = 0; yy88 < alto66; yy88++)
            {
                int yypos888 = (yy88 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                for (int xx88 = 0; xx88 < ancho66; xx88++)
                {
                    MyGame.Instance.ScreenInGame.Colorsobre33[amount].PackedValue = MyGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                    amount++;
                }
            }
            for (int r = 0; r < amount; r++)
            {
                if (MyGame.Instance.ScreenInGame.SteelON)
                {
                    sx = r % ancho66;
                    int sy = r / ancho66;
                    x.X = px + sx;
                    x.Y = py + sy;
                    if (Array.Exists(MyGame.Instance.ScreenInGame.Steel, s => s.area.Contains(x)))
                        sx = -777;
                    if (sx == -777)
                        continue;
                }
                if (MyGame.Instance.ScreenInGame.Colormask33[r].R > 0 || MyGame.Instance.ScreenInGame.Colormask33[r].G > 0 || MyGame.Instance.ScreenInGame.Colormask33[r].B > 0)
                {
                    MyGame.Instance.ScreenInGame.Colorsobre33[r].PackedValue = 0;
                }
            }
            rectangleFill.X = px;
            rectangleFill.Y = py;
            rectangleFill.Width = ancho66;
            rectangleFill.Height = alto66;
            MyGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, MyGame.Instance.ScreenInGame.Colorsobre33, 0, amount);
            // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
            amount = 0;
            for (int yy33 = 0; yy33 < alto66; yy33++)
            {
                int yypos555 = (yy33 + py) * MyGame.Instance.ScreenInGame.Earth.Width;
                for (int xx33 = 0; xx33 < ancho66; xx33++)
                {
                    MyGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = MyGame.Instance.ScreenInGame.Colorsobre33[amount].PackedValue;
                    amount++;
                }
            }
            Dead = true;
            MyGame.Instance.ScreenInGame.Numlemnow--;
            Explode = false;
            Exploser = false;
            // luto luto sound fix
            MyGame.Instance.Sfx.Explode.Replay();
            //explosions addons emitter - particles logic add
            int xExp = PosX + 14;
            int yExp = PosY + 14;
            MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, 0].MaxCounter = 0;
            MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, 0].Counter = 0;
            for (int Iexplo = 0; Iexplo < GlobalConst.PARTICLE_NUM; Iexplo++)
            {
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].MaxCounter = 0;
                byte colorr = (byte)GlobalConst.Rnd.Next(255);
                byte colorg = (byte)GlobalConst.Rnd.Next(255);
                byte colorb = (byte)GlobalConst.Rnd.Next(255);
                Color colorFill = new()
                {
                    R = colorr,
                    G = colorg,
                    B = colorb,
                    A = 255,
                };
                int LifeCount = GlobalConst.LIFE_COUNTER + (int)(GlobalConst.Rnd.NextDouble() * 2 * GlobalConst.LIFE_VARIANCE) - GlobalConst.LIFE_VARIANCE;
                if (LifeCount > MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, 0].MaxCounter)
                    MyGame.Instance.Explosion[0, 0].MaxCounter = LifeCount;
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].dx = (GlobalConst.Rnd.NextDouble() * (SizeSprites.MAX_DX - SizeSprites.MIN_DX) + SizeSprites.MIN_DX);
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].dy = (GlobalConst.Rnd.NextDouble() * (SizeSprites.MAX_DY - SizeSprites.MIN_DY) + SizeSprites.MIN_DY);
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].x = xExp;
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].y = yExp;
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].Color = colorFill;
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].LifeCtr = LifeCount;
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].Rotation = (float)GlobalConst.Rnd.NextDouble();
                MyGame.Instance.Explosion[MyGame.Instance.ScreenInGame.ActItem, Iexplo].Size = (float)(GlobalConst.Rnd.NextDouble() / 2);
            }
            MyGame.Instance.ScreenInGame.Exploding = true;
            MyGame.Instance.ScreenInGame.ActItem++;
            if (MyGame.Instance.ScreenInGame.ActItem > GlobalConst.totalExplosions - 1)
                MyGame.Instance.ScreenInGame.ActItem = GlobalConst.totalExplosions - 1;
            return;
        }
        if (!Falling && Active)
        {
            if (below >= 3)
            {
                PosY += 3;
                PixelsDrop += 3;
            }
            else
            {
                PosY += below;
                PixelsDrop += below;
            } // fall 3 MAX---MAX 3 FALL PIXELS
        }
        else
        {
            if (!Drown && MyGame.Instance.ScreenInGame.Drawing)
            {
                if (below >= 3)
                {
                    PosY += 3;
                }
                else
                {
                    PosY += below;
                }
            }
        }
        if (PosY < -27) // walker top of the screen
        {
            if (Right)
            {
                Right = false;
                PosX -= 3;
                PosY++;
            }
            else
            {
                Right = true;
                PosX += 3;
                PosY++;
            }
        }
        if (PosX < -16 ||
            PosX + 14 > MyGame.Instance.ScreenInGame.Earth.Width)// limits of the screen from LEFT
        {
            Active = false;
            Dead = true;
            MyGame.Instance.ScreenInGame.Numlemnow--;
            Explode = false;
            Exploser = false;
            MyGame.Instance.Sfx.Die.Replay();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int framesale;
        if (Dead)
            return;
        if (Exploser && !Explode)
        {
            if (Time == 0)
                Time = MyGame.Instance.ScreenInGame.TotalTime;
            double timez = MyGame.Instance.ScreenInGame.TotalTime - Time;
            int crono = (int)(6f - (float)timez);
            MyGame.Instance.Fonts.TextLem(string.Format("{0}", crono), new Vector2(PosX + 3 - MyGame.Instance.ScreenInGame.ScrollX, PosY - 10 - MyGame.Instance.ScreenInGame.ScrollY), Color.White, 0.4f, 0.000000000004f, spriteBatch);
            if (crono <= 0)
            {
                // luto luto sound monogame 3.2 works ok without catch exception
                MyGame.Instance.Sfx.OhNo.Replay();
                Explode = true;
                Active = false;
                Umbrella = false;
                Walker = false;
                Digger = false;
                Climber = false;
                Fall = false;
                Falling = false;
                Climbing = false;
                Exit = false;
                Blocker = false;
                Builder = false;
                Bridge = false;
                Basher = false;
                Miner = false;
                Actualframe = 0;
                Numframes = SizeSprites.bomber_frames;
            }
        }
        int framereal55;
        if (Burned) // scale POSDraw x+0,y+0 at 1.2f x-5,y+0 at 1.35f
        {
            spriteBatch.Draw(MyGame.Instance.Gfx.Squemado, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX - 5, PosY - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(0, Actualframe * 28, 32, 28),
            (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, GlobalConst.SizeL, SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
            spriteBatch.Draw(MyGame.Instance.Gfx.Lhiss, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX, PosY - 20 - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(0, 0, MyGame.Instance.Gfx.Lhiss.Width, MyGame.Instance.Gfx.Lhiss.Height),
                Color.White, 0f, Vector2.Zero, (0.5f + (0.01f * Actualframe)), SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Drown) // scale POSDraw x+0,y+10 at 1.2f x-8,y+7 at 1.35f  //puto ahoga
        {
            spriteBatch.Draw(MyGame.Instance.Sprites.Drowner, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.water_xpos, PosY + SizeSprites.water_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(Actualframe * SizeSprites.water_width, 0, SizeSprites.water_width, SizeSprites.water_height),
                (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.water_size, SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Walker)
        {
            framereal55 = (Actualframe * SizeSprites.walker_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Walker, new Vector2((PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.walker_xpos), PosY - MyGame.Instance.ScreenInGame.ScrollY + SizeSprites.walker_ypos), new Rectangle(framereal55, 0, SizeSprites.walker_width, SizeSprites.walker_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.walker_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Blocker) // blocker scale POSDraw x-5 y+4 at 1.2f x-7 y+1 at 1.35f  //puto
        {
            framesale = (Actualframe * SizeSprites.blocker_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Blocker, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.blocker_xpos, PosY + SizeSprites.blocker_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.blocker_width, SizeSprites.blocker_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.blocker_size, SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
            if (MyGame.Instance.DebugOsd.Debug)
            {
                Rectangle bloqueo = new(PosX, PosY, 28, 28);
                spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(bloqueo.Left - MyGame.Instance.ScreenInGame.ScrollX, bloqueo.Top - MyGame.Instance.ScreenInGame.ScrollY, bloqueo.Width, bloqueo.Height), null,
                    Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            }
        }
        if (Bridge) // scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f
        {
            framesale = (Actualframe * 26);
            spriteBatch.Draw(MyGame.Instance.Gfx.Puente_nomas, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX - 7, PosY - 7 - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(0, framesale, 32, 26), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, GlobalConst.SizeL, (Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Builder)  //scale POSDraw x-5,y-3 at 1.2f x-7,y-7 at 1.35f  builder builder draws
        {
            if (Numstairs >= 10) // chink draws
            {
                spriteBatch.Draw(MyGame.Instance.Sprites.Chink, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX - 10, PosY - 30 - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(0, 0, MyGame.Instance.Sprites.Chink.Width, MyGame.Instance.Sprites.Chink.Height),
                    Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * Actualframe), SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
            }
            framesale = (Actualframe * SizeSprites.builder_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Puente, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.builder_xpos, PosY + SizeSprites.builder_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.builder_width, SizeSprites.builder_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.builder_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Miner)  //scale POSDraw x-5,y-2 at 1.2f x-9,y-7 at 1.35f pico pico miner miner
        {
            framesale = (Actualframe * SizeSprites.miner_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Pico, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.miner_xpos + (Right ? 0 : 10), PosY + SizeSprites.miner_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.miner_width, SizeSprites.miner_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.miner_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Basher) //puto
        {           // scale basher RIGHT POSDRAW x-10,y+4 at 1.2f x-15,y+1 at 1.35f
            framesale = (Actualframe * SizeSprites.basher_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Pared, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + (Right ? SizeSprites.basher_xpos : SizeSprites.basher_xposleft), PosY + SizeSprites.basher_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.basher_width, SizeSprites.basher_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.basher_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Explode) // explotando explotando bomber bomber
        {
            // bomber scale POSDraw x-5,y+4 at 1.2f x-9,y+2 at 1.35f
            framesale = (Actualframe * SizeSprites.bomber_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Exploder, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.bomber_xpos, PosY + SizeSprites.bomber_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.bomber_width, SizeSprites.bomber_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.bomber_size, SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
            spriteBatch.Draw(MyGame.Instance.Sprites.Lohno, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX - 20, PosY - 25 - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(0, 0, MyGame.Instance.Sprites.Lohno.Width, MyGame.Instance.Sprites.Lohno.Height),
                Color.White, 0f, Vector2.Zero, 0.7f + (0.01f * Actualframe), SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Breakfloor) // scale POSDraw x-5,y+4 at 1.2f  x-9,y+2 at 1.35f breakfloor breakfloor
        {
            framesale = (Actualframe * SizeSprites.floor_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Rompesuelo, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.floor_xpos, PosY + SizeSprites.floor_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.floor_width, SizeSprites.floor_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floor_size, SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
            if (Actualframe == SizeSprites.floor_frames - 1)
            {
                Dead = true;
                MyGame.Instance.ScreenInGame.Numlemnow--;
                Explode = false;
                Exploser = false;
            }
        }
        if (Falling) //umbrella paraguas falling with umbrella
        {
            if (!Framescut && Actualframe == SizeSprites.floater_frames - 1)
            {
                Framescut = true;
                Actualframe = 0;
                Numframes = SizeSprites.floater_frames - 1 - 4;
            }
            if (!Framescut)
                framesale = (Actualframe * SizeSprites.floater_width);
            else
                framesale = (Actualframe + 4) * SizeSprites.floater_width; // scale floater POSDraw x-5,y-4 at 1.2f x-9,y-7 at 1.35f
            spriteBatch.Draw(MyGame.Instance.Sprites.Paraguas, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.floater_xpos, PosY + SizeSprites.floater_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.floater_width, SizeSprites.floater_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.floater_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Fall) //fall cae
        {
            framereal55 = (Actualframe * SizeSprites.faller_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Falling, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.faller_xpos, PosY - MyGame.Instance.ScreenInGame.ScrollY + SizeSprites.faller_ypos), new Rectangle(framereal55, 0, SizeSprites.faller_width, SizeSprites.faller_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.faller_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Exit && !Dead) //sale sale exit exit out out
        {
            framesale = (Actualframe * SizeSprites.sale_width); // exit scale POSDraw  x-1,y+1 at 1.2f x-3,y-1 at 1.35f
            spriteBatch.Draw(MyGame.Instance.Gfx.Sale, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.sale_xpos, PosY + SizeSprites.sale_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.sale_width, SizeSprites.sale_height), Color.White, 0f, Vector2.Zero, SizeSprites.sale_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
        if (Digger)
        {
            framereal55 = (Actualframe * SizeSprites.digger_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Digger, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + SizeSprites.digger_xpos, PosY + 6 - MyGame.Instance.ScreenInGame.ScrollY + SizeSprites.digger_ypos), new Rectangle(framereal55, 0, SizeSprites.digger_width, SizeSprites.digger_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.digger_size, SpriteEffects.None, GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }

        if (Climbing) // scale POSDraw x-5,y+6 at 1.2f x-8.y+3 at 1.35f  //puto33
        {
            framesale = (Actualframe * SizeSprites.climber_width);
            spriteBatch.Draw(MyGame.Instance.Sprites.Climber, new Vector2(PosX - MyGame.Instance.ScreenInGame.ScrollX + (Right ? SizeSprites.climber_xpos : SizeSprites.climber_xposleft), PosY + SizeSprites.climber_ypos - MyGame.Instance.ScreenInGame.ScrollY), new Rectangle(framesale, 0, SizeSprites.climber_width, SizeSprites.climber_height), (Onmouse ? Color.Red : Color.White), 0f, Vector2.Zero, SizeSprites.climber_size, (Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None), GlobalConst.Lem_depth + (NumLemming * 0.00001f));
        }
    }
}
