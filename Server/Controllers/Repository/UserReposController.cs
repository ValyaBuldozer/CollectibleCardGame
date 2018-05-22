using System;
using System.Collections.Generic;
using System.Linq;
using Server.Models;
using Server.Repositories;

namespace Server.Controllers.Repository
{
    public class UserReposController
    {
        private readonly UserRepository _repository;

        public IEnumerable<User> GetEnumerable => _repository.IsDatabaseConnected ?
            _repository.DatabaseCollection.ToList() : _repository.Collection.ToList();

        public UserReposController(UserRepository repository)
        {
            _repository = repository;
        }

        public void Add(User value)
        {
            if (_repository.IsDatabaseConnected)
            {
                _repository.DatabaseCollection.Add(value);
                _repository.Update();
            }
            else
                _repository.Collection.Add(value);
        }

        /// <summary>
        /// Removing object by ID
        /// </summary>
        /// <param name="value"></param>
        public void Remove(User value)
        {
            if (_repository.IsDatabaseConnected)
            {
                _repository.DatabaseCollection.Attach(value);
                _repository.DatabaseCollection.Remove(value);
                _repository.Update();
            }
            else
                _repository.Collection.Remove(value);
        }

        public void Edit(User value)
        {
            if (_repository.IsDatabaseConnected)
            {
                var newDbValue = _repository.DatabaseCollection.FirstOrDefault(v => v.Id == value.Id);

                if (newDbValue == null)
                    return;

                newDbValue.Username = value.Username;
                newDbValue.Password = value.Password;
                newDbValue.UserInfo = value.UserInfo;

                _repository.Update();
            }
            else
            {
                var newValue = _repository.Collection.FirstOrDefault(v => v.Id == value.Id);

                if(newValue == null)
                    return;

                newValue.Username = value.Username;
                newValue.Password = value.Password;
                newValue.UserInfo = value.UserInfo;
            }
        }

    }
}
