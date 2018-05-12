using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;
using GameData.Models.Units;
using Unity.Attributes;

namespace GameData.Controllers.Global
{
    public interface IGameStateController
    {
        void Start(Stack<Card> firstDeck, string firstUsername, UnitCard firstHero,
            Stack<Card> secondDeck, string secondUsername, UnitCard secondHero);
        event EventHandler<GameEndEventArgs> GameEnd;
        event EventHandler<GameStartObserverAction> GameStart;
        event EventHandler<PlayerStateChangesObserverAction> PlayerStateChanged;
    }

    public class GameStateController : IGameStateController
    {
        private readonly TableCondition _tableCondition;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;
        private readonly IDeckController _deckController;
        private readonly IDataRepositoryController<Entity> _entitytRepositoryController;
        private readonly ICardDrawController _cardDrawController;

        public event EventHandler<GameEndEventArgs> GameEnd;

        public event EventHandler<GameStartObserverAction> GameStart;

        public event EventHandler<PlayerStateChangesObserverAction> PlayerStateChanged;

        public GameStateController(TableCondition tableCondition,IPlayerTurnDispatcher playerTurnDispatcher,
            IDeckController deckController,IDataRepositoryController<Entity> entitytRepositoryController,
            ICardDrawController cardDrawController)
        {
            _tableCondition = tableCondition;
            _playerTurnDispatcher = playerTurnDispatcher;
            _deckController = deckController;
            _entitytRepositoryController = entitytRepositoryController;
            _cardDrawController = cardDrawController;
        }

        public void Start(Stack<Card> firstDeck, string firstUsername, UnitCard firstHero,
            Stack<Card> secondDeck, string secondUsername,UnitCard secondHero)
        {
            if(string.IsNullOrEmpty(firstUsername) && string.IsNullOrEmpty(secondUsername))
                throw new NullReferenceException("Username is null");

            var firstPLayer = new Player(firstHero)
            {
                Username = firstUsername
            };
            var secondPlayer = new Player(secondHero)
            {
                Username = secondUsername
            };

            _tableCondition.Players.Add(firstPLayer);
            _tableCondition.Players.Add(secondPlayer);

            _entitytRepositoryController.AddNewItem(firstPLayer);
            _entitytRepositoryController.AddNewItem(secondPlayer);

            _entitytRepositoryController.AddNewItem(firstPLayer.HeroUnit);
            _entitytRepositoryController.AddNewItem(secondPlayer.HeroUnit);

            _deckController.AddDeck(firstUsername,firstDeck);
            _deckController.AddDeck(secondUsername,secondDeck);

            firstPLayer.Mana.Changed += Mana_Changed;
            secondPlayer.Mana.Changed += Mana_Changed;

            GameStart?.Invoke(this,new GameStartObserverAction(firstPLayer,secondPlayer));

            //выдача стартовой руки
            foreach (var iplayer in _tableCondition.Players)
                _cardDrawController.DealCardsToPlayer(iplayer,4);

            //начинаем отсчет ходов (период хода в мс)
            _playerTurnDispatcher.Start();

        }

        private void Mana_Changed(object sender, PlayerManaChangeEventArgs e)
        {
            PlayerStateChanged?.Invoke(this,new PlayerStateChangesObserverAction(
                (sender as Player)?.Username,e.PlayerMana));
        }

        private void RunGameEndEvent(GameEndEventArgs e)
        {
            GameEnd?.Invoke(this,e);
        }
    }
}
