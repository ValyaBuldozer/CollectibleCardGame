using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Observer;
using GameData.Models.PlayerTurn;
using GameData.Models.Repository;
using Unity;
using Unity.Lifetime;
using Unity.Registration;

namespace GameData.Kernel
{
    public class Container
    {
        private readonly UnityContainer _container;

        public Container()
        {
            _container = new UnityContainer();
        }

        public void Initialize()
        {
            RegisterBindings();

            //ititializing observer for events bindings
            Get<GlobalGameObserver>();
        }

        private void RegisterBindings()
        {
            //models
            _container.RegisterType<TableCondition>(new ContainerControlledLifetimeManager());

            //repository
            _container.RegisterType<DeckRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<EntityRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<GameActionRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ObserverActionRepository>(new ContainerControlledLifetimeManager());

            //repository controllers
            _container.RegisterType<IDeckController, DeckController>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDataRepositoryController<Entity>, EntityRepositoryController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IDataRepositoryController<GameAction>, GameActionRepositoryController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<ObserverActionRepositoryController>(
                new ContainerControlledLifetimeManager());

            //logic controllers
            _container.RegisterType<ICardDrawController, CardDrawController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IPlayerTurnDispatcher, PlayerTurnDispatcher>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IActionTableControlller, InActionTableController>();
            _container.RegisterType<IGameActionController, GameActionController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IUnitDispatcher, UnitDispatcher>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<ICardDeployDispatcher, CardDeployDispatcher>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IGameStateController, GameStateController>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<GlobalGameObserver>(new ContainerControlledLifetimeManager());

            //player turn handlers
            _container.RegisterType<IPlayerTurnValidator, PlayerTurnValidator>();
            _container.RegisterType<IPlayerTurnHandler<CardDeployPlayerTurn>,
                CardDeployPlayerTurnHandler>();
            _container.RegisterType<IPlayerTurnHandler<UnitAttackPlayerTurn>,
                UnitAttackPlayerTurnHandler>();
            _container.RegisterType<IPlayerTurnHandler<EndPlayerTurn>,
                EndPlayerTurnHandler>();
        }

        public T Get<T>()
        {
            return _container.Resolve<T>();
        }

        public IEnumerable<IContainerRegistration> GetRegistrations => _container.Registrations;
    }
}
