using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Network.Messages
{
    public class ErrorMessage : IContent
    {
        public string ErrorInfo { set; get; }

        public object AnswerData { set; get; }
    }
}
