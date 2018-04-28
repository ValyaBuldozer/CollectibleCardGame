using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.ObserverAction;
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

    public class GameEndEventArgs : BaseObserverActionEventArgs
    {
        public GameEndReason Reason { get; }

        public string WinnerUsername { get; }

        public GameEndEventArgs(GameEndReason reason, string username)
        {
            Reason = reason;
            WinnerUsername = username;
        }
    }

    public class UnitRecievedDamageEventArgs : EventArgs
    {
        public Unit Unit { get; }

        public int Damage { get; }

        public UnitRecievedDamageEventArgs(Unit unit, int damage)
        {
            Unit = unit;
            Damage = damage;
        }
    }

    public class UnitDiesEventargs : EventArgs
    {
        public Unit Unit { get; }

        public UnitDiesEventargs(Unit unit)
        {
            Unit = unit;
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

    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; }

        public bool IsSystemError { get; }

        public Player Player { get; }

        public ErrorEventArgs(string message,bool isSystemError,Player player = null)
        {
            Message = message;
            IsSystemError = isSystemError;
            Player = player;
        }
    }
}
