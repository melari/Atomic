using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{

    /*
     * Particles are generally used for special effects in games. They have a position and velocity just 
     * like any other game object, but also can have a gravity that is self contained. This can be used
     * to make interesting effects.
     */

    class Particle
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 gravity;
        protected ParticleEngine engine;

        public Particle(ParticleEngine engine, Vector2 position)
            : this(engine, position, Vector2.Zero, Vector2.Zero)
        {
        }
        public Particle(ParticleEngine engine, Vector2 position, Vector2 velocity)
            : this(engine, position, velocity, Vector2.Zero)
        {
        }
        public Particle(ParticleEngine engine, Vector2 position, Vector2 velocity, Vector2 gravity)
        {
            this.engine = engine;
            this.position = position;
            this.velocity = velocity;
            this.gravity = gravity;
        }

        // Destroy only tells the ParticleEngine that this particle would like to be removed.
        // It is still up to the manager when those changes are actually applied.
        public void Destroy()
        {
            engine.particles.RemoveBuffer.Add(this);
        }

        public virtual void Update()
        {
            velocity += gravity;
            position += velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
