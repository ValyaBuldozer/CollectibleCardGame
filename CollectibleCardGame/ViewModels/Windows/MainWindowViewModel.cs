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
        private string _title;
        private readonly LogInFramePageShell _logInFramePageShell;
        private readonly MainMenuFramePage _mainMenuFramePage;
        private readonly GameEngineFramePage _gameEngineFramePage;

        public Page FramePage
        {
            get => _framePage;
            set
            {
                _framePage = value;
                NotifyPropertyChanged(nameof(FramePage));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
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

        public MainWindowViewModel(LogInFramePageShell logInFramePageShell,
            MainMenuFramePage mainMenuFramePage,GameEngineFramePage gameEngineFramePage)
        {
            _logInFramePageShell = logInFramePageShell;
            _mainMenuFramePage = mainMenuFramePage;
            _gameEngineFramePage = gameEngineFramePage;
            _framePage = _logInFramePageShell;

            _title = "Collectible card game";
        }

        public void SetLogInFrame()
        {
            FramePage = _logInFramePageShell;
        }

        public void SetMainMenuFrame()
        {
            FramePage = _mainMenuFramePage;
        }

        public void SetGameEngineFrame()
        {
            FramePage = _gameEngineFramePage;
        }

        public void StartBusyIndicator(string busyMessage)
        {
            BusyMessage = busyMessage;
            IsBusy = true;
        }

        public void StopBusyIndicator()
        {
            IsBusy = false;
        }
    }
}
