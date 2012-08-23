using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public delegate void TransitionAction();

    public class FadeTransition : State
    {
        float animationSpeed;
        bool skipFadeOut;
        TransitionAction midAction, endAction;

        int stage = 1;
        float alpha = 0;

        public FadeTransition(Atom a, int layer, State nextState)
            : this(a, layer, 0.05f, false, nextState) { }
        public FadeTransition(Atom a, int layer, float animationSpeed, bool skipFadeOut, State nextState)
            : base(a, layer)
        {
            this.animationSpeed = animationSpeed;
            this.skipFadeOut = skipFadeOut;
            this.midAction = delegate() { a.stateManager.AddState(nextState); };
            this.endAction = delegate() { a.stateManager.AddFocus(nextState); };
        }
        public FadeTransition(Atom a, int layer, float animationSpeed, bool skipFadeOut, TransitionAction midAction, TransitionAction endAction = null)
            : base(a, layer)
        {
            this.animationSpeed = animationSpeed;
            this.skipFadeOut = skipFadeOut;
            this.midAction = midAction;
            this.endAction = endAction;
        }

        private void NextStage()
        {
            stage = -1;
            alpha = 1f;
            midAction();
        }

        public override void BackgroundUpdate() { Update(); }
        public override void Update()
        {
            if (skipFadeOut)
            {
                skipFadeOut = false;
                NextStage();
            }
            else
            {
                alpha += animationSpeed * stage;
                if (alpha >= 1f)
                    NextStage();
                if (alpha <= 0f)
                {
                    if (endAction != null)
                        endAction();
                    a.stateManager.EndState(this);
                }
            }
        }

        public override void BackgroundDraw(SpriteBatch spriteBatch) { Draw(spriteBatch); }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            DrawHelp.DrawRectangle(spriteBatch, Vector2.Zero, a.resolution, Color.Black * alpha);
            spriteBatch.End();
        }
    }
}
