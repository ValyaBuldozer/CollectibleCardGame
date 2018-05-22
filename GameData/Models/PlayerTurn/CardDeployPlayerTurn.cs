using GameData.Enums;
using GameData.Models.Cards;
using GameData.Models.Units;

namespace GameData.Models.PlayerTurn
{
    public class CardDeployPlayerTurn : PlayerTurn
    {
        public CardDeployPlayerTurn(Player sender, Card card, Unit target = null)
        {
            Sender = sender;
            Card = card;
            ActionTarget = target;
            Type = PlayerTurnType.CardDeploy;
        }

        public Card Card { set; get; }

        public Unit ActionTarget { set; get; }
    }
}