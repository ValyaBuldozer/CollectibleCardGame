using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
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
    }

    public class GameStateController : IGameStateController
    {
        private readonly TableCondition _tableCondition;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;

        [Dependency]
        public IDeckController DeckController { set; get; }

        public event EventHandler<GameEndEventArgs> GameEnd;

        public event EventHandler<GameStartObserverAction> GameStart; 

        public GameStateController(TableCondition tableCondition,IPlayerTurnDispatcher playerTurnDispatcher)
        {
            _tableCondition = tableCondition;
            _playerTurnDispatcher = playerTurnDispatcher;
        }

        public void Start(Stack<Card> firstDeck, string firstUsername, UnitCard firstHero,
            Stack<Card> secondDeck, string secondUsername,UnitCard secondHero)
        {
            if(string.IsNullOrEmpty(firstUsername) && string.IsNullOrEmpty(secondUsername))
                throw new NullReferenceException("Username is null");

            _tableCondition.Players.Add(new Player(firstHero)
            {
                Username = firstUsername
            });
            _tableCondition.Players.Add(new Player(secondHero)
            {
                Username = secondUsername
            });

            DeckController.AddDeck(firstUsername,firstDeck);
            DeckController.AddDeck(secondUsername,secondDeck);

            GameStart?.Invoke(this,new GameStartObserverAction(firstUsername,secondUsername));

            //выдача стартовой руки
            foreach (var iplayer in _tableCondition.Players)
                iplayer.HandCards.AddRange(DeckController.PopCards(iplayer.Username,4));

            //начинаем отсчет ходов (период хода в мс)
            _playerTurnDispatcher.Start(30000);

        }

        

        private void RunGameEndEvent(GameEndEventArgs e)
        {
            GameEnd?.Invoke(this,e);
        }
    }
}
