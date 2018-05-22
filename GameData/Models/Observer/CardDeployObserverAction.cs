using GameData.Enums;
using GameData.Models.Cards;
using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class CardDeployObserverAction : ObserverAction
    {
        public CardDeployObserverAction(Card card, Unit gameActionTarget)
        {
            Type = ObserverActionType.CardDeploy;
            Card = card;
            GameActionTarget = gameActionTarget;
        }

        public CardDeployObserverAction()
        {
            Type = ObserverActionType.CardDeploy;
        }

        public Card Card { set; get; }

        public Unit GameActionTarget { set; get; }
    }
}