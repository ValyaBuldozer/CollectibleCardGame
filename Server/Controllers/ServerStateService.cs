using System;
using System.Collections.Generic;
using System.Threading;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Repository;
using GameData.Network.Messages;
using Server.Controllers.Repository;
using Server.Models;
using Server.Network.Models;

namespace Server.Controllers
{
    public class ServerStateService
    {
        private readonly AwaitingClientsQueueController _clientsQueueController;
        private readonly UserReposController _userReposController;
        private readonly CardRepository _cardRepository;

        public ServerStateService(AwaitingClientsQueueController clientsQueueController,
            UserReposController userReposController,CardRepository cardRepository)
        {
            _clientsQueueController = clientsQueueController;
            _userReposController = userReposController;
            _cardRepository = cardRepository;
        }

        /// <summary>
        /// Поиск или добавление в очередь
        /// </summary>
        /// <param name="client"></param>
        /// <returns>true - лобби создано, false - игрок добавлен в очередь</returns>
        public bool FindLobby(Client client,Stack<Card> deck, UnitCard heroUnit)
        {
            if (_clientsQueueController.GetClientsQueue().Count == 0)
            {
                client.CurrentLobby = new GameLobby()
                {
                    FirstClient = client,
                    FirstPlayerDeck = deck,
                    FirstPlayerHeroUnit = heroUnit
                };
                _clientsQueueController.Enqueue(client);
                return false;
            }

            var firstPlayerClient = _clientsQueueController.Dequeue();

            //todo: запалить многопоточность - слабое место

            try
            {
                //todo : говнокод что делать то

                client.CurrentLobby = firstPlayerClient.CurrentLobby;
                firstPlayerClient.CurrentLobby.SecondClient = client;
                firstPlayerClient.CurrentLobby.SecondPlayerDeck = deck;
                firstPlayerClient.CurrentLobby.SecondPlayerHeroUnit = heroUnit;

                //todo :изменение настроек
                var msg = new MessageBase(MessageBaseType.GameRequestMessage
                    , new GameRequestMessage()
                    {
                        HeroUnitCard = heroUnit,
                        CardDeckIdList = new List<int>(),
                        AnswerData = true
                    });
                firstPlayerClient.ClientController.SendMessage(msg);
                client.ClientController.SendMessage(msg);

                var defaultSettings = new GameSettings()
                {
                    PlayerTurnInterval = 120000,
                    IsPlayerTurnTimerEnabled = true,
                    MaxDeckCardsCount = 30,
                    PlayerHandCardsMaxCount = 10,
                    PlayersCount = 2,
                    PlayerTableUnitsMaxCount = 10,
                    MaxPlayerMana = 10
                };
                client.CurrentLobby.InitializeGame(defaultSettings,_cardRepository);
                client.CurrentLobby.OnClose += OnLobbyClose;
                client.CurrentLobby.StartGame();

                return true;
            }
            catch (NotImplementedException)
            {
                //todo : допилить обработку исключений
                return false;
            }
        }

        public GameLobby CreateLobby(Client firstClient, Client secondClient)
        {
            if(firstClient == null || secondClient == null)
                throw new NullReferenceException();

            var gameLobby = new GameLobby(firstClient, secondClient);

            var message = new MessageBase(MessageBaseType.GameStartMessage, new GameStartMessage()
            {
                //TableCondition = gameLobby.TableCondition,
                EnemyUsername = secondClient.User.Username
            }, null);

            firstClient.ClientController.SendMessage(message);
            firstClient.CurrentLobby = gameLobby;

            ((GameStartMessage) message.Content).EnemyUsername = firstClient.User.Username;
            secondClient.ClientController.SendMessage(message);
            secondClient.CurrentLobby = gameLobby;

            return gameLobby;
        }

        private void OnLobbyClose(object sender, GameLobbyCloseEventArgs e)
        {
            if(!(sender is GameLobby lobby)) return;

            lobby.FirstClient.CurrentLobby = null;
            lobby.SecondClient.CurrentLobby = null;
            //todo : запись в статистику

            lobby.OnClose -= OnLobbyClose;
        }
    }
}
