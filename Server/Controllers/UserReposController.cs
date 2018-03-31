using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;
using Server.Repositories;

namespace Server.Controllers
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

        public void Remove(User value)
        {
            _repository.Collection.Remove(value);
            _repository.Update();
        }

        public void Edit(User value)
        {
            var dbValue = _repository.Collection.FirstOrDefault(v => v.Id == value.Id);

            if(dbValue == null)
                throw new NullReferenceException("No value found");

            _repository.Collection.Remove(dbValue);
            _repository.Collection.Add(value);

            _repository.Update();
        }

    }
}
