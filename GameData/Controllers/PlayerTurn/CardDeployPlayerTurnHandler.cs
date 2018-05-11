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
        private readonly IPlayerTurnValidator _validator;

        public CardDeployPlayerTurnHandler(ICardDeployDispatcher cardDeployDispatcher,
            IPlayerTurnValidator validator)
        {
            _cardDeployDispatcher = cardDeployDispatcher;
            _validator = validator;
        }

        public void Execute(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            var validatedTurn = _validator.Validate(playerTurn);

            if (validatedTurn != null && validatedTurn is CardDeployPlayerTurn deployTurn)
                _cardDeployDispatcher.CardDeployRequest(deployTurn.Card, deployTurn.Sender,
                    deployTurn.ActionTarget);
        }
    }
}
