using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Table;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public class UnitAttackPlayerTurnHandler : IPlayerTurnHandler<UnitAttackPlayerTurn>
    {
        private readonly IUnitDispatcher _unitDispatcher;

        public UnitAttackPlayerTurnHandler(IUnitDispatcher unitDispatcher)
        {
            _unitDispatcher = unitDispatcher;
        }

        public void Execute(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            if(playerTurn is UnitAttackPlayerTurn unitTurn)
                _unitDispatcher.HandleAttack(unitTurn.Unit,unitTurn.TargetUnit);
        }
    }
}
