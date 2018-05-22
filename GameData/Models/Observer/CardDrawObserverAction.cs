using GameData.Enums;
using GameData.Models.Cards;

namespace GameData.Models.Observer
{
    public class CardDrawObserverAction : ObserverAction
    {
        public CardDrawObserverAction(Card card, string toPlayerUsername)
        {
            Type = ObserverActionType.CardDraw;
            ToPlayerUsername = toPlayerUsername;
            Card = card;
        }

        public CardDrawObserverAction()
        {
            Type = ObserverActionType.CardDraw;
        }

        /// <summary>
        ///     Кому была дана карта
        /// </summary>
        public string ToPlayerUsername { set; get; }

        public Card Card { set; get; }
    }
}