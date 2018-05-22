using GameData.Models.PlayerTurn;

namespace GameData.Network.Messages
{
    public class PlayerTurnMessage : IContent
    {
        public PlayerTurn PlayerTurn { set; get; }

        public object AnswerData { set; get; }
    }
}