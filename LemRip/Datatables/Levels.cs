using System;
using System.IO;

using Lemmings.NET.Constants;
using Lemmings.NET.Exceptions;
using Lemmings.NET.Models;
using Lemmings.NET.Structs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal static class Levels
{
    internal static OneLevel GetLevel(int numLevel)
    {
        OneLevel lvl = new();
        if (numLevel == 0)
            return lvl;
        string[] levelContent;
        levelContent = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, MyGame.Instance.Content.RootDirectory, "levels", $"level{numLevel}.ini"));
        string name;
        string value;
        string[] splitter;

        foreach (string line in levelContent)
        {
            splitter = line.Split('=');
            name = splitter[0].Trim().ToLower();
            value = splitter[1].Trim();
            switch (name)
            {
                case "totallemmings":
                    lvl.TotalLemmings = int.Parse(value);
                    break;
                case "namelev":
                    lvl.NameLev = value;
                    break;
                case "numberexploders":
                    lvl.NumberExploders = int.Parse(value);
                    break;
                case "numberblockers":
                    lvl.NumberBlockers = int.Parse(value);
                    break;
                case "numberbuilders":
                    lvl.NumberBuilders = int.Parse(value);
                    break;
                case "doorexitdepth":
                    lvl.DoorExitDepth = float.Parse(value);
                    break;
                case "doorx":
                    lvl.DoorX = int.Parse(value);
                    break;
                case "doory":
                    lvl.DoorY = int.Parse(value);
                    break;
                case "exitx":
                    lvl.ExitX = int.Parse(value);
                    break;
                case "exity":
                    lvl.ExitY = int.Parse(value);
                    break;
                case "frequencycomming":
                    lvl.FrequencyComming = int.Parse(value);
                    break;
                case "înitposx":
                    lvl.InitPosX = int.Parse(value);
                    break;
                case "minfrequencycomming":
                    lvl.MinFrequencyComming = int.Parse(value);
                    break;
                case "nameoflevel":
                    lvl.NameOfLevel = value;
                    break;
                case "nblemmingstosave":
                    lvl.NbLemmingsToSave = int.Parse(value);
                    break;
                case "numberbashers":
                    lvl.NumberBashers = int.Parse(value);
                    break;
                case "numberclimbers":
                    lvl.NumberClimbers = int.Parse(value);
                    break;
                case "numberdiggers":
                    lvl.NumberDiggers = int.Parse(value);
                    break;
                case "numberminers":
                    lvl.NumberMiners = int.Parse(value);
                    break;
                case "numberumbrellas":
                    lvl.NumberUmbrellas = int.Parse(value);
                    break;
                case "totaltime":
                    lvl.TotalTime = int.Parse(value);
                    break;
                case "typeofdoor":
                    lvl.TypeOfDoor = int.Parse(value);
                    break;
                case "typeofexit":
                    lvl.TypeOfExit = int.Parse(value);
                    break;
                case "initposx":
                    lvl.InitPosX = int.Parse(value);
                    break;
                default:
                    throw new LemmingsNetException("Unknown property : " + name);
            }
        }
        return lvl;
    }

    internal static void VariablesTraps()
    {
        switch (MyGame.Instance.CurrentLevelNumber)
        {
            case 1:
                MyGame.Instance.ScreenInGame.Sprite = new Varsprites[6];

                MyGame.Instance.ScreenInGame.Sprite[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisX = 4;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisY = 4;
                MyGame.Instance.ScreenInGame.Sprite[0].Depth = 0.806f;
                MyGame.Instance.ScreenInGame.Sprite[0].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].G = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].B = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].Transparency = 200;
                MyGame.Instance.ScreenInGame.Sprite[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[0].Scale = 9f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[0].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[0].Framesecond = 4;
                MyGame.Instance.ScreenInGame.Sprite[0].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Gfx.MagmaMask;
                MyGame.Instance.ScreenInGame.Sprite[0].MinusScrollX = false;

                MyGame.Instance.ScreenInGame.Sprite[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].AxisX = 8;
                MyGame.Instance.ScreenInGame.Sprite[1].AxisY = 8;
                MyGame.Instance.ScreenInGame.Sprite[1].Depth = 0.406f;
                MyGame.Instance.ScreenInGame.Sprite[1].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].G = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].B = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].Pos = new Vector2(1188, 337); //340
                MyGame.Instance.ScreenInGame.Sprite[1].Scale = 0.35f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[1].Rotation = 0.1f;
                MyGame.Instance.ScreenInGame.Sprite[1].Framesecond = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].Sprite = MyGame.Instance.Content.Load<Texture2D>("sprite/flame");
                MyGame.Instance.ScreenInGame.Sprite[1].MinusScrollX = true;

                MyGame.Instance.ScreenInGame.Sprite[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[2].AxisX = 8;
                MyGame.Instance.ScreenInGame.Sprite[2].AxisY = 8;
                MyGame.Instance.ScreenInGame.Sprite[2].Depth = 0.405f;
                MyGame.Instance.ScreenInGame.Sprite[2].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[2].G = 225;
                MyGame.Instance.ScreenInGame.Sprite[2].B = 225;
                MyGame.Instance.ScreenInGame.Sprite[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[2].Pos = new Vector2(1136, 337);
                MyGame.Instance.ScreenInGame.Sprite[2].Scale = 0.35f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[2].Rotation = 0.05f;
                MyGame.Instance.ScreenInGame.Sprite[2].Framesecond = 0;
                MyGame.Instance.ScreenInGame.Sprite[2].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[2].Sprite = MyGame.Instance.Content.Load<Texture2D>("sprite/flame");
                MyGame.Instance.ScreenInGame.Sprite[2].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[3].AxisX = 6;
                MyGame.Instance.ScreenInGame.Sprite[3].AxisY = 1;
                MyGame.Instance.ScreenInGame.Sprite[3].Depth = 0.405f;
                MyGame.Instance.ScreenInGame.Sprite[3].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[3].G = 225;
                MyGame.Instance.ScreenInGame.Sprite[3].B = 225;
                MyGame.Instance.ScreenInGame.Sprite[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[3].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[3].Scale = 0.5f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[3].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[3].Framesecond = 1;
                MyGame.Instance.ScreenInGame.Sprite[3].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[3].Sprite = MyGame.Instance.Content.Load<Texture2D>("touch/arana");
                MyGame.Instance.ScreenInGame.Sprite[3].Calc = true;
                MyGame.Instance.ScreenInGame.Sprite[3].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[3].Dest = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[3].Speed = 0.578f;  // this field is important for move logic of sprites != 0
                MyGame.Instance.ScreenInGame.Sprite[3].ActVect = 0;
                MyGame.Instance.ScreenInGame.Sprite[3].Center.X = ((MyGame.Instance.ScreenInGame.Sprite[3].Sprite.Width / MyGame.Instance.ScreenInGame.Sprite[3].AxisX) / 2);
                MyGame.Instance.ScreenInGame.Sprite[3].Center.Y = ((MyGame.Instance.ScreenInGame.Sprite[3].Sprite.Height / MyGame.Instance.ScreenInGame.Sprite[3].AxisY) / 2);
                MyGame.Instance.ScreenInGame.Sprite[3].Path = new Vector3[7];
                MyGame.Instance.ScreenInGame.Sprite[3].Path[0] = new Vector3(48, 65, 1.5f);
                MyGame.Instance.ScreenInGame.Sprite[3].Path[1] = new Vector3(200, 140, 1.7f);
                MyGame.Instance.ScreenInGame.Sprite[3].Path[2] = new Vector3(238, 139, 1.9f);
                MyGame.Instance.ScreenInGame.Sprite[3].Path[3] = new Vector3(146, 407, 1.6f);
                MyGame.Instance.ScreenInGame.Sprite[3].Path[4] = new Vector3(326, 475, 2f);
                MyGame.Instance.ScreenInGame.Sprite[3].Path[5] = new Vector3(405, 322, 1.2f);
                MyGame.Instance.ScreenInGame.Sprite[3].Path[6] = new Vector3(470, 211, 1.5f);
                MyGame.Instance.ScreenInGame.Sprite[4].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[4].AxisX = 2;
                MyGame.Instance.ScreenInGame.Sprite[4].AxisY = 10;
                MyGame.Instance.ScreenInGame.Sprite[4].Depth = 0.505f;
                MyGame.Instance.ScreenInGame.Sprite[4].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[4].G = 225;
                MyGame.Instance.ScreenInGame.Sprite[4].B = 225;
                MyGame.Instance.ScreenInGame.Sprite[4].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[4].Pos = new Vector2(120, -190);
                MyGame.Instance.ScreenInGame.Sprite[4].Scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[4].Rotation = 1.57f;
                MyGame.Instance.ScreenInGame.Sprite[4].Framesecond = 2;
                MyGame.Instance.ScreenInGame.Sprite[4].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[4].Sprite = MyGame.Instance.Content.Load<Texture2D>("touch/fire_sprites_other");
                MyGame.Instance.ScreenInGame.Sprite[4].MinusScrollX = false;
                MyGame.Instance.ScreenInGame.Sprite[4].Minus = false;
                MyGame.Instance.ScreenInGame.Sprite[5].Calc = true;
                MyGame.Instance.ScreenInGame.Sprite[5].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[5].AxisX = 6;
                MyGame.Instance.ScreenInGame.Sprite[5].AxisY = 1;
                MyGame.Instance.ScreenInGame.Sprite[5].Depth = 0.405f;
                MyGame.Instance.ScreenInGame.Sprite[5].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[5].G = 225;
                MyGame.Instance.ScreenInGame.Sprite[5].B = 225;
                MyGame.Instance.ScreenInGame.Sprite[5].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[5].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[5].Scale = 0.3f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[5].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[5].Framesecond = 2;
                MyGame.Instance.ScreenInGame.Sprite[5].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[5].Sprite = MyGame.Instance.Content.Load<Texture2D>("touch/arana");
                MyGame.Instance.ScreenInGame.Sprite[5].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[5].Dest = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[5].Speed = 0.578f;  // this field is important for move logic of sprites != 0
                MyGame.Instance.ScreenInGame.Sprite[5].ActVect = 0;
                MyGame.Instance.ScreenInGame.Sprite[5].Center.X = ((MyGame.Instance.ScreenInGame.Sprite[3].Sprite.Width / MyGame.Instance.ScreenInGame.Sprite[3].AxisX) / 2);
                MyGame.Instance.ScreenInGame.Sprite[5].Center.Y = ((MyGame.Instance.ScreenInGame.Sprite[3].Sprite.Height / MyGame.Instance.ScreenInGame.Sprite[3].AxisY) / 2);
                MyGame.Instance.ScreenInGame.Sprite[5].Path = new Vector3[6];
                MyGame.Instance.ScreenInGame.Sprite[5].Path[0] = new Vector3(1000, 5, 1.5f);
                MyGame.Instance.ScreenInGame.Sprite[5].Path[1] = new Vector3(1090, 95, 1.7f);
                MyGame.Instance.ScreenInGame.Sprite[5].Path[2] = new Vector3(1069, 252, 1.9f);
                MyGame.Instance.ScreenInGame.Sprite[5].Path[3] = new Vector3(1173, 300, 1.6f);
                MyGame.Instance.ScreenInGame.Sprite[5].Path[4] = new Vector3(1241, 138, 2f);
                MyGame.Instance.ScreenInGame.Sprite[5].Path[5] = new Vector3(1300, 5, 1.2f);

                break;
            case 4:
                MyGame.Instance.ScreenInGame.Sprite = new Varsprites[1];
                MyGame.Instance.ScreenInGame.Sprite[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisX = 4;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisY = 4;
                MyGame.Instance.ScreenInGame.Sprite[0].Depth = 0.806f;
                MyGame.Instance.ScreenInGame.Sprite[0].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].G = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].B = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].Transparency = 200;
                MyGame.Instance.ScreenInGame.Sprite[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[0].Scale = 9f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[0].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[0].Framesecond = 4;
                MyGame.Instance.ScreenInGame.Sprite[0].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Gfx.MagmaMask;
                MyGame.Instance.ScreenInGame.Sprite[0].MinusScrollX = false;
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(820, 462, 1529 - 820, 40); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(820, 467, 1529 - 820, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 5:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(510, 480, 300, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(510, 485, 300, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Ice;
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2; // kind of variable scroll the trap 1=z1 -- 2=-z1 --
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(518, 460, 280, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(518, 465, 280, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 130;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.Ice;
                break;
            case 6:
            case 47:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(320, 472, 2189 - 320, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(320, 477, 2189 - 320, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 160;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1162 - 159 + 30, 108 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1162, 108);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 7:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(192, 472, 3220 - 192, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(192, 477, 3220 - 192, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 8:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(325, 480, 3500 - 325, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(325, 485, 3500 - 325, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 9:
            case 56:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 462, GlobalConst.GameResolution.X, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 470, GlobalConst.GameResolution.X, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Acid;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(507 - 23, 202 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(507, 202);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(852 - 23, 399 - 38 / 2, 23 * 2, 10); //see .pos
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(852, 399);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(164 - 23, 393 - 38 / 2, 23 * 2, 10);
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(164, 393);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 10:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(380, 490, 1843 - 380, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(380, 495, 1843 - 380, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].G = 150;
                MyGame.Instance.ScreenInGame.Trap[0].B = 20;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 480, 2303, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].G = 20;
                MyGame.Instance.ScreenInGame.Trap[1].B = 20;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 11:
            case 78:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(468, 161, 773 - 468, 440 - 161);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(239, 440, 1227 - 239, 512 - 440);
                break;
            case 13:
            case 101:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 3203, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 477, 3203, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 14:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(180, 480, 2884 - 180, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(180, 485, 2884 - 180, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 15:
            case 61:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(3317 - 5, 455 - 5, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(3317, 455);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 16:
            case 63:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 2205, 512);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 475, 2205, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(845 - 30, 250 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(845, 250);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(895 - 30, 250 - 30 / 2, 30 * 2, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(895, 250);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 17:
            case 66:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 4;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(17).DoorX, GetLevel(17).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(GetLevel(17).DoorX + 220, GetLevel(17).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(GetLevel(17).DoorX + 430, GetLevel(17).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[3].doorMoreXY = new Vector2(GetLevel(17).DoorX + 640, GetLevel(17).DoorY);//IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(261, 373, 1154 - 261, 450 - 373);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(997, 284, 1153 - 997, 370 - 284);
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(391 - 5, 497 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(391, 497);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 31;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 57;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(590 - 5, 497 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(590, 497);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 31;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 57;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(792 - 5, 497 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(792, 497);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 31;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 57;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(998 - 5, 497 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(998, 497);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 31;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 57;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_marble");
                break;
            case 18:
            case 79:
                MyGame.Instance.ScreenInGame.NumTotTraps = 6;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(875 - 30, 454 - 30 / 2, 30 * 2, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(875, 454);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(460 - 30 + 4, 162 - 30 / 2, 30 * 2 - 4, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(460, 162);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(890 - 30 + 4, 158 - 30 / 2, 30 * 2 - 4, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(890, 158);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(587 - 30 + 4, 251 - 30 / 2, 30 * 2 - 4, 10);
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(587, 251);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].areaTrap = new Rectangle(957 - 30 + 4, 312 - 30 / 2, 30 * 2 - 4, 10);
                MyGame.Instance.ScreenInGame.Trap[4].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[4].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].pos = new Vector2(957, 312);
                MyGame.Instance.ScreenInGame.Trap[4].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[4].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[4].depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[4].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].areaTrap = new Rectangle(529 - 30 + 4, 377 - 30 / 2, 30 * 2 - 4, 10);
                MyGame.Instance.ScreenInGame.Trap[5].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[5].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[5].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].pos = new Vector2(529, 377);
                MyGame.Instance.ScreenInGame.Trap[5].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[5].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[5].depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[5].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 20:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(127, 462, 3638 - 127, 40); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(127, 467, 3638 - 127, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Acid;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1121 - 23, 376 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1121, 376);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 21:
            case 116:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 470, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 470, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(1001, 480, 1133 - 1001, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1001, 485, 1133 - 1001, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(3143, 480, 3757 - 3143, 32);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(3143, 485, 3757 - 3143, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(2510 - 5, 473 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(2510, 473);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 16;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                break;
            case 23:
                MyGame.Instance.ScreenInGame.NumTOTexits = 2;
                MyGame.Instance.ScreenInGame.Moreexits = new Varmoreexits[MyGame.Instance.ScreenInGame.NumTOTexits];
                MyGame.Instance.ScreenInGame.Moreexits[0].exitMoreXY = new Vector2(GetLevel(23).ExitX, GetLevel(23).ExitY); //73,460 ----- LEVEL 23 TWO EXITS
                MyGame.Instance.ScreenInGame.Moreexits[1].exitMoreXY = new Vector2(GetLevel(23).ExitX, 180);//73,180 //IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(2475, 472, 2594 - 2475, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(2475, 477, 2594 - 2475, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(606 - 159 + 30, 308 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(606, 308);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 24:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(754, 143, 860 - 754, 216 - 143);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(862, 129, 1304 - 862, 306 - 129);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1477 - 5, 345 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1477, 345);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 47;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 87;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_marble2_fix");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(1071, 225, 1266 - 1071, 40); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1071, 230, 1266 - 1071, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 26:
            case 103:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(2797 - 5, 245 - 5, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(2797, 245);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 480, 3007, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 485, 3007, 10);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 27:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 487, 2624, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 492, 2624, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 28:
            case 95:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(253, 472, 861 - 253, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(253, 477, 861 - 253, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1334 - 159 + 30, 192 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1334, 192);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(2038, 472, 2622 - 2038, 40);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(2038, 477, 2622 - 2038, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(1495, 192 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(1495, 192);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].B = 160;
                MyGame.Instance.ScreenInGame.Trap[3].G = 160;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 29:
            case 99:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(1812, 480, 2205 - 1812, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1812, 485, 2205 - 1812, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.200000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 190;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 30:
            case 114:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(71, 472, 1150 - 71, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(71, 477, 1150 - 71, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 230;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            //tricky levels tricky
            case 31:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3022, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 480, 3022, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 33:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(370, 480, 1479 - 370, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(370, 480, 1479 - 370, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 34:
            case 67:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(1063, 45, 1214 - 1063, 284 - 45);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 190;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(84, 284, 1682 - 84, 330 - 284);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3030, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 3030, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 35:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(728, 480, 3277 - 728, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(728, 485, 3277 - 728, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 36:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 490, 2088, 512); //special size
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 495, 2088, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].B = 130;
                MyGame.Instance.ScreenInGame.Trap[0].G = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(444, 341 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(444, 341);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(1761 - 159 + 30, 251 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(1761, 251);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 37:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 1281, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 1281, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 120;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 38:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 2088, 40); //normal size
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 482, 2088, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].B = 130;
                MyGame.Instance.ScreenInGame.Trap[0].G = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 39:
            case 96:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 2;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(685, 336, 781 - 685, 421 - 336);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Arrow[1].right = false;
                MyGame.Instance.ScreenInGame.Arrow[1].area = new Rectangle(2466, 110, 2577 - 2466, 213 - 110); // mask texture full steel zone
                MyGame.Instance.ScreenInGame.Arrow[1].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[1].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[1].area.Width, MyGame.Instance.ScreenInGame.Arrow[1].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[1].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[1].transparency = 255;
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3153, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 3153, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 120;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(553, 216, 2775 - 553, 330 - 216);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(2698, 144, 2851 - 2698, 216 - 144);
                break;
            case 40:
            case 105:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(40).DoorX, GetLevel(40).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(2240, GetLevel(40).DoorY);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 2853, 40); //normal size
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 482, 2853, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].B = 80;
                MyGame.Instance.ScreenInGame.Trap[0].G = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 41:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(1027, 480, 1553 - 1027, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1027, 485, 1553 - 1027, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 43:
            case 115:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 441, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 441, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 49:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 490, 3391, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 495, 3391, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 50:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(1318, 164, 1478 - 1318, 460 - 164);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.NumTotTraps = 5;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(217, 411 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(217, 411);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(754 - 30, 160 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(754, 160);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(801 - 30, 160 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(801, 160);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(1770 - 30, 169 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(1770, 169);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].areaTrap = new Rectangle(1821 - 30, 169 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[4].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[4].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].pos = new Vector2(1821, 169);
                MyGame.Instance.ScreenInGame.Trap[4].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[4].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[4].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 52:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(384, 480, 3197 - 384, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(384, 485, 3197 - 384, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.4000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 480, 278, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 485, 278, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.4000000011f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(3197, 480, 3649 - 3197, 32);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(3197, 485, 3649 - 3197, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 55:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(2511 - 5, 334 - 5, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(2511, 334);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(1300, 480, 4089 - 1300, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1300, 485, 4089 - 1300, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 480, 787, 32);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(0, 485, 787, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 57:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(1306, 35, 1805 - 1306, 318 - 35);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 777, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 777, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 58:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3117, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 3117, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(1780, 239, 2006 - 1780, 280 - 239);
                break;
            case 59:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(59).DoorX, GetLevel(59).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(459, 150);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(770, 46);
                MyGame.Instance.ScreenInGame.NumTotTraps = 7;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(623 - 23, 429 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(623, 429);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1266 - 23, 451 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1266, 451);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(1591 - 23, 132 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(1591, 132);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(1604 - 23, 367 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(1604, 367);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].areaTrap = new Rectangle(2224 - 23, 153 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[4].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[4].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[4].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].pos = new Vector2(2224, 153);
                MyGame.Instance.ScreenInGame.Trap[4].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[4].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[4].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].areaTrap = new Rectangle(2154 - 23, 332 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[5].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[5].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[5].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].pos = new Vector2(2154, 332);
                MyGame.Instance.ScreenInGame.Trap[5].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[5].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[5].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[5].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[6].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[6].areaTrap = new Rectangle(2763 - 23, 334 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[6].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[6].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[6].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[6].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[6].pos = new Vector2(2763, 334);
                MyGame.Instance.ScreenInGame.Trap[6].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[6].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[6].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[6].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[6].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 60:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3090, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 3090, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 224;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            // TAXING LEVELS TAXING //////////////////////////////////////////////////////////////////////////
            case 62:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(62).DoorX, GetLevel(62).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(1962, GetLevel(62).DoorY);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(1614, 129, 1967 - 1614, 253 - 129);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(2352 - 5, 157 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(2352, 157);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 16;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1052 - 5, 459 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1052, 459);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 16;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(2712 - 5, 134 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 12;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(2712, 134);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 40;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 105;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_10");
                break;
            case 64:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(64).DoorX, GetLevel(64).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(1174, GetLevel(64).DoorY);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(1000, 272, 1507 - 1000, 318 - 272);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(1265, 319, 1309 - 1265, 442 - 319);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1003 - 12, 175 - 18, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1003, 175);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 26;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_left");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(908, 272 - 18, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(908, 272);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 26;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_right");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(1574 - 5, 445 - 5, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(1574, 445);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 10;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 71;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 65:
                MyGame.Instance.ScreenInGame.NumTOTexits = 2;
                MyGame.Instance.ScreenInGame.Moreexits = new Varmoreexits[MyGame.Instance.ScreenInGame.NumTOTexits];
                MyGame.Instance.ScreenInGame.Moreexits[0].exitMoreXY = new Vector2(GetLevel(65).ExitX, GetLevel(65).ExitY);
                MyGame.Instance.ScreenInGame.Moreexits[1].exitMoreXY = new Vector2(GetLevel(65).ExitX, 461);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(161, 436, 390 - 161, 488 - 436);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(537, 160, 610 - 537, 309 - 160);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(611, 196 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(611, 196);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(611, 274 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(611, 274);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[2].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 472, 1152, 40);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(0, 477, 1152, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 68:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3405, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 3405, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 69:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 771, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 771, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(1448, 480, 2304 - 1448, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1448, 485, 2304 - 1448, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.400000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 70:
                MyGame.Instance.ScreenInGame.NumTotTraps = 5;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(439, 342 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(439, 342);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1767 - 159 + 30, 217 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1767, 217);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                MyGame.Instance.ScreenInGame.Trap[2].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 472, 1275, 40);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(0, 477, 1275, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[3].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(1435, 472, 2090 - 1435, 40);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(1435, 477, 2090 - 1435, 10);
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 1;
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[4].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[4].areaDraw = new Rectangle(1281, 482, 1429 - 1281, 40);
                MyGame.Instance.ScreenInGame.Trap[4].areaTrap = new Rectangle(1291, 497, 1, 1); //null kill area
                MyGame.Instance.ScreenInGame.Trap[4].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].type = 1;
                MyGame.Instance.ScreenInGame.Trap[4].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[4].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[4].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[4].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 71:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3604, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 3604, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 72:
                MyGame.Instance.ScreenInGame.NumTotTraps = 6;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 3000, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 477, 3000, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(752, 205 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(752, 205);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(1336 - 30, 178 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(1336, 178);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(1386 - 30, 178 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(1386, 178);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].areaTrap = new Rectangle(1642 - 30, 119 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[4].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[4].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].pos = new Vector2(1642, 119);
                MyGame.Instance.ScreenInGame.Trap[4].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[4].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[4].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].areaTrap = new Rectangle(1681 - 30, 119 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[5].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[5].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[5].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].pos = new Vector2(1691, 119);
                MyGame.Instance.ScreenInGame.Trap[5].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[5].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[5].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[5].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 73:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 2;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(1737, 121, 1932 - 1737, 326 - 121);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow2");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Arrow[1].right = true;
                MyGame.Instance.ScreenInGame.Arrow[1].area = new Rectangle(478, 42, 631 - 478, 374 - 42); // mask texture full steel zone
                MyGame.Instance.ScreenInGame.Arrow[1].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[1].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[1].area.Width, MyGame.Instance.ScreenInGame.Arrow[1].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[1].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[1].transparency = 255;
                break;
            case 74:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 4002, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 4002, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 150;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 76:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(994, 472, 2544 - 994, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(994, 477, 2544 - 994, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].R = 140;
                MyGame.Instance.ScreenInGame.Trap[0].G = 120;
                MyGame.Instance.ScreenInGame.Trap[0].B = 190;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 77:
                MyGame.Instance.ScreenInGame.NumTOTexits = 2;
                MyGame.Instance.ScreenInGame.Moreexits = new Varmoreexits[MyGame.Instance.ScreenInGame.NumTOTexits];
                MyGame.Instance.ScreenInGame.Moreexits[0].exitMoreXY = new Vector2(GetLevel(77).ExitX, GetLevel(77).ExitY);
                MyGame.Instance.ScreenInGame.Moreexits[1].exitMoreXY = new Vector2(GetLevel(77).ExitX, 180);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(2475, 472, 2594 - 2475, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(2475, 477, 2594 - 2475, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(606 - 159 + 30, 308 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(606, 308);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 81:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(963 - 159 + 30, 358 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(963, 358);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(487, 358 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(487, 358);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 83:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 1281, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 477, 1281, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 84:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 3283, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 477, 3283, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 86:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(86).DoorX, GetLevel(86).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(930, 382);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(500, 56);
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(297, 472, 836 - 297, 512);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(297, 475, 836 - 297, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(930 - 30, 128 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(930, 128);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(972 - 30, 128 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(972, 128);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(88, 129 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(88, 129);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 87:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 2083, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 2083, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.300000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 230;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Ice;
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(102, 126 - 32, 426 - 102, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(1640, 126 - 32, 1965 - 1640, 32);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 88:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(2425 - 5, 238 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(2425, 238);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 33;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 25;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_laser");
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 480, 3517, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 485, 3517, 10);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 89:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(881, 480, 1481 - 881, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(881, 485, 1481 - 881, 10);// not necessary
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 90:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 3207, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 477, 3207, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            // MAYHEM LEVELS MAYHEM /////////////////////////////////////////////////////////////////////
            case 92:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(830, 219 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(830, 219);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(1318, 472, 1800 - 1318, 40);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1318, 477, 1800 - 1318, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 93:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(754, 143, 860 - 754, 216 - 143);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(862, 129, 1304 - 862, 306 - 129);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1477 - 5, 345 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1477, 345);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 47;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 87;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_marble2_fix");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(1071, 233, 1266 - 1071, 32); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1071, 238, 1266 - 1071, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 97:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(127, 480, 3638 - 127, 32); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(127, 485, 3638 - 127, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1121 - 23, 376 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1121, 376);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 98:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(1006, 50, 1166 - 1006, 393 - 50);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(1006, 398, 1105 - 1006, 512 - 398);
                break;
            case 102:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 7;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(912, 46, 986 - 912, 380 - 46);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(605, 231, 1295 - 605, 306 - 231);
                MyGame.Instance.ScreenInGame.Steel[2].area = new Rectangle(449, 153, 676 - 449, 229 - 153);
                MyGame.Instance.ScreenInGame.Steel[3].area = new Rectangle(66, 155, 370 - 66, 228 - 155);
                MyGame.Instance.ScreenInGame.Steel[4].area = new Rectangle(255, 306, 337 - 255, 379 - 306);
                MyGame.Instance.ScreenInGame.Steel[5].area = new Rectangle(104, 303, 180 - 104, 381 - 303);
                MyGame.Instance.ScreenInGame.Steel[6].area = new Rectangle(909, 438, 986 - 909, 512 - 428);
                break;
            case 104:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 3;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(742, 398, 1566 - 742, 467 - 398);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(579, 323, 742 - 579, 440 - 323);
                MyGame.Instance.ScreenInGame.Steel[2].area = new Rectangle(1447, 324, 1651 - 1447, 399 - 324);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(661, 330, 1576 - 661, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(661, 335, 1576 - 661, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Acid;
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(661, 350, 1576 - 661, 40);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(661, 355, 1576 - 661, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 106:
            case 117:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(334 - 23, 303 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(334, 303);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1484 - 23, 358 - 38 / 2, 23 * 2, 10); //see .pos
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1484, 358);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 107:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 2187, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 2187, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 108:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 4;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(108).DoorX, GetLevel(108).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(1500, GetLevel(108).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(GetLevel(108).DoorX, 376);
                MyGame.Instance.ScreenInGame.MoreDoors[3].doorMoreXY = new Vector2(1500, 376);
                break;
            case 113:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(1341, 480, 2508 - 1341, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1341, 485, 2508 - 1341, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 118:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1975 - 5, 285 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1975, 285);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 47;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 87;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_marble2_fix");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 480, 3922, 32); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 485, 3922, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 120:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(120).DoorX, GetLevel(120).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(4094, GetLevel(120).DoorY);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1589 - 5, 434 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1589, 434);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 16;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(368 - 5, 474 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 12;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(368, 474);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 40;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 105;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_10");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(3301 - 5, 453 - 5, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 12;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(3301, 453);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 40;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 105;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_10");
                break;
            //bonus levels bonus ////////////////////////////////////////////////////////////////////////////////////////
            case 124:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1341 - 23, 324 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1341, 324);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(936 - 23, 355 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(936, 355);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 126:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 2;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(605, 0, 700 - 605, 245);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Arrow[1].right = false;
                MyGame.Instance.ScreenInGame.Arrow[1].area = new Rectangle(952, 0, 1047 - 952, 245); // mask texture full steel zone
                MyGame.Instance.ScreenInGame.Arrow[1].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow2");
                MyGame.Instance.ScreenInGame.Arrow[1].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[1].area.Width, MyGame.Instance.ScreenInGame.Arrow[1].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[1].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[1].transparency = 255;
                break;
            case 127:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 2148, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 2148, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.300000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Ice;
                break;
            case 130:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1016 - 5, 213 - 5, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1016, 213);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 132:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 3909, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 3909, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.300000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.Ice;
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 1; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 485, 3909, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 490, 3909, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.Ice;
                break;
            case 133:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(395, 472, 1647 - 395, 40);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(395, 477, 1647 - 395, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                break;
            case 134:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(134).DoorX, GetLevel(134).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(160, 92);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1080 - 12, 406 - 18, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1080, 406);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 26;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_left");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(144, 118 - 18, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(144, 118);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 26;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_arrow_right");
                break;
            case 136:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(172, 490, 1843 - 172, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(172, 495, 1843 - 172, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].R = 20;
                MyGame.Instance.ScreenInGame.Trap[0].B = 20;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 480, 2049, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].R = 20;
                MyGame.Instance.ScreenInGame.Trap[1].B = 20;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 137:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1146 - 159 + 30, 235 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1146, 235);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 138:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(138).DoorX, GetLevel(138).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(GetLevel(138).DoorX - 300, GetLevel(138).DoorY);
                //moredoors[1].doormorexy = new Vector2(1110,220); TEST THIS OPTION -- BASHER TO LEFT FAILS??????
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].area = new Rectangle(961, 219, 1011 - 961, 409 - 219);
                MyGame.Instance.ScreenInGame.Arrow[0].flechas = MyGame.Instance.Content.Load<Texture2D>("fondos/arrow1");
                MyGame.Instance.ScreenInGame.Arrow[0].flechassobre = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.ScreenInGame.Arrow[0].area.Width, MyGame.Instance.ScreenInGame.Arrow[0].area.Height);
                MyGame.Instance.ScreenInGame.Arrow[0].desplaza = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].transparency = 255;
                break;
            case 139:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(2208 - 5, 430 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(2208, 430);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 16;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(2843 - 5, 430 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(2843, 430);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 16;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_trampa");
                break;
            case 140:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(133, 155 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(133, 155);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                break;
            case 141:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(141).DoorX, GetLevel(141).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(GetLevel(141).DoorX + 400, GetLevel(141).DoorY);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(671 - 23, 184 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(671, 184);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 143:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 1632, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 1632, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 144:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(134, 326);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(GetLevel(144).DoorX, GetLevel(144).DoorY);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(624 - 23, 461 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(624, 461);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 145:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(974 - 23, 229 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(974, 229);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(110 - 23, 357 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(110, 357);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(970 - 23, 357 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(970, 357);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 147:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(79, 480, 2489 - 79, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(79, 485, 2489 - 79, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].G = 130;
                MyGame.Instance.ScreenInGame.Trap[0].R = 130;
                MyGame.Instance.ScreenInGame.Trap[0].B = 180;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 149:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, 1794, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, 1794, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1022 - 23, 301 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1022, 301);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 150:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(690 - 5, 467 - 5, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(690, 467);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_soga");

                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1328 - 5, 427 - 5, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1328, 427);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 10;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 71;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_soga");
                break;
            case 151:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 2177, 512);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 475, 2177, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1113 - 30, 225 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1113, 225);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(1163 - 30, 225 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(1163, 225);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                break;
            case 152:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 472, 1493, 512);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 475, 1493, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego1");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(990 - 30, 336 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(990, 336);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(1035 - 30, 336 - 30 / 2, 30 * 2, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(1035, 336);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(638 - 159 + 30, 24 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(638, 24);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 153:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 490, 1158, 32); //size smaller to fit
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 492, 1158, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(883 - 23, 396 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(883, 396);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 154:
                MyGame.Instance.ScreenInGame.NumTotTraps = 9;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(459 - 23, 231 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(459, 231);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(935 - 23, 182 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(935, 182);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].areaTrap = new Rectangle(1574 - 23, 129 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = new Vector2(1574, 129);
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].areaTrap = new Rectangle(2095 - 23, 21 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = new Vector2(2095, 21);
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].areaTrap = new Rectangle(40 - 23, 255 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[4].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[4].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[4].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].pos = new Vector2(40, 255);
                MyGame.Instance.ScreenInGame.Trap[4].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[4].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[4].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].areaTrap = new Rectangle(507 - 23, 52 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[5].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[5].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[5].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].pos = new Vector2(507, 52);
                MyGame.Instance.ScreenInGame.Trap[5].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[5].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[5].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[5].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[6].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[6].areaTrap = new Rectangle(1272 - 23, 205 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[6].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[6].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[6].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[6].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[6].pos = new Vector2(1272, 205);
                MyGame.Instance.ScreenInGame.Trap[6].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[6].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[6].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[6].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[6].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[7].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[7].areaTrap = new Rectangle(1963 - 23, 307 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[7].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[7].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[7].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[7].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[7].pos = new Vector2(1963, 307);
                MyGame.Instance.ScreenInGame.Trap[7].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[7].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[7].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[7].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[7].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                MyGame.Instance.ScreenInGame.Trap[8].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[8].areaTrap = new Rectangle(2192 - 23, 460 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[8].numFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[8].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[8].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[8].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[8].pos = new Vector2(2192, 460);
                MyGame.Instance.ScreenInGame.Trap[8].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[8].vvY = 38;
                MyGame.Instance.ScreenInGame.Trap[8].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[8].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[8].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_spin");
                break;
            case 159:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 4;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(159).DoorX, GetLevel(159).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(559, 131);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(96, 327);
                MyGame.Instance.ScreenInGame.MoreDoors[3].doorMoreXY = new Vector2(1033, 419);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(1172 - 5, 381 - 5, 10, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 9;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 666;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(1172, 381);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 48;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_almeja");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1176 - 5, 269 - 5, 10, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 29;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1176, 269);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 120;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_bombona");
                MyGame.Instance.ScreenInGame.Sprite = new Varsprites[2];
                MyGame.Instance.ScreenInGame.Sprite[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisX = 1;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisY = 1;
                MyGame.Instance.ScreenInGame.Sprite[0].Depth = 0.20888886f;
                MyGame.Instance.ScreenInGame.Sprite[0].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].G = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].B = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].Transparency = 200;
                MyGame.Instance.ScreenInGame.Sprite[0].Pos = new Vector2(100, 100);
                MyGame.Instance.ScreenInGame.Sprite[0].Scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[0].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[0].Framesecond = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Content.Load<Texture2D>("sprite/nube1");
                MyGame.Instance.ScreenInGame.Sprite[0].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[0].Typescroll = 3f;
                MyGame.Instance.ScreenInGame.Sprite[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].AxisX = 1;
                MyGame.Instance.ScreenInGame.Sprite[1].AxisY = 1;
                MyGame.Instance.ScreenInGame.Sprite[1].Depth = 0.28888805f;
                MyGame.Instance.ScreenInGame.Sprite[1].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].G = 225;
                MyGame.Instance.ScreenInGame.Sprite[1].B = 225;
                MyGame.Instance.ScreenInGame.Sprite[1].Transparency = 200;
                MyGame.Instance.ScreenInGame.Sprite[1].Pos = new Vector2(300, 300);
                MyGame.Instance.ScreenInGame.Sprite[1].Scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[1].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[1].Framesecond = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].Sprite = MyGame.Instance.Content.Load<Texture2D>("sprite/nube2");
                MyGame.Instance.ScreenInGame.Sprite[1].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[1].Typescroll = 2;
                break;
            case 160:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(160).DoorX, GetLevel(160).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(1280, 462);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(357, 333 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = new Vector2(357, 333);
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego3");
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(64 - 159 + 30, 333 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(64, 333);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/fuego4");
                break;
            case 161:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(892, 862, 1120 - 892, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(892, 867, 1120 - 892, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1451 - 5, 469 - 5, 10, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 29;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 666;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = new Vector2(1451, 469);
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 120;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Content.Load<Texture2D>("traps/dead_bombona");
                break;
            case 162:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(162).DoorX, GetLevel(162).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(GetLevel(162).DoorX + 180, GetLevel(162).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(GetLevel(162).DoorX, 345);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(458, 0, 501 - 458, 319);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(145, 269, 277 - 145, 320 - 269);
                break;
            case 163:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(163).DoorX, GetLevel(163).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(GetLevel(163).DoorX, 220);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(GetLevel(163).DoorX, 382);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(853, 504, 1932 - 853, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(853, 509, 1932 - 853, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(624, 483 - 32, 672 - 624, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(624, 483 - 27, 672 - 624, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Sprite = new Varsprites[3];
                MyGame.Instance.ScreenInGame.Sprite[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisX = 1;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisY = 7;
                MyGame.Instance.ScreenInGame.Sprite[0].Depth = 0.406f;
                MyGame.Instance.ScreenInGame.Sprite[0].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].G = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].B = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].Pos = new Vector2(404, 295); //340
                MyGame.Instance.ScreenInGame.Sprite[0].Scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[0].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[0].Framesecond = 4;
                MyGame.Instance.ScreenInGame.Sprite[0].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Content.Load<Texture2D>("antorcha_l2");
                MyGame.Instance.ScreenInGame.Sprite[0].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].AxisX = 1;
                MyGame.Instance.ScreenInGame.Sprite[1].AxisY = 7;
                MyGame.Instance.ScreenInGame.Sprite[1].Depth = 0.406f;
                MyGame.Instance.ScreenInGame.Sprite[1].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].G = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].B = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[1].Pos = new Vector2(1615, 387); //340
                MyGame.Instance.ScreenInGame.Sprite[1].Scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[1].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[1].Framesecond = 2;
                MyGame.Instance.ScreenInGame.Sprite[1].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[1].Sprite = MyGame.Instance.Content.Load<Texture2D>("antorcha_l2");
                MyGame.Instance.ScreenInGame.Sprite[1].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[2].AxisX = 1;
                MyGame.Instance.ScreenInGame.Sprite[2].AxisY = 7;
                MyGame.Instance.ScreenInGame.Sprite[2].Depth = 0.405f;
                MyGame.Instance.ScreenInGame.Sprite[2].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[2].G = 225;
                MyGame.Instance.ScreenInGame.Sprite[2].B = 225;
                MyGame.Instance.ScreenInGame.Sprite[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[2].Pos = new Vector2(1095, 92);
                MyGame.Instance.ScreenInGame.Sprite[2].Scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[2].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[2].Framesecond = 6;
                MyGame.Instance.ScreenInGame.Sprite[2].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[2].Sprite = MyGame.Instance.Content.Load<Texture2D>("antorcha_l2");
                MyGame.Instance.ScreenInGame.Sprite[2].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(315, 270, 356 - 315, 320 - 270);
                break;
            case 165:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 480, GlobalConst.GameResolution.X, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 485, GlobalConst.GameResolution.X, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(511, 242, 552 - 511, 346 - 242);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(735, 243, 776 - 735, 291 - 243);
                break;
            case 166:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 485, 1882, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(0, 490, 1882, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 504, 1882, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 509, 1882, 10);
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(224, 321, 1835 - 224, 373 - 321);
                break;
            case 167:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(580, 456 - 32, 806 - 580, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(580, 456 - 27, 806 - 580, 10);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(1209, 536 - 32, 1391 - 1209, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(1209, 536 - 27, 1391 - 1209, 10);
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 5;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(539, 55, 581 - 539, 480 - 55);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(808, 268, 848 - 808, 479 - 268);
                MyGame.Instance.ScreenInGame.Steel[2].area = new Rectangle(1165, 376, 1208 - 1165, 536 - 376);
                MyGame.Instance.ScreenInGame.Steel[3].area = new Rectangle(1389, 324, 1432 - 1389, 536 - 324);
                MyGame.Instance.ScreenInGame.Steel[4].area = new Rectangle(270, 430, 539 - 270, 479 - 430);
                break;
            case 168:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 3;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(1389, 81, 1429 - 1389, 454 - 81);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(1432, 215, 1565 - 1432, 262 - 215);
                MyGame.Instance.ScreenInGame.Steel[2].area = new Rectangle(1702, 188, 2281 - 1702, 239 - 188);
                break;
            case 169:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 6;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(1479, 107, 1655 - 1479, 212 - 107);
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(1434, 162, 1476 - 1434, 266 - 162);
                MyGame.Instance.ScreenInGame.Steel[2].area = new Rectangle(1390, 215, 1431 - 1390, 319 - 215);
                MyGame.Instance.ScreenInGame.Steel[3].area = new Rectangle(1345, 270, 1386 - 1345, 424 - 270);
                MyGame.Instance.ScreenInGame.Steel[4].area = new Rectangle(1300, 322, 1342 - 1300, 373 - 322);
                MyGame.Instance.ScreenInGame.Steel[5].area = new Rectangle(1165, 482, 1655 - 1165, 532 - 482);
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(0, 515 - 32, 3091, 32);
                MyGame.Instance.ScreenInGame.Trap[0].vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600005f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(0, 536 - 32, 3091, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(0, 536 - 27, 3091, 10);
                MyGame.Instance.ScreenInGame.Trap[1].vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].areaDraw = new Rectangle(1028, 483 - 32, 1165 - 1028, 32);
                MyGame.Instance.ScreenInGame.Trap[2].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[3].areaDraw = new Rectangle(1387, 375 - 32, 1613 - 1387, 32);
                MyGame.Instance.ScreenInGame.Trap[3].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].type = 1;
                MyGame.Instance.ScreenInGame.Trap[3].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[3].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[3].depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 170:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].areaDraw = new Rectangle(402, 447 - 32, 987 - 402, 32);
                MyGame.Instance.ScreenInGame.Trap[0].areaTrap = new Rectangle(402, 447 - 27, 987 - 402, 10);
                MyGame.Instance.ScreenInGame.Trap[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].depth = 0.600005f;
                MyGame.Instance.ScreenInGame.Trap[0].transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].areaDraw = new Rectangle(222, 178 - 32, 583 - 222, 32);
                MyGame.Instance.ScreenInGame.Trap[1].areaTrap = new Rectangle(222, 178 - 27, 583 - 222, 10);
                MyGame.Instance.ScreenInGame.Trap[1].numFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].actFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].isOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].vvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].vvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].depth = 0.600005f;
                MyGame.Instance.ScreenInGame.Trap[1].transparency = 190;
                MyGame.Instance.ScreenInGame.Trap[1].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 171:
                MyGame.Instance.ScreenInGame.NumTOTplats = 2;
                MyGame.Instance.ScreenInGame.PlatsON = true;
                MyGame.Instance.ScreenInGame.Plats = new Varplat[MyGame.Instance.ScreenInGame.NumTOTplats];
                MyGame.Instance.ScreenInGame.Plats[0].actStep = 0;
                MyGame.Instance.ScreenInGame.Plats[0].framesecond = 8;
                MyGame.Instance.ScreenInGame.Plats[0].frame = 0;
                MyGame.Instance.ScreenInGame.Plats[0].numSteps = 22;
                MyGame.Instance.ScreenInGame.Plats[0].up = true;
                MyGame.Instance.ScreenInGame.Plats[0].step = 5;
                MyGame.Instance.ScreenInGame.Plats[0].areaDraw = new Rectangle(710, 1110, 220, 60);
                MyGame.Instance.ScreenInGame.Plats[0].sprite = MyGame.Instance.Gfx.TrapElevator;
                MyGame.Instance.ScreenInGame.Plats[1].actStep = 0;
                MyGame.Instance.ScreenInGame.Plats[1].framesecond = 0;
                MyGame.Instance.ScreenInGame.Plats[1].frame = 0;
                MyGame.Instance.ScreenInGame.Plats[1].numSteps = 30;
                MyGame.Instance.ScreenInGame.Plats[1].up = true;
                MyGame.Instance.ScreenInGame.Plats[1].step = 1;
                MyGame.Instance.ScreenInGame.Plats[1].areaDraw = new Rectangle(528, 1216, 200, 35);
                MyGame.Instance.ScreenInGame.Plats[1].sprite = MyGame.Instance.Gfx.TrapElevator;
                MyGame.Instance.ScreenInGame.AddsON = true;
                MyGame.Instance.ScreenInGame.Adds = new Varadds[1];
                MyGame.Instance.ScreenInGame.Adds[0].areaDraw = new Rectangle(250, 1271, 100, 50); //y 110 orig
                MyGame.Instance.ScreenInGame.Adds[0].numFrames = 8;
                MyGame.Instance.ScreenInGame.Adds[0].actFrame = 0;
                MyGame.Instance.ScreenInGame.Adds[0].frame = 0;
                MyGame.Instance.ScreenInGame.Adds[0].framesecond = 2;
                MyGame.Instance.ScreenInGame.Adds[0].sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 172:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 26;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(464 + 16, 82 + 16, 534 - 464, 160 - 82);  // add +16 on x & y only first for mouse sprite fix on "F1"
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(608 + 16, 109 + 16, 676 - 608, 185 - 109);
                MyGame.Instance.ScreenInGame.Steel[2].area = new Rectangle(370 + 16, 490 + 16, 745 - 370, 495 - 490);
                MyGame.Instance.ScreenInGame.Steel[3].area = new Rectangle(471 + 16, 437 + 16, 539 - 471, 491 - 437);
                MyGame.Instance.ScreenInGame.Steel[4].area = new Rectangle(349 + 16, 286 + 16, 414 - 349, 364 - 286);
                MyGame.Instance.ScreenInGame.Steel[5].area = new Rectangle(462 + 16, 222 + 16, 528 - 462, 299 - 222);
                MyGame.Instance.ScreenInGame.Steel[6].area = new Rectangle(303 + 16, 150 + 16, 372 - 303, 228 - 150);
                MyGame.Instance.ScreenInGame.Steel[7].area = new Rectangle(709 + 16, 329 + 16, 911 - 709, 410 - 329);
                MyGame.Instance.ScreenInGame.Steel[8].area = new Rectangle(675 + 16, 465 + 16, 777 - 675, 488 - 465);
                MyGame.Instance.ScreenInGame.Steel[9].area = new Rectangle(169 + 16, 249 + 16, 268 - 169, 407 - 249);
                MyGame.Instance.ScreenInGame.Steel[10].area = new Rectangle(172 + 16, 117 + 16, 234 - 172, 247 - 117);
                MyGame.Instance.ScreenInGame.Steel[11].area = new Rectangle(830 + 16, 71 + 16, 965 - 830, 120 - 71);
                MyGame.Instance.ScreenInGame.Steel[12].area = new Rectangle(594 + 16, 267 + 16, 627 - 594, 374 - 267);
                MyGame.Instance.ScreenInGame.Steel[13].area = new Rectangle(813 + 16, 278 + 16, 941 - 813, 326 - 278);
                MyGame.Instance.ScreenInGame.Steel[14].area = new Rectangle(763 + 16, 148 + 16, 832 - 763, 197 - 148);
                MyGame.Instance.ScreenInGame.Steel[15].area = new Rectangle(797 + 16, 121 + 16, 865 - 797, 146 - 121);
                MyGame.Instance.ScreenInGame.Steel[16].area = new Rectangle(526 + 16, 349 + 16, 593 - 526, 401 - 349);
                MyGame.Instance.ScreenInGame.Steel[17].area = new Rectangle(731 + 16, 179 + 16, 763 - 731, 230 - 179);
                MyGame.Instance.ScreenInGame.Steel[18].area = new Rectangle(698 + 16, 214 + 16, 730 - 698, 264 - 214);
                MyGame.Instance.ScreenInGame.Steel[19].area = new Rectangle(663 + 16, 228 + 16, 699 - 663, 277 - 228);
                MyGame.Instance.ScreenInGame.Steel[20].area = new Rectangle(628 + 16, 241 + 16, 668 - 628, 290 - 241);
                MyGame.Instance.ScreenInGame.Steel[21].area = new Rectangle(561 + 16, 322 + 16, 596 - 561, 347 - 322);
                MyGame.Instance.ScreenInGame.Steel[22].area = new Rectangle(271 + 16, 437 + 16, 403 - 271, 489 - 437);
                MyGame.Instance.ScreenInGame.Steel[23].area = new Rectangle(268 + 16, 356 + 16, 303 - 268, 462 - 356);
                MyGame.Instance.ScreenInGame.Steel[24].area = new Rectangle(745 + 16, 411 + 16, 814 - 745, 461 - 411);
                MyGame.Instance.ScreenInGame.Steel[25].area = new Rectangle(302 + 16, 410 + 16, 336 - 302, 436 - 410);
                break;
            case 173:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 3;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].area = new Rectangle(1506 + 16, 176 + 16, 1571 - 1506, 496 - 176);  // add +16 on x & y only first for mouse sprite fix on "F1"
                MyGame.Instance.ScreenInGame.Steel[1].area = new Rectangle(2526 + 16, 153 + 16, 2592 - 2526, 402 - 153);
                break;
            case 177:
                MyGame.Instance.ScreenInGame.Sprite = new Varsprites[1];
                MyGame.Instance.ScreenInGame.Sprite[0].Calc = true;
                MyGame.Instance.ScreenInGame.Sprite[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisX = 6;
                MyGame.Instance.ScreenInGame.Sprite[0].AxisY = 1;
                MyGame.Instance.ScreenInGame.Sprite[0].Depth = 0.405f;
                MyGame.Instance.ScreenInGame.Sprite[0].R = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].G = 225;
                MyGame.Instance.ScreenInGame.Sprite[0].B = 225;
                MyGame.Instance.ScreenInGame.Sprite[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Sprite[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[0].Scale = 0.5f; //1f->normal size -- 0.5f->half size -- etc.
                MyGame.Instance.ScreenInGame.Sprite[0].Rotation = 0f;
                MyGame.Instance.ScreenInGame.Sprite[0].Framesecond = 2;
                MyGame.Instance.ScreenInGame.Sprite[0].Frame = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Content.Load<Texture2D>("touch/arana");
                MyGame.Instance.ScreenInGame.Sprite[0].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[0].Dest = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[0].Speed = 0.578f;  // this field is important for move logic of sprites != 0
                MyGame.Instance.ScreenInGame.Sprite[0].ActVect = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Center.X = ((MyGame.Instance.ScreenInGame.Sprite[0].Sprite.Width / MyGame.Instance.ScreenInGame.Sprite[0].AxisX) / 2);
                MyGame.Instance.ScreenInGame.Sprite[0].Center.Y = ((MyGame.Instance.ScreenInGame.Sprite[0].Sprite.Height / MyGame.Instance.ScreenInGame.Sprite[0].AxisY) / 2);
                MyGame.Instance.ScreenInGame.Sprite[0].Path = new Vector3[6];
                MyGame.Instance.ScreenInGame.Sprite[0].Path[0] = new Vector3(402 - 20, 109 + 50, 1.5f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[1] = new Vector3(424 - 20, 231 + 50, 0.3f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[2] = new Vector3(461 - 20, 230 + 50, 1.9f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[3] = new Vector3(462 - 20, 164 + 50, 1.6f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[4] = new Vector3(525 - 20, 162 + 50, 0.7f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[5] = new Vector3(525 - 20, 280 + 50, 1.2f);
                break;
            case 179:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].doorMoreXY = new Vector2(GetLevel(179).DoorX, GetLevel(179).DoorY);
                MyGame.Instance.ScreenInGame.MoreDoors[1].doorMoreXY = new Vector2(GetLevel(179).DoorX + 100, GetLevel(179).DoorY + 160);
                MyGame.Instance.ScreenInGame.MoreDoors[2].doorMoreXY = new Vector2(GetLevel(179).DoorX + 190, GetLevel(179).DoorY + 330);
                break;
            default:
                MyGame.Instance.ScreenInGame.ArrowsON = false;
                MyGame.Instance.ScreenInGame.TrapsON = false;
                MyGame.Instance.ScreenInGame.SteelON = false;
                MyGame.Instance.ScreenInGame.PlatsON = false;
                MyGame.Instance.ScreenInGame.AddsON = false;
                break;
        }
    }
}
