using System;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;

namespace BaseNetworkArchitecture.Server
{
    public interface IClientConnection
    {
        INetworkCommunicator Communicator { set; get; }
        string IdentificatorTocken { set; get; }
        bool IsInSystem { set; get; }
    }
}