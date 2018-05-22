using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models;

namespace GameData.Tests.TestData
{
    public class TestTableCondition
    {
        private TestCards _testCards;

        public TableCondition GetFirstCondition => new TableCondition()
        {
            Players = new List<Player>()
            {
                new Player(_testCards.FirstCard)
                {
                    Username = "FirstTestUser",
                    State = new PlayerState(){Base = 3,Current = 3}
                },
                new Player(_testCards.SecondCard)
                {
                    Username = "SecondTestUser",
                    State = new PlayerState(){Base = 3,Current = 3}
                }
            }
        };

        public TestTableCondition()
        {
            _testCards = new TestCards();
        }
    }
}
