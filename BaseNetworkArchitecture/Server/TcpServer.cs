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

        public Thread GetListenerThread { get; private set; }

        public ICollection<IClient> Clients { set; get; }

        public TcpListener TcpListener { get; private set; }

        public void Start()
        {
            GetListenerThread = new Thread(new ParameterizedThreadStart(AcceptClients));
            GetListenerThread.Start(this);
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
            GetListenerThread.Abort();

            TcpListener.Stop();
            Console.WriteLine("Server stoped");
        }

        private static void AcceptClients(object serverObj)
        {
            TcpServer server = (TcpServer) serverObj;
            Console.WriteLine("Server is startinng...");
            try
            {
                server.TcpListener.Start();
                while (true)
                {
                    var result =
                        server.TcpListener.BeginAcceptTcpClient(
                            new AsyncCallback(server.DoAcceptTcpClientCallback), server.TcpListener);
                    result.AsyncWaitHandle.WaitOne();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                server.TcpListener.Stop();
            }
        }

        public TcpServer(int port)
        {
            PORT = port;
            TcpListener = new TcpListener(IPAddress.Parse(LOCALHOST_IP), PORT);
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