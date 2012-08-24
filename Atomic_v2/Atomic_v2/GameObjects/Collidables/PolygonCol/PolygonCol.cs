using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public class PolygonCol : GameObject
    {
        public List<Edge> edges { get; protected set; }
        public List<Vector2> points { get; protected set; }
        public bool closed { get; protected set; }

        public PolygonCol(Atom a, Vector2 point1, Vector2 point2)
            : base(a, point1)
        {
            edges = new List<Edge>();
            points = new List<Vector2>();

            points.Add(point1);
            points.Add(point2);
            edges.Add(new Edge(point1, point2));
        }

        public void AddPoint(Vector2 point)
        {
            edges.Add(new Edge(points.Last(), point));
            points.Add(point);
        }

        public void Close()
        {
            edges.Add(new Edge(points.Last(), points.First()));
            closed = true;
        }

        public void Translate(Vector2 offset)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] += offset;
            }
            foreach (Edge e in edges)
            {
                e.Translate(offset);
            }
        }

        public virtual void Update(List<PolygonCol> polys, int depth = 0)
        {
            velocity += acceleration;
            acceleration = Vector2.Zero;

            float maxDist = velocity.Length();
            Vector2 collisionPoint = Vector2.Zero;
            Edge collisionEdge = null;
            bool collision = false;

            foreach (PolygonCol poly in polys)
            {
                if (this != poly)
                {
                    foreach (Vector2 vertex in poly.points)
                    {
                        Edge e = new Edge(vertex, vertex - velocity);
                        foreach (Edge edge in edges)
                        {
                            if (!edge.IsParallel(e))
                            {
                                Vector2 intersection = edge.FindIntersection(e);
                                if (e.InRange(intersection) && edge.InRange(intersection))
                                {
                                    maxDist = Math.Min(maxDist, (intersection - vertex).Length());
                                    collisionPoint = intersection;
                                    collisionEdge = edge;
                                    collision = true;
                                }
                            }
                        }
                    }
                }
            }

            foreach (Vector2 vertex in points)
            {
                Edge e = new Edge(vertex, vertex + velocity);
                foreach (PolygonCol poly in polys)
                {
                    if (this != poly)
                    {
                        foreach (Edge other in poly.edges)
                        {
                            if (!other.IsParallel(e))
                            {
                                Vector2 intersection = other.FindIntersection(e);
                                Vector2 a = velocity;
                                a.Normalize();
                                if (a == Vector2.UnitX && (intersection - vertex).Length() < 1)
                                {
                                }
                                if (e.InRange(intersection) && other.InRange(intersection))
                                {
                                    maxDist = Math.Min(maxDist, (intersection - vertex).Length());
                                    collisionPoint = intersection;
                                    collisionEdge = other;
                                    collision = true;
                                }
                            }
                        }
                    }
                }
            }



            if (collision && velocity != Vector2.Zero)
            {
                Vector2 vel_unit = velocity;
                vel_unit.Normalize();
                Translate(vel_unit * Math.Max(0, maxDist - 0.1f));

                if (depth < 2)
                {
                    Vector2 wall = collisionEdge.vector;
                    wall.Normalize();
                    velocity = wall * (float)Vector2.Dot(velocity, wall);

                    Update(polys, depth + 1);
                }
                else
                {
                    velocity = Vector2.Zero;
                }
            }
            else
            {
                Translate(velocity);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Edge e in edges)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
