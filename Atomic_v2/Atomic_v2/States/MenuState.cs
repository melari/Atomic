using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public delegate void MenuAction(MenuState menu);

    public abstract class MenuItem
    {
        public MenuAction action;
        protected string text;

        public MenuItem(string text, MenuAction action)
        {
            this.text = text;
            this.action = action;
        }

        public abstract void Update(bool selected);
        public virtual string GetText()
        {
            return text;
        }
    }

    public class SlidingMenuItem : MenuItem
    {
        public float x;
        public float dest_x;

        public float moveSpeed, unselectedPosition, selectedPosition;

        public SlidingMenuItem(string text, float x, MenuAction action)
            : this(text, x, action, 5, 30, 100) { }
        public SlidingMenuItem(string text, float x, MenuAction action, float moveSpeed, float unselectedPosition, float selectedPosition)
            : base(text, action)
        {
            this.text = text;
            this.x = x;
            this.dest_x = unselectedPosition;
            this.action = action;

            this.moveSpeed = moveSpeed;
            this.unselectedPosition = unselectedPosition;
            this.selectedPosition = selectedPosition;
        }

        public override void Update(bool selected)
        {
            dest_x = selected ? selectedPosition : unselectedPosition;
            x += (dest_x - x) / moveSpeed;
        }
    }

    public abstract class MenuState : State
    {
        int offset = -100;

        List<MenuItem> menuItems = new List<MenuItem>();
        int selected = 0;

        string title = "";

        public MenuState(Atom a, int layer, string title)
            : base(a, layer)
        {
            this.title = title;
        }

        public void AddSlidingMenuItem(string name, MenuAction action)
        {
            AddMenuItem(new SlidingMenuItem(name, offset, action));
            offset -= 500;
        }
        public void AddSlidingMenuItem(string name, MenuAction action, float unselectedPosition)
        {
            AddMenuItem(new SlidingMenuItem(name, offset, action, 5, unselectedPosition, unselectedPosition+70));
            offset -= 500;
        }

        public void AddMenuItem(MenuItem item)
        {
            menuItems.Add(item);
        }

        private void UpdateMenuItems()
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].Update(i == selected);
            }
        }

        public override void BackgroundUpdate()
        {
            UpdateMenuItems();
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
                menuItems[selected].action(this);
            }

            UpdateMenuItems();
        }

        public override void BackgroundDraw(SpriteBatch spriteBatch) { Draw(spriteBatch); }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Resources.GetFont("TitleFont"), title, new Vector2(300, 10), Color.Black);

            float y = 200;
            foreach (SlidingMenuItem item in menuItems)
            {
                spriteBatch.DrawString(Resources.GetFont("MenuFont"), item.GetText(), new Vector2(item.x, y), Color.White);
                y += 50;
            }
            DrawHelp.DrawCircle(spriteBatch, Input.mouse, 5, Color.Black, 8);
            spriteBatch.End();
        }
    }
}
