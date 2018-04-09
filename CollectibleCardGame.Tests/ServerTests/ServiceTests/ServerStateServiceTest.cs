using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common.Messages;
using BaseNetworkArchitecture.Server;
using GameData.Enums;
using GameData.Network.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Models;
using Server.Network.Controllers;
using Server.Network.Models;
using Server.Unity;

namespace CollectibleCardGame.Tests.ServerTests.ServiceTests
{
    [TestClass]
    public class ServerStateServiceTest
    {
        [TestMethod]
        public void ClientGameRequestTest()
        {
            UnityKernel.InitializeKernel();
            Client firstClient = new Client(new TcpClientConnection(null))
            {
                User = new User() { Username = "test"}
            };
            Client secondClient = new Client(new TcpClientConnection(null))
            {
                User = new User() { Username = "test2"}
            };
            var gameRequestNetworkMessage = UnityKernel.Get<NetworkMessageConverter>().SerializeMessage(
                new MessageBase(MessageBaseType.GameRequestMessage, new GameRequestMessage(), null));

            firstClient.ClientController.OnMessageRecieved(firstClient,new MessageEventArgs()
            {
                NetworkMessage = gameRequestNetworkMessage
            });
            secondClient.ClientController.OnMessageRecieved(secondClient, new MessageEventArgs()
            {
                NetworkMessage = gameRequestNetworkMessage
            });

            Assert.IsNotNull(firstClient.CurrentLobby);
            Assert.IsNotNull(secondClient.CurrentLobby);
        }
    }
}
