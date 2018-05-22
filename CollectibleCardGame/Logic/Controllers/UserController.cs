using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Models;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using GameData.Enums;
using GameData.Network.Messages;
using Unity.Attributes;

namespace CollectibleCardGame.Logic.Controllers
{
    public class UserController
    {
        [Dependency]
        public CurrentUserService CurrentUserService { set; get; }

        private readonly INetworkController _networkController;
        private readonly ILogger _logger;
        private readonly LogInFramePageViewModel _logInViewModel;
        private readonly RegistrationFramePageViewModel _registrationViewModel;

        public UserController(ILogger logger,INetworkController networkController,
            LogInFramePageViewModel logInViewModel,RegistrationFramePageViewModel registrationViewModel)
        {
            _logger = logger;
            _networkController = networkController;
            _logInViewModel = logInViewModel;
            _registrationViewModel = registrationViewModel;

            _logInViewModel.LogInRequest += OnLogInRequest;
            _registrationViewModel.RegisterRequest += OnRegisterRequest;
        }

        private void OnRegisterRequest(object sender, Services.LogInRegisterRequestEventArgs e)
        {
            RegistrationRequest(e.Username,e.Password);
        }

        private void OnLogInRequest(object sender, Services.LogInRegisterRequestEventArgs e)
        {
            LogInRequest(e.Username,e.Password);
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

            _networkController.SendMessage(message);
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

            _networkController.SendMessage(message);
        }

        public void ResetUser()
        {
            //todo : доделать
            CurrentUserService = null;
        }

        public void SetUser(string username)
        {
            if(CurrentUserService != null)
                ResetUser();

            CurrentUserService.Username = username;
        }
    }
}
