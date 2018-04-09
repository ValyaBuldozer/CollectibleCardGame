using GameData.Enums;
using Newtonsoft.Json;

namespace GameData.Network.Messages
{
    public class MessageBase
    {
        public MessageBaseType Type { get; }

        /// <summary>
        /// Object из-за десериализации
        /// </summary>
        public object Content { get; }

        [JsonIgnore]
        public IMessageHandler MessageHandler { get; }

        public MessageBase(MessageBaseType type,object content,IMessageHandler messageHandler=null)
        {
            Type = type;
            Content = content;
            MessageHandler = messageHandler;
        }

        public IContent HandleMessage(object sender)
        {
            IContent content = Content as IContent;
            return MessageHandler.Execute(content,sender);
        }
    }
}
