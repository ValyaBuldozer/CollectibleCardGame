using System;
using GameData.Enums;

namespace GameData.Models.CardAction
{
    public class CardActionEntity
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public ActionType Type { set; get; }

        public ActionParameterType ParameterType { set; get; }

        /// <summary>
        /// TableCondition - table
        /// First object - sender
        /// Second object - target
        /// Third object - action parameter
        /// </summary>
        public Action<TableCondition,object,object,object> Action { set; get; }
    }
}
