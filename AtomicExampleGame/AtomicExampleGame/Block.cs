using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework;

namespace AtomicExampleGame
{
    class Block : RectangleCollidable
    {
        public Block(Atom a, Vector2 position, Vector2 size)
            : base(a, position, size)
        {
        }
    }
}
