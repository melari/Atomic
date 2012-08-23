using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atomic
{
    public class CircleCollisionMask : CollisionMask
    {
        GameObject o;
        public float radius;

        public CircleCollisionMask(bool isStatic, GameObject o, float radius)
            : base(isStatic)
        {
            this.o = o;
            this.radius = radius;
        }


    }
}
