using Lemmings.NET.Constants;
using Lemmings.NET.Models;
using Lemmings.NET.Screens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lemmings.NET.Structs;

public struct Lem //puto
{
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
    public int Pixelscaida { get; set; }
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

    private int _below;

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
        for (int b = 0; b < LemmingsNetGame.Instance.ScreenInGame.NumLemmings; b++)
        {
            if (LemmingsNetGame.Instance.ScreenInGame.Lemming[b].Blocker && b != LemmingsNetGame.Instance.ScreenInGame.ActLEM)
            {
                bloqueo.X = LemmingsNetGame.Instance.ScreenInGame.Lemming[b].PosX;
                bloqueo.Y = LemmingsNetGame.Instance.ScreenInGame.Lemming[b].PosY;
                bloqueo.Width = 28;
                bloqueo.Height = 28;
                if (LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].Miner)
                {
                    bloqueo.X = LemmingsNetGame.Instance.ScreenInGame.Lemming[b].PosX + 10;
                    bloqueo.Y = LemmingsNetGame.Instance.ScreenInGame.Lemming[b].PosY;
                    bloqueo.Width = 9;
                    bloqueo.Height = 28;
                }
                poslem.X = LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].PosX + medx;
                poslem.Y = LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].PosY + medy;
                if (bloqueo.Contains(poslem))
                {
                    if (LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].Right)
                    {
                        if (LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].PosX < LemmingsNetGame.Instance.ScreenInGame.Lemming[b].PosX)
                        {
                            LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].Right = false;
                            break;
                        }
                    }
                    else
                    {
                        if (LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].PosX > LemmingsNetGame.Instance.ScreenInGame.Lemming[b].PosX - 1)
                        {
                            LemmingsNetGame.Instance.ScreenInGame.Lemming[LemmingsNetGame.Instance.ScreenInGame.ActLEM].Right = true;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        Onmouse = false; //LEMMING SKILL STRING MOUSE ON
        if ((Input.CurrentMouseState.X + 16 >= PosX - LemmingsNetGame.Instance.ScreenInGame.ScrollX && Input.CurrentMouseState.X + 16 <= PosX - LemmingsNetGame.Instance.ScreenInGame.ScrollX + 28
            && Input.CurrentMouseState.Y + 16 >= PosY - LemmingsNetGame.Instance.ScreenInGame.ScrollY && Input.CurrentMouseState.Y + 16 <= PosY + 28 - LemmingsNetGame.Instance.ScreenInGame.ScrollY) && !LemmingsNetGame.Instance.ScreenInGame.MouseOnLem)
        {
            if (Walker)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Walker";
            else if (Builder)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Builder";
            else if (Basher)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Basher";
            else if (Blocker)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Blocker";
            else if (Miner)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Miner";
            else if (Digger)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Digger";
            if (Climber)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill += ",C";
            if (Umbrella)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill += ",F";
            if (Climbing)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Climber";
            else if (Climbing && Umbrella)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Climber,F";
            else if ((Fall || Falling) && !Umbrella)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Faller";
            else if ((Fall || Falling) && Umbrella)
                LemmingsNetGame.Instance.ScreenInGame.LemSkill = "Floater";
            LemmingsNetGame.Instance.ScreenInGame.MouseOnLem = true;
            Onmouse = true;
        } //  inside the mouse rectangle lemming ON
        if (LemmingsNetGame.Instance.ScreenInGame.TrapsON && !MyGame.Paused) //Traps logic and sounds
        {
            for (int ti = 0; ti < LemmingsNetGame.Instance.ScreenInGame.NumTotTraps; ti++)
            {
                x.X = PosX + 14;
                x.Y = PosY + 25;
                if (LemmingsNetGame.Instance.ScreenInGame.Trap[ti].areaTrap.Contains(x) && !LemmingsNetGame.Instance.ScreenInGame.Trap[ti].isOn && LemmingsNetGame.Instance.ScreenInGame.Trap[ti].type == 666)
                {
                    LemmingsNetGame.Instance.ScreenInGame.Trap[ti].isOn = true;
                    Active = false;
                    Walker = false;
                    Dead = true;
                    LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
                    Explode = false;
                    Exploser = false;
                    switch (LemmingsNetGame.Instance.ScreenInGame.Trap[ti].sprite.Name)
                    {
                        case "traps/dead_marble":
                        case "traps/dead_marble2_fix":
                            if (LemmingsNetGame.Instance.Sfx.StrapTenton.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.StrapTenton.Stop();
                            }
                            LemmingsNetGame.Instance.Sfx.StrapTenton.Play();
                            break;
                        case "traps/dead_trampa":
                            if (LemmingsNetGame.Instance.Sfx.StrapMan.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.StrapMan.Stop();
                            }
                            LemmingsNetGame.Instance.Sfx.StrapMan.Play();
                            break;
                        case "traps/dead_soga":
                            if (LemmingsNetGame.Instance.Sfx.StrapChain.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.StrapChain.Stop();
                            }
                            LemmingsNetGame.Instance.Sfx.StrapChain.Play();
                            break;
                        case "traps/dead_bombona":
                            if (LemmingsNetGame.Instance.Sfx.StrapChupar.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.StrapChupar.Stop();
                            }
                            LemmingsNetGame.Instance.Sfx.StrapChupar.Play();
                            break;
                        case "traps/dead_10":
                            if (LemmingsNetGame.Instance.Sfx.StrapTenTonnes.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.StrapTenTonnes.Stop();
                            }
                            LemmingsNetGame.Instance.Sfx.StrapTenTonnes.Play();
                            break;
                        default:
                            if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.Die.Stop();
                            }
                            try
                            {
                                LemmingsNetGame.Instance.Sfx.Die.Play();
                            }
                            catch (InstancePlayLimitException) { /* Ignore errors */ }
                            break;
                    }
                    break;
                }
                rectangleFill.X = PosX + 14;
                rectangleFill.Y = PosY;
                rectangleFill.Width = 1;
                rectangleFill.Height = 28;
                if (LemmingsNetGame.Instance.ScreenInGame.Trap[ti].areaTrap.Intersects(rectangleFill) && !Burned && !Drown && LemmingsNetGame.Instance.ScreenInGame.Trap[ti].type != 666)
                {
                    switch (LemmingsNetGame.Instance.ScreenInGame.Trap[ti].sprite.Name)
                    {
                        case "traps/dead_spin":
                        case "traps/fuego1":
                        case "traps/fuego2":
                        case "traps/fuego3":
                        case "traps/fuego4":
                            if (LemmingsNetGame.Instance.Sfx.Fire.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.Fire.Stop();
                            }
                            try
                            {
                                LemmingsNetGame.Instance.Sfx.Fire.Play();
                            }
                            catch (InstancePlayLimitException) { /* Ignore errors */ }
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
                            if (LemmingsNetGame.Instance.Sfx.Glup.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.Glup.Stop();
                            }
                            try
                            {
                                LemmingsNetGame.Instance.Sfx.Glup.Play();
                            }
                            catch (InstancePlayLimitException) { /* Ignore errors */ }
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
                            if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                            {
                                LemmingsNetGame.Instance.Sfx.Die.Stop();
                            }
                            try
                            {
                                LemmingsNetGame.Instance.Sfx.Die.Play();
                            }
                            catch (InstancePlayLimitException) { /* Ignore errors */ }
                            Explode = false;
                            Exploser = false;
                            Active = false;
                            Walker = false;
                            Dead = true;
                            LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
                            break;
                    }
                }
            }
        }
        // assign skills to lemmings //////////////////////////////////////////////
        if (LemmingsNetGame.Instance.ScreenInGame.MouseOnLem && (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed))
        {
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.DIGGER && !Digger && Onmouse //DIGGER
                && (Walker || Builder || Basher || Miner))
            {
                LemmingsNetGame.Instance.ScreenInGame.NbDiggerRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbDiggerRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbDiggerRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
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
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.CLIMBER && Onmouse && !Climber) //CLIMBER
            {
                LemmingsNetGame.Instance.ScreenInGame.NbClimberRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbClimberRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbClimberRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
                    Climber = true;
                    return;
                }
            }
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.FLOATER && Onmouse && !Umbrella && !Breakfloor) //FLOATER
            {
                LemmingsNetGame.Instance.ScreenInGame.NbFloaterRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbFloaterRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbFloaterRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
                    Umbrella = true;
                    return;
                }
            }
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.EXPLODER && Onmouse && !Exploser) //BOMBER
            {
                LemmingsNetGame.Instance.ScreenInGame.NbExploderRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbExploderRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbExploderRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
                    Exploser = true;
                    return;
                }
            }
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.BLOCKER && Onmouse && !Blocker //BLOCKER
                && (Walker || Digger || Builder || Basher || Miner))
            {
                LemmingsNetGame.Instance.ScreenInGame.NbBlockerRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbBlockerRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbBlockerRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
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
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.BUILDER && Onmouse && !Builder //BUILDER
                && (Walker || Digger || Basher || Miner || Bridge))
            {
                LemmingsNetGame.Instance.ScreenInGame.NbBuilderRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbBuilderRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbBuilderRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
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
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.BASHER && Onmouse && !Basher //BASHER
                && (Walker || Digger || Builder || Miner))
            {
                LemmingsNetGame.Instance.ScreenInGame.NbBasherRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbBasherRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbBasherRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
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
            if (LemmingsNetGame.Instance.ScreenInGame.InGameMenu.CurrentSelectedSkill == ECurrentSkill.MINER && Onmouse && !Miner //MINER
                && (Walker || Digger || Basher || Builder))
            {
                LemmingsNetGame.Instance.ScreenInGame.NbMinerRemaining--;
                if (LemmingsNetGame.Instance.ScreenInGame.NbMinerRemaining < 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.NbMinerRemaining = 0;
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
                }
                else
                {
                    if (LemmingsNetGame.Instance.Sfx.MousePre.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.MousePre.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.MousePre.Play();
                    Miner = true;
                    Actualframe = 0;
                    Walker = false;
                    Digger = false;
                    Basher = false;
                    Builder = false;
                    Numframes = SizeSprites.pico_frames;
                    return;
                }
            }

        }
        if (MyGame.Paused)
            return;
        if (LemmingsNetGame.Instance.ScreenInGame.Draw_builder && Builder)
        {
            Actualframe++;
            if (Actualframe > Numframes - 1 && !Explode)
            {
                Actualframe = 0;
            }
        }
        if (LemmingsNetGame.Instance.ScreenInGame.Draw_walker && !Builder && !Basher && !Miner
            && !Burned && !Drown)
        {
            Actualframe++;
            if (Actualframe > Numframes - 1 && !Explode)
            {
                Actualframe = 0;
            }
            //be carefull with bomber frames actualization
        }
        if (LemmingsNetGame.Instance.ScreenInGame.Draw2 && (Basher || Miner
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
                LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
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
                LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
                LemmingsNetGame.Instance.ScreenInGame.NumSaved++;  // here is where the lemming go inside after door animation
            }
            return;
        }
        int arriba = 0;
        _below = 0;
        int pixx = PosX + medx;
        for (int x55 = 0; x55 <= 8; x55++)
        {
            int pos_real = PosY + x55 + medy + medy;  ///////////// pixel por debajo -> beneath.............
            if (pos_real == LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
            {
                _below = 9;
                break;
            }
            if (pos_real < 0)
                pos_real = 0;
            if (pos_real > LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
            {
                Dead = true;
                _below = 9;
                Active = false;
                LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
                Explode = false;
                Exploser = false;
                if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
                {
                    LemmingsNetGame.Instance.Sfx.Die.Stop();
                }
                try
                {
                    LemmingsNetGame.Instance.Sfx.Die.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
                break;
            }
            if (LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx].R == 0 && LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx].G == 0 && LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx].B == 0)
            {
                _below++;
            }
            else
            {
                break;
            }
        }
        // very important to check digger and miner before change to falling
        if (Pixelscaida > MyGame.useumbrella && !Falling && Umbrella
            && (!Digger && !Miner && !Builder) && Active)
        {
            Pixelscaida = 11;
            Falling = true;
            Fall = false;
            Actualframe = 0;
            Numframes = SizeSprites.floater_frames;
        }
        if ((_below > 8 && !Fall && (!Digger || !Miner)) && !Falling
            && !Explode && Active)
        {
            Fall = true;
            Pixelscaida = 0;
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
        if ((_below == 0) && (Fall || Falling) && (!Digger && !Miner)) //OJO LOCO A VECES AL CAVAR Y SIGUE WALKER
        {
            if (Pixelscaida <= MyGame.maxnumberfalling)
            {
                Pixelscaida = 0;
                Framescut = false;
                Falling = false;
                Walker = true;
                Fall = false;
                Actualframe = 0;
                Numframes = SizeSprites.walker_frames;  //8 walker;4 fall;16 digger;breakfloor 16;escala ...
            }
            else
            {
                if (LemmingsNetGame.Instance.Sfx.Splat.State == SoundState.Playing)
                {
                    LemmingsNetGame.Instance.Sfx.Splat.Stop();
                }
                try
                {
                    LemmingsNetGame.Instance.Sfx.Splat.Play();
                }
                catch (InstancePlayLimitException) { /* Ignore errors */ }
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
        if ((_below == 0) && Walker && (!Digger && !Miner))
        {
            Pixelscaida = 0;
        }
        for (int x55 = 0; x55 <= 20; x55++)
        {
            int pos_real = PosY + medy + medy - x55;
            if (pos_real == LemmingsNetGame.Instance.ScreenInGame.Earth.Height)    // rompe los calculos si sale de la pantalla o se cuelga AARRIBBBAAAA
            {
                Active = false;
                break;
            }
            if (pos_real < LemmingsNetGame.Instance.ScreenInGame.Earth.Height && pos_real > 0)
            {
                if (LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx].R > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx].G > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx].B > 0)
                {
                    arriba++;
                }
                else
                {
                    break;
                }
            }
        }
        if (Blocker && _below > 2)
        {
            Blocker = false;
            Fall = true;
            Pixelscaida = 0;
            Actualframe = 0;
            Numframes = SizeSprites.faller_frames;
            return;
        }
        if (Miner && LemmingsNetGame.Instance.ScreenInGame.Draw2 && Actualframe == 42)  // miner logic pico logic
        {
            if (LemmingsNetGame.Instance.ScreenInGame.ArrowsON) // miner arrows logic areaTrap Intersects
            {
                bool nominer = false;
                Rectangle arrowLem;
                arrowLem.X = PosX;
                arrowLem.Y = PosY;
                arrowLem.Width = 28;
                arrowLem.Height = 28;
                for (int wer3 = 0; wer3 < LemmingsNetGame.Instance.ScreenInGame.NumTotArrow; wer3++)
                {
                    if (LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].area.Intersects(arrowLem) && Right && !LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].right)
                    {
                        nominer = true;
                        continue;
                    }
                    if (LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].area.Intersects(arrowLem) && Left && LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].right)
                    {
                        nominer = true;
                    }
                }
                if (nominer)
                {
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
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
                if (px + width2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Height - py;
                }
                LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(LemmingsNetGame.Instance.ScreenInGame.Colormask2);
                //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = LemmingsNetGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                for (int r = 0; r < amount; r++)
                {
                    if (LemmingsNetGame.Instance.ScreenInGame.SteelON)
                    {
                        sx = r % width2;
                        int sy = r / width2;
                        x.X = px + sx;
                        x.Y = py + sy;
                        for (int xz = 0; xz < LemmingsNetGame.Instance.ScreenInGame.NumTOTsteel; xz++)
                        {
                            if (LemmingsNetGame.Instance.ScreenInGame.Steel[xz].area.Contains(x))
                            {
                                sx = -777;
                                break;
                            }
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
                    if (LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[r].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[r].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[r].B > 0)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Frente2++;
                    }
                    if (LemmingsNetGame.Instance.ScreenInGame.Colormask2[r].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[r].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[r].B > 0)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[r].PackedValue = 0;
                    }
                }
                rectangleFill.X = px;
                rectangleFill.Y = py;
                rectangleFill.Width = width2;
                rectangleFill.Height = top2;
                LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
                        amount++;
                    }
                }
                if (sx == -777)
                    return;
                PosX += 12;
                PosY++;
                if (LemmingsNetGame.Instance.ScreenInGame.Frente2 == 0)
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
                if (px + width2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Height - py;
                }
                LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(LemmingsNetGame.Instance.ScreenInGame.Colormask2);
                //////// optimized for hd3000 laptop ARROWS OPTIMIZED
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = LemmingsNetGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                for (int r = 0; r < amount; r++)
                {
                    if (LemmingsNetGame.Instance.ScreenInGame.SteelON)
                    {
                        sx = r % width2;
                        int sy = r / width2;
                        x.X = px + sx;
                        x.Y = py + sy;
                        for (int xz = 0; xz < LemmingsNetGame.Instance.ScreenInGame.NumTOTsteel; xz++)
                        {
                            if (LemmingsNetGame.Instance.ScreenInGame.Steel[xz].area.Contains(x))
                            {
                                sx = -777;
                                break;
                            }
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
                    if (LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount - 1 - r].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount - 1 - r].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount - 1 - r].B > 0)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Frente2++;
                    }
                    if (LemmingsNetGame.Instance.ScreenInGame.Colormask2[amount - 1 - r].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[amount - 1 - r].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[amount - 1 - r].B > 0)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[r].PackedValue = 0;
                    }
                }
                rectangleFill.X = px;
                rectangleFill.Y = py;
                rectangleFill.Width = width2;
                rectangleFill.Height = top2;
                LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
                        amount++;
                    }
                }
                if (sx == -777)
                    return;
                PosX -= 12;
                PosY++;
                if (LemmingsNetGame.Instance.ScreenInGame.Frente2 == 0)
                {
                    Basher = false;
                    Walker = true;
                    Actualframe = 0;
                    Numframes = SizeSprites.walker_frames;
                    return;
                }
            }
            LemmingsNetGame.Instance.ScreenInGame.Frente2 = 0;  /////// PPPPPPPPIIIIIIIIIICCCCCCCCCCCCCCCCCOOOOOOOOOOOOOOOOOOO  BASHER LOGIC puto33
        }

        if (Basher && (Actualframe == 10 || Actualframe == 37) && LemmingsNetGame.Instance.ScreenInGame.Draw2)
        {
            if (LemmingsNetGame.Instance.ScreenInGame.ArrowsON) // basher arrows logic areaTrap Intersects
            {
                bool nobasher = false;
                Rectangle arrowLem;
                arrowLem.X = PosX;
                arrowLem.Y = PosY;
                arrowLem.Width = 28;
                arrowLem.Height = 28;
                for (int wer3 = 0; wer3 < LemmingsNetGame.Instance.ScreenInGame.NumTotArrow; wer3++)
                {
                    if (LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].area.Intersects(arrowLem) && Right && !LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].right)
                    {
                        nobasher = true;
                        continue;
                    }
                    if (LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].area.Intersects(arrowLem) && Left && LemmingsNetGame.Instance.ScreenInGame.Arrow[wer3].right)
                    {
                        nobasher = true;
                    }
                }
                if (nobasher)
                {
                    if (LemmingsNetGame.Instance.Sfx.Ting.State == SoundState.Playing)
                    {
                        LemmingsNetGame.Instance.Sfx.Ting.Stop();
                    }
                    LemmingsNetGame.Instance.Sfx.Ting.Play();
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
                if (px + width2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Height - py;
                }
                LemmingsNetGame.Instance.Gfx.Mascarapared.GetData(LemmingsNetGame.Instance.ScreenInGame.Colormask2);
                //////// optimized for hd3000 laptop
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = LemmingsNetGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                int xEmpty = 0;
                int xErase = width2;
                LemmingsNetGame.Instance.ScreenInGame.Frente = 0;
                sx = 0;
                for (int valX = 0; valX < width2; valX++)
                {
                    LemmingsNetGame.Instance.ScreenInGame.Frente = 0;
                    for (int valY = 0; valY < top2; valY++)
                    {
                        if (LemmingsNetGame.Instance.ScreenInGame.SteelON)
                        {
                            x.X = px + valX;
                            x.Y = py + valY;
                            for (int xz = 0; xz < LemmingsNetGame.Instance.ScreenInGame.NumTOTsteel; xz++)
                            {
                                if (LemmingsNetGame.Instance.ScreenInGame.Steel[xz].area.Contains(x))
                                {
                                    sx = -777;
                                    break;
                                }
                            }
                            if (sx == -777)
                            {
                                Walker = true;
                                Numframes = SizeSprites.walker_frames;
                                Actualframe = 0;
                                Basher = false;
                                break;
                            }
                        }
                        if ((LemmingsNetGame.Instance.ScreenInGame.Colormask2[(valY * width2) + valX].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[(valY * width2) + valX].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[(valY * width2) + valX].B > 0) &&
                            (LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].B > 0))
                        {
                            LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[(valY * width2) + valX].PackedValue = 0;
                            LemmingsNetGame.Instance.ScreenInGame.Frente++;
                        }
                    }
                    if (sx == -777)
                        break;
                    if (LemmingsNetGame.Instance.ScreenInGame.Frente == 0)
                        xEmpty = valX;
                    if (LemmingsNetGame.Instance.ScreenInGame.Frente > 0)
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
                LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
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
                if (px + width2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Width)
                {
                    width2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Width - px;
                }
                if (py + top2 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
                {
                    top2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Height - py;
                }
                int amount = 0;
                for (int yy88 = 0; yy88 < top2; yy88++)
                {
                    int yypos888 = (yy88 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx88 = 0; xx88 < width2; xx88++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue = LemmingsNetGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                        amount++;
                    }
                }
                int xEmpty = width2;
                int xErase = 0;
                LemmingsNetGame.Instance.ScreenInGame.Frente = 0;
                sx = 0;
                for (int valX = width2 - 1; valX >= 0; valX--)
                {
                    LemmingsNetGame.Instance.ScreenInGame.Frente = 0;
                    for (int valY = 0; valY < top2; valY++)
                    {
                        if (LemmingsNetGame.Instance.ScreenInGame.SteelON)
                        {
                            x.X = px + valX;
                            x.Y = py + valY;
                            for (int xz = 0; xz < LemmingsNetGame.Instance.ScreenInGame.NumTOTsteel; xz++)
                            {
                                if (LemmingsNetGame.Instance.ScreenInGame.Steel[xz].area.Contains(x))
                                {
                                    sx = -777;
                                    break;
                                }
                            }
                            if (sx == -777)
                            {
                                Walker = true;
                                Numframes = SizeSprites.walker_frames;
                                Actualframe = 0;
                                Basher = false;
                                break;
                            }
                        }
                        if ((LemmingsNetGame.Instance.ScreenInGame.Colormask2[valY * width2 + valX].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[valY * width2 + valX].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask2[valY * width2 + valX].B > 0) &&
                            (LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].B > 0))
                        {
                            LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[valY * width2 + valX].PackedValue = 0;
                            LemmingsNetGame.Instance.ScreenInGame.Frente++;
                        }
                    }
                    if (sx == -777)
                        break;
                    if (LemmingsNetGame.Instance.ScreenInGame.Frente == 0)
                        xEmpty = valX;
                    if (LemmingsNetGame.Instance.ScreenInGame.Frente > 0)
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
                LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colorsobre2, 0, amount);
                // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
                amount = 0;
                for (int yy33 = 0; yy33 < top2; yy33++)
                {
                    int yypos555 = (yy33 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                    for (int xx33 = 0; xx33 < width2; xx33++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = LemmingsNetGame.Instance.ScreenInGame.Colorsobre2[amount].PackedValue;
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
            LemmingsNetGame.Instance.ScreenInGame.Frente2 = 0;
            ////////////////////////////////////////////////////////////////////// PPPPPPPPPAAAAAAARRRRRRRRRRRRRRRREEEEEEEDDDDDDDDD
        }
        if (Basher && _below > 3)
        {
            Basher = false;
            Walker = true;
            Actualframe = 0;
            Numframes = SizeSprites.walker_frames;
            return;
        }
        if (Builder && LemmingsNetGame.Instance.ScreenInGame.Draw_builder) // BUILDER LOGIC HERE chink sound see limits tooo FIX FIX FIX
        {
            if (Actualframe >= 48 && Numstairs < 12) // >=33 old with dibuja2
            {
                LemmingsNetGame.Instance.ScreenInGame.Frente = 0;
                Actualframe = SizeSprites.builder_frames + 1;  // erase with // no compiling//  to see full frames
                if (Right)
                {
                    if (arriba > 1)
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
                        int posi_real = (PosY + 24 + y) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xx88 = 14; xx88 <= 28; xx88++)
                        {
                            if (LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88].R == 0 && LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88].G == 0 && LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88].B == 0)
                            {
                                Color colorFill = new()
                                {
                                    R = Convert.ToByte(255 - (Numstairs * 5)),
                                    G = 0,
                                    B = Convert.ToByte(255 - (Numstairs * 10)),
                                    A = 255,
                                };
                                LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88] = colorFill;
                            } //steps color stairs
                            else
                            {
                                if (xx88 == 19)
                                    LemmingsNetGame.Instance.ScreenInGame.Frente++;
                            }
                        }
                    }
                    Numstairs++;
                    PosY -= 3;
                    PosX += 6;
                    if (Numstairs >= 10)
                    {
                        if (LemmingsNetGame.Instance.Sfx.Chink.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Chink.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Chink.Play();
                    }
                    int amount = 0;
                    for (int ykk = 27; ykk < 31; ykk++)
                    {
                        int posi_real = (PosY + ykk) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xkk = 0; xkk < 28; xkk++)
                        {
                            LemmingsNetGame.Instance.ScreenInGame.Colormask22[amount] = LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xkk];
                            amount++;
                        }
                    }
                    rectangleFill.X = PosX;
                    rectangleFill.Y = PosY + 27;
                    rectangleFill.Width = 28;
                    rectangleFill.Height = 4;
                    LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colormask22, 0, 28 * 4);
                    if (LemmingsNetGame.Instance.ScreenInGame.Frente == 3)
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
                    if (arriba > 1)
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
                        int posi_real = (PosY + 24 + y) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xx88 = 0; xx88 <= 14; xx88++)
                        {
                            if (LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88].R == 0 && LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88].G == 0 && LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88].B == 0)
                            {
                                Color colorFill = new()
                                {
                                    R = Convert.ToByte(255 - (Numstairs * 5)),
                                    G = 0,
                                    B = Convert.ToByte(255 - (Numstairs * 10)),
                                    A = 255,
                                };
                                LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88] = colorFill;
                            }//magenta stairs
                            else
                            {
                                if (xx88 == 9)
                                    LemmingsNetGame.Instance.ScreenInGame.Frente++;
                            }
                        }
                    }
                    Numstairs++;
                    PosY -= 3;
                    PosX -= 6;
                    if (Numstairs >= 10)
                    {
                        if (LemmingsNetGame.Instance.Sfx.Chink.State == SoundState.Playing)
                        {
                            LemmingsNetGame.Instance.Sfx.Chink.Stop();
                        }
                        LemmingsNetGame.Instance.Sfx.Chink.Play();
                    }
                    //earth.SetData<Color>(c25); //OPTIMIZED BUILDER SETDATA
                    int amount = 0;
                    int px = PosX;
                    if (px < 0)
                        px = 0;
                    for (int ykk = 27; ykk < 31; ykk++)
                    {
                        int posi_real = (PosY + ykk) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width + px;
                        for (int xkk = 0; xkk < 28; xkk++)
                        {
                            LemmingsNetGame.Instance.ScreenInGame.Colormask22[amount] = LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xkk];
                            amount++;
                        }
                    }
                    rectangleFill.X = px;
                    rectangleFill.Y = PosY + 27;
                    rectangleFill.Width = 28;
                    rectangleFill.Height = 4;
                    LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colormask22, 0, 28 * 4);
                    if (LemmingsNetGame.Instance.ScreenInGame.Frente == 3)
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
                Pixelscaida = 0;
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
            if (_below == 0 || _below == 1) // 5 ok que no se aceleren a digger si hay mas de 2 juntos antes era <9 los pixeles debajo de sus pies
            {
                int abajo2 = 0;
                int pixx2 = PosX + 14;
                for (int xx88 = 0; xx88 <= 4; xx88++)
                {
                    int pos_real2 = PosY + xx88 + 28;  ///////////// pixel por debajo.............
                    if (pos_real2 == LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
                    {
                        abajo2 = 9;
                        break;
                    }
                    if (pos_real2 < 0)
                        pos_real2 = 0;
                    if (pos_real2 > LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
                    {
                        pos_real2 = LemmingsNetGame.Instance.ScreenInGame.Earth.Height;
                    }
                    if (LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx2].R > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx2].G > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx2].B > 0)
                    {
                        abajo2++;
                    }
                    else
                    {
                        break;
                    }
                }
                if ((Actualframe == 11 || Actualframe == 26) && LemmingsNetGame.Instance.ScreenInGame.Draw_walker)
                {
                    sx = 0;
                    for (int y = 9; y <= 18; y++)  // 14 es la posicion de los pies del lemming[i].posy porque tiene 28 pixels de alto 28/2=14
                    {
                        int posi_real = (PosY + 14 + y) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width + PosX;
                        if (PosY + 14 + y > LemmingsNetGame.Instance.ScreenInGame.Earth.Height)
                        {
                            break;
                        } // cortar si esta en el limite por debajo 512=earth.height
                        for (int xx88 = 4; xx88 <= 24; xx88++)
                        {
                            if (LemmingsNetGame.Instance.ScreenInGame.SteelON)
                            {
                                x.X = PosX + xx88;
                                x.Y = PosY + 14 + y;
                                for (int xz = 0; xz < LemmingsNetGame.Instance.ScreenInGame.NumTOTsteel; xz++)
                                {
                                    if (LemmingsNetGame.Instance.ScreenInGame.Steel[xz].area.Contains(x))
                                    {
                                        sx = -777;
                                        break;
                                    }
                                }
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
                            LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xx88] = colorFill; // Color.TransparentBlack is the same i think. 0,0,0,0.
                        }
                    }
                    //earth.SetData<Color>(c25); //OPTIMIZED digger SETDATA
                    int amount = 0;
                    for (int ykk = 9; ykk <= 18; ykk++)
                    {
                        int posi_real = (PosY + 14 + ykk) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width + PosX;
                        for (int xkk = 0; xkk < 28; xkk++)
                        {
                            LemmingsNetGame.Instance.ScreenInGame.Colormask22[amount] = LemmingsNetGame.Instance.ScreenInGame.C25[posi_real + xkk];
                            amount++;
                        }
                    }
                    rectangleFill.X = PosX;
                    rectangleFill.Y = PosY + 23;
                    rectangleFill.Width = 28;
                    rectangleFill.Height = 10;
                    LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colormask22, 0, 28 * 10);
                    if (sx == -777)
                        return;
                    PosY += abajo2;
                    return;
                }
            }
            else
            {
                if (PosY + 28 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Height) // erase draws bottom when die and exit level height 21x10
                {
                    for (int ykk = 0; ykk < 210; ykk++)
                    {
                        LemmingsNetGame.Instance.ScreenInGame.Colormask22[ykk].PackedValue = 0;
                    }
                    rectangleFill.Y = 502;
                    rectangleFill.X = PosX + 4;
                    rectangleFill.Width = 21;
                    rectangleFill.Height = 10;
                    LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colormask22, 0, 210);
                }
                Basher = false;
                Builder = false;
                Miner = false;
                Climbing = false;
                Digger = false;
                Fall = true;
                Walker = false;
                Pixelscaida = 0;
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
                Pixelscaida = 0;
                Numframes = SizeSprites.faller_frames;
                Actualframe = 0;
                Builder = false;
                Right = !Right;
                return;
            }
            if (Right)
            {
                int pos_real2 = PosY + 27;
                if (LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx - 2].R > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx - 2].G > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx - 2].B > 0)
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
                if (LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx + 2].R > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx + 2].G > 0 || LemmingsNetGame.Instance.ScreenInGame.C25[(pos_real2 * LemmingsNetGame.Instance.ScreenInGame.Earth.Width) + pixx + 2].B > 0)
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
            if (arriba > 0 && LemmingsNetGame.Instance.ScreenInGame.Dibuja)
            {
                PosY--;
            }
            if (arriba == 0)
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
            if (_below < 3 && Right)
            {
                PosX++;
                if (arriba < 16)
                {
                    PosY -= arriba;
                }
            }  //// <6 o <8 falla cava
            if (_below < 3 && Left)
            {
                PosX--;
                if (arriba < 16)
                {
                    PosY -= arriba;
                }
            }
            if (arriba >= 16)
            {
                if (!Climber)
                {
                    if (Right && arriba >= 16)
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
                    Pixelscaida = 0;
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
            if (px + ancho66 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Width)  // right of the level
            {
                ancho66 = LemmingsNetGame.Instance.ScreenInGame.Earth.Width - px;
            }
            if (py + alto66 >= LemmingsNetGame.Instance.ScreenInGame.Earth.Height) //bottom of the level
            {
                alto66 = LemmingsNetGame.Instance.ScreenInGame.Earth.Height - py;
            }
            int amount = ancho66 * alto66;
            rectangleFill.X = px2;
            rectangleFill.Y = py2;
            rectangleFill.Width = ancho66;
            rectangleFill.Height = alto66;
            LemmingsNetGame.Instance.ScreenMainMenu.MainMenuGfx.Mascaraexplosion.GetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colormask33, 0, amount);
            amount = 0;
            for (int yy88 = 0; yy88 < alto66; yy88++)
            {
                int yypos888 = (yy88 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                for (int xx88 = 0; xx88 < ancho66; xx88++)
                {
                    LemmingsNetGame.Instance.ScreenInGame.Colorsobre33[amount].PackedValue = LemmingsNetGame.Instance.ScreenInGame.C25[yypos888 + px + xx88].PackedValue;
                    amount++;
                }
            }
            for (int r = 0; r < amount; r++)
            {
                if (LemmingsNetGame.Instance.ScreenInGame.SteelON)
                {
                    sx = r % ancho66;
                    int sy = r / ancho66;
                    x.X = px + sx;
                    x.Y = py + sy;
                    for (int xz = 0; xz < LemmingsNetGame.Instance.ScreenInGame.NumTOTsteel; xz++)
                    {
                        if (LemmingsNetGame.Instance.ScreenInGame.Steel[xz].area.Contains(x))
                        {
                            sx = -777;
                            break;
                        }
                    }
                    if (sx == -777)
                        continue;
                }
                if (LemmingsNetGame.Instance.ScreenInGame.Colormask33[r].R > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask33[r].G > 0 || LemmingsNetGame.Instance.ScreenInGame.Colormask33[r].B > 0)
                {
                    LemmingsNetGame.Instance.ScreenInGame.Colorsobre33[r].PackedValue = 0;
                }
            }
            rectangleFill.X = px;
            rectangleFill.Y = py;
            rectangleFill.Width = ancho66;
            rectangleFill.Height = alto66;
            LemmingsNetGame.Instance.ScreenInGame.Earth.SetData(0, rectangleFill, LemmingsNetGame.Instance.ScreenInGame.Colorsobre33, 0, amount);
            // this code update c25 rectangle px-py-ancho66-alto66 only not all like before
            amount = 0;
            for (int yy33 = 0; yy33 < alto66; yy33++)
            {
                int yypos555 = (yy33 + py) * LemmingsNetGame.Instance.ScreenInGame.Earth.Width;
                for (int xx33 = 0; xx33 < ancho66; xx33++)
                {
                    LemmingsNetGame.Instance.ScreenInGame.C25[yypos555 + px + xx33].PackedValue = LemmingsNetGame.Instance.ScreenInGame.Colorsobre33[amount].PackedValue;
                    amount++;
                }
            }
            Dead = true;
            LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
            Explode = false;
            Exploser = false;
            // luto luto sound fix
            if (LemmingsNetGame.Instance.Sfx.Explode.State == SoundState.Playing)
            {
                LemmingsNetGame.Instance.Sfx.Explode.Stop();
            }
            try
            {
                LemmingsNetGame.Instance.Sfx.Explode.Play();
            }
            catch (InstancePlayLimitException) { /* Ignore errors */ }
            //explosions addons emitter - particles logic add
            int xExp = PosX + 14;
            int yExp = PosY + 14;
            LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, 0].MaxCounter = 0;
            LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, 0].Counter = 0;
            for (int Iexplo = 0; Iexplo < MyGame.PARTICLE_NUM; Iexplo++)
            {
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].MaxCounter = 0;
                byte colorr = (byte)MyGame.Rnd.Next(255);
                byte colorg = (byte)MyGame.Rnd.Next(255);
                byte colorb = (byte)MyGame.Rnd.Next(255);
                Color colorFill = new()
                {
                    R = colorr,
                    G = colorg,
                    B = colorb,
                    A = 255,
                };
                int LifeCount = MyGame.LIFE_COUNTER + (int)(MyGame.Rnd.NextDouble() * 2 * MyGame.LIFE_VARIANCE) - MyGame.LIFE_VARIANCE;
                if (LifeCount > LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, 0].MaxCounter)
                    LemmingsNetGame.Instance.Explosion[0, 0].MaxCounter = LifeCount;
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].dx = (MyGame.Rnd.NextDouble() * (SizeSprites.MAX_DX - SizeSprites.MIN_DX) + SizeSprites.MIN_DX);
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].dy = (MyGame.Rnd.NextDouble() * (SizeSprites.MAX_DY - SizeSprites.MIN_DY) + SizeSprites.MIN_DY);
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].x = xExp;
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].y = yExp;
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].Color = colorFill;
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].LifeCtr = LifeCount;
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].Rotation = (float)MyGame.Rnd.NextDouble();
                LemmingsNetGame.Instance.Explosion[LemmingsNetGame.Instance.ScreenInGame.ActItem, Iexplo].Size = (float)(MyGame.Rnd.NextDouble() / 2);
            }
            LemmingsNetGame.Instance.ScreenInGame.Exploding = true;
            LemmingsNetGame.Instance.ScreenInGame.ActItem++;
            if (LemmingsNetGame.Instance.ScreenInGame.ActItem > MyGame.totalExplosions - 1)
                LemmingsNetGame.Instance.ScreenInGame.ActItem = MyGame.totalExplosions - 1;
            return;
        }
        if (!Falling && Active)
        {
            if (_below >= 3)
            {
                PosY += 3;
                Pixelscaida += 3;
            }
            else
            {
                PosY += _below;
                Pixelscaida += _below;
            } // fall 3 MAX---MAX 3 FALL PIXELS
        }
        else
        {
            if (!Drown && LemmingsNetGame.Instance.ScreenInGame.Dibuja)
            {
                if (_below >= 3)
                {
                    PosY += 3;
                }
                else
                {
                    PosY += _below;
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
        if (PosX < -16)// limits of the screen from LEFT
        {
            Active = false;
            Dead = true;
            LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
            Explode = false;
            Exploser = false;
            if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
            {
                LemmingsNetGame.Instance.Sfx.Die.Stop();
            }
            try
            {
                LemmingsNetGame.Instance.Sfx.Die.Play();
            }
            catch (InstancePlayLimitException) { /* Ignore errors */ }
        }
        if (PosX + 14 > LemmingsNetGame.Instance.ScreenInGame.Earth.Width)// limits of the screen from RIGHT
        {
            Active = false;
            Dead = true;
            LemmingsNetGame.Instance.ScreenInGame.Numlemnow--;
            Explode = false;
            Exploser = false;
            if (LemmingsNetGame.Instance.Sfx.Die.State == SoundState.Playing)
            {
                LemmingsNetGame.Instance.Sfx.Die.Stop();
            }
            try
            {
                LemmingsNetGame.Instance.Sfx.Die.Play();
            }
            catch (InstancePlayLimitException) { /* Ignore errors */ }
        }
    }
}
