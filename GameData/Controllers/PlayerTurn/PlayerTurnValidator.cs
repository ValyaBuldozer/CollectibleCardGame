using System;
using GameData.Controllers.Global;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public interface IPlayerTurnValidator
    {
        bool Validate(Models.PlayerTurn.PlayerTurn playerTurn);
        event EventHandler<ErrorEventArgs> ValidateError;
    }


    public class PlayerTurnValidator : IPlayerTurnValidator
    {
        private readonly TableCondition _tableCondition;
        private readonly IPlayerTurnDispatcher _playerTurnDispatcher;

        public event EventHandler<ErrorEventArgs> ValidateError; 

        public PlayerTurnValidator(TableCondition tableCondition,IPlayerTurnDispatcher playerTurnDispatcher)
        {
            _tableCondition = tableCondition;
            _playerTurnDispatcher = playerTurnDispatcher;
        }

        public bool Validate(Models.PlayerTurn.PlayerTurn playerTurn)
        {
            switch (playerTurn.GetType().Name)
            {
                case nameof(CardDeployPlayerTurn):
                    return Validate((CardDeployPlayerTurn)playerTurn);
                case nameof(UnitAttackPlayerTurn):
                    return Validate((UnitAttackPlayerTurn)playerTurn);
                case nameof(EndPlayerTurnHandler):
                    return Validate((EndPlayerTurn)playerTurn);
                default:
                    throw new InvalidOperationException("No such player turn was found");
            }
        }

        public bool Validate(CardDeployPlayerTurn playerTurn)
        {
            if (!playerTurn.Sender.HandCards.Exists(c => c.Equals(playerTurn.Card)))
            {
                RunValidateError(new ErrorEventArgs("No such card in hand",true,playerTurn.Sender));
                return false;
            }

            if (!_playerTurnDispatcher.CurrentPlayer.Equals(playerTurn.Sender) || 
                !playerTurn.Card.CanBePlayedOnEnemyTurn)
            {
                RunValidateError(new ErrorEventArgs("Нельзя разыграть эту карту во время хода протиника",
                    false,playerTurn.Sender));
                return false;
            }

            if (playerTurn.Sender.Mana.Current < playerTurn.Card.Cost)
            {
                RunValidateError(new ErrorEventArgs("Недостаточно маны", true,playerTurn.Sender));
                return false;
            }

            return true;
        }

        public bool Validate(UnitAttackPlayerTurn playerTurn)
        {
            if (!playerTurn.Sender.TableUnits.Exists(u => u.Equals(playerTurn.Unit)))
            {
                RunValidateError(new ErrorEventArgs("No such unit found", true,playerTurn.Sender));
                return false;
            }
            
            if(!_playerTurnDispatcher.CurrentPlayer.Equals(playerTurn.Sender))
            {
                RunValidateError(new ErrorEventArgs("Нельзя атаковать во время хода протиника",
                    false,playerTurn.Sender));
                return false;
            }

            //todo : проверка провокаторов и тд

            return true;
        }

        public bool Validate(EndPlayerTurn playerTurn)
        {
            if (!_playerTurnDispatcher.CurrentPlayer.Equals(playerTurn.Sender))
            {
                RunValidateError(new ErrorEventArgs("Not your turn",true,playerTurn.Sender));
                return false;
            }

            return true;
        }

        private void RunValidateError(ErrorEventArgs e)
        {
            ValidateError?.Invoke(this,e);
        }
    }
}
