using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class DeadMethod : ParticleMethod
    {
        public DeadMethod(Particle part, ParticleEngine engine)
            : base(part, engine)
        {            
        }

        public override void Update()
        {
            engine.BufferedDestroyParticle(part);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            engine.BufferedDestroyParticle(part);
        }
    }
}
