using Lemmings.NET.Exceptions;

using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Constants;

internal enum EGfxTrap
{
    None = 0,
    Magma = 1,
    Flame = 2,
    Spider = 3,
    Fire = 4,
    Acid = 5,
    Ice = 6,
    Lava = 7,
    FireJet = 8,
    WaterBlue = 9,
    DeadSpin = 10,
    Arrow = 11,
    DeadHanged = 12,
    Fire2 = 13,
    DeadPiston = 14,
    WolfTrap = 15,
    DeadPiston2 = 16,
    FireJet2 = 17,
    DeadAnvil = 18,
    DeadSpadeLeft = 19,
    DeadSpadeRight = 20,
    Arrow2 = 21,
    DeadLaser = 22,
    Cloud1 = 23,
    Cloud2 = 24,
    DeadBell = 25,
    DeadClam = 26,
    Torch = 27,
    TrapElevator = 28,
}

internal static class ConvertGfx
{
    internal static Texture2D GetTexture(this EGfxTrap trap)
    {
        return trap switch
        {
            EGfxTrap.Magma => MyGame.Instance.Gfx.MagmaMask,
            EGfxTrap.Flame => MyGame.Instance.Gfx.Flame,
            EGfxTrap.Spider => MyGame.Instance.Gfx.Spider,
            EGfxTrap.Fire => MyGame.Instance.Gfx.Fire,
            EGfxTrap.Acid => MyGame.Instance.Gfx.Acid,
            EGfxTrap.Ice => MyGame.Instance.Gfx.Ice,
            EGfxTrap.Lava => MyGame.Instance.Gfx.Lava,
            EGfxTrap.FireJet => MyGame.Instance.Gfx.FireJet,
            EGfxTrap.WaterBlue => MyGame.Instance.Gfx.WaterBlue,
            EGfxTrap.DeadSpin => MyGame.Instance.Gfx.DeadSpin,
            EGfxTrap.Arrow => MyGame.Instance.Gfx.Arrow,
            EGfxTrap.DeadHanged => MyGame.Instance.Gfx.DeadHanged,
            EGfxTrap.Fire2 => MyGame.Instance.Gfx.Fire2,
            EGfxTrap.DeadPiston => MyGame.Instance.Gfx.DeadPiston,
            EGfxTrap.WolfTrap => MyGame.Instance.Gfx.WolfTrap,
            EGfxTrap.DeadPiston2 => MyGame.Instance.Gfx.DeadPiston2,
            EGfxTrap.FireJet2 => MyGame.Instance.Gfx.FireJet2,
            EGfxTrap.DeadAnvil => MyGame.Instance.Gfx.DeadAnvil,
            EGfxTrap.DeadSpadeLeft => MyGame.Instance.Gfx.DeadSpadeLeft,
            EGfxTrap.DeadSpadeRight => MyGame.Instance.Gfx.DeadSpadeRight,
            EGfxTrap.Arrow2 => MyGame.Instance.Gfx.Arrow2,
            EGfxTrap.DeadLaser => MyGame.Instance.Gfx.DeadLaser,
            EGfxTrap.Cloud1 => MyGame.Instance.Gfx.Cloud1,
            EGfxTrap.Cloud2 => MyGame.Instance.Gfx.Cloud2,
            EGfxTrap.DeadBell => MyGame.Instance.Gfx.DeadBell,
            EGfxTrap.DeadClam => MyGame.Instance.Gfx.DeadClam,
            EGfxTrap.Torch => MyGame.Instance.Gfx.Torch,
            EGfxTrap.TrapElevator => MyGame.Instance.Gfx.TrapElevator,
            _ => throw new LemmingsNetException("Unknow Gfx type"),
        };
    }
}
