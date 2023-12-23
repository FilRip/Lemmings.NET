﻿using Lemmings.NET.Constants;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Models;

internal abstract class OneProp
{
    internal abstract ETypeTrap TypeTrap { get; }

    protected Vector2 Pos;

    internal int ActFrame;

    internal int Frame;

    internal abstract void Draw(SpriteBatch spriteBatch);

    internal abstract void Update();
}
