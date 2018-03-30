using System.Net.Sockets;
using BaseNetworkArchitecture.Common;

namespace BaseNetworkArchitecture.Server
{
    internal class Client : IClient
    {
        public Client(TcpClient tcpClient)
        {
            IsInSystem = false;
            Communicator = new TcpCommunicator(tcpClient);
        }

        public string IdentificatorTocken { set; get; }

        public INetworkCommunicator Communicator { set; get; }

        public bool IsInSystem { set; get; }
    }
}