using System;
using System.Net;
using System.Net.Sockets;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;

namespace CollectibleCardGame.Logic.Controllers
{
    /// <summary>
    ///     Класс - контроллер, отвечает за старт, закрытие, запрос на соединение с сервером, обработка разрыва соединения с
    ///     сервером.
    /// </summary>
    public class GlobalAppStateController : IGlobalController
    {
        private readonly INetworkController _connectionController;
        private readonly LogInFramePageShellViewModel _framePageShellViewModel;
        private readonly ILogger _logger;
        private readonly MainWindowViewModel _mainWindowViewModel;

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
            //_mainWindowViewModel.StartBusyIndicator("Подключение к серверу");
            //if (!TryConnect(adress, port))
            //{
            //    _mainWindowViewModel.StopBusyIndicator();
            //    _framePageShellViewModel.SetErrorPage();
            //    return;
            //}

            _mainWindowViewModel.SetLogInFrame();
            _framePageShellViewModel.SetConnectionPage();
        }

        public void OnConnectionLost()
        {
            throw new NotImplementedException();
        }

        public void OnClose()
        {
            throw new NotImplementedException();
        }

        public bool TryConnect(IPAddress address, int port)
        {
            try
            {
                _mainWindowViewModel.StartBusyIndicator("Подключение к серверу");
                _connectionController.Connect(address, port);
                _mainWindowViewModel.StopBusyIndicator();
                _framePageShellViewModel.SetLogInPage();
                return true;
            }
            catch (SocketException)
            {
                _logger.LogAndPrint("Ошибка при попытке подключения");
                _mainWindowViewModel.StopBusyIndicator();
                return false;
            }
        }
    }
}