using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers;
using Server.Controllers.Repository;
using Server.Database;
using Server.Exceptions;
using Server.Models;
using Server.Network.Models;
using Server.Unity;
using UserInfo = GameData.Network.UserInfo;

namespace Server.Network.Controllers.MessageHandlers
{
    public class LogInMessageHandler : MessageHandlerBase<LogInMessage>
    {
        private readonly ConnectedClientsRepositoryController _clientsRepositoryController;

        public LogInMessageHandler(ConnectedClientsRepositoryController clientsRepositoryController)
        {
            _clientsRepositoryController = clientsRepositoryController;
        }

        public override IContent Execute(IContent content,object sender)
        {
            if(!(content is LogInMessage))
                throw new InvalidOperationException();

            var message = (LogInMessage) content;
            var client = (Client) sender;

            try
            {
                var user = UnityKernel.Get<UserService>().LogIn(message.Username, message.Password);
                if (user == null) return null;
                
                    var userInfo = new UserInfo()
                    {
                        Username = user.Username,
                        DarkDeck = user.UserInfo.DarkDeck,
                        NorthDeck = user.UserInfo.NorthDeck,
                        SouthDeck = user.UserInfo.SouthDeck
                    };
                    message.AnswerData = userInfo;
                    client.User = user;
                client.ClientConnection.IdentificatorTocken = user.Username;

                var awaitingLobby = _clientsRepositoryController.GetCollection.FirstOrDefault(c =>
                    c.CurrentLobby?.AwaitingClintUsername == client.User.Username)?.CurrentLobby;

                //переподключение к игре
                if (awaitingLobby != null)
                {
                    client.CurrentLobby = awaitingLobby;
                    awaitingLobby.OnClientReconnect(client);
                    return new GameRequestMessage() {Fraction = Fraction.Common,AnswerData = message};
                }

                return message;
            }
            catch (UserServiceException e)
            {
                return new ErrorMessage() {ErrorInfo = e.Message};
            }
        }
    }
}
