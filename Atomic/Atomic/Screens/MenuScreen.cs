using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Atomic
{
    class MenuItem
    {
        public static readonly float MOVE_SPEED = 5;

        public string text;        
        public float x;
        public float dest_x;

        public MenuItem(string text, float x, float dest_x)
        {
            this.text = text;
            this.x = x;            
            this.dest_x = x;
        }

        public void Update()
        {
            x += (dest_x - x)/MOVE_SPEED;
        }

        public virtual string GetText()
        {
            return text;
        }
    }

    class MenuScreen : Screen
    {
        public static readonly float DEFAULT_POS = 30;
        public static readonly float SELECTED_POS = 100;

        List<MenuItem> menuItems = new List<MenuItem>();
        int selected = 0;        

        public MenuScreen(Engine engine)
            : base(engine)
        {            
            menuItems.Add(new MenuItem("Partical Example", -100, DEFAULT_POS));
            menuItems.Add(new MenuItem("Multiplayer Example", -600, DEFAULT_POS));
            menuItems.Add(new MenuItem("Options", -1100, DEFAULT_POS));
            menuItems.Add(new MenuItem("Exit", -1600, DEFAULT_POS));
        }

        public override bool ExecuteCommand(string c, string[] args, Console console)
        {
            return false;
        }

        public override void Update()
        {
            if (Input.KeyPressed(Keys.Up))
            { 
                selected--;
                if (selected < 0) { selected = menuItems.Count - 1; }
            }

            if (Input.KeyPressed(Keys.Down)) 
            { 
                selected++;
                if (selected == menuItems.Count) { selected = 0; }
            }

            if (Input.KeyPressed(Keys.Enter))
            {
                switch (selected)
                {
                    case 0:
                        engine.ChangeScreen(new TestScreen(engine), new Fade(0.1f));
                        break;
                    case 1:
                        engine.ChangeScreen(new MultiplayerTestScreen(engine), new Fade(0.1f));
                        break;
                    case 2:
                        engine.ChangeScreen(new OptionsScreen(engine), new Fade(0.1f));
                        break;
                    case 3:
                        engine.Exit();
                        break;
                }
            }

            for (int i = 0; i < menuItems.Count; i++)
            {                
                if (i == selected) { menuItems[i].dest_x = SELECTED_POS; }
                else { menuItems[i].dest_x = DEFAULT_POS; }
                menuItems[i].Update();
            }            
        }

        public override void  Draw(SpriteBatch spriteBatch)
        {                                   
            spriteBatch.Begin();
            spriteBatch.DrawString(Resources.GetFont("TitleFont"), "Atomic - Simple 2d Class Library", new Vector2(300, 10), Color.Black);

            float y = 200;
            foreach(MenuItem item in menuItems)
            {
                spriteBatch.DrawString(Resources.GetFont("MenuFont"), item.GetText(), new Vector2(item.x, y), Color.White);
                y += 50;
            }
            DrawHelp.DrawCircle(spriteBatch, Input.mouse, 5, Color.Black, 8);
            spriteBatch.End();            
        }
    }
}
