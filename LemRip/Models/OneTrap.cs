using Lemmings.NET.Constants;
using Lemmings.NET.Interfaces;

using Microsoft.Xna.Framework;

namespace Lemmings.NET.Models;

internal class OneTrap
{
    internal ETypeTrap TypeTrap { get; set; }

    internal Vector2 Pos { get; set; }

    internal ITrap Trap { get; set; }
}
