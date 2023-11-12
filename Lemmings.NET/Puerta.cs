using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Lemmings.NET
{
    partial class LemmingsNetGame : Game
    {
        private bool puertaon = true, allBlow = false, sacalem = false;
        private int puerta1x, puerta1y;
        private int salida1x, salida1y, ex11;
        private int framepuerta = 0, framesalida = 0; // 0--10   0--6
        private int exitFrame = 999, numerosaca = 0, numerofrecuencia = 50, numerominfrecuencia = 50; // frecuency lemmings go in
        private Rectangle salida_rect; // rectangle exit
        private Point x;
        private bool debug = false; // ACTIVE DEBUG MODE //be careful with spritebacht begin---end debug mode fails
        private const int numTotalLevels = 182;
        private void Puerta()
        {
            salida_rect.X = salida1x - 5;
            salida_rect.Y = salida1y - 5;
            salida_rect.Width = 10;
            salida_rect.Height = 10;
            if (dibuja2 && puertaon && Frame > 30)
            {
                tiempototal = 0;
                xx55 = varDoor[level[levelNumber].typeOfDoor].numFram - 1;
                framepuerta++;
                if (framepuerta == 1 && doorInstance.State == SoundState.Stopped && !doorwavOn)
                {
                    doorInstance.Play();
                    doorwavOn = true;
                }
                if (framepuerta > xx55)
                {
                    songInstance.IsLooped = true;
                    songInstance.Play();
                    puertaon = false;
                    framepuerta = xx55;
                }
            }
            sacalem = false;
            retardoporcien = 27 - numerofrecuencia * 0.26f; // see to fix speed of lemmings release on door only when change frecuency (not so good)
            if (dibuja && !puertaon)
            {
                exitFrame++;
                if (exitFrame >= (int)retardoporcien)
                {
                    exitFrame = 0;
                    sacalem = true;
                }
            }
            //test to see difference with anterior process
            if (sacalem && numerosaca != Numlems && !allBlow)
            {
                if (numTOTdoors > 1 && moredoors != null) // more than 1 door is different calculation
                {
                    puerta1y = (int)moredoors[numACTdoor].doormorexy.Y;
                    puerta1x = (int)moredoors[numACTdoor].doormorexy.X;
                    numACTdoor++;
                    if (numACTdoor >= numTOTdoors)
                        numACTdoor = 0;
                    lemming[numerosaca].Posy = puerta1y;
                    lemming[numerosaca].Posx = puerta1x + 35;
                }
                else
                {
                    lemming[numerosaca].Posy = puerta1y;
                    lemming[numerosaca].Posx = puerta1x + 35;
                }
                lemming[numerosaca].Posy = puerta1y;
                lemming[numerosaca].Posx = puerta1x + 35;
                lemming[numerosaca].Numframes = 0;
                lemming[numerosaca].Right = true;
                lemming[numerosaca].Fall = true;
                lemming[numerosaca].Walker = false;
                lemming[numerosaca].Pixelscaida = 0;
                lemming[numerosaca].Numframes = faller_frames;
                lemming[numerosaca].Actualframe = 0;
                lemming[numerosaca].Onmouse = false;
                lemming[numerosaca].Activo = true;
                lemming[numerosaca].Exit = false;
                lemming[numerosaca].Dead = false;
                lemming[numerosaca].Digger = false;
                lemming[numerosaca].Escalar = false;
                lemming[numerosaca].Escalando = false;
                lemming[numerosaca].Umbrella = false;
                lemming[numerosaca].Falling = false;
                lemming[numerosaca].Framescut = false;
                lemming[numerosaca].Breakfloor = false;
                lemming[numerosaca].Explota = false;
                lemming[numerosaca].Explotando = false;
                lemming[numerosaca].Time = 0;
                lemming[numerosaca].Blocker = false;
                lemming[numerosaca].Builder = false;
                lemming[numerosaca].Basher = false;
                lemming[numerosaca].Miner = false;
                lemming[numerosaca].Puentenomas = false;
                lemming[numerosaca].Quemado = false;
                lemming[numerosaca].Ahoga = false;
                numerosaca++;
                numlemnow++;
            }

            for (actLEM2 = 0; actLEM2 < numerosaca; actLEM2++)
            {
                x.X = lemming[actLEM2].Posx + 14;
                x.Y = lemming[actLEM2].Posy + 25;
                if (lemming[actLEM2].Exit && lemming[actLEM2].Actualframe == 13) // change frame of yipee sound, old frame was init or 0 now different for frames
                {
                    if (oingInstance.State == SoundState.Playing)
                    {
                        oingInstance.Stop();
                    }
                    try
                    {
                        oingInstance.Play();
                    }
                    catch (InstancePlayLimitException) { /* Ignore errors */ }
                }
                if (moreexits == null)
                {
                    if (salida_rect.Contains(x) && !lemming[actLEM2].Exit && !lemming[actLEM2].Explotando)
                    {
                        lemming[actLEM2].Posx = salida1x - 19;
                        lemming[actLEM2].Posy = salida1y - 30;
                        lemming[actLEM2].Activo = false;
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
                        salida1x = (int)moreexits[ex11].exitmorexy.X;
                        salida1y = (int)moreexits[ex11].exitmorexy.Y;
                        salida_rect.X = salida1x - 5;
                        salida_rect.Y = salida1y - 5;
                        salida_rect.Width = 10;
                        salida_rect.Height = 10;
                        if (salida_rect.Contains(x) && !lemming[actLEM2].Exit && !lemming[actLEM2].Explotando)
                        {
                            lemming[actLEM2].Posx = salida1x - 19; //14+5 middle of the exit rect
                            lemming[actLEM2].Posy = salida1y - 30; //25+5
                            lemming[actLEM2].Activo = false;
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
