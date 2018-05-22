using GameData.Enums;
using Newtonsoft.Json;

namespace GameData.Network.Messages
{
    public class MessageBase
    {
        public MessageBase(MessageBaseType type, object content, IMessageHandler messageHandler = null)
        {
            Type = type;
            Content = content;
            MessageHandler = messageHandler;
        }

        public MessageBaseType Type { get; }

        /// <summary>
        ///     Object из-за десериализации
        /// </summary>
        public object Content { get; }

        [JsonIgnore]
        public IMessageHandler MessageHandler { set; get; }

        public IContent HandleMessage(object sender)
        {
            //todo : тут вылетает NullReference рандомно
            var content = Content as IContent;
            return MessageHandler.Execute(content, sender);
        }
    }
}