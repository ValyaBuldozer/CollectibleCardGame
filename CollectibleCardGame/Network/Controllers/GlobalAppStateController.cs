using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Controllers;
using CollectibleCardGame.Unity;
using CollectibleCardGame.ViewModels.Windows;
using Unity.Attributes;

namespace CollectibleCardGame.Network.Controllers
{
    /// <summary>
    /// Класс - контроллер, отвечает за старт, закрытие, запрос на соединение с сервером, обработка разрыва соединения с сервером.
    /// </summary>
    public class GlobalAppStateController
    {
        [Dependency]
        public NetworkConnectionController ConnectionController { set; get; }

        public void OnStartup()
        {
            //UnityKernel.InitializeKernel();
            var mainViewModel = UnityKernel.Get<MainWindowViewModel>();
            mainViewModel.BusyMessage = "Подключение к серверу";
            mainViewModel.IsBusy = true;
            if (!TryConnect())
            {
                mainViewModel.IsBusy = false;

            }

            mainViewModel.IsBusy = false;
            
        }

        public void OnConnectionLost()
        {

        }

        public void OnClose()
        {
            throw new NotImplementedException();
        }

        public bool TryConnect()
        {
            try
            {
                //todo : изменение ip и порта
                ConnectionController.Connect(IPAddress.Parse("127.0.0.1"), 8800);
                return true;
            }
            catch (SocketException)
            {
                UnityKernel.Get<ILogger>().LogAndPrint("Ошибка при попытке подключения");
                return false;
            }
        }
    }
}
