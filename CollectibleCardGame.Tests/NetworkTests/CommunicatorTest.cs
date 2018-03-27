using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseNetworkArchitecture;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Server;

namespace CollectibleCardGame.Tests.NetworkTests
{
    [TestClass]
    public class TcpCommunicatorTest
    {
        [TestMethod]
        public void ConnectionTest()
        {
            TcpServer server = new TcpServer(8800);
            server.Start();

            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 8800);

            TcpCommunicator clientCommunicator = new TcpCommunicator(client);

            clientCommunicator.SendMessage(new Message() { Content = "test" });
            var answer = clientCommunicator.ReadMessage();

            Assert.IsTrue(answer != null);
        }
    }
}