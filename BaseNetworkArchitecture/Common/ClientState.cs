using System.Net.Sockets;

namespace BaseNetworkArchitecture.Common
{
    internal class ClientState
    {
        // Object to contain client state, including the client socket
        // and the receive buffer

        private const int BUFSIZE = 32; // Size of receive buffer

        public ClientState(TcpClient tcpClient, int buffLength)
        {
            TcpClient = tcpClient;
            RcvBuffer = new byte[buffLength]; // Receive buffer
        }

        public byte[] RcvBuffer { get; set; }

        public TcpClient TcpClient { get; set; }
    }
}