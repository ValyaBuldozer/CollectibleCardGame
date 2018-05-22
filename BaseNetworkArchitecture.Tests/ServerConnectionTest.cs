using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseNetworkArchitecture.Tests
{
    [TestClass]
    public class ServerConnectionTest
    {
        [TestMethod]
        public void ServConnect()
        {
            var сlient = new TcpClient();
            сlient.Connect(IPAddress.Parse("178.57.32.250"), 8800);

            Assert.IsTrue(сlient.Connected);
        }
    }
}