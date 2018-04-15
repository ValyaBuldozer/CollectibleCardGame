using System;
using GameData.Enums;
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
        public bool FindLobby(Client client)
        {
            if (_clientsQueueController.GetClientsQueue().Count == 0)
            {
                _clientsQueueController.Enqueue(client);
                return false;
            }

            var secondClient = _clientsQueueController.Dequeue();

            //todo: запалить многопоточность - слабое место

            try
            {
                CreateLobby(firstClient: client, secondClient: secondClient);
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
            gameLobby.InitializeGame();


            var message = new MessageBase(MessageBaseType.GameStartMessage, new GameStartMessage()
            {
                TableCondition = gameLobby.TableCondition,
                EnemyUsername = secondClient.User.Username
            }, null);

            firstClient.ClientController.SendMessage(message);
            firstClient.CurrentLobby = gameLobby;

            (message.Content as GameStartMessage).EnemyUsername = firstClient.User.Username;
            secondClient.ClientController.SendMessage(message);
            secondClient.CurrentLobby = gameLobby;

            return gameLobby;
        }
    }
}
