using System;
using System.Collections.Generic;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Cards;
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


        public ServerStateService(AwaitingClientsQueueController clientsQueueController,
            UserReposController userReposController)
        {
            _clientsQueueController = clientsQueueController;
            _userReposController = userReposController;
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
                //отправляем сообщения о начале игры обоим клиентам
                var message = new MessageBase(MessageBaseType.GameStartMessage, new GameStartMessage()
                {
                    EnemyUsername = firstPlayerClient.User.Username
                }, null);
                client.ClientController.SendMessage(message);
                ((GameStartMessage)message.Content).EnemyUsername = client.User.Username;
                firstPlayerClient.ClientController.SendMessage(message);

                client.CurrentLobby = firstPlayerClient.CurrentLobby;
                firstPlayerClient.CurrentLobby.SecondClient = client;
                firstPlayerClient.CurrentLobby.SecondPlayerDeck = deck;
                firstPlayerClient.CurrentLobby.SecondPlayerHeroUnit = heroUnit;

                //todo :изменение настроек

                var defaultSettings = new GameSettings()
                {
                    PlayerTurnInterval = 60000,
                    IsPlayerTurnTimerEnabled = true,
                    MaxDeckCardsCount = 30,
                    PlayerHandCardsMaxCount = 10,
                    PlayersCount = 2,
                    PlayerTableUnitsMaxCount = 10
                };
                client.CurrentLobby.InitializeGame(defaultSettings);
                client.CurrentLobby.StartGame();

                return true;
            }
            catch (NullReferenceException)
            {
                //todo : допилить обработку исключений
                return false;
            }
        }

        public GameLobby CreateLobby(Client firstClient, Client secondClient)
        {
            //todo : МНОГОПОТОЧНОСТЬ
            //todo : ЗАПРЕТИТЬ ИГРАТЬ С САМИМ СОБОЙ - ПРОВЕРКИ
            if(firstClient == null || secondClient == null)
                throw new NullReferenceException();

            var gameLobby = new GameLobby(firstClient, secondClient);
            //gameLobby.InitializeGame();


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
    }
}
