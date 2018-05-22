using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;
using GameData.Models.Units;

namespace GameData.Controllers.Table
{
    public interface ICardDeployDispatcher
    {
        bool CardDeployRequest(Card card, Player sender,Unit actionTarget);
        void DeployCard(UnitCard card, Player sender,Unit actionTarget);
        void DeployCard(SpellCard card, Player sender,Unit actionTarget);
        event EventHandler<CardDeployObserverAction> OnCardDeploy;
    }

    public class CardDeployDispatcher : ICardDeployDispatcher
    {
        private readonly TableCondition _tableCondition;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;
        private readonly IGameActionController _gameActionController;
        private readonly IUnitDispatcher _unitDispatcher;

        public CardDeployDispatcher(TableCondition tableCondition,
            IPlayerTurnDispatcher playerTurnDispatcher, IGameActionController gameActionController,
            IUnitDispatcher unitDispatcher)
        {
            _tableCondition = tableCondition;
            _playerTurnDispatcher = playerTurnDispatcher;
            _gameActionController = gameActionController;
            _unitDispatcher = unitDispatcher;
        }

        public event EventHandler<CardDeployObserverAction> OnCardDeploy;

        public bool CardDeployRequest(Card card, Player sender,Unit actionTarget)
        {
            //todo : сообщение об ошибках
            if(!_tableCondition.Players.Contains(sender))
                throw new InvalidOperationException();

            if (!sender.HandCards.Contains(card))
                return false;

            if (sender.State.Current < card.Cost)
                return false;

            switch (card.GetType().Name)
            {
                case nameof(UnitCard):
                    DeployCard((UnitCard)card,sender,actionTarget);
                    break;
                case nameof(SpellCard):
                    DeployCard((SpellCard)card,sender,actionTarget);
                    break;          
            }

            OnCardDeploy?.Invoke(this,new CardDeployObserverAction(card,actionTarget));
            return true;
        }

        public void DeployCard(UnitCard card,Player sender,Unit actionTarget)
        {
            if(!_unitDispatcher.CardPlayedSpawn(card,sender,actionTarget))
                return;

            sender.State.Current -= card.Cost;
            sender.HandCards.Remove(card);
        }

        public void DeployCard(SpellCard card,Player sender,Unit actionTarget)
        {
            _gameActionController.ExecuteAction(card.ActionInfo,sender,actionTarget);
            sender.State.Current -= card.Cost;
            sender.HandCards.Remove(card);
        }
    }
}
