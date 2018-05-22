using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using GameData.Network;
using GameData.Network.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Controllers;
using Server.Network.Controllers;
using Server.Repositories;
using Server.Unity;

namespace CollectibleCardGame.Tests.ServerTests
{
    [TestClass]
    public class ServerControllerTest
    {
        [TestMethod]
        public void RegistrationTest()
        {
            UnityKernel.InitializeKernel();
            UnityKernel.Get<ServerController>().Start(IPAddress.Parse("127.0.0.1"), 8800);

            TcpClient tcpClient = new TcpClient("localhost",8800);
            var registrationClient = new TcpCommunicator(tcpClient);
            Random rnd = new Random();
            string username = "testuser" + rnd.Next(0, 1000);
            var collection = UnityKernel.Get<UserRepository>().DatabaseCollection;

            RegistrationMessage message = new RegistrationMessage()
            {
                Username = username,
                Password = "test"
            };

            var networkMessage = UnityKernel.Get<NetworkMessageConverter>().SerializeMessage(message);
            registrationClient.SendMessage(networkMessage);
            var answer = registrationClient.ReadMessage();

            Assert.IsTrue(collection.FirstOrDefault(
                c=>c.Username == username)!=null);
        }
    }
}
