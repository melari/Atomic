using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public class ScaleMethod : ParticleMethod
    {
        protected float startScale = 0.75f;
        protected float endScale = 1.0f;

        public ScaleMethod(ParticleEngine engine, float startScale, float endScale)
            : base(engine)
        {
            this.startScale = startScale;
            this.endScale = endScale;
        }

        public override void Update(Particle part)
        {            
            part.scale = startScale + (endScale - startScale) * part.normalizedLifetime;
        }
    }
}
