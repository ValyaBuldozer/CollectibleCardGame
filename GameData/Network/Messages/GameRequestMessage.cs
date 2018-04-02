using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Network.Messages
{
    public class GameRequestMessage :IContent
    {
        public object AnswerData { set; get; }
    }
}
