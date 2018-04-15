using System;
using System.Net;
using System.Net.Sockets;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Unity;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using Unity.Attributes;

namespace CollectibleCardGame.Logic.Controllers
{
    /// <summary>
    /// Класс - контроллер, отвечает за старт, закрытие, запрос на соединение с сервером, обработка разрыва соединения с сервером.
    /// </summary>
    public class GlobalAppStateController : IGlobalController
    {
        [Dependency]
        public INetworkController ConnectionController { set; get; }

        [Dependency]
        public MainWindowViewModel MainWindowViewModel { set; get; }

        [Dependency]
        public LogInFramePageShellViewModel FramePageShellViewModel { set; get; }

        public void OnStartup()
        {
            //todo : говнокод переделать
            MainWindowViewModel.BusyMessage = "Подключение к серверу";
            MainWindowViewModel.IsBusy = true;
            if (!TryConnect())
            {
                MainWindowViewModel.IsBusy = false;
                FramePageShellViewModel.SetErrorPage();
                return;
            }

            MainWindowViewModel.IsBusy = false;
            MainWindowViewModel.SetLogInFrame();
            FramePageShellViewModel.SetLogInPage();
        }

        public void OnConnectionLost()
        {
            throw new NotImplementedException();
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
