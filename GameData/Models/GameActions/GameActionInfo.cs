using GameData.Enums;

namespace GameData.Models.Action
{
    public class GameActionInfo
    {
        public int Parameter { set; get; }

        public GameAction Action { set; get; }

        public ActionParameterType ParameterType { set; get; }

        protected bool Equals(GameActionInfo other)
        {
            return Parameter == other.Parameter && Action.ID == other.Action.ID
                                                && ParameterType == other.ParameterType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GameActionInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Parameter;
                hashCode = (hashCode * 397) ^ (Action != null ? Action.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) ParameterType;
                return hashCode;
            }
        }
    }
}