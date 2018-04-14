using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Network;
using GameData.Network.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Network.Controllers;

namespace CollectibleCardGame.Tests.JsonParserTest
{
    [TestClass]
    public class NetworkMessageConverterTest
    {
       // [TestMethod]
        public void SerializeTest()
        {
            NetworkMessageConverter converter = new NetworkMessageConverter();
            MessageBase msg = new MessageBase(MessageBaseType.LogInMessage,new LogInMessage(), null);

            var networkMessage = converter.SerializeMessage(msg);

            Assert.IsNotNull(networkMessage);
        }

        [TestMethod]
        public void SerializeDeserializeTest()
        {
            var converter = new NetworkMessageConverter();

            MessageBase msg = new MessageBase(MessageBaseType.LogInMessage, new LogInMessage()
                {Username = "test",Password = "test"}, null);

            var networkMessage = converter.SerializeMessage(msg);

            var messageBase = converter.DeserializeMessage(networkMessage);

            Assert.IsTrue(((LogInMessage)messageBase.Content).Username == "test");
        }
    }
}
