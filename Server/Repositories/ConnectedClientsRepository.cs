using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network.Models;

namespace Server.Repositories
{
    public class ConnectedClientsRepository
    {
        public List<Client> Collection { private set; get; }

        public ConnectedClientsRepository()
        {
            Collection = new List<Client>();
        }
    }
}
