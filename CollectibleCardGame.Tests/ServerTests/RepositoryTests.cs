using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Controllers;
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
            UnityKernel.Get<UserReposController>().Add(new User(){Username = "testUser",Password = "test"});
            var user = new User() {Username = "testUser", Password = "test"};
            if(UnityKernel.Get<UserRepository>().Collection.FirstOrDefault(u=>u.Equals(user))!=null)
                UnityKernel.Get<UserReposController>().Remove(user);

            Assert.IsTrue(UnityKernel.Get<UserRepository>().Collection.FirstOrDefault(u=>u.Username == "testUser")==null);
        }
    }
}
