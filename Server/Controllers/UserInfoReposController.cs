using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;
using Server.Repositories;

namespace Server.Controllers
{
    public class UserInfoReposController
    {
        private readonly UserInfoRepository _repository;

        public IEnumerable<UserInfo> GetEnumerable => _repository.Collection;

        public UserInfoReposController(UserInfoRepository repository)
        {
            _repository = repository;
        }

        public void Add(UserInfo value)
        {
            _repository.Collection.Add(value);
            _repository.Update();
        }

        public void Remove(UserInfo value)
        {
            _repository.Collection.Remove(value);
            _repository.Update();
        }

        public void Edit(UserInfo value)
        {
            var dbValue = _repository.Collection.FirstOrDefault(v => v.Id == value.Id);

            if (dbValue == null)
                throw new NullReferenceException("No value found");

            _repository.Collection.Remove(dbValue);
            _repository.Collection.Add(value);

            _repository.Update();
        }

    }
}
