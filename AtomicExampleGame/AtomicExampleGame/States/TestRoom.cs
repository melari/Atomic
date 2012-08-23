using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtomicExampleGame
{
    public class TestRoom : WorldState, IConsoleCommandable
    {
        ParticleEngine pe = new ParticleEngine();

        public TestRoom(Engine a, int layer)
            : base(a, layer)
        {
            a.console.RegisterStateCommands(this);
            a.console.RegisterStateCommands(pe);
            a.console.Execute("fps true");

            objects.Add(new Player(a, new Vector2(100f, 100f)));

            Particle p = new EmitterParticle(pe, new Vector2(a.resolution.X / 4 * 3, a.resolution.Y * 3 / 4 + 50), delegate(Particle emitter, ParticleEngine engine)
            {
                Particle tail = new SpriteParticle(engine, Resources.GetSprite("p_explosion"), emitter.position, Color.Orange);
                tail.rotation = (float)(MathExtra.random.NextDouble() * Math.PI * 2);
                tail.SetLife(150);
                tail.alpha = 0;
                tail.methods.Add(new FadeInOutMethod(engine));
                tail.methods.Add(new ScaleMethod(engine, 0.15f, 0.01f));
                engine.AddParticle(tail, true);
            });
            p.methods.Add(new PointForceMethod(pe, new Vector2(a.resolution.X / 4 * 3 + 50, a.resolution.Y * 3 / 4 + 50), 0.5f));
            p.velocity = new Vector2(0.0f, -10.0f);
            pe.AddParticle(p);
        }

        public void RegisterCommands(ConsoleState cs)
        {
            cs.AddCommand("test", delegate(ConsoleState cl, string[] args)
            {
                cl.AddLine("It works!", Color.Blue);
            },
            "A test command defined from an external state."
            );

            cs.AddCommand("menu", delegate(ConsoleState cl, string[] args)
            {
                cs.DeRegisterStateCommands(this);
                cs.DeRegisterStateCommands(pe);
                a.stateManager.EndState(this);
                a.stateManager.StartState(new MainMenu(a, 0));
            },
            "Returns to the main menu."
            );
        }

        public override void BackgroundUpdate() { Update(); }
        public override void Update()
        {
            if (MathExtra.random.NextDouble() < 0.2f)
            {
                pe.CreateFire(a.resolution/2, Color.White, 0.25f, true);
            }

            if (MathExtra.random.NextDouble() < 0.4f)
            {
                pe.CreateFire(new Vector2(a.resolution.X / 4, a.resolution.Y / 2), Color.Green, 0.25f, false);
                pe.CreateFire(new Vector2(a.resolution.X / 4 * 3, a.resolution.Y / 2), Color.Blue, 0.25f, false);
            }

            pe.Update();
            base.Update();
        }

        public override void BackgroundDraw(SpriteBatch spriteBatch) { Draw(spriteBatch); }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            base.Draw(spriteBatch);
            pe.Draw(spriteBatch);
        }
    }
}
