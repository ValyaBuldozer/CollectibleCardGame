using System;
using GameData.Network;
using GameData.Network.Messages;

namespace Server.Network.Controllers.MessageHandlers
{
    public class GameStartMessageHandler : MessageHandlerBase<GameStartMessage>
    {
        public override IContent Execute(IContent content, object sender)
        {
            throw new NotImplementedException();
        }
    }
}