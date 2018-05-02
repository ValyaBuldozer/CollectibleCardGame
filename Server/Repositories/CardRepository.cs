using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;
using Server.Models;

namespace Server.Repositories
{
    public class CardRepository
    {
        public List<Card> Collection { set; get; }

        public CardRepository()
        {
            Collection = new List<Card>
            {
                new UnitCard()
                {
                    ID = 0,
                    Name = "TestUnit1_1",
                    Cost = 1,
                    CanBePlayedOnEnemyTurn = false,
                    AttackPriority = 1,
                    BaseHP = 1,
                    BaseAttack = 1,
                },
                new UnitCard()
                {
                    ID = 1,
                    Name = "TestUnit3_3",
                    Cost = 3,
                    CanBePlayedOnEnemyTurn = false,
                    AttackPriority = 1,
                    BaseHP = 3,
                    BaseAttack = 3,
                },
                new UnitCard()
                {
                    ID = 2,
                    Name = "TestUnitBAaction",
                    Cost = 1,
                    CanBePlayedOnEnemyTurn = false,
                    AttackPriority = 1,
                    BaseHP = 1,
                    BaseAttack = 1,
                    BattleCryActionInfo = new CardActionInfo()
                    {
                        ActionId = 1,
                        ParameterType = ActionParameterType.Damage,
                        ParameterValue = 1
                    }
                },
                new UnitCard()
                {
                    ID = 3,
                    Name = "TestUnit10_10",
                    Cost = 10,
                    CanBePlayedOnEnemyTurn = false,
                    AttackPriority = 2,
                    BaseHP = 10,
                    BaseAttack = 10,
                },
                new SpellCard()
                {
                    ID = 4,
                    Name = "TestSpell1",
                    Cost = 1,
                    CanBePlayedOnEnemyTurn = false,
                    ActionInfo = new CardActionInfo()
                    {
                        ActionId = 2,
                        ParameterType = ActionParameterType.Damage,
                        ParameterValue = 2
                    }
                },
                new SpellCard()
                {
                    ID = 5,
                    Name = "TestSpell2",
                    Cost = 1,
                    CanBePlayedOnEnemyTurn = false,
                    ActionInfo = new CardActionInfo()
                    {
                        ActionId = 3,
                        ParameterType = ActionParameterType.Empty,
                        ParameterValue = 0
                    }
                }
            };
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
