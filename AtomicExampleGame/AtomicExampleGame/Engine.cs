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
using Atomic;

namespace AtomicExampleGame
{
    public class Engine : Atom
    {
        new string[] sprites = { };
        new string[] fonts = { };
        new string[] sounds = { };

        public ConsoleState console { get; private set; }

        public Engine()
            : base()
        {
            MainMenu menu = new MainMenu(this, 0);
            stateManager.StartState(new FadeTransition(this, 1, 0.05f, true, menu));

            console = new ConsoleState(this, 100);
            console.RegisterStateCommands(stateManager);
            stateManager.AddState(console);
            stateManager.Update();

            CorrectBugN();
        }

        /// <summary>
        /// Simple fix to force the game into windowed mode when using BugN.
        /// </summary>
        public void CorrectBugN()
        {
            console.Execute("echo \"Correcting for BugN\"; StateManager.focus \"ConsoleState\"; StateManager.pause true; fullscreen true; fullscreen false; sleep 80; echo \"Corection completed.\"; sleep 30; StateManager.dropFocus; StateManager.pause false");
        }
    }
}