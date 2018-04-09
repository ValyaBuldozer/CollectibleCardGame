using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Common.Messages
{
    public class MessageEventArgs : EventArgs
    {
        public NetworkMessage NetworkMessage { set; get; }
        public string Sender { set; get; }
    }
}
