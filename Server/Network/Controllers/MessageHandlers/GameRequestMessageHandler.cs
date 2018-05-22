using System;
using GameData.Controllers.Data;
using GameData.Models.Cards;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers;
using Server.Network.Models;
using Server.Unity;

namespace Server.Network.Controllers.MessageHandlers
{
    public class GameRequestMessageHandler : MessageHandlerBase<GameRequestMessage>
    {
        private readonly IDataRepositoryController<Card> _cardReposController;

        public GameRequestMessageHandler(IDataRepositoryController<Card> cardReposController)
        {
            _cardReposController = cardReposController;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (!(content is GameRequestMessage message))
                throw new InvalidOperationException("Incorrect message type");

            if (!(sender is Client client))
                throw new InvalidOperationException("Incorrect sender");

            if (client.CurrentLobby != null)
                return new ErrorMessage();

            if (string.IsNullOrEmpty(client.User.UserInfo.GetDeck(message.Fraction)))
                return new ErrorMessage {ErrorInfo = "No deck found with this fraction"};

            //Stack<Card> deck = new Stack<Card>(_cardReposController.GetById(message.CardDeckIdList));

            message.AnswerData = UnityKernel.Get<ServerStateService>().FindLobby(
                client, message.Fraction);

            return message;
        }
    }
}