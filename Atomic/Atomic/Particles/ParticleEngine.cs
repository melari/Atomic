using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
                

        public void AddParticle(Particle part)
        {
            AddParticle(part, false);
        }
        public void AddParticle(Particle part, bool additive)
        {
            if (additive) { additiveParticles.AddBuffer.Add(part); }
            else { alphaParticles.AddBuffer.Add(part); }            
        }        

        public void DestroyParticle(Particle part)
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


        /* =========================================================
         * Below are helper functions to create
         * predetermined particle patterns (ie fire, smoke, ect.)
         * ======================================================= */
        public void CreateFire(Vector2 position, Color color, float scale = 0.25f, bool smoke = true)
        {
            Particle p = new Flame(this, position, color);            
            new DriftMethod(p,
                        this,
                        new Vector2((float)MathExtra.rand.NextDouble() / 4 - 0.125f, (float)MathExtra.rand.NextDouble() - 1.5f),
                        Vector2.Zero,
                        150,
                        scale,
                        scale);                        
            this.AddParticle(p, true);

            if (smoke)
            {
                p = new Smoke(this, position - Vector2.UnitY*50);
                new DriftMethod(p, this,
                                    new Vector2((float)MathExtra.rand.NextDouble() / 4 - 0.125f, (float)MathExtra.rand.NextDouble() - 1.25f),
                                    new Vector2(0.001f, 0.0f),
                                    300,
                                    scale/2,
                                    scale);
                this.AddParticle(p);
            }
        }
    }
}
