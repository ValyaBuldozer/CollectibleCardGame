namespace BaseNetworkArchitecture.Common
{
    public class NetworkMessage
    {
        public NetworkMessage()
        {
            Encoder = new Encoder();
        }

        public NetworkMessage(string content)
        {
            Encoder = new Encoder();
            Content = content;
        }

        public string Content { set; get; }
        public string Length { set; get; }
        public Encoder Encoder { set; get; }
    }
}