using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atomic
{
    public abstract class ParticleMethod
    {
        protected ParticleEngine engine;

        public ParticleMethod(ParticleEngine engine)
        {
            this.engine = engine;
        }

        public abstract void Update(Particle part);
    }
}
