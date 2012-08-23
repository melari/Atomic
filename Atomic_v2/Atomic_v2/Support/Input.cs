using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public static class Input
    {
        private static KeyboardState ks = new KeyboardState();
        private static KeyboardState pks = new KeyboardState();

        private static MouseState ms = new MouseState();
        private static MouseState pms = new MouseState();

        public static Vector2 mouse = Vector2.Zero;

        public static void Update()
        {
            pks = ks;
            ks = Keyboard.GetState();

            pms = ms;
            ms = Mouse.GetState();

            mouse.X = ms.X;
            mouse.Y = ms.Y;
        }

        public static bool KeyPressed(Keys key)
        {
            return ks.IsKeyDown(key) && pks.IsKeyUp(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return ks.IsKeyUp(key) && pks.IsKeyDown(key);
        }

        public static bool KeyDown(Keys key)
        {
            return ks.IsKeyDown(key);
        }

        public static string GetTyped()
        {
            string current = "";

            bool shift = Input.KeyDown(Keys.LeftShift) || Input.KeyDown(Keys.RightShift);

            foreach (Keys key in ks.GetPressedKeys())
            {
                if (pks.IsKeyUp(key))
                {
                    string l = key.ToString();
                    switch (l)
                    {
                        case "Enter":
                            current += "\n";
                            break;

                        case "OemTilde":
                            current += "~";
                            break;

                        case "OemMinus":
                            if (shift) { current += "_"; }
                            else { current += "-"; }
                            break;

                        case "OemPlus":
                            if (shift) { current += "+"; }
                            else { current += "="; }
                            break;

                        case "Back":
                            current += Convert.ToChar(0x8);
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
                            if (l.Length > 1)
                            {
                                l = l.Substring(1);
                                int test;
                                if (!int.TryParse(l, out test)) { break; }

                                if (shift)
                                {
                                    switch (l)
                                    {
                                        case "1":
                                            current += "!";
                                            continue;
                                        case "2":
                                            current += "@";
                                            continue;
                                        case "3":
                                            current += "#";
                                            continue;
                                        case "4":
                                            current += "$";
                                            continue;
                                        case "5":
                                            current += "%";
                                            continue;
                                        case "6":
                                            current += "^";
                                            continue;
                                        case "7":
                                            current += "&";
                                            continue;
                                        case "8":
                                            current += "*";
                                            continue;
                                        case "9":
                                            current += "(";
                                            continue;
                                        case "0":
                                            current += ")";
                                            continue;
                                    }
                                }
                            }
                            if (!shift) { l = l.ToLower(); }
                            current += l;
                            break;
                    }
                }
            }

            return current;
        }


        public static bool MLBPressed() { return ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released; }
        public static bool MLBReleased() { return ms.LeftButton == ButtonState.Released && pms.LeftButton == ButtonState.Pressed; }
        public static bool MLBDown() { return ms.LeftButton == ButtonState.Pressed; }

        public static bool MRBPressed() { return ms.RightButton == ButtonState.Pressed && pms.RightButton == ButtonState.Released; }
        public static bool MRBReleased() { return ms.RightButton == ButtonState.Released && pms.RightButton == ButtonState.Pressed; }
        public static bool MRBDown() { return ms.RightButton == ButtonState.Pressed; }
    }
}
