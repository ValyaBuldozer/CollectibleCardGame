using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BaseNetworkArchitecture.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class TcpServerTests
    {
        private bool _clientConnect;

        /// <summary>
        ///     ������������ ������ (����� event)
        /// </summary>
        [TestMethod]
        public void IS_TestClientConnect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var �lient = new TcpClient();

            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            //assert
            Assert.IsTrue(_clientConnect);
        }

        /// <summary>
        ///     ����������� ������ (����� ������ ��������)
        /// </summary>
        [TestMethod]
        public void IS_TestClientConnectList()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var �lient = new TcpClient();


            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            //assert
            Assert.AreEqual(1, serv.Clients.Count);
        }

        /// <summary>
        ///     ����������� ����������
        /// </summary>
        [TestMethod]
        public void IS_TestSeveralClientsConnectList() //�������� ����� ���
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var �lient1 = new TcpClient();
            var �lient2 = new TcpClient();
            var �lient3 = new TcpClient();


            //act
            �lient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient3.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            //assert
            Assert.AreEqual(3, serv.Clients.Count);
        }

        /// <summary>
        ///     ����������� � ������ ���������� (���� ������������)
        /// </summary>
        [TestMethod]
        public void IS_TestClientDisconnect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var �lient = new TcpClient();


            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            �lient.Close();

            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        ///     ���������� � ������ ���������� (��������� �������������)
        /// </summary>
        [TestMethod]
        public void IS_TestSeveralClientsDisconnect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var �lient1 = new TcpClient();
            var �lient2 = new TcpClient();
            var �lient3 = new TcpClient();


            //act
            �lient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(50);
            �lient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(50);
            �lient3.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            �lient1.Close();
            �lient2.Close();
            �lient3.Close();

            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        ///     ����� �������, ����������� ������ �������, ����� ��������� �������
        /// </summary>
        [TestMethod]
        public void IS_TestServerStop_OneClient() //
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var �lient = new TcpClient();


            //act
            �lient.Connect(IPAddress.Parse("127.0.0.1"), port);

            serv.Stop();
            Thread.Sleep(50);
            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        ///     ����� �������, ����������� ���������� ��������, ����� ��������� �������
        /// </summary>
        [TestMethod]
        public void IS_TestServerStop_SeveralClients() //
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var �lient1 = new TcpClient();
            var �lient2 = new TcpClient();
            var �lient3 = new TcpClient();


            //act
            �lient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            �lient3.Connect(IPAddress.Parse("127.0.0.1"), port);
            serv.Stop();
            Thread.Sleep(50);
            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }


        private void Serv_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            _clientConnect = true;
        }
    }
}