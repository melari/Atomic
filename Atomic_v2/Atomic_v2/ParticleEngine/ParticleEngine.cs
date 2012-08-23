using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public class ParticleEngine : IConsoleCommandable
    {
        public BufferedList<Particle> alphaParticles = new BufferedList<Particle>();
        public BufferedList<Particle> additiveParticles = new BufferedList<Particle>();


        public void RegisterCommands(ConsoleState cs)
        {
            cs.AddCommand("count", delegate(ConsoleState cl, string[] args)
            {
                cs.AddLine(alphaParticles.Count.ToString() + " alpha particles.");
                cs.AddLine(additiveParticles.Count.ToString() + " additive particles.");
                cs.AddLine((alphaParticles.Count + additiveParticles.Count).ToString() + " total.", Color.Green);
            },
            "Shows the total number of existing particles."
            );
        }

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
            Particle p = new SpriteParticle(this, Resources.GetSprite("p_explosion"), position, color);
            p.rotation = (float)(MathExtra.random.NextDouble() * Math.PI * 2);
            p.maxSpeed = 2;
            p.SetLife(150);
            p.scale = scale;
            p.alpha = 0;
            p.methods.Add(new ConstantForceMethod(this, new Vector2((float)MathExtra.random.NextDouble() / 4 - 0.125f, (float)MathExtra.random.NextDouble() / 2f - 1f)));
            p.methods.Add(new FadeInOutMethod(this));
            this.AddParticle(p, true);

            if (smoke)
            {
                p = new SpriteParticle(this, Resources.GetSprite("p_smoke"), position - Vector2.UnitY * 50, Color.White);
                p.rotation = (float)(MathExtra.random.NextDouble() * Math.PI * 2);
                p.maxSpeed = 1.5f;
                p.SetLife(300);
                p.alpha = 0;
                p.methods.Add(new ScaleMethod(this, scale / 2, scale));
                p.methods.Add(new ConstantForceMethod(this, new Vector2((float)MathExtra.random.NextDouble() / 4 - 0.125f, (float)MathExtra.random.NextDouble() / 2f - 1f)));
                p.methods.Add(new FadeInOutMethod(this));
                this.AddParticle(p);
            }
        }
    }
}
