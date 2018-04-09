using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CollectibleCardGame.Tests.JsonParserTest
{
    //[TestClass]
    public class ParserInterfaceTest
    {
        //[TestMethod]
        public void ContentTest()
        {
           // JsonSerializer jsonSerializer = new JsonSerializer();

            string jsonStirng = JsonConvert.SerializeObject(new Message()
            {
                Type = MessageType.First,
                Content = new FirstMessage() {FirstProperty = 100}
            });

            var DeserializeObject = JsonConvert.DeserializeObject<Message>(jsonStirng);

            var content = (DeserializeObject.Content as JObject).ToObject<FirstMessage>();

            Assert.IsTrue(content.FirstProperty==100);
        }
    }

    public interface IContent
    {
        //object Data { set; get; }
    }

    public enum MessageType { First,Second }

    public class Message
    {
        public MessageType Type { set; get; }
        public object Content { set; get; }
    }

    public class FirstMessage : IContent
    {
        public object Data { set; get; }

        public int FirstProperty { set; get; }
    }

    public class SecondMessage : IContent
    {
        public object Data { set; get; }

        public string SecondProperty { set; get; }
    }
}
