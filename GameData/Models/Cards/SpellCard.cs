using GameData.Models.Action;

namespace GameData.Models.Cards
{
    public class SpellCard : Card
    {
        public CardActionInfo ActionInfo { set; get; }

        public override Card DeepCopy()
        {
            var other = (SpellCard) MemberwiseClone();
            other.ActionInfo = ActionInfo?.ShallowCopy();
            return other;
        }
    }
}