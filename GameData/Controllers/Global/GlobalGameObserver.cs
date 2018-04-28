using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.PlayerTurn;
using GameData.Controllers.Table;
using GameData.Models;

namespace GameData.Controllers.Global
{
    public class GlobalGameObserver
    {
        private readonly IGameStateController _gameStateController;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;
        private readonly ICardDeployDispatcher _cardDeployDispatcher;
        private readonly ICardDrawController _cardDrawController;
        private readonly IUnitDispatcher _unitDispatcher;
        private readonly IGameActionController _gameActionController;
        private readonly IPlayerTurnValidator _playerTurnValidator;

        public GlobalGameObserver(IGameStateController gameStateController,
            IPlayerTurnDispatcher playerTurnDispatcher,
            ICardDrawController cardDrawController,
            ICardDeployDispatcher cardDeployDispatcher,
            IUnitDispatcher unitDispatcher,
            IGameActionController gameActionController,
            IPlayerTurnValidator playerTurnValidator)
        {
            _gameStateController = gameStateController;
            _playerTurnDispatcher = playerTurnDispatcher;
            _cardDeployDispatcher = cardDeployDispatcher;
            _cardDrawController = cardDrawController;
            _unitDispatcher = unitDispatcher;
            _gameActionController = gameActionController;
            _playerTurnValidator = playerTurnValidator;

            _playerTurnValidator.ValidateError += ObserverEventHandler;
            _gameStateController.GameEnd += ObserverEventHandler;
        }

        private void ObserverEventHandler(object sender, GameEndEventArgs e)
        {

        }

        private void ObserverEventHandler(object sender, ErrorEventArgs e)
        {

        }
    }
}
