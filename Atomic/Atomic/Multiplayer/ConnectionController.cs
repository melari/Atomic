using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using System.Net;
using Microsoft.Xna.Framework;

namespace Atomic
{
    class ConnectionController
    {
        Engine engine;
        MPScreen screen;

        NetClient client;
        public Dictionary<long, MPlayer> players { get; private set; }

        public ConnectionController(Engine engine, MPScreen screen)
        {
            this.engine = engine;
            this.screen = screen;

            players = new Dictionary<long, MPlayer>();

            NetPeerConfiguration config = new NetPeerConfiguration("Atomic"); //This should match the identifier in the server.
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);

            client = new NetClient(config);
            client.Start();
        }

        public void Initialize()
        {
            client.DiscoverKnownPeer("localhost", 14243);
            //client.DiscoverLocalPeers(14243);
        }        

        public void Shutdown()
        {
            client.Shutdown("disconnected by user.");
        }        

        public void Update()
        {
            ((MPScreen)engine.GetCurrentScreen()).SendInput(client);

            NetIncomingMessage msg;
            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        client.Connect(msg.SenderEndpoint);
                        break;

                    case NetIncomingMessageType.Data:
                        long who = msg.ReadInt64();

                        if (!players.ContainsKey(who)) 
                        { 
                            players.Add(who, screen.NewPlayer(who));
                        }
                        players[who].MPUpdate(msg);                        
                        break;
                }
            }
        }
    }
}
