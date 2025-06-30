using System.Security.Cryptography.X509Certificates;

namespace PgadCommunication.Configuration
{
    public class Config
    {
        public Config(string username, int fornitoreServizi, int concessionario, int idRete, int concessionarioConto, int idReteConcessionarioConto, string aamsPlublickeyThumprint, string cncPrivateKeyThumprint)
        {
            this.IdFSC = fornitoreServizi;
            this.Username = username;
            this.Concessionario = concessionario;
            this.IdRete = idRete;
            this.ConcessionarioConto = concessionarioConto;
            this.IdReteConcessionarioConto = idReteConcessionarioConto;

            if (!string.IsNullOrEmpty(aamsPlublickeyThumprint))
            {
                this.AAMSPlublickey = X509Manager.GetCert(aamsPlublickeyThumprint);
            }
            if (!string.IsNullOrEmpty(cncPrivateKeyThumprint))
            {
                this.CNCPrivateKey = X509Manager.GetCert(cncPrivateKeyThumprint);
            }
        }

        public int IdFSC { get; set; }
        public int Concessionario { get; set; }
        public int IdRete { get; set; }
        public int ConcessionarioConto { get; set; }
        public int IdReteConcessionarioConto { get; set; }
        public string Username { get; set; }
        public X509Certificate2 AAMSPlublickey;
        public X509Certificate2 CNCPrivateKey;
    }
}
