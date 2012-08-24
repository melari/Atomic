using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atomic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AtomicExampleGame
{
    public class Player : PolygonCol
    {
        public Player(Atom a, Vector2 position)
            : base(a, position, position + new Vector2(32, 0))
        {
            AddPoint(position + new Vector2(32, 32));
            Close();
        }

        public void Update(List<PolygonCol> objs)
        {
            if (Input.KeyDown(Keys.Left))
                ApplyForce(new Vector2(-0.5f, 0));
            if (Input.KeyDown(Keys.Right))
                ApplyForce(new Vector2(0.5f, 0));
            if (Input.KeyDown(Keys.Up))
                ApplyForce(new Vector2(0, -0.5f));
            if (Input.KeyDown(Keys.Down))
                ApplyForce(new Vector2(0, 0.5f));

            ApplyFriction(0.1f);


            base.Update(objs);
        }
    }
}
