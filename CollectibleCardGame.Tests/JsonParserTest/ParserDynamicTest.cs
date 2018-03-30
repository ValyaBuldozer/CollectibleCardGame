using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using CollectibleCardGame.Tests.NetworkTests;

namespace CollectibleCardGame.Tests.JsonParserTest
{
    [TestClass]
    public class ParserDynamicTest
    {
        [TestMethod]
        public void JObjectDynamicTest()
        {
            dynamic d = JObject.Parse("{number:100, str:'test'}");

            int n = 0;

            A a  =new A();
            a.Number = 1;

            d = a;

            //d.number = d.number + 1;
            if (IsPropertyExist(d,"Number"))
                n = d.number + 1;

            Assert.AreEqual(n, 101);
        }

        public static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
    }
}
