using GameData.Models;
using GameData.Models.PlayerTurn;

namespace GameData.Controllers.PlayerTurn
{
    public interface IPlayerTurnHandler<T>
    {
        void Execute(IPlayerTurn playerTurn, TableCondition tableCondition);
    }
}
