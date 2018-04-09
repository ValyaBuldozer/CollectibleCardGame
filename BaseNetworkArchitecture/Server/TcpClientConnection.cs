using System.Net.Sockets;
using BaseNetworkArchitecture.Common;

namespace BaseNetworkArchitecture.Server
{
    public class TcpClientConnection : IClientConnection
    {
        public TcpClientConnection(TcpClient tcpClient,ILogger logger = null)
        {
            IsInSystem = false;
            Communicator = new TcpCommunicator(tcpClient){Logger = logger};
        }

        public string IdentificatorTocken { set; get; }

        public INetworkCommunicator Communicator { set; get; }

        public bool IsInSystem { set; get; }
    }
}