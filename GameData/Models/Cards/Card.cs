using GameData.Enums;

namespace GameData.Models.Cards
{
    public class Card : Entity
    {
        protected Card()
        {
            EntityType = EntityType.Card;
        }

        public int ID { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public int Cost { set; get; }

        public bool CanBePlayedOnEnemyTurn { set; get; }

        public string ImagePath { set; get; }

        public Fraction Fraction { set; get; }

        protected bool Equals(Card other)
        {
            return ID == other.ID && string.Equals(Name, other.Name) &&
                   string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Card) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ID;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual Card ShallowCopy()
        {
            return (Card) MemberwiseClone();
        }

        public virtual Card DeepCopy()
        {
            return (Card) MemberwiseClone();
        }
    }
}