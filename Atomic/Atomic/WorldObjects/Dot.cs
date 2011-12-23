using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    class Dot : MPlayer
    {
        public Dot(long who, Screen screen)
            : base(who, screen)
        {            
        }

        public override void MPUpdate(NetIncomingMessage msg)
        {
            position.X = msg.ReadInt32();
            position.Y = msg.ReadInt32();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawHelp.DrawRectangle(spriteBatch, (int)position.X, (int)position.Y, 2, 2, Color.Black);
        }
    }
}
