using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{

    /*
     * The default method for a particle if non is defined.
     * Simply destroys the particle on the first update. Because of the buffer remove, there is sometimes
     * a single frame in which the particle is drawn to the screen.
     */

    class DeadMethod : ParticleMethod
    {
        public DeadMethod(Particle part, ParticleEngine engine)
            : base(part, engine)
        {            
        }

        public override void Update()
        {
            engine.DestroyParticle(part);
        }        
    }
}
