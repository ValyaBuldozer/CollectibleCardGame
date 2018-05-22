using System;
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
            if (!(content is ObserverActionMessage message))
                throw new InvalidOperationException();

            switch (message.ObserverAction.Type)
            {
                case ObserverActionType.GameStart:
                    _gameEngineController.HandleObserverAction(
                        (GameStartObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.CardDeploy:
                    _gameEngineController.HandleObserverAction(
                        (CardDeployObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.CardDraw:
                    _gameEngineController.HandleObserverAction(
                        (CardDrawObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.UnitSpawn:
                    _gameEngineController.HandleObserverAction(
                        (UnitSpawnObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.PlayerStateChange:
                    _gameEngineController.HandleObserverAction(
                        (PlayerStateChangesObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.UnitDeath:
                    _gameEngineController.HandleObserverAction(
                        (UnitDeathObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.UnitStateChange:
                    break;
                case ObserverActionType.GameAction:
                    break;
                case ObserverActionType.TurnStart:
                    _gameEngineController.HandleObserverAction(
                        (TurnStartObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.TurnEnd:
                    break;
                case ObserverActionType.Error:
                    _gameEngineController.HandleObserverAction(
                        (ErrorObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.EntityStateChange:
                    _gameEngineController.HandleObserverAction(
                        (EntityStateChangeObserverAction) message.ObserverAction);
                    break;
                case ObserverActionType.GameEnd:
                    _gameEngineController.HandleObserverAction(
                        (GameEndObserverAction) message.ObserverAction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}