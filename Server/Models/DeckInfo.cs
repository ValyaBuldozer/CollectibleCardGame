using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Cards;

namespace Server.Models
{
    public class DeckInfo
    {
        public Fraction Fraction { set; get; }

        public int[] DeckIds { set; get; }

        public UnitCard HeroCard { set; get; }
    }
}
