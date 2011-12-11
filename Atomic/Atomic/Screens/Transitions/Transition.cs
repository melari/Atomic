using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{

    /*
     * Transition objects can be used to add effects during the time when the game is
     * changing active screens. There are two stages to the process, OUT and IN. It is easiest
     * to think of this in terms of a fading effect. The OUT functions are run during the fadeout
     * (the first screen is still being drawn), then the IN functions are run during the fadein
     * (the new screen is now being drawn).
     */

    public abstract class Transition
    {
        /*
         * OUT_Update() and IN_Update() should return True iff they have finished their transition animation.
         */

        public abstract bool OUT_Update();
        public abstract void OUT_Draw(SpriteBatch spriteBatch);

        public abstract bool IN_Update();
        public abstract void IN_Draw(SpriteBatch spriteBatch);
    }
}
