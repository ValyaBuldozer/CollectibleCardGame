using System.Collections.Generic;
using System.Linq;
using Server.Network.Models;
using Server.Repositories;
using Unity.Interception.Utilities;

namespace Server.Controllers.Repository
{
    public class AwaitingClientsQueueController
    {
        private readonly AwaitingClientsQueue _repository;

        public AwaitingClientsQueueController(AwaitingClientsQueue clientsQueue)
        {
            _repository = clientsQueue;
        }

        public void Enqueue(Client client)
        {
            _repository.Clients.Enqueue(client);
        }

        public Client Dequeue()
        {
            return _repository.Clients.Count == 0 ? null : _repository.Clients.Dequeue();
        }

        public Queue<Client> GetClientsQueue()
        {
            return _repository.Clients;
        }

        public void Remove(Client client)
        {
            if(!_repository.Clients.Contains(client)) return;
            
            var queue = new Queue<Client>();

            while (_repository.Clients.Count != 0)
            {
                var item = _repository.Clients.Dequeue();

                if(item != client)
                    queue.Enqueue(item);
            }

            queue.ForEach(c=>_repository.Clients.Enqueue(c));
        }
    }
}
