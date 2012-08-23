using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public class SpriteParticle : Particle
    {
        Texture2D sprite;
        Vector2 origin;

        public SpriteParticle(ParticleEngine engine, Texture2D sprite, Vector2 position)
            : this(engine, sprite, position, Color.White)
        {                        
        }
        public SpriteParticle(ParticleEngine engine, Texture2D sprite, Vector2 position, Color color)
            : base(engine, position)                  
        {
            this.sprite = sprite;
            this.color = color;

            origin = Resources.GetSpriteCenter(sprite);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(sprite, position, null, color * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
