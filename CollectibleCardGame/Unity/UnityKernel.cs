using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Server;
using CollectibleCardGame.Controllers;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Network.Controllers.MessageHandlers;
using CollectibleCardGame.ViewModels.Windows;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace CollectibleCardGame.Unity
{
    public static class UnityKernel
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            set => _container = value;
            get => _container;
        }

        public static T Get<T>()
        {
            if (_container == null)
                InitializeKernel();

            return _container.Resolve<T>();
        }

        public static void InitializeKernel()
        {
            _container = new UnityContainer();


            //repository binding
            _container.RegisterType<INetworkCommunicator, TcpCommunicator>(new InjectionConstructor(new object[]
            {
                new TcpClient() //localhost injection, can be changed
            }));

            //controller binding
            _container.RegisterType<NetworkMessageConverter>();
            _container.RegisterType<NetworkConnectionController>(new ContainerControlledLifetimeManager());


            //message handlers binding
            _container.RegisterType<GameRequestMessageHandler>();
            _container.RegisterType<RegistrationMessageHandler>();
            _container.RegisterType<LogInMessageHandler>();

            //viewmodels binding
            _container.RegisterType<MainWindowViewModel>();

            //view binding
            _container.RegisterType<MainWindow>(new ContainerControlledLifetimeManager());
        }

        public static object Get(Type t)
        {
            if (_container == null)
                InitializeKernel();

            return _container.Resolve(t);
        }
    }
}
