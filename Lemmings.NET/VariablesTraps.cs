using Lemmings.NET.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET
{
    partial class LemmingsNetGame : Game
    {
        private void VariablesTraps()
        {
            switch (levelNumber)
            {
                case 1:
                    sprite = new Varsprites[6];
                    sprite[0].actFrame = 0;
                    sprite[0].axisX = 4;
                    sprite[0].axisY = 4;
                    sprite[0].depth = 0.806f;
                    sprite[0].R = 255;
                    sprite[0].G = 255;
                    sprite[0].B = 255;
                    sprite[0].transparency = 200;
                    sprite[0].pos = new Vector2(0, 0);
                    sprite[0].scale = 9f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[0].rotation = 0f;
                    sprite[0].framesecond = 4;
                    sprite[0].frame = 0;
                    sprite[0].sprite = Content.Load<Texture2D>("sprite/magma_mask");
                    sprite[0].minusScrollx = false;
                    sprite[1].actFrame = 0;
                    sprite[1].axisX = 8;
                    sprite[1].axisY = 8;
                    sprite[1].depth = 0.406f;
                    sprite[1].R = 255;
                    sprite[1].G = 255;
                    sprite[1].B = 255;
                    sprite[1].transparency = 255;
                    sprite[1].pos = new Vector2(1188, 337); //340
                    sprite[1].scale = 0.35f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[1].rotation = 0.1f;
                    sprite[1].framesecond = 0;
                    sprite[1].frame = 0;
                    sprite[1].sprite = Content.Load<Texture2D>("sprite/flame");
                    sprite[1].minusScrollx = true;
                    sprite[2].actFrame = 0;
                    sprite[2].axisX = 8;
                    sprite[2].axisY = 8;
                    sprite[2].depth = 0.405f;
                    sprite[2].R = 255;
                    sprite[2].G = 225;
                    sprite[2].B = 225;
                    sprite[2].transparency = 255;
                    sprite[2].pos = new Vector2(1136, 337);
                    sprite[2].scale = 0.35f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[2].rotation = 0.05f;
                    sprite[2].framesecond = 0;
                    sprite[2].frame = 0;
                    sprite[2].sprite = Content.Load<Texture2D>("sprite/flame");
                    sprite[2].minusScrollx = true;
                    sprite[3].actFrame = 0;
                    sprite[3].axisX = 6;
                    sprite[3].axisY = 1;
                    sprite[3].depth = 0.405f;
                    sprite[3].R = 255;
                    sprite[3].G = 225;
                    sprite[3].B = 225;
                    sprite[3].transparency = 255;
                    sprite[3].pos = new Vector2(0, 0);
                    sprite[3].scale = 0.5f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[3].rotation = 0f;
                    sprite[3].framesecond = 1;
                    sprite[3].frame = 0;
                    sprite[3].sprite = Content.Load<Texture2D>("touch/arana");
                    sprite[3].calc = true;
                    sprite[3].minusScrollx = true;
                    sprite[3].dest = new Vector2(0, 0);
                    sprite[3].speed = 0.578f;  // this field is important for move logic of sprites != 0
                    sprite[3].actVect = 0;
                    sprite[3].center.X = ((sprite[3].sprite.Width / sprite[3].axisX) / 2);
                    sprite[3].center.Y = ((sprite[3].sprite.Height / sprite[3].axisY) / 2);
                    sprite[3].path = new Vector3[7];
                    sprite[3].path[0] = new Vector3(48, 65, 1.5f);
                    sprite[3].path[1] = new Vector3(200, 140, 1.7f);
                    sprite[3].path[2] = new Vector3(238, 139, 1.9f);
                    sprite[3].path[3] = new Vector3(146, 407, 1.6f);
                    sprite[3].path[4] = new Vector3(326, 475, 2f);
                    sprite[3].path[5] = new Vector3(405, 322, 1.2f);
                    sprite[3].path[6] = new Vector3(470, 211, 1.5f);
                    sprite[4].actFrame = 0;
                    sprite[4].axisX = 2;
                    sprite[4].axisY = 10;
                    sprite[4].depth = 0.505f;
                    sprite[4].R = 255;
                    sprite[4].G = 225;
                    sprite[4].B = 225;
                    sprite[4].transparency = 255;
                    sprite[4].pos = new Vector2(120, -190);
                    sprite[4].scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[4].rotation = 1.57f;
                    sprite[4].framesecond = 2;
                    sprite[4].frame = 0;
                    sprite[4].sprite = Content.Load<Texture2D>("touch/fire_sprites_other");
                    sprite[4].minusScrollx = false;
                    sprite[4].minus = false;
                    sprite[5].calc = true;
                    sprite[5].actFrame = 0;
                    sprite[5].axisX = 6;
                    sprite[5].axisY = 1;
                    sprite[5].depth = 0.405f;
                    sprite[5].R = 255;
                    sprite[5].G = 225;
                    sprite[5].B = 225;
                    sprite[5].transparency = 255;
                    sprite[5].pos = new Vector2(0, 0);
                    sprite[5].scale = 0.3f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[5].rotation = 0f;
                    sprite[5].framesecond = 2;
                    sprite[5].frame = 0;
                    sprite[5].sprite = Content.Load<Texture2D>("touch/arana");
                    sprite[5].minusScrollx = true;
                    sprite[5].dest = new Vector2(0, 0);
                    sprite[5].speed = 0.578f;  // this field is important for move logic of sprites != 0
                    sprite[5].actVect = 0;
                    sprite[5].center.X = ((sprite[3].sprite.Width / sprite[3].axisX) / 2);
                    sprite[5].center.Y = ((sprite[3].sprite.Height / sprite[3].axisY) / 2);
                    sprite[5].path = new Vector3[6];
                    sprite[5].path[0] = new Vector3(1000, 5, 1.5f);
                    sprite[5].path[1] = new Vector3(1090, 95, 1.7f);
                    sprite[5].path[2] = new Vector3(1069, 252, 1.9f);
                    sprite[5].path[3] = new Vector3(1173, 300, 1.6f);
                    sprite[5].path[4] = new Vector3(1241, 138, 2f);
                    sprite[5].path[5] = new Vector3(1300, 5, 1.2f);

                    break;
                case 4:
                    sprite = new Varsprites[1];
                    sprite[0].actFrame = 0;
                    sprite[0].axisX = 4;
                    sprite[0].axisY = 4;
                    sprite[0].depth = 0.806f;
                    sprite[0].R = 255;
                    sprite[0].G = 255;
                    sprite[0].B = 255;
                    sprite[0].transparency = 200;
                    sprite[0].pos = new Vector2(0, 0);
                    sprite[0].scale = 9f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[0].rotation = 0f;
                    sprite[0].framesecond = 4;
                    sprite[0].frame = 0;
                    sprite[0].sprite = Content.Load<Texture2D>("sprite/magma_mask");
                    sprite[0].minusScrollx = false;
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(820, 462, 1529 - 820, 40); // 512-40=462 bottom of the screen
                    trap[0].areaTrap = new Rectangle(820, 467, 1529 - 820, 10); //normally +5 on Y and 10 of height
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/acid");
                    break;
                case 5:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1; // kind of variable scroll the trap 1=z1--
                    trap[0].areaDraw = new Rectangle(510, 480, 300, 32); //512-32=480 bottom of the screen
                    trap[0].areaTrap = new Rectangle(510, 485, 300, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/ice_water");
                    trap[1].vvscroll = 2; // kind of variable scroll the trap 1=z1 -- 2=-z1 --
                    trap[1].areaDraw = new Rectangle(518, 460, 280, 32);
                    trap[1].areaTrap = new Rectangle(518, 465, 280, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 130;
                    trap[1].sprite = Content.Load<Texture2D>("traps/ice_water");
                    break;
                case 6:
                case 47:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 2;
                    trap[0].areaDraw = new Rectangle(320, 472, 2189 - 320, 40);
                    trap[0].areaTrap = new Rectangle(320, 477, 2189 - 320, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 160;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1162 - 159 + 30, 108 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1162, 108);
                    trap[1].vvX = 159;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.400000009f;
                    trap[1].transparency = 200;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego4");
                    break;
                case 7:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(192, 472, 3220 - 192, 40);
                    trap[0].areaTrap = new Rectangle(192, 477, 3220 - 192, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/acid");
                    break;
                case 8:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(325, 480, 3500 - 325, 32);
                    trap[0].areaTrap = new Rectangle(325, 485, 3500 - 325, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 9:
                case 56:
                    NumTotTraps = 4;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 462, gameResolution.X, 40);
                    trap[0].areaTrap = new Rectangle(0, 470, gameResolution.X, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/acid");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(507 - 23, 202 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(507, 202);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(852 - 23, 399 - 38 / 2, 23 * 2, 10); //see .pos
                    trap[2].numFrames = 16;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(852, 399);
                    trap[2].vvX = 32;
                    trap[2].vvY = 38;
                    trap[2].depth = 0.200000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(164 - 23, 393 - 38 / 2, 23 * 2, 10);
                    trap[3].numFrames = 16;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(164, 393);
                    trap[3].vvX = 32;
                    trap[3].vvY = 38;
                    trap[3].depth = 0.200000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 10:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(380, 490, 1843 - 380, 32);
                    trap[0].areaTrap = new Rectangle(380, 495, 1843 - 380, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].G = 150;
                    trap[0].B = 20;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 480, 2303, 32);
                    trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.400000009f;
                    trap[1].G = 20;
                    trap[1].B = 20;
                    trap[1].transparency = 170;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 11:
                case 78:
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = false;
                    arrow[0].area = new Rectangle(468, 161, 773 - 468, 440 - 161);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(239, 440, 1227 - 239, 512 - 440);
                    break;
                case 13:
                case 101:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 472, 3203, 40);
                    trap[0].areaTrap = new Rectangle(0, 477, 3203, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 14:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(180, 480, 2884 - 180, 32);
                    trap[0].areaTrap = new Rectangle(180, 485, 2884 - 180, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 15:
                case 61:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(3317 - 5, 455 - 5, 10, 10);//see .pos minus 5 on both axis
                    trap[0].numFrames = 37;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(3317, 455);
                    trap[0].vvX = 10;
                    trap[0].vvY = 71;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_soga");
                    break;
                case 16:
                case 63:
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 472, 2205, 512);
                    trap[0].areaTrap = new Rectangle(0, 475, 2205, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(0, 0);
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(845 - 30, 250 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(845, 250);
                    trap[1].vvX = 30;
                    trap[1].vvY = 30;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(895 - 30, 250 - 30 / 2, 30 * 2, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(895, 250);
                    trap[2].vvX = 30;
                    trap[2].vvY = 30;
                    trap[2].depth = 0.600000008f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    break;
                case 17:
                case 66:
                    numTOTdoors = 4; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[17].doorX, level[17].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(level[17].doorX + 220, level[17].doorY);
                    moreDoors[2].doorMoreXY = new Vector2(level[17].doorX + 430, level[17].doorY);
                    moreDoors[3].doorMoreXY = new Vector2(level[17].doorX + 640, level[17].doorY);//IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                    SteelON = true; numTOTsteel = 2;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(261, 373, 1154 - 261, 450 - 373);
                    steel[1].area = new Rectangle(997, 284, 1153 - 997, 370 - 284);
                    NumTotTraps = 4;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(391 - 5, 497 - 5, 10, 10);
                    trap[0].numFrames = 15;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(391, 497);
                    trap[0].vvX = 31;
                    trap[0].vvY = 57;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_marble");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(590 - 5, 497 - 5, 10, 10);
                    trap[1].numFrames = 15;
                    trap[1].actFrame = 0;
                    trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(590, 497);
                    trap[1].vvX = 31;
                    trap[1].vvY = 57;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_marble");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(792 - 5, 497 - 5, 10, 10);
                    trap[2].numFrames = 15;
                    trap[2].actFrame = 0;
                    trap[2].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(792, 497);
                    trap[2].vvX = 31;
                    trap[2].vvY = 57;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_marble");
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(998 - 5, 497 - 5, 10, 10);
                    trap[3].numFrames = 15;
                    trap[3].actFrame = 0;
                    trap[3].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(998, 497);
                    trap[3].vvX = 31;
                    trap[3].vvY = 57;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/dead_marble");
                    break;
                case 18:
                case 79:
                    NumTotTraps = 6;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(875 - 30, 454 - 30 / 2, 30 * 2, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(875, 454);
                    trap[0].vvX = 30;
                    trap[0].vvY = 30;
                    trap[0].depth = 0.600000008f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(460 - 30 + 4, 162 - 30 / 2, 30 * 2 - 4, 10);//see .pos
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(460, 162);
                    trap[1].vvX = 30;
                    trap[1].vvY = 30;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(890 - 30 + 4, 158 - 30 / 2, 30 * 2 - 4, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(890, 158);
                    trap[2].vvX = 30;
                    trap[2].vvY = 30;
                    trap[2].depth = 0.600000008f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(587 - 30 + 4, 251 - 30 / 2, 30 * 2 - 4, 10);
                    trap[3].numFrames = 8;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(587, 251);
                    trap[3].vvX = 30;
                    trap[3].vvY = 30;
                    trap[3].depth = 0.600000008f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[4].areaTrap = new Rectangle(957 - 30 + 4, 312 - 30 / 2, 30 * 2 - 4, 10);
                    trap[4].numFrames = 8;
                    trap[4].actFrame = 0;
                    trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[4].isOn = false;
                    trap[4].pos = new Vector2(957, 312);
                    trap[4].vvX = 30;
                    trap[4].vvY = 30;
                    trap[4].depth = 0.600000008f;
                    trap[4].transparency = 255;
                    trap[4].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[5].areaTrap = new Rectangle(529 - 30 + 4, 377 - 30 / 2, 30 * 2 - 4, 10);
                    trap[5].numFrames = 8;
                    trap[5].actFrame = 0;
                    trap[5].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[5].isOn = false;
                    trap[5].pos = new Vector2(529, 377);
                    trap[5].vvX = 30;
                    trap[5].vvY = 30;
                    trap[5].depth = 0.600000008f;
                    trap[5].transparency = 255;
                    trap[5].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    break;
                case 20:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(127, 462, 3638 - 127, 40); // 512-40=462 bottom of the screen
                    trap[0].areaTrap = new Rectangle(127, 467, 3638 - 127, 10); //normally +5 on Y and 10 of height
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/acid");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1121 - 23, 376 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1121, 376);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 21:
                case 116:
                    NumTotTraps = 4;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 470, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 470, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(1001, 480, 1133 - 1001, 32);
                    trap[1].areaTrap = new Rectangle(1001, 485, 1133 - 1001, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 170;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[2].areaDraw = new Rectangle(3143, 480, 3757 - 3143, 32);
                    trap[2].areaTrap = new Rectangle(3143, 485, 3757 - 3143, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 170;
                    trap[2].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(2510 - 5, 473 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[3].numFrames = 15;
                    trap[3].actFrame = 0;
                    trap[3].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(2510, 473);
                    trap[3].vvX = 16;
                    trap[3].vvY = 42;  //38
                    trap[3].depth = 0.400000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/dead_trampa");
                    break;
                case 23:
                    numTOTexits = 2;
                    moreexits = new Varmoreexits[numTOTexits];
                    moreexits[0].exitMoreXY = new Vector2(level[23].exitX, level[23].exitY); //73,460 ----- LEVEL 23 TWO EXITS
                    moreexits[1].exitMoreXY = new Vector2(level[23].exitX, 180);//73,180 //IMPORTANT LEVEL[??] SAME AS CASE: FOR FUTURE BUGS
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(2475, 472, 2594 - 2475, 40);
                    trap[0].areaTrap = new Rectangle(2475, 477, 2594 - 2475, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(606 - 159 + 30, 308 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(606, 308);
                    trap[1].vvX = 159;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego4");
                    break;
                case 24:
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = true;
                    arrow[0].area = new Rectangle(754, 143, 860 - 754, 216 - 143);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(862, 129, 1304 - 862, 306 - 129);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1477 - 5, 345 - 5, 10, 10);
                    trap[0].numFrames = 15;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1477, 345);
                    trap[0].vvX = 47;
                    trap[0].vvY = 87;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_marble2_fix");
                    trap[1].areaDraw = new Rectangle(1071, 225, 1266 - 1071, 40); // 512-40=462 bottom of the screen
                    trap[1].areaTrap = new Rectangle(1071, 230, 1266 - 1071, 10); //normally +5 on Y and 10 of height
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/acid");
                    break;
                case 26:
                case 103:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(2797 - 5, 245 - 5, 10, 10);//see .pos minus 5 on both axis
                    trap[0].numFrames = 37;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(2797, 245);
                    trap[0].vvX = 10;
                    trap[0].vvY = 71;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_soga");
                    trap[1].areaDraw = new Rectangle(0, 480, 3007, 32);
                    trap[1].areaTrap = new Rectangle(0, 485, 3007, 10);// not necessary
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.400000009f;
                    trap[1].transparency = 170;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 27:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 487, 2624, 40);
                    trap[0].areaTrap = new Rectangle(0, 492, 2624, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/acid");
                    break;
                case 28:
                case 95:
                    NumTotTraps = 4;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(253, 472, 861 - 253, 40);
                    trap[0].areaTrap = new Rectangle(253, 477, 861 - 253, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].vvscroll = 2;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1334 - 159 + 30, 192 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1334, 192);
                    trap[1].vvX = 159;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego4");
                    trap[2].areaDraw = new Rectangle(2038, 472, 2622 - 2038, 40);
                    trap[2].areaTrap = new Rectangle(2038, 477, 2622 - 2038, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].vvscroll = 1;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(1495, 192 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[3].numFrames = 10;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(1495, 192);
                    trap[3].vvX = 0;
                    trap[3].vvY = 27;
                    trap[3].depth = 0.600000009f;
                    trap[3].B = 160;
                    trap[3].G = 160;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/fuego3");
                    break;
                case 29:
                case 99:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(1812, 480, 2205 - 1812, 32);
                    trap[0].areaTrap = new Rectangle(1812, 485, 2205 - 1812, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.200000009f; //lemmings depth 0.300f
                    trap[0].transparency = 190;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 30:
                case 114:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(71, 472, 1150 - 71, 40);
                    trap[0].areaTrap = new Rectangle(71, 477, 1150 - 71, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 230;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                //tricky levels tricky
                case 31:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(0, 480, 3022, 32);
                    trap[0].areaTrap = new Rectangle(0, 480, 3022, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 33:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 2;
                    trap[0].areaDraw = new Rectangle(370, 480, 1479 - 370, 32);
                    trap[0].areaTrap = new Rectangle(370, 480, 1479 - 370, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 34:
                case 67:
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = false;
                    arrow[0].area = new Rectangle(1063, 45, 1214 - 1063, 284 - 45);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 190;
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(84, 284, 1682 - 84, 330 - 284);
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 3030, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 3030, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 35:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(728, 480, 3277 - 728, 32);
                    trap[0].areaTrap = new Rectangle(728, 485, 3277 - 728, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 36:
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 490, 2088, 512); //special size
                    trap[0].areaTrap = new Rectangle(0, 495, 2088, 10);
                    trap[0].vvscroll = 2;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(0, 0);
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].B = 130;
                    trap[0].G = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(444, 341 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(444, 341);
                    trap[1].vvX = 0;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(1761 - 159 + 30, 251 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[2].numFrames = 10;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(1761, 251);
                    trap[2].vvX = 159;
                    trap[2].vvY = 27;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego4");
                    break;
                case 37:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 1281, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 1281, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 120;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 38:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 472, 2088, 40); //normal size
                    trap[0].areaTrap = new Rectangle(0, 482, 2088, 10);
                    trap[0].vvscroll = 2;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(0, 0);
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].B = 130;
                    trap[0].G = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 39:
                case 96:
                    ArrowsON = true; NumTotArrow = 2;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = true;
                    arrow[0].area = new Rectangle(685, 336, 781 - 685, 421 - 336);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    arrow[1].right = false;
                    arrow[1].area = new Rectangle(2466, 110, 2577 - 2466, 213 - 110); // mask texture full steel zone
                    arrow[1].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[1].flechassobre = new Texture2D(GraphicsDevice, arrow[1].area.Width, arrow[1].area.Height);
                    arrow[1].desplaza = 0;
                    arrow[1].transparency = 255;
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 3153, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 3153, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 120;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    SteelON = true; numTOTsteel = 2;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(553, 216, 2775 - 553, 330 - 216);
                    steel[1].area = new Rectangle(2698, 144, 2851 - 2698, 216 - 144);
                    break;
                case 40:
                case 105:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[40].doorX, level[40].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(2240, level[40].doorY);
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 472, 2853, 40); //normal size
                    trap[0].areaTrap = new Rectangle(0, 482, 2853, 10);
                    trap[0].vvscroll = 1;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(0, 0);
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].B = 80;
                    trap[0].G = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 41:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(1027, 480, 1553 - 1027, 32);
                    trap[0].areaTrap = new Rectangle(1027, 485, 1553 - 1027, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 43:
                case 115:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 441, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 441, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 49:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(0, 490, 3391, 32);
                    trap[0].areaTrap = new Rectangle(0, 495, 3391, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 50:
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = false;
                    arrow[0].area = new Rectangle(1318, 164, 1478 - 1318, 460 - 164);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    NumTotTraps = 5;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(217, 411 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(217, 411);
                    trap[0].vvX = 0;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(754 - 30, 160 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(754, 160);
                    trap[1].vvX = 30;
                    trap[1].vvY = 30;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(801 - 30, 160 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(801, 160);
                    trap[2].vvX = 30;
                    trap[2].vvY = 30;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(1770 - 30, 169 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[3].numFrames = 8;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(1770, 169);
                    trap[3].vvX = 30;
                    trap[3].vvY = 30;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[4].areaTrap = new Rectangle(1821 - 30, 169 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[4].numFrames = 8;
                    trap[4].actFrame = 0;
                    trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[4].isOn = false;
                    trap[4].pos = new Vector2(1821, 169);
                    trap[4].vvX = 30;
                    trap[4].vvY = 30;
                    trap[4].depth = 0.600000009f;
                    trap[4].transparency = 255;
                    trap[4].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    break;
                case 52:
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(384, 480, 3197 - 384, 32);
                    trap[0].areaTrap = new Rectangle(384, 485, 3197 - 384, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.4000000011f;
                    trap[0].transparency = 200;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 480, 278, 32);
                    trap[1].areaTrap = new Rectangle(0, 485, 278, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.4000000011f;
                    trap[1].transparency = 200;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[2].areaDraw = new Rectangle(3197, 480, 3649 - 3197, 32);
                    trap[2].areaTrap = new Rectangle(3197, 485, 3649 - 3197, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.6000000011f;
                    trap[2].transparency = 200;
                    trap[2].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 55:
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(2511 - 5, 334 - 5, 10, 10);//see .pos minus 5 on both axis
                    trap[0].numFrames = 37;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(2511, 334);
                    trap[0].vvX = 10;
                    trap[0].vvY = 71;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_soga");
                    trap[1].vvscroll = 1;
                    trap[1].areaDraw = new Rectangle(1300, 480, 4089 - 1300, 32);
                    trap[1].areaTrap = new Rectangle(1300, 485, 4089 - 1300, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.6000000011f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[2].vvscroll = 2;
                    trap[2].areaDraw = new Rectangle(0, 480, 787, 32);
                    trap[2].areaTrap = new Rectangle(0, 485, 787, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.6000000011f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 57:
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = false;
                    arrow[0].area = new Rectangle(1306, 35, 1805 - 1306, 318 - 35);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 2;
                    trap[0].areaDraw = new Rectangle(0, 480, 777, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 777, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 58:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(0, 480, 3117, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 3117, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(1780, 239, 2006 - 1780, 280 - 239);
                    break;
                case 59:
                    numTOTdoors = 3; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[59].doorX, level[59].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(459, 150);
                    moreDoors[2].doorMoreXY = new Vector2(770, 46);
                    NumTotTraps = 7;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(623 - 23, 429 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(623, 429);
                    trap[0].vvX = 32;
                    trap[0].vvY = 38;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1266 - 23, 451 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1266, 451);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(1591 - 23, 132 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[2].numFrames = 16;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(1591, 132);
                    trap[2].vvX = 32;
                    trap[2].vvY = 38;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(1604 - 23, 367 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[3].numFrames = 16;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(1604, 367);
                    trap[3].vvX = 32;
                    trap[3].vvY = 38;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[4].areaTrap = new Rectangle(2224 - 23, 153 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[4].numFrames = 16;
                    trap[4].actFrame = 0;
                    trap[4].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[4].isOn = false;
                    trap[4].pos = new Vector2(2224, 153);
                    trap[4].vvX = 32;
                    trap[4].vvY = 38;
                    trap[4].depth = 0.600000009f;
                    trap[4].transparency = 255;
                    trap[4].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[5].areaTrap = new Rectangle(2154 - 23, 332 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[5].numFrames = 16;
                    trap[5].actFrame = 0;
                    trap[5].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[5].isOn = false;
                    trap[5].pos = new Vector2(2154, 332);
                    trap[5].vvX = 32;
                    trap[5].vvY = 38;
                    trap[5].depth = 0.600000009f;
                    trap[5].transparency = 255;
                    trap[5].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[6].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[6].areaTrap = new Rectangle(2763 - 23, 334 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[6].numFrames = 16;
                    trap[6].actFrame = 0;
                    trap[6].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[6].isOn = false;
                    trap[6].pos = new Vector2(2763, 334);
                    trap[6].vvX = 32;
                    trap[6].vvY = 38;
                    trap[6].depth = 0.600000009f;
                    trap[6].transparency = 255;
                    trap[6].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 60:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 2;
                    trap[0].areaDraw = new Rectangle(0, 480, 3090, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 3090, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 224;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                // TAXING LEVELS TAXING //////////////////////////////////////////////////////////////////////////
                case 62:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[62].doorX, level[62].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(1962, level[62].doorY);
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(1614, 129, 1967 - 1614, 253 - 129);
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(2352 - 5, 157 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 15;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(2352, 157);
                    trap[0].vvX = 16;
                    trap[0].vvY = 42;  //38
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_trampa");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1052 - 5, 459 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 15;
                    trap[1].actFrame = 0;
                    trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1052, 459);
                    trap[1].vvX = 16;
                    trap[1].vvY = 42;  //38
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_trampa");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(2712 - 5, 134 - 5, 10, 10);
                    trap[2].numFrames = 12;
                    trap[2].actFrame = 0;
                    trap[2].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(2712, 134);
                    trap[2].vvX = 40;
                    trap[2].vvY = 105;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_10");
                    break;
                case 64:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[64].doorX, level[64].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(1174, level[64].doorY);
                    SteelON = true; numTOTsteel = 2;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(1000, 272, 1507 - 1000, 318 - 272);
                    steel[1].area = new Rectangle(1265, 319, 1309 - 1265, 442 - 319);
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1003 - 12, 175 - 18, 15, 36);
                    trap[0].numFrames = 7;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1003, 175);
                    trap[0].vvX = 30;
                    trap[0].vvY = 26;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_arrow_left");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(908, 272 - 18, 15, 36);
                    trap[1].numFrames = 7;
                    trap[1].actFrame = 0;
                    trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(908, 272);
                    trap[1].vvX = 0;
                    trap[1].vvY = 26;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_arrow_right");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(1574 - 5, 445 - 5, 10, 10);//see .pos minus 5 on both axis
                    trap[2].numFrames = 37;
                    trap[2].actFrame = 0;
                    trap[2].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(1574, 445);
                    trap[2].vvX = 10;
                    trap[2].vvY = 71;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_soga");
                    break;
                case 65:
                    numTOTexits = 2;
                    moreexits = new Varmoreexits[numTOTexits];
                    moreexits[0].exitMoreXY = new Vector2(level[65].exitX, level[65].exitY);
                    moreexits[1].exitMoreXY = new Vector2(level[65].exitX, 461);
                    SteelON = true; numTOTsteel = 2;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(161, 436, 390 - 161, 488 - 436);
                    steel[1].area = new Rectangle(537, 160, 610 - 537, 309 - 160);
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(611, 196 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(611, 196);
                    trap[0].vvX = 0;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(611, 274 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(611, 274);
                    trap[1].vvX = 0;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[2].vvscroll = 0;
                    trap[2].areaDraw = new Rectangle(0, 472, 1152, 40);
                    trap[2].areaTrap = new Rectangle(0, 477, 1152, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 68:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 3405, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 3405, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 69:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 771, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 771, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(1448, 480, 2304 - 1448, 32);
                    trap[1].areaTrap = new Rectangle(1448, 485, 2304 - 1448, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.400000009f; //lemmings depth 0.300f
                    trap[1].transparency = 200;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 70:
                    NumTotTraps = 5;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(439, 342 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(439, 342);
                    trap[0].vvX = 0;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1767 - 159 + 30, 217 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1767, 217);
                    trap[1].vvX = 159;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego4");
                    trap[2].vvscroll = 0;
                    trap[2].areaDraw = new Rectangle(0, 472, 1275, 40);
                    trap[2].areaTrap = new Rectangle(0, 477, 1275, 10);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[3].vvscroll = 0;
                    trap[3].areaDraw = new Rectangle(1435, 472, 2090 - 1435, 40);
                    trap[3].areaTrap = new Rectangle(1435, 477, 2090 - 1435, 10);
                    trap[3].numFrames = 8;
                    trap[3].actFrame = 0;
                    trap[3].type = 1;
                    trap[3].isOn = false;
                    trap[3].pos = Vector2.Zero;
                    trap[3].vvX = 0;
                    trap[3].vvY = 0;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[4].vvscroll = 0;
                    trap[4].areaDraw = new Rectangle(1281, 482, 1429 - 1281, 40);
                    trap[4].areaTrap = new Rectangle(1291, 497, 1, 1); //null kill area
                    trap[4].numFrames = 8;
                    trap[4].actFrame = 0;
                    trap[4].type = 1;
                    trap[4].isOn = false;
                    trap[4].pos = Vector2.Zero;
                    trap[4].vvX = 0;
                    trap[4].vvY = 0;
                    trap[4].depth = 0.600000009f;
                    trap[4].transparency = 255;
                    trap[4].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 71:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 3604, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 3604, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].vvscroll = 2;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f; //lemmings depth 0.300f
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 72:
                    NumTotTraps = 6;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(0, 472, 3000, 40);
                    trap[0].areaTrap = new Rectangle(0, 477, 3000, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].vvscroll = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(752, 205 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(752, 205);
                    trap[1].vvX = 0;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(1336 - 30, 178 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(1336, 178);
                    trap[2].vvX = 30;
                    trap[2].vvY = 30;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(1386 - 30, 178 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[3].numFrames = 8;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(1386, 178);
                    trap[3].vvX = 30;
                    trap[3].vvY = 30;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[4].areaTrap = new Rectangle(1642 - 30, 119 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[4].numFrames = 8;
                    trap[4].actFrame = 0;
                    trap[4].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[4].isOn = false;
                    trap[4].pos = new Vector2(1642, 119);
                    trap[4].vvX = 30;
                    trap[4].vvY = 30;
                    trap[4].depth = 0.600000009f;
                    trap[4].transparency = 255;
                    trap[4].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[5].areaTrap = new Rectangle(1681 - 30, 119 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[5].numFrames = 8;
                    trap[5].actFrame = 0;
                    trap[5].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[5].isOn = false;
                    trap[5].pos = new Vector2(1691, 119);
                    trap[5].vvX = 30;
                    trap[5].vvY = 30;
                    trap[5].depth = 0.600000009f;
                    trap[5].transparency = 255;
                    trap[5].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    break;
                case 73:
                    ArrowsON = true; NumTotArrow = 2;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = true;
                    arrow[0].area = new Rectangle(1737, 121, 1932 - 1737, 326 - 121);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow2");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    arrow[1].right = true;
                    arrow[1].area = new Rectangle(478, 42, 631 - 478, 374 - 42); // mask texture full steel zone
                    arrow[1].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[1].flechassobre = new Texture2D(GraphicsDevice, arrow[1].area.Width, arrow[1].area.Height);
                    arrow[1].desplaza = 0;
                    arrow[1].transparency = 255;
                    break;
                case 74:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 4002, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 4002, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].vvscroll = 1;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.400000009f; //lemmings depth 0.300f
                    trap[0].transparency = 150;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 76:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(994, 472, 2544 - 994, 40);
                    trap[0].areaTrap = new Rectangle(994, 477, 2544 - 994, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvscroll = 2;
                    trap[0].R = 140;
                    trap[0].G = 120;
                    trap[0].B = 190;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 77:
                    numTOTexits = 2;
                    moreexits = new Varmoreexits[numTOTexits];
                    moreexits[0].exitMoreXY = new Vector2(level[77].exitX, level[77].exitY);
                    moreexits[1].exitMoreXY = new Vector2(level[77].exitX, 180);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(2475, 472, 2594 - 2475, 40);
                    trap[0].areaTrap = new Rectangle(2475, 477, 2594 - 2475, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(606 - 159 + 30, 308 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(606, 308);
                    trap[1].vvX = 159;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego4");
                    break;
                case 81:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(963 - 159 + 30, 358 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(963, 358);
                    trap[0].vvX = 159;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego4");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(487, 358 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(487, 358);
                    trap[1].vvX = 0;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego3");
                    break;
                case 83:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(0, 472, 1281, 40);
                    trap[0].areaTrap = new Rectangle(0, 477, 1281, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvscroll = 1;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 84:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(0, 472, 3283, 40);
                    trap[0].areaTrap = new Rectangle(0, 477, 3283, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvscroll = 1;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 86:
                    numTOTdoors = 3; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[86].doorX, level[86].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(930, 382);
                    moreDoors[2].doorMoreXY = new Vector2(500, 56);
                    NumTotTraps = 4;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(297, 472, 836 - 297, 512);
                    trap[0].areaTrap = new Rectangle(297, 475, 836 - 297, 10);
                    trap[0].vvscroll = 1;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(0, 0);
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(930 - 30, 128 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(930, 128);
                    trap[1].vvX = 30;
                    trap[1].vvY = 30;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(972 - 30, 128 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(972, 128);
                    trap[2].vvX = 30;
                    trap[2].vvY = 30;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(88, 129 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[3].numFrames = 10;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(88, 129);
                    trap[3].vvX = 0;
                    trap[3].vvY = 27;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/fuego3");
                    break;
                case 87:
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 2083, 32); //512-32=480 bottom of the screen
                    trap[0].areaTrap = new Rectangle(0, 485, 2083, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.300000009f;
                    trap[0].transparency = 230;
                    trap[0].sprite = Content.Load<Texture2D>("traps/ice_water");
                    trap[1].vvscroll = 2;
                    trap[1].areaDraw = new Rectangle(102, 126 - 32, 426 - 102, 32);
                    trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 170;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[2].vvscroll = 1;
                    trap[2].areaDraw = new Rectangle(1640, 126 - 32, 1965 - 1640, 32);
                    trap[2].areaTrap = new Rectangle(0, 0, 0, 0);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.6000000011f;
                    trap[2].transparency = 170;
                    trap[2].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 88:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(2425 - 5, 238 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(2425, 238);
                    trap[0].vvX = 33;
                    trap[0].vvY = 25;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_laser");
                    trap[1].vvscroll = 2;
                    trap[1].areaDraw = new Rectangle(0, 480, 3517, 32);
                    trap[1].areaTrap = new Rectangle(0, 485, 3517, 10);// not necessary
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 89:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(881, 480, 1481 - 881, 32);
                    trap[0].areaTrap = new Rectangle(881, 485, 1481 - 881, 10);// not necessary
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 90:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 472, 3207, 40);
                    trap[0].areaTrap = new Rectangle(0, 477, 3207, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                // MAYHEM LEVELS MAYHEM /////////////////////////////////////////////////////////////////////
                case 92:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(830, 219 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(830, 219);
                    trap[0].vvX = 0;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[1].areaDraw = new Rectangle(1318, 472, 1800 - 1318, 40);
                    trap[1].areaTrap = new Rectangle(1318, 477, 1800 - 1318, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 93:
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = true;
                    arrow[0].area = new Rectangle(754, 143, 860 - 754, 216 - 143);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(862, 129, 1304 - 862, 306 - 129);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1477 - 5, 345 - 5, 10, 10);
                    trap[0].numFrames = 15;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1477, 345);
                    trap[0].vvX = 47;
                    trap[0].vvY = 87;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_marble2_fix");
                    trap[1].areaDraw = new Rectangle(1071, 233, 1266 - 1071, 32); // 512-40=462 bottom of the screen
                    trap[1].areaTrap = new Rectangle(1071, 238, 1266 - 1071, 10); //normally +5 on Y and 10 of height
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 97:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(127, 480, 3638 - 127, 32); // 512-40=462 bottom of the screen
                    trap[0].areaTrap = new Rectangle(127, 485, 3638 - 127, 10); //normally +5 on Y and 10 of height
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1121 - 23, 376 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1121, 376);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 98:
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = false;
                    arrow[0].area = new Rectangle(1006, 50, 1166 - 1006, 393 - 50);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(1006, 398, 1105 - 1006, 512 - 398);
                    break;
                case 102:
                    SteelON = true; numTOTsteel = 7;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(912, 46, 986 - 912, 380 - 46);
                    steel[1].area = new Rectangle(605, 231, 1295 - 605, 306 - 231);
                    steel[2].area = new Rectangle(449, 153, 676 - 449, 229 - 153);
                    steel[3].area = new Rectangle(66, 155, 370 - 66, 228 - 155);
                    steel[4].area = new Rectangle(255, 306, 337 - 255, 379 - 306);
                    steel[5].area = new Rectangle(104, 303, 180 - 104, 381 - 303);
                    steel[6].area = new Rectangle(909, 438, 986 - 909, 512 - 428);
                    break;
                case 104:
                    SteelON = true; numTOTsteel = 3;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(742, 398, 1566 - 742, 467 - 398);
                    steel[1].area = new Rectangle(579, 323, 742 - 579, 440 - 323);
                    steel[2].area = new Rectangle(1447, 324, 1651 - 1447, 399 - 324);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(661, 330, 1576 - 661, 40);
                    trap[0].areaTrap = new Rectangle(661, 335, 1576 - 661, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/acid");
                    trap[1].vvscroll = 2;
                    trap[1].areaDraw = new Rectangle(661, 350, 1576 - 661, 40);
                    trap[1].areaTrap = new Rectangle(661, 355, 1576 - 661, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/acid");
                    break;
                case 106:
                case 117:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(334 - 23, 303 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(334, 303);
                    trap[0].vvX = 32;
                    trap[0].vvY = 38;
                    trap[0].depth = 0.200000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1484 - 23, 358 - 38 / 2, 23 * 2, 10); //see .pos
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1484, 358);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 107:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(0, 480, 2187, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 2187, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 108:
                    numTOTdoors = 4; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[108].doorX, level[108].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(1500, level[108].doorY);
                    moreDoors[2].doorMoreXY = new Vector2(level[108].doorX, 376);
                    moreDoors[3].doorMoreXY = new Vector2(1500, 376);
                    break;
                case 113:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(1341, 480, 2508 - 1341, 32);
                    trap[0].areaTrap = new Rectangle(1341, 485, 2508 - 1341, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].vvscroll = 1;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 118:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1975 - 5, 285 - 5, 10, 10);
                    trap[0].numFrames = 15;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1975, 285);
                    trap[0].vvX = 47;
                    trap[0].vvY = 87;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_marble2_fix");
                    trap[1].areaDraw = new Rectangle(0, 480, 3922, 32); // 512-40=462 bottom of the screen
                    trap[1].areaTrap = new Rectangle(0, 485, 3922, 10); //normally +5 on Y and 10 of height
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 120:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[120].doorX, level[120].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(4094, level[120].doorY);
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1589 - 5, 434 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 15;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1589, 434);
                    trap[0].vvX = 16;
                    trap[0].vvY = 42;  //38
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_trampa");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(368 - 5, 474 - 5, 10, 10);
                    trap[1].numFrames = 12;
                    trap[1].actFrame = 0;
                    trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(368, 474);
                    trap[1].vvX = 40;
                    trap[1].vvY = 105;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_10");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(3301 - 5, 453 - 5, 10, 10);
                    trap[2].numFrames = 12;
                    trap[2].actFrame = 0;
                    trap[2].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(3301, 453);
                    trap[2].vvX = 40;
                    trap[2].vvY = 105;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_10");
                    break;
                //bonus levels bonus ////////////////////////////////////////////////////////////////////////////////////////
                case 124:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1341 - 23, 324 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1341, 324);
                    trap[0].vvX = 32;
                    trap[0].vvY = 38;
                    trap[0].depth = 0.200000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(936 - 23, 355 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(936, 355);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 126:
                    ArrowsON = true; NumTotArrow = 2;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = false;
                    arrow[0].area = new Rectangle(605, 0, 700 - 605, 245);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    arrow[1].right = false;
                    arrow[1].area = new Rectangle(952, 0, 1047 - 952, 245); // mask texture full steel zone
                    arrow[1].flechas = Content.Load<Texture2D>("fondos/arrow2");
                    arrow[1].flechassobre = new Texture2D(GraphicsDevice, arrow[1].area.Width, arrow[1].area.Height);
                    arrow[1].desplaza = 0;
                    arrow[1].transparency = 255;
                    break;
                case 127:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 2; // kind of variable scroll the trap 1=z1--
                    trap[0].areaDraw = new Rectangle(0, 480, 2148, 32); //512-32=480 bottom of the screen
                    trap[0].areaTrap = new Rectangle(0, 485, 2148, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.300000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/ice_water");
                    break;
                case 130:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1016 - 5, 213 - 5, 10, 10);//see .pos minus 5 on both axis
                    trap[0].numFrames = 37;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1016, 213);
                    trap[0].vvX = 10;
                    trap[0].vvY = 71;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_soga");
                    break;
                case 132:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 2; // kind of variable scroll the trap 1=z1--
                    trap[0].areaDraw = new Rectangle(0, 480, 3909, 32); //512-32=480 bottom of the screen
                    trap[0].areaTrap = new Rectangle(0, 485, 3909, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.300000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/ice_water");
                    trap[1].vvscroll = 1; // kind of variable scroll the trap 1=z1--
                    trap[1].areaDraw = new Rectangle(0, 485, 3909, 32); //512-32=480 bottom of the screen
                    trap[1].areaTrap = new Rectangle(0, 490, 3909, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/ice_water");
                    break;
                case 133:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 0;
                    trap[0].areaDraw = new Rectangle(395, 472, 1647 - 395, 40);
                    trap[0].areaTrap = new Rectangle(395, 477, 1647 - 395, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    break;
                case 134:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[134].doorX, level[134].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(160, 92);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1080 - 12, 406 - 18, 15, 36);
                    trap[0].numFrames = 7;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1080, 406);
                    trap[0].vvX = 30;
                    trap[0].vvY = 26;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_arrow_left");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(144, 118 - 18, 15, 36);
                    trap[1].numFrames = 7;
                    trap[1].actFrame = 0;
                    trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(144, 118);
                    trap[1].vvX = 0;
                    trap[1].vvY = 26;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_arrow_right");
                    break;
                case 136:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(172, 490, 1843 - 172, 32);
                    trap[0].areaTrap = new Rectangle(172, 495, 1843 - 172, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].R = 20;
                    trap[0].B = 20;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].vvscroll = 2;
                    trap[1].areaDraw = new Rectangle(0, 480, 2049, 32);
                    trap[1].areaTrap = new Rectangle(0, 0, 0, 0);// not necessary
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].R = 20;
                    trap[1].B = 20;
                    trap[1].transparency = 170;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 137:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1146 - 159 + 30, 235 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1146, 235);
                    trap[0].vvX = 159;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego4");
                    break;
                case 138:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[138].doorX, level[138].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(level[138].doorX - 300, level[138].doorY);
                    //moredoors[1].doormorexy = new Vector2(1110,220); TEST THIS OPTION -- BASHER TO LEFT FAILS??????
                    ArrowsON = true; NumTotArrow = 1;
                    arrow = new Vararrows[NumTotArrow];
                    arrow[0].right = false;
                    arrow[0].area = new Rectangle(961, 219, 1011 - 961, 409 - 219);
                    arrow[0].flechas = Content.Load<Texture2D>("fondos/arrow1");
                    arrow[0].flechassobre = new Texture2D(GraphicsDevice, arrow[0].area.Width, arrow[0].area.Height);
                    arrow[0].desplaza = 0;
                    arrow[0].transparency = 255;
                    break;
                case 139:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(2208 - 5, 430 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 15;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(2208, 430);
                    trap[0].vvX = 16;
                    trap[0].vvY = 42;  //38
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_trampa");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(2843 - 5, 430 - 5, 10, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 15;
                    trap[1].actFrame = 0;
                    trap[1].type = 666; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(2843, 430);
                    trap[1].vvX = 16;
                    trap[1].vvY = 42;  //38
                    trap[1].depth = 0.400000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_trampa");
                    break;
                case 140:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(133, 155 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(133, 155);
                    trap[0].vvX = 0;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.400000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego3");
                    break;
                case 141:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[141].doorX, level[141].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(level[141].doorX + 400, level[141].doorY);
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(671 - 23, 184 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(671, 184);
                    trap[0].vvX = 32;
                    trap[0].vvY = 38;
                    trap[0].depth = 0.200000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 143:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 1632, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 1632, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 144:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(134, 326);
                    moreDoors[1].doorMoreXY = new Vector2(level[144].doorX, level[144].doorY);
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(624 - 23, 461 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(624, 461);
                    trap[0].vvX = 32;
                    trap[0].vvY = 38;
                    trap[0].depth = 0.200000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 145:
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(974 - 23, 229 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(974, 229);
                    trap[0].vvX = 32;
                    trap[0].vvY = 38;
                    trap[0].depth = 0.200000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(110 - 23, 357 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(110, 357);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(970 - 23, 357 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[2].numFrames = 16;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(970, 357);
                    trap[2].vvX = 32;
                    trap[2].vvY = 38;
                    trap[2].depth = 0.200000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 147:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(79, 480, 2489 - 79, 32);
                    trap[0].areaTrap = new Rectangle(79, 485, 2489 - 79, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].G = 130;
                    trap[0].R = 130;
                    trap[0].B = 180;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 200;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 149:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, 1794, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, 1794, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1022 - 23, 301 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1022, 301);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 150:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(690 - 5, 467 - 5, 10, 10);//see .pos minus 5 on both axis
                    trap[0].numFrames = 37;
                    trap[0].actFrame = 0;
                    trap[0].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(690, 467);
                    trap[0].vvX = 10;
                    trap[0].vvY = 71;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_soga");

                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1328 - 5, 427 - 5, 10, 10);//see .pos minus 5 on both axis
                    trap[1].numFrames = 37;
                    trap[1].actFrame = 0;
                    trap[1].type = 666; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1328, 427);
                    trap[1].vvX = 10;
                    trap[1].vvY = 71;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_soga");
                    break;
                case 151:
                    NumTotTraps = 3;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 472, 2177, 512);
                    trap[0].areaTrap = new Rectangle(0, 475, 2177, 10);
                    trap[0].vvscroll = 1;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(0, 0);
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1113 - 30, 225 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1113, 225);
                    trap[1].vvX = 30;
                    trap[1].vvY = 30;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(1163 - 30, 225 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(1163, 225);
                    trap[2].vvX = 30;
                    trap[2].vvY = 30;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    break;
                case 152:
                    NumTotTraps = 4;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 472, 1493, 512);
                    trap[0].areaTrap = new Rectangle(0, 475, 1493, 10);
                    trap[0].vvscroll = 2;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(0, 0);
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego1");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(990 - 30, 336 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(990, 336);
                    trap[1].vvX = 30;
                    trap[1].vvY = 30;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(1035 - 30, 336 - 30 / 2, 30 * 2, 10);//see .pos
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // 666= traps specials that activate when touch areatrap NO UPDATE FRAMES UNTIL IS ON
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(1035, 336);
                    trap[2].vvX = 30;
                    trap[2].vvY = 30;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/fuego2"); //34 height sprite
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(638 - 159 + 30, 24 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[3].numFrames = 10;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(638, 24);
                    trap[3].vvX = 159;
                    trap[3].vvY = 27;
                    trap[3].depth = 0.400000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/fuego4");
                    break;
                case 153:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 490, 1158, 32); //size smaller to fit
                    trap[0].areaTrap = new Rectangle(0, 492, 1158, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.6000000011f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(883 - 23, 396 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(883, 396);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.200000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 154:
                    NumTotTraps = 9;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(459 - 23, 231 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[0].numFrames = 16;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(459, 231);
                    trap[0].vvX = 32;
                    trap[0].vvY = 38;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(935 - 23, 182 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[1].numFrames = 16;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(935, 182);
                    trap[1].vvX = 32;
                    trap[1].vvY = 38;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[2].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[2].areaTrap = new Rectangle(1574 - 23, 129 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[2].numFrames = 16;
                    trap[2].actFrame = 0;
                    trap[2].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[2].isOn = false;
                    trap[2].pos = new Vector2(1574, 129);
                    trap[2].vvX = 32;
                    trap[2].vvY = 38;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[3].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[3].areaTrap = new Rectangle(2095 - 23, 21 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[3].numFrames = 16;
                    trap[3].actFrame = 0;
                    trap[3].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[3].isOn = false;
                    trap[3].pos = new Vector2(2095, 21);
                    trap[3].vvX = 32;
                    trap[3].vvY = 38;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[4].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[4].areaTrap = new Rectangle(40 - 23, 255 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[4].numFrames = 16;
                    trap[4].actFrame = 0;
                    trap[4].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[4].isOn = false;
                    trap[4].pos = new Vector2(40, 255);
                    trap[4].vvX = 32;
                    trap[4].vvY = 38;
                    trap[4].depth = 0.600000009f;
                    trap[4].transparency = 255;
                    trap[4].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[5].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[5].areaTrap = new Rectangle(507 - 23, 52 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[5].numFrames = 16;
                    trap[5].actFrame = 0;
                    trap[5].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[5].isOn = false;
                    trap[5].pos = new Vector2(507, 52);
                    trap[5].vvX = 32;
                    trap[5].vvY = 38;
                    trap[5].depth = 0.600000009f;
                    trap[5].transparency = 255;
                    trap[5].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[6].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[6].areaTrap = new Rectangle(1272 - 23, 205 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[6].numFrames = 16;
                    trap[6].actFrame = 0;
                    trap[6].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[6].isOn = false;
                    trap[6].pos = new Vector2(1272, 205);
                    trap[6].vvX = 32;
                    trap[6].vvY = 38;
                    trap[6].depth = 0.600000009f;
                    trap[6].transparency = 255;
                    trap[6].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[7].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[7].areaTrap = new Rectangle(1963 - 23, 307 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[7].numFrames = 16;
                    trap[7].actFrame = 0;
                    trap[7].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[7].isOn = false;
                    trap[7].pos = new Vector2(1963, 307);
                    trap[7].vvX = 32;
                    trap[7].vvY = 38;
                    trap[7].depth = 0.600000009f;
                    trap[7].transparency = 255;
                    trap[7].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    trap[8].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[8].areaTrap = new Rectangle(2192 - 23, 460 - 38 / 2, 23 * 2, 10); //se .pos minis vvY/2 -vvx long vvx*2
                    trap[8].numFrames = 16;
                    trap[8].actFrame = 0;
                    trap[8].type = 555; // traps that uses vector2 to draws areadraw if filled 0's
                    trap[8].isOn = false;
                    trap[8].pos = new Vector2(2192, 460);
                    trap[8].vvX = 32;
                    trap[8].vvY = 38;
                    trap[8].depth = 0.600000009f;
                    trap[8].transparency = 255;
                    trap[8].sprite = Content.Load<Texture2D>("traps/dead_spin");
                    break;
                case 159:
                    numTOTdoors = 4; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[159].doorX, level[159].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(559, 131);
                    moreDoors[2].doorMoreXY = new Vector2(96, 327);
                    moreDoors[3].doorMoreXY = new Vector2(1033, 419);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(1172 - 5, 381 - 5, 10, 10); //normally +5 on Y and 10 of height
                    trap[0].numFrames = 9;
                    trap[0].actFrame = 0;
                    trap[0].type = 666;
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(1172, 381);
                    trap[0].vvX = 32;
                    trap[0].vvY = 48;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/dead_almeja");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1176 - 5, 269 - 5, 10, 10); //normally +5 on Y and 10 of height
                    trap[1].numFrames = 29;
                    trap[1].actFrame = 0;
                    trap[1].type = 666;
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1176, 269);
                    trap[1].vvX = 32;
                    trap[1].vvY = 120;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_bombona");
                    sprite = new Varsprites[2];
                    sprite[0].actFrame = 0;
                    sprite[0].axisX = 1;
                    sprite[0].axisY = 1;
                    sprite[0].depth = 0.20888886f;
                    sprite[0].R = 255;
                    sprite[0].G = 255;
                    sprite[0].B = 255;
                    sprite[0].transparency = 200;
                    sprite[0].pos = new Vector2(100, 100);
                    sprite[0].scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[0].rotation = 0f;
                    sprite[0].framesecond = 0;
                    sprite[0].frame = 0;
                    sprite[0].sprite = Content.Load<Texture2D>("sprite/nube1");
                    sprite[0].minusScrollx = true;
                    sprite[0].typescroll = 3f;
                    sprite[1].actFrame = 0;
                    sprite[1].axisX = 1;
                    sprite[1].axisY = 1;
                    sprite[1].depth = 0.28888805f;
                    sprite[1].R = 255;
                    sprite[1].G = 225;
                    sprite[1].B = 225;
                    sprite[1].transparency = 200;
                    sprite[1].pos = new Vector2(300, 300);
                    sprite[1].scale = 2f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[1].rotation = 0f;
                    sprite[1].framesecond = 0;
                    sprite[1].frame = 0;
                    sprite[1].sprite = Content.Load<Texture2D>("sprite/nube2");
                    sprite[1].minusScrollx = true;
                    sprite[1].typescroll = 2;
                    break;
                case 160:
                    numTOTdoors = 2; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[160].doorX, level[160].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(1280, 462);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[0].areaTrap = new Rectangle(357, 333 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[0].numFrames = 10;
                    trap[0].actFrame = 0;
                    trap[0].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[0].isOn = false;
                    trap[0].pos = new Vector2(357, 333);
                    trap[0].vvX = 0;
                    trap[0].vvY = 27;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/fuego3");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(64 - 159 + 30, 333 - 5, 159 - 30, 10); //fire shooter see .pos vector2 minus vvX-20 and vvY-5
                    trap[1].numFrames = 10;
                    trap[1].actFrame = 0;
                    trap[1].type = 555; // traps animated  that uses vector2 to draws areadraw if filled 0's
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(64, 333);
                    trap[1].vvX = 159;
                    trap[1].vvY = 27;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/fuego4");
                    break;
                case 161:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(892, 862, 1120 - 892, 32);
                    trap[0].areaTrap = new Rectangle(892, 867, 1120 - 892, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 0, 0, 0);
                    trap[1].areaTrap = new Rectangle(1451 - 5, 469 - 5, 10, 10); //normally +5 on Y and 10 of height
                    trap[1].numFrames = 29;
                    trap[1].actFrame = 0;
                    trap[1].type = 666;
                    trap[1].isOn = false;
                    trap[1].pos = new Vector2(1451, 469);
                    trap[1].vvX = 32;
                    trap[1].vvY = 120;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/dead_bombona");
                    break;
                case 162:
                    numTOTdoors = 3; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[162].doorX, level[162].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(level[162].doorX + 180, level[162].doorY);
                    moreDoors[2].doorMoreXY = new Vector2(level[162].doorX, 345);
                    SteelON = true; numTOTsteel = 2;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(458, 0, 501 - 458, 319);
                    steel[1].area = new Rectangle(145, 269, 277 - 145, 320 - 269);
                    break;
                case 163:
                    numTOTdoors = 3;
                    numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[163].doorX, level[163].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(level[163].doorX, 220);
                    moreDoors[2].doorMoreXY = new Vector2(level[163].doorX, 382);
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].vvscroll = 1;
                    trap[0].areaDraw = new Rectangle(853, 504, 1932 - 853, 32);
                    trap[0].areaTrap = new Rectangle(853, 509, 1932 - 853, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 170;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(624, 483 - 32, 672 - 624, 32);
                    trap[1].areaTrap = new Rectangle(624, 483 - 27, 672 - 624, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 170;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    sprite = new Varsprites[3];
                    sprite[0].actFrame = 0;
                    sprite[0].axisX = 1;
                    sprite[0].axisY = 7;
                    sprite[0].depth = 0.406f;
                    sprite[0].R = 255;
                    sprite[0].G = 255;
                    sprite[0].B = 255;
                    sprite[0].transparency = 255;
                    sprite[0].pos = new Vector2(404, 295); //340
                    sprite[0].scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[0].rotation = 0f;
                    sprite[0].framesecond = 4;
                    sprite[0].frame = 0;
                    sprite[0].sprite = Content.Load<Texture2D>("antorcha_l2");
                    sprite[0].minusScrollx = true;
                    sprite[1].actFrame = 0;
                    sprite[1].axisX = 1;
                    sprite[1].axisY = 7;
                    sprite[1].depth = 0.406f;
                    sprite[1].R = 255;
                    sprite[1].G = 255;
                    sprite[1].B = 255;
                    sprite[1].transparency = 255;
                    sprite[1].pos = new Vector2(1615, 387); //340
                    sprite[1].scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[1].rotation = 0f;
                    sprite[1].framesecond = 2;
                    sprite[1].frame = 0;
                    sprite[1].sprite = Content.Load<Texture2D>("antorcha_l2");
                    sprite[1].minusScrollx = true;
                    sprite[2].actFrame = 0;
                    sprite[2].axisX = 1;
                    sprite[2].axisY = 7;
                    sprite[2].depth = 0.405f;
                    sprite[2].R = 255;
                    sprite[2].G = 225;
                    sprite[2].B = 225;
                    sprite[2].transparency = 255;
                    sprite[2].pos = new Vector2(1095, 92);
                    sprite[2].scale = 1f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[2].rotation = 0f;
                    sprite[2].framesecond = 6;
                    sprite[2].frame = 0;
                    sprite[2].sprite = Content.Load<Texture2D>("antorcha_l2");
                    sprite[2].minusScrollx = true;
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(315, 270, 356 - 315, 320 - 270);
                    break;
                case 165:
                    NumTotTraps = 1;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 480, gameResolution.X, 32);
                    trap[0].areaTrap = new Rectangle(0, 485, gameResolution.X, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    SteelON = true; numTOTsteel = 2;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(511, 242, 552 - 511, 346 - 242);
                    steel[1].area = new Rectangle(735, 243, 776 - 735, 291 - 243);
                    break;
                case 166:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 485, 1882, 32);
                    trap[0].areaTrap = new Rectangle(0, 490, 1882, 10);
                    trap[0].vvscroll = 1;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 504, 1882, 32);
                    trap[1].areaTrap = new Rectangle(0, 509, 1882, 10);
                    trap[1].vvscroll = 2;
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    SteelON = true; numTOTsteel = 1;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(224, 321, 1835 - 224, 373 - 321);
                    break;
                case 167:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(580, 456 - 32, 806 - 580, 32);
                    trap[0].areaTrap = new Rectangle(580, 456 - 27, 806 - 580, 10);
                    trap[0].vvscroll = 1;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600000009f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(1209, 536 - 32, 1391 - 1209, 32);
                    trap[1].areaTrap = new Rectangle(1209, 536 - 27, 1391 - 1209, 10);
                    trap[1].vvscroll = 2;
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    SteelON = true; numTOTsteel = 5;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(539, 55, 581 - 539, 480 - 55);
                    steel[1].area = new Rectangle(808, 268, 848 - 808, 479 - 268);
                    steel[2].area = new Rectangle(1165, 376, 1208 - 1165, 536 - 376);
                    steel[3].area = new Rectangle(1389, 324, 1432 - 1389, 536 - 324);
                    steel[4].area = new Rectangle(270, 430, 539 - 270, 479 - 430);
                    break;
                case 168:
                    SteelON = true; numTOTsteel = 3;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(1389, 81, 1429 - 1389, 454 - 81);
                    steel[1].area = new Rectangle(1432, 215, 1565 - 1432, 262 - 215);
                    steel[2].area = new Rectangle(1702, 188, 2281 - 1702, 239 - 188);
                    break;
                case 169:
                    SteelON = true; numTOTsteel = 6;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(1479, 107, 1655 - 1479, 212 - 107);
                    steel[1].area = new Rectangle(1434, 162, 1476 - 1434, 266 - 162);
                    steel[2].area = new Rectangle(1390, 215, 1431 - 1390, 319 - 215);
                    steel[3].area = new Rectangle(1345, 270, 1386 - 1345, 424 - 270);
                    steel[4].area = new Rectangle(1300, 322, 1342 - 1300, 373 - 322);
                    steel[5].area = new Rectangle(1165, 482, 1655 - 1165, 532 - 482);
                    NumTotTraps = 4;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(0, 515 - 32, 3091, 32);
                    trap[0].vvscroll = 1;
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600005f;
                    trap[0].transparency = 255;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(0, 536 - 32, 3091, 32);
                    trap[1].areaTrap = new Rectangle(0, 536 - 27, 3091, 10);
                    trap[1].vvscroll = 2;
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600000009f;
                    trap[1].transparency = 255;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[2].areaDraw = new Rectangle(1028, 483 - 32, 1165 - 1028, 32);
                    trap[2].numFrames = 8;
                    trap[2].actFrame = 0;
                    trap[2].type = 1;
                    trap[2].isOn = false;
                    trap[2].pos = Vector2.Zero;
                    trap[2].vvX = 0;
                    trap[2].vvY = 0;
                    trap[2].depth = 0.600000009f;
                    trap[2].transparency = 255;
                    trap[2].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[3].areaDraw = new Rectangle(1387, 375 - 32, 1613 - 1387, 32);
                    trap[3].numFrames = 8;
                    trap[3].actFrame = 0;
                    trap[3].type = 1;
                    trap[3].isOn = false;
                    trap[3].pos = Vector2.Zero;
                    trap[3].vvX = 0;
                    trap[3].vvY = 0;
                    trap[3].depth = 0.600000009f;
                    trap[3].transparency = 255;
                    trap[3].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 170:
                    NumTotTraps = 2;
                    TrapsON = true;
                    trap = new Vartraps[NumTotTraps];
                    trap[0].areaDraw = new Rectangle(402, 447 - 32, 987 - 402, 32);
                    trap[0].areaTrap = new Rectangle(402, 447 - 27, 987 - 402, 10);
                    trap[0].numFrames = 8;
                    trap[0].actFrame = 0;
                    trap[0].type = 1;
                    trap[0].isOn = false;
                    trap[0].pos = Vector2.Zero;
                    trap[0].vvX = 0;
                    trap[0].vvY = 0;
                    trap[0].depth = 0.600005f;
                    trap[0].transparency = 200;
                    trap[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    trap[1].areaDraw = new Rectangle(222, 178 - 32, 583 - 222, 32);
                    trap[1].areaTrap = new Rectangle(222, 178 - 27, 583 - 222, 10);
                    trap[1].numFrames = 8;
                    trap[1].actFrame = 0;
                    trap[1].type = 1;
                    trap[1].isOn = false;
                    trap[1].pos = Vector2.Zero;
                    trap[1].vvX = 0;
                    trap[1].vvY = 0;
                    trap[1].depth = 0.600005f;
                    trap[1].transparency = 190;
                    trap[1].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 171:
                    numTOTplats = 2; PlatsON = true;
                    plats = new Varplat[numTOTplats];
                    plats[0].actStep = 0;
                    plats[0].framesecond = 8;
                    plats[0].frame = 0;
                    plats[0].numSteps = 22;
                    plats[0].up = true;
                    plats[0].step = 5;
                    plats[0].areaDraw = new Rectangle(710, 1110, 220, 60);
                    plats[0].sprite = Content.Load<Texture2D>("traps/elevator");
                    plats[1].actStep = 0;
                    plats[1].framesecond = 0;
                    plats[1].frame = 0;
                    plats[1].numSteps = 30;
                    plats[1].up = true;
                    plats[1].step = 1;
                    plats[1].areaDraw = new Rectangle(528, 1216, 200, 35);
                    plats[1].sprite = Content.Load<Texture2D>("traps/elevator");
                    numTOTadds = 1; AddsON = true;
                    adds = new Varadds[numTOTadds];
                    adds[0].areaDraw = new Rectangle(250, 1271, 100, 50); //y 110 orig
                    adds[0].numFrames = 8;
                    adds[0].actFrame = 0;
                    adds[0].frame = 0;
                    adds[0].framesecond = 2;
                    adds[0].sprite = Content.Load<Texture2D>("traps/water_blue");
                    break;
                case 172:
                    SteelON = true; numTOTsteel = 26;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(464 + 16, 82 + 16, 534 - 464, 160 - 82);  // add +16 on x & y only first for mouse sprite fix on "F1"
                    steel[1].area = new Rectangle(608 + 16, 109 + 16, 676 - 608, 185 - 109);
                    steel[2].area = new Rectangle(370 + 16, 490 + 16, 745 - 370, 495 - 490);
                    steel[3].area = new Rectangle(471 + 16, 437 + 16, 539 - 471, 491 - 437);
                    steel[4].area = new Rectangle(349 + 16, 286 + 16, 414 - 349, 364 - 286);
                    steel[5].area = new Rectangle(462 + 16, 222 + 16, 528 - 462, 299 - 222);
                    steel[6].area = new Rectangle(303 + 16, 150 + 16, 372 - 303, 228 - 150);
                    steel[7].area = new Rectangle(709 + 16, 329 + 16, 911 - 709, 410 - 329);
                    steel[8].area = new Rectangle(675 + 16, 465 + 16, 777 - 675, 488 - 465);
                    steel[9].area = new Rectangle(169 + 16, 249 + 16, 268 - 169, 407 - 249);
                    steel[10].area = new Rectangle(172 + 16, 117 + 16, 234 - 172, 247 - 117);
                    steel[11].area = new Rectangle(830 + 16, 71 + 16, 965 - 830, 120 - 71);
                    steel[12].area = new Rectangle(594 + 16, 267 + 16, 627 - 594, 374 - 267);
                    steel[13].area = new Rectangle(813 + 16, 278 + 16, 941 - 813, 326 - 278);
                    steel[14].area = new Rectangle(763 + 16, 148 + 16, 832 - 763, 197 - 148);
                    steel[15].area = new Rectangle(797 + 16, 121 + 16, 865 - 797, 146 - 121);
                    steel[16].area = new Rectangle(526 + 16, 349 + 16, 593 - 526, 401 - 349);
                    steel[17].area = new Rectangle(731 + 16, 179 + 16, 763 - 731, 230 - 179);
                    steel[18].area = new Rectangle(698 + 16, 214 + 16, 730 - 698, 264 - 214);
                    steel[19].area = new Rectangle(663 + 16, 228 + 16, 699 - 663, 277 - 228);
                    steel[20].area = new Rectangle(628 + 16, 241 + 16, 668 - 628, 290 - 241);
                    steel[21].area = new Rectangle(561 + 16, 322 + 16, 596 - 561, 347 - 322);
                    steel[22].area = new Rectangle(271 + 16, 437 + 16, 403 - 271, 489 - 437);
                    steel[23].area = new Rectangle(268 + 16, 356 + 16, 303 - 268, 462 - 356);
                    steel[24].area = new Rectangle(745 + 16, 411 + 16, 814 - 745, 461 - 411);
                    steel[25].area = new Rectangle(302 + 16, 410 + 16, 336 - 302, 436 - 410);
                    break;
                case 173:
                    SteelON = true; numTOTsteel = 3;
                    steel = new Varsteel[numTOTsteel];
                    steel[0].area = new Rectangle(1506 + 16, 176 + 16, 1571 - 1506, 496 - 176);  // add +16 on x & y only first for mouse sprite fix on "F1"
                    steel[1].area = new Rectangle(2526 + 16, 153 + 16, 2592 - 2526, 402 - 153);
                    break;
                case 177:
                    sprite = new Varsprites[1];
                    sprite[0].calc = true;
                    sprite[0].actFrame = 0;
                    sprite[0].axisX = 6;
                    sprite[0].axisY = 1;
                    sprite[0].depth = 0.405f;
                    sprite[0].R = 255;
                    sprite[0].G = 225;
                    sprite[0].B = 225;
                    sprite[0].transparency = 255;
                    sprite[0].pos = new Vector2(0, 0);
                    sprite[0].scale = 0.5f; //1f->normal size -- 0.5f->half size -- etc.
                    sprite[0].rotation = 0f;
                    sprite[0].framesecond = 2;
                    sprite[0].frame = 0;
                    sprite[0].sprite = Content.Load<Texture2D>("touch/arana");
                    sprite[0].minusScrollx = true;
                    sprite[0].dest = new Vector2(0, 0);
                    sprite[0].speed = 0.578f;  // this field is important for move logic of sprites != 0
                    sprite[0].actVect = 0;
                    sprite[0].center.X = ((sprite[0].sprite.Width / sprite[0].axisX) / 2);
                    sprite[0].center.Y = ((sprite[0].sprite.Height / sprite[0].axisY) / 2);
                    sprite[0].path = new Vector3[6];
                    sprite[0].path[0] = new Vector3(402 - 20, 109 + 50, 1.5f);
                    sprite[0].path[1] = new Vector3(424 - 20, 231 + 50, 0.3f);
                    sprite[0].path[2] = new Vector3(461 - 20, 230 + 50, 1.9f);
                    sprite[0].path[3] = new Vector3(462 - 20, 164 + 50, 1.6f);
                    sprite[0].path[4] = new Vector3(525 - 20, 162 + 50, 0.7f);
                    sprite[0].path[5] = new Vector3(525 - 20, 280 + 50, 1.2f);
                    break;
                case 179:
                    numTOTdoors = 3; numACTdoor = 0;
                    moreDoors = new Varmoredoors[numTOTdoors];
                    moreDoors[0].doorMoreXY = new Vector2(level[179].doorX, level[179].doorY);
                    moreDoors[1].doorMoreXY = new Vector2(level[179].doorX + 100, level[179].doorY + 160);
                    moreDoors[2].doorMoreXY = new Vector2(level[179].doorX + 190, level[179].doorY + 330);
                    break;
                default:
                    ArrowsON = false;
                    TrapsON = false;
                    SteelON = false;
                    PlatsON = false;
                    AddsON = false;
                    break;
            }

        }
    }
}

