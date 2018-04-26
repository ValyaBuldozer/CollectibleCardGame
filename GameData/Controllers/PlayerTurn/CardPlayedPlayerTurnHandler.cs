using System;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public class CardPlayedPlayerTurnHandler : IPlayerTurnHandler<CardPlayedPlayerTurn>
    {
        public void Execute(IPlayerTurn playerTurn, TableCondition tableCondition)
        {
            if(!(playerTurn is CardPlayedPlayerTurn))
                throw new InvalidOperationException("Invalid PlayerTurn type");


        }
    }
}
