using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{
    class FollowPointMethod : ParticleMethod
    {
        Vector2 point;
        float strength;
        public Vector2 velocity = Vector2.Zero;

        public FollowPointMethod(Particle part, ParticleEngine engine, Vector2 point, float strength)
            : base(part, engine)
        {
            this.point = point;
            this.strength = strength;
        }

        public void SetFollowPoint(Vector2 point)
        {
            this.point = point;
        }

        public override void Update()
        {
            Vector2 dir = point - part.position;
            dir.Normalize();
            velocity += dir * strength;

            part.position += velocity;

            Particle tail = new Flame(engine, part.position, Color.Red);
            new DriftMethod(tail, engine, Vector2.Zero, Vector2.Zero, 50, 0.15f, 0.01f);
            engine.AddParticle(tail, true);            
        } 
    }
}
