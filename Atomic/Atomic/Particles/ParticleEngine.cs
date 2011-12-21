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
        public BufferedList<Particle> alphaParticles = new BufferedList<Particle>();
        public BufferedList<Particle> additiveParticles = new BufferedList<Particle>();

        public int updateRate = -1;
        public int drawRate = -1;
        int updateCount = 0;
        int drawCount = 0;

        public void AddParticle(Particle part)
        {
            AddParticle(part, false);
        }
        public void AddParticle(Particle part, bool additive)
        {
            if (additive) { additiveParticles.Add(part); }
            else { alphaParticles.Add(part); }
        }

        public void DestroyParticle(Particle part)
        {
            alphaParticles.Remove(part);
            additiveParticles.Remove(part);
        }

        public void BufferedDestroyParticle(Particle part)
        {
            alphaParticles.RemoveBuffer.Add(part);
            additiveParticles.RemoveBuffer.Add(part);
        }

        public int ParticleCount()
        {
            return additiveParticles.Count + alphaParticles.Count;
        }

        public void Update()
        {
            if (updateRate == -1 || updateCount++ > 60 - updateRate)
            {
                updateCount = 0;

                foreach (Particle p in alphaParticles)
                {
                    p.Update();
                }
                foreach (Particle p in additiveParticles)
                {
                    p.Update();
                }
                alphaParticles.ApplyBuffers();
                additiveParticles.ApplyBuffers();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            foreach (Particle p in alphaParticles)
            {
                p.Draw(spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            foreach (Particle p in additiveParticles)
            {
                p.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
