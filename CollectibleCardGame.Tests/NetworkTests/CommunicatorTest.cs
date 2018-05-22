using System.Net;
using System.Net.Sockets;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using BaseNetworkArchitecture.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests.NetworkTests
{
    [TestClass]
    public class TcpCommunicatorTest
    {
        // [TestMethod]
        public void ConnectionTest()
        {
            var server = new TcpServer();
            server.ClientConnected += OnClientConnected;
            server.Start(IPAddress.Parse("127.0.0.1"), 8800);

            var client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 8800);

            var clientCommunicator = new TcpCommunicator(client);

            clientCommunicator.SendMessage(new NetworkMessage {Content = "test"});
            var answer = clientCommunicator.ReadMessage();

            Assert.IsTrue(answer != null);
        }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            ((TcpCommunicator) sender).SendMessage(e.NetworkMessage);
        }

        public void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            ((TcpCommunicator) e.ClientConnection.Communicator).MessageRecievedEvent += OnMessageRecieved;
        }
    }
}