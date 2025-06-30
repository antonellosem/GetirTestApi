using System;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Linq;
using PgadCommon.Enum;
using PgadCommunication.Model;


namespace PgadCommunication.CustomMessageEncoder
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadTextMessageEncoder : MessageEncoder
    {
        private PgadTextMessageEncoderFactory factory;
        private XmlWriterSettings writerSettings;
        private string contentType;
        private int idTransazioneInvio;

        public PgadTextMessageEncoder(PgadTextMessageEncoderFactory factory)
        {
            this.factory = factory;
            this.writerSettings = new XmlWriterSettings { Encoding = Encoding.GetEncoding(factory.CharSet) };
            this.contentType = $"{this.factory.MediaType}; charset={this.writerSettings.Encoding.HeaderName}";
        }

        public override bool IsContentTypeSupported(string contentType)
        {
            if (base.IsContentTypeSupported(contentType))
            {
                return true;
            }
            if (contentType.Length == this.MediaType.Length)
            {
                return contentType.Equals(this.MediaType, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                if (contentType.StartsWith(this.MediaType, StringComparison.OrdinalIgnoreCase)
                    && (contentType[this.MediaType.Length] == ';'))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ContentType => this.contentType;

        public override string MediaType => factory.MediaType;

        public override MessageVersion MessageVersion => this.factory.MessageVersion;

        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            byte[] msgContents = new byte[buffer.Count];
            Array.Copy(buffer.Array, buffer.Offset, msgContents, 0, msgContents.Length);
            bufferManager.ReturnBuffer(buffer.Array);

            //Recupero la risposta da inserire nel transazionale
            ASCIIEncoding enc = new ASCIIEncoding();
            string msg = enc.GetString(msgContents);

            //Salvo la risposta nel db transazionale

            PgadTransactionStore.StoreTransaction(msg, Direction.Inwards, idTransazioneInvio);

            XDocument xmlMessage = XDocument.Parse(msg);
            XNamespace sec = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
            XNamespace saml = "urn:oasis:names:tc:SAML:1.0:assertion";
            XNamespace wsu = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

            xmlMessage.Descendants(sec + "UsernameToken").Remove();
            xmlMessage.Descendants(saml + "Assertion").Remove();
            xmlMessage.Descendants(sec + "BinarySecurityToken").Remove();
            xmlMessage.Descendants(wsu + "Timestamp").Remove();
            xmlMessage.Descendants(sec + "Security").Remove();

            return Message.CreateMessage(xmlMessage.CreateReader(), int.MaxValue, this.MessageVersion);
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            XmlReader reader = XmlReader.Create(stream);
            Message message = Message.CreateMessage(reader, maxSizeOfHeaders, this.MessageVersion);
            return message;
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            //Salvo il messaggio nel transazionale
            idTransazioneInvio = PgadTransactionStore.StoreTransaction(message.ToString(), Direction.Outwards, 0);

            MemoryStream stream = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(stream, this.writerSettings);
            message.WriteMessage(writer);
            writer.Close();

            byte[] messageBytes = stream.GetBuffer();
            int messageLength = (int)stream.Position;
            stream.Close();

            int totalLength = messageLength + messageOffset;
            byte[] totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(messageBytes, 0, totalBytes, messageOffset, messageLength);

            ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, messageOffset, messageLength);
            return byteArray;
        }

        public override void WriteMessage(Message message, Stream stream)
        {
            XmlWriter writer = XmlWriter.Create(stream, this.writerSettings);
            message.WriteMessage(writer);
            writer.Close();
        }
    }
}
