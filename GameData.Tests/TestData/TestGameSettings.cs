using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models;

namespace GameData.Tests.TestData
{
    public class TestGameSettings
    {
        public static GameSettings Get => new GameSettings()
        {
            IsPlayerTurnTimerEnabled = false,
            MaxDeckCardsCount = 50,
            PlayerHandCardsMaxCount = 10,
            PlayerTurnInterval = 10000,
            PlayersCount = 2,
            PlayerTableUnitsMaxCount = 10,
            MaxPlayerMana = 10
        };
    }
}
