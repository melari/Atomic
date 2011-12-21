using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class Smoke : Particle
    {
        public Smoke(ParticleEngine engine, Vector2 position)
            : base(engine, 
                    position,                     
                    Resources.GetSprite("PART_Smoke"))
        {
            method = new DriftMethod(this, engine,
                                    new Vector2((float)MathExtra.rand.NextDouble() / 4 - 0.125f, (float)MathExtra.rand.NextDouble() - 1.25f),
                                    new Vector2(0.001f, 0.0f),
                                    300,
                                    0.125f,
                                    0.25f);            
        }
    }
}
