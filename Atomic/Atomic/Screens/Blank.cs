using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    class Blank : Screen
    {
        public Blank(Engine engine)
            : base(engine)
        {
        }

        public override bool ExecuteCommand(string c, string[] args, Console console)
        {
            return false;
        }

        public override void Update()
        {            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
        }
    }
}
