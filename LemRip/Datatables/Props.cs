using System.Collections.Generic;
using System.Linq;

using Lemmings.NET.Models;

namespace Lemmings.NET.Datatables;

internal class Props
{
    private List<OneEntry> _listDoors;
    private List<OneExit> _listExits;

    internal OneEntry GetEntry(int id)
    {
        return _listDoors.Single(d => d.Id == id);
    }

    internal OneExit GetExit(int id)
    {
        return _listExits.Single(e => e.Id == id);
    }

    private void LoadDoors()
    {
        _listDoors = [];
        OneEntry entry;

        entry = new()
        {
            Width = 96,
            Height = 50,
            NumFrame = 10,
            Id = 1,
        };
        _listDoors.Add(entry);

        entry = new()
        {
            Width = 96,
            Height = 50,
            NumFrame = 10,
            Id = 2,
        };
        _listDoors.Add(entry);

        entry = new()
        {
            Width = 96,
            Height = 50,
            NumFrame = 10,
            Id = 3,
        };
        _listDoors.Add(entry);

        entry = new()
        {
            Width = 96,
            Height = 48,
            NumFrame = 9,
            Id = 4,
        };
        _listDoors.Add(entry);

        entry = new()
        {
            Width = 96,
            Height = 50,
            NumFrame = 10,
            Id = 5,
        };
        _listDoors.Add(entry);

        entry = new()
        {
            Width = 96,
            Height = 50,
            NumFrame = 10,
            Id = 6,
        };
        _listDoors.Add(entry);

        entry = new()
        {
            Width = 96,
            Height = 50,
            NumFrame = 10,
            Id = 7,
        };
        _listDoors.Add(entry);

        entry = new()
        {
            Width = 96,
            Height = 48,
            NumFrame = 8,
            Id = 8,
        };
        _listDoors.Add(entry);
    }

    private void LoadExits()
    {
        _listExits = [];
        OneExit exit;

        exit = new()
        {
            Width = 96,
            Height = 16,
            NumFrame = 6,
            MoreX = 53,
            MoreY = 50,
            MoreX2 = 53,
            MoreY2 = 34,
            Id = 1,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 64,
            Height = 26,
            NumFrame = 4,
            MoreX = 37,
            MoreY = 38,
            MoreX2 = 37,
            MoreY2 = 70,
            Id = 2,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 96,
            Height = 48,
            NumFrame = 6,
            MoreX = 44,
            MoreY = 104,
            MoreX2 = 44,
            MoreY2 = 56,
            Id = 3,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 96,
            Height = 16,
            NumFrame = 6,
            MoreX = 53,
            MoreY = 50,
            MoreX2 = 53,
            MoreY2 = 34,
            Id = 4,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 16,
            Height = 16,
            NumFrame = 14,
            MoreX = -19,  // values x & y of the animation exit sprite
            MoreY = 16,
            MoreX2 = 35, // values x & y of exit principal sprite with no animation
            MoreY2 = 64,
            Id = 5,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 16,
            Height = 16,
            NumFrame = 14,
            MoreX = -19,  // values x & y of the animation exit sprite
            MoreY = 76,
            MoreX2 = 37, // values x & y of exit principal sprite with no animation
            MoreY2 = 60,
            Id = 6,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 96,
            Height = 16,
            NumFrame = 6,
            MoreX = 45,
            MoreY = 47,
            MoreX2 = 45,
            MoreY2 = 31,
            Id = 7,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 32,
            Height = 16,
            NumFrame = 7,
            MoreX = 18, //animation
            MoreY = 79,
            MoreX2 = 50,
            MoreY2 = 63,
            Id = 8,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 96,
            Height = 16,
            NumFrame = 6,
            MoreX = 53,
            MoreY = 47,
            MoreX2 = 53,
            MoreY2 = 31,
            Id = 9,
        };
        _listExits.Add(exit);

        exit = new()
        {
            Width = 32,
            Height = 16,
            NumFrame = 6,
            MoreX = 20, //Animation
            MoreY = 135,
            MoreX2 = 40,
            MoreY2 = 120,
            Id = 10,
        };
        _listExits.Add(exit);
    }

    internal void Load()
    {
        LoadDoors();
        LoadExits();
    }
}
