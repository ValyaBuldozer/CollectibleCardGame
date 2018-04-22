using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Models;
using GameData.Models.Cards;

namespace GameData.Controllers.Table
{
    public class CardDeployDispatcher
    {
        private readonly TableCondition _tableCondition;
        private readonly IDeckController _deckController;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;

        public CardDeployDispatcher(TableCondition tableCondition, IDeckController deckController,
            IPlayerTurnDispatcher playerTurnDispatcher)
        {
            _tableCondition = tableCondition;
            _deckController = deckController;
            _playerTurnDispatcher = playerTurnDispatcher;
        }

        public bool CardDeployRequest(Card card, Player sender)
        {
            //todo : сообщение об ошибках
            if(!_tableCondition.Players.Contains(sender))
                throw new InvalidOperationException();

            if (!sender.HandCards.Contains(card))
                return false;

            if (sender.Mana.Current < card.Cost)
                return false;

            if (!Equals(_playerTurnDispatcher.CurrentPlayer, sender) || !card.CanBePlayedOnEnemyTurn)
                return false;

            switch (card.GetType().Name)
            {
                case nameof(UnitCard):
                    DeployCard((UnitCard)card,sender);
                    break;
                case nameof(SpellCard):
                    DeployCard((SpellCard)card,sender);
                    break;          
            }

            return true;
        }

        public void DeployCard(UnitCard card,Player sender)
        {
            sender.Mana.Current -= card.Cost;
            sender.HandCards.Remove(card);

            
        }

        public void DeployCard(SpellCard card,Player sender)
        {
            sender.Mana.Current -= card.Cost;
            sender.HandCards.Remove(card);
            //todo : выполнение action карты
        }
    }
}
