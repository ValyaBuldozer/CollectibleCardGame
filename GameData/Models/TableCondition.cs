using GameData.Enums;

namespace GameData.Models
{
    public class TableCondition
    {
        public PlayerInfo FirstPlayer { set; get; }

        public PlayerInfo SecondPlayer { set; get; }

        public PlayerSequencing PlayerSequencing { set; get; }

        protected bool Equals(TableCondition other)
        {
            return Equals(FirstPlayer, other.FirstPlayer) && Equals(SecondPlayer, other.SecondPlayer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TableCondition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FirstPlayer != null ? FirstPlayer.GetHashCode() : 0) * 397) ^
                       (SecondPlayer != null ? SecondPlayer.GetHashCode() : 0);
            }
        }
    }
}
