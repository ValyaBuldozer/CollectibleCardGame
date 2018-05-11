using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests
{
    [TestClass]
    public class OutConnectionTest
    {
        [TestMethod]
        public void Test()
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("2.94.182.1"),103);
            var bytes = Encoding.UTF8.GetBytes("Обэмэ");
            var bytesLength = Encoding.UTF8.GetBytes(bytes.Length.ToString());
            client.GetStream().Write(bytesLength,0,bytesLength.Length);
            client.GetStream().Write(bytes,0,bytes.Length);
            Assert.IsTrue(client.Connected);
        }
    }
}
