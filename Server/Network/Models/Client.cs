using System;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using BaseNetworkArchitecture.Server;
using Server.Models;
using Server.Network.Controllers;
using Server.Unity;

namespace Server.Network.Models
{
    public class Client
    {
        public IClientConnection ClientConnection { set; get; }

        public User User { set; get; }

        public UserInfo UserInfo => User.UserInfo;

        public GameLobby CurrentLobby { set; get; }

        public ClientController ClientController { set; get; }

        public event EventHandler<MessageEventArgs> MessageRecived;

        public event EventHandler<BreakConnectionEventArgs> BreakConnection; 

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            MessageRecived?.Invoke(this,e);
        }

        private void OnBreakConnection(object sender, BreakConnectionEventArgs e)
        {
            BreakConnection?.Invoke(this, e);
        }

        public Client(IClientConnection clientConnection)
        {
            ClientConnection = clientConnection;
            ClientConnection.Communicator.MessageRecievedEvent += OnMessageRecieved;
            ClientConnection.Communicator.BreakConnectionEvent += OnBreakConnection;
            ClientController = UnityKernel.Get<ClientController>();
            ClientController.Client = this;

            MessageRecived += ClientController.OnMessageRecieved;
            BreakConnection += ClientController.OnBreakConnection;
        }
    }
}
