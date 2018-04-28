using GameData.Models.Cards;

namespace GameData.Models.Observer
{
    public class CardDrawObserverAction : ObserverAction
    {
        public Card Card { set; get; }

        public CardDrawObserverAction(Card card)
        {
            Card = card;
        }

        public CardDrawObserverAction() { }
    }
}
