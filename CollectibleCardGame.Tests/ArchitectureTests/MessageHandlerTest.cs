using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests.ArchitectureTests
{
    [TestClass]
    public class MessageHandlerTest
    {
        [TestMethod]
        public void HandlerTest()
        {
            MessageHandler handler = new MessageHandler();
            IMessage msg = new FirstMessage();
            var ansewer = handler.Handle(msg);

            Assert.IsTrue(ansewer);
        }
    }

    public interface IMessage
    {
        object Data { set; get; }
    }

    public class FirstMessage : IMessage
    {
        public int FirstNumber { set; get; }
        public object Data { set; get; }
    }

    public class SecondMessage : IMessage
    {
        public int SecondNumber { set; get; }
        public object Data { set; get; }
    }

    public class MessageHandler
    {
        public bool Handle(IMessage msg)
        {
            switch (msg.GetType().Name)
            {
                case nameof(FirstMessage):
                    return Handle((FirstMessage)msg);
                case nameof(SecondMessage):
                    return Handle((FirstMessage)msg);
                default:
                    throw new NullReferenceException();
            }
        }

        public bool Handle(FirstMessage msg)
        {
            return true;
        }

        public bool Handle(SecondMessage msg)
        {
            return false;
        }
    }
}
