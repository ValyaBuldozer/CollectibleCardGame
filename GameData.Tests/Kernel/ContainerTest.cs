using GameData.Kernel;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Kernel
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void InitializeContainerTest()
        {
            var container = new Container();

            container.Initialize(TestGameSettings.Get);

            Assert.IsTrue(container != null);
        }
    }
}