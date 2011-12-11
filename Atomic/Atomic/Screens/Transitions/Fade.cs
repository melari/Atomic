using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    class Fade : Transition
    {
        float alpha = 0;
        float animate_speed = 0.05f;
        bool skipOut = false;

        public Fade(float animate_speed, bool skipOut = false)
        {
            this.animate_speed = animate_speed;
            this.skipOut = skipOut;
        }

        public override bool OUT_Update()
        {
            if (skipOut)
            {
                alpha = 1.0f;
                return true;
            }

            alpha += animate_speed;
            if (alpha >= 1) { return true; }
            return false;
        }

        public override bool IN_Update()
        {
            alpha -= animate_speed;
            if (alpha <= 0) { return true; }
            return false;
        }

        public override void OUT_Draw(SpriteBatch spriteBatch)
        {
            DrawHelp.DrawRectangle(spriteBatch, 0, 0, VideoSettings.resolution.X, VideoSettings.resolution.Y, new Color(0, 0, 0, alpha));
        }
        public override void IN_Draw(SpriteBatch spriteBatch)
        {
            OUT_Draw(spriteBatch);
        }
    }
}
