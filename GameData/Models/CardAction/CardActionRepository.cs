using System.Collections.Generic;
using System.Linq;
using GameData.Enums;

namespace GameData.Models.CardAction
{
    public class CardActionRepository
    {
        public IEnumerable<CardActionEntity> CardActions { get; private set; }

        public CardActionRepository()
        {
            CardActions = new List<CardActionEntity>()
            {
                new CardActionEntity()
                {
                    ID = 1,
                    Type = ActionType.Other,
                    Action = (delegate(TableCondition condition, object sender, object target, object param)
                    {
                        
                    })
                }
            };
        }

        public CardActionEntity FindActionByID(int id)
        {
            return CardActions.FirstOrDefault(a => a.ID == id);
        }
    }
}
