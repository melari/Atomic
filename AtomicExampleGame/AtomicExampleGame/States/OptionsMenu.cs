using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;

namespace AtomicExampleGame
{
    class OptionsMenu : MenuState
    {
        public OptionsMenu(Atom a, int layer)
            : base(a, layer, "")
        {
            AddSlidingMenuItem("Back", delegate(MenuState menu)
            {
                a.stateManager.EndState(this);
            },
            300);
        }
    }
}
