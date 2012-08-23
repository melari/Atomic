using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public class PointForceMethod : ParticleMethod
    {
        Vector2 point;
        float strength;

        public PointForceMethod(ParticleEngine engine, Vector2 point, float strength)
            : base(engine)
        {
            this.point = point;
            this.strength = strength;
        }

        public override void Update(Particle part)
        {
            Vector2 force = point - part.position;
            MathExtra.SetVectorLength(ref force, strength);
            part.AddForce(force);
        }
    }
}
