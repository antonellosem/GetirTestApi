using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace PgadCommunication.CustomMessageEncoder
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadTextMessageEncoderFactory : MessageEncoderFactory
    {
        private readonly MessageEncoder encoder;
        private readonly MessageVersion version;
        private readonly string mediaType;
        private readonly string charSet;

        internal PgadTextMessageEncoderFactory(string mediaType, string charSet,
            MessageVersion version)
        {
            this.version = version;
            this.mediaType = mediaType;
            this.charSet = charSet;
            encoder = new PgadTextMessageEncoder(this);
        }

        public override MessageEncoder Encoder => this.encoder;

        public override MessageVersion MessageVersion => this.version;

        internal string MediaType => this.mediaType;

        internal string CharSet => this.charSet;
    }
}
