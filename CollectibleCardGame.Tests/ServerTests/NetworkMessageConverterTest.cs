using GameData.Enums;
using GameData.Network;
using GameData.Network.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests.JsonParserTest
{
    [TestClass]
    public class NetworkMessageConverterTest
    {
        // [TestMethod]
        public void SerializeTest()
        {
            var converter = new NetworkMessageConverter();
            var msg = new MessageBase(MessageBaseType.LogInMessage, new LogInMessage(), null);

            var networkMessage = converter.SerializeMessage(msg);

            Assert.IsNotNull(networkMessage);
        }

        [TestMethod]
        public void SerializeDeserializeTest()
        {
            var converter = new NetworkMessageConverter();

            var msg = new MessageBase(MessageBaseType.LogInMessage,
                new LogInMessage {Username = "test", Password = "test"}, null);

            var networkMessage = converter.SerializeMessage(msg);

            var messageBase = converter.DeserializeMessage(networkMessage);

            Assert.IsTrue(((LogInMessage) messageBase.Content).Username == "test");
        }
    }
}