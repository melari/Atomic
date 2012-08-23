using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public delegate void ParticleEmitter(Particle emitter, ParticleEngine engine);

    public class EmitterParticle : Particle
    {
        ParticleEmitter pe;

        public EmitterParticle(ParticleEngine engine, Vector2 position, ParticleEmitter pe)
            : base(engine, position)
        {
            this.pe = pe;
        }

        public override void Update()
        {
            pe(this, engine);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch) { }
    }
}
