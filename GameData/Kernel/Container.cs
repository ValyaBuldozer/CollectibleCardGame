using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Observer;
using GameData.Models.Repository;
using Unity;
using Unity.Lifetime;

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
            _container.RegisterType<IActionTableControlller, InActionTableController>();
        }

        public T Get<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
