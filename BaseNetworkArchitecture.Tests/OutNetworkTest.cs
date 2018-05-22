using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class OutNetworkTest
    {
        [TestMethod]
        public void CreateListener()
        {
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8800);
            listener.Start();

            var client = listener.AcceptTcpClient();

            Assert.IsTrue(client.Connected);
        }

        [TestMethod]
        public void ConnectTest()
        {
            var listener = new TcpListener(IPAddress.Parse("192.168.0.105"), 8800);
            var client = new TcpClient();
            listener.Start();

            var result = listener.AcceptTcpClientAsync();
            client.Connect(IPAddress.Parse("178.57.32.250"), 8800);
            var tcp = result.GetAwaiter().GetResult();

            Assert.IsTrue(client.Connected);
        }
    }
}