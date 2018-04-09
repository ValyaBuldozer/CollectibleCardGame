using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controller.PlayerHandler
{
    public interface IPlayerTurnHandler<T>
    {
        void Execute(IPlayerTurn playerTurn, TableCondition tableCondition);
    }
}
