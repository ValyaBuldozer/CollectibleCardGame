using System.Net;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Server;
using Server.Controllers.Repository;
using Server.Network.Models;
using Server.Unity;

namespace Server.Network.Controllers
{
    public class ServerController
    {
        private readonly ConnectedClientsRepositoryController _clientsRepositoryController;
        private readonly IServer _server;

        public ServerController(IServer server, ConnectedClientsRepositoryController clientsRepositoryController)
        {
            _server = server;
            _server.ClientConnected += OnClientConnected;
            _clientsRepositoryController = clientsRepositoryController;
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            var client = new Client(e.ClientConnection);
            _clientsRepositoryController.Add(client);
            UnityKernel.Get<ILogger>().Print("Client connected");
        }

        public void Start(IPAddress ipAddress, int port)
        {
            _server.Start(ipAddress, port);
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}