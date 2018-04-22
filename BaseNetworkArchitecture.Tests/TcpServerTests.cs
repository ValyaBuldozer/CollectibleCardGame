using System;
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

        /// <summary>
        /// ������������ ������ (����� event)
        /// </summary>
        [TestMethod]
        public void IS_TestClientConnect()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            TcpClient �lient = new TcpClient();

            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"),port);
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.IsTrue(_clientConnect);

        }

        /// <summary>
        /// ����������� ������ (����� ������ ��������)
        /// </summary>
        [TestMethod]
        public void IS_TestClientConnectList()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            TcpClient �lient = new TcpClient();


            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.AreEqual(1,serv.Clients.Count);

        }

        /// <summary>
        /// ����������� ����������
        /// </summary>
        [TestMethod]
        public void IS_TestSeveralClientsConnectList() //�������� ����� ���
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            TcpClient �lient1 = new TcpClient();
            TcpClient �lient2 = new TcpClient();
            TcpClient �lient3 = new TcpClient();


            //act
            �lient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient3.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.AreEqual(3, serv.Clients.Count);

        }

        /// <summary>
        /// ����������� � ������ ���������� (���� ������������)
        /// </summary>
        [TestMethod]
        public void IS_TestClientDisconnect()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            TcpClient �lient = new TcpClient();

            
            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            �lient.Close();
            
            //assert
            Assert.AreEqual(0, serv.Clients.Count);

        }

        /// <summary>
        /// ���������� � ������ ���������� (��������� �������������)
        /// </summary>
        [TestMethod]
        public void IS_TestSeveralClientsDisconnect()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            TcpClient �lient1 = new TcpClient();
            TcpClient �lient2 = new TcpClient();
            TcpClient �lient3 = new TcpClient();


            //act
            �lient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(50);
            �lient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(50);
            �lient3.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            �lient1.Close();
            �lient2.Close();
            �lient3.Close();

            //assert
            Assert.AreEqual(0, serv.Clients.Count);

        }

        /// <summary>
        /// ����� �������, ����������� ������ �������, ����� ��������� �������
        /// </summary>
        [TestMethod]
        public void IS_TestServerStop_OneClient() //
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            TcpClient �lient = new TcpClient();
            


            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"), port);
            
            serv.Stop();
            System.Threading.Thread.Sleep(50);
            //assert
            Assert.AreEqual(0, serv.Clients.Count);

        }

        /// <summary>
        /// ����� �������, ����������� ���������� ��������, ����� ��������� �������
        /// </summary>
        [TestMethod]
        public void IS_TestServerStop_SeveralClients() //
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            TcpClient �lient1 = new TcpClient();
            TcpClient �lient2 = new TcpClient();
            TcpClient �lient3 = new TcpClient();


            //act
            �lient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient3.Connect(IPAddress.Parse("127.0.0.1"), port);
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
