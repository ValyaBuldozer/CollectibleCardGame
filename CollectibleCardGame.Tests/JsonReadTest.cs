using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CollectibleCardGame.Tests
{
    [TestClass]
    public class JsonReadTest
    {
        [TestMethod]
        public void Test()
        {
            StreamReader streamReader = new StreamReader("C:\\Users\\1\\Source\\Repos\\CollectibleCardGame\\CollectibleCardGame\\Resources\\CardsRepository.json");

            var resultString = streamReader.ReadToEnd();
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            IList<Card> cards = JsonConvert.DeserializeObject<IList<Card>>(resultString,settings);

            Assert.IsNotNull(cards);

            streamReader.Close();
        }

        [TestMethod]
        public void SerializeNamed()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            List<Card> cards = new List<Card>()
            {
                new UnitCard()
                {
                    Name = "test",
                    Cost = 5,
                    AttackPriority = 2,
                    BaseAttack = 1,
                    BaseHP = 1,
                    DeathRattleActionInfo = new CardActionInfo()
                    {
                        ActionId = 1,
                        ParameterType = ActionParameterType.Buff,
                        ParameterValue = 1
                    }
                },
                new SpellCard()
                {
                    Name = "test",
                    Cost = 5,
                    ActionInfo = new CardActionInfo()
                    {
                        ActionId = 1,
                        ParameterType = ActionParameterType.Buff,
                        ParameterValue = 1
                    }
                }
            };

            string json = JsonConvert.SerializeObject(cards, settings);
            var deserialized = JsonConvert.DeserializeObject<List<Card>>(json,settings);
            Assert.IsNotNull(json);
            Assert.AreEqual(cards,deserialized);
        }
    }
}
