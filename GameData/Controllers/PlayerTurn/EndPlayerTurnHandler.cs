using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public class EndPlayerTurnHandler : IPlayerTurnHandler<EndPlayerTurn>
    {
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;
        private readonly IPlayerTurnValidator _validator;

        public EndPlayerTurnHandler(IPlayerTurnDispatcher playerTurnDispatcher,
            IPlayerTurnValidator validator)
        {
            _playerTurnDispatcher = playerTurnDispatcher;
            _validator = validator;
        }

        public void Execute(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            if(_validator.Validate(playerTurn) != null)
                _playerTurnDispatcher.NextPlayer();
        }
    }
}
