using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Common
{
    public class BreakConnectionEventArgs : EventArgs
    {
        public string DisconnectReason { set; get; }
    }
}
