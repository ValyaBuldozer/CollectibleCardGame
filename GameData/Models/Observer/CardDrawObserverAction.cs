using GameData.Enums;
using GameData.Models.Cards;

namespace GameData.Models.Observer
{
    public class CardDrawObserverAction : ObserverAction
    {
        /// <summary>
        /// Кому была дана карта
        /// </summary>
        public Player ToPlayer { set; get; }

        public Card Card { set; get; }

        public CardDrawObserverAction(Card card, Player toPlayer)
        {
            Type = ObserverActionType.CardDraw;
            ToPlayer = toPlayer;
            Card = card;
        }

        public CardDrawObserverAction()
        {
            Type = ObserverActionType.CardDraw;
        }
    }
}
