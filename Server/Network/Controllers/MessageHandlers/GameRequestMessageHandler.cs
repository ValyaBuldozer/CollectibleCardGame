using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers;
using Server.Controllers.Repository;
using Server.Network.Models;
using Server.Unity;

namespace Server.Network.Controllers.MessageHandlers
{
    public class GameRequestMessageHandler : MessageHandlerBase<GameRequestMessage>
    {
        private readonly CardReposController _cardReposController;

        public GameRequestMessageHandler(CardReposController cardReposController)
        {
            _cardReposController = cardReposController;
        }

        public override IContent Execute(IContent content,object sender)
        {
            if(!(content is GameRequestMessage))
                throw new InvalidOperationException("Incorrect message type");

            if(!(sender is Client))
                throw new InvalidOperationException("Incorrect sender");

            var message = (GameRequestMessage) content;
            var client = (Client) sender;
            Stack<Card> deck = _cardReposController.GetDeckById(message.CardDeckIdList);

            message.AnswerData = UnityKernel.Get<ServerStateService>().FindLobby(
                client,deck,message.HeroUnitCard);

            return message;
        }
    }
}
