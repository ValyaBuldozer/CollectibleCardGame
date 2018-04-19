using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
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
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
            
            

            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);

            //result.Wait();


            //assert
            Assert.IsTrue(nc.IsConnected);
        }

        /// <summary>
        /// Старт IServer, подключение коммуникатора, разрыв соединения коммуникатора
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndConnectThenDisconnec()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            TcpClient сlient = new TcpClient();
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
        /// Старт, подключение, остановка сервера
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndStop()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            TcpClient сlient = new TcpClient();
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
        /// Старт, подключение нескольких коммуникаторов
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndSeveralConnect()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            TcpClient сlient1 = new TcpClient();
            TcpClient сlient2 = new TcpClient();
            TcpClient сlient3 = new TcpClient();
            INetworkCommunicator nc1 = new TcpCommunicator(сlient1);
            INetworkCommunicator nc2 = new TcpCommunicator(сlient2);
            INetworkCommunicator nc3 = new TcpCommunicator(сlient3);
            serv.Start(IPAddress.Parse("127.0.0.1"), port);
           


            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            nc2.Connect(IPAddress.Parse("127.0.0.1"), port);
            nc3.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            //result.Wait();


            //assert
            Assert.AreEqual(3, serv.Clients.Count);
        }

        /// <summary>
        /// Старт, подключение нескольких коммуникаторов, отключение коммуникаторов
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndSeveralConnectDisconnect()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            TcpClient сlient1 = new TcpClient();
            TcpClient сlient2 = new TcpClient();
            TcpClient сlient3 = new TcpClient();
            INetworkCommunicator nc1 = new TcpCommunicator(сlient1);
            INetworkCommunicator nc2 = new TcpCommunicator(сlient2);
            INetworkCommunicator nc3 = new TcpCommunicator(сlient3);
            serv.Start(IPAddress.Parse("127.0.0.1"), port);



            //act
            nc1.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            nc2.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            nc2.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            nc1.Disconnect();
            nc2.Disconnect();
            nc3.Disconnect();

            //result.Wait();


            //assert
            Assert.AreEqual(0, serv.Clients.Count);
        }

        /// <summary>
        /// Старт, подключение, сервер отправляет сообшение
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndSeverSend()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            TcpClient сlient = new TcpClient();
          
            INetworkCommunicator nc = new TcpCommunicator(сlient);
           
            serv.Start(IPAddress.Parse("127.0.0.1"), port);



            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            foreach (var client in serv.Clients)
            {
                client.Communicator.SendMessage(new NetworkMessage("test"));
            }
            NetworkMessage mes = nc.ReadMessage();


            //result.Wait();


            //assert
            Assert.AreEqual("test", mes.Content );
        }

        /// <summary>
        /// Старт, подключение, отправка сообщения клиентом
        /// </summary>
        [TestMethod]
        public void Interaction_StartAndClientSend()
        {

            //arrange
            Random rnd = new Random();
            int port = rnd.Next(8000, 30001);
            IServer serv = new TcpServer();
            TcpClient сlient = new TcpClient();

            INetworkCommunicator nc = new TcpCommunicator(сlient);

            serv.Start(IPAddress.Parse("127.0.0.1"), port);



            //act
            nc.Connect(IPAddress.Parse("127.0.0.1"), port);
            System.Threading.Thread.Sleep(50);
            
            foreach (var client in serv.Clients)
            {
                client.Communicator.SendMessage(new NetworkMessage("test"));
            }
            NetworkMessage mes = nc.ReadMessage();


            //result.Wait();


            //assert
            Assert.AreEqual("test", mes.Content);
        }
    }
}
