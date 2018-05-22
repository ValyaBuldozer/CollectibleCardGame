using System;
using System.Collections.Specialized;
using System.Linq;
using GameData.Models;
using GameData.Models.Observer;
using GameData.Models.Repository;

namespace GameData.Controllers.Data
{
    public class ObserverActionRepositoryController
    {
        private readonly ObserverActionRepository _repository;

        public ObserverActionRepositoryController(ObserverActionRepository repository)
        {
            _repository = repository;
            _repository.Collection.CollectionChanged += CollectionChanged;
        }

        public event EventHandler<ObserverActionAddedEventArgs> ItemAdded;

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                ItemAdded?.Invoke(this, new ObserverActionAddedEventArgs((ObserverAction) e.NewItems[0]));
        }

        public void Add(ObserverAction item)
        {
            if (item != null)
                _repository.Collection.Add(item);
        }

        public void Remove(ObserverAction item)
        {
            if (item != null)
                _repository.Collection.Remove(item);
        }

        public ObserverAction Dequeue()
        {
            if (_repository.Collection.Count == 0)
                return null;

            var item = _repository.Collection.First();
            _repository.Collection.Remove(item);
            return item;
        }
    }
}