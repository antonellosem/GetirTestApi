using System.IdentityModel.Selectors;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using PgadCommunication.Utility;

namespace PgadCommunication.CustomMessageEncoder
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadClientCredentials : ClientCredentials
    {
        X509Certificate2 clientSigningCert;
        X509Certificate2 serviceEncryptCert;

        public PgadClientCredentials()
        { }

        public PgadClientCredentials(string Username, int TitolareSistema)
        {
            clientSigningCert = Settings.Instance.Config[Username + "-" + TitolareSistema].CNCPrivateKey;
            serviceEncryptCert = Settings.Instance.Config[Username + "-" + TitolareSistema].AAMSPlublickey;
        }

        protected PgadClientCredentials(PgadClientCredentials other) : base(other)
        {
            this.clientSigningCert = other.clientSigningCert;
            this.serviceEncryptCert = other.serviceEncryptCert;
        }

        public X509Certificate2 ClientSigningCertificate
        {
            get
            {
                return this.clientSigningCert;
            }
            set
            {
                this.clientSigningCert = value;
            }
        }

        public X509Certificate2 ServiceEncryptCert
        {
            get
            {
                return this.serviceEncryptCert;
            }
            set
            {
                this.serviceEncryptCert = value;
            }
        }

        public override SecurityTokenManager CreateSecurityTokenManager()
        {
            return new PgadClientCredentialsSecurityTokenManager(this);
        }

        protected override ClientCredentials CloneCore()
        {
            return new PgadClientCredentials(this);
        }
    }

}
