using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Logic.Controllers;
using GameData.Enums;
using GameData.Models.Observer;
using GameData.Network;
using GameData.Network.Messages;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class ObserverActionMessageHandler : MessageHandlerBase<ObserverActionMessage>
    {
        private readonly GameEngineController _gameEngineController;

        public ObserverActionMessageHandler(GameEngineController gameEngineController)
        {
            _gameEngineController = gameEngineController;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if(!(content is ObserverActionMessage message))
                throw new InvalidOperationException();

            switch (message.ObserverAction.Type)
            {
                case ObserverActionType.GameStart:
                    _gameEngineController.HandleObserverAction(
                        (GameStartObserverAction)message.ObserverAction);
                    break;
                case ObserverActionType.CardDeploy:
                    _gameEngineController.HandleObserverAction(
                        (CardDeployObserverAction)message.ObserverAction);
                    break;
                case ObserverActionType.CardDraw:
                    _gameEngineController.HandleObserverAction(
                        (CardDrawObserverAction)message.ObserverAction);
                    break;
                case ObserverActionType.UnitSpawn:
                    break;
                case ObserverActionType.UnitDeath:
                    break;
                case ObserverActionType.UnitStateChange:
                    break;
                case ObserverActionType.GameAction:
                    break;
                case ObserverActionType.TurnStart:
                    _gameEngineController.HandleObserverAction(
                        (TurnStartObserverAction)message.ObserverAction);
                    break;
                case ObserverActionType.TurnEnd:
                    break;
                case ObserverActionType.Error:
                    _gameEngineController.HandleObserverAction(
                        (ErrorObserverAction)message.ObserverAction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}
