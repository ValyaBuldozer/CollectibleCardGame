using GameData.Models.Observer;

namespace GameData.Network.Messages
{
    public class ObserverActionMessage : IContent
    {
        public ObserverAction ObserverAction { set; get; }

        public object AnswerData { set; get; }
    }
}