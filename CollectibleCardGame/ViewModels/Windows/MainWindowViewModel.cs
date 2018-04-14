using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;
using Unity.Attributes;

namespace CollectibleCardGame.ViewModels.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Page _framePage;
        private bool _isBusy;
        private string _busyMessage;
        private LogInFramePageShell _logInFramePageShell;
        private MainMenuFramePage _mainMenuFramePage;

        public Page FramePage
        {
            get => _framePage;
            set
            {
                _framePage = value;
                NotifyPropertyChanged(nameof(FramePage));
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                NotifyPropertyChanged(nameof(IsBusy));
            }
        }

        public string BusyMessage
        {
            get => _busyMessage;
            set
            {
                _busyMessage = value;
                NotifyPropertyChanged(nameof(BusyMessage));
            }
        }

        public MainWindowViewModel(LogInFramePageShell logInFramePageShell)
        {
            _logInFramePageShell = logInFramePageShell;
            //todo : переделать под внедрение зависимостей
            _mainMenuFramePage = new MainMenuFramePage();
            _framePage = _logInFramePageShell;
        }

        public void SetLogInFrame()
        {
            FramePage = _logInFramePageShell;
        }

        public void SetMainMenuFrame()
        {
            FramePage = _mainMenuFramePage;
        }
    }
}
