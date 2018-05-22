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
        ///     Подлключение одного (через event)
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
            var сlient = new TcpClient();

            //act
            сlient.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            //assert
            Assert.IsTrue(_clientConnect);
        }

        /// <summary>
        ///     Подключение одного (через список клиентов)
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
            var сlient = new TcpClient();


            //act
            сlient.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            //assert
            Assert.AreEqual(1, serv.Clients.Count);
        }

        /// <summary>
        ///     Подключение нескольких
        /// </summary>
        [TestMethod]
        public void IS_TestSeveralClientsConnectList() //работает через раз
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            serv.ClientConnected += Serv_ClientConnected;
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            var сlient1 = new TcpClient();
            var сlient2 = new TcpClient();
            var сlient3 = new TcpClient();


            //act
            сlient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            сlient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            сlient3.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            //assert
            Assert.AreEqual(3, serv.Clients.Count);
        }

        /// <summary>
        ///     Подключение и разрыв соединения (один пользователь)
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
            var сlient = new TcpClient();


            //act
            сlient.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            сlient.Close();

            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        ///     Подлючение и разрыв соединения (несколько пользователей)
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
            var сlient1 = new TcpClient();
            var сlient2 = new TcpClient();
            var сlient3 = new TcpClient();


            //act
            сlient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(50);
            сlient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(50);
            сlient3.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            сlient1.Close();
            сlient2.Close();
            сlient3.Close();

            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        ///     Старт сервера, подключение одного клиента, затем остановка сервера
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
            var сlient = new TcpClient();


            //act
            сlient.Connect(IPAddress.Parse("127.0.0.1"), port);

            serv.Stop();
            Thread.Sleep(50);
            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        ///     Старт сервера, подключение несколькиз клиентов, затем остановка сервера
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
            var сlient1 = new TcpClient();
            var сlient2 = new TcpClient();
            var сlient3 = new TcpClient();


            //act
            сlient1.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            сlient2.Connect(IPAddress.Parse("127.0.0.1"), port);
            //System.Threading.Thread.Sleep(5);
            сlient3.Connect(IPAddress.Parse("127.0.0.1"), port);
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