using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Models;
using Server.Repositories;
using Server.Services;
using Server.Unity;

namespace CollectibleCardGame.Tests.ServerTests.ServiceTests
{
    [TestClass]
    public class UserServiceTest
    {
        public UserServiceTest()
        {
            UnityKernel.InitializeKernel();
        }

        //[TestMethod]
        public void RegisterUserTest()
        {
            var user = new User()
            {
                Username = "testUser1",
                Password = "test",
                UserInfo = new UserInfo() {GameLoseCount = 1, GameWinCount = 1}
            };
            var repos = UnityKernel.Get<UserRepository>();
            var r2 = UnityKernel.Get<UserRepository>();
            var info = UnityKernel.Get<UserInfoRepository>();
            
            UnityKernel.Get<UserService>().RegisterUser("testUser8", "test");
            //UnityKernel.Get<UserService>().RegisterUser("testUser5", "test");

            Assert.IsTrue(repos.Collection.
                FirstOrDefault(u=>u.Username == "testUser8") != null);
        }

        [TestMethod]
        public void LogIn()
        {
            var userCollection = UnityKernel.Get<UserRepository>().Collection;
            var service = UnityKernel.Get<UserService>();

            if (userCollection.FirstOrDefault(u => u.Username == "test") == null)
                service.RegisterUser("test", "test");

            var user = service.LogIn("test", "test");

            Assert.IsNotNull(user);
        }
    }
}
