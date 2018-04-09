namespace GameData.Network.Messages
{
    public class RegistrationMessage :IContent
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public object AnswerData { set; get; }
    }
}
