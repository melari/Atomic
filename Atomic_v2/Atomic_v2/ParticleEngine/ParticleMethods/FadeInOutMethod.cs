using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public class FadeInOutMethod : ParticleMethod
    {
        public FadeInOutMethod(ParticleEngine engine) : base(engine) { }

        public override void Update(Particle part)
        {
            part.alpha = 4 * part.normalizedLifetime * (1 - part.normalizedLifetime);
        }
    }
}
