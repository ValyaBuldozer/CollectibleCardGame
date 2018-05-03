using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Kernel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Kernel
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void InitializeContainerTest()
        {
            Container container = new Container();

            container.Initialize();

            Assert.IsTrue(container != null);
        }
    }
}
