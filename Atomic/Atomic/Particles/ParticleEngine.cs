using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{

    /*
     * ParticleEngine is simply a container for many particle objects.
     * There are several functions to manage creation and removal of particles
     * as well as the standard Update/Draw that must be called.
     */

    class ParticleEngine
    {        
        public BufferedList<Particle> particles = new BufferedList<Particle>();
        

        public void AddParticle(Particle part)
        {
            particles.Add(part);
        }

        public void DestroyParticle(Particle part)
        {
            particles.Remove(part);
        }        

        public void Update()
        {
            foreach (Particle p in particles)
            {
                p.Update();
            }
            particles.ApplyBuffers();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}
