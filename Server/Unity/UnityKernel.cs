using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Server;
using Server.Controllers;
using Server.Database;
using Server.Models;
using Server.Network.Controllers;
using Server.Network.Controllers.MessageHandlers;
using Server.Repositories;
using Server.Services;
using Unity;
using Unity.Lifetime;

namespace Server.Unity
{
    public class UnityKernel
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

            _container.RegisterType<IServer, TcpServer>(new ContainerControlledLifetimeManager());

            //context binding
            _container.RegisterType<IContext, AppDbContext>();

            //repository binding
            _container.RegisterType<UserRepository>();
            _container.RegisterType<UserInfoRepository>();
            _container.RegisterType<CardRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<AwaitingClientsQueue>(new ContainerControlledLifetimeManager());

            //controller binding
            _container.RegisterType<UserReposController>();
            _container.RegisterType<UserInfoReposController>();
            _container.RegisterType<CardReposController>();
            _container.RegisterType<AwaitingClientsQueueController>(new ContainerControlledLifetimeManager());
            _container.RegisterType<NetworkMessageConverter>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ClientController>();

            //service binding
            _container.RegisterType<UserService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ServerStateService>(new ContainerControlledLifetimeManager());

            //message handlers binding
            _container.RegisterType<LogInMessageHandler>();
            _container.RegisterType<RegistrationMessageHandler>();
            _container.RegisterType<GameStartMessageHandler>();
        }

        public static object Get(Type t)
        {
            if(_container == null)
                InitializeKernel();

            return _container.Resolve(t);
        }
    }
}
