using Server.Network.Models;
using Server.Repositories;

namespace Server.Controllers.Repository
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
            if(_repository.Collection.Contains(client))
                _repository.Collection.Remove(client);
        }
    }
}
