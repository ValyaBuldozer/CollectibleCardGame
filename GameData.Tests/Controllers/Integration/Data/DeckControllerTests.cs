using System.Collections.Generic;
using GameData.Controllers.Data;
using GameData.Models.Cards;
using GameData.Models.Repository;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Controllers.Integration.Data
{
    [TestClass]
    public class DeckControllerTests
    {
        [TestMethod]
        public void ShuffleTest()
        {
            var testCards = new TestCards();
            var controller = new DeckController(new DeckRepository());

            var deck = new Stack<Card>();
            deck.Push(testCards.FirstCard);
            deck.Push(testCards.SecondCard);
            deck.Push(testCards.FirstCard);
            deck.Push(testCards.SecondCard);
            deck.Push(testCards.SecondCard);

            controller.AddDeck("test", deck);
            controller.ShuffleDeck("test");

            Assert.AreNotEqual(deck, controller.GetDeck("test"));
        }
    }
}