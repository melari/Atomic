using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    abstract class ParticleMethod
    {
        protected Particle part;
        protected ParticleEngine engine;

        public ParticleMethod(Particle part, ParticleEngine engine)
        {
            this.part = part;
            this.engine = engine;
        }

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
