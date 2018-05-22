namespace GameData.Network.Messages
{
    public class ErrorMessage : IContent
    {
        public string ErrorInfo { set; get; }

        public object AnswerData { set; get; }
    }
}