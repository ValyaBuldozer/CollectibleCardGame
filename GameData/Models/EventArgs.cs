using System;
using GameData.Enums;
using GameData.Models.Observer;
using GameData.Models.Units;

namespace GameData.Models
{
    public class ZeroHpEventArgs : EventArgs
    {
        public ZeroHpEventArgs(Unit unit)
        {
            Unit = unit;
        }

        public Unit Unit { get; }
    }

    public class GameEndEventArgs : EventArgs
    {
        public GameEndEventArgs(GameEndReason reason, string username)
        {
            Reason = reason;
            WinnerUsername = username;
        }

        public GameEndReason Reason { get; }

        public string WinnerUsername { get; }
    }

    public class UnitRecievedDamageEventArgs : EventArgs
    {
        public UnitRecievedDamageEventArgs(Unit unit, int damage)
        {
            Unit = unit;
            Damage = damage;
        }

        public Unit Unit { get; }

        public int Damage { get; }
    }

    public class UnitDiesEventargs : EventArgs
    {
        public UnitDiesEventargs(Unit unit)
        {
            Unit = unit;
        }

        public Unit Unit { get; }
    }

    public class HeroUnitDiedEventArgs : EventArgs
    {
        public HeroUnitDiedEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
    }

    public class PlayerTurnStartEventArgs : EventArgs
    {
        public PlayerTurnStartEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
    }

    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(string message, bool isSystemError, Player player = null)
        {
            Message = message;
            IsSystemError = isSystemError;
            Player = player;
        }

        public string Message { get; }

        public bool IsSystemError { get; }

        public Player Player { get; }
    }

    public class PlayerManaChanged : EventArgs
    {
        public PlayerManaChanged(int manaChange, Player player)
        {
            ManaChange = manaChange;
            Player = player;
        }

        public int ManaChange { get; }

        public Player Player { get; }
    }

    public class ObserverActionAddedEventArgs : EventArgs
    {
        public ObserverActionAddedEventArgs(ObserverAction item)
        {
            Item = item;
        }

        public ObserverAction Item { get; }
    }

    public class PlayerManaChangeEventArgs : EventArgs
    {
        public PlayerManaChangeEventArgs(PlayerState state)
        {
            PlayerState = state;
        }

        public PlayerState PlayerState { get; }
    }
}