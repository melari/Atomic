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

        public Vector2 position, velocity = Vector2.Zero, acceleration = Vector2.Zero;

        public GameObject(Atom a, Vector2 position)
        {
            this.a = a;
            this.position = position;
        }

        public virtual void ApplyForce(Vector2 force)
        {
            acceleration += force;
        }

        public virtual void ApplyFriction(float friction)
        {
            if (velocity.Length() <= friction)
            {
                ApplyForce(velocity * -1);
            }
            else
            {
                Vector2 frictionForce = velocity * -1;
                frictionForce.Normalize();
                ApplyForce(frictionForce * friction);
            }
        }

        public void UpdatePhysics()
        {
            position += velocity;
            velocity += acceleration;
            acceleration = Vector2.Zero;
        }

        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawHelp.DrawCircle(spriteBatch, position, 3, Color.White, 3);
        }
    }
}
