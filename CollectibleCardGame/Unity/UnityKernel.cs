﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Server;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Models;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Network.Controllers.MessageHandlers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;
using GameData.Network;
using GameData.Network.Messages;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace CollectibleCardGame.Unity
{
    /// <summary>
    /// Client
    /// </summary>
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

            _container.RegisterType<ILogger, MessageBoxLogger>();

            //view and viewmodels bindings
            _container.RegisterType<LogInFramePageViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<LogInFramePage>(new ContainerControlledLifetimeManager());
            _container.RegisterType<RegistrationFramePageViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ToRegisterFramePage>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ErrorFramePageViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ConnectionErrorFramePage>(new ContainerControlledLifetimeManager());

            _container.RegisterType<LogInFramePageShellViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<LogInFramePageShell>(new ContainerControlledLifetimeManager());

            _container.RegisterType<GoGameFramePageViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<GoGameFramePage>(new ContainerControlledLifetimeManager());

            _container.RegisterType<MenuFramePageViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<MainMenuFramePage>(new ContainerControlledLifetimeManager());

            _container.RegisterType<GameEngineViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<GameEngineFramePage>(new ContainerControlledLifetimeManager());

            _container.RegisterType<MainWindowViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<MainWindow>(new ContainerControlledLifetimeManager());

            //repository binding
            _container.RegisterType<CurrentUser>(new ContainerControlledLifetimeManager());



            //controller binding
            _container.RegisterType<NetworkMessageConverter>(new PerResolveLifetimeManager());
            _container.RegisterType<INetworkCommunicator, TcpCommunicator>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new object[]
                {
                    new TcpClient() //localhost injection, can be changed
                }));
            _container.RegisterType<INetworkController,NetworkConnectionController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<UserController>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IGlobalController, GlobalAppStateController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<GameController>(new ContainerControlledLifetimeManager());

            //messagehandler binding
            _container.RegisterType<MessageHandlerBase<LogInMessage>, LogInMessageHandler>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<MessageHandlerBase<RegistrationMessage>, RegistrationMessageHandler>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<MessageHandlerBase<GameRequestMessage>, GameRequestMessageHandler>(
                new ContainerControlledLifetimeManager());

            _container.Resolve<MainWindow>();
            //initializating observer controllers
            _container.Resolve<UserController>();
            _container.Resolve<IGlobalController>();
            _container.Resolve<GameController>();
        }

        public static object Get(Type t)
        {
            if (_container == null)
                InitializeKernel();

            return _container.Resolve(t);
        }
    }
}
