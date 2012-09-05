using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Atomic
{
    public class TextInput
    {
        public string content = "";
        public int cursorIndex;
        public bool multiLine;
        public bool password = false;

        public TextInput()
            : this(false) { }
        public TextInput(bool multiLine)
        {
            this.multiLine = multiLine;
        }

        public void Update(string input)
        {
            if (input == "\n" && !multiLine)
                return;

            if (input == Convert.ToChar(0x8).ToString())
            {
                if (cursorIndex > 0)
                {
                    content = content.Remove(cursorIndex - 1, 1);
                    cursorIndex--;
                }
            }
            else if (input != "")
            {
                if (cursorIndex == content.Length)
                    content += input;
                else
                    content = content.Insert(cursorIndex, input);
                cursorIndex += input.Length;
            }

            if (Input.KeyPressed(Keys.Left) && cursorIndex > 0)
                cursorIndex--;
            if (Input.KeyPressed(Keys.Right) && cursorIndex < content.Length)
                cursorIndex++;
            if (Input.KeyPressed(Keys.Home))
                cursorIndex = 0;
            if (Input.KeyPressed(Keys.End))
                cursorIndex = content.Length;
        }

        public string GetContentWithPipe()
        {
            string first = content.Substring(0, cursorIndex);
            string second = content.Substring(cursorIndex);

            if (password)
            {
                string val = "";
                for (int i = 0; i < first.Length; i++)
                    val += "*";
                val += "|";
                for (int i = 0; i < second.Length; i++)
                    val += "*";
                return val;
            }
            else
                return first + "|" + second;
        }

        public string GetContent()
        {
            if (password)
            {
                string val = "";
                for (int i = 0; i < content.Length; i++)
                    val += "*";
                return val;
            }
            else
                return content;
        }

        public void Clear()
        {
            content = "";
            cursorIndex = 0;
        }

        public void SetContent(string content)
        {
            this.content = content;
            cursorIndex = content.Length;
        }
    }
}
