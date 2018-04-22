using GameData.Models.ActionParameters;

namespace GameData.Models.Action
{
    public class GameActionInfo
    {
        public IActionParameter Parameter { set; get; }

        public GameAction Action { set; get; }
    }
}
