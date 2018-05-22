using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;

namespace GameData.Controllers.Table
{
    public interface ICardDrawController
    {
        void DealCardsToPlayer(string username, int count);
        void DealCardsToPlayer(Player player, int count);
        void DealCard(Player player, Card card);
        event EventHandler<CardDrawObserverAction> OnCardDraw;
    }

    public class CardDrawController : ICardDrawController
    {
        private readonly TableCondition _tableCondition;
        private readonly IDeckController _deckController;
        private readonly GameSettings _settings;
        private readonly IDataRepositoryController<Entity> _entityController;

        public CardDrawController(TableCondition tableCondition, IDeckController deckController,
            GameSettings settings,IDataRepositoryController<Entity> entityController)
        {
            _tableCondition = tableCondition;
            _deckController = deckController;
            _settings = settings;
            _entityController = entityController;
        }

        public event EventHandler<CardDrawObserverAction> OnCardDraw; 

        public void DealCardsToPlayer(string username,int count)
        {
            DealCardsToPlayer(_tableCondition.GetPlayerByUsername(username),count);
        }

        public void DealCardsToPlayer(Player player,int count)
        {
            var cards = _deckController.PopCards(player.Username, count);

            foreach (var iCard in cards)
            {
                DealCard(player,iCard);
            }
        }

        public void DealCard(Player player, Card card)
        {
            if (player.HandCards.Count < _settings.PlayerHandCardsMaxCount)
            {
                player.HandCards.Add(card);
                _entityController.AddNewItem(card);
                player.State.DeckCardsCount--;
                OnCardDraw?.Invoke(this, new CardDrawObserverAction(card, player.Username));
            }
            else
                //todo : card burn event
                player.State.DeckCardsCount--;
            return;
        }
    }
}
