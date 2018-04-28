using System;
using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public interface IPlayerTurnHandler<T>
    {
        void Execute(Models.PlayerTurn.PlayerTurn playerTurn);
    }
}
