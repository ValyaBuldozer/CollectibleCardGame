using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Services;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class GoGameFramePageViewModel : BaseViewModel
    {
        private bool _isBusy;
        private string _busyMessage;
        private RelayCommand _gameRequestCommand;

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
                          GameRequest?.Invoke(this, new GameRequestEventArgs("test"));
                      }));

        public event EventHandler<GameRequestEventArgs> GameRequest;

        public GoGameFramePageViewModel()
        {

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
