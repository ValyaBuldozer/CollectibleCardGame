using System;
using System.Collections.Generic;
using System.Linq;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;

namespace GameData.Controllers.Global
{
    public interface IGameStateController
    {
        void Start(Stack<Card> firstDeck, string firstUsername, UnitCard firstHero,
            Stack<Card> secondDeck, string secondUsername, UnitCard secondHero);

        event EventHandler<GameEndEventArgs> GameEnd;
        event EventHandler<GameStartObserverAction> GameStart;
        event EventHandler<PlayerStateChangesObserverAction> PlayerStateChanged;
        void SendTableConditionRequest(string username);
    }

    public class GameStateController : IGameStateController
    {
        private readonly ICardDrawController _cardDrawController;
        private readonly IDeckController _deckController;
        private readonly IDataRepositoryController<Entity> _entitytRepositoryController;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;
        private readonly TableCondition _tableCondition;
        private readonly IUnitDispatcher _unitDispatcher;

        public GameStateController(TableCondition tableCondition, IPlayerTurnDispatcher playerTurnDispatcher,
            IDeckController deckController, IDataRepositoryController<Entity> entitytRepositoryController,
            ICardDrawController cardDrawController, IUnitDispatcher unitDispatcher)
        {
            _tableCondition = tableCondition;
            _playerTurnDispatcher = playerTurnDispatcher;
            _deckController = deckController;
            _entitytRepositoryController = entitytRepositoryController;
            _cardDrawController = cardDrawController;
            _unitDispatcher = unitDispatcher;
        }

        public event EventHandler<GameEndEventArgs> GameEnd;

        public event EventHandler<GameStartObserverAction> GameStart;

        public event EventHandler<PlayerStateChangesObserverAction> PlayerStateChanged;

        public void Start(Stack<Card> firstDeck, string firstUsername, UnitCard firstHero,
            Stack<Card> secondDeck, string secondUsername, UnitCard secondHero)
        {
            if (string.IsNullOrEmpty(firstUsername) && string.IsNullOrEmpty(secondUsername))
                throw new NullReferenceException("Username is null");

            var firstPlayer = new Player(firstHero)
            {
                Username = firstUsername
            };
            var secondPlayer = new Player(secondHero)
            {
                Username = secondUsername
            };

            _tableCondition.Players.Add(firstPlayer);
            _tableCondition.Players.Add(secondPlayer);

            _entitytRepositoryController.AddNewItem(firstPlayer);
            _entitytRepositoryController.AddNewItem(secondPlayer);

            _entitytRepositoryController.AddNewItem(firstPlayer.HeroUnit);
            _entitytRepositoryController.AddNewItem(secondPlayer.HeroUnit);

            firstPlayer.State.DeckCardsCount = firstDeck.Count;
            secondPlayer.State.DeckCardsCount = secondDeck.Count;

            firstPlayer.HeroUnit.State.PropertyChanged += _unitDispatcher.OnUnitStateChanges;
            secondPlayer.HeroUnit.State.PropertyChanged += _unitDispatcher.OnUnitStateChanges;

            firstPlayer.HeroUnit.DiedEvent += OnUnitDies;
            secondPlayer.HeroUnit.DiedEvent += OnUnitDies;

            _deckController.AddDeck(firstUsername, firstDeck);
            _deckController.AddDeck(secondUsername, secondDeck);

            firstPlayer.State.Changed += Mana_Changed;
            secondPlayer.State.Changed += Mana_Changed;

            GameStart?.Invoke(this, new GameStartObserverAction(firstPlayer, secondPlayer));

            //выдача стартовой руки
            foreach (var iplayer in _tableCondition.Players)
                _cardDrawController.DealCardsToPlayer(iplayer, 4);

            //начинаем отсчет ходов (период хода в мс)
            _playerTurnDispatcher.Start();
        }

        public void SendTableConditionRequest(string username)
        {
            if (!_tableCondition.Players.Exists(p => p.Username == username))
                return;

            var firstPlayer = _tableCondition.Players.FirstOrDefault(p => p.Username == username);
            var secondPlayer = _tableCondition.Players.FirstOrDefault(p => p.Username != username);
            var observerAction = new GameStartObserverAction(firstPlayer, secondPlayer)
            {
                TargetPlayer = firstPlayer,
                CurrentPlayerUsername = _playerTurnDispatcher.CurrentPlayer.Username
            };

            GameStart?.Invoke(this, observerAction);
        }

        private void Mana_Changed(object sender, PlayerManaChangeEventArgs e)
        {
            PlayerStateChanged?.Invoke(this, new PlayerStateChangesObserverAction(
                (sender as Player)?.Username, e.PlayerState));
        }

        private void OnUnitDies(object sender, HeroUnitDiedEventArgs e)
        {
            var winner = _tableCondition.Players.FirstOrDefault(p => p.Username != e.Player.Username);

            GameEnd?.Invoke(this, new GameEndEventArgs(GameEndReason.HeroUnitKill,
                winner?.Username));
        }
    }
}