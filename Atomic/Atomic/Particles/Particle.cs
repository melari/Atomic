using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{    
    /*
     * A Particle defines a special effect. They may only directly define Draw methods, but no Update.
     * For this, a ParticleMethod object must be created and set to control the particle.
     */

    abstract class Particle
    {
        protected ParticleEngine engine;        
        protected ParticleMethod method;

        public Vector2 position;        
        public float scale = 1.0f;
        public float rotation = 0.0f;
        public Color color = Color.White;

        public Particle(ParticleEngine engine, Vector2 position)
        {
            this.engine = engine;
            this.position = position;                        

            method = new DeadMethod(this, engine);
        }

        public void SetMethod(ParticleMethod method)
        {
            this.method = method;
        }

        public ParticleMethod GetMethod()
        {
            return method;
        }

        public void Update()
        {
            method.Update();
        }

        public abstract void Draw(SpriteBatch spriteBatch);        
    }
}
