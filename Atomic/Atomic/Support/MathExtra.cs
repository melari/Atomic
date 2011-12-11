using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{

    /*
     * MathExtra provides a few extra functions that are unavailable
     * either through the Math or MathHelper classes.
     */

    static class MathExtra
    {
        public static Random rand = new Random();

        //Returns the cartesion x component of the polar vector (r, theta)
        public static float comp_x(float r, float theta)
        {
            return (float)Math.Cos((double)theta)*r;
        }

        //Returns the cartesion y component of the polar vector (r, theta)
        public static float comp_y(float r, float theta)
        {
            return (float)Math.Sin((double)theta)*r;            
        }
    }
}
