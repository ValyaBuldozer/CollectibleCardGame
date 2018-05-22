using GameData.Enums;

namespace GameData.Models.PlayerTurn
{
    public class EndPlayerTurn : PlayerTurn
    {
        public EndPlayerTurn(Player sender)
        {
            Sender = sender;
            Type = PlayerTurnType.TurnEnd;
        }
    }
}