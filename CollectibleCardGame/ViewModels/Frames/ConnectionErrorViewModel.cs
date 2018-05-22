using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.Unity;

namespace CollectibleCardGame.ViewModels.Frames
{
    //todo : obsolete
    public class ConnectionErrorViewModel : BaseViewModel
    {
        private string _busyMessage;
        private bool _isBusy;
        private RelayCommand _reconnectRelayCommand;

        public RelayCommand ReconnectRelayCommand =>
            _reconnectRelayCommand ?? (_reconnectRelayCommand = new RelayCommand(obj =>
            {
                BusyMessage = "Подключаемся...";
                IsBusy = true;
                if (UnityKernel.Get<IGlobalController>().TryConnect(null, 8800))
                    //todo : Обращение от VM к VM
                {
                    IsBusy = false;
                    UnityKernel.Get<LogInFramePageShellViewModel>().SetLogInPage();
                    return;
                }

                IsBusy = false;
                UnityKernel.Get<ILogger>().Print("Error");
            }));

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
    }
}