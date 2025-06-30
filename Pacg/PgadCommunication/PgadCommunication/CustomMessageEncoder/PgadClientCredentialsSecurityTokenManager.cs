using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;

namespace PgadCommunication.CustomMessageEncoder
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadClientCredentialsSecurityTokenManager : ClientCredentialsSecurityTokenManager
    {
        PgadClientCredentials credentials;

        public PgadClientCredentialsSecurityTokenManager(PgadClientCredentials credentials)
            : base(credentials)
        {
            this.credentials = credentials;
        }

        public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement requirement)
        {
            SecurityTokenProvider result = null;

            if (requirement.TokenType == SecurityTokenTypes.X509Certificate)
            {
                MessageDirection direction = requirement.GetProperty<MessageDirection>(ServiceModelSecurityTokenRequirement.MessageDirectionProperty);
                if (direction == MessageDirection.Output)
                {
                    if (requirement.KeyUsage == SecurityKeyUsage.Signature)
                    {
                        result = new X509SecurityTokenProvider(this.credentials.ClientSigningCertificate);
                    }
                    else
                    {
                        result = new X509SecurityTokenProvider(this.credentials.ServiceEncryptCert);
                    }
                }
                else
                {
                    //
                }
            }
            else
            {
                result = base.CreateSecurityTokenProvider(requirement);
            }

            return result;
        }

        public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
        {
            return base.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
        }

        public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
        {
            return base.CreateSecurityTokenSerializer(version);
        }
    }
}
