using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Controllers.Repository;
using Server.Models;
using Server.Repositories;
using Server.Unity;

namespace CollectibleCardGame.Tests.ServerTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void RemoveReposTest()
        {
            UnityKernel.InitializeKernel();
            var controller = UnityKernel.Get<UserReposController>();
            //UnityKernel.Get<UserReposController>().Add(new User(){Username = "testUser",Password = "test"});
            //var user = new User() {Username = "testUser", Password = "test"};
            //if(UnityKernel.Get<UserRepository>().Collection.ToList().FirstOrDefault(u=>u.Equals(user))!=null)
            //    UnityKernel.Get<UserReposController>().Remove(user);

            controller.Add(new User {Username = "repostest", Password = "test"});
            var deletingUser = controller.GetEnumerable.FirstOrDefault(u => u.Username == "repostest");
            controller.Remove(deletingUser);

            Assert.IsTrue(UnityKernel.Get<UserRepository>().DatabaseCollection
                              .FirstOrDefault(u => u.Username == "repostest") == null);
        }

        //[TestMethod]
        public void ForechRepostTest()
        {
            UnityKernel.InitializeKernel();
            var i = 0;
            foreach (var iUser in UnityKernel.Get<UserRepository>().DatabaseCollection) i++;

            Assert.IsTrue(i > 0);
        }
    }
}