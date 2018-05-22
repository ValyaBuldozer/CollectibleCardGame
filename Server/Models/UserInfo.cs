using System;
using GameData.Enums;

namespace Server.Models
{
    public class UserInfo
    {
        public int Id { set; get; }
        public int GameLoseCount { set; get; }
        public int GameWinCount { set; get; }
        public string NorthDeck { set; get; }
        public string SouthDeck { set; get; }
        public string DarkDeck { set; get; }

        public string GetDeck(Fraction fraction)
        {
            switch (fraction)
            {
                case Fraction.Common:
                    return null;
                case Fraction.North:
                    return NorthDeck;
                case Fraction.South:
                    return SouthDeck;
                case Fraction.Dark:
                    return DarkDeck;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fraction), fraction, null);
            }
        }

        protected bool Equals(UserInfo other)
        {
            return string.Equals(NorthDeck, other.NorthDeck) && string.Equals(SouthDeck, other.SouthDeck)
                                                             && string.Equals(DarkDeck, other.DarkDeck) &&
                                                             Equals(GameWinCount, other.GameWinCount)
                                                             && Equals(GameLoseCount, other.GameLoseCount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UserInfo) obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}