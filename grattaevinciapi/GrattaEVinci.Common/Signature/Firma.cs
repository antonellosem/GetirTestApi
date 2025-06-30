using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Gamenet.Common.Enum;
using GrattaEVinci.Common.Entities;
using GrattaEVinci.Common.ModelConfiguration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace GrattaEVinci.Common.Signature
{
    public static class Firma
    {
        public static byte[] ReadCertificate(string certificateName, string basePath)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            try
            {
                string path = Path.GetFullPath(basePath + "Certificati\\" + certificateName);

                using (FileStream f = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    int size = (int)f.Length;
                    byte[] data = new byte[size];
                    size = f.Read(data, 0, size);
                    f.Close();

                    return data;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static bool VerificaCorrettezzaFirma(string datiCalcolati, string datiFirmati, string idRivenditore, IOptions<Config> appSettings)
        {
            try
            {
                var certificateName = "";

                if (idRivenditore == appSettings.Value.IdRivenditore)
                    certificateName = appSettings.Value.Certificati.CertificatiGoldbet.CertificatoLolGrattaEVinci_igp;
                else if (idRivenditore == appSettings.Value.Better.IdRivenditore)
                    certificateName = appSettings.Value.Better.CertificatiBetter.CertificatoLolGrattaEVinci_igpBetter;

                string path = Path.GetFullPath(appSettings.Value.BasePath + "Certificati\\" + certificateName);
                var cert = new X509Certificate2(X509Certificate.CreateFromCertFile(path));

                ASCIIEncoding byteConverter = new ASCIIEncoding();
                byte[] dataToSign = byteConverter.GetBytes(datiCalcolati);
                byte[] signedData = Convert.FromBase64String(datiFirmati);

                var rsaParam = cert.GetRSAPublicKey().ExportParameters(false);
                var rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(rsaParam);

                bool retval = rsa.VerifyData(dataToSign, "SHA1", signedData);

                return retval;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static string FirmaDati(string firma, Brand idBrand, IOptions<Config> appSettings)
        {
            var certificateName = "";
            var certificatePwd = "";

            if (idBrand == Brand.Goldbet)
            {
                certificateName = appSettings.Value.Certificati.CertificatiGoldbet.CertificatoGoldbetGrattaEVinci;
                certificatePwd = appSettings.Value.Certificati.CertificatiGoldbet.CertificatoGoldbetPwdGrattaEVinci;
            }
            else if (idBrand == Brand.Better)
            {
                certificateName = appSettings.Value.Better.CertificatiBetter.CertificatoBetterGrattaEVinci;
                certificatePwd = appSettings.Value.Better.CertificatiBetter.CertificatoBetterPwdGrattaEVinci;
            }

            byte[] rawData = ReadCertificate(certificateName, appSettings.Value.BasePath);
            var cert = new X509Certificate2(rawData, certificatePwd, X509KeyStorageFlags.MachineKeySet);


            ASCIIEncoding ByteConverter = new ASCIIEncoding();

            byte[] signedData;

            using (RSA rsa = cert.GetRSAPrivateKey())
            {
                var byteFirma = ByteConverter.GetBytes(firma);
                signedData = rsa.SignData(byteFirma, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            }
            return Convert.ToBase64String(signedData);
        }

        public static string FirmaConCertificatoAdm(string dati, IOptions<Config> appSettings)
        {
            var certificateName = appSettings.Value.Certificati.CertificatoLolGrattaEVinci_aams;

            byte[] rawData = ReadCertificate(certificateName, appSettings.Value.BasePath);
            var cert = new X509Certificate2(rawData);
            
            var modulus = new BigInteger(1, cert.GetRSAPublicKey().ExportParameters(false).Modulus);
            var exponent = new BigInteger(1, cert.GetRSAPublicKey().ExportParameters(false).Exponent);
            AsymmetricKeyParameter keyParameter = new RsaKeyParameters(false, modulus, exponent);

            var bytesToEncrypt = Encoding.UTF8.GetBytes(dati);

            var encryptEngine = new Pkcs1Encoding(new RsaEngine());

            encryptEngine.Init(true, keyParameter);

            var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));

            return encrypted;
        }

        public static AnagraficaKeys CriptaAnagrafica(string xmlAnagrafica, string? ede3Key, IOptions<Config> appSettings)
        {
            return JavaSecurityUtil.EncryptPersonalData(xmlAnagrafica, ede3Key, appSettings);
        }

        public static string CryptoTripleDes(string dati, string key)
        {
            byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x01, 0x02 };
            
            byte[] results;
            UTF8Encoding UTF8 = new UTF8Encoding();

            byte[] tdeskey = Convert.FromBase64String(key);
            byte[] data = Convert.FromBase64String(dati);

            using (var tdes = TripleDES.Create())
            {

                tdes.Key = tdeskey;
                tdes.IV = iv;
                tdes.Mode = CipherMode.CBC;
                tdes.Padding = PaddingMode.None;

                ICryptoTransform enc = tdes.CreateEncryptor();

                results = enc.TransformFinalBlock(data, 0, data.Length);
            }
            
            return Convert.ToBase64String(results);
        }

        public static string GetIdRivenditoreDaBrand(Brand idBrand, IOptions<Config> appSettings)
            => idBrand == Brand.Goldbet ? appSettings.Value.IdRivenditore : appSettings.Value.Better.IdRivenditore;

        public static string GetCodiceConcessioneDaBrand(Brand idBrand, IOptions<Config> appSettings)
            => idBrand == Brand.Goldbet ? appSettings.Value.CodiceConcessione : appSettings.Value.Better.CodiceConcessione;

        public static Brand GetBrandDaCodiceConcessione(string idRivenditore, IOptions<Config> appSettings)
            => idRivenditore == appSettings.Value.Better.IdRivenditore ? Brand.Better : Brand.Goldbet;
    }
}
