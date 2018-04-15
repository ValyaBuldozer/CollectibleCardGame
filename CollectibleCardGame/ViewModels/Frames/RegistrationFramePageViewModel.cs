using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.Unity;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class RegistrationFramePageViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private RelayCommand _registrationCommand;

        public string Username
        {
            set
            {
                _username = value;
                NotifyPropertyChanged(nameof(Username));
            }
            get => _username;
        }

        public string Password
        {
            set
            {
                _password = value;
                NotifyPropertyChanged(nameof(Password));
            }
            get => _password;
        }

        public RelayCommand RegistrationCommand => _registrationCommand ?? (_registrationCommand = new RelayCommand(obj =>
        {
            //todo : валидация пароля логина
            UnityKernel.Get<UserController>().RegistrationRequest(Username, Password);
        }));
    }
}
