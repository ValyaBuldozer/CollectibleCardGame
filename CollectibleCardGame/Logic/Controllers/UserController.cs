using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Models;
using CollectibleCardGame.Network.Controllers;
using GameData.Enums;
using GameData.Network.Messages;
using Unity.Attributes;

namespace CollectibleCardGame.Logic.Controllers
{
    public class UserController
    {
        [Dependency]
        public CurrentUser CurrentUser { set; get; }

        [Dependency]
        public INetworkController NetworkConnectionController { set; get; }

        private readonly ILogger _logger;

        public UserController(ILogger logger)
        {
            _logger = logger;
        }

        public void LogInRequest(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new NullReferenceException("Username and password can not be null");

            var message = new MessageBase(MessageBaseType.LogInMessage,new LogInMessage()
            {
                Username = username,
                Password = password
            });

            NetworkConnectionController.SendMessage(message);
        }

        public void RegistrationRequest(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new NullReferenceException("Username and password can not be null");

            var message = new MessageBase(MessageBaseType.RegistrationMessage, new RegistrationMessage()
            {
                Username = username,
                Password = password
            });

            NetworkConnectionController.SendMessage(message);
        }

        public void ResetUser()
        {
            //todo : доделать
            CurrentUser = null;
        }

        public void SetUser(string username)
        {
            if(CurrentUser != null)
                ResetUser();

            CurrentUser = new CurrentUser() {Username = username};
        }
    }
}
