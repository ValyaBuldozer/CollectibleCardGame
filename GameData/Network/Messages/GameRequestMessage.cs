﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;

namespace GameData.Network.Messages
{
    public class GameRequestMessage :IContent
    {
        public List<int> CardDeckIdList { set; get; }
        
        public UnitCard HeroUnitCard { set; get; }

        public object AnswerData { set; get; }
    }
}
