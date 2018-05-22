using System;
using BaseNetworkArchitecture.Common.Messages;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Models.Observer;
using GameData.Models.PlayerTurn;
using GameData.Network;
using GameData.Network.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Network.Controllers
{
    [TestClass]
    public class NetworkMessageConverterTests
    {
        [TestMethod]
        public void SerializeTest()
        {
            var converter = new NetworkMessageConverter();
            //var networkMessage = new NetworkMessage();
            var messageBase = new MessageBase(MessageBaseType.LogInMessage, new LogInMessage
            {
                Username = "test",
                Password = "test"
            });

            var serializedMessage = converter.SerializeMessage(messageBase);

            Assert.IsTrue(serializedMessage.Content.Contains("test"));
        }

        [TestMethod]
        public void Deserialize()
        {
            var converter = new NetworkMessageConverter();
            var networkMessage = new NetworkMessage(
                "{\"Type\":0,\"Content\":{\"Username\":\"test\",\"Password\":\"test\",\"AnswerData\":null}}");
            var result = new MessageBase(MessageBaseType.LogInMessage, new LogInMessage
            {
                Username = "test",
                Password = "test"
            });

            var deserializedMessage = converter.DeserializeMessage(networkMessage);

            Assert.IsTrue(result.Content.Equals(deserializedMessage.Content));
        }

        [TestMethod]
        public void DeserializeHandlerTest()
        {
            var converter =
                new NetworkMessageConverter {LogInMessageHandlerBase = new TestLogInMessageHandler()};
            var networkMessage = new NetworkMessage(
                "{\"Type\":0,\"Content\":{\"Username\":\"test\",\"Password\":\"test\",\"AnswerData\":null}}");
            var deserializedMessage = converter.DeserializeMessage(networkMessage);

            var handlerResult = deserializedMessage.HandleMessage(null);

            Assert.IsTrue(handlerResult is LogInMessage message && message.Username == "test");
        }


        [TestMethod]
        public void PlayerTurnDeserializeTest()
        {
            var converter = new NetworkMessageConverter();
            var message = new MessageBase(MessageBaseType.PlayerTurnMessage,
                new PlayerTurnMessage
                {
                    PlayerTurn = new CardDeployPlayerTurn(null, new UnitCard())
                });

            var json = converter.SerializeMessage(message);

            var deserialized = converter.DeserializeMessage(json);

            Assert.AreEqual(message.Content as PlayerTurnMessage, deserialized.Content as PlayerTurnMessage);
        }

        [TestMethod]
        public void ObserverActionDeserializeTest()
        {
            var converter = new NetworkMessageConverter();
            var message = new MessageBase(MessageBaseType.ObserverActionMessage,
                new ObserverActionMessage
                {
                    ObserverAction = new CardDeployObserverAction(new UnitCard(), null)
                });
            var json = converter.SerializeMessage(message);

            var deserialized = converter.DeserializeMessage(json);

            Assert.AreEqual(message.Content as ObserverActionMessage,
                deserialized.Content as ObserverActionMessage);
        }
    }

    public class TestLogInMessageHandler : MessageHandlerBase<LogInMessage>
    {
        public override IContent Execute(IContent content, object sender)
        {
            if (!(content is LogInMessage))
                throw new InvalidOperationException();

            return (LogInMessage) content;
        }
    }
}