using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Controllers.UnitTests.Data
{
    [TestClass]
    public class EntityRepositoryControllerTest
    {
        [TestMethod]
        public void AddNotEmptyReposEntiryTest()
        {
            var repository = new EntityRepository
            {
                Collection = new List<Entity>
                {
                    new Entity(0, EntityType.Card),
                    new Entity(1, EntityType.Card)
                }
            };
            var controller = new EntityRepositoryController(repository);

            controller.Add(new Entity(0, EntityType.Player));

            Assert.IsTrue(repository.Collection.Exists(item => item.EntityType == EntityType.Player));
        }

        [TestMethod]
        public void AddEmptyReposEntityTest()
        {
            var repository = new EntityRepository();
            var controller = new EntityRepositoryController(repository);

            controller.Add(new Entity {EntityType = EntityType.Card});
            controller.Add(new Entity {EntityType = EntityType.Player});
            controller.Add(new Entity {EntityType = EntityType.Scene});

            var cardItem = repository.Collection.FirstOrDefault(item => item.EntityType == EntityType.Card);
            var playerItem = repository.Collection.FirstOrDefault(item => item.EntityType == EntityType.Player);
            var sceneItem = repository.Collection.FirstOrDefault(item => item.EntityType == EntityType.Scene);

            Assert.AreEqual(cardItem.EntityId, 0);
            Assert.AreEqual(playerItem.EntityId, 1);
            Assert.AreEqual(sceneItem.EntityId, 2);
        }

        [TestMethod]
        public void AddIdReferenceTest()
        {
            var card = new UnitCard();
            var repository = new EntityRepository
            {
                Collection = new EditableList<Entity>
                {
                    new Entity(0, EntityType.Card),
                    new Entity(1, EntityType.Card)
                }
            };
            var controller = new EntityRepositoryController(repository);

            controller.AddNewItem(card);

            Assert.AreEqual(card.EntityId, 2);
        }
    }
}