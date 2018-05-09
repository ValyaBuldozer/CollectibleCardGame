using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;
using Unity.Attributes;

namespace GameData.Models.Repository
{
    public class CardRepository
    {
        private readonly string _filePath;

        public List<Card> Collection { private set; get; }

        [InjectionConstructor]
        public CardRepository()
        {
            Collection = new List<Card>()
            {
                //test cards
                new UnitCard()
                {
                    ID = 301,
                    Name = "TestUnitCard1",
                    Cost = 0,
                    CanBePlayedOnEnemyTurn = false,
                    BaseAttack = 1,
                    BaseHP = 1
                },
                new UnitCard()
                {
                    ID = 302,
                    Name = "TestUnitCard2",
                    Cost = 1,
                    CanBePlayedOnEnemyTurn = false,
                    BaseAttack = 2,
                    BaseHP = 1
                },
                new UnitCard()
                {
                    ID = 303,
                    Name = "TestUnitCard3",
                    Cost = 5,
                    CanBePlayedOnEnemyTurn = false,
                    BaseAttack = 4,
                    BaseHP = 4
                },
                new SpellCard()
                {
                    ID = 304,
                    Name = "TesSpellCard1",
                    Cost = 1,
                    CanBePlayedOnEnemyTurn = false,
                    ActionInfo = new CardActionInfo()
                    {
                        ActionId = 301,
                        ParameterType = ActionParameterType.Damage,
                        ParameterValue = 1
                    }
                }
            };
        }

        public CardRepository(string filePath)
        {
            _filePath = filePath;

            //todo : чтение из файла
        }

        public CardRepository(IEnumerable<Card> collection)
        {
            Collection = new List<Card>(collection);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
