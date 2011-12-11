using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    class VariableMenuItem : MenuItem
    {
        public string val;        

        public VariableMenuItem(string text, float x, float dest_x, string val)
            : base(text, x, dest_x)
        {
            this.val = val;            
        }

        public override string GetText()
        {
            return text + val.ToString();
        }        
    }

    class OptionsScreen : Screen
    {
        public static readonly float DEFAULT_POS = 30;
        public static readonly float SELECTED_POS = 100;

        List<MenuItem> menuItems = new List<MenuItem>();
        int selected = 0;

        public OptionsScreen(Engine engine)
            : base(engine, Color.Red)
        {            
            menuItems.Add(new VariableMenuItem("Resolution: ", -100, DEFAULT_POS, VideoSettings.resolution.X.ToString() + "x" + VideoSettings.resolution.Y.ToString()));            
            menuItems.Add(new VariableMenuItem("Fullscreen: ", -1100, DEFAULT_POS, VideoSettings.fullScreen.ToString()));
            menuItems.Add(new MenuItem("Apply", -1600, DEFAULT_POS));
            menuItems.Add(new MenuItem("Back", -2100, DEFAULT_POS));            
        }

        public override bool ExecuteCommand(string c, string[] args, Console console)
        {
            switch (c)
            {
                case "help":
                    console.AddLine("==OPTION SCREEN COMMANDS==");
                    console.AddLine("special - display a text.");
                    break;

                case "special":
                    console.AddLine("This is a special per-screen command.");
                    break;

                default:
                    return false;
            }
            return true;
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

            if (Input.KeyPressed(Keys.Left))
            {
                switch (selected)
                {
                    case 0:                        
                        string[] parts = ((VariableMenuItem)menuItems[0]).val.Split(new char[] { 'x' });
                        int[] current = new int[2] { int.Parse(parts[0]), int.Parse(parts[1]) };
                        int i = 0;
                        foreach (int[] res in VideoSettings.resolutions) { if (res[0] == current[0] && res[1] == current[1]) { break; } i++; }
                        int[] newRes = VideoSettings.resolutions[Math.Max(i-1, 0)];
                        ((VariableMenuItem)menuItems[0]).val = newRes[0].ToString() + "x" + newRes[1].ToString();
                        break;

                    case 1:
                        ((VariableMenuItem)menuItems[1]).val = (!bool.Parse(((VariableMenuItem)menuItems[1]).val)).ToString();
                        break;

                }
            }

            if (Input.KeyPressed(Keys.Right))
            {
                switch(selected)
                {
                    case 0:
                        string[] parts = ((VariableMenuItem)menuItems[0]).val.Split(new char[] { 'x' });
                        int[] current = new int[2] { int.Parse(parts[0]), int.Parse(parts[1]) };
                        int i = 0;
                        foreach (int[] res in VideoSettings.resolutions) { if (res[0] == current[0] && res[1] == current[1]) { break; } i++; }
                        int[] newRes = VideoSettings.resolutions[Math.Min(i+1, VideoSettings.resolutions.Count - 1)];
                        ((VariableMenuItem)menuItems[0]).val = newRes[0].ToString() + "x" + newRes[1].ToString();
                        break;

                    case 1:
                        ((VariableMenuItem)menuItems[1]).val = (!bool.Parse(((VariableMenuItem)menuItems[1]).val)).ToString();
                        break;
                }
            }

            if (Input.KeyPressed(Keys.Enter))
            {
                switch (selected)
                {
                    case 2:
                        string[] res = ((VariableMenuItem)menuItems[0]).val.Split(new char[] { 'x' });
                        VideoSettings.resolution.X = int.Parse(res[0]);
                        VideoSettings.resolution.Y = int.Parse(res[1]);
                        VideoSettings.fullScreen = bool.Parse(((VariableMenuItem)menuItems[1]).val);
                        VideoSettings.ApplyChanges();
                        VideoSettings.SaveVideoSettings();
                        break;
                    case 3:
                        engine.ChangeScreen(new MenuScreen(engine), new Fade(0.1f));
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
            spriteBatch.DrawString(Resources.GetFont("TitleFont"), "Atomic - Rapid Game Prototyping", new Vector2(300, 10), Color.Black);

            float y = 200;
            foreach(MenuItem item in menuItems)
            {
                spriteBatch.DrawString(Resources.GetFont("MenuFont"), item.GetText(), new Vector2(item.x, y), Color.White);
                y += 50;
            }

            DrawHelp.DrawCircle(spriteBatch, Input.mouse, 5, Color.Black, 8);            
        }
    }
}
