using System;
using System.Collections.Generic;
using System.Net;

namespace BaseNetworkArchitecture.Server
{
    public interface IServer
    {
        ICollection<IClientConnection> Clients { set; get; }
        void Start(IPAddress ipAddress,int port);
        void Stop();
        event EventHandler<ClientConnectedEventArgs> ClientConnected;
    }
}