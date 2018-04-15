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
        public void IS_TestClientConnect()
        {

            //arrange
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñlient = new TcpClient();

            //act
            ñlient.Connect(IPAddress.Parse("127.0.0.1"),8800);
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.IsTrue(_clientConnect);

        }

        [TestMethod]
        public void IS_TestClientConnectList()
        {

            //arrange
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñlient = new TcpClient();


            //act
            ñlient.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.AreEqual(1,serv.Clients.Count);

        }

        [TestMethod]
        public void IS_TestSeveralClientsConnectList() //ðàáîòàåò ÷åðåç ðàç
        {

            //arrange
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñlient1 = new TcpClient();
            TcpClient ñlient2 = new TcpClient();
            TcpClient ñlient3 = new TcpClient();


            //act
            ñlient1.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            //System.Threading.Thread.Sleep(5);
            ñlient2.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            //System.Threading.Thread.Sleep(5);
            ñlient3.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.AreEqual(3, serv.Clients.Count);

        }

        [TestMethod]
        public void IS_TestClientDisconnect()
        {

            //arrange
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñlient = new TcpClient();

            
            //act
            ñlient.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            System.Threading.Thread.Sleep(50);
            ñlient.Close();
            
            //assert
            Assert.AreEqual(0, serv.Clients.Count);

        }

        [TestMethod]
        public void IS_TestSeveralClientsDisconnect()
        {

            //arrange
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñlient1 = new TcpClient();
            TcpClient ñlient2 = new TcpClient();
            TcpClient ñlient3 = new TcpClient();


            //act
            ñlient1.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            //System.Threading.Thread.Sleep(50);
            ñlient2.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            //System.Threading.Thread.Sleep(50);
            ñlient3.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            System.Threading.Thread.Sleep(50);
            ñlient1.Close();
            ñlient2.Close();
            ñlient3.Close();

            //assert
            Assert.AreEqual(0, serv.Clients.Count);

        }

        [TestMethod]
        public void IS_TestServerStop_OneClient() //
        {

            //arrange
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñlient = new TcpClient();
            


            //act
            ñlient.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            
            serv.Stop();
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.AreEqual(0, serv.Clients.Count);

        }

        [TestMethod]
        public void IS_TestServerStop_SeveralClients() //
        {

            //arrange
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start();
            TcpClient ñlient1 = new TcpClient();
            TcpClient ñlient2 = new TcpClient();
            TcpClient ñlient3 = new TcpClient();


            //act
            ñlient1.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            //System.Threading.Thread.Sleep(5);
            ñlient2.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            //System.Threading.Thread.Sleep(5);
            ñlient3.Connect(IPAddress.Parse("127.0.0.1"), 8800);
            serv.Stop();
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.AreEqual(0, serv.Clients.Count);

        }


        private void Serv_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            _clientConnect = true;
        }
    }
}
