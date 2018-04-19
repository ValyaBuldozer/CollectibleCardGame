using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class INwCommunicatorAndIServerTests
    {
        /// <summary>
        /// Старт IServer и подключение INetwork Communicator
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndConnect()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            TcpClient сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            serv.Start();
            
            

            //act
            nc.Connect();

            //result.Wait();


            //assert
            Assert.IsTrue(nc.IsConnected);
        }
    }
}
