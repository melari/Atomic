using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class FPSCounterState : State
    {
        int fps = 0;
        int count = 0;

        int update_fps = 0;
        int update_count = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public FPSCounterState(Atom a, int layer)
            : base(a, layer)
        {
        }

        public override void BackgroundUpdate(GameTime gameTime) { Update(gameTime); }
        public override void Update(GameTime gameTime)
        {
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

        public override void BackgroundDraw(SpriteBatch spriteBatch) { Draw(spriteBatch); }
        public override void Draw(SpriteBatch spriteBatch)
        {
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
