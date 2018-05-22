using GameData.Enums;

namespace GameData.Network.Messages
{
    public class GameRequestMessage : IContent
    {
        public Fraction Fraction { set; get; }

        public object AnswerData { set; get; }
    }
}