using GameData.Enums;

namespace GameData.Network.Messages
{
    public class MessageBase
    {
        public MessageBaseType Type { get; }

        public IContent Content { get; }

        public IMessageHandler MessageHandler { get; }

        public MessageBase(MessageBaseType type,IContent content,IMessageHandler messageHandler)
        {
            Type = type;
            Content = content;
            MessageHandler = messageHandler;
        }

        public IContent HandleMessage(IContent content)
        {
            return MessageHandler.Execute(content);
        }
    }
}
