using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Models;
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;
using Unity.Attributes;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame.Logic.Controllers
{
    public class GameEngineController
    {
        private readonly GameEngineViewModel _gameViewModel;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly CurrentUser _user;
        private readonly IDataRepositoryController<Entity> _entityRepositoryController;
        private readonly IDataRepositoryController<Card> _cardRepositoryController;
        private readonly ILogger _logger;

        [InjectionConstructor]
        public GameEngineController(GameEngineViewModel gameViewModel, MainWindowViewModel mainViewModel,
            CurrentUser user, ILogger logger,
            IDataRepositoryController<Entity> entityRepositoryController,
            IDataRepositoryController<Card> cardRepositoryController)
        {
            _gameViewModel = gameViewModel;
            _mainWindowViewModel = mainViewModel;
            _user = user;
            _entityRepositoryController = entityRepositoryController;
            _cardRepositoryController = cardRepositoryController;
            _logger = logger;
        }

        public void HandleObserverAction(GameStartObserverAction action)
        {
            _entityRepositoryController.Add(action.FirstPlayer);
            _entityRepositoryController.Add(action.SecondPlayer);
            _gameViewModel.Player =
                action.FirstPlayer.Username == _user.Username ? action.FirstPlayer : action.SecondPlayer;
            _gameViewModel.EnemyPlayer =
                action.FirstPlayer.Username == _user.Username ? action.SecondPlayer : action.FirstPlayer;
        }

        public void HandleObserverAction(ErrorObserverAction action)
        {
            if(!action.IsSystemError)
                _logger?.LogAndPrint(action.ErrorMessage);
        }

        public void HandleObserverAction(CardDrawObserverAction action)
        {
            var card = _cardRepositoryController.GetById(action.Card.ID);
            if(card == null)
                return;
            card.EntityId = action.Card.EntityId;
            _entityRepositoryController.Add(card);

            _gameViewModel.CurrentDispatcher.Invoke(() =>
            {
                if (action.ToPlayerUsername == _user.Username)
                    _gameViewModel.PlayerCards.Add(new CardViewModel(card));
                else
                    _gameViewModel.EnemyCards.Add(new CardViewModel(card));
            });
        }

        public void HandleObserverAction(CardDeployObserverAction action)
        {
            _gameViewModel.CurrentDispatcher.Invoke(() =>
            {
                var card = _gameViewModel.PlayerCards.FirstOrDefault(
                    c => c.Card.EntityId == action.Card.EntityId);

                if (card != null)
                {
                    _gameViewModel.PlayerCards.Remove(card);
                    return;
                }

                card = _gameViewModel.EnemyCards.FirstOrDefault(
                    c => c.Card.EntityId == action.Card.EntityId);

                if (card != null)
                    _gameViewModel.EnemyCards.Remove(card);
            });

        }

        public void HandleObserverAction(UnitSpawnObserverAction action)
        {
            _entityRepositoryController.Add(action.Unit);

            _gameViewModel.CurrentDispatcher.Invoke(() =>
            {
                if (action.PlayerUsername == _user.Username)
                {
                    action.Unit.Player = _gameViewModel.Player;
                    _gameViewModel.PlayerUnits.Add(new UnitViewModel(action.Unit));
                }
                else
                {
                    action.Unit.Player = _gameViewModel.EnemyPlayer;
                    _gameViewModel.EnemyUnits.Add(new UnitViewModel(action.Unit));
                }
            });
        }

        public void HandleObserverAction(TurnStartObserverAction action)
        {
            _gameViewModel.CurrentDispatcher.Invoke(() =>
            {
                _gameViewModel.CurrentPlayerUsername = action.CurrentPlayerUsername;
            });
        }

        public void HandleObserverAction(PlayerStateChangesObserverAction action)
        {
            if (action.PlayerUsername == _user.Username)
                _gameViewModel.CurrentDispatcher.Invoke(() =>
                {
                    _gameViewModel.PlayerMana = action.PlayerMana;
                });
        }
    }
}
