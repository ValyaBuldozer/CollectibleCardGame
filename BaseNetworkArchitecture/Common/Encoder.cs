using System.Text;

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