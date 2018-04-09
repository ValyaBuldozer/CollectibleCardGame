using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network.Models;
using Server.Repositories;

namespace Server.Controllers
{
    public class ConnectedClientsRepositoryController
    {
        private readonly ConnectedClientsRepository _repository;

        public ConnectedClientsRepositoryController(ConnectedClientsRepository repository)
        {
            _repository = repository;
        }

        public void Add(Client client)
        {
            _repository.Collection.Add(client);
        }

        public void Remove(Client client)
        {
            _repository.Collection.Remove(client);
        }
    }
}
