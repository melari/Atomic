using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{    
    /*
     * Screen objects are the main containers of the game. There can only be one active at a time at any given point in the execution of the program,
     * but it can be switched at any time. Switching between screens can also include transitions as defined by a Transition object.
     * Screens contain their own Update and Draw logic and usually would contain Lists of objects that are contained on that screen to be drawn. Only the 
     * currently active screen will have its Update and Draw functions called.
     * Console commands can also be defined specific to active screen. When a command is entered into the console, if no match is found the console will
     * pass the command and arguments into the ExecuteCommand function for custom handling.
     */


    public abstract class Screen
    {
        protected Engine engine;
        protected Color backgroundColor;

        public Screen(Engine engine) : this(engine, Color.Green) { }        
        public Screen(Engine engine, Color backgroundColor)
        {
            this.engine = engine;
            this.backgroundColor = backgroundColor;
        }

        public Color GetColor() { return backgroundColor; }

        public abstract bool ExecuteCommand(string c, string[] args, Console console);
        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
