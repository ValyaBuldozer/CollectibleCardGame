using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Models;
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.Frames;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Observer;
using Unity.Attributes;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame.Logic.Controllers
{
    public class GameEngineController
    {
        private readonly GameEngineViewModel _gameViewModel;
        private readonly CurrentUser _user;
        private readonly IDataRepositoryController<Entity> _entityRepositoryController;
        private readonly ILogger _logger;

        [InjectionConstructor]
        public GameEngineController(GameEngineViewModel gameViewModel,CurrentUser user,
            IDataRepositoryController<Entity> entityRepositoryController,ILogger logger)
        {
            _gameViewModel = gameViewModel;
            _user = user;
            _entityRepositoryController = entityRepositoryController;
            _logger = logger;
        }

        public void HandleObserverAction(GameStartObserverAction action)
        {
            _entityRepositoryController.Add(action.FirstPlayer);
            _entityRepositoryController.Add(action.SecondPlayer);
        }

        public void HandleObserverAction(ErrorObserverAction action)
        {
            if(!action.IsSystemError)
                _logger?.LogAndPrint(action.ErrorMessage);
        }

        public void HandleObserverAction(CardDrawObserverAction action)
        {
            _entityRepositoryController.Add(action.Card);

            if(action.ToPlayer.Username == _user.Username)
                _gameViewModel.PlayerCards.Add(new CardViewModel(action.Card));
            else
                _gameViewModel.EnemyCards.Add(new CardViewModel(action.Card));
        }
    }
}
