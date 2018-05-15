﻿using System;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.PlayerTurn;
using GameData.Models.Units;

namespace GameData.Controllers.PlayerTurn
{
    public interface IPlayerTurnValidator
    {
        Models.PlayerTurn.PlayerTurn Validate(Models.PlayerTurn.PlayerTurn playerTurn);
        event EventHandler<ErrorEventArgs> ValidateError;
    }


    public class PlayerTurnValidator : IPlayerTurnValidator
    {
        private readonly TableCondition _tableCondition;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;
        private readonly IDataRepositoryController<Entity> _entityRepositoryController;

        public event EventHandler<ErrorEventArgs> ValidateError; 

        public PlayerTurnValidator(TableCondition tableCondition,IPlayerTurnDispatcher playerTurnDispatcher,
            IDataRepositoryController<Entity> entityRepositoryController)
        {
            _tableCondition = tableCondition;
            _playerTurnDispatcher = playerTurnDispatcher;
            _entityRepositoryController = entityRepositoryController;
        }

        public Models.PlayerTurn.PlayerTurn Validate(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            switch (playerTurn.GetType().Name)
            {
                case nameof(CardDeployPlayerTurn):
                    return Validate((CardDeployPlayerTurn)playerTurn);
                case nameof(UnitAttackPlayerTurn):
                    return Validate((UnitAttackPlayerTurn)playerTurn);
                case nameof(EndPlayerTurn):
                    return Validate((EndPlayerTurn)playerTurn);
                default:
                    throw new InvalidOperationException("No such player turn was found");
            }
        }

        public CardDeployPlayerTurn Validate(CardDeployPlayerTurn playerTurn)
        {
            if (!(_entityRepositoryController.GetById(playerTurn.Card.EntityId) is Card card &&
                  _entityRepositoryController.GetById(playerTurn.Sender.EntityId) is Player sender))
            {
                RunValidateError(new ErrorEventArgs("EntityID not found", true, playerTurn.Sender));
                return null;
            }

            Unit target = null;

            if (playerTurn.ActionTarget != null)
                target = _entityRepositoryController.GetById(playerTurn.ActionTarget.EntityId) as Unit;

            if (!playerTurn.Sender.HandCards.Exists(c => c.Equals(card)))
            {
                RunValidateError(new ErrorEventArgs("No such card in hand",true,playerTurn.Sender));
                return null;
            }

            if (_playerTurnDispatcher.CurrentPlayer.Username != sender.Username || 
                card.CanBePlayedOnEnemyTurn)
            {
                RunValidateError(new ErrorEventArgs("Нельзя разыграть эту карту во время хода протиника",
                    false,playerTurn.Sender));
                return null;
            }

            if (sender.Mana.Current < card.Cost)
            {
                RunValidateError(new ErrorEventArgs("Недостаточно маны", true,playerTurn.Sender));
                return null;
            }

            return new CardDeployPlayerTurn(sender,card,target);
        }

        public UnitAttackPlayerTurn Validate(UnitAttackPlayerTurn playerTurn)
        {
            if (!(_entityRepositoryController.GetById(playerTurn.Unit.EntityId) is Unit senderUnit &&
                  _entityRepositoryController.GetById(playerTurn.TargetUnit.EntityId) is Unit target &&
                  _entityRepositoryController.GetById(playerTurn.Sender.EntityId) is Player sender))
            {
                RunValidateError(new ErrorEventArgs("EntityID not found", true, playerTurn.Sender));
                return null;
            }

            if (!sender.TableUnits.Exists(u => u.Equals(senderUnit)))
            {
                RunValidateError(new ErrorEventArgs("No such unit found", true,playerTurn.Sender));
                return null;
            }
            
            if(_playerTurnDispatcher.CurrentPlayer.Username!=sender.Username)
            {
                RunValidateError(new ErrorEventArgs("Нельзя атаковать во время хода протиника",
                    false,playerTurn.Sender));
                return null;
            }

            if (!senderUnit.State.CanAttack)
            {
                RunValidateError(new ErrorEventArgs("Этот юнит сейчас не может атаковать",
                    false, playerTurn.Sender));
                return null;
            }

            if (target.State.AttackPriority == 0)
            //атака маскировки
            {
                RunValidateError(new ErrorEventArgs("Нельзя атаковать существо с маскировкой",
                    false, playerTurn.Sender));
                return null;
            }

            if (target.State.AttackPriority != 2 && target.Player.TableUnits.Exists(u => u.State.AttackPriority == 2))
                //есть провокатор у противника
            {
                RunValidateError(new ErrorEventArgs("Нужно атаковать провокатора",
                    false, playerTurn.Sender));
                return null;
            }

            if (target.Player.Username == senderUnit.Player.Username)
            {
                RunValidateError(new ErrorEventArgs("Self attack",
                    true, playerTurn.Sender));
                return null;
            }

            //todo : проверка провокаторов и тд

            return new UnitAttackPlayerTurn(sender,senderUnit,target);
        }

        public EndPlayerTurn Validate(EndPlayerTurn playerTurn)
        {
            if (!(_entityRepositoryController.GetById(playerTurn.Sender.EntityId) is Player sender))
            {
                RunValidateError(new ErrorEventArgs("EntityID not found", true, playerTurn.Sender));
                return null;
            }

            if (_playerTurnDispatcher.CurrentPlayer.Username != sender.Username)
            {
                RunValidateError(new ErrorEventArgs("Not your turn",true,playerTurn.Sender));
                return null;
            }

            return new EndPlayerTurn(sender);
        }

        private void RunValidateError(ErrorEventArgs e)
        {
            ValidateError?.Invoke(this,e);
        }
    }
}
