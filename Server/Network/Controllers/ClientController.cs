using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using GameData.Network;
using GameData.Network.Messages;
using Server.Network.Models;
using Unity.Attributes;

namespace Server.Network.Controllers
{
    public class ClientController
    {
        private readonly IMessageConverter _messageConverter;

        public ClientController(IMessageConverter converter)
        {
            _messageConverter = converter;
        }

        public Client Client { set; get; }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            var deserializedMessage = _messageConverter.DeserializeMessage(e.NetworkMessage);
            var returnMessage = deserializedMessage.HandleMessage(sender);

            if(returnMessage!=null)
                Client.ClientConnection.Communicator.SendMessage(
                    _messageConverter.SerializeMessage(returnMessage));
        }

        public void OnBreakConnection(object sender, BreakConnectionEventArgs e)
        {
            ((Client) sender).MessageRecived -= OnMessageRecieved;
            ((Client) sender).BreakConnection -= OnBreakConnection;
        }

        public void SendMessage(MessageBase message)
        {
            if(message == null)
                throw new NullReferenceException();

            var networkMessage = _messageConverter.SerializeMessage(message);
            Client.ClientConnection.Communicator.SendMessage(networkMessage);
        }
    }
}
