using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Network.Messages
{
    public class GameStartMessage : IContent
    {
        public TableCondition TableCondition { set; get; }
        public string EnemyUsername { set; get; }
        public object AnswerData { set; get; }
    }
}
