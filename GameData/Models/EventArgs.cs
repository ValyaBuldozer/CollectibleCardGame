using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models
{
    public class ZeroHpEventArgs : EventArgs
    {
        public Unit Unit { get; }

        public ZeroHpEventArgs(Unit unit)
        {
            Unit = unit;
        }
    }

    public class GameEndEventArgs : EventArgs
    {
        public GameEndReason Reason { get; }

        public string WinnerUsername { get; }

        public GameEndEventArgs(GameEndReason reason, string username)
        {
            Reason = reason;
            WinnerUsername = username;
        }
    }

    public class HeroUnitDiedEventArgs : EventArgs
    {
        public Player Player { get; }

        public HeroUnitDiedEventArgs(Player player)
        {
            Player = player;
        }
    }

    public class PlayerTurnStartEventArgs : EventArgs
    {
        public Player Player { get; }

        public PlayerTurnStartEventArgs(Player player)
        {
            Player = player;
        }
    }
}
