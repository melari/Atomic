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
        protected ParticleEngine engine;
        public Vector2 position;
        public Texture2D sprite;
        protected ParticleMethod method;
        
        public Color color = new Color(1.0f, 1.0f, 1.0f, 0);           

        public Particle(ParticleEngine engine, Vector2 position, Texture2D sprite)
        {
            this.engine = engine;
            this.position = position;            
            this.sprite = sprite;

            method = new DeadMethod(this, engine);
        }        

        public virtual void Update()
        {
            method.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            method.Draw(spriteBatch);
        }
    }
}
