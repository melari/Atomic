using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    static class FpsCounter
    {
        public static bool ENABLED = false;        

        static int fps = 0;
        static int count = 0;

        static int update_fps = 0;
        static int update_count = 0;
        static TimeSpan elapsedTime = TimeSpan.Zero;

        public static void Update(GameTime gameTime)
        {
            if (!ENABLED) { return; }

            update_count++;

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                fps = count;
                count = 0;
                update_fps = update_count;
                update_count = 0;                
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (!ENABLED) { return; }
            count++;
            
            spriteBatch.Begin();
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), "FPS: " + fps.ToString(), new Vector2(33, 33), Color.Black);
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), "FPS: " + fps.ToString(), new Vector2(32, 32), Color.White);

            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), "UFPS: " + update_fps.ToString(), new Vector2(33, 66), Color.Black);
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), "UFPS: " + update_fps.ToString(), new Vector2(32, 65), Color.White);
            spriteBatch.End();
        }
    }
}
