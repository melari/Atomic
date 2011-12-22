using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class FireballMethod : ParticleMethod
    {
        Vector2 velocity;
        Color color;

        public FireballMethod(Particle part, ParticleEngine engine, Vector2 velocity, Color color)
            : base(part, engine)
        {
            this.velocity = velocity;
            this.color = color;
        }

        public override void Update()
        {
            part.position += velocity;
            if (part.position.Y < 0)
            {
                engine.DestroyParticle(part);
            }
            
            Particle tail = new Flame(engine, part.position, color);
            new DriftMethod(tail, engine, Vector2.Zero, Vector2.Zero, 50, 0.15f, 0.01f);                        
            engine.AddParticle(tail, true);            
        }        
    }
}
