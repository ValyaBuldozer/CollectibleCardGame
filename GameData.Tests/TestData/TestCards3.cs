﻿using System.Collections.Generic;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;

namespace GameData.Tests.TestData
{
    internal class TestCards3
    {
        public TestCards3()
        {
            FirstHero = new UnitCard
            {
                ID = 1,
                Name = "Hero1",
                BaseHP = 30,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
            SecondHero = new UnitCard
            {
                ID = 2,
                Name = "Hero2",
                BaseHP = 30,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
        }

        public UnitCard FirstHero { set; get; }

        public UnitCard SecondHero { set; get; }

        public Stack<Card> FirstRandomDeck => new Stack<Card>(new List<Card>
        {
            new SpellCard
            {
                ID = 9,
                Name = "SpellCard3_5",
                Description = "Бахает случайного вражеского юнита на 5",
                ActionInfo = new CardActionInfo
                {
                    ActionId = 8,
                    ParameterType = ActionParameterType.Damage,
                    ParameterValue = 5
                }
            },
            new UnitCard
            {
                ID = 1,
                Name = "UnitCard1_5/5",
                BaseHP = 5,
                AttackPriority = 1,
                BaseAttack = 5,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard
            {
                ID = 2,
                Name = "UnitCard2_3/6",
                BaseHP = 6,
                AttackPriority = 1,
                BaseAttack = 3,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard
            {
                ID = 3,
                Name = "UnitCard3_4/5",
                BaseHP = 5,
                AttackPriority = 1,
                BaseAttack = 4,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard
            {
                ID = 4,
                Name = "UnitCard4_4/4",
                BaseHP = 4,
                AttackPriority = 1,
                BaseAttack = 4,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard
            {
                ID = 5,
                Name = "UnitCard5_6/4",
                BaseHP = 4,
                AttackPriority = 1,
                BaseAttack = 6,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard
            {
                ID = 6,
                Name = "UnitCard6_6/6",
                BaseHP = 6,
                AttackPriority = 1,
                BaseAttack = 6,
                Cost = 0,
                Description = "Test"
            },
            new SpellCard
            {
                ID = 7,
                Name = "SpellCard1_5",
                Description = "Бахает всех врагов на 5",
                ActionInfo = new CardActionInfo
                {
                    ActionId = 3,
                    ParameterType = ActionParameterType.Damage,
                    ParameterValue = 5
                }
            },
            new SpellCard
            {
                ID = 8,
                Name = "SpellCard2_5",
                Description = "Бахает вражеского героя на 5",
                ActionInfo = new CardActionInfo
                {
                    ActionId = 9,
                    ParameterType = ActionParameterType.Damage,
                    ParameterValue = 5
                }
            }
        });
    }
}