using System;
using System.Net;
using System.Net.Sockets;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Network;
using GameData.Network.Messages;
using Unity.Attributes;

namespace CollectibleCardGame.Network.Controllers
{
    public class NetworkConnectionController : INetworkController
    {
        private INetworkCommunicator _serverCommunicator;

        [Dependency]
        public INetworkCommunicator ServerCommunicator
        {
            set
            {
                if (_serverCommunicator != null)
                {
                    _serverCommunicator.MessageRecievedEvent -= OnMessageRecieved;
                    _serverCommunicator.BreakConnectionEvent -= OnBreakConnection;
                    _serverCommunicator.Disconnect();
                }

                _serverCommunicator = value;
                _serverCommunicator.MessageRecievedEvent += OnMessageRecieved;
                _serverCommunicator.BreakConnectionEvent += OnBreakConnection;
            }
            get => _serverCommunicator;
        }

        private readonly IMessageConverter _converter;
        private readonly MainWindowViewModel _mainViewModel;
        private readonly ILogger _logger;
        private readonly LogInFramePageShellViewModel _logInViewModel;

        public NetworkConnectionController(IMessageConverter converter,MainWindowViewModel mainViewModel,
            ILogger logger,LogInFramePageShellViewModel loginViewModel)
        {
            _mainViewModel = mainViewModel;
            _converter = converter;
            _logger = logger;
            _logInViewModel = loginViewModel;
        }

        public void Connect(IPAddress ipAddress, int port)
        {
            if(!ServerCommunicator.Connect(ipAddress, port))
                throw new SocketException();

            ServerCommunicator.StartReadMessages();
        }

        public void Disconnect()
        {
            if(!ServerCommunicator.Disconnect())
                throw new SocketException();
        }

        public void SendMessage(MessageBase message)
        {
            ServerCommunicator.SendMessage(_converter.SerializeMessage(message));
        }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            var deserializedMessage = _converter.DeserializeMessage(e.NetworkMessage);

            deserializedMessage?.HandleMessage(sender);
        }

        public void OnBreakConnection(object sender, BreakConnectionEventArgs e)
        {
            _logger.LogAndPrint("Соединение с сервером разорвано");
            _mainViewModel.SetLogInFrame();
            _logInViewModel.SetErrorPage();
        }
    }
}
