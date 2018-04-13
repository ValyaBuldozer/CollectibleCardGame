using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CollectibleCardGame.Services;
using CollectibleCardGame.Views.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class LogInFramePageShellViewModel : BaseViewModel
    {
        private Page _currentFramePage;
        private LogInFramePage _logInFramePage;
        private ToRegisterFramePage _toRegisterFramePage;
        private ConnectionErrorFramePage _connectionErrorFramePage;

        private RelayCommand _switchFrameCommand;

        public Page CurrentFramePage
        {
            get => _currentFramePage;
            set
            {
                _currentFramePage = value;
                NotifyPropertyChanged(nameof(CurrentFramePage));
            }
        }

        public RelayCommand SwitchFrameCommand
        {
            get => _switchFrameCommand ?? (_switchFrameCommand = new RelayCommand(obj =>
            {
                if (_currentFramePage is LogInFramePage)
                    _currentFramePage = _toRegisterFramePage;
                if (_currentFramePage is ToRegisterFramePage)
                    _currentFramePage = _logInFramePage;
            }));
        }

        public LogInFramePageShellViewModel(LogInFramePage logInFramePage,
            ToRegisterFramePage toRegisterFramePage,ConnectionErrorFramePage connectionErrorFramePage)
        {
            _logInFramePage = logInFramePage;
            _toRegisterFramePage = toRegisterFramePage;
            _connectionErrorFramePage = connectionErrorFramePage;
            CurrentFramePage = _logInFramePage;
            //todo : говнокод пределывай
            _logInFramePage.ToRegisterButton.Click += ToRegisterButton_Click;
            _toRegisterFramePage.GoBackButton.Click += GoBackButton_Click;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentFramePage = _toRegisterFramePage;
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
