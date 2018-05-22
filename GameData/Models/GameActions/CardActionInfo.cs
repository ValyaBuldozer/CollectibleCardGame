using GameData.Enums;

namespace GameData.Models.Action
{
    public class CardActionInfo
    {
        public int ActionId { set; get; }

        public int ParameterValue { set; get; }

        public ActionParameterType ParameterType { set; get; }

        public bool IsTargeted { set; get; }

        public CardActionInfo ShallowCopy()
        {
            return (CardActionInfo) MemberwiseClone();
        }
    }
}