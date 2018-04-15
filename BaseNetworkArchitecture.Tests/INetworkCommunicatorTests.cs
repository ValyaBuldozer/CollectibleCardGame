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
    public class INetworkCommunicatorTests
    {
        private bool _clientConnect;

        [TestMethod]
        public void INWC_Connect() // не понятно
        {

            //arrange
            TcpClient сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8800);
            listener.Start();
            listener.AcceptTcpClientAsync();

            nc.Connect(IPAddress.Parse("127.0.0.1"), 8800);


            //TcpServer serv = new TcpServer();
            //serv.ClientConnected += Serv_ClientConnected;
            //serv.Start();


            //act
            //сlient.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            //System.Threading.Thread.Sleep(50);
            //assert
            Assert.IsTrue(_clientConnect);

        }
        private void Serv_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            _clientConnect = true;
        }
    }
}
