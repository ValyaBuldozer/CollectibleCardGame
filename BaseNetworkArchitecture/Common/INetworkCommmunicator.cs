using System;
using BaseNetworkArchitecture.Common.Messages;

namespace BaseNetworkArchitecture.Common
{
    public interface INetworkCommunicator
    {
        bool SendMessage(NetworkMessage networkMessage);
        NetworkMessage ReadMessage();
        bool Connect();
        bool Disconnect(); event EventHandler<MessageEventArgs> MessageRecievedEvent;
        event EventHandler<BreakConnectionEventArgs> BreakConnectionEvent;
    }
}