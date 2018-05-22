using System;
using System.IO;
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
using Server.Network.Controllers;
using Server.Network.Controllers.MessageHandlers;
using Server.Repositories;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Server.Unity
{
    /// <summary>
    ///     Server
    /// </summary>
    public class UnityKernel
    {
        public static UnityContainer Container { set; get; }

        public static T Get<T>()
        {
            if (Container == null)
                InitializeKernel();

            return Container.Resolve<T>();
        }

        public static void InitializeKernel()
        {
            Container = new UnityContainer();

            var defaultSettings = new GameSettings
            {
                PlayerTurnInterval = 120000,
                IsPlayerTurnTimerEnabled = true,
                MaxDeckCardsCount = 30,
                PlayerHandCardsMaxCount = 10,
                PlayersCount = 2,
                PlayerTableUnitsMaxCount = 10,
                MaxPlayerMana = 10
            };

            Container.RegisterInstance(defaultSettings, new ContainerControlledLifetimeManager());

            Container.RegisterType<ILogger, ConsoleLogger>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IServer, TcpServer>(new ContainerControlledLifetimeManager());

            //context binding
            Container.RegisterType<IContext, AppDbContext>();

            //repository binding
            Container.RegisterType<UserRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<UserInfoRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<CardRepository>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(LoadCardsFile()));
            Container.RegisterType<AwaitingClientsQueue>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ConnectedClientsRepository>(new ContainerControlledLifetimeManager());

            //controller binding
            Container.RegisterType<UserReposController>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDataRepositoryController<Card>, CardRepositroryController>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<AwaitingClientsQueueController>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ConnectedClientsRepositoryController>(new ContainerControlledLifetimeManager());

            //messagehandler binding
            Container.RegisterType<MessageHandlerBase<LogInMessage>, LogInMessageHandler>();
            Container.RegisterType<MessageHandlerBase<RegistrationMessage>, RegistrationMessageHandler>();
            Container.RegisterType<MessageHandlerBase<GameStartMessage>, GameStartMessageHandler>();
            Container.RegisterType<MessageHandlerBase<GameRequestMessage>, GameRequestMessageHandler>();
            Container.RegisterType<MessageHandlerBase<SetDeckMessage>, SetDeckMessageHandler>(
                new ContainerControlledLifetimeManager());

            Container.RegisterType<IMessageConverter, MessageConverter>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<ClientController>();
            Container.RegisterType<ServerController>(new ContainerControlledLifetimeManager());

            //service binding
            Container.RegisterType<UserService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ServerStateService>(new ContainerControlledLifetimeManager());

            //message handlers binding
            Container.RegisterType<MessageHandlerBase<LogInMessage>,
                LogInMessageHandler>();
            Container.RegisterType<MessageHandlerBase<RegistrationMessage>,
                RegistrationMessageHandler>();
            Container.RegisterType<MessageHandlerBase<GameStartMessage>, GameStartMessageHandler>();
            Container.RegisterType<MessageHandlerBase<PlayerTurnMessage>, PlayerTurnMessageHandler>();

            Container.Resolve<CardRepository>();
        }

        public static object Get(Type t)
        {
            if (Container == null)
                InitializeKernel();

            return Container.Resolve(t);
        }

        private static string LoadCardsFile()
        {
            using (var streamReader = new StreamReader("..\\..\\Resources\\CardsRepository.json"))
            {
                //StreamReader streamReader = new StreamReader(stream);
                var readedString = streamReader.ReadToEnd();
                streamReader.Close();
                return readedString;
            }
        }
    }
}