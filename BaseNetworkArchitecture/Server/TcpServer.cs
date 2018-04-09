using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BaseNetworkArchitecture.Common;
using Unity.Attributes;

namespace BaseNetworkArchitecture.Server
{
    public class TcpServer : IServer
    {
        private const string LOCALHOST_IP = "127.0.0.1";
        private readonly int PORT;

        [Dependency]
        public ILogger Logger { set; get; }

        public TcpServer()
        {
            PORT = 8800;
            TcpListener = new TcpListener(IPAddress.Parse(LOCALHOST_IP), PORT);
            Clients = new List<IClientConnection>();
        }

        //public TcpServer(int port)
        //{
        //    PORT = port;
        //    TcpListener = new TcpListener(IPAddress.Parse(LOCALHOST_IP), PORT);
        //    Clients = new List<IClientConnection>();
        //}

        //public TcpServer(IPAddress ipAddress, int port)
        //{
        //    PORT = port;
        //    TcpListener = new TcpListener(ipAddress, PORT);
        //    Clients = new List<IClientConnection>();
        //}

        public Thread GetListenerThread { get; private set; }

        public TcpListener TcpListener { get; }

        public ICollection<IClientConnection> Clients { set; get; }

        public void Start()
        {
            GetListenerThread = new Thread(AcceptClients);
            GetListenerThread.Start(this);
        }

        public void Stop()
        {
            foreach (var iClient in Clients)
                try
                {
                    iClient.Communicator.Disconnect();
                }
                catch (SocketException e)
                {
                    Logger?.Log(e);
                }

            GetListenerThread.Abort();

            TcpListener.Stop();
            Logger?.LogAndPrint("Server stoped");
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        private static void AcceptClients(object serverObj)
        {
            var server = (TcpServer) serverObj;
            server.Logger?.LogAndPrint("Server is startinng...");
            try
            {
                server.TcpListener.Start();
                while (true)
                {
                    var result =
                        server.TcpListener.BeginAcceptTcpClient(
                            server.DoAcceptTcpClientCallback, server.TcpListener);
                    result.AsyncWaitHandle.WaitOne();
                }
            }
            catch (SocketException e)
            {
                server.Logger?.Log(e);
                server.TcpListener.Stop();
            }
        }

        private static void RunClientConnectedEvent(object sender, ClientConnectedEventArgs e)
        {
            ((TcpServer) sender).ClientConnected?.Invoke(sender, e);
        }

        public void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            var listener = (TcpListener) ar.AsyncState;

            var tcpClient = listener.EndAcceptTcpClient(ar);

            var client = new TcpClientConnection(tcpClient,Logger);
            Clients.Add(client);
            Logger?.LogAndPrint("Client connected");
            
            RunClientConnectedEvent(this, new ClientConnectedEventArgs {ClientConnection = client});
            ((TcpCommunicator) client.Communicator).StartReadMessages();

        }
    }

    public class ClientConnectedEventArgs : EventArgs
    {
        public IClientConnection ClientConnection { set; get; }
    }
}