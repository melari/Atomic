using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Atomic
{

    /*
     * The Resources class manages all resources in the game (graphics, fonts, sound effects)
     * All resources that will be used should be loaded in the LoadContent function that will
     * be run at the start of the game.
     * Resources can then be accessed by their defined name from any class by using the static functions.
     */

    static class Resources
    {
        private static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

        public static void LoadContent(ContentManager content)
        {
            fonts.Add("MenuFont", content.Load<SpriteFont>("MenuFont"));
            fonts.Add("TitleFont", content.Load<SpriteFont>("TitleFont"));
            fonts.Add("ConsoleFont", content.Load<SpriteFont>("ConsoleFont"));

            sprites.Add("Pixel", content.Load<Texture2D>("Pixel"));
        }

        public static SpriteFont GetFont(string name)
        {
            return fonts[name];
        }

        public static Texture2D GetSprite(string name)
        {
            return sprites[name];
        }

        public static SoundEffect GetSound(string name)
        {
            return sounds[name];
        }
    }
}
