using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Atomic
{

    /*
     * The Input class takes care of all mouse and keyboard input. There are several
     * static functions which really simplify checking the state of any button.
     * 
     * Pressed - activated on the moment the key goes down.
     * Released - activated on the moment the key goes up.
     * Down - stays active constantly while the key is down, deactivates while key is up.
     */

    static class Input
    {
        private static KeyboardState ks = new KeyboardState();
        private static KeyboardState pks = new KeyboardState();

        private static MouseState ms = new MouseState();
        private static MouseState pms = new MouseState();

        public static Vector2 mouse = Vector2.Zero;

        public static bool locked = false;

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
            return ks.IsKeyDown(key) && pks.IsKeyUp(key) && !locked;
        }

        public static bool KeyReleased(Keys key)
        {
            return ks.IsKeyUp(key) && pks.IsKeyDown(key) && !locked;
        }

        public static bool KeyDown(Keys key)
        {
            return ks.IsKeyDown(key) && !locked;
        }
        public static bool OverrideKeyDown(Keys key)
        {
            return ks.IsKeyDown(key);
        }

        public static string GetTyped()
        {
            foreach (Keys key in ks.GetPressedKeys())
            {
                if (pks.IsKeyUp(key))
                {
                    return key.ToString();
                }
            }

            return "";
        }


        public static bool MLBPressed() { return ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released; }
        public static bool MLBReleased() { return ms.LeftButton == ButtonState.Released && pms.LeftButton == ButtonState.Pressed; }
        public static bool MLBDown() { return ms.LeftButton == ButtonState.Pressed; }

        public static bool MRBPressed() { return ms.RightButton == ButtonState.Pressed && pms.RightButton == ButtonState.Released; }
        public static bool MRBReleased() { return ms.RightButton == ButtonState.Released && pms.RightButton == ButtonState.Pressed; }
        public static bool MRBDown() { return ms.RightButton == ButtonState.Pressed; }        
    }
}
