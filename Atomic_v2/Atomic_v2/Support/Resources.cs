using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public static class Resources
    {
        private static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

        public static void LoadContent(ContentManager content, string[] sprites, string[] fonts, string[] sounds)
        {
            foreach (string sprite in sprites)
                LoadSprite(content, sprite);

            foreach (string font in fonts)
                LoadFont(content, font);

            foreach (string sound in sounds)
                LoadSound(content, sound);
        }

        public static void LoadSprite(ContentManager content, string name)
        {
            LoadSprite(content, name, name);
        }
        public static void LoadSprite(ContentManager content, string name, string path)
        {
            sprites.Add(name, content.Load<Texture2D>(path));
        }

        public static void LoadFont(ContentManager content, string name)
        {
            fonts.Add(name, content.Load<SpriteFont>(name));
        }

        public static void LoadSound(ContentManager content, string name)
        {
            sounds.Add(name, content.Load<SoundEffect>(name));
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

        public static Vector2 GetSpriteCenter(string name)
        {
            return GetSpriteCenter(GetSprite(name));
        }
        public static Vector2 GetSpriteCenter(Texture2D sprite)
        {
            return new Vector2(sprite.Width / 2, sprite.Height / 2);
        }
    }
}
