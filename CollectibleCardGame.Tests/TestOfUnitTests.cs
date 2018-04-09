using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests
{
    class A
    {
        public int Number { set; get; }
    }
    class B
    {
        public int Number { set; get; }
    }

    [TestClass]
    public class TestOfUnitTests
    {
        [TestMethod]
        public void TestMethod()
        {
            A a  =new A();
            a.Number = 1;
            B b = new B();

            b.Number = a.Number;

            Assert.IsTrue(a.Number == b.Number);
        }
        
    }
}
