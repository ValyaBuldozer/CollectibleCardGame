using GameData.Controllers.Table;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public class UnitAttackPlayerTurnHandler : IPlayerTurnHandler<UnitAttackPlayerTurn>
    {
        private readonly IUnitDispatcher _unitDispatcher;
        private readonly IPlayerTurnValidator _validator;

        public UnitAttackPlayerTurnHandler(IUnitDispatcher unitDispatcher,
            IPlayerTurnValidator validator)
        {
            _unitDispatcher = unitDispatcher;
            _validator = validator;
        }

        public void Execute(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            var validatedTurn = _validator.Validate(playerTurn);

            if (validatedTurn != null && validatedTurn is UnitAttackPlayerTurn unitTurn)
                _unitDispatcher.HandleAttack(unitTurn.Unit, unitTurn.TargetUnit);
        }
    }
}