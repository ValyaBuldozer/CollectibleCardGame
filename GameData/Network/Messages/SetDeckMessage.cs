using GameData.Enums;

namespace GameData.Network.Messages
{
    public class SetDeckMessage : IContent
    {
        public Fraction Fraction { set; get; }

        public int[] DeckIDs { set; get; }

        public int HeroCardId { set; get; }

        public object AnswerData { set; get; }
    }
}