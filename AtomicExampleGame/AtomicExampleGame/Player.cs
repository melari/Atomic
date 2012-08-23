using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AtomicExampleGame
{
    public class Player : GameObject
    {
        public Player(Atom a, Vector2 position)
            : base(a, position, new Vector2(16f, 16f))
        {
        }

        public override void Update()
        {
            if (Input.KeyDown(Keys.Left))
                position.X -= 1;
            if (Input.KeyDown(Keys.Right))
                position.X += 1;
            if (Input.KeyDown(Keys.Up))
                position.Y -= 1;
            if (Input.KeyDown(Keys.Down))
                position.Y += 1;

            base.Update();
        }
    }
}
