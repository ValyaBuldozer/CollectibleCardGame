namespace GameData.Network.Messages
{
    public class LogInMessage : IContent
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public object AnswerData { set; get; }
    }
}
