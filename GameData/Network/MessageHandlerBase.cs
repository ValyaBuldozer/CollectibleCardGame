using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Network.Messages;

namespace GameData.Network
{
    public class MessageHandlerBase<T> : IMessageHandler
    {
        public virtual IContent Execute(IContent content,object sender)
        {
            return content;
        }
    }
}
