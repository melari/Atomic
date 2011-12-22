using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class Flame : Particle
    {        
        public Flame(ParticleEngine engine, Vector2 position)
            : this(engine, position, Color.White)
        {                        
        }
        public Flame(ParticleEngine engine, Vector2 position, Color color)
            : base(engine, position)                  
        {
            this.color = color;            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(Resources.GetSprite("PART_Fire"), position, null, color, rotation, new Vector2(256, 256), scale, SpriteEffects.None, 0.0f);
        }
    }
}
