using System;
using GameData.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameData.Models.Observer
{
    public class ObserverAction : EventArgs
    {
        /// <summary>
        /// Тип события изменения модели
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ObserverActionType Type { protected set; get; }

        /// <summary>
        /// Игрок, которому необходимо сообщить об изменении (null если всем)
        /// </summary>
        [JsonIgnore]
        public Player TargetPlayer { set; get; }
    }
}
