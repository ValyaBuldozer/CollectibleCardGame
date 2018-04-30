using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Exceptions;
using GameData.Models;
using GameData.Models.Repository;

namespace GameData.Controllers.Data
{
    public class EntityRepositoryController : IDataRepositoryController<Entity>
    {
        private readonly EntityRepository _repository;

        public EntityRepositoryController(EntityRepository repository)
        {
            _repository = repository;
        }

        public Entity GetById(int id)
        {
            return _repository.Collection.FirstOrDefault(e => e.EntityId == id);
        }

        public void Add(Entity item)
        {
            if (_repository.Collection.Exists(e => e.EntityId == item.EntityId))
                throw new RepositoryItemAlreadyExistsExcepction("Item with this id is already declared");

            _repository.Collection.Add(item);
        }

        public void AddNewItem(ref Entity item)
        {
            if (_repository.Collection.Count != 0)
                item.EntityId = _repository.Collection.Max(e => e.EntityId) + 1;
            else
                item.EntityId = 0;

            _repository.Collection.Add(item);
        }

        public void Remove(Entity item)
        {
            _repository.Collection.Remove(item);
        }

        public void Remove(int id)
        {
            var item = _repository.Collection.FirstOrDefault(e => e.EntityId == id);

            if (item != null)
                _repository.Collection.Remove(item);
        }

        public void Edit(Entity item, int id)
        {
            Remove(id);
            item.EntityId = id;
            _repository.Collection.Add(item);
        }
    }
}
