using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Common
{
    public class Message
    {
        public string Content { set; get; }
        public string Length { set; get; }
        public Encoder Encoder { set; get; }

        public Message()
        {
            Encoder = new Encoder();
        }

        public Message(string content)
        {
            Encoder = new Encoder();
            Content = content;
        }
    }
}