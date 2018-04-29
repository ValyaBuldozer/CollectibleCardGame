﻿using System;
using GameData.Controllers.Table;
using GameData.Enums;
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
        public Action<InActionTableController,object,Unit,int> Action { get; }

        public GameAction(string name, int id,string description, ActionParameterType parameterType,
            Action<InActionTableController, object, Unit, int> action)
        {
            Name = name;
            ID = id;
            ParameterType = parameterType;
            Action = action;
            Description = description;
        }

        public GameAction() { }
    }
}
