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
            : base(engine, 
                    position,                                                            
                    Resources.GetSprite("PART_Fire"))                  
        {
            this.color = color;
            this.method = new DriftMethod(this,
                            engine,
                            new Vector2((float)MathExtra.rand.NextDouble() / 4 - 0.125f, (float)MathExtra.rand.NextDouble() - 1.5f),
                            Vector2.Zero,
                            150,
                            0.25f,
                            0.25f);
        }
    }
}
