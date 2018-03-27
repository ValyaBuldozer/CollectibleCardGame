using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Common
{
    public class Encoder
    {
        public byte[] GetBytes(string encodingString)
        {
            return Encoding.UTF8.GetBytes(encodingString);
        }

        public string GetString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}