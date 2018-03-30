using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Models;

namespace CollectibleCardGame.Tests.UnityTests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void ReposSingletoneTest()
        {
            UnityKernel.InitializeKernel();
            UnityKernel.Get<IRepositoryController<User>>().Add(new User());
            UnityKernel.Get<IRepositoryController<User>>().Add(new User());

            Assert.IsTrue(((UserRepository)(UnityKernel.Get<IRepository<User>>())).Collection.Count == 3);
        }
    }



    public interface IRepository<T>
    {
        void Save();
    }

    public class UserRepository : IRepository<User>
    {
        private List<User> _collection;

        public List<User> Collection
        {
            set => _collection = value;
            get => _collection;
        }

        public void Save()
        {

        }

        public UserRepository()
        {
            _collection = new List<User>
            {
                new User() {Id =1,Password = "test",Userame = "test"}
            };
        }
    }

    public interface IRepositoryController<T>
    {
        IRepository<T> Repository { get; }
        void Add(T value);
        void Remove(T value);
        void Edit(int id, T value);
    }

    public class UserRepositoryController : IRepositoryController<User>
    {
        public IRepository<User> Repository { private set; get; }

        public UserRepositoryController(IRepository<User> repository)
        {
            Repository = repository;
        }

        public void Add(User value)
        {
            ((UserRepository)Repository).Collection.Add(value);
            Repository.Save();
        }

        public void Remove(User value)
        {

        }

        public void Edit(int id, User value)
        {

        }
    }
}
