using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Models;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Network.Controllers.MessageHandlers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Repository;
using GameData.Network;
using GameData.Network.Messages;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace CollectibleCardGame.Unity
{
    /// <summary>
    ///     Client
    /// </summary>
    public static class UnityKernel
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

            Container.RegisterType<ILogger, MessageBoxLogger>();

            //view and viewmodels bindings
            Container.RegisterType<LogInFramePageViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<LogInFramePage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<RegistrationFramePageViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ToRegisterFramePage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<ErrorFramePageViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ConnectionErrorFramePage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<ServerConnectionViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ServerConnectionPage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<LogInFramePageShellViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<LogInFramePageShell>(new ContainerControlledLifetimeManager());

            Container.RegisterType<GoGameFramePageViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<GoGameFramePage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<DeckSettingsViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<DecksSettingsFramePage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<MenuFramePageViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<MainMenuFramePage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<GameEngineViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<GameEngineFramePage>(new ContainerControlledLifetimeManager());

            Container.RegisterType<MainWindowViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<MainWindow>(new ContainerControlledLifetimeManager());

            //repository binding
            Container.RegisterType<EntityRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<CardRepository>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(LoadCardsFile()));


            //controller binding
            //repository
            Container.RegisterType<IDataRepositoryController<Entity>, EntityRepositoryController>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDataRepositoryController<Card>, CardRepositroryController>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<CurrentUserService>(new ContainerControlledLifetimeManager());

            //network
            Container.RegisterType<IMessageConverter, MessageConverter>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<INetworkCommunicator, TcpCommunicator>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new TcpClient()));
            Container.RegisterType<INetworkController, NetworkConnectionController>(
                new ContainerControlledLifetimeManager());

            //logic
            Container.RegisterType<UserController>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGlobalController, GlobalAppStateController>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<GameController>(new ContainerControlledLifetimeManager());

            //messagehandlers
            Container.RegisterType<MessageHandlerBase<LogInMessage>, LogInMessageHandler>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<MessageHandlerBase<RegistrationMessage>, RegistrationMessageHandler>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<MessageHandlerBase<GameRequestMessage>, GameRequestMessageHandler>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<MessageHandlerBase<ObserverActionMessage>, ObserverActionMessageHandler>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<MessageHandlerBase<ErrorMessage>, ErrorMessageHandler>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<MessageHandlerBase<SetDeckMessage>, SetDeckMessageHandler>(
                new ContainerControlledLifetimeManager());

            Container.Resolve<CardRepository>();
            Container.Resolve<MainWindow>();
            //initializating observer controllers
            Container.Resolve<UserController>();
            Container.Resolve<IGlobalController>();
            Container.Resolve<GameController>();
        }

        public static object Get(Type t)
        {
            if (Container == null)
                InitializeKernel();

            return Container.Resolve(t);
        }

        private static string LoadCardsFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CollectibleCardGame.Resources.CardsRepository.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                var streamReader = new StreamReader(stream);
                var readedString = streamReader.ReadToEnd();
                streamReader.Close();
                return readedString;
            }
        }
    }
}