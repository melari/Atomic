using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{
    class ConstantForceMethod : ParticleMethod
    {
        Vector2 force;

        public ConstantForceMethod(ParticleEngine engine, Vector2 force)
            : base(engine)
        {
            this.force = force;

        }

        public override void Update(Particle part)
        {
            part.AddForce(force);
        }
    }
}
