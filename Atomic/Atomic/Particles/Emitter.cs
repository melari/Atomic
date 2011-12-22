using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{

    /*
     * The emitter particle is an invisible particle that draws nothing to the screen and
     * simply becomes a container for a ParticleMethod.
     * This is most commonly used for a particle whose only job is to create more particles.
     * For example - an Emitter with a FireballMethod will make a moving ball with a tail emerging
     * in the oposite direction of movement.
     */

    class Emitter : Particle
    {
        public Emitter(ParticleEngine engine, Vector2 position)
            : base(engine, position)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
        }
    }
}
