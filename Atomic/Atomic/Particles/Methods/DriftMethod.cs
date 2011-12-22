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

        Color baseColor;
        
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
            part.rotation = (float)(MathExtra.rand.NextDouble() * Math.PI * 2);

            baseColor = part.color;
            part.color = Color.Transparent;            
        }

        public override void Update()
        {
            velocity += gravity;
            part.position += velocity;

            normalizedLifetime = 1 - ((float)(life--) / lifeLength);

            float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
            part.color = new Color(((float)baseColor.R / 255f) * alpha, ((float)baseColor.G / 255f) * alpha, ((float)baseColor.B / 255f) * alpha, alpha);
            part.scale = startScale + (endScale - startScale) * normalizedLifetime;

            if (life <= 0)
            {
                engine.DestroyParticle(part);
            }
        }        
    }
}
