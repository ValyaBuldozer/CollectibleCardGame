using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Services;
using GameData.Enums;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class GoGameFramePageViewModel : BaseViewModel
    {
        private ILogger _logger;

        private bool _isBusy;
        private string _busyMessage;
        private RelayCommand _gameRequestCommand;

        private bool _isNorthChecked;
        private bool _isSouthChecked;
        private bool _isDarkChecked;

        private Fraction _fraction;

        public bool IsNorthChecked
        {
            get => _isNorthChecked;
            set
            {
                _isNorthChecked = value;
                NotifyPropertyChanged(nameof(IsNorthChecked));

                if (value) _fraction = Fraction.North;
            }
        }

        public bool IsSouthChecked
        {
            get => _isSouthChecked;
            set
            {
                _isSouthChecked = value;
                NotifyPropertyChanged(nameof(IsSouthChecked));

                if (value) _fraction = Fraction.South;
            }
        }

        public bool IsDarkChecked
        {
            get => _isDarkChecked;
            set
            {
                _isDarkChecked = value;
                NotifyPropertyChanged(nameof(IsDarkChecked));

                if (value) _fraction = Fraction.Dark;
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

        public RelayCommand GameRequestCommand => _gameRequestCommand ?? (
                      _gameRequestCommand = new RelayCommand(o =>
                      {
                          if (_fraction == Fraction.Common)
                          {
                              _logger.LogAndPrint("Выберите фракцию");
                              return;
                          }

                          GameRequest?.Invoke(this, new GameRequestEventArgs(_fraction));
                      }));

        public event EventHandler<GameRequestEventArgs> GameRequest;

        public GoGameFramePageViewModel(ILogger logger)
        {
            _logger = logger;
            _fraction = Fraction.Common;
        }

        public void StartBusyIndicator(string message)
        {
            BusyMessage = message;
            IsBusy = true;
        }

        public void StopBusyIndicator()
        {
            IsBusy = false;
        }
    }
}
