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
        string[] _sprites = { };
        string[] _fonts = { };
        string[] _sounds = { };

        public override string[] sprites { get { return _sprites; } protected set { _sprites = value; } }
        public override string[] fonts { get { return _fonts; } protected set { _fonts = value; } }
        public override string[] sounds { get { return _sounds; } protected set { _sounds = value; } }

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
            console.Execute("echo \"Correcting for BugN\"; StateManager.focus \"ConsoleState\"; StateManager.pause true; fullscreen true; sleep 60; fullscreen false; sleep 80; echo \"Corection completed.\"; sleep 30; StateManager.dropFocus; StateManager.pause false");
        }
    }
}