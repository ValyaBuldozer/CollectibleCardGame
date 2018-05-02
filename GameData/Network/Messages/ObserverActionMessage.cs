using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Observer;

namespace GameData.Network.Messages
{
    public class ObserverActionMessage : IContent
    {
        public ObserverAction ObserverAction { set; get; }

        public object AnswerData { set; get; }
    }
}
