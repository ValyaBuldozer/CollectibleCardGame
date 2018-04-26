using GameData.Enums;
using GameData.Models.ActionParameters;

namespace GameData.Models.Action
{
    public class GameActionInfo
    {
        public int Parameter { set; get; }

        public GameAction Action { set; get; }

        public ActionParameterType ParameterType { set; get; }
    }
}
