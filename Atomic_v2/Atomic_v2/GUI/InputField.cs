using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Atomic
{
    public class InputField
    {
        TextInput content = new TextInput();
        public bool focused;
        public bool enabled = true;
        public InputField tab = null;

        public Vector2 position;
        public Vector2 size;

        bool maxLength = false;

        public InputField(Vector2 position, Vector2 size, bool password = false)
        {
            this.position = position;
            this.size = size;
            this.content.password = password;
        }

        public void Update()
        {
            if (focused)
            {
                int max = (int)(size.X / 9) - 1;
                content.Update(Input.GetTyped());

                if (content.content.Length > max) { content.SetContent(content.content.Substring(0, max)); }
                maxLength = content.content.Length == max;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawHelp.DrawRectangle(spriteBatch, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y, Color.LightGray);
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), focused ? content.GetContentWithPipe() : content.GetContent(), position + Vector2.UnitX * 5, Color.Black);
        }
    }
}
