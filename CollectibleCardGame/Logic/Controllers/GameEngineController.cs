using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Models;
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.UserControls;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;
using GameData.Models.Units;
using Unity.Attributes;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame.Logic.Controllers
{
    public class GameEngineController
    {
        private readonly GameEngineViewModel _gameViewModel;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly CurrentUserService _userService;
        private readonly IDataRepositoryController<Entity> _entityRepositoryController;
        private readonly IDataRepositoryController<Card> _cardRepositoryController;
        private readonly Lazy<GameController> _gameControllerLazy;
        private readonly ILogger _logger;

        [InjectionConstructor]
        public GameEngineController(GameEngineViewModel gameViewModel, MainWindowViewModel mainViewModel,
            CurrentUserService userService, ILogger logger,
            IDataRepositoryController<Entity> entityRepositoryController,
            IDataRepositoryController<Card> cardRepositoryController,
            Lazy<GameController> gameControllerLazy)
        {
            _gameViewModel = gameViewModel;
            _mainWindowViewModel = mainViewModel;
            _userService = userService;
            _entityRepositoryController = entityRepositoryController;
            _cardRepositoryController = cardRepositoryController;
            _gameControllerLazy = gameControllerLazy;
            _logger = logger;
        }

        public void HandleObserverAction(GameStartObserverAction action)
        {
            _entityRepositoryController.Add(action.FirstPlayer);
            _entityRepositoryController.Add(action.SecondPlayer);
            _entityRepositoryController.Add(action.FirstPlayer.HeroUnit);
            _entityRepositoryController.Add(action.SecondPlayer.HeroUnit);

            action.FirstPlayer.TableUnits.ForEach(u=>_entityRepositoryController.Add(u));
            action.SecondPlayer.TableUnits.ForEach(u => _entityRepositoryController.Add(u));
            action.FirstPlayer.HandCards.ForEach(c => _entityRepositoryController.Add(c));
            action.SecondPlayer.HandCards.ForEach(c => _entityRepositoryController.Add(c));

            _gameViewModel.CurrentDispatcher.Invoke(() =>
            {
                //здесь проблема - экшен приходит ПЕРЕД логином - костыль
                if (action.FirstPlayer.Username == _userService.Username ||
                    string.IsNullOrEmpty(_userService.Username))
                {
                    _gameViewModel.Player = action.FirstPlayer;
                    _gameViewModel.EnemyPlayer = action.SecondPlayer;
                }
                else
                {
                    _gameViewModel.Player = action.SecondPlayer;
                    _gameViewModel.EnemyPlayer = action.FirstPlayer;
                }

                if (!string.IsNullOrEmpty(action.CurrentPlayerUsername))
                    _gameViewModel.CurrentPlayerUsername = action.CurrentPlayerUsername;
            });
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
                if (action.ToPlayerUsername == _userService.Username)
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
                if (action.PlayerUsername == _userService.Username)
                {
                    action.Unit.Player = _gameViewModel.Player;
                    _gameViewModel.PlayerUnits.Add(new UnitViewModel(action.Unit));
                }
                else
                {
                    action.Unit.Player = _gameViewModel.EnemyPlayer;
                    _gameViewModel.EnemyUnits.Add(new UnitViewModel(action.Unit){IsCanAttack = false});
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
                _gameViewModel.CurrentDispatcher.Invoke(() =>
                {
                    if (action.PlayerUsername == _userService.Username)
                        _gameViewModel.PlayerViewModel.PlayerMana = action.PlayerMana;
                    else
                        _gameViewModel.EnemyViewModel.PlayerMana = action.PlayerMana;
                });
        }

        public void HandleObserverAction(EntityStateChangeObserverAction action)
        {
            var entity = _entityRepositoryController.GetById(action.EntityId);

            if (!(action.EntityState is Unit unit)) return;
            if (!(entity is Unit oldUnitState)) return;

            _gameViewModel.CurrentDispatcher.Invoke(()=>
            {
                oldUnitState.State.SetState = unit.State;
            });
        }

        public void HandleObserverAction(UnitDeathObserverAction action)
        {
            var entity = _entityRepositoryController.GetById(action.Unit.EntityId);

            if(!(entity is Unit unit)) return;

            _gameViewModel.CurrentDispatcher.Invoke(() =>
            {
                var unitViewModel = _gameViewModel.PlayerUnits.FirstOrDefault(
                    vm => vm.BaseUnit.EntityId == unit.EntityId);

                if (unitViewModel != null)
                {
                    _gameViewModel.PlayerUnits.Remove(unitViewModel);
                    return;
                }

                unitViewModel = _gameViewModel.EnemyUnits.FirstOrDefault(
                    vm => vm.BaseUnit.EntityId == unit.EntityId);

                if (unitViewModel != null)
                    _gameViewModel.EnemyUnits.Remove(unitViewModel);
            });
        }

        public void HandleObserverAction(GameEndObserverAction action)
        {
            _gameViewModel.CurrentDispatcher.Invoke(() =>
            {
                _logger.LogAndPrint(action.WinnerUsername == 
                                _userService.Username ? "ВЫ ПОБЕДИЛИ!!!" : "ВЫ ПРОИГРАЛИ!!!");

                _gameViewModel.Clear();
                _entityRepositoryController.ClearRepository();
                _gameControllerLazy.Value.EndGame();
            });
        }
    }
}
