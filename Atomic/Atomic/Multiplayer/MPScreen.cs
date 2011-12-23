using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Atomic
{
    abstract class MPScreen : Screen
    {
        public MPScreen(Engine engine)
            : this(engine, Color.Green)
        {
        }
        public MPScreen(Engine engine, Color backgroundColor)
            : base(engine, backgroundColor)
        {
        }

        public abstract void SendInput(NetClient client);
        public abstract MPlayer NewPlayer(long who);
    }
}
