using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class INetworkCommunicatorTests
    {
        private readonly string _bigText =
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest" +
            "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest";


        [TestMethod]
        public void BigLengthMessageTest()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);

            INetworkCommunicator nc1 = new TcpCommunicator(new TcpClient());

            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            var result = listener.AcceptTcpClientAsync();

            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            var client = result.GetAwaiter().GetResult();
            INetworkCommunicator nc2 = new TcpCommunicator(client);

            var message = new NetworkMessage(_bigText);
            var flag = false;

            nc2.MessageRecievedEvent += (sender, args) =>
            {
                if (args.NetworkMessage.Content == _bigText)
                    flag = true;
            };

            nc2.StartReadMessages();
            nc1.SendMessage(message);

            Thread.Sleep(10);
            //Assert.IsTrue(flag);
        }

        #region 1

        /// <summary>
        ///     Подключение к TcpListener
        /// </summary>
        [TestMethod]
        public void INwC_Connect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            var сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            var result = listener.AcceptTcpClientAsync();

            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);

            //result.Wait();


            //assert
            Assert.IsTrue(nc.IsConnected);
        }

        /// <summary>
        ///     Подлючение к TcpListener и вызов Disconect
        /// </summary>
        [TestMethod]
        public void INwC_ConnectDisconnect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            var сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            var result = listener.AcceptTcpClientAsync();

            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);
            nc.Disconnect();

            //result.Wait();


            //assert
            Assert.IsFalse(nc.IsConnected);
        }

        /// <summary>
        ///     Подключение к TcpListener и разрыв соединения
        /// </summary>
        [TestMethod]
        public void INwC_ConnectExtraDisconnect() // екстра дисконект = разрыв соединения
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);
            var сlient = new TcpClient();
            INetworkCommunicator nc = new TcpCommunicator(сlient);
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            var result = listener.AcceptTcpClientAsync();

            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);
            listener.Stop();

            //result.Wait();


            //assert
            Assert.IsFalse(nc.IsConnected);
        }

        #endregion

        #region 2

        /// <summary>
        ///     Подключение к TcpListener  и создание второго коммуникатора
        /// </summary>
        [TestMethod]
        public void INwC_2_Connect()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);

            INetworkCommunicator nc1 = new TcpCommunicator(new TcpClient());

            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            var result = listener.AcceptTcpClientAsync();

            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            var client = result.GetAwaiter().GetResult();
            INetworkCommunicator nc2 = new TcpCommunicator(client);
            //result.Wait();


            //assert
            Assert.IsTrue(nc2.IsConnected);
        }

        /// <summary>
        ///     Подключение, первый отправляет второму сообщение
        /// </summary>
        [TestMethod]
        public void INwC_2_ConnectAndChat()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);

            INetworkCommunicator nc1 = new TcpCommunicator(new TcpClient());

            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            var result = listener.AcceptTcpClientAsync();
            var message = new NetworkMessage("test");


            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            var client = result.GetAwaiter().GetResult();
            INetworkCommunicator nc2 = new TcpCommunicator(client);

            nc1.SendMessage(message);
            var rezMessage = nc2.ReadMessage();
            //result.Wait();


            //assert
            Assert.AreEqual(message.Content, rezMessage.Content);
        }

        /// <summary>
        ///     Подключение, первый отправляет другому, другой принимет и отправляет
        /// </summary>
        [TestMethod]
        public void INwC_2_ConnectAndChatBoth()
        {
            //arrange
            var rnd = new Random();
            var port = rnd.Next(8000, 30001);

            INetworkCommunicator nc1 = new TcpCommunicator(new TcpClient());

            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            var result = listener.AcceptTcpClientAsync();
            var message = new NetworkMessage("test");


            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            var client = result.GetAwaiter().GetResult();
            INetworkCommunicator nc2 = new TcpCommunicator(client);

            //nc1.StartReadMessages();                            
            //nc2.StartReadMessages();


            nc1.SendMessage(message);
            var mes = nc2.ReadMessage();

            nc2.SendMessage(mes);
            var rezMessage = nc1.ReadMessage();
            //result.Wait();


            //assert
            Assert.AreEqual(message.Content, rezMessage.Content);
        }

        #endregion
    }
}