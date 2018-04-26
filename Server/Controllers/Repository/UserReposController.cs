﻿using System;
using System.Collections.Generic;
using System.Linq;
using Server.Models;
using Server.Repositories;

namespace Server.Controllers.Repository
{
    public class UserReposController
    {
        private readonly UserRepository _repository;

        public IEnumerable<User> GetEnumerable => _repository.Collection;

        public UserReposController(UserRepository repository)
        {
            _repository = repository;
        }

        public void Add(User value)
        {
            _repository.Collection.Add(value);
            _repository.Update();
        }

        /// <summary>
        /// Removing object by ID
        /// </summary>
        /// <param name="value"></param>
        public void Remove(User value)
        {
            _repository.Collection.Attach(value);
            _repository.Collection.Remove(value);
            _repository.Update();
        }

        public void Edit(User value)
        {
            var dbValue = _repository.Collection.FirstOrDefault(v => v.Id == value.Id);

            if(dbValue == null)
                throw new NullReferenceException("No value found");

            //_repository.Collection.Remove(dbValue);
            //_repository.Collection.Add(value);
            dbValue.Username = value.Username;
            dbValue.Password = value.Password;
            dbValue.UserInfo = value.UserInfo;

            _repository.Update();
        }

    }
}
