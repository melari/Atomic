using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public abstract class Particle
    {
        protected ParticleEngine engine;
        public List<ParticleMethod> methods = new List<ParticleMethod>();

        public Vector2 position, velocity = Vector2.Zero, acceleration = Vector2.Zero;
        public float scale = 1.0f;
        public float rotation = 0.0f;
        public float maxSpeed = 1000;
        public float alpha = 1;
        public Color color = Color.White;

        public float normalizedLifetime, life, lifeLength;

        public Particle(ParticleEngine engine, Vector2 position)
            : this(engine, position, -1) { }
        public Particle(ParticleEngine engine, Vector2 position, float lifeLength)
        {
            this.engine = engine;
            this.position = position;
        }

        public void SetLife(float life)
        {
            this.life = life;
            lifeLength = life;
            normalizedLifetime = 1;
        }

        public void AddForce(Vector2 force)
        {
            acceleration += force;
        }

        public virtual void Update()
        {
            if (life != -1)
                normalizedLifetime = 1 - ((float)(life--) / lifeLength);
            else
                normalizedLifetime = 1;
            if (life == 0)
                engine.DestroyParticle(this);

            foreach (ParticleMethod method in methods)
                method.Update(this);

            velocity += acceleration;
            MathExtra.RestrictVectorLength(ref velocity, maxSpeed);
            position += velocity;
            acceleration = Vector2.Zero;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
