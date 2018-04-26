using System.Collections.Generic;
using Server.Network.Models;
using Server.Repositories;

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
    }
}
