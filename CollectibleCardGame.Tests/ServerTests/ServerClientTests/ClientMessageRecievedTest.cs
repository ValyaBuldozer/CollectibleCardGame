﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common.Messages;
using BaseNetworkArchitecture.Server;
using GameData.Enums;
using GameData.Network;
using GameData.Network.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Server.Network.Controllers;
using Server.Network.Models;
using Server.Repositories;
using Server.Unity;

namespace CollectibleCardGame.Tests.ServerTests.ServerClientTests
{
    //[TestClass]
    public class ClientMessageRecievedTest
    {
        //[TestMethod]
        public void ClientControllerRegistrationTest()
        {
            //NetworkMessage clientMessage = new NetworkMessage()
            //{
            //    Content = JsonConvert.SerializeObject(new RegistrationMessage()
            //    {
            //        Username = "ClientControllerTest1",
            //        Password = "test"
            //    })
            //};
            //UnityKernel.InitializeKernel();
            //TcpClientConnection connection = new TcpClientConnection(new TcpClient());
            //Client client = new Client(connection);
            
            UnityKernel.InitializeKernel();
            UnityKernel.Get<ClientController>().OnMessageRecieved(new Client(new TcpClientConnection(null)),new MessageEventArgs()
            {
                NetworkMessage = UnityKernel.Get<NetworkMessageConverter>().SerializeMessage(
                    new MessageBase(MessageBaseType.LogInMessage,new LogInMessage()
                    {
                        Username = "ClientControlerTest1",
                        Password = "test"
                    }, null)
                    )
            } );

            Assert.IsTrue(UnityKernel.Get<UserRepository>().Collection.FirstOrDefault(u=>u.Username == "ClientControlerTest1")!=null);
        }

        private void Communicator_MessageRecievedEvent(object sender, MessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
