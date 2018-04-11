using System;
using System.Net;
using BaseNetworkArchitecture.Common.Messages;

namespace BaseNetworkArchitecture.Common
{
    public interface INetworkCommunicator
    {
        bool SendMessage(NetworkMessage networkMessage);
        NetworkMessage ReadMessage();
        bool Connect();
        bool Connect(IPAddress ipAddress, int port);
        bool Disconnect();
        event EventHandler<MessageEventArgs> MessageRecievedEvent;
        event EventHandler<BreakConnectionEventArgs> BreakConnectionEvent;
    }
}