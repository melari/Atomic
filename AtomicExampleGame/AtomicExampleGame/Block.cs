using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework;

namespace AtomicExampleGame
{
    class Block : PolygonCol
    {
        public Block(Atom a, Vector2 position, Vector2 size)
            : base(a, position, position + new Vector2(size.X, 0))
        {
            AddPoint(position + size);
            Close();
        }
    }
}
