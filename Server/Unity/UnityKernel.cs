using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Server;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Repository;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers;
using Server.Controllers.Repository;
using Server.Database;
using Server.Models;
using Server.Network.Controllers;
using Server.Network.Controllers.MessageHandlers;
using Server.Repositories;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Server.Unity
{
    /// <summary>
    /// Server
    /// </summary>
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

            var defaultSettings = new GameSettings()
            {
                PlayerTurnInterval = 120000,
                IsPlayerTurnTimerEnabled = true,
                MaxDeckCardsCount = 30,
                PlayerHandCardsMaxCount = 10,
                PlayersCount = 2,
                PlayerTableUnitsMaxCount = 10,
                MaxPlayerMana = 10,
                StartHandCardsCount = 4
            };

            _container.RegisterInstance(defaultSettings, new ContainerControlledLifetimeManager());

            _container.RegisterType<ILogger, ConsoleLogger>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IServer, TcpServer>(new ContainerControlledLifetimeManager());

            //context binding
            _container.RegisterType<IContext, AppDbContext>();

            //repository binding
            _container.RegisterType<UserRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<UserInfoRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<CardRepository>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(LoadCardsFile()));
            _container.RegisterType<AwaitingClientsQueue>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ConnectedClientsRepository>(new ContainerControlledLifetimeManager());

            //controller binding
            _container.RegisterType<UserReposController>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDataRepositoryController<Card>,CardRepositroryController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<AwaitingClientsQueueController>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ConnectedClientsRepositoryController>(new ContainerControlledLifetimeManager());

            //messagehandler binding
            _container.RegisterType<MessageHandlerBase<LogInMessage>, LogInMessageHandler>();
            _container.RegisterType<MessageHandlerBase<RegistrationMessage>, RegistrationMessageHandler>();
            _container.RegisterType<MessageHandlerBase<GameStartMessage>, GameStartMessageHandler>();
            _container.RegisterType<MessageHandlerBase<GameRequestMessage>, GameRequestMessageHandler>();
            _container.RegisterType<MessageHandlerBase<SetDeckMessage>, SetDeckMessageHandler>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<IMessageConverter,MessageConverter>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<ClientController>();
            _container.RegisterType<ServerController>(new ContainerControlledLifetimeManager());

            //service binding
            _container.RegisterType<UserService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ServerStateService>(new ContainerControlledLifetimeManager());

            //message handlers binding
            _container.RegisterType<MessageHandlerBase<LogInMessage>,
                LogInMessageHandler>();
            _container.RegisterType<MessageHandlerBase<RegistrationMessage>,
                RegistrationMessageHandler>();
            _container.RegisterType<MessageHandlerBase<GameStartMessage>,GameStartMessageHandler>();
            _container.RegisterType<MessageHandlerBase<PlayerTurnMessage>, PlayerTurnMessageHandler>();

            _container.Resolve<CardRepository>();
        }

        public static object Get(Type t)
        {
            if(_container == null)
                InitializeKernel();

            return _container.Resolve(t);
        }

        private static string LoadCardsFile()
        {
            using (StreamReader streamReader = new StreamReader("..\\..\\Resources\\CardsRepository.json"))
            {
                //StreamReader streamReader = new StreamReader(stream);
                var readedString = streamReader.ReadToEnd();
                streamReader.Close();
                return readedString;
            }
        }
    }
}
