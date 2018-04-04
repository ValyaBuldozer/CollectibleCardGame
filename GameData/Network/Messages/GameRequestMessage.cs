using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Network.Messages
{
    public class GameRequestMessage :IContent
    {
        //todo : добавить инфы или вообще убрать этот тип

        public object AnswerData { set; get; }
    }
}
