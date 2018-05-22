using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using GameData.Network;
using GameData.Network.Messages;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class SetDeckMessageHandler : MessageHandlerBase<SetDeckMessage>
    {
        private readonly ILogger _logger;

        public SetDeckMessageHandler(ILogger logger)
        {
            _logger = logger;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (!(content is SetDeckMessage message)) return null;

            _logger.LogAndPrint("Колода установлена!");
            return null;
        }
    }
}
