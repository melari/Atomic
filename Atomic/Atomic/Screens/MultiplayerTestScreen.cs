using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Lidgren.Network;
using Microsoft.Xna.Framework.Input;

namespace Atomic
{
    class MultiplayerTestScreen : MPScreen
    {
        ConnectionController cc;

        BufferedList<Dot> dots = new BufferedList<Dot>();

        public MultiplayerTestScreen(Engine engine)
            : base(engine, Color.White)
        {
            cc = new ConnectionController(engine, this);
            cc.Initialize();
        }

        public override bool ExecuteCommand(string c, string[] args, Console console)
        {
            switch (c)
            {
                case "disconnect":
                    cc.Shutdown();
                    engine.ChangeScreen(new MenuScreen(engine), new Fade(0.1f));
                    return true;

                case "help":
                    console.AddLine("===== Network Commands ======");
                    console.AddLine("disconnect - disconnect from server and return to main menu.");
                    return true;
            }
            return false;
        }

        public override void Update()
        {
            cc.Update();
        }

        public override MPlayer NewPlayer(long who)
        {
            engine.console.AddLine("Player ID" + who + " connected.");
            Dot dot = new Dot(who, this);
            dots.Add(dot);
            return dot; 
        }

        public override void SendInput(NetClient client)
        {
            sbyte xinput = 0;
            sbyte yinput = 0;

            if (Input.KeyDown(Keys.Left)) { xinput = -1; }
            if (Input.KeyDown(Keys.Right)) { xinput = 1; }
            if (Input.KeyDown(Keys.Up)) { yinput = -1; }
            if (Input.KeyDown(Keys.Down)) { yinput = 1; }

            if (xinput != 0 || yinput != 0)
            {
                NetOutgoingMessage om = client.CreateMessage();
                om.Write(xinput);
                om.Write(yinput);
                client.SendMessage(om, NetDeliveryMethod.Unreliable);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Dot dot in dots)
            {
                dot.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
