using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public class ConsoleState : State
    {
        string[] lines = new string[20];
        Color[] colors = new Color[20];
        int line_i = 0;

        string current = "";
        int cursorIndex = 0;

        public delegate void Command(ConsoleState cl, string[] args);

        List<string> history = new List<string>();
        int history_i = 0;

        Dictionary<string, string> vars = new Dictionary<string, string>();
        Dictionary<string, Command> commands = new Dictionary<string, Command>();
        Dictionary<IConsoleCommandable, List<string>> commandOwners = new Dictionary<IConsoleCommandable, List<string>>();
        Dictionary<string, string> helps = new Dictionary<string, string>();
        int maxCommandNameLength = 0;

        IConsoleCommandable newCommandsState;

        string sleepCom = "";
        int sleepCount = 0;

        FPSCounterState fpsCount;

        public ConsoleState(Atom a, int layer)
            : base(a, layer)
        {
            for (int i = 0; i < 20; i++) 
            { 
                lines[i] = "";
                colors[i] = Color.Green;
            }
            AddLine("-----ATOMIC CONSOLE-----");
            AddLine("Type 'help' for a list of available commands.");
            AddLine("Press '~' or type 'close' to close console.");
            AddLine("");

            SetVariable("true", "1");
            SetVariable("True", "1");
            SetVariable("false", "0");
            SetVariable("False", "0");

            AddCommand("help", delegate(ConsoleState cl, string[] args)
            { 
                DisplayHelp();
            },
            "Display this help file."
            );

            AddCommand("close", delegate(ConsoleState cl, string[] args)
            {
                a.stateManager.DropFocus();
            },
            "Closes the console window."
            );

            AddCommand("exit", delegate(ConsoleState cl, string[] args)
            {
                a.Exit();
            },
            "Forces an exit of the game."
            );

            AddCommand("echo", delegate(ConsoleState cl, string[] args)
            {
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
            },
            "Prints the value of arg0 to the console window."
            );

            AddCommand("resolution", delegate(ConsoleState cl, string[] args)
            {
                int x, y;
                if (int.TryParse(args[0], out x) && int.TryParse(args[1], out y))
                {
                    a.ApplyResolution(x, y, a.fullscreen);
                }
                else
                {
                    AddLine("Dimensions must be integer values.");
                }
            },
            "Sets the resolution of the window to (arg0, arg1)"
            );

            AddCommand("fullscreen", delegate(ConsoleState cl, string[] args)
            {
                if (args[0] == "0" || args[0] == "1")
                {
                    a.ApplyResolution((int)a.resolution.X, (int)a.resolution.Y, args[0] == "1");
                }
                else
                {
                    AddLine("Must specify a boolean value");
                }
            },
            "Sets the window to fullscreen if arg0 is true, or a window if false."
            );

            AddCommand("clear", delegate(ConsoleState cl, string[] args)
            {
                if (args[0] == "#help" || args[0] == null)
                {
                    AddLine("Usage: clear <option>");
                    AddLine("Options: #help    Show this help text.");
                    AddLine("         #history   Clear the command history.");
                    AddLine("         #output    Clear the console output window.");
                }
                else if (args[0] == "#history")
                {
                    history.Clear();
                    history_i = 0;
                }
                else if (args[0] == "#output")
                {
                    for (int i = 0; i < 20; i++)
                    {
                        lines[i] = "";
                        colors[i] = Color.Green;
                    }
                }
            },
            "Clears a variety of things. Use clear #help for more info."
            );

            AddCommand("sleep", delegate(ConsoleState cl, string[] args)
            {
                if (Int32.TryParse(args[0], out sleepCount))
                {
                    sleepCom = "true";
                }
                else
                {
                    AddLine("Could not sleep. Cycle count must be a valid integer.", Color.Red);
                }

            },
            "Stops execution of the console window for the amount given amount of cycles in arg0."
            );

            AddCommand("fps", delegate(ConsoleState cl, string[] args)
            {
                if (args[0] == "0")
                    a.stateManager.EndState(fpsCount);
                else
                {
                    fpsCount = new FPSCounterState(a, layer + 1);
                    a.stateManager.AddState(fpsCount);
                }
            },
            "Shows or hides the on-screen FPS counter."
            );
        }

        public void AddCommand(string name, Command function, string help = null)
        {
            if (newCommandsState != null)
            {
                name = newCommandsState.GetType().Name + "." + name;

                if (commandOwners.ContainsKey(newCommandsState))
                    commandOwners[newCommandsState].Add(name);
                else
                {
                    commandOwners.Add(newCommandsState, new List<string>() { name });
                }
            }

            commands.Add(name, function);
            if (help != null)
            {
                if (name.Length > maxCommandNameLength)
                    maxCommandNameLength = name.Length;
                helps.Add(name, help.ToString());
            }
        }

        public void RegisterStateCommands(IConsoleCommandable cc)
        {
            newCommandsState = cc;
            cc.RegisterCommands(this);
            newCommandsState = null;
        }

        public void DeRegisterStateCommands(IConsoleCommandable cc)
        {
            foreach (string command in commandOwners[cc])
            {
                commands.Remove(command);
                helps.Remove(command);
            }
            commandOwners.Remove(cc);
        }

        public void AddLine(string text) { AddLine(text, Color.White); }
        public void AddLine(string text, Color color)
        {
            if (text == null)
            {
                AddLine("ERROR: Cannot print a null text.", Color.Red);
                return;
            }
            lines[line_i] = text;
            colors[line_i] = color;
            line_i++;
            if (line_i >= 20) { line_i = 0; }
        }

        private void SetVariable(string name, string value)
        {
            vars[name] = value;
        }

        private string GetVariable(string name)
        {
            if (name == "") { return ""; }
            if (name == "=") { return "="; }

            float test;
            if (float.TryParse(name, out test)) { return name; }

            if (vars.ContainsKey(name)) { return vars[name]; }
            else
            {
                AddLine("Unknown Variable '" + name + "'");
                return null;
            }
        }

        public void Execute(string inText)
        {
            if (sleepCom == "")
                AddLine("> " + inText);

            foreach (string command in inText.Split(';'))
            {
                string comStr = command.Trim();
                if (comStr == "")
                {
                    sleepCom = "";
                    continue;
                }

                string c = comStr;
                string[] args = new string[5] { null, null, null, null, null };
                if (comStr.Contains(' '))
                {
                    int i = comStr.IndexOf(' ');
                    c = comStr.Substring(0, i);
                    string a = comStr.Substring(i + 1);
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
                                if (p.StartsWith("#"))
                                    args[arg_i] += p;
                                else
                                    args[arg_i] += GetVariable(p);
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

                if (commands.ContainsKey(c))
                {
                    commands[c](this, args);
                    if (sleepCom == "true")
                    {
                        if (inText.Contains(';'))
                        {
                            sleepCom = inText.Substring(inText.IndexOf("sleep"));
                            sleepCom = sleepCom.Substring(sleepCom.IndexOf(";") + 1);
                        }
                        else
                            sleepCom = " ";
                        return;
                    }
                    else
                        sleepCom = "";
                }
                else
                {
                    if (args[0] == "=" && args[1] != null)
                    {
                        SetVariable(c, args[1]);
                    }
                    else
                    {
                        AddLine("ERROR: Unknown command '" + c + "'");
                    }
                }
            }
        }

        private void DisplayHelp()
        {
            AddLine("-----Available Commands-----");
            AddLine("");

            foreach (string command in helps.Keys)
            {
                string line = command;
                while (line.Length < maxCommandNameLength)
                    line += " ";
                AddLine(line + " - " + helps[command]);
            }
        }

        public override void Update()
        {
            if (sleepCom != "")
            {
                if (sleepCount-- <= 0)
                {
                    Execute(sleepCom);
                }
                return;
            }

            string l = Input.GetTyped();
            if (l == "~")
            {
                return;
            }
            else if (l == "\n")
            {
                history.Add(current);
                history_i = history.Count;
                Execute(current);
                current = "";
                cursorIndex = 0;
            }
            else if (l == Convert.ToChar(0x8).ToString())
            {
                if (cursorIndex > 0) 
                {
                    current = current.Remove(cursorIndex-1, 1);
                    cursorIndex--;
                }
            }
            else if (l != "")
            {
                if (cursorIndex == current.Length)
                    current += l;
                else
                    current = current.Insert(cursorIndex, l);
                cursorIndex += l.Length;
            }

            if (Input.KeyPressed(Keys.Up) && history_i > 0)
            {
                current = history[--history_i];
                cursorIndex = current.Length;
            }
            if (Input.KeyPressed(Keys.Down) && history_i < history.Count)
            {
                current = ++history_i == history.Count ? "" : history[history_i];
                cursorIndex = current.Length;
            }
            if (Input.KeyPressed(Keys.Left) && cursorIndex > 0)
                cursorIndex--;
            if (Input.KeyPressed(Keys.Right) && cursorIndex < current.Length)
                cursorIndex++;
            if (Input.KeyPressed(Keys.Home))
                cursorIndex = 0;
            if (Input.KeyPressed(Keys.End))
                cursorIndex = current.Length;

            if (Input.KeyReleased(Keys.OemTilde))
                a.stateManager.DropFocus();
        }

        public override void BackgroundUpdate()
        {
            if (Input.KeyReleased(Keys.OemTilde))
            {
                a.stateManager.BringToFront(this);
            }

            if (sleepCom != "")
                Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Begin();

            DrawHelp.DrawRectangle(spriteBatch, Vector2.Zero, a.resolution, Color.Black * 0.8f);

            float y = 10f;
            for (int i = line_i; i < 20; i++)
            {
                y += 20;
                DrawLine(spriteBatch, i, y);
            }
            for (int i = 0; i < line_i; i++)
            {
                y += 20;
                DrawLine(spriteBatch, i, y);
            }

            if (sleepCom == "")
            {
                string line = "> " + current.Substring(0, cursorIndex) + "|" + current.Substring(cursorIndex);
                spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), line, new Vector2(10, y + 20), Color.Green);
            }
            spriteBatch.End();
        }

        private void DrawLine(SpriteBatch spriteBatch, int i, float y)
        {
            spriteBatch.DrawString(Resources.GetFont("ConsoleFont"), lines[i], new Vector2(10, y), colors[i]);
        }
    }
}
