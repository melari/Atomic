using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{

    /*
     * WorldObjects are usually contained within a Screen object and would be the 
     * active objects in the game world. They are defined by a position and a velocity.
     * By default physics, position is the integral of velocity and is updated every Update()
     */

    class WorldObject
    {
        protected Screen screen;
        public Vector2 position;
        public Vector2 velocity;

        public WorldObject(Screen screen)
            : this(screen, Vector2.Zero) { }
        public WorldObject(Screen screen, Vector2 position)
            : this(screen, position, Vector2.Zero) { }
        public WorldObject(Screen screen, Vector2 position, Vector2 velocity)
        {
            this.screen = screen;
            this.position = position;
            this.velocity = velocity;
        }

        public virtual void Update()
        {
            position += velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
