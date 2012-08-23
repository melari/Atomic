using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    static class DrawHelp
    {
        public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, Vector2 size, Color color)
        {
            DrawRectangle(spriteBatch, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y, color);
        }
        public static void DrawRectangle(SpriteBatch spriteBatch, int x, int y, int width, int height, Color color)
        {
            spriteBatch.Draw(Resources.GetSprite("Pixel"), new Rectangle(x, y, width, height), color);
        }

        public static void DrawRectangleOutline(SpriteBatch spriteBatch, float x, float y, float width, float height, Color color)
        {
            DrawLine(spriteBatch, new Vector2(x, y), new Vector2(x + width, y), color);
            DrawLine(spriteBatch, new Vector2(x + width, y), new Vector2(x + width, y + height), color);
            DrawLine(spriteBatch, new Vector2(x + width, y + height), new Vector2(x, y + height), color);
            DrawLine(spriteBatch, new Vector2(x, y + height), new Vector2(x, y), color);
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 position, float radius, Color color, int resolution)
        {
            Vector2 first = position + new Vector2(MathExtra.ComponentX(radius, 0f), MathExtra.ComponentY(radius, 0f));
            Vector2 prev = first;
            for (int i = 1; i < resolution; i++)
            {
                float angle = (float)((float)i * 2.0f * Math.PI / resolution);
                Vector2 p = position + new Vector2(MathExtra.ComponentX(radius, angle), MathExtra.ComponentY(radius, angle));
                DrawLine(spriteBatch, prev, p, color);
                prev = p;
            }
            DrawLine(spriteBatch, prev, first, color);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 vector1, Vector2 vector2, Color color)
        {
            float distance = Vector2.Distance(vector1, vector2);
            float angle = (float)Math.Atan2((double)(vector2.Y - vector1.Y), (double)(vector2.X - vector1.X));
            spriteBatch.Draw(Resources.GetSprite("Pixel"), vector1, null, color, angle, Vector2.Zero, new Vector2(distance, 1), SpriteEffects.None, 0);
        }
    }
}
