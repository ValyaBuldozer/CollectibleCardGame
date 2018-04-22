using System;
using GameData.Enums;
using GameData.Models.ActionParameters;
using GameData.Models.Units;

namespace GameData.Models.Action
{
    public class GameAction
    {
        public int ID { get; }

        public string Name { get; }

        public string Description { get; }

        public ActionParameterType ParameterType { get; }

        /// <summary>
        /// Action 
        /// </summary>
        public Action<TableCondition,object,Unit,int> Action { get; }

        public GameAction(string name, int id,string description, ActionParameterType parameterType,
            Action<TableCondition, object, Unit, int> action)
        {
            Name = name;
            ID = id;
            ParameterType = parameterType;
            Action = action;
            Description = description;
        }
    }
}
