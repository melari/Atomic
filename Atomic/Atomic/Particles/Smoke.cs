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
            : base(engine, position)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Resources.GetSprite("PART_Smoke"), position, null, color, rotation, new Vector2(256, 256), scale, SpriteEffects.None, 0.0f);
        }
    }
}
