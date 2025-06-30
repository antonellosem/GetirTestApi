using System.Security.Cryptography;
using System.Text;
using GrattaEVinci.Common.Entities;
using GrattaEVinci.Common.ModelConfiguration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Math;

//using X509Certificate = System.Security.Cryptography.X509Certificates.X509Certificate;

namespace GrattaEVinci.Common.Signature
{
    public static class JavaSecurityUtil
    {
        public static AnagraficaKeys EncryptPersonalData(string personalData, string desKey, IOptions<Config> appSettings)
        {
            AnagraficaKeys res = new AnagraficaKeys();

            if (string.IsNullOrEmpty(desKey))
                desKey = Generate3DesKeyForGameAccount();

            var byteMod = Encoding.Default.GetBytes(desKey);

            byteMod = SetParityBit(byteMod, 0);

            byte[] arrayMod = byteMod.ToList().GetRange(0, 24).ToArray();
            var javaKey = Convert.ToBase64String(arrayMod);

            var dataWithRsa = Firma.FirmaConCertificatoAdm(personalData, appSettings);

            var dataWithTripleDes = Firma.CryptoTripleDes(dataWithRsa, javaKey);

            res.ChiaveCriptazione = desKey;
            res.AnagraficaCriptata = dataWithTripleDes;
            return res;
        }

        private static string Generate3DesKeyForGameAccount()
         => Convert.ToBase64String(TripleDES.Create().Key);

        private static byte[] SetParityBit(byte[] key, int offset)
        {
            for (int i = 0; i < key.Length; i++)
            {
                var b = key[offset] & 254;
                b |= (BigInteger.BitCnt(b) & 1) ^ 1;
                key[offset++] = (byte)b;
            }
            return key;
        }
    }
}
