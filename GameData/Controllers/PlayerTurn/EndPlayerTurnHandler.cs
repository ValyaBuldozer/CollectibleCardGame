using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Global;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public class EndPlayerTurnHandler : IPlayerTurnHandler<EndPlayerTurn>
    {
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;

        public EndPlayerTurnHandler(IPlayerTurnDispatcher playerTurnDispatcher)
        {
            _playerTurnDispatcher = playerTurnDispatcher;
        }

        public void Execute(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            if(playerTurn is EndPlayerTurn endTurn)
                _playerTurnDispatcher.NextPlayer();
        }
    }
}
