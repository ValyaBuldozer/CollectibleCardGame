using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Cards;

namespace GameData.Network.Messages
{
    public class SetDeckMessage : IContent
    {
        public Fraction Fraction { set; get; }

        public int[] DeckIDs { set; get; }

        public int HeroCardId { set; get; }

        public object AnswerData { set; get; }
    }
}
