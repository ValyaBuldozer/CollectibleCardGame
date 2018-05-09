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
    public class LogInFramePageViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private RelayCommand _logInCommand;

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

        public RelayCommand LogInCommand => _logInCommand ?? (_logInCommand = new RelayCommand(obj =>
        {
            if(string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
                return;

            LogInRequest?.Invoke(this,new LogInRegisterRequestEventArgs(_username,_password));
        }));

        public event EventHandler<LogInRegisterRequestEventArgs> LogInRequest;
    }
}
