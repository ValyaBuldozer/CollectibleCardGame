using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests.ArchitectureTests
{
    [TestClass]
    public class EventArgsInterfaceTest
    {
        private string _first;
        private int _second;
        public event EventHandler<IEventArgs> TestEvent;

        public event EventHandler<FirstEventArgs> FirstEvent;
        public event EventHandler<SecondEventArgs> SecondEvent;

        public void OnTestEvent(object sender, FirstEventArgs e)
        {
            _first = e.FirstProp;
        }

        public void OnTestEvent(object sender, SecondEventArgs e)
        {
            _second = e.SecondProp;
        }

        //public void OnTestEvent(object sender, IEventArgs e)
        //{
        //    if (e is FirstEventArgs args1)
        //        _first = args1.FirstProp;

        //    if (e is SecondEventArgs args2)
        //        _second = args2.SecondProp;
        //}

        [TestMethod]
        public void InterfaceTest()
        {
            var first = new FirstEventArgs {FirstProp = "test"};
            var second = new SecondEventArgs {SecondProp = 1};

            //TestEvent += OnTestEvent;
            FirstEvent += OnTestEvent;
            SecondEvent += OnTestEvent;

            FirstEvent?.Invoke(this, first);
            SecondEvent?.Invoke(this, second);

            Assert.IsTrue(_first == "test" && _second == 1);
        }
    }


    public interface IEventArgs
    {
        object SomeData { set; get; }
    }

    public class FirstEventArgs : IEventArgs
    {
        public string FirstProp { set; get; }
        public object SomeData { set; get; }
    }

    public class SecondEventArgs : IEventArgs
    {
        public int SecondProp { set; get; }
        public object SomeData { set; get; }
    }
}