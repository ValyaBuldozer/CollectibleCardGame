using System.Collections.Generic;
using Server.Network.Models;

namespace Server.Repositories
{
    public class ConnectedClientsRepository
    {
        public ConnectedClientsRepository()
        {
            Collection = new List<Client>();
        }

        public List<Client> Collection { get; }
    }
}