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

        [Dependency]
        public LogInFramePage LogInFramePage { set; get; }

        [Dependency]
        public ToRegisterFramePage ToRegisterFramePage { set; get; }

        [Dependency]
        public ConnectionErrorFramePage ErrorFramePage { set; get; }

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
                    _currentFramePage = ToRegisterFramePage;
                if (_currentFramePage is ToRegisterFramePage)
                    _currentFramePage = LogInFramePage;
            }));
        }

        public LogInFramePageShellViewModel()
        {
            LogInFramePage = new LogInFramePage();
            ToRegisterFramePage = new ToRegisterFramePage();
            _currentFramePage = LogInFramePage;
            LogInFramePage.ToRegisterButton.Click += ToRegisterButton_Click;
            ToRegisterFramePage.GoBackButton.Click += GoBackButton_Click;

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentFramePage = LogInFramePage;
        }

        private void ToRegisterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentFramePage = ToRegisterFramePage;
        }

    }
}
