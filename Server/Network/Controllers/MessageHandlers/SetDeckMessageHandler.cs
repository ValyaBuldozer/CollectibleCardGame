using System;
using System.Linq;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Network;
using GameData.Network.Messages;
using Newtonsoft.Json;
using Server.Controllers.Repository;
using Server.Network.Models;

namespace Server.Network.Controllers.MessageHandlers
{
    public class SetDeckMessageHandler : MessageHandlerBase<SetDeckMessage>
    {
        private readonly IDataRepositoryController<Card> _cardRepositoryController;
        private readonly UserReposController _userReposController;

        public SetDeckMessageHandler(IDataRepositoryController<Card> cardRepositoryController,
            UserReposController userReposController)
        {
            _cardRepositoryController = cardRepositoryController;
            _userReposController = userReposController;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (!(content is SetDeckMessage message))
                return null;

            if (!(sender is Client client))
                return null;

            var deck = _cardRepositoryController.GetById(message.DeckIDs);
            var heroUnit = _cardRepositoryController.GetById(message.HeroCardId);

            if (deck.ToList().Exists(c => c == null))
                return new ErrorMessage {ErrorInfo = "Invalid card deck id"};

            if (heroUnit == null || heroUnit.ID < 3000 || !(heroUnit is UnitCard heroUnitCard))
                return new ErrorMessage {ErrorInfo = "Invalid hero unit card"};

            var deckInfo = new DeckInfo
            {
                DeckIds = message.DeckIDs,
                Fraction = message.Fraction,
                HeroCard = heroUnitCard
            };

            var user = _userReposController.GetEnumerable.FirstOrDefault(
                c => client.User.Username == c.Username);

            switch (message.Fraction)
            {
                case Fraction.Common:
                    return new ErrorMessage {ErrorInfo = "Error common fraciton"};
                case Fraction.North:
                    user.UserInfo.NorthDeck = JsonConvert.SerializeObject(deckInfo);
                    break;
                case Fraction.South:
                    user.UserInfo.SouthDeck = JsonConvert.SerializeObject(deckInfo);
                    break;
                case Fraction.Dark:
                    user.UserInfo.DarkDeck = JsonConvert.SerializeObject(deckInfo);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _userReposController.Edit(user);

            return message;
        }
    }
}