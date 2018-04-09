using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network.Models;

namespace Server.Repositories
{
    public class AwaitingClientsQueue
    {
        public Queue<Client> Clients { set; get; }

        public AwaitingClientsQueue()
        {
            Clients = new Queue<Client>();
        }
    }
}
