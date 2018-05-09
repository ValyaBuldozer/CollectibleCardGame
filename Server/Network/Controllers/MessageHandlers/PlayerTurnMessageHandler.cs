using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.PlayerTurn;
using GameData.Network;
using GameData.Network.Messages;
using Server.Network.Models;

namespace Server.Network.Controllers.MessageHandlers
{
    public class PlayerTurnMessageHandler : MessageHandlerBase<PlayerTurn>
    {
        public override IContent Execute(IContent content,object sender)
        {
            if(!(sender is Client client && content is PlayerTurnMessage message))
                throw new InvalidOperationException();

            if(message.PlayerTurn.Sender.Username != client.User.Username)
                return new ErrorMessage(){ AnswerData = "Username error"};

            switch (message.PlayerTurn.Type)
            {
                case PlayerTurnType.CardDeploy:
                    client.CurrentLobby.HandlePlayerTurn((CardDeployPlayerTurn)message.PlayerTurn);
                    break;
                case PlayerTurnType.UnitAttack:
                    client.CurrentLobby.HandlePlayerTurn((UnitAttackPlayerTurn)message.PlayerTurn);
                    break;
                case PlayerTurnType.TurnEnd:
                    client.CurrentLobby.HandlePlayerTurn((EndPlayerTurn)message.PlayerTurn);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}
