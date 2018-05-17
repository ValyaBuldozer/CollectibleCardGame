using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Observer;
using GameData.Models.Units;
using Exception = System.Exception;

namespace GameData.Controllers.Data
{
    public interface IGameActionController
    {
        GameActionInfo GetGameActionInfo(CardActionInfo cardInfo);
        void ExecuteAction(GameActionInfo actionInfo, Entity sender, Unit target);
        void ExecuteAction(CardActionInfo actionInfo, Entity sender, Unit target);
        event EventHandler<GameActionTriggerObserverAction> ActionTrigerred;
    }

    public class GameActionController : IGameActionController
    {
        private readonly IDataRepositoryController<GameAction> _repositoryController;
        private readonly IActionTableControlller _tableController;
        private readonly ILogger _logger;

        public GameActionController(IDataRepositoryController<GameAction> repositoryController,
             IActionTableControlller tableController,ILogger logger)
        {
            _logger = logger;
            _repositoryController = repositoryController;
            _tableController = tableController;
        }

        public event EventHandler<GameActionTriggerObserverAction> ActionTrigerred;

        public GameActionInfo GetGameActionInfo(CardActionInfo cardInfo)
        {
            if (cardInfo == null)
                return null;

            var action = _repositoryController.GetById(cardInfo.ActionId);
            if (action == null)
                return null;

            if(action.ParameterType != cardInfo.ParameterType)
                throw new InvalidOperationException();

            return new GameActionInfo()
            {
                Action = action,
                ParameterType = action.ParameterType,
                Parameter = cardInfo.ParameterValue
            };
        }

        public void ExecuteAction(GameActionInfo actionInfo, Entity sender, Unit target)
        {
            if(actionInfo == null)
                return;

            try
            {
                actionInfo.Action?.Action.Invoke(_tableController, sender, target, actionInfo.Parameter);
                ActionTrigerred?.Invoke(this, new GameActionTriggerObserverAction(actionInfo.Action.ID,
                    sender.EntityId, target?.EntityId));
            }
            catch (Exception e)
            {
                //любые исключение в экшене
                _logger?.Log(e);
            }
        }

        public void ExecuteAction(CardActionInfo actionInfo, Entity sender, Unit target)
        {
            var action = GetGameActionInfo(actionInfo);

            if(action == null) return;

            try
            {
                GetGameActionInfo(actionInfo).Action.Action.Invoke(
                    _tableController, sender, target, actionInfo.ParameterValue);
                ActionTrigerred?.Invoke(this,new GameActionTriggerObserverAction(action.Action.ID,
                    sender.EntityId,target?.EntityId));
            }
            catch (Exception e)
            {
                //любые исключение в экшене
                _logger?.Log(e);
            }
        }
    }
}
