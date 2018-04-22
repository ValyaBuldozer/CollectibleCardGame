﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;

namespace GameData.Tests.TestData
{
    public class TestCards
    {
        public UnitCard FirstCard { set; get; }

        public UnitCard SecondCard { set; get; }

        public TestCards()
        {
            FirstCard = new UnitCard()
            {
                ID = 1,
                Name = "test1",
                BaseHP = 30,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
            SecondCard = new UnitCard()
            {
                ID = 2,
                Name = "test2",
                BaseHP = 40,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
        }
    }
}
