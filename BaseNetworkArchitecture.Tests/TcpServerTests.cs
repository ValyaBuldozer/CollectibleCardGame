using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using BaseNetworkArchitecture.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class TcpServerTests
    {
        private bool _clientConnect;
        [TestMethod]
        public void StartServerTest()
        {

            //arrange
            TcpServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñ = new TcpClient();

            //act
            ñ.Connect(IPAddress.Parse("127.0.0.1"),8800);

            //assert
            System.Threading.Thread.Sleep(5);
            Assert.IsTrue(_clientConnect);

        }

        private void Serv_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            _clientConnect = true;
        }
    }
}
