using System;
using System.IO;

using Lemmings.NET.Constants;
using Lemmings.NET.Helpers;
using Lemmings.NET.Models;
using Lemmings.NET.Models.Props;

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
                    Sprite = iniFile.ReadEnum<EGfxProp>(section, "sprite").GetTexture(),
                    Path = [],
                };
                int j = 0;
                while (true)
                {
                    j++;
                    if (string.IsNullOrWhiteSpace(iniFile.ReadString(section, $"Path{j}")))
                        break;
                    spr.Path = spr.Path.Add(iniFile.ReadVector3(section, $"Path{j}"));
                }
                lvl.ListAllProps.Add(spr);
            }
        }

        int nbTrap = iniFile.NumberOfSection("trap");
        if (nbTrap > 0)
        {
            OneTrap trap;
            for (int i = 1; i <= nbTrap; i++)
            {
                section = $"Trap{i}";
                trap = new OneTrap()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    AreaDraw = iniFile.ReadRectangle(section, "areadraw"),
                    AreaTrap = iniFile.ReadRectangle(section, "areatrap"),
                    Color = iniFile.ReadColor(section, "color"),
                    Depth = iniFile.ReadFloat(section, "depth"),
                    NumFrames = iniFile.ReadInteger(section, "numframes"),
                    Sprite = iniFile.ReadEnum<EGfxProp>(section, "sprite").GetTexture(),
                    Type = iniFile.ReadInteger(section, "type"),
                    Vvscroll = iniFile.ReadInteger(section, "vvscroll"),
                    VvX = iniFile.ReadInteger(section, "vvx"),
                    VvY = iniFile.ReadInteger(section, "vvy"),
                };
                lvl.ListAllProps.Add(trap);
            }
        }

        int nbSteel = iniFile.NumberOfSection("steel");
        if (nbSteel > 0)
        {
            OneSteel steel;
            for (int i = 1; i < nbSteel; i++)
            {
                section = $"steel{i}";
                steel = new OneSteel()
                {
                    Area = iniFile.ReadRectangle(section, "area"),
                };
                lvl.ListAllProps.Add(steel);
            }
        }

        int nbPlat = iniFile.NumberOfSection("plat");
        if (nbPlat > 0)
        {
            OnePlat plat;
            for (int i = 1; i < nbPlat; i++)
            {
                section = $"plat{i}";
                plat = new OnePlat()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    ActStep = iniFile.ReadInteger(section, "actstep"),
                    AreaDraw = iniFile.ReadRectangle(section, "areadraw"),
                    Frame = iniFile.ReadInteger(section, "frame"),
                    Framesecond = iniFile.ReadInteger(section, "framesecond"),
                    NumSteps = iniFile.ReadInteger(section, "numsteps"),
                    Sprite = iniFile.ReadEnum<EGfxProp>(section, "sprite").GetTexture(),
                    Step = iniFile.ReadInteger(section, "step"),
                    Up = iniFile.ReadBoolean(section, "up"),
                };
                lvl.ListAllProps.Add(plat);
            }
        }

        int nbAdd = iniFile.NumberOfSection("add");
        if (nbAdd > 0)
        {
            OneAdd add;
            for (int i = 1; i < nbAdd; i++)
            {
                section = $"add{i}";
                add = new OneAdd()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    AreaDraw = iniFile.ReadRectangle(section, "areadraw"),
                    Frame = iniFile.ReadInteger(section, "frame"),
                    Framesecond = iniFile.ReadInteger(section, "framesecond"),
                    NumFrames = iniFile.ReadInteger(section, "numframes"),
                    Sprite = iniFile.ReadEnum<EGfxProp>(section, "sprite").GetTexture(),
                };
                lvl.ListAllProps.Add(add);
            }
        }

        int nbArrow = iniFile.NumberOfSection("arrow");
        if (nbArrow > 0)
        {
            OneArrow arrow;
            for (int i = 1; i < nbArrow; i++)
            {
                section = $"arrow{i}";
                arrow = new OneArrow()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    Area = iniFile.ReadRectangle(section, "area"),
                    Arrow = iniFile.ReadEnum<EGfxProp>(section, "arrow").GetTexture(),
                    EnvelopArrow = iniFile.ReadEnum<EGfxProp>(section, "enveloparrow").GetTexture(),
                    Frame = iniFile.ReadInteger(section, "frame"),
                    Moving = iniFile.ReadInteger(section, "moving"),
                    Right = iniFile.ReadBoolean(section, "right"),
                    Transparency = iniFile.ReadInteger(section, "transparency"),
                };
                lvl.ListAllProps.Add(arrow);
            }
        }

        int nbMoreDoor = iniFile.NumberOfSection("moredoors");
        if (nbMoreDoor > 0)
        {
            OneMoreDoor door;
            for (int i = 1; i < nbMoreDoor; i++)
            {
                section = $"moredoors{i}";
                door = new OneMoreDoor()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    Frame = iniFile.ReadInteger(section, "frame"),
                    DoorMoreXY = iniFile.ReadVector2(section, "doormorexy"),
                };
                lvl.ListAllProps.Add(door);
            }
        }

        int nbMoreExit = iniFile.NumberOfSection("moreexits");
        if (nbMoreExit > 0)
        {
            OneMoreExit exit;
            for (int i = 1; i < nbMoreExit; i++)
            {
                section = $"moreexits{i}";
                exit = new OneMoreExit()
                {
                    ActFrame = iniFile.ReadInteger(section, "actframe"),
                    Frame = iniFile.ReadInteger(section, "frame"),
                    ExitMoreXY = iniFile.ReadVector2(section, "exitmorexy"),
                };
                lvl.ListAllProps.Add(exit);
            }
        }

        iniFile.Dispose();
        return lvl;
    }
}
