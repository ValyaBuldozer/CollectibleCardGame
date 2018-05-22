namespace GameData.Network.Messages
{
    public class UserInfoRequestMessage : IContent
    {
        public string Username { set; get; }
        public object AnswerData { set; get; }
    }
}