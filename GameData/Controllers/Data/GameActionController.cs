using System;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Observer;
using GameData.Models.Units;

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

        public GameActionController(IDataRepositoryController<GameAction> repositoryController,
            IActionTableControlller tableController)
        {
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

            //if(action.ParameterType != cardInfo.ParameterType)
            //    throw new InvalidOperationException();

            return new GameActionInfo
            {
                Action = action,
                ParameterType = action.ParameterType,
                Parameter = cardInfo.ParameterValue
            };
        }

        public void ExecuteAction(GameActionInfo actionInfo, Entity sender, Unit target)
        {
            if (actionInfo == null)
                return;

            try
            {
                actionInfo.Action?.Action.Invoke(_tableController, sender, target, actionInfo.Parameter);
                ActionTrigerred?.Invoke(this, new GameActionTriggerObserverAction(actionInfo.Action.ID,
                    sender.EntityId, target?.EntityId));
            }
            catch (NotImplementedException e)
            {
                //todo : включить обычный эксепшен
                //любые исключение в экшене
            }
        }

        public void ExecuteAction(CardActionInfo actionInfo, Entity sender, Unit target)
        {
            var action = GetGameActionInfo(actionInfo);

            if (action == null) return;

            try
            {
                GetGameActionInfo(actionInfo).Action.Action.Invoke(
                    _tableController, sender, target, actionInfo.ParameterValue);
                ActionTrigerred?.Invoke(this, new GameActionTriggerObserverAction(action.Action.ID,
                    sender.EntityId, target?.EntityId));
            }
            catch (NotImplementedException e)
            {
                //любые исключение в экшене
            }
        }
    }
}