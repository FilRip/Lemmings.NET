﻿using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models.Props
{
    internal class OneTrap : OneProp
    {
        internal int Type, VvX, VvY, NumFrames, Vvscroll;
        internal Color Color;
        internal Rectangle AreaDraw, AreaTrap;
        internal float Depth;
        internal bool IsOn;
        internal Texture2D Sprite;

        internal override ETypeTrap TypeTrap
        {
            get { return ETypeTrap.Trap; }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        internal override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}