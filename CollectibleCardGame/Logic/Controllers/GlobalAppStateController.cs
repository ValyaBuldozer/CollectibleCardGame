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
        private readonly INetworkController _connectionController;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly LogInFramePageShellViewModel _framePageShellViewModel;
        private readonly ILogger _logger;

        public GlobalAppStateController(INetworkController networkController,
            MainWindowViewModel mainWindowViewModel, LogInFramePageShellViewModel framePageShellViewModel,
            ILogger logger)
        {
            _connectionController = networkController;
            _mainWindowViewModel = mainWindowViewModel;
            _framePageShellViewModel = framePageShellViewModel;
            _logger = logger;
        }

        public void OnStartup()
        {
            _mainWindowViewModel.StartBusyIndicator("Подключение к серверу");
            if (!TryConnect())
            {
                _mainWindowViewModel.StopBusyIndicator();
                _framePageShellViewModel.SetErrorPage();
                return;
            }

            _mainWindowViewModel.StopBusyIndicator();
            _mainWindowViewModel.SetLogInFrame();
            _framePageShellViewModel.SetLogInPage();
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
                _connectionController.Connect(IPAddress.Parse("127.0.0.1"), 8800);
                return true;
            }
            catch (SocketException)
            {
               _logger.LogAndPrint("Ошибка при попытке подключения");
                return false;
            }
        }
    }
}
