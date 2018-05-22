using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Exceptions;
using GameData.Models.Cards;
using GameData.Models.Repository;

namespace GameData.Controllers.Data
{
    public class CardRepositroryController : IDataRepositoryController<Card>
    {
        private readonly CardRepository _repository;

        public CardRepositroryController(CardRepository repository)
        {
            _repository = repository;
        }

        public Card GetById(int id)
        {
            var item = _repository.Collection.FirstOrDefault(c => c.ID == id);

            return item?.DeepCopy();
        }

        public IEnumerable<Card> GetById(IEnumerable<int> idCollection)
        {
            return idCollection?.Select(GetById).ToList();
        }

        public void Add(Card item)
        {
            if(_repository.Collection.Exists(c=>c.ID == item.ID))
                throw new RepositoryItemAlreadyExistsExcepction("Item with this id already declared");

            _repository.Collection.Add(item);
        }

        public void AddNewItem(Card item)
        {
            if (_repository.Collection.Count != 0)
                item.ID = _repository.Collection.Max(c => c.ID) + 1;
            else
                item.ID = 0;

            _repository.Collection.Add(item);
        }

        public void Remove(Card item)
        {
            _repository.Collection.Remove(item);
        }

        public void Remove(int id)
        {
            var item = _repository.Collection.FirstOrDefault(c => c.ID == id);

            if (item != null)
                _repository.Collection.Remove(item);
        }

        public void Edit(Card item, int id)
        {
            Remove(id);
            item.ID = id;
            Add(item);
        }

        public void ClearRepository()
        {
            _repository.Collection.Clear();
        }

        public IEnumerable<Card> GetCollection()
        {
            return _repository.Collection.ToList();
        }
    }
}
