using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using Server.Network.Models;

namespace Server.Network.Controllers
{
    public class ClientController
    {
        public NetworkMessageConverter MessageConverter { set; get; }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {

        }

        public void OnBreakConnection(object sender, BreakConnectionEventArgs e)
        {
            ((Client) sender).MessageRecived -= OnMessageRecieved;
            ((Client) sender).BreakConnection -= OnBreakConnection;
        }

        public ClientController(NetworkMessageConverter messageConverter)
        {
            this.MessageConverter = messageConverter;
        }
    }
}
