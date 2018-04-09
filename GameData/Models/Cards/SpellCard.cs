using GameData.Enums;

namespace GameData.Models.Cards
{
    public class SpellCard : Card
    {
        public CardAction.CardAction OnCardPlayedAction { set; get; }

        public SpellTargetType TargetType { set; get; }
    }
}
