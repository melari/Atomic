using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Atomic
{
    class TestScreen : Screen
    {
        ParticleEngine particleEngine = new ParticleEngine();

        public TestScreen(Engine engine)
            : base(engine, Color.Black)
        {
            Particle p = new Emitter(particleEngine, new Vector2(VideoSettings.resolution.X / 4 * 3, VideoSettings.resolution.Y / 2 + 50));
            new FollowPointMethod(p, particleEngine, new Vector2(VideoSettings.resolution.X / 4 * 3 + 50, VideoSettings.resolution.Y / 2 + 50), 0.5f);
            ((FollowPointMethod)p.GetMethod()).velocity = new Vector2(0.0f, -10.0f);
            particleEngine.AddParticle(p);
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
                particleEngine.CreateFire(new Vector2(VideoSettings.resolution.X / 2, VideoSettings.resolution.Y / 2), Color.White, 0.25f, true);
            }

            if (MathExtra.rand.NextDouble() < 0.4f)
            {
                particleEngine.CreateFire(new Vector2(VideoSettings.resolution.X / 4, VideoSettings.resolution.Y / 2), Color.Green, 0.25f, false);
                particleEngine.CreateFire(new Vector2(VideoSettings.resolution.X / 4 * 3, VideoSettings.resolution.Y / 2), Color.Blue, 0.25f, false);
            }             

            if (Input.KeyPressed(Keys.Space))
            {                
                Particle p = new Emitter(particleEngine, new Vector2(10f, 10f));
                //new FireballMethod(p, particleEngine, new Vector2(0.0f, -10.0f), Color.Red);
                new FollowPointMethod(p, particleEngine, Input.mouse, 0.5f);
                particleEngine.AddParticle(p);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            particleEngine.Draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.DrawString(Resources.GetFont("TitleFont"), "Atomic - Example Particle Effects Screen", new Vector2(300, 10), Color.White);
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), "Particles: " + particleEngine.ParticleCount().ToString(), new Vector2(10, 10), Color.White);

            DrawHelp.DrawCircle(spriteBatch, Input.mouse, 5, Color.White, 8);
            DrawHelp.DrawCircle(spriteBatch, Input.mouse, 4, Color.Black, 8);
            spriteBatch.End();
        }
    }
}
