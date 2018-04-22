using System;
using System.Net;
using System.Net.Sockets;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using GameData.Network;
using GameData.Network.Messages;
using Unity.Attributes;

namespace CollectibleCardGame.Network.Controllers
{
    public class NetworkConnectionController : INetworkController
    {
        private INetworkCommunicator _serverCommunicator;

        [Dependency]
        public INetworkCommunicator ServerCommunicator
        {
            set
            {
                if (_serverCommunicator != null)
                {
                    _serverCommunicator.MessageRecievedEvent -= OnMessageRecieved;
                    _serverCommunicator.BreakConnectionEvent -= OnBreakConnection;
                    _serverCommunicator.Disconnect();
                }

                _serverCommunicator = value;
                _serverCommunicator.MessageRecievedEvent += OnMessageRecieved;
                _serverCommunicator.BreakConnectionEvent += OnBreakConnection;
            }
            get => _serverCommunicator;
        }

        private readonly NetworkMessageConverter _converter;

        public NetworkConnectionController(NetworkMessageConverter converter)
        {
            _converter = converter;
        }

        public void Connect(IPAddress ipAddress, int port)
        {
            if(!ServerCommunicator.Connect(ipAddress, port))
                throw new SocketException();

            ServerCommunicator.StartReadMessages();
        }

        public void Disconnect()
        {
            if(!ServerCommunicator.Disconnect())
                throw new SocketException();
        }

        public void SendMessage(MessageBase message)
        {
            ServerCommunicator.SendMessage(_converter.SerializeMessage(message));
        }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            var deserializedMessage = _converter.DeserializeMessage(e.NetworkMessage);

            deserializedMessage?.HandleMessage(sender);
        }

        public void OnBreakConnection(object sender, BreakConnectionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
