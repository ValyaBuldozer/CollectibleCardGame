using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.Unity;
using CollectibleCardGame.Views.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class LogInFramePageShellViewModel : BaseViewModel
    {
        private Page _currentFramePage;
        private readonly LogInFramePage _logInFramePage;
        private readonly ToRegisterFramePage _toRegisterFramePage;
        private readonly ConnectionErrorFramePage _connectionErrorFramePage;

        private RelayCommand _switchFrameCommand;
        private RelayCommand _reconnectCommand;

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
                                UnityKernel.Get<GlobalAppStateController>().TryConnect("127.0.0.1",8800);
                            }));

        public LogInFramePageShellViewModel(LogInFramePage logInFramePage,
            ToRegisterFramePage toRegisterFramePage,ConnectionErrorFramePage connectionErrorFramePage)
        {
            _logInFramePage = logInFramePage;
            _toRegisterFramePage = toRegisterFramePage;
            _connectionErrorFramePage = connectionErrorFramePage;
            CurrentFramePage = _logInFramePage;
            //todo : пeределывай
            _logInFramePage.ToRegisterButton.Click += ToRegisterButton_Click;
            _toRegisterFramePage.GoBackButton.Click += GoBackButton_Click;
            _connectionErrorFramePage.ReconnectButton.Click += ReconnectButton_Click;
        }

        private void ReconnectButton_Click(object sender, RoutedEventArgs e)
        {
            //todo : ИЗМЕНЕНИЕ АДРЕСА
            UnityKernel.Get<GlobalAppStateController>().OnStartup("127.0.0.1",8800);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentFramePage = _logInFramePage;
        }

        private void ToRegisterButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
    }
}
