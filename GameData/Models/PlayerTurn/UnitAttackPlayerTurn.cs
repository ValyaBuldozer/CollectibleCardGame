using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models.PlayerTurn
{
    public class UnitAttackPlayerTurn : IPlayerTurn
    {
        public PlayerInfo Sender { set; get; }

        public Unit Unit { set; get; }

        public Unit TargetUnit { set; get; }

    }
}
