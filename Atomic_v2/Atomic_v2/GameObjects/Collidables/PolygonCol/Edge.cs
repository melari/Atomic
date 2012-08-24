using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public class Edge
    {
        public Vector2 start { get; private set; }
        public Vector2 end { get; private set; }
        public float slope { get; private set; }

        public bool vertical { get; private set; } //special handling for undefined slopes on verticle lines.

        public Vector2 vector { get; private set; }

        public Edge(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;

            slope = (end.Y - start.Y) / (end.X - start.X);
            vertical = end.X == start.X;

            vector = new Vector2(end.X - start.X, end.Y - start.Y);
        }

        public void Translate(Vector2 offset)
        {
            start += offset;
            end += offset;
        }

        public bool IsParallel(Edge other)
        {
            return other.slope == slope;
        }

        /**
         * Answer is invalid for edges that are parallel.
         * Make sure to check if they are parallel in advance by using the IsParallel() function.
         */
        public Vector2 FindIntersection(Edge other)
        {
            double x, y;
            if (vertical)
            {
                x = start.X;
                y = other.slope * (x - other.start.X) + other.start.Y;
            }
            else if (other.vertical)
            {
                x = other.start.X;
                y = slope * (x - start.X) + start.Y;
            }
            else
            {
                x = (start.Y - other.start.Y + other.slope * other.start.X - slope * start.X) / (other.slope - slope);
                y = slope * (x - start.X) + start.Y;
            }
            return new Vector2((float)x, (float)y);
        }

        public bool InRange(Vector2 point)
        {
            return (((start.X >= end.X && point.X <= start.X && point.X >= end.X) ||
                     (start.X < end.X && point.X >= start.X && point.X <= end.X)) &&
                    ((start.Y >= end.Y && point.Y <= start.Y && point.Y >= end.Y) ||
                     (start.Y < end.Y && point.Y >= start.Y && point.Y <= end.Y)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawHelp.DrawLine(spriteBatch, start, end, Color.Black);
        }
    }
}
