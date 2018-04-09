using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;
using Newtonsoft.Json;

namespace GameData.Models
{
    public class PlayerInfo
    {
        public string Username { set; get; }

        public Unit HeroUnit { set; get; }

        public int DeckCardsCount { set; get; }

        public List<Card> HandCards { set; get; }

        public List<Unit> TableUnits { set; get; }

        [JsonIgnore]
        public List<Card> DeckCards { set; get; }

        protected bool Equals(PlayerInfo other)
        {
            return string.Equals(Username, other.Username) && Equals(HeroUnit, other.HeroUnit) &&
                   DeckCardsCount == other.DeckCardsCount && Equals(HandCards, other.HandCards) &&
                   Equals(TableUnits, other.TableUnits) && Equals(DeckCards, other.DeckCards);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Username != null ? Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HeroUnit != null ? HeroUnit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DeckCardsCount;
                hashCode = (hashCode * 397) ^ (HandCards != null ? HandCards.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TableUnits != null ? TableUnits.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DeckCards != null ? DeckCards.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayerInfo) obj);
        }
    }
}
