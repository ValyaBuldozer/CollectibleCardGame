using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Models;
using Unity;
using Unity.Lifetime;

namespace CollectibleCardGame.Tests.UnityTests
{
    [TestClass]
    public class KernelTest
    {
        [TestMethod]
        public void KernelUnityTest()
        {
            UnityTestKernel.InitializeKernel();
            A a = new A() {Number = 1};
            IContent content = new TestContent(){Data = a,AnotherData = a};
            var t = content.GetType();

           // var controller = UnityKernel.Get(content.ControllerType);

            var controller = UnityTestKernel.Get<IContentController<TestContent>>();

            Assert.IsTrue(controller.Execute(content));
        }
    }

    public class UnityTestKernel
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            set => _container = value;
            get => _container;
        }

        public static T Get<T>()
        {
            return _container.Resolve<T>();
        }

        public static void InitializeKernel()
        {
            _container = new UnityContainer();

            _container.RegisterType<IContentController<TestContent>, TestContentController>();

            _container.RegisterType<IRepository<User>, UserRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepositoryController<User>, UserRepositoryController>();
        }

        public static object Get(Type t)
        {
            return _container.Resolve(t);
        }
    }

    public interface IContent
    {
        object Data { set; get; }
        Type ControllerType { get; }
    }

    public class TestContent :IContent
    {
        public object Data { set; get; }
        public object AnotherData { set; get; }
        public Type ControllerType => typeof(TestContentController);
    }

    //in - ???
    public interface IContentController<in T>
    {
        bool Execute(IContent message);
    }

    public class TestContentController : IContentController<TestContent>
    {
        public bool Execute(IContent message)
        {
            var msg = (TestContent) message;

            return msg.Data == msg.AnotherData;
        }
    }
}
