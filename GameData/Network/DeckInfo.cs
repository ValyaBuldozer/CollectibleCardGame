using GameData.Enums;
using GameData.Models.Cards;

namespace GameData.Network
{
    public class DeckInfo
    {
        public Fraction Fraction { set; get; }

        public int[] DeckIds { set; get; }

        public UnitCard HeroCard { set; get; }
    }
}
