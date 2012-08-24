using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public abstract class RectangleCollidable : GameObject
    {
        Vector2 size;
        public Vector2 center { get { return position + size / 2; } set { position = value - size / 2; } }
        public float bottom { get { return position.Y + size.Y; } set { position.Y = value - size.Y; } }
        public float right { get { return position.X + size.X; } set { position.X = value - size.X; } }

        public RectangleCollidable(Atom a, Vector2 position, Vector2 size)
            : base(a, position)
        {
            this.size = size;
        }

        public Vector2 Move(List<RectangleCollidable> objs)
        {
            velocity += acceleration;
            acceleration = Vector2.Zero;
            return new Vector2(MoveX(objs), MoveY(objs));
        }

        public virtual void CollideWith(RectangleCollidable obj) { }

        private float MoveX(List<RectangleCollidable> objs)
        {
            float _normal = 0;

            // Calculate the minimum and maximum values for x and y.
            float _minX = float.NegativeInfinity;
            float _maxX = float.PositiveInfinity;

            // Sets the closest object collided with to null
            RectangleCollidable _closestMinObj = null;
            RectangleCollidable _closestMaxObj = null;

            // Checks each object in the room and finds which objects restrict X.
            foreach (RectangleCollidable obj in objs)
            {
                if (obj == this) continue;  // Don't check for collision with self

                // Finds only the objects at the same Y level as this one.
                if (this.bottom <= obj.position.Y || this.position.Y >= obj.bottom) continue;

                // Checks if the object if to the left, and if it is closer than the current minimum X.
                if (velocity.X < 0 && obj.right > _minX && obj.right <= position.X)
                {
                    // Sets this object as the closest from the left.
                    _minX = obj.right;
                    _closestMinObj = obj;
                }

                // Checks if the object is to the right, and if it is closer than the current maximum X.
                else if (velocity.X > 0 && obj.position.X < _maxX && obj.position.X >= right)
                {
                    // Sets this object as the closest from the right.
                    _maxX = obj.position.X;
                    _closestMaxObj = obj;
                }
            }

            // Checks if after moving the maximum X is violated.
            if (velocity.X > 0 && this.right + this.velocity.X > _maxX)
            {
                _normal = _maxX - (this.right + this.velocity.X);
                this.velocity.X = 0;
                this.position.X = _maxX - this.size.X;
                // Calls the CollideWith method.
                if (_closestMaxObj != null) this.CollideWith(_closestMaxObj);
            }
            // Checks if after moving the minimum X is violated.
            else if (velocity.X < 0 && this.position.X + this.velocity.X < _minX)
            {
                _normal = _minX - this.position.X - this.velocity.X;
                this.position.X = _minX;
                this.velocity.X = 0;
                if (_closestMinObj != null) this.CollideWith(_closestMinObj);
            }
            else // If all is well, it moves the object.
            {
                this.position.X += this.velocity.X;
            }

            return _normal;
        }

        private float MoveY(List<RectangleCollidable> objs)
        {
            float _normal = 0;

            float _minY = float.NegativeInfinity;
            float _maxY = float.PositiveInfinity;

            RectangleCollidable _closestMinObj = null;
            RectangleCollidable _closestMaxObj = null;

            // Investigates all objects to find max and min.
            foreach (RectangleCollidable obj in objs)
            {
                if (obj == this) continue;  // Don't check for collision with self

                if (right <= obj.position.X || position.X >= obj.right) continue;

                if (velocity.Y > 0 && obj.position.Y < _maxY && obj.position.Y >= bottom)
                {
                    _maxY = obj.position.Y;
                    _closestMaxObj = obj;
                }
                else if (velocity.Y < 0 && obj.bottom <= position.Y && obj.bottom > _minY)
                {
                    _minY = obj.bottom;
                    _closestMinObj = obj;
                }

            }

            // Updates the objects position accordingly.
            if (velocity.Y > 0 && bottom + velocity.Y > _maxY)
            {
                _normal = _maxY - (bottom + velocity.Y);
                this.velocity.Y = 0;
                this.position.Y = _maxY - size.Y;
                this.CollideWith(_closestMaxObj);
            }
            else if (velocity.Y < 0 && position.Y + velocity.Y < _minY)
            {
                _normal = _minY - (position.Y + velocity.Y);
                this.velocity.Y = 0;
                this.position.Y = _minY;
                this.CollideWith(_closestMinObj);
            }
            else
            {
                this.position.Y += velocity.Y;
            }

            return _normal;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawHelp.DrawRectangle(spriteBatch, position, size, Color.White);
        }
    }
}
