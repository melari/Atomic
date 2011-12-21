using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class TestScreen : Screen
    {
        ParticleEngine particleEngine = new ParticleEngine();

        public TestScreen(Engine engine)
            : base(engine, Color.Black)
        {
        }

        public override bool ExecuteCommand(string c, string[] args, Console console)
        {
            switch (c)
            {
                case "help":
                    console.AddLine("==SCREEN COMMANDS==");
                    console.AddLine("menu - return to the main menu.");
                    break;

                case "menu":
                    engine.ChangeScreen(new MenuScreen(engine), new Fade(0.1f));
                    return true;                    

                default:
                    return false;
            }
            return false;
        }

        public override void Update()
        {
            particleEngine.Update();
            if (MathExtra.rand.NextDouble() < 0.2f)
            {
                particleEngine.AddParticle(new Flame(particleEngine, new Vector2(VideoSettings.resolution.X / 2, VideoSettings.resolution.Y / 2)), true);
                particleEngine.AddParticle(new Smoke(particleEngine, new Vector2(VideoSettings.resolution.X / 2, VideoSettings.resolution.Y / 2 - 50)));                
            }

            if (MathExtra.rand.NextDouble() < 0.4f)
            {                
                particleEngine.AddParticle(new Flame(particleEngine, new Vector2(VideoSettings.resolution.X / 4, VideoSettings.resolution.Y / 2), Color.Green), true);
                particleEngine.AddParticle(new Flame(particleEngine, new Vector2(VideoSettings.resolution.X / 4 * 3, VideoSettings.resolution.Y / 2), Color.Blue), true);                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            particleEngine.Draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.DrawString(Resources.GetFont("TitleFont"), "Atomic - Example Particle Effects Screen", new Vector2(300, 10), Color.White);
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), "Particles: " + particleEngine.ParticleCount().ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.End();
        }
    }
}
