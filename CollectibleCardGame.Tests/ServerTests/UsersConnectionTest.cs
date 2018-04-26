using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Network.Controllers;
using Server.Unity;

namespace CollectibleCardGame.Tests.ServerTests
{
    [TestClass]
    public class UsersConnectionTest
    {
        private void ServerStart()
        {
            UnityKernel.InitializeKernel();
            UnityKernel.Get<ServerController>().Start(IPAddress.Parse("127.0.0.1"), 8800);
        }

        [TestMethod]
        public void OneUserLogInTest()
        {
            ServerStart();

        }
    }
}
