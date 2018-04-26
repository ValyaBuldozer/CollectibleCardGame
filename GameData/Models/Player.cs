using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;
using GameData.Models.Units;
using Newtonsoft.Json;

namespace GameData.Models
{
    public class Player
    {
        public string Username { set; get; }

        public HeroUnit HeroUnit { get; }

        public int DeckCardsCount { set; get; }

        public List<Card> HandCards { set; get; }

        public List<Unit> TableUnits { set; get; }

        public PlayerMana Mana { set; get; }

        public Player(UnitCard hero)
        {
            HeroUnit = new HeroUnit(this,hero);
            HandCards = new List<Card>();
            TableUnits = new List<Unit>();
            Mana = new PlayerMana();
        }

        protected bool Equals(Player other)
        {
            return string.Equals(Username, other.Username) && Equals(HeroUnit, other.HeroUnit) &&
                   DeckCardsCount == other.DeckCardsCount && Equals(HandCards, other.HandCards) &&
                   Equals(TableUnits, other.TableUnits);
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
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }
    }
}
