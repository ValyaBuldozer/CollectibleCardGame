using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using BaseNetworkArchitecture.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class INwCommunicatorAndIServerTests
    {
        private bool flag;

        /// <summary>
        ///     Старт IServer и подключение INetwork Communicator
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndConnect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            var сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            serv.Start(IPAddress.Parse("127.0.0.1"), port);


            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);

            //result.Wait();


            //assert
            Assert.IsTrue(nc.IsConnected);
        }

        /// <summary>
        ///     Старт IServer, подключение коммуникатора, разрыв соединения коммуникатора
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndConnectThenDisconnec()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            var сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            serv.Start(IPAddress.Parse("127.0.0.1"), port);


            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);
            nc.Disconnect();
            //result.Wait();


            //assert
            Assert.IsFalse(nc.IsConnected);
        }

        /// <summary>
        ///     Старт, подключение, остановка сервера
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndStop()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            var сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            serv.Start(IPAddress.Parse("127.0.0.1"), port);


            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);
            serv.Stop();
            //result.Wait();


            //assert
            Assert.IsFalse(nc.IsConnected);
        }

        /// <summary>
        ///     Старт, подключение нескольких коммуникаторов
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndSeveralConnect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            var сlient1 = new TcpClient();
            var сlient2 = new TcpClient();
            var сlient3 = new TcpClient();
            INetworkCommunicator nc1 = new TcpCommunicator(сlient1);
            INetworkCommunicator nc2 = new TcpCommunicator(сlient2);
            INetworkCommunicator nc3 = new TcpCommunicator(сlient3);
            serv.Start(IPAddress.Parse("127.0.0.1"), port);


            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            nc2.Connect(IPAddress.Parse("127.0.0.1"), port);
            nc3.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50); //перестраховка
            //result.Wait();


            //assert
            Assert.AreEqual(3, serv.Clients.Count);
        }

        /// <summary>
        ///     Старт, подключение нескольких коммуникаторов, отключение коммуникаторов
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndSeveralConnectDisconnect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            var сlient1 = new TcpClient();
            var сlient2 = new TcpClient();
            var сlient3 = new TcpClient();
            INetworkCommunicator nc1 = new TcpCommunicator(сlient1);
            INetworkCommunicator nc2 = new TcpCommunicator(сlient2);
            INetworkCommunicator nc3 = new TcpCommunicator(сlient3);
            serv.Start(IPAddress.Parse("127.0.0.1"), port);


            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);

            nc2.Connect(IPAddress.Parse("127.0.0.1"), port);

            nc2.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            nc1.Disconnect();
            nc2.Disconnect();
            nc3.Disconnect();

            //result.Wait();


            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        ///     Старт, подключение, сервер отправляет сообшение
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndServerSend()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            var сlient = new TcpClient();

            INetworkCommunicator nc = new TcpCommunicator(сlient);

            serv.Start(IPAddress.Parse("127.0.0.1"), port);


            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            foreach (var client in serv.Clients) client.Communicator.SendMessage(new NetworkMessage("test"));
            var mes = nc.ReadMessage();


            //result.Wait();


            //assert
            Assert.AreEqual("test", mes.Content);
        }

        /// <summary>
        ///     Старт, подключение, отправка сообщения клиентом
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndClientSend()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            var сlient1 = new TcpClient();
            var сlient2 = new TcpClient();

            INetworkCommunicator nc1 = new TcpCommunicator(сlient1);
            INetworkCommunicator nc2 = new TcpCommunicator(сlient2);

            serv.Start(IPAddress.Parse("127.0.0.1"), port);


            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            nc2.Connect(IPAddress.Parse("127.0.0.1"), port);
            Thread.Sleep(50);
            nc2.StartReadMessages();
            nc2.MessageRecievedEvent += Nc2_MessageRecievedEvent;

            nc1.SendMessage(new NetworkMessage("test"));

            //NetworkMessage mes =nc2.ReadMessage();


            //result.Wait();


            //assert
            Assert.IsTrue(flag);
            //Assert.AreEqual("test", mes.Content);
        }

        private void Nc2_MessageRecievedEvent(object sender, MessageEventArgs e)
        {
            flag = true;
        }
    }
}