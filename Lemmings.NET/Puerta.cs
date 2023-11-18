using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using static Lemmings.NET.Constants.SizeSprites;

namespace Lemmings.NET
{
    partial class LemmingsNetGame : Game
    {
        private bool doorOn = true, _allBlow = false, pullLemmings = false;
        private int door1X, door1Y;
        private int output1X, output1Y, ex11;
        private int frameDoor = 0, frameExit = 0; // 0--10   0--6
        private int exitFrame = 999, numLemmings = 0, frequencyNumber = 50, numerominfrecuencia = 50; // frecuency lemmings go in
        private Rectangle exit_rect; // rectangle exit
        private Point x;
        private bool debug = false; // ACTIVE DEBUG MODE //be careful with spritebacht begin---end debug mode fails
        private const int numTotalLevels = 182;
        private void Door()
        {
            exit_rect.X = output1X - 5;
            exit_rect.Y = output1Y - 5;
            exit_rect.Width = 10;
            exit_rect.Height = 10;
            if (draw2 && doorOn && Frame > 30)
            {
                totalTime = 0;
                xx55 = varDoor[_level[_currentLevelNumber].TypeOfDoor].numFram - 1;
                frameDoor++;
                if (frameDoor == 1 && _sfx.EntryLemmings.State == SoundState.Stopped && !doorWaveOn)
                {
                    _sfx.EntryLemmings.Play();
                    doorWaveOn = true;
                }
                if (frameDoor > xx55)
                {
                    songInstance.IsLooped = true;
                    songInstance.Play();
                    doorOn = false;
                    frameDoor = xx55;
                }
            }
            pullLemmings = false;
            delayPercent = 27 - frequencyNumber * 0.26f; // see to fix speed of lemmings release on door only when change frecuency (not so good)
            if (dibuja && !doorOn)
            {
                exitFrame++;
                if (exitFrame >= (int)delayPercent)
                {
                    exitFrame = 0;
                    pullLemmings = true;
                }
            }
            //test to see difference with anterior process
            if (pullLemmings && numLemmings != Numlems && !_allBlow)
            {
                if (numTOTdoors > 1 && moreDoors != null) // more than 1 door is different calculation
                {
                    door1Y = (int)moreDoors[numACTdoor].doorMoreXY.Y;
                    door1X = (int)moreDoors[numACTdoor].doorMoreXY.X;
                    numACTdoor++;
                    if (numACTdoor >= numTOTdoors)
                        numACTdoor = 0;
                    lemming[numLemmings].PosY = door1Y;
                    lemming[numLemmings].PosX = door1X + 35;
                }
                else
                {
                    lemming[numLemmings].PosY = door1Y;
                    lemming[numLemmings].PosX = door1X + 35;
                }
                lemming[numLemmings].PosY = door1Y;
                lemming[numLemmings].PosX = door1X + 35;
                lemming[numLemmings].Numframes = 0;
                lemming[numLemmings].Right = true;
                lemming[numLemmings].Fall = true;
                lemming[numLemmings].Walker = false;
                lemming[numLemmings].Pixelscaida = 0;
                lemming[numLemmings].Numframes = faller_frames;
                lemming[numLemmings].Actualframe = 0;
                lemming[numLemmings].Onmouse = false;
                lemming[numLemmings].Active = true;
                lemming[numLemmings].Exit = false;
                lemming[numLemmings].Dead = false;
                lemming[numLemmings].Digger = false;
                lemming[numLemmings].Climber = false;
                lemming[numLemmings].Climbing = false;
                lemming[numLemmings].Umbrella = false;
                lemming[numLemmings].Falling = false;
                lemming[numLemmings].Framescut = false;
                lemming[numLemmings].Breakfloor = false;
                lemming[numLemmings].Exploser = false;
                lemming[numLemmings].Explode = false;
                lemming[numLemmings].Time = 0;
                lemming[numLemmings].Blocker = false;
                lemming[numLemmings].Builder = false;
                lemming[numLemmings].Basher = false;
                lemming[numLemmings].Miner = false;
                lemming[numLemmings].Bridge = false;
                lemming[numLemmings].Burned = false;
                lemming[numLemmings].Drown = false;
                numLemmings++;
                numlemnow++;
            }

            for (actLEM2 = 0; actLEM2 < numLemmings; actLEM2++)
            {
                x.X = lemming[actLEM2].PosX + 14;
                x.Y = lemming[actLEM2].PosY + 25;
                if (lemming[actLEM2].Exit && lemming[actLEM2].Actualframe == 13) // change frame of yipee sound, old frame was init or 0 now different for frames
                {
                    if (_sfx.Yippe.State == SoundState.Playing)
                    {
                        _sfx.Yippe.Stop();
                    }
                    try
                    {
                        _sfx.Yippe.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                }
                if (moreexits == null)
                {
                    if (exit_rect.Contains(x) && !lemming[actLEM2].Exit && !lemming[actLEM2].Explode)
                    {
                        lemming[actLEM2].PosX = output1X - 19;
                        lemming[actLEM2].PosY = output1Y - 30;
                        lemming[actLEM2].Active = false;
                        lemming[actLEM2].Walker = false;
                        lemming[actLEM2].Fall = false;
                        lemming[actLEM2].Falling = false;
                        lemming[actLEM2].Exit = true;
                        lemming[actLEM2].Numframes = sale_frames;
                        lemming[actLEM2].Actualframe = 0;
                    }
                }
                else
                {
                    for (ex11 = 0; ex11 < numTOTexits; ex11++) // more than one EXIT place
                    {
                        output1X = (int)moreexits[ex11].exitMoreXY.X;
                        output1Y = (int)moreexits[ex11].exitMoreXY.Y;
                        exit_rect.X = output1X - 5;
                        exit_rect.Y = output1Y - 5;
                        exit_rect.Width = 10;
                        exit_rect.Height = 10;
                        if (exit_rect.Contains(x) && !lemming[actLEM2].Exit && !lemming[actLEM2].Explode)
                        {
                            lemming[actLEM2].PosX = output1X - 19; //14+5 middle of the exit rect
                            lemming[actLEM2].PosY = output1Y - 30; //25+5
                            lemming[actLEM2].Active = false;
                            lemming[actLEM2].Walker = false;
                            lemming[actLEM2].Fall = false;
                            lemming[actLEM2].Falling = false;
                            lemming[actLEM2].Exit = true;
                            lemming[actLEM2].Numframes = sale_frames;
                            lemming[actLEM2].Actualframe = 0; // break; //i'm not sure if it's necessary this break
                        }
                    }
                }
            }
        }
    }
}
