using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Units;

namespace GameData.Controllers.Data
{
    public interface IGameActionController
    {
        GameActionInfo GetGameActionInfo(CardActionInfo cardInfo);
        void ExecuteAction(GameActionInfo actionInfo, object sender, Unit target);
        void ExecuteAction(CardActionInfo actionInfo, object sender, Unit target);
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

        public GameActionInfo GetGameActionInfo(CardActionInfo cardInfo)
        {
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

        public void ExecuteAction(GameActionInfo actionInfo, object sender, Unit target)
        {
            actionInfo.Action?.Action.Invoke(_tableController,sender,target,actionInfo.Parameter);
        }

        public void ExecuteAction(CardActionInfo actionInfo, object sender, Unit target)
        {
            GetGameActionInfo(actionInfo)?.Action.Action.Invoke(
                _tableController, sender, target, actionInfo.ParameterValue);
        }
    }
}
