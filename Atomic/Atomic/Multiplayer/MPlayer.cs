using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Atomic
{
    abstract class MPlayer : WorldObject
    {
        public long who { get; private set; }

        public MPlayer(long who, Screen screen)
            : this(who, screen, Vector2.Zero) { }
        public MPlayer(long who, Screen screen, Vector2 position)
            : this(who, screen, position, Vector2.Zero) { }
        public MPlayer(long who, Screen screen, Vector2 position, Vector2 velocity)
            : base(screen, position, velocity)
        {
            this.who = who;
        }

        public abstract void MPUpdate(NetIncomingMessage msg);
    }
}
