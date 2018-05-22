using GameData.Controllers.Data;
using GameData.Controllers.PlayerTurn;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Observer;

namespace GameData.Controllers.Global
{
    public class GlobalGameObserver
    {
        //private readonly IGameStateController _gameStateController;
        //private readonly IPlayerTurnDispatcher _playerTurnDispatcher;
        //private readonly ICardDeployDispatcher _cardDeployDispatcher;
        //private readonly ICardDrawController _cardDrawController;
        //private readonly IUnitDispatcher _unitDispatcher;
        //private readonly IGameActionController _gameActionController;
        //private readonly IPlayerTurnValidator _playerTurnValidator;
        private readonly ObserverActionRepositoryController _repositoryController;

        public GlobalGameObserver(IGameStateController gameStateController,
            IPlayerTurnDispatcher playerTurnDispatcher,
            ICardDrawController cardDrawController,
            ICardDeployDispatcher cardDeployDispatcher,
            IUnitDispatcher unitDispatcher,
            IGameActionController gameActionController,
            IPlayerTurnValidator playerTurnValidator,
            ObserverActionRepositoryController repositoryController)
        {
            //_gameStateController = gameStateController;
            //_playerTurnDispatcher = playerTurnDispatcher;
            //_cardDeployDispatcher = cardDeployDispatcher;
            //_cardDrawController = cardDrawController;
            //_unitDispatcher = unitDispatcher;
            //_gameActionController = gameActionController;
            //_playerTurnValidator = playerTurnValidator;
            _repositoryController = repositoryController;


            playerTurnValidator.ValidateError += ObserverEventHandler;

            gameStateController.GameEnd += ObserverEventHandler;
            gameStateController.GameStart += ObserverEventHandler;
            gameStateController.PlayerStateChanged += ObserverEventHandler;

            cardDrawController.OnCardDraw += ObserverEventHandler;
            cardDeployDispatcher.OnCardDeploy += ObserverEventHandler;

            gameActionController.ActionTrigerred += ObserverEventHandler;

            unitDispatcher.OnUnitSpawn += ObserverEventHandler;
            unitDispatcher.OnUnitDeath += ObserverEventHandler;
            unitDispatcher.OnUnitStateChange += ObserverEventHandler;

            playerTurnDispatcher.TurnStart += ObserverEventHandler;
        }

        private void ObserverEventHandler(object sender, GameEndEventArgs e)
        {
            _repositoryController.Add(new GameEndObserverAction(e.WinnerUsername, e.Reason));
        }

        private void ObserverEventHandler(object sender, ErrorEventArgs e)
        {
            _repositoryController.Add(new ErrorObserverAction(e.Message, e.Player));
        }

        private void ObserverEventHandler(object sender, ObserverAction e)
        {
            _repositoryController.Add(e);
        }
    }
}