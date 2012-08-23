using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework.Graphics;

namespace AtomicExampleGame
{
    class MainMenu : MenuState
    {
        public MainMenu(Atom a, int layer)
            : base(a, layer, "Atomic Engine Example Game")
        {
            AddSlidingMenuItem("Start", delegate(MenuState menu)
            {
                a.stateManager.EndState(this);

                TestRoom room = new TestRoom((Engine)a, 0);
                a.stateManager.StartState(new FadeTransition(a, 1, room));
            });

            AddSlidingMenuItem("Options", delegate(MenuState menu)
            {
                a.stateManager.StartState(new OptionsMenu(a, 1));
            });

            AddSlidingMenuItem("Exit", delegate(MenuState menu)
            {
                a.Exit();
            });
        }
    }
}
