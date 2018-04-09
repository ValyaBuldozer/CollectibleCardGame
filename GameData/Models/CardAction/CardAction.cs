using Newtonsoft.Json;

namespace GameData.Models.CardAction
{
    public class CardAction
    {
        public int EntityID { set; get; }

        public object Parameter { set; get; }

        [JsonIgnore]
        public CardActionEntity Entity { set; get; }
    }
}
