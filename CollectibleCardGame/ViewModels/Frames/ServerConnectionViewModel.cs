﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.Unity;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class ServerConnectionViewModel : BaseViewModel
    {
        private string _ipAdress;
        private int _port;
        private RelayCommand _connectCommand;

        private readonly ILogger _logger;

        public string IpAdress
        {
            get => _ipAdress;
            set
            {
                _ipAdress = value;
                NotifyPropertyChanged(nameof(IpAdress));
            }
        }

        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                NotifyPropertyChanged(nameof(Port));
            }
        }

        public RelayCommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand(o =>
        {
            if (string.IsNullOrEmpty(_ipAdress))
                return;

            if(_port < 0 || _port > 65535)
                _logger.LogAndPrint("Invalid port");

            if (IPAddress.TryParse(IpAdress, out var ipAddress))
                UnityKernel.Get<IGlobalController>().TryConnect(ipAddress, _port);
            else
                _logger.LogAndPrint("Invalid IP adress");
        }));

        public ServerConnectionViewModel(ILogger logger)
        {
            _logger = logger;
            _ipAdress = "127.0.0.1";
            _port = 8800;
        }
    }
}
