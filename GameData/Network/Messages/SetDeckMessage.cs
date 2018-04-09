using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;

namespace GameData.Network.Messages
{
    public class SetDeckMessage : IContent
    {
        public Fraction Fraction { set; get; }
        public string DeckString { set; get; }
        public object AnswerData { set; get; }
    }
}
