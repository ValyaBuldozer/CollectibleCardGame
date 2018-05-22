using System;

namespace BaseNetworkArchitecture.Common
{
    public class BreakConnectionEventArgs : EventArgs
    {
        public string DisconnectReason { set; get; }
    }
}