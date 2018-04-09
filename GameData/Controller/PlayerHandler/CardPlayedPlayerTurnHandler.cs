using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controller.PlayerHandler
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
