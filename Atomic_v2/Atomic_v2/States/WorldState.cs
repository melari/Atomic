using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atomic
{
    public abstract class WorldState : State
    {
        protected BufferedList<GameObject> objects = new BufferedList<GameObject>();

        public WorldState(Atom a, int layer) : base(a, layer) { }

        public override void Update()
        {
            foreach(GameObject o in objects)
                o.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (GameObject o in objects)
                o.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
