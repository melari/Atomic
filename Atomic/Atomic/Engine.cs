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

    /*
     * Engine is the main class of the game. It controls all Screens, as well as the ingame console, Resources, Input, VideoSettings, ect.
     * See the header information in these other classes for a more specific explination as to what each one does.
     * 
     * Engine calls Update/Draw on currently active screen => Currently active screen calls Update/Draw on all of its game objects.
     */

    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        

        Screen currentScreen;

        bool transiting = false;
        Transition trans;
        bool OUT = true;
        Screen nextScreen;

        Console console;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";            
        }

        /*============================================
         * Initialize is run once at the start of the game.
         * This is where at least the first screen should be created and 
         * set using ChangeScreen()
         ============================================*/
        protected override void Initialize()
        {
            VideoSettings.init(graphics);
            console = new Console(this);

            currentScreen = new Blank(this);
            ChangeScreen(new MenuScreen(this), new Fade(0.02f, true));

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.LoadContent(Content);
        }
        
        protected override void UnloadContent()
        {            
        }


        /*============================================
         * Immediately give focus to the given Screen,
         * or if a transition is given, change to the given
         * screen using the defined transition.
         ============================================*/
        public void ChangeScreen(Screen screen)
        {
            currentScreen = screen;
        }
        public void ChangeScreen(Screen screen, Transition trans)
        {
            OUT = true;
            transiting = true;
            nextScreen = screen;
            this.trans = trans;
        }

        public Screen GetCurrentScreen()
        {
            return currentScreen;
        }


        /*============================================
         * The Update function is where all the game calculations should go.
         * It calls the currently active screen's update function as well as updates
         * the various support classes.
         ============================================*/
        protected override void Update(GameTime gameTime)
        {
            Input.Update();                                 // Grab the latest input from the keybard and mouse
            console.Update();                               // Update the ingame console.
            
            if (Input.KeyDown(Keys.Escape))                 // Allows the game to exit.
                this.Exit();

            currentScreen.Update();                         // Update the screen that currently has focus.

            if (transiting)                                 // If we are transitioning to another screen, call the transition object's update.
            {
                if (OUT)                                    // Transition Out
                {
                    if (trans.OUT_Update()) 
                    { 
                        OUT = false;
                        currentScreen = nextScreen;
                    }
                }
                else                                        // Transition In
                {
                    if (trans.IN_Update())
                    {
                        transiting = false;
                    }
                }
            }

            base.Update(gameTime);
        }


        /*============================================
         * The Draw function is what actually gets run each frame cycle.
         * The screen is cleard and the current screen gets drawn.
         * Everything is double buffered, so no need to worry about sync issues.
         ============================================*/
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(currentScreen.GetColor());     // Clear the screen
            
            currentScreen.Draw(spriteBatch);                    // Draw the screen that currently has focus.            

            if (transiting)                                     // Draw any transition effects if we are changing screens.
            {
                spriteBatch.Begin();
                if (OUT) { trans.OUT_Draw(spriteBatch); }
                else { trans.IN_Draw(spriteBatch); }
                spriteBatch.End();
            }

            console.Draw(spriteBatch);                          // Draw the console on top of everything.            

            base.Draw(gameTime);
        }
    }
}
