using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{

    /*
     * Particles are generally used for special effects in games. There are many different settings available
     * to change how the particles behave.
     */

    class Particle
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 gravity;
        protected ParticleEngine engine;

        protected Texture2D sprite;        
        private float normalizedLifetime;
        protected float lifeLength = 300;
        protected float life;
        protected Color color = new Color(1.0f, 1.0f, 1.0f, 0);
        protected float rotation = 0.0f;
        protected float startScale = 0.75f;
        protected float endScale = 1.0f;    

        public Particle(ParticleEngine engine, Vector2 position, float lifeLength)
            : this(engine, position, Vector2.Zero, Vector2.Zero, lifeLength)
        {
        }
        public Particle(ParticleEngine engine, Vector2 position, Vector2 velocity, float lifeLength)
            : this(engine, position, velocity, Vector2.Zero, lifeLength)
        {
        }
        public Particle(ParticleEngine engine, Vector2 position, Vector2 velocity, Vector2 gravity, float lifeLength)
        {
            this.engine = engine;
            this.position = position;
            this.velocity = velocity;
            this.gravity = gravity;

            this.lifeLength = lifeLength;
            life = lifeLength;
        }        

        public virtual void Update()
        {
            velocity += gravity;
            position += velocity;

            normalizedLifetime = 1 - ((float)(life--) / lifeLength);

            if (life <= 0)
            {
                engine.BufferedDestroyParticle(this);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
            Color drawColor = new Color(((float)color.R/255f) * alpha, ((float)color.G/255f) * alpha, ((float)color.B/255f) * alpha, alpha);
            float drawScale = startScale + (endScale - startScale) * normalizedLifetime;         
            spriteBatch.Draw(sprite, position, null, drawColor, rotation, new Vector2(256, 256), drawScale, SpriteEffects.None, 0.0f);
        }
    }
}
