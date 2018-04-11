using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers.Services;
using Server.Network.Models;
using Server.Unity;

namespace Server.Network.Controllers.MessageHandlers
{
    public class GameRequestMessageHandler : IMessageHandler
    {
        public IContent Execute(IContent content,object sender)
        {
            if(!(content is GameRequestMessage))
                throw new InvalidOperationException("Incorrect message type");

            if(!(sender is Client))
                throw new InvalidOperationException("Incorrect sender");

            var message = (GameRequestMessage) content;
            var client = (Client) sender;

            message.AnswerData = UnityKernel.Get<ServerStateService>().FindLobby(client);

            return message;
        }
    }
}
