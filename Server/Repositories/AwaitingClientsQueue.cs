using System.Collections.Generic;
using Server.Network.Models;

namespace Server.Repositories
{
    public class AwaitingClientsQueue
    {
        public AwaitingClientsQueue()
        {
            Clients = new Queue<Client>();
        }

        public Queue<Client> Clients { set; get; }
    }
}