using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.Unity;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class ConnectionErrorViewModel : BaseViewModel
    {
        private RelayCommand _reconnectRelayCommand;
        private bool _isBusy;
        private string _busyMessage;

        public RelayCommand ReconnectRelayCommand
        {
            get => _reconnectRelayCommand ?? (_reconnectRelayCommand = new RelayCommand(obj =>
                       {
                           BusyMessage = "Подключаемся...";
                           IsBusy = true;
                           if (UnityKernel.Get<GlobalAppStateController>().TryConnect())
                               //todo : Обращение от VM к VM
                           {
                               IsBusy = false;
                               UnityKernel.Get<LogInFramePageShellViewModel>().CurrentFramePage = 
                                   UnityKernel.Get<LogInFramePageShellViewModel>().LogInFramePage;
                               return;
                           }

                           IsBusy = false;
                           UnityKernel.Get<ILogger>().Print("Error");
                       }));
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
    }
}
