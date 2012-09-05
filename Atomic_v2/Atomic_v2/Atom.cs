using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Atomic
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Atom : Microsoft.Xna.Framework.Game
    {
        string[] _sprites = { };
        string[] _fonts = { };
        string[] _sounds = { };

        public virtual string[] sprites { get { return _sprites; } protected set { _sprites = value; } }
        public virtual string[] fonts { get { return _fonts; } protected set { _fonts = value; } }
        public virtual string[] sounds { get { return _sounds; } protected set { _sounds = value; } }

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public StateManager stateManager { get; private set; }

        public Vector2 resolution { get; private set; }
        public Rectangle resolutionRectangle
        {
            get { return new Rectangle(0, 0, (int)resolution.X, (int)resolution.Y); }
        }
        public bool fullscreen { get; private set; }

        public Atom()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            resolution = new Vector2(1600, 900);
            fullscreen = false;

            stateManager = new StateManager();
        }

        protected override void Initialize()
        {
            ApplyResolution(1600, 900, false);

            base.Initialize();
        }

        public void ApplyResolution(int width, int height, bool fullscreen)
        {
            resolution = new Vector2(width, height);
            this.fullscreen = fullscreen;
            graphics.PreferredBackBufferWidth = (int)resolution.X;
            graphics.PreferredBackBufferHeight = (int)resolution.Y;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //General Sprites
            Resources.LoadSprite(Content, "Pixel");

            //Particle Effects
            Resources.LoadSprite(Content, "p_explosion", "particles/p_explosion");
            Resources.LoadSprite(Content, "p_smoke", "particles/p_smoke");

            //General Fonts
            Resources.LoadFont(Content, "ConsoleFont");
            Resources.LoadFont(Content, "TitleFont");
            Resources.LoadFont(Content, "MenuFont");

            Resources.LoadContent(Content, sprites, fonts, sounds);
        }

        

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            stateManager.Update();
            stateManager.UpdateStates(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            stateManager.DrawStates(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
