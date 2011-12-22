using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    /*
     * ParticleMethods are the controllers of actual Particle objects. They
     * define the behavior (generally including life, movement, and slight visual changes).
     * The actual drawing is defined in the particle object itself.
     */

    abstract class ParticleMethod
    {
        protected Particle part;
        protected ParticleEngine engine;

        public ParticleMethod(Particle part, ParticleEngine engine)
        {
            this.part = part;
            this.engine = engine;
             part.SetMethod(this);
        }

        public abstract void Update();        
    }
}
