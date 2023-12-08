using Lemmings.NET.Helpers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.NET.Screens;

internal class EndLevel
{
    internal bool EndSongPlayed { get; set; }

    internal void Draw(SpriteBatch spriteBatch)
    {
        if (!EndSongPlayed)
        {
            if (MyGame.Instance.ScreenInGame.CurrentMusic.State == SoundState.Playing)
                MyGame.Instance.ScreenInGame.CurrentMusic.Stop();
            if (MyGame.Instance.ScreenInGame.ExitBad && MyGame.Instance.Sfx.OhNo.State != SoundState.Playing)
                MyGame.Instance.Sfx.OhNo.Play();
            else if (!MyGame.Instance.ScreenInGame.ExitBad && !SaveGame.MuteMusic && MyGame.Instance.Music.WinMusic.State != SoundState.Playing)
                MyGame.Instance.Music.WinMusic.Play();
        }
        EndSongPlayed = true;
        Color colorFill = new()
        {
            R = 0, //color.black for this change to see differents options
            G = 0,
            B = 0,
            A = 150,
        };
        spriteBatch.Draw(MyGame.Instance.Gfx.Texture1pixel, new Rectangle(45, 32, 1005, 600), null, colorFill, 0f, Vector2.Zero, SpriteEffects.None, 0.001f);
        spriteBatch.Draw(MyGame.Instance.ScreenMainMenu.MainMenuGfx.mainMenuSign2, new Rectangle(-200, -120, 1500, 900), null,
           Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.00005f);
        int percent = (100 * MyGame.Instance.ScreenInGame.NumSaved) / MyGame.Instance.ScreenInGame.CurrentLevel.TotalLemmings;
        MyGame.Instance.Fonts.TextLem("All lemmings accounted for:", new Vector2(150, 100), Color.Cyan, 1.5f, 0.0000000001f, spriteBatch);
        MyGame.Instance.Fonts.TextLem("You rescued " + string.Format("{0}", percent) + "%",
             new Vector2(270, 160), Color.Violet, 1.5f, 0.0000000001f, spriteBatch);
        percent = (100 * MyGame.Instance.ScreenInGame.Lemsneeded) / MyGame.Instance.ScreenInGame.CurrentLevel.TotalLemmings;
        MyGame.Instance.Fonts.TextLem("You needed " + string.Format("{0}", percent) + "%",
             new Vector2(300, 220), Color.DodgerBlue, 1.5f, 0.0000000001f, spriteBatch);
        MyGame.Instance.Fonts.TextLem("Press <ESC> or <Left Mouse Button>", new Vector2(70, 400), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
        if (MyGame.Instance.ScreenInGame.ExitBad)
            MyGame.Instance.Fonts.TextLem("to retry level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
        else if (MyGame.Instance.ScreenInGame.NumSaved >= MyGame.Instance.ScreenInGame.Lemsneeded)
            MyGame.Instance.Fonts.TextLem("to next level...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
        else
            MyGame.Instance.Fonts.TextLem("to continue...", new Vector2(100, 440), Color.LightCyan, 1.3f, 0.0000000001f, spriteBatch);
        MyGame.Instance.Fonts.TextLem("Press <Enter> or <Right Mouse Button>", new Vector2(70, 520), Color.Yellow, 1.3f, 0.0000000001f, spriteBatch);
        MyGame.Instance.Fonts.TextLem("to Main Menu...", new Vector2(100, 560), Color.Yellow, 1.3f, 0.0000000001f, spriteBatch);
    }
}
