using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Network;
using GameData.Network.Messages;

namespace Server.Network.Controllers.MessageHandlers
{
    public class GameStartMessageHandler : IMessageHandler
    {
        public IContent Execute(IContent content,object sender)
        {
            throw new NotImplementedException();
        }
    }
}
