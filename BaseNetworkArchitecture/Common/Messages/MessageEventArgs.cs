using System;

namespace BaseNetworkArchitecture.Common.Messages
{
    public class MessageEventArgs : EventArgs
    {
        public NetworkMessage NetworkMessage { set; get; }
        public string Sender { set; get; }
    }
}