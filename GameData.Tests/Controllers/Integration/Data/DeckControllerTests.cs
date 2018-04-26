using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TestCards testCards = new TestCards();
            DeckController controller = new DeckController()
            {
                Repository = new DeckRepository()
            };
            Stack<Card> deck = new Stack<Card>();
            deck.Push(testCards.FirstCard);
            deck.Push(testCards.SecondCard);
            deck.Push(testCards.FirstCard);
            deck.Push(testCards.SecondCard);
            deck.Push(testCards.SecondCard);

            controller.AddDeck("test",deck);
            controller.ShuffleDeck("test");

            Assert.AreNotEqual(deck,controller.GetDeck("test"));
        }
    }
}
