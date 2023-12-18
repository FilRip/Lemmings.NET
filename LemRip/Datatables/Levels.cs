using System;
using System.IO;

using Lemmings.NET.Constants;
using Lemmings.NET.Helpers;
using Lemmings.NET.Models;
using Lemmings.NET.Models.Props;
using Lemmings.NET.Structs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal static class Levels
{
    internal static OneLevel GetLevel(int numLevel)
    {
        OneLevel lvl = new(numLevel);
        if (numLevel == 0)
            return lvl;

		string section = "level";
        ManageIniFile iniFile = ManageIniFile.OpenIniFile(Path.Combine(Environment.CurrentDirectory, MyGame.Instance.Content.RootDirectory, "levels", $"level{numLevel}.ini"));
        lvl.TotalLemmings = iniFile.ReadInteger(section, "totallemmings");
        lvl.NameLev = iniFile.ReadString(section, "namelev");
        lvl.NumberExploders = iniFile.ReadInteger(section, "numberexploders");
        lvl.NumberBlockers = iniFile.ReadInteger(section, "numberblockers");
        lvl.NumberBuilders = iniFile.ReadInteger(section, "numberbuilders");
        lvl.DoorExitDepth = iniFile.ReadFloat(section, "doorexitdepth");
        lvl.DoorX = iniFile.ReadInteger(section, "doorx");
        lvl.DoorY = iniFile.ReadInteger(section, "doory");
        lvl.ExitX = iniFile.ReadInteger(section, "exitx");
        lvl.ExitY = iniFile.ReadInteger(section, "exity");
        lvl.FrequencyComming = iniFile.ReadInteger(section, "frequencycomming");
        lvl.InitPosX = iniFile.ReadInteger(section, "initposx");
        lvl.MinFrequencyComming = iniFile.ReadInteger(section, "minfrequencycomming");
        lvl.NameOfLevel = iniFile.ReadString(section, "nameoflevel");
        lvl.NbLemmingsToSave = iniFile.ReadInteger(section, "nblemmingstosave");
        lvl.NumberBashers = iniFile.ReadInteger(section, "numberbashers");
        lvl.NumberClimbers = iniFile.ReadInteger(section, "numberclimbers");
        lvl.NumberDiggers = iniFile.ReadInteger(section, "numberdiggers");
        lvl.NumberMiners = iniFile.ReadInteger(section, "numberminers");
        lvl.NumberUmbrellas = iniFile.ReadInteger(section, "numberumbrellas");
        lvl.TotalTime = iniFile.ReadInteger(section, "totaltime");
        lvl.TypeOfDoor = iniFile.ReadInteger(section, "typeofdoor");
        lvl.TypeOfExit = iniFile.ReadInteger(section, "typeofexit");
		
		int nbSprite = iniFile.NumberOfSection("sprite");
        if (nbSprite > 0)
        {
            OnePropSprite spr;
            for (int i = 1; i <= nbSprite; i++)
            {
                section = $"sprite{i}";
                spr = new OnePropSprite()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    AxisX = iniFile.ReadInteger(section, "axisx"),
                    AxisY = iniFile.ReadInteger(section, "axisy"),
                    Color = iniFile.ReadColor(section, "color"),
                    Framesecond = iniFile.ReadInteger(section, "framesecond"),
                    ActVect = iniFile.ReadInteger(section, "actvect"),
                    Dest = iniFile.ReadVector2(section, "dest"),
                    Center = iniFile.ReadVector2(section, "center"),
                    Depth = iniFile.ReadFloat(section, "depth"),
                    Rotation = iniFile.ReadFloat(section, "rotation"),
                    Scale = iniFile.ReadFloat(section, "scale"),
                    Typescroll = iniFile.ReadFloat(section, "typescroll"),
                    Speed = iniFile.ReadFloat(section, "speed"),
                    MinusScrollX = iniFile.ReadBoolean(section, "minusscrollx"),
                    Minus = iniFile.ReadBoolean(section, "minus"),
                    Calc = iniFile.ReadBoolean(section, "calc"),
                    Sprite = iniFile.ReadEnum<EGfxTrap>(section, "sprite").GetTexture(),
                    Path = [],
                };
                int j = 0;
                while (true)
                {
                    j++;
                    if (string.IsNullOrWhiteSpace(iniFile.ReadString(section, $"Path{j}")))
                        break;
                    spr.Path.Add(iniFile.ReadVector3(section, $"Path{j}"));
                }
            }
        }

        int nbTrap = iniFile.NumberOfSection("trap");
        if (nbTrap > 0)
        {
            OneTrap trap;
            for (int i = 1; i <=nbTrap; i++)
            {
                section = $"Trap{i}";
                trap = new OneTrap()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    AreaDraw = iniFile.ReadRectangle(section, "areadraw"),
                    AreaTrap = iniFile.ReadRectangle(section, "areatrap"),
                    Color = iniFile.ReadColor(section, "coloe"),
                    Depth = iniFile.ReadFloat(section, "depth"),
                    NumFrames = iniFile.ReadInteger(section, "numframes"),
                    Sprite = iniFile.ReadEnum<EGfxTrap>(section, "sprite").GetTexture(),
                    Type = iniFile.ReadInteger(section, "type"),
                    Vvscroll = iniFile.ReadInteger(section, "vvscroll"),
                    VvX = iniFile.ReadInteger(section, "vvx"),
                    VvY = iniFile.ReadInteger(section, "vvy"),
                };
            }
        }

        iniFile.Dispose();
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
                MyGame.Instance.ScreenInGame.Sprite[1].Sprite = MyGame.Instance.Gfx.Flame;
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
                MyGame.Instance.ScreenInGame.Sprite[2].Sprite = MyGame.Instance.Gfx.Flame;
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
                MyGame.Instance.ScreenInGame.Sprite[3].Sprite = MyGame.Instance.Gfx.Spider;
                MyGame.Instance.ScreenInGame.Sprite[3].Calc = true;
                MyGame.Instance.ScreenInGame.Sprite[3].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[3].Dest = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[3].Speed = 0.578f;  // this field is important for move logic of sprites != 0
                MyGame.Instance.ScreenInGame.Sprite[3].ActVect = 0;
                MyGame.Instance.ScreenInGame.Sprite[3].Center.X = 32;
                MyGame.Instance.ScreenInGame.Sprite[3].Center.Y = 32;
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
                MyGame.Instance.ScreenInGame.Sprite[4].Sprite = MyGame.Instance.Gfx.Fire;
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
                MyGame.Instance.ScreenInGame.Sprite[5].Sprite = MyGame.Instance.Gfx.Spider;
                MyGame.Instance.ScreenInGame.Sprite[5].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[5].Dest = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[5].Speed = 0.578f;  // this field is important for move logic of sprites != 0
                MyGame.Instance.ScreenInGame.Sprite[5].ActVect = 0;
                MyGame.Instance.ScreenInGame.Sprite[5].Center.X = 32;
                MyGame.Instance.ScreenInGame.Sprite[5].Center.Y = 32;
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
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(820, 462, 709, 40); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(820, 467, 709, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 5:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(510, 480, 300, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(510, 485, 300, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Ice;
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2; // kind of variable scroll the trap 1=z1 -- 2=-z1 --
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(518, 460, 280, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(518, 465, 280, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 130;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Ice;
                break;
            case 6:
            case 47:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(320, 472, 1869, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(320, 477, 1869, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 160;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1033, 97, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1162, 108);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet;
                break;
            case 7:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(192, 472, 3028, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(192, 477, 3028, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 8:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(325, 480, 3175, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(325, 485, 3175, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 9:
            case 56:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 462, 1100, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 470, 1100, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Acid;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(484, 183, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(507, 202);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(829, 380, 46, 10); //see .pos
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(852, 399);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(141, 374, 46, 10);
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(164, 393);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 10:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(380, 490, 1463, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(380, 495, 1463, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].G = 150;
                MyGame.Instance.ScreenInGame.Trap[0].B = 20;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 480, 2303, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].G = 20;
                MyGame.Instance.ScreenInGame.Trap[1].B = 20;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 11:
            case 78:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(468, 161, 305, 279);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 305, 279);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(239, 440, 988, 72);
                break;
            case 13:
            case 101:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 3203, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 477, 3203, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 14:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(180, 480, 2704, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(180, 485, 2704, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 15:
            case 61:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(3312, 450, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(3317, 455);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadHanged;
                break;
            case 16:
            case 63:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 2205, 512);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 475, 2205, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(815, 235, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(845, 250);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(865, 235, 60, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(895, 250);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                break;
            case 17:
            case 66:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 4;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(288, 145);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(508, 145);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(718, 145);
                MyGame.Instance.ScreenInGame.MoreDoors[3].DoorMoreXY = new Vector2(928, 145);//IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(261, 373, 893, 77);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(997, 284, 156, 86);
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(386, 492, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(391, 497);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 31;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 57;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadPiston;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(585, 492, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(590, 497);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 31;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 57;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadPiston;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(787, 492, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(792, 497);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 31;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 57;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadPiston;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(993, 492, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(998, 497);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 31;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 57;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.DeadPiston;
                break;
            case 18:
            case 79:
                MyGame.Instance.ScreenInGame.NumTotTraps = 6;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(845, 439, 60, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(875, 454);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(434, 147, 56, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(460, 162);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(856, 143, 56, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(890, 158);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(561, 236, 56, 10);
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(587, 251);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[4].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].AreaTrap = new Rectangle(931, 297, 56, 10);
                MyGame.Instance.ScreenInGame.Trap[4].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[4].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].Pos = new Vector2(957, 312);
                MyGame.Instance.ScreenInGame.Trap[4].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[4].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[4].Depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[4].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[5].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].AreaTrap = new Rectangle(503, 362, 56, 10);
                MyGame.Instance.ScreenInGame.Trap[5].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[5].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[5].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].Pos = new Vector2(529, 377);
                MyGame.Instance.ScreenInGame.Trap[5].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[5].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[5].Depth = 0.600000008f;
                MyGame.Instance.ScreenInGame.Trap[5].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                break;
            case 20:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(127, 462, 3511, 40); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(127, 467, 3511, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Acid;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1098, 361, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1121, 376);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 21:
            case 116:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 470, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 470, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(1001, 480, 132, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1001, 485, 132, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(3143, 480, 614, 32);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(3143, 485, 614, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(2505, 468, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(2510, 473);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 16;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.WolfTrap;
                break;
            case 23:
            case 77:
                MyGame.Instance.ScreenInGame.NumTOTexits = 2;
                MyGame.Instance.ScreenInGame.Moreexits = new Varmoreexits[MyGame.Instance.ScreenInGame.NumTOTexits];
                MyGame.Instance.ScreenInGame.Moreexits[0].ExitMoreXY = new Vector2(73, 460); //73,460 ----- LEVEL 23 TWO EXITS
                MyGame.Instance.ScreenInGame.Moreexits[1].ExitMoreXY = new Vector2(73, 180);//73,180 //IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(2475, 472, 119, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(2475, 477, 119, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(477, 303, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(606, 308);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet;
                break;
            case 24:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(754, 143, 106, 73);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 106, 73);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(862, 129, 442, 177);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1472, 340, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1477, 345);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 47;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 87;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadPiston2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(1071, 225, 195, 40); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1071, 230, 195, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 26:
            case 103:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(2792, 240, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(2797, 245);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadHanged;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 480, 3007, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 485, 3007, 10);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 27:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 487, 2624, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 492, 2624, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 28:
            case 95:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(253, 472, 608, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(253, 477, 608, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1205, 187, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1334, 192);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(2038, 472, 584, 40);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(2038, 477, 584, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(1495, 187, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(1495, 192);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].B = 160;
                MyGame.Instance.ScreenInGame.Trap[3].G = 160;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.FireJet2;
                break;
            case 29:
            case 99:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(1812, 480, 393, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1812, 485, 393, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.200000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 190;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 30:
            case 114:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(71, 472, 1079, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(71, 477, 1079, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 230;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            //tricky levels tricky
            case 31:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3022, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 480, 3022, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 33:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(370, 480, 1109, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(370, 480, 1109, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 34:
            case 67:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(1063, 45, 151, 239);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 151, 239);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 190;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(84, 284, 1598, 46);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3030, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 3030, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 35:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(728, 480, 2549, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(728, 485, 2549, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 36:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 490, 2088, 512); //special size
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 495, 2088, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].B = 130;
                MyGame.Instance.ScreenInGame.Trap[0].G = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(444, 336, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(444, 341);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(1632, 246, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(1761, 251);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.FireJet;
                break;
            case 37:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 1281, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 1281, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 120;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 38:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 2088, 40); //normal size
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 482, 2088, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].B = 130;
                MyGame.Instance.ScreenInGame.Trap[0].G = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 39:
            case 96:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 2;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(685, 336, 96, 85);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 96, 85);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Arrow[1].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[1].Area = new Rectangle(2466, 110, 111, 103); // mask texture full steel zone
                MyGame.Instance.ScreenInGame.Arrow[1].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[1].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 111, 103);
                MyGame.Instance.ScreenInGame.Arrow[1].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3153, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 3153, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 120;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(553, 216, 2222, 114);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(2698, 144, 153, 72);
                break;
            case 40:
            case 105:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(388, 225);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(2240, 225);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 2853, 40); //normal size
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 482, 2853, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].B = 80;
                MyGame.Instance.ScreenInGame.Trap[0].G = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 41:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(1027, 480, 526, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1027, 485, 526, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 43:
            case 115:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 441, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 441, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 49:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 490, 3391, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 495, 3391, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 50:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(1318, 164, 160, 296);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 160, 296);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.NumTotTraps = 5;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(217, 406, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(217, 411);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(724, 145, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(754, 160);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(771, 145, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(801, 160);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(1740, 154, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(1770, 169);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[4].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].AreaTrap = new Rectangle(1791, 154, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[4].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[4].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].Pos = new Vector2(1821, 169);
                MyGame.Instance.ScreenInGame.Trap[4].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[4].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[4].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                break;
            case 52:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(384, 480, 2813, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(384, 485, 2813, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.4000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 480, 278, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 485, 278, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.4000000011f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(3197, 480, 452, 32);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(3197, 485, 452, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 55:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(2506, 329, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(2511, 334);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadHanged;
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(1300, 480, 2789, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1300, 485, 2789, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 480, 787, 32);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(0, 485, 787, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 57:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(1306, 35, 499, 283);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 499, 283);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 777, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 777, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 58:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3117, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 3117, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(1780, 239, 226, 41);
                break;
            case 59:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(159, 303);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(459, 150);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(770, 46);
                MyGame.Instance.ScreenInGame.NumTotTraps = 7;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(600, 410, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(623, 429);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1243, 432, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1266, 451);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(1568, 113, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(1591, 132);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(1581, 348, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(1604, 367);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[4].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].AreaTrap = new Rectangle(2201, 134, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[4].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[4].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[4].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].Pos = new Vector2(2224, 153);
                MyGame.Instance.ScreenInGame.Trap[4].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[4].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[4].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[5].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].AreaTrap = new Rectangle(2131, 313, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[5].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[5].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[5].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].Pos = new Vector2(2154, 332);
                MyGame.Instance.ScreenInGame.Trap[5].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[5].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[5].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[5].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[6].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[6].AreaTrap = new Rectangle(2740, 315, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[6].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[6].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[6].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[6].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[6].Pos = new Vector2(2763, 334);
                MyGame.Instance.ScreenInGame.Trap[6].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[6].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[6].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[6].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[6].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 60:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3090, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 3090, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 224;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            // TAXING LEVELS TAXING //////////////////////////////////////////////////////////////////////////
            case 62:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(1400, 65);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(1962, 65);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(1614, 129, 353, 124);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(2347, 152, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(2352, 157);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 16;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WolfTrap;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1047, 454, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1052, 459);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 16;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WolfTrap;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(2707, 129, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 12;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(2712, 134);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 40;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 105;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadAnvil;
                break;
            case 64:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(1316, 321);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(1174, 321);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(1000, 272, 507, 46);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(1265, 319, 44, 123);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(991, 157, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1003, 175);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 26;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpadeLeft;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(908, 254, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(908, 272);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 26;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpadeRight;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(1569, 440, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(1574, 445);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 10;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 71;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadHanged;
                break;
            case 65:
                MyGame.Instance.ScreenInGame.NumTOTexits = 2;
                MyGame.Instance.ScreenInGame.Moreexits = new Varmoreexits[MyGame.Instance.ScreenInGame.NumTOTexits];
                MyGame.Instance.ScreenInGame.Moreexits[0].ExitMoreXY = new Vector2(969, 190);
                MyGame.Instance.ScreenInGame.Moreexits[1].ExitMoreXY = new Vector2(969, 461);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(161, 436, 229, 52);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(537, 160, 73, 149);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(611, 191, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(611, 196);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(611, 269, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(611, 274);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[2].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 472, 1152, 40);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(0, 477, 1152, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 68:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3405, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 3405, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 69:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 771, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 771, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(1448, 480, 856, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1448, 485, 856, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.400000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 70:
                MyGame.Instance.ScreenInGame.NumTotTraps = 5;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(439, 337, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(439, 342);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1638, 212, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1767, 217);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet;
                MyGame.Instance.ScreenInGame.Trap[2].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 472, 1275, 40);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(0, 477, 1275, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[3].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(1435, 472, 655, 40);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(1435, 477, 655, 10);
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[4].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[4].AreaDraw = new Rectangle(1281, 482, 148, 40);
                MyGame.Instance.ScreenInGame.Trap[4].AreaTrap = new Rectangle(1291, 497, 1, 1); //null kill area
                MyGame.Instance.ScreenInGame.Trap[4].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[4].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[4].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[4].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[4].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 71:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3604, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 3604, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 72:
                MyGame.Instance.ScreenInGame.NumTotTraps = 6;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 3000, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 477, 3000, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(752, 200, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(752, 205);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(1306, 163, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(1336, 178);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(1356, 163, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(1386, 178);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[4].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].AreaTrap = new Rectangle(1612, 104, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[4].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[4].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[4].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].Pos = new Vector2(1642, 119);
                MyGame.Instance.ScreenInGame.Trap[4].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[4].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[4].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[5].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].AreaTrap = new Rectangle(1651, 104, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[5].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[5].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[5].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].Pos = new Vector2(1691, 119);
                MyGame.Instance.ScreenInGame.Trap[5].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[5].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[5].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[5].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                break;
            case 73:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 2;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(1737, 121, 195, 205);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow2;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 195, 205);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Arrow[1].Right = true;
                MyGame.Instance.ScreenInGame.Arrow[1].Area = new Rectangle(478, 42, 153, 332); // mask texture full steel zone
                MyGame.Instance.ScreenInGame.Arrow[1].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[1].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 153, 332);
                MyGame.Instance.ScreenInGame.Arrow[1].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[1].Transparency = 255;
                break;
            case 74:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 4002, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 4002, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f; //lemmings depth 0.300f
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 150;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 76:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(994, 472, 1550, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(994, 477, 1550, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].R = 140;
                MyGame.Instance.ScreenInGame.Trap[0].G = 120;
                MyGame.Instance.ScreenInGame.Trap[0].B = 190;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 81:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(834, 353, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(963, 358);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(487, 353, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(487, 358);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet2;
                break;
            case 83:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 1281, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 477, 1281, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 84:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 3283, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 477, 3283, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 86:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(84, 382);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(930, 382);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(500, 56);
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(297, 472, 539, 512);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(297, 475, 539, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(900, 113, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(930, 128);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(942, 113, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(972, 128);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(88, 124, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(88, 129);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.FireJet2;
                break;
            case 87:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 2083, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 2083, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.300000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 230;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Ice;
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(102, 94, 324, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(1640, 94, 325, 32);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 88:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(2420, 233, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(2425, 238);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 33;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 25;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadLaser;
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 480, 3517, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 485, 3517, 10);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 89:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(881, 480, 600, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(881, 485, 600, 10);// not necessary
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 90:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 3207, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 477, 3207, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            // MAYHEM LEVELS MAYHEM /////////////////////////////////////////////////////////////////////
            case 92:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(830, 214, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(830, 219);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(1318, 472, 482, 40);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1318, 477, 482, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 93:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = true;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(754, 143, 106, 73);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 106, 73);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(862, 129, 442, 177);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1472, 340, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1477, 345);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 47;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 87;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadPiston2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(1071, 233, 195, 32); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1071, 238, 195, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 97:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(127, 480, 3511, 32); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(127, 485, 3511, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1098, 357, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1121, 376);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 98:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(1006, 50, 160, 343);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 160, 343);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(1006, 398, 99, 114);
                break;
            case 102:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 7;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(912, 46, 74, 334);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(605, 231, 690, 75);
                MyGame.Instance.ScreenInGame.Steel[2].Area = new Rectangle(449, 153, 227, 76);
                MyGame.Instance.ScreenInGame.Steel[3].Area = new Rectangle(66, 155, 304, 73);
                MyGame.Instance.ScreenInGame.Steel[4].Area = new Rectangle(255, 306, 82, 73);
                MyGame.Instance.ScreenInGame.Steel[5].Area = new Rectangle(104, 303, 76, 78);
                MyGame.Instance.ScreenInGame.Steel[6].Area = new Rectangle(909, 438, 77, 84);
                break;
            case 104:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 3;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(742, 398, 824, 69);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(579, 323, 163, 117);
                MyGame.Instance.ScreenInGame.Steel[2].Area = new Rectangle(1447, 324, 204, 75);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(661, 330, 915, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(661, 335, 915, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Acid;
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(661, 350, 915, 40);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(661, 355, 915, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Acid;
                break;
            case 106:
            case 117:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(311, 284, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(334, 303);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1461, 339, 46, 10); //see .pos
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1484, 358);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 107:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 2187, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 2187, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 108:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 4;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(1240, 75);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(1500, 75);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(1240, 376);
                MyGame.Instance.ScreenInGame.MoreDoors[3].DoorMoreXY = new Vector2(1500, 376);
                break;
            case 113:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(1341, 480, 1167, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1341, 485, 1167, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 118:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1970, 280, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1975, 285);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 47;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 87;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadPiston2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 480, 3922, 32); // 512-40=462 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 485, 3922, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 120:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(99, 328);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(4094, 328);
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1584, 429, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1589, 434);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 16;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WolfTrap;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(363, 469, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 12;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(368, 474);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 40;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 105;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadAnvil;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(3296, 448, 10, 10);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 12;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(3301, 453);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 40;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 105;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadAnvil;
                break;
            //bonus levels bonus ////////////////////////////////////////////////////////////////////////////////////////
            case 124:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1318, 305, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1341, 324);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(913, 336, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(936, 355);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 126:
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 2;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(605, 0, 95, 245);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 95, 245);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Arrow[1].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[1].Area = new Rectangle(952, 0, 95, 245); // mask texture full steel zone
                MyGame.Instance.ScreenInGame.Arrow[1].Arrow = MyGame.Instance.Gfx.Arrow2;
                MyGame.Instance.ScreenInGame.Arrow[1].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 95, 245);
                MyGame.Instance.ScreenInGame.Arrow[1].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[1].Transparency = 255;
                break;
            case 127:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 2148, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 2148, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.300000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Ice;
                break;
            case 130:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1011, 208, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1016, 213);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadHanged;
                break;
            case 132:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 3909, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 3909, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.300000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Ice;
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 1; // kind of variable scroll the trap 1=z1--
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 485, 3909, 32); //512-32=480 bottom of the screen
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 490, 3909, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Ice;
                break;
            case 133:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 0;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(395, 472, 1252, 40);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(395, 477, 1252, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                break;
            case 134:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(970, 381);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(160, 92);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1068, 388, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1080, 406);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 26;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpadeLeft;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(144, 100, 15, 36);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 7;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(144, 118);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 26;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpadeRight;
                break;
            case 136:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(172, 490, 1671, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(172, 495, 1671, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].R = 20;
                MyGame.Instance.ScreenInGame.Trap[0].B = 20;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 480, 2049, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].R = 20;
                MyGame.Instance.ScreenInGame.Trap[1].B = 20;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 137:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1017, 230, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1146, 235);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet;
                break;
            case 138:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(383, 237);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(83, 237);
                //moredoors[1].doormorexy = new Vector2(1110,220); TEST THIS OPTION -- BASHER TO LEFT FAILS??????
                MyGame.Instance.ScreenInGame.ArrowsON = true;
                MyGame.Instance.ScreenInGame.NumTotArrow = 1;
                MyGame.Instance.ScreenInGame.Arrow = new Vararrows[MyGame.Instance.ScreenInGame.NumTotArrow];
                MyGame.Instance.ScreenInGame.Arrow[0].Right = false;
                MyGame.Instance.ScreenInGame.Arrow[0].Area = new Rectangle(961, 219, 50, 190);
                MyGame.Instance.ScreenInGame.Arrow[0].Arrow = MyGame.Instance.Gfx.Arrow;
                MyGame.Instance.ScreenInGame.Arrow[0].EnvelopArrow = new Texture2D(MyGame.Instance.GraphicsDevice, 50, 190);
                MyGame.Instance.ScreenInGame.Arrow[0].Moving = 0;
                MyGame.Instance.ScreenInGame.Arrow[0].Transparency = 255;
                break;
            case 139:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(2203, 425, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(2208, 430);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 16;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WolfTrap;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(2838, 425, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 15;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(2843, 430);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 16;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 42;  //38
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WolfTrap;
                break;
            case 140:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(133, 150, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(133, 155);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet2;
                break;
            case 141:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(358, 32);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(758, 32);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(648, 165, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(671, 184);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 143:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 1632, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 1632, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 144:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(134, 326);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(464, 3);
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(601, 442, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(624, 461);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 145:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(951, 210, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(974, 229);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(87, 338, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(110, 357);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(947, 338, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(970, 357);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 147:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(79, 480, 2410, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(79, 485, 2410, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].G = 130;
                MyGame.Instance.ScreenInGame.Trap[0].R = 130;
                MyGame.Instance.ScreenInGame.Trap[0].B = 180;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 149:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 1794, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 1794, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(999, 282, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1022, 301);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 150:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(685, 462, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(690, 467);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 10;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 71;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadHanged;

                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1323, 422, 10, 10);//see .pos minus 5 on both axis
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 37;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1328, 427);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 10;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 71;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadHanged;
                break;
            case 151:
                MyGame.Instance.ScreenInGame.NumTotTraps = 3;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 2177, 512);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 475, 2177, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1083, 210, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1113, 225);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(1133, 210, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(1163, 225);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                break;
            case 152:
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 472, 1493, 512);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 475, 1493, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.Lava;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(960, 321, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(990, 336);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(1035 - 30, 321, 60, 10);//see .pos
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(1035, 336);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 30;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 30;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.Fire2; //34 height sprite
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(509, 19, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(638, 24);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.400000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.FireJet;
                break;
            case 153:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 490, 1158, 32); //size smaller to fit
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 492, 1158, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.6000000011f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(860, 377, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(883, 396);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.200000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 154:
                MyGame.Instance.ScreenInGame.NumTotTraps = 9;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(436, 212, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(459, 231);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(912, 163, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(935, 182);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[2].AreaTrap = new Rectangle(1551, 110, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = new Vector2(1574, 129);
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[3].AreaTrap = new Rectangle(2072, 2, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = new Vector2(2095, 21);
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[4].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[4].AreaTrap = new Rectangle(17, 236, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[4].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[4].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[4].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[4].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[4].Pos = new Vector2(40, 255);
                MyGame.Instance.ScreenInGame.Trap[4].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[4].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[4].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[4].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[4].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[5].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[5].AreaTrap = new Rectangle(484, 33, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[5].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[5].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[5].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[5].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[5].Pos = new Vector2(507, 52);
                MyGame.Instance.ScreenInGame.Trap[5].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[5].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[5].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[5].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[5].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[6].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[6].AreaTrap = new Rectangle(1249, 186, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[6].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[6].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[6].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[6].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[6].Pos = new Vector2(1272, 205);
                MyGame.Instance.ScreenInGame.Trap[6].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[6].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[6].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[6].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[6].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[7].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[7].AreaTrap = new Rectangle(1940, 288, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[7].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[7].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[7].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[7].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[7].Pos = new Vector2(1963, 307);
                MyGame.Instance.ScreenInGame.Trap[7].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[7].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[7].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[7].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[7].Sprite = MyGame.Instance.Gfx.DeadSpin;
                MyGame.Instance.ScreenInGame.Trap[8].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[8].AreaTrap = new Rectangle(2169, 441, 46, 10); //se .pos minis vvY/2 -vvx long vvx*2
                MyGame.Instance.ScreenInGame.Trap[8].NumFrames = 16;
                MyGame.Instance.ScreenInGame.Trap[8].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[8].Type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[8].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[8].Pos = new Vector2(2192, 460);
                MyGame.Instance.ScreenInGame.Trap[8].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[8].VvY = 38;
                MyGame.Instance.ScreenInGame.Trap[8].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[8].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[8].Sprite = MyGame.Instance.Gfx.DeadSpin;
                break;
            case 159:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 4;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(136, 14);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(559, 131);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(96, 327);
                MyGame.Instance.ScreenInGame.MoreDoors[3].DoorMoreXY = new Vector2(1033, 419);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(1167, 376, 10, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 9;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 666;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(1172, 381);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 48;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.DeadClam;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1171, 264, 10, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 29;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1176, 269);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 120;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadBell;
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
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Gfx.Cloud1;
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
                MyGame.Instance.ScreenInGame.Sprite[1].Sprite = MyGame.Instance.Gfx.Cloud2;
                MyGame.Instance.ScreenInGame.Sprite[1].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[1].Typescroll = 2;
                break;
            case 160:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 2;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(136, 154);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(1280, 462);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(357, 328, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = new Vector2(357, 333);
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.FireJet2;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(-65, 328, 129, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 10;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(64, 333);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 159;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 27;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.FireJet;
                break;
            case 161:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(892, 862, 228, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(892, 867, 228, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 0, 0, 0);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1446, 464, 10, 10); //normally +5 on Y and 10 of height
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 29;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 666;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = new Vector2(1451, 469);
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 32;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 120;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.DeadBell;
                break;
            case 162:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(158, 99);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(338, 99);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(158, 345);
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(458, 0, 43, 319);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(145, 269, 132, 51);
                break;
            case 163:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(54, 39);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(54, 220);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(54, 382);
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(853, 504, 1079, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(853, 509, 1079, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(624, 451, 48, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(624, 456, 48, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 170;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
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
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Gfx.Torch;
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
                MyGame.Instance.ScreenInGame.Sprite[1].Sprite = MyGame.Instance.Gfx.Torch;
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
                MyGame.Instance.ScreenInGame.Sprite[2].Sprite = MyGame.Instance.Gfx.Torch;
                MyGame.Instance.ScreenInGame.Sprite[2].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(315, 270, 41, 50);
                break;
            case 165:
                MyGame.Instance.ScreenInGame.NumTotTraps = 1;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 480, 1100, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 485, 1100, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 2;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(511, 242, 41, 104);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(735, 243, 41, 48);
                break;
            case 166:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 485, 1882, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(0, 490, 1882, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 504, 1882, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 509, 1882, 10);
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 1;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(224, 321, 1611, 52);
                break;
            case 167:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(580, 424, 226, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(580, 429, 226, 10);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(1209, 504, 182, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(1209, 509, 182, 10);
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 5;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(539, 55, 42, 425);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(808, 268, 40, 211);
                MyGame.Instance.ScreenInGame.Steel[2].Area = new Rectangle(1165, 376, 43, 160);
                MyGame.Instance.ScreenInGame.Steel[3].Area = new Rectangle(1389, 324, 43, 212);
                MyGame.Instance.ScreenInGame.Steel[4].Area = new Rectangle(270, 430, 269, 49);
                break;
            case 168:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 3;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(1389, 81, 40, 373);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(1432, 215, 133, 47);
                MyGame.Instance.ScreenInGame.Steel[2].Area = new Rectangle(1702, 188, 579, 51);
                break;
            case 169:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 6;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(1479, 107, 176, 105);
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(1434, 162, 42, 104);
                MyGame.Instance.ScreenInGame.Steel[2].Area = new Rectangle(1390, 215, 41, 104);
                MyGame.Instance.ScreenInGame.Steel[3].Area = new Rectangle(1345, 270, 41, 154);
                MyGame.Instance.ScreenInGame.Steel[4].Area = new Rectangle(1300, 322, 42, 51);
                MyGame.Instance.ScreenInGame.Steel[5].Area = new Rectangle(1165, 482, 490, 50);
                MyGame.Instance.ScreenInGame.NumTotTraps = 4;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(0, 483, 3091, 32);
                MyGame.Instance.ScreenInGame.Trap[0].Vvscroll = 1;
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600005f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(0, 504, 3091, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(0, 509, 3091, 10);
                MyGame.Instance.ScreenInGame.Trap[1].Vvscroll = 2;
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[2].AreaDraw = new Rectangle(1028, 451, 137, 32);
                MyGame.Instance.ScreenInGame.Trap[2].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[2].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[2].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[2].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[2].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[2].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[2].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[2].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[2].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[3].AreaDraw = new Rectangle(1387, 343, 226, 32);
                MyGame.Instance.ScreenInGame.Trap[3].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[3].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[3].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[3].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[3].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[3].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[3].Depth = 0.600000009f;
                MyGame.Instance.ScreenInGame.Trap[3].Transparency = 255;
                MyGame.Instance.ScreenInGame.Trap[3].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 170:
                MyGame.Instance.ScreenInGame.NumTotTraps = 2;
                MyGame.Instance.ScreenInGame.TrapsON = true;
                MyGame.Instance.ScreenInGame.Trap = new Vartraps[MyGame.Instance.ScreenInGame.NumTotTraps];
                MyGame.Instance.ScreenInGame.Trap[0].AreaDraw = new Rectangle(402, 415, 585, 32);
                MyGame.Instance.ScreenInGame.Trap[0].AreaTrap = new Rectangle(402, 420, 585, 10);
                MyGame.Instance.ScreenInGame.Trap[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[0].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[0].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[0].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[0].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[0].Depth = 0.600005f;
                MyGame.Instance.ScreenInGame.Trap[0].Transparency = 200;
                MyGame.Instance.ScreenInGame.Trap[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                MyGame.Instance.ScreenInGame.Trap[1].AreaDraw = new Rectangle(222, 146, 361, 32);
                MyGame.Instance.ScreenInGame.Trap[1].AreaTrap = new Rectangle(222, 151, 361, 10);
                MyGame.Instance.ScreenInGame.Trap[1].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Trap[1].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Type = 1;
                MyGame.Instance.ScreenInGame.Trap[1].IsOn = false;
                MyGame.Instance.ScreenInGame.Trap[1].Pos = Vector2.Zero;
                MyGame.Instance.ScreenInGame.Trap[1].VvX = 0;
                MyGame.Instance.ScreenInGame.Trap[1].VvY = 0;
                MyGame.Instance.ScreenInGame.Trap[1].Depth = 0.600005f;
                MyGame.Instance.ScreenInGame.Trap[1].Transparency = 190;
                MyGame.Instance.ScreenInGame.Trap[1].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 171:
                MyGame.Instance.ScreenInGame.NumTOTplats = 2;
                MyGame.Instance.ScreenInGame.PlatsON = true;
                MyGame.Instance.ScreenInGame.Plats = new Varplat[MyGame.Instance.ScreenInGame.NumTOTplats];
                MyGame.Instance.ScreenInGame.Plats[0].ActStep = 0;
                MyGame.Instance.ScreenInGame.Plats[0].Framesecond = 8;
                MyGame.Instance.ScreenInGame.Plats[0].Frame = 0;
                MyGame.Instance.ScreenInGame.Plats[0].NumSteps = 22;
                MyGame.Instance.ScreenInGame.Plats[0].Up = true;
                MyGame.Instance.ScreenInGame.Plats[0].Step = 5;
                MyGame.Instance.ScreenInGame.Plats[0].AreaDraw = new Rectangle(710, 1110, 220, 60);
                MyGame.Instance.ScreenInGame.Plats[0].Sprite = MyGame.Instance.Gfx.TrapElevator;
                MyGame.Instance.ScreenInGame.Plats[1].ActStep = 0;
                MyGame.Instance.ScreenInGame.Plats[1].Framesecond = 0;
                MyGame.Instance.ScreenInGame.Plats[1].Frame = 0;
                MyGame.Instance.ScreenInGame.Plats[1].NumSteps = 30;
                MyGame.Instance.ScreenInGame.Plats[1].Up = true;
                MyGame.Instance.ScreenInGame.Plats[1].Step = 1;
                MyGame.Instance.ScreenInGame.Plats[1].AreaDraw = new Rectangle(528, 1216, 200, 35);
                MyGame.Instance.ScreenInGame.Plats[1].Sprite = MyGame.Instance.Gfx.TrapElevator;
                MyGame.Instance.ScreenInGame.AddsON = true;
                MyGame.Instance.ScreenInGame.Adds = new Varadds[1];
                MyGame.Instance.ScreenInGame.Adds[0].AreaDraw = new Rectangle(250, 1271, 100, 50); //y 110 orig
                MyGame.Instance.ScreenInGame.Adds[0].NumFrames = 8;
                MyGame.Instance.ScreenInGame.Adds[0].ActFrame = 0;
                MyGame.Instance.ScreenInGame.Adds[0].Frame = 0;
                MyGame.Instance.ScreenInGame.Adds[0].Framesecond = 2;
                MyGame.Instance.ScreenInGame.Adds[0].Sprite = MyGame.Instance.Gfx.WaterBlue;
                break;
            case 172:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 26;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(480, 98, 70, 78);  // add +16 on x & y only first for mouse sprite fix on "F1"
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(624, 125, 68, 76);
                MyGame.Instance.ScreenInGame.Steel[2].Area = new Rectangle(386, 506, 375, 5);
                MyGame.Instance.ScreenInGame.Steel[3].Area = new Rectangle(487, 453, 68, 54);
                MyGame.Instance.ScreenInGame.Steel[4].Area = new Rectangle(365, 302, 65, 78);
                MyGame.Instance.ScreenInGame.Steel[5].Area = new Rectangle(478, 238, 66, 77);
                MyGame.Instance.ScreenInGame.Steel[6].Area = new Rectangle(319, 166, 69, 78);
                MyGame.Instance.ScreenInGame.Steel[7].Area = new Rectangle(725, 345, 202, 81);
                MyGame.Instance.ScreenInGame.Steel[8].Area = new Rectangle(691, 481, 102, 23);
                MyGame.Instance.ScreenInGame.Steel[9].Area = new Rectangle(185, 265, 99, 158);
                MyGame.Instance.ScreenInGame.Steel[10].Area = new Rectangle(188, 133, 62, 130);
                MyGame.Instance.ScreenInGame.Steel[11].Area = new Rectangle(846, 87, 135, 49);
                MyGame.Instance.ScreenInGame.Steel[12].Area = new Rectangle(610, 283, 33, 107);
                MyGame.Instance.ScreenInGame.Steel[13].Area = new Rectangle(829, 294, 128, 48);
                MyGame.Instance.ScreenInGame.Steel[14].Area = new Rectangle(779, 164, 69, 49);
                MyGame.Instance.ScreenInGame.Steel[15].Area = new Rectangle(813, 137, 68, 25);
                MyGame.Instance.ScreenInGame.Steel[16].Area = new Rectangle(542, 365, 67, 52);
                MyGame.Instance.ScreenInGame.Steel[17].Area = new Rectangle(747, 195, 32, 51);
                MyGame.Instance.ScreenInGame.Steel[18].Area = new Rectangle(714, 230, 32, 50);
                MyGame.Instance.ScreenInGame.Steel[19].Area = new Rectangle(679, 244, 36, 49);
                MyGame.Instance.ScreenInGame.Steel[20].Area = new Rectangle(644, 257, 40, 49);
                MyGame.Instance.ScreenInGame.Steel[21].Area = new Rectangle(577, 338, 35, 25);
                MyGame.Instance.ScreenInGame.Steel[22].Area = new Rectangle(287, 453, 132, 52);
                MyGame.Instance.ScreenInGame.Steel[23].Area = new Rectangle(284, 372, 35, 106);
                MyGame.Instance.ScreenInGame.Steel[24].Area = new Rectangle(761, 427, 69, 50);
                MyGame.Instance.ScreenInGame.Steel[25].Area = new Rectangle(318, 426, 34, 26);
                break;
            case 173:
                MyGame.Instance.ScreenInGame.SteelON = true;
                MyGame.Instance.ScreenInGame.NumTOTsteel = 3;
                MyGame.Instance.ScreenInGame.Steel = new Varsteel[MyGame.Instance.ScreenInGame.NumTOTsteel];
                MyGame.Instance.ScreenInGame.Steel[0].Area = new Rectangle(1522, 192, 65, 320);  // add +16 on x & y only first for mouse sprite fix on "F1"
                MyGame.Instance.ScreenInGame.Steel[1].Area = new Rectangle(2542, 169, 66, 249);
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
                MyGame.Instance.ScreenInGame.Sprite[0].Sprite = MyGame.Instance.Gfx.Spider;
                MyGame.Instance.ScreenInGame.Sprite[0].MinusScrollX = true;
                MyGame.Instance.ScreenInGame.Sprite[0].Dest = new Vector2(0, 0);
                MyGame.Instance.ScreenInGame.Sprite[0].Speed = 0.578f;  // this field is important for move logic of sprites != 0
                MyGame.Instance.ScreenInGame.Sprite[0].ActVect = 0;
                MyGame.Instance.ScreenInGame.Sprite[0].Center.X = 32;
                MyGame.Instance.ScreenInGame.Sprite[0].Center.Y = 32;
                MyGame.Instance.ScreenInGame.Sprite[0].Path = new Vector3[6];
                MyGame.Instance.ScreenInGame.Sprite[0].Path[0] = new Vector3(382, 159, 1.5f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[1] = new Vector3(404, 281, 0.3f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[2] = new Vector3(441, 280, 1.9f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[3] = new Vector3(442, 214, 1.6f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[4] = new Vector3(505, 212, 0.7f);
                MyGame.Instance.ScreenInGame.Sprite[0].Path[5] = new Vector3(505, 330, 1.2f);
                break;
            case 179:
                MyGame.Instance.ScreenInGame.NumTOTdoors = 3;
                MyGame.Instance.ScreenInGame.NumACTdoor = 0;
                MyGame.Instance.ScreenInGame.MoreDoors = new Varmoredoors[MyGame.Instance.ScreenInGame.NumTOTdoors];
                MyGame.Instance.ScreenInGame.MoreDoors[0].DoorMoreXY = new Vector2(156, 41);
                MyGame.Instance.ScreenInGame.MoreDoors[1].DoorMoreXY = new Vector2(256, 201);
                MyGame.Instance.ScreenInGame.MoreDoors[2].DoorMoreXY = new Vector2(346, 371);
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
