using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public class GuiManager : List<InputField>
    {
        public InputField focus { get; private set; }

        public void Update()
        {
            foreach (InputField field in this)
            {
                if (field.enabled && Input.MLBPressed() && Input.mouse.X >= field.position.X && Input.mouse.Y >= field.position.Y && Input.mouse.X <= (field.position + field.size).X && Input.mouse.Y <= (field.position + field.size).Y)
                {
                    if (focus != null)
                        focus.focused = false;
                    field.focused = true;
                    focus = field;
                }
                field.Update();
            }

            if (Input.KeyPressed(Keys.Tab) && focus != null && focus.tab != null)
            {
                focus.focused = false;
                focus.tab.focused = true;
                focus = focus.tab;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (InputField field in this)
                field.Draw(spriteBatch);
        }
    }
}
