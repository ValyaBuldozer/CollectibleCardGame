using System;
using GameData.Network;
using GameData.Network.Messages;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class GameRequestMessageHandler : MessageHandlerBase<GameRequestMessage>
    {
        public override IContent Execute(IContent content, object sender)
        {
            throw new NotImplementedException();
        }
    }
}
