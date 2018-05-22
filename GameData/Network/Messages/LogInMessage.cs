namespace GameData.Network.Messages
{
    public class LogInMessage : IContent
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public object AnswerData { set; get; }

        protected bool Equals(LogInMessage other)
        {
            return string.Equals(Username, other.Username) && string.Equals(Password, other.Password) &&
                   Equals(AnswerData, other.AnswerData);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LogInMessage) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Username != null ? Username.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnswerData != null ? AnswerData.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}