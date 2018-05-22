using System;
using CollectibleCardGame.Services;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class LogInFramePageViewModel : BaseViewModel
    {
        private RelayCommand _logInCommand;
        private string _password;
        private string _username;

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
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
                return;

            LogInRequest?.Invoke(this, new LogInRegisterRequestEventArgs(_username, _password));
        }));

        public event EventHandler<LogInRegisterRequestEventArgs> LogInRequest;
    }
}