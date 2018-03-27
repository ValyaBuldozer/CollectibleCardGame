using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Common
{
    public interface INetworkCommunicator
    {
        bool SendMessage(Message message);
        Message ReadMessage();
        bool Connect();
        bool Disconnect();
    }
}