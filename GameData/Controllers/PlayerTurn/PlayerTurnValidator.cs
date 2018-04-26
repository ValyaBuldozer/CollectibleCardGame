using System;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public class PlayerTurnValidator
    {
        public bool Validate(IPlayerTurn playerTurn, TableCondition tableCondition)
        {
            switch (playerTurn.GetType().Name)
            {
                case nameof(CardPlayedPlayerTurn):
                    return false;
                case nameof(UnitAttackPlayerTurn):
                    return false;
                case nameof(EndPlayerTurn):
                    return false;
                default:
                    throw new InvalidOperationException("No such player turn was found");
            }
        }

        public bool Validate(CardPlayedPlayerTurn playerTurn, TableCondition tableCondition)
        {
            if(playerTurn == null || tableCondition ==null)
                throw new NullReferenceException();

            throw new NotImplementedException();
        }
    }
}
