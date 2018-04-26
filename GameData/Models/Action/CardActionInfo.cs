﻿using GameData.Enums;

namespace GameData.Models.Action
{
    public class CardActionInfo
    {
        public int ActionId { set; get; }

        public int ParameterValue { set; get; }

        public ActionParameterType ParameterType { set; get; }
    }
}
