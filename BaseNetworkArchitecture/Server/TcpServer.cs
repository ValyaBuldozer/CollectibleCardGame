using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;

namespace BaseNetworkArchitecture.Server
{
    public class TcpServer : IServer
    {
        private const string LOCALHOST_IP = "127.0.0.1";
        private readonly int PORT;

        private readonly TcpListener _tcpListener;

        public ICollection<IClient> Clients { set; get; }

        public void Start()
        {
            Console.WriteLine("Server is startinng...");
            try
            {
                _tcpListener.Start();
                while (true)
                {
                    var result =
                        _tcpListener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), _tcpListener);
                    result.AsyncWaitHandle.WaitOne();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                _tcpListener.Stop();
            }
        }

        public void Stop()
        {
            foreach (var iClient in Clients)
            {
                try
                {
                    iClient.Communicator.Disconnect();
                }
                catch (SocketException e) { }
            }

            _tcpListener.Stop();
            Console.WriteLine("Server stoped");
        }

        public TcpServer(int port)
        {
            PORT = port;
            _tcpListener = new TcpListener(IPAddress.Parse(LOCALHOST_IP), PORT);
            Clients = new List<IClient>();
        }

        public void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;

            TcpClient tcpClient = listener.EndAcceptTcpClient(ar);

            Client client = new Client(tcpClient);
            Clients.Add(client);
            Console.WriteLine("Client connected");
            ((TcpCommunicator)client.Communicator).StartReadMessages();
        }
    }
}