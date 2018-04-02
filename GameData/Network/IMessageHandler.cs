using GameData.Network.Messages;

namespace GameData.Network
{
    public interface IMessageHandler
    {
        IContent Execute(IContent content);
    }
}
