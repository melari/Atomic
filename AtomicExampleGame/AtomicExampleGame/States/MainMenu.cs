using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtomicExampleGame
{
    class MainMenu : MenuState
    {
        GuiManager guiManager = new GuiManager();

        public MainMenu(Atom a, int layer)
            : base(a, layer, "Atomic Engine Example Game")
        {
            AddSlidingMenuItem("Start", delegate(MenuState menu)
            {
                a.stateManager.EndState(this);

                TestRoom room = new TestRoom((Engine)a, 0);
                a.stateManager.StartState(new FadeTransition(a, 1, room));
            });

            AddSlidingMenuItem("Options", delegate(MenuState menu)
            {
                a.stateManager.StartState(new OptionsMenu(a, 1));
            });

            AddSlidingMenuItem("Exit", delegate(MenuState menu)
            {
                a.Exit();
            });


            guiManager.Add(new InputField(new Vector2(500, 500), new Vector2(150, 25)));
            guiManager.Add(new InputField(new Vector2(500, 530), new Vector2(150, 25), true));
            guiManager[0].tab = guiManager[1];
            guiManager[1].tab = guiManager[0];
        }

        public override void Update()
        {
            guiManager.Update();
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            guiManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
