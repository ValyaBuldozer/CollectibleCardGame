using System.Net;
using System.Windows;
using System.Windows.Controls;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.Unity;
using CollectibleCardGame.Views.Frames;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class LogInFramePageShellViewModel : BaseViewModel
    {
        private readonly ConnectionErrorFramePage _connectionErrorFramePage;
        private readonly LogInFramePage _logInFramePage;
        private readonly ServerConnectionPage _serverConnectionPage;
        private readonly ToRegisterFramePage _toRegisterFramePage;
        private Page _currentFramePage;
        private RelayCommand _reconnectCommand;

        private RelayCommand _switchFrameCommand;

        public LogInFramePageShellViewModel(LogInFramePage logInFramePage,
            ToRegisterFramePage toRegisterFramePage, ConnectionErrorFramePage connectionErrorFramePage,
            ServerConnectionPage serverConnectionPage)
        {
            _logInFramePage = logInFramePage;
            _toRegisterFramePage = toRegisterFramePage;
            _connectionErrorFramePage = connectionErrorFramePage;
            CurrentFramePage = _logInFramePage;
            _serverConnectionPage = serverConnectionPage;
            //todo : пeределывай
            _logInFramePage.ToRegisterButton.Click += ToRegisterButton_Click;
            _toRegisterFramePage.GoBackButton.Click += GoBackButton_Click;
            _connectionErrorFramePage.ReconnectButton.Click += ReconnectButton_Click;
        }

        public Page CurrentFramePage
        {
            get => _currentFramePage;
            set
            {
                _currentFramePage = value;
                NotifyPropertyChanged(nameof(CurrentFramePage));
            }
        }

        public RelayCommand SwitchFrameCommand => _switchFrameCommand ?? (
                                                      _switchFrameCommand = new RelayCommand(obj =>
                                                      {
                                                          if (_currentFramePage is LogInFramePage)
                                                              _currentFramePage = _toRegisterFramePage;
                                                          if (_currentFramePage is ToRegisterFramePage)
                                                              _currentFramePage = _logInFramePage;
                                                      }));

        public RelayCommand ReconnectCommand => _reconnectCommand ?? (
                                                    _reconnectCommand = new RelayCommand(o =>
                                                    {
                                                        UnityKernel.Get<GlobalAppStateController>()
                                                            .TryConnect(IPAddress.Parse("127.0.0.1"), 8800);
                                                    }));

        private void ReconnectButton_Click(object sender, RoutedEventArgs e)
        {
            //UnityKernel.Get<GlobalAppStateController>().OnStartup("127.0.0.1",8800);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentFramePage = _logInFramePage;
        }

        private void ToRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentFramePage = _toRegisterFramePage;
        }

        public void SetLogInPage()
        {
            CurrentFramePage = _logInFramePage;
        }

        public void SetRegisterPage()
        {
            CurrentFramePage = _toRegisterFramePage;
        }

        public void SetErrorPage()
        {
            CurrentFramePage = _connectionErrorFramePage;
        }

        public void SetConnectionPage()
        {
            CurrentFramePage = _serverConnectionPage;
        }
    }
}