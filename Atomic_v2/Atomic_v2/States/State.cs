using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public abstract class State : IComparable
    {
        protected Atom a;
        public bool enabled { get; protected set; }
        public int layer = 0;

        protected State(Atom a, int layer)
        {
            this.a = a;
            this.layer = layer;
            this.enabled = true;
        }

        public int CompareTo(object obj)
        {
            if (obj is State)
                return layer - ((State)obj).layer;
            else
                return 0;
        }

        /// <summary>
        /// Called when this State gains focus.
        /// </summary>
        public virtual void OnFocus() { }

        /// <summary>
        /// Called when this State looses focus.
        /// </summary>
        public virtual void OnBlur() { }

        /// <summary>
        /// Called as a replacement to the standard Update call when this State does not have focus.
        /// </summary>
        public virtual void BackgroundUpdate() { }

        /// <summary>
        /// Called as a replacement to the standard Draw call when this State does not have focus.
        /// </summary>
        public virtual void BackgroundDraw(SpriteBatch spriteBatch) { }

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void Update(GameTime gameTime) { Update(); }
        public virtual void BackgroundUpdate(GameTime gameTime) { BackgroundUpdate(); }
    }
}
