using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models.PlayerTurn
{
    public class UnitAttackPlayerTurn : PlayerTurn
    {
        public UnitAttackPlayerTurn(Player sender, Unit unit, Unit targetUnit)
        {
            Sender = sender;
            Unit = unit;
            TargetUnit = targetUnit;
            Type = PlayerTurnType.UnitAttack;
        }

        public Unit Unit { set; get; }

        public Unit TargetUnit { set; get; }
    }
}