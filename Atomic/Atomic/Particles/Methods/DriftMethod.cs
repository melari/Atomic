using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class DriftMethod : ParticleMethod
    {        
        Vector2 velocity;
        Vector2 gravity;
        float lifeLength;
        float life;
        float normalizedLifetime;

        protected float rotation = 0.0f;
        protected float startScale = 0.75f;
        protected float endScale = 1.0f; 

        public DriftMethod(Particle part, ParticleEngine engine, Vector2 velocity, Vector2 gravity, float lifeLength, float startScale, float endScale)
            : base(part, engine)
        {            
            this.velocity = velocity;
            this.gravity = gravity;
            this.lifeLength = lifeLength;

            this.startScale = startScale;
            this.endScale = endScale;

            life = lifeLength;
            rotation = (float)(MathExtra.rand.NextDouble() * Math.PI * 2);
        }

        public override void Update()
        {
            velocity += gravity;
            part.position += velocity;

            normalizedLifetime = 1 - ((float)(life--) / lifeLength);

            if (life <= 0)
            {
                engine.BufferedDestroyParticle(part);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
            Color drawColor = new Color(((float)part.color.R / 255f) * alpha, ((float)part.color.G / 255f) * alpha, ((float)part.color.B / 255f) * alpha, alpha);
            float drawScale = startScale + (endScale - startScale) * normalizedLifetime;
            spriteBatch.Draw(part.sprite, part.position, null, drawColor, rotation, new Vector2(256, 256), drawScale, SpriteEffects.None, 0.0f);
        }
    }
}
