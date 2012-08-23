using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public abstract class CollisionMask
    {
        public bool isStatic;

        public CollisionMask(bool isStatic)
        {
            this.isStatic = isStatic;
        }
    }
}
