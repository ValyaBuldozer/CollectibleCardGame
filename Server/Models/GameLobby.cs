using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Enums;
using GameData.Kernel;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.PlayerTurn;
using GameData.Models.Repository;
using GameData.Network.Messages;
using Server.Network.Models;
using NullReferenceException = System.NullReferenceException;

namespace Server.Models
{
    public class GameLobby
    {
        private readonly Container _gameDataContainer;

        public Client FirstClient { set; get; }

        public Stack<Card> FirstPlayerDeck { set; get; }

        public UnitCard FirstPlayerHeroUnit { set; get; }

        public Client SecondClient { set; get; }

        public Stack<Card> SecondPlayerDeck { set; get; }

        public UnitCard SecondPlayerHeroUnit { set; get; }

        public TableCondition GeTableCondition => _gameDataContainer.Get<TableCondition>();

        public event EventHandler<GameLobbyCloseEventArgs> OnClose; 

        public GameLobby(Client firstClient,Client secondClient)
        {
            _gameDataContainer = new Container();
            FirstClient = firstClient;
            SecondClient = secondClient;
        }

        public GameLobby()
        {
            _gameDataContainer = new Container();
        }

        public void HandlePlayerTurn(CardDeployPlayerTurn playerTurn,string senderUsername)
        {
            var sender = GeTableCondition.Players.FirstOrDefault(p => p.Username == senderUsername);

            if(sender == null)
                return;

            playerTurn.Sender = sender;
            _gameDataContainer.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);
        }

        public void HandlePlayerTurn(UnitAttackPlayerTurn playerTurn,string senderUsername)
        {
            var sender = GeTableCondition.Players.FirstOrDefault(p => p.Username == senderUsername);

            if (sender == null)
                return;

            playerTurn.Sender = sender;
            _gameDataContainer.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(playerTurn);
        }

        public void HandlePlayerTurn(EndPlayerTurn playerTurn,string senderUsername)
        {
            var sender = GeTableCondition.Players.FirstOrDefault(p => p.Username == senderUsername);

            if (sender == null)
                return;

            playerTurn.Sender = sender;
            _gameDataContainer.Get<IPlayerTurnHandler<EndPlayerTurn>>().Execute(playerTurn);
        }

        public void InitializeGame(GameSettings settings,CardRepository cardRepository)
        {
            if(FirstClient == null && SecondClient == null)
                throw new NullReferenceException();

            _gameDataContainer.CardRepository = cardRepository;
            _gameDataContainer.Initialize(settings);
            _gameDataContainer.Get<ObserverActionRepositoryController>().ItemAdded += OnObserverActionAdded;
            _gameDataContainer.Get<IGameStateController>().GameEnd += OnGameEnd;
        }

        public void StartGame()
        {
            if(FirstClient == null || SecondClient == null)
                throw new NullReferenceException("Players are null");

            if(FirstPlayerDeck == null || SecondPlayerDeck == null)
                throw new NullReferenceException("Decks are null");

            if(FirstPlayerHeroUnit == null || SecondPlayerHeroUnit == null)
                throw new NullReferenceException("HeroUnits are null");

            //перемешиваем колоды
            var shuffleDeck = FirstPlayerDeck.OrderBy(c => Guid.NewGuid()).ToList();
            FirstPlayerDeck = new System.Collections.Generic.Stack<Card>(shuffleDeck);

            shuffleDeck = SecondPlayerDeck.OrderBy(c => Guid.NewGuid()).ToList();
            SecondPlayerDeck = new System.Collections.Generic.Stack<Card>(shuffleDeck);

            _gameDataContainer.Get<IGameStateController>().Start(FirstPlayerDeck,FirstClient.User.Username,
                FirstPlayerHeroUnit,SecondPlayerDeck,SecondClient.User.Username,SecondPlayerHeroUnit);
        }

        private void OnObserverActionAdded(object sender, ObserverActionAddedEventArgs e)
        {
            var message = new MessageBase(MessageBaseType.ObserverActionMessage, new ObserverActionMessage()
            {
                ObserverAction = e.Item
            });

            if(e.Item.TargetPlayer == null || FirstClient.User.Username == e.Item.TargetPlayer.Username)
                FirstClient.ClientController.SendMessage(message);

            if(e.Item.TargetPlayer == null || SecondClient.User.Username == e.Item.TargetPlayer.Username)
                SecondClient.ClientController.SendMessage(message);
        }

        private void OnGameEnd(object sender, GameEndEventArgs e)
        {
            OnClose?.Invoke(this,new GameLobbyCloseEventArgs(e.WinnerUsername));
        }
    }
}
