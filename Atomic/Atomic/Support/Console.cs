using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{

    /*
     * The in-game console provides an interface for simplified, yet powerful command execution. Usually, the console can
     * be very beneficial in decreasing production time of a game by allowing developers to more quickly and easily test
     * certain parts of the game. It also allows for debug information to be more easily output to the screen in an easy
     * to read format.
     * 
     * The console can be brought up at any time using the ~ key. When a command is entered, it is first seperated into
     * the opcode and operands. Commands can be defined in the Execute() function. If the command is not found, it is passed
     * on to the current active screen to check if its a screen dependant command.
     */

    public class Console
    {
        Engine engine;
        string[] lines = new string[20];
        int line_i = 0;
        string current = "";

        bool enabled = false;

        Dictionary<string, string> vars = new Dictionary<string, string>();

        public Console(Engine engine)
        {
            this.engine = engine;

            for (int i = 0; i < 20; i++) { lines[i] = ""; }
            AddLine("-----ATOMIC CONSOLE-----");
            AddLine("Type 'help' for a list of available commands.");            
            AddLine("Press '~' or type 'close' to close console.");
            AddLine("");
        }

        public void AddLine(string text)
        {
            if (text == null)
            {
                AddLine("ERROR: Cannot print a null text.");
                return;
            }
            lines[line_i] = text;
            line_i++;
            if (line_i >= 20) { line_i = 0; }
        }

        private string GetValue(string arg)
        {
            if (arg == "") { return ""; }
            if (arg == "=") { return "="; }

            float test;
            if (float.TryParse(arg, out test)) { return arg; }

            if (vars.ContainsKey(arg)) { return vars[arg]; }
            else
            {
                AddLine("Unknown Variable '" + arg + "'");
                return null;
            }
        }

        public void Execute(string inText)
        {
            AddLine("> " + inText);

            string c = inText;
            string[] args = new string[5] { null, null, null, null, null };
            if (inText.Contains(' '))
            {
                int i = inText.IndexOf(' ');
                c = inText.Substring(0, i);
                string a = inText.Substring(i + 1);
                string[] a2 = a.Split(new char[] { '"' });
                bool inq = false;

                int arg_i = 0;
                foreach (string q in a2)
                {
                    if (!inq)
                    {
                        string[] parts = q.Split(new char[] { ' ' });
                        bool first = true;
                        foreach (string p in parts)
                        {
                            if (!first) { arg_i++; }
                            args[arg_i] += GetValue(p);
                            first = false;
                        }
                    }
                    else
                    {
                        args[arg_i] += q;
                    }
                    inq = !inq;
                }                
            }            

            switch (c)
            {
                case "close":
                    Input.locked = false;
                    enabled = false;
                    break;

                case "help":
                    DisplayHelp();
                    engine.GetCurrentScreen().ExecuteCommand("help", args, this);
                    break;

                case "exit":
                    engine.Exit();
                    break;

                case "echo":
                    int repeat = 1;
                    if (args[1] != null)
                    {
                        if (!int.TryParse(args[1], out repeat))
                        {
                            AddLine("'" + args[1] + "' is not a valid integer value.");
                        }
                    }
                    for (int i = 0; i < repeat; i++)
                    {
                        AddLine(args[0]);
                    }
                    break;
                
                case "fullscreen":
                    if (args[0] == "1")
                    {
                        VideoSettings.fullScreen = true;
                        VideoSettings.ApplyChanges();
                    }
                    else
                    {
                        VideoSettings.fullScreen = false;
                        VideoSettings.ApplyChanges();
                    }
                    break;

                case "resolution":
                    int x, y;
                    if (int.TryParse(args[0], out x) && int.TryParse(args[1], out y))
                    {
                        VideoSettings.resolution = new Point(x, y);
                        VideoSettings.ApplyChanges();
                    }
                    else
                    {
                        AddLine("Dimensions must be integer values.");
                    }
                    break;

                default:
                    if (args[0] == "=" && args[1] != null)
                    {
                        vars[c] = args[1];
                    }
                    else
                    {
                        if (!engine.GetCurrentScreen().ExecuteCommand(c, args, this))
                        {
                            AddLine("ERROR: Unknown command '" + c + "'");
                        }
                    }
                    break;
            }
        }

        private void DisplayHelp()
        {
            AddLine("-----ATOMIC CONSOLE HELP-----");
            AddLine("");
            AddLine("help                        - Show this help file.");
            AddLine("close                       - Close console Window.");
            AddLine("exit                        - Force quit game.");
            AddLine("echo <text> <[count=1]>     - Writes <text> to the console window <count> number of times.");
            AddLine("<name> = <value>            - Assigns <value> to variable <name>");
            AddLine("fullscreen <value>          - <value> to 0 sets windowed mode. 1 sets fullscreen.");
            AddLine("resolution <width> <height> - Sets resolution to given dimensions.");
        }

        public void Update()
        {            
            if (enabled)
            {
                Input.locked = true;

                bool shift = Input.OverrideKeyDown(Keys.LeftShift) || Input.OverrideKeyDown(Keys.RightShift);

                string l = Input.GetTyped();
                switch (l)
                {
                    case "Enter":
                        Execute(current);
                        current = "";
                        break;

                    case "OemTilde":
                        Input.locked = false;
                        enabled = false;
                        return;

                    case "OemMinus":
                        if (shift) { current += "_"; }
                        else { current += "-"; }
                        break;

                    case "OemPlus":
                        if (shift) { current += "+"; }
                        else { current += "="; }
                        break;

                    case "Back":
                        if (current.Length > 0) { current = current.Substring(0, current.Length - 1); }
                        break;

                    case "Space":
                        current += " ";
                        break;                  

                    case "OemQuotes":
                        if (shift) { current += "\""; }
                        else { current += "'"; }
                        break;

                    case "OemQuestion":
                        if (shift) { current += "?"; }
                        else { current += "/"; }
                        break;

                    case "OemSemicolon":
                        if (shift) { current += ":"; }
                        else { current += ";"; }
                        break;

                    case "OemPeriod":
                        if (shift) { current += ">"; }
                        else { current += "."; }
                        break;

                    case "OemComma":
                        if (shift) { current += "<"; }
                        else { current += ","; }
                        break;

                    default:
                        if (Input.OverrideKeyDown(Keys.Escape)) { current += l; }
                        if (l.Length > 1)
                        {
                            l = l.Substring(1);
                            int test;
                            if (!int.TryParse(l, out test)) { break; }

                            if (shift)
                            {
                                switch (l)
                                {
                                    case "8":
                                        current += "*";
                                        return;
                                    case "9":
                                        current += "(";
                                        return;
                                    case "0":
                                        current += ")";
                                        return;                                    
                                }
                            }                        
                        }
                        if (!shift) { l = l.ToLower(); }
                        current += l;
                        break;
                }
            }

            if (Input.KeyPressed(Keys.OemTilde)) { enabled = true; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enabled)
            {
                spriteBatch.Begin();
                DrawHelp.DrawRectangle(spriteBatch, 0, 0, VideoSettings.resolution.X, VideoSettings.resolution.Y, new Color(0f, 0f, 0f, 0.85f));

                float y = 10f;
                for (int i = line_i; i < 20; i++)
                {
                    y+=20;
                    DrawLine(spriteBatch, i, y);
                }
                for (int i = 0; i < line_i; i++)
                {
                    y+=20;
                    DrawLine(spriteBatch, i, y);
                }

                spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), "> " + current + "|", new Vector2(10, y + 20), Color.Green);
                spriteBatch.End();
            }            
        }

        private void DrawLine(SpriteBatch spriteBatch, int i, float y)
        {            
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), lines[i], new Vector2(10, y), Color.White);            
        }
    }
}
