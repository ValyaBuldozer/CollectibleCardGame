using System;
using GameData.Controllers.Table;
using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models.Action
{
    public class GameAction
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public ActionParameterType ParameterType { set; get; }

        public bool IsTargeted { set; get; }

        /// <summary>
        /// Action - контроллер стола, sender, Цель, параметр
        /// </summary>
        public Action<IActionTableControlller,Entity,Unit,int> Action { set; get; }

        public GameAction(string name, int id,string description, ActionParameterType parameterType,
            Action<IActionTableControlller, Entity, Unit, int> action,bool isTargeted=false)
        {
            Name = name;
            ID = id;
            ParameterType = parameterType;
            Action = action;
            Description = description;
            IsTargeted = isTargeted;
        }

        public GameAction() { }
    }
}
