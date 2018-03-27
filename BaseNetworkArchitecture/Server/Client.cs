using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;

namespace BaseNetworkArchitecture.Server
{
    class Client : IClient
    {
        public string IdentificatorTocken { set; get; }

        public INetworkCommunicator Communicator { set; get; }

        public bool IsInSystem { set; get; }

        public Client(TcpClient tcpClient)
        {
            IsInSystem = false;
            Communicator = new TcpCommunicator(tcpClient);
        }
    }
}