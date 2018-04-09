using System.Collections.Generic;

namespace GameData.Models.Cards
{
    public class UnitCard : Card
    {
        public int Attack { set; get; }

        public int HP { set; get; }

        public List<CardAction.CardAction> OnCardPlayedActions { set; get; }

        public CardAction.CardAction OnUnitDiesAction { set; get; }

        public CardAction.CardAction OnUnitAttacksAction { set; get; }

        public CardAction.CardAction OnUnitWasAttackedAction { set; get; }
    }
}
