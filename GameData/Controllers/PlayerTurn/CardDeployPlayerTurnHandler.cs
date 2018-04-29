using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public class CardDeployPlayerTurnHandler : IPlayerTurnHandler<CardDeployPlayerTurn>
    {
        private readonly ICardDeployDispatcher _cardDeployDispatcher;

        public CardDeployPlayerTurnHandler(ICardDeployDispatcher cardDeployDispatcher)
        {
            _cardDeployDispatcher = cardDeployDispatcher;
        }

        public void Execute(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            if (playerTurn is CardDeployPlayerTurn deployTurn)
                _cardDeployDispatcher.CardDeployRequest(deployTurn.Card, deployTurn.Sender,
                    deployTurn.ActionTarget);
        }
    }
}
