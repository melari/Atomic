using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public class GameObject
    {
        protected Atom a;

        protected Vector2 position, velocity = Vector2.Zero, acceleration = Vector2.Zero;
        protected Vector2 size;

        public GameObject(Atom a, Vector2 position) : this(a, position, Vector2.Zero) { } 
        public GameObject(Atom a, Vector2 position, Vector2 size)
        {
            this.a = a;
            this.position = position;
            this.size = size;
        }

        public virtual void Update()
        {
            position += velocity;
            velocity += acceleration;
            acceleration = Vector2.Zero;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawHelp.DrawRectangle(spriteBatch, position, size, Color.Black);
        }
    }
}
