using System;
using System.Net;
using BaseNetworkArchitecture.Common.Messages;

namespace BaseNetworkArchitecture.Common
{
    public interface INetworkCommunicator
    {
        bool IsConnected { get; }
        bool SendMessage(NetworkMessage networkMessage);
        void StartReadMessages();
        NetworkMessage ReadMessage();
        bool Connect();
        bool Connect(IPAddress ipAddress, int port);
        bool Disconnect();
        event EventHandler<MessageEventArgs> MessageRecievedEvent;
        event EventHandler<BreakConnectionEventArgs> BreakConnectionEvent;
    }
}