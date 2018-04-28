using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models.PlayerTurn
{
    public class UnitAttackPlayerTurn : PlayerTurn
    {
        public Unit Unit { set; get; }

        public Unit TargetUnit { set; get; }

        public UnitAttackPlayerTurn(Player sender, Unit unit, Unit targetUnit)
        {
            Sender = sender;
            Unit = unit;
            TargetUnit = targetUnit;
            Type = PlayerTurnType.UnitAttack;
        }
    }
}
