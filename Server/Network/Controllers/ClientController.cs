using System;
using System.Collections.Generic;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using BaseNetworkArchitecture.Server;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers.Repository;
using Server.Network.Models;

namespace Server.Network.Controllers
{
    public class ClientController
    {
        private readonly AwaitingClientsQueueController _awaitingClientsController;
        private readonly ICollection<IClientConnection> _clientConnected;
        private readonly ConnectedClientsRepositoryController _clientsRepositoryController;
        private readonly ILogger _logger;
        private readonly IMessageConverter _messageConverter;

        public ClientController(IMessageConverter converter,
            ConnectedClientsRepositoryController clientsRepositoryController,
            AwaitingClientsQueueController awaitingClientsController,
            IServer server, ILogger logger)
        {
            _messageConverter = converter;
            _clientsRepositoryController = clientsRepositoryController;
            _awaitingClientsController = awaitingClientsController;
            _clientConnected = server.Clients;
            _logger = logger;
        }

        public Client Client { set; get; }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            var deserializedMessage = _messageConverter.DeserializeMessage(e.NetworkMessage);
            var returnMessage = deserializedMessage.HandleMessage(sender);

            if (returnMessage != null)
                Client.ClientConnection.Communicator.SendMessage(
                    _messageConverter.SerializeMessage(returnMessage));
        }

        public void OnBreakConnection(object sender, BreakConnectionEventArgs e)
        {
            if (!(sender is Client client)) return;

            client.MessageRecived -= OnMessageRecieved;
            client.BreakConnection -= OnBreakConnection;

            _logger.LogAndPrint("Client disconnected");

            client.CurrentLobby?.OnClientDisconnect(client);
            _clientsRepositoryController.Remove(client);
            _awaitingClientsController.Remove(client);
            _clientConnected.Remove(client.ClientConnection);
        }

        public void SendMessage(MessageBase message)
        {
            if (message == null)
                throw new NullReferenceException();

            var networkMessage = _messageConverter.SerializeMessage(message);
            Client.ClientConnection.Communicator.SendMessage(networkMessage);
        }
    }
}