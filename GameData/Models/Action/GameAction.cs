using System;
using GameData.Enums;
using GameData.Models.ActionParameters;

namespace GameData.Models.Action
{
    public class GameAction
    {
        public string Name { get; }

        public int ID { get; }

        public ActionParameterType ParameterType { get; }

        public ActionType Type { get; }

        public Action<TableCondition,object,object,IActionParameter> Action { get; }

        public GameAction(string name, int id, ActionParameterType parameterType, ActionType actionType,
            Action<TableCondition, object, object, IActionParameter> action)
        {
            Name = name;
            ID = id;
            ParameterType = parameterType;
            Type = actionType;
            Action = action;
        }
    }
}
