using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Exceptions;
using GameData.Models.Action;
using GameData.Models.Repository;

namespace GameData.Controllers.Data
{
    public class GameActionRepositoryController : IDataRepositoryController<GameAction>
    {
        private readonly GameActionRepository _repository;

        public GameActionRepositoryController(GameActionRepository repository)
        {
            _repository = repository;
        }

        public GameAction GetById(int id)
        {
            return _repository.Collection.FirstOrDefault(a => a.ID == id);
        }

        public void Add(GameAction item)
        {
            if(_repository.Collection.Find(a=>a.ID == item.ID) != null)
                throw new RepositoryItemAlreadyExistsExcepction();

            _repository.Collection.Add(item);
        }

        public void AddNewItem(GameAction item)
        {
            if (_repository.Collection.Count != 0)
                item.ID = _repository.Collection.Max(ga => ga.ID) + 1;
            else
                item.ID = 0;

            _repository.Collection.Add(item);
        }

        public void Remove(GameAction element)
        {
            _repository.Collection.Remove(element);
        }

        public void Remove(int id)
        {
            _repository.Collection.Remove(_repository.Collection.FirstOrDefault(a => a.ID == id));
        }

        public void Edit(GameAction element, int id)
        {
            if(element.ID != id)
                throw new InvalidOperationException();

            _repository.Collection.Remove(_repository.Collection.FirstOrDefault(a => a.ID == id));
            _repository.Collection.Add(element);
        }
    }
}
