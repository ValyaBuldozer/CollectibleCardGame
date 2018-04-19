using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using BaseNetworkArchitecture.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class INetworkCommunicatorTests
    {
        private bool _clientConnect;
        private NetworkMessage _networkMessageRecieved;

        [TestMethod]
        public void INwC_Connect() // не понятно
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
            Assert.IsTrue(nc.IsConnected);

        }

        [TestMethod]
        public void CommunicatorsStartReadMessageTest()
        {
            int randomPort = (new Random()).Next(10000,20000);
            INetworkCommunicator communicator = new TcpCommunicator(new TcpClient());
            communicator.MessageRecievedEvent += Communicator_MessageRecievedEvent;
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"),randomPort);

            NetworkMessage message = new NetworkMessage("test");
            byte[] messageBuff = message.Encoder.GetBytes(message.Content);
            byte[] lengthBuff = message.Encoder.GetBytes(messageBuff.Length.ToString());

            listener.Start();
            var result = listener.AcceptTcpClientAsync();
            communicator.Connect(IPAddress.Parse("127.0.0.1"), randomPort);
            var client = result.GetAwaiter().GetResult();

            communicator.StartReadMessages();

            client.GetStream().Write(lengthBuff,0,lengthBuff.Length);
            client.GetStream().Write(messageBuff, 0, messageBuff.Length);
            Thread.Sleep(1);

            Assert.IsTrue(_networkMessageRecieved.Content == message.Content);
        }

        [TestMethod]
        public void CommunicatorFewMessagesRead()
        {
            int randomPort = (new Random()).Next(10000, 20000);
            INetworkCommunicator communicator = new TcpCommunicator(new TcpClient());
            communicator.MessageRecievedEvent += Communicator_MessageRecievedEvent;
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), randomPort);

            NetworkMessage message = new NetworkMessage("test");
            byte[] messageBuff = message.Encoder.GetBytes(message.Content);
            byte[] lengthBuff = message.Encoder.GetBytes(messageBuff.Length.ToString());

            listener.Start();
            var result = listener.AcceptTcpClientAsync();
            communicator.Connect(IPAddress.Parse("127.0.0.1"), randomPort);
            var client = result.GetAwaiter().GetResult();

            communicator.StartReadMessages();

            client.GetStream().Write(lengthBuff, 0, lengthBuff.Length);
            client.GetStream().Write(messageBuff, 0, messageBuff.Length);
            //todo : use await
            Thread.Sleep(1);
            Thread.Sleep(1);

            Assert.IsTrue(_networkMessageRecieved.Content == message.Content);

            message.Content = "test2";
            messageBuff = message.Encoder.GetBytes(message.Content);
            lengthBuff = message.Encoder.GetBytes(messageBuff.Length.ToString());

            client.GetStream().Write(lengthBuff, 0, lengthBuff.Length);
            client.GetStream().Write(messageBuff, 0, messageBuff.Length);
            Thread.Sleep(1);
            Thread.Sleep(1);

            Assert.IsTrue(_networkMessageRecieved.Content == message.Content);

            message.Content = "test3";
            messageBuff = message.Encoder.GetBytes(message.Content);
            lengthBuff = message.Encoder.GetBytes(messageBuff.Length.ToString());

            client.GetStream().Write(lengthBuff, 0, lengthBuff.Length);
            client.GetStream().Write(messageBuff, 0, messageBuff.Length);
            Thread.Sleep(1);
            Thread.Sleep(1);

            Assert.IsTrue(_networkMessageRecieved.Content == message.Content);
        }

        private void Communicator_MessageRecievedEvent(object sender, Common.Messages.MessageEventArgs e)
        {
            _networkMessageRecieved = e.NetworkMessage;
        }

        private void Serv_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            _clientConnect = true;
        }
    }
}
