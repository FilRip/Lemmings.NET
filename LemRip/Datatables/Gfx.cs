using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Datatables;

internal class Gfx
{
    internal Texture2D Texture1pixel { get; private set; }
    internal Texture2D Backmenu2 { get; private set; }
    internal Texture2D Backlogo { get; private set; }
    internal Texture2D Logo_fondo { get; private set; }
    internal Texture2D Explosion_particle { get; private set; }
    internal Texture2D Lhiss { get; private set; }
    internal Texture2D Squemado { get; private set; }
    internal Texture2D Puente_nomas { get; private set; }
    internal Texture2D CrateNormals { get; private set; }
    internal Texture2D Text { get; private set; }
    internal Texture2D Mascarapared { get; private set; }
    internal Texture2D WaterBlue { get; private set; }
    internal Texture2D TrapElevator { get; private set; }
    internal Texture2D Sale { get; private set; }
    internal Texture2D MagmaMask { get; private set; }
    internal Texture2D Acid { get; private set; }
    internal Texture2D Ice { get; private set; }
    internal Texture2D Flame { get; private set; }
    internal Texture2D Spider { get; private set; }
    internal Texture2D Fire { get; private set; }
    internal Texture2D Lava { get; private set; }
    internal Texture2D FireJet { get; private set; }
    internal Texture2D DeadSpin { get; private set; }
    internal Texture2D Arrow { get; private set; }
    internal Texture2D Fire2 { get; private set; }
    internal Texture2D DeadPiston { get; private set; }
    internal Texture2D WolfTrap { get; private set; }
    internal Texture2D DeadPiston2 { get; private set; }
    internal Texture2D DeadHanged { get; private set; }
    internal Texture2D FireJet2 { get; private set; }
    internal Texture2D DeadAnvil { get; private set; }
    internal Texture2D DeadSpadeLeft { get; private set; }
    internal Texture2D DeadSpadeRight { get; private set; }
    internal Texture2D Arrow2 { get; private set; }
    internal Texture2D DeadLaser { get; private set; }
    internal Texture2D DeadClam { get; private set; }
    internal Texture2D DeadBell { get; private set; }
    internal Texture2D Cloud1 { get; private set; }
    internal Texture2D Cloud2 { get; private set; }
    internal Texture2D Torch { get; private set; }

    internal void Load(GraphicsDevice GraphicsDevice, ContentManager content)
    {
        Texture1pixel = new Texture2D(GraphicsDevice, 1, 1);
        Texture1pixel.SetData(new Color[] { Color.White });  // texture for DRAWLINE 1x1
        Backlogo = content.Load<Texture2D>("lemlogo_01");
        Backmenu2 = content.Load<Texture2D>("background_012");
        Logo_fondo = content.Load<Texture2D>("logo");
        Explosion_particle = content.Load<Texture2D>("sprite/stater");  //stater nice with rotation too
        Lhiss = content.Load<Texture2D>("sprite/hiss");
        Squemado = content.Load<Texture2D>("quemado");
        Puente_nomas = content.Load<Texture2D>("puente_nomas");
        CrateNormals = content.Load<Texture2D>("craten");
        Text = content.Load<Texture2D>("crate");
        Mascarapared = content.Load<Texture2D>("mascara_pared");
        WaterBlue = content.Load<Texture2D>("traps/water_blue");
        TrapElevator = content.Load<Texture2D>("traps/elevator");
        Sale = content.Load<Texture2D>("sale");
        MagmaMask = content.Load<Texture2D>("sprite/magma_mask");
        Acid = content.Load<Texture2D>("traps/acid");
        Ice = content.Load<Texture2D>("traps/ice_water");
        Flame = content.Load<Texture2D>("sprite/flame");
        Spider = content.Load<Texture2D>("touch/arana");
        Fire = content.Load<Texture2D>("touch/fire_sprites_other");
        Lava = content.Load<Texture2D>("traps/fuego1");
        FireJet = content.Load<Texture2D>("traps/fuego4");
        DeadSpin = content.Load<Texture2D>("traps/dead_spin");
        Arrow = content.Load<Texture2D>("fondos/arrow1");
        Fire2 = content.Load<Texture2D>("traps/fuego2");
        DeadPiston = content.Load<Texture2D>("traps/dead_marble");
        WolfTrap = content.Load<Texture2D>("traps/dead_trampa");
        DeadPiston2 = content.Load<Texture2D>("traps/dead_marble2_fix");
        DeadHanged = content.Load<Texture2D>("traps/dead_soga");
        FireJet2 = content.Load<Texture2D>("traps/fuego3");
        DeadAnvil = content.Load<Texture2D>("traps/dead_10");
        DeadSpadeLeft = content.Load<Texture2D>("traps/dead_arrow_left");
        DeadSpadeRight = content.Load<Texture2D>("traps/dead_arrow_right");
        Arrow2 = content.Load<Texture2D>("fondos/arrow2");
        DeadLaser = content.Load<Texture2D>("traps/dead_laser");
        DeadClam = content.Load<Texture2D>("traps/dead_almeja");
        DeadBell = content.Load<Texture2D>("traps/dead_bombona");
        Cloud1 = content.Load<Texture2D>("sprite/nube1");
        Cloud2 = content.Load<Texture2D>("sprite/nube2");
        Torch = content.Load<Texture2D>("antorcha_l2");
    }

    internal void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color pintado, Int32 grosor, float layer)
    {
        Vector2 edge = end - start;// calculate angle to rotate line
        float angle = (float)Math.Atan2(edge.Y, edge.X);
        Rectangle rectangleFill = new()
        {
            X = (int)start.X,
            Y = (int)start.Y,
            Width = (int)edge.Length(),
            Height = grosor,
        };
        sb.Draw(Texture1pixel, rectangleFill, null, pintado, angle, Vector2.Zero, SpriteEffects.None, layer);
    }
}
