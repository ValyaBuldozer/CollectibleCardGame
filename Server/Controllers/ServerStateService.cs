using System;
using System.Collections.Generic;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Repository;
using GameData.Network;
using GameData.Network.Messages;
using Newtonsoft.Json;
using Server.Controllers.Repository;
using Server.Models;
using Server.Network.Models;

namespace Server.Controllers
{
    public class ServerStateService
    {
        private readonly CardRepository _cardRepository;
        private readonly IDataRepositoryController<Card> _cardRepositoryController;
        private readonly AwaitingClientsQueueController _clientsQueueController;
        private readonly GameSettings _gameSettings;

        public ServerStateService(AwaitingClientsQueueController clientsQueueController,
            CardRepository cardRepository, GameSettings gameSettings,
            IDataRepositoryController<Card> cardRepositoryController)
        {
            _clientsQueueController = clientsQueueController;
            _cardRepository = cardRepository;
            _cardRepositoryController = cardRepositoryController;
            _gameSettings = gameSettings;
        }

        /// <summary>
        ///     Поиск или добавление в очередь
        /// </summary>
        /// <param name="client"></param>
        /// <returns>true - лобби создано, false - игрок добавлен в очередь</returns>
        public bool FindLobby(Client client, Fraction fraction)
        {
            var deck = GetDeck(client, fraction);
            var heroUnit = GetHeroCard(client, fraction);

            if (_clientsQueueController.GetClientsQueue().Count == 0)
            {
                client.CurrentLobby = new GameLobby
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
                client.CurrentLobby = firstPlayerClient.CurrentLobby;
                firstPlayerClient.CurrentLobby.SecondClient = client;
                firstPlayerClient.CurrentLobby.SecondPlayerDeck = deck;
                firstPlayerClient.CurrentLobby.SecondPlayerHeroUnit = heroUnit;

                //todo :изменение настроек
                var msg = new MessageBase(MessageBaseType.GameRequestMessage
                    , new GameRequestMessage
                    {
                        Fraction = fraction,
                        AnswerData = true
                    });
                firstPlayerClient.ClientController.SendMessage(msg);
                client.ClientController.SendMessage(msg);

                client.CurrentLobby.InitializeGame(_gameSettings, _cardRepository);
                client.CurrentLobby.OnClose += OnLobbyClose;
                client.CurrentLobby.StartGame();

                return true;
            }
            catch (Exception e)
            {
                //todo : допилить обработку исключений
                return false;
            }
        }

        public GameLobby CreateLobby(Client firstClient, Client secondClient)
        {
            if (firstClient == null || secondClient == null)
                throw new NullReferenceException();

            var gameLobby = new GameLobby(firstClient, secondClient);

            var message = new MessageBase(MessageBaseType.GameStartMessage, new GameStartMessage
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

        public void OnClietnReconnect(Client client)
        {
        }

        private void OnLobbyClose(object sender, GameLobbyCloseEventArgs e)
        {
            if (!(sender is GameLobby lobby)) return;

            lobby.FirstClient.CurrentLobby = null;
            lobby.SecondClient.CurrentLobby = null;
            //todo : запись в статистику

            lobby.OnClose -= OnLobbyClose;
        }

        private Stack<Card> GetDeck(Client client, Fraction fraction)
        {
            int[] array;
            switch (fraction)
            {
                case Fraction.North:
                    array = JsonConvert.DeserializeObject<DeckInfo>(client.User.UserInfo.NorthDeck).DeckIds;
                    break;
                case Fraction.South:
                    array = JsonConvert.DeserializeObject<DeckInfo>(client.User.UserInfo.SouthDeck).DeckIds;
                    break;
                case Fraction.Dark:
                    array = JsonConvert.DeserializeObject<DeckInfo>(client.User.UserInfo.DarkDeck).DeckIds;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fraction), fraction, null);
            }

            return new Stack<Card>(_cardRepositoryController.GetById(array));
        }

        private UnitCard GetHeroCard(Client client, Fraction fraction)
        {
            var deckInfo = JsonConvert.DeserializeObject<DeckInfo>(client.User.UserInfo.GetDeck(fraction));

            return deckInfo.HeroCard;
        }
    }
}