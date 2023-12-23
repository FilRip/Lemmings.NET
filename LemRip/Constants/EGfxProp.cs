using Lemmings.NET.Exceptions;

using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Constants;

internal enum EGfxProp
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
    internal static Texture2D GetTexture(this EGfxProp trap)
    {
        return trap switch
        {
            EGfxProp.Magma => MyGame.Instance.Gfx.MagmaMask,
            EGfxProp.Flame => MyGame.Instance.Gfx.Flame,
            EGfxProp.Spider => MyGame.Instance.Gfx.Spider,
            EGfxProp.Fire => MyGame.Instance.Gfx.Fire,
            EGfxProp.Acid => MyGame.Instance.Gfx.Acid,
            EGfxProp.Ice => MyGame.Instance.Gfx.Ice,
            EGfxProp.Lava => MyGame.Instance.Gfx.Lava,
            EGfxProp.FireJet => MyGame.Instance.Gfx.FireJet,
            EGfxProp.WaterBlue => MyGame.Instance.Gfx.WaterBlue,
            EGfxProp.DeadSpin => MyGame.Instance.Gfx.DeadSpin,
            EGfxProp.Arrow => MyGame.Instance.Gfx.Arrow,
            EGfxProp.DeadHanged => MyGame.Instance.Gfx.DeadHanged,
            EGfxProp.Fire2 => MyGame.Instance.Gfx.Fire2,
            EGfxProp.DeadPiston => MyGame.Instance.Gfx.DeadPiston,
            EGfxProp.WolfTrap => MyGame.Instance.Gfx.WolfTrap,
            EGfxProp.DeadPiston2 => MyGame.Instance.Gfx.DeadPiston2,
            EGfxProp.FireJet2 => MyGame.Instance.Gfx.FireJet2,
            EGfxProp.DeadAnvil => MyGame.Instance.Gfx.DeadAnvil,
            EGfxProp.DeadSpadeLeft => MyGame.Instance.Gfx.DeadSpadeLeft,
            EGfxProp.DeadSpadeRight => MyGame.Instance.Gfx.DeadSpadeRight,
            EGfxProp.Arrow2 => MyGame.Instance.Gfx.Arrow2,
            EGfxProp.DeadLaser => MyGame.Instance.Gfx.DeadLaser,
            EGfxProp.Cloud1 => MyGame.Instance.Gfx.Cloud1,
            EGfxProp.Cloud2 => MyGame.Instance.Gfx.Cloud2,
            EGfxProp.DeadBell => MyGame.Instance.Gfx.DeadBell,
            EGfxProp.DeadClam => MyGame.Instance.Gfx.DeadClam,
            EGfxProp.Torch => MyGame.Instance.Gfx.Torch,
            EGfxProp.TrapElevator => MyGame.Instance.Gfx.TrapElevator,
            _ => throw new LemmingsNetException("Unknow Gfx type"),
        };
    }
}
