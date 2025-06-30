using System.Security.Cryptography.X509Certificates;

namespace PgadCommunication.Configuration
{
    public class X509Manager
    {
        public static X509Certificate2 GetCert(string thumbprint, StoreName storeName, StoreLocation storeLocation)
        {
            X509Certificate2 cert = null;
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            X509Certificate2Enumerator enumerator = certCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                cert = enumerator.Current;
            }
            if (cert == null)
            {
                throw new X509ManagerException(
                    $"Certificato Non Trovato: Thumbprint {thumbprint}, StoreName {storeName}, StoreLocation {storeLocation}");
            }
            return cert;
        }

        /// <summary>
        /// Recupera il certificato con i seguenti parametri di default:
        /// StoreName StoreName.My
        /// StoreLocation StoreLocation.LocalMachine
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        /// <exception cref="">Torna X509ManagerException se il certificato non viente trovato</exception>
        public static X509Certificate2 GetCert(string thumbprint)
        {
            X509Certificate2 cert = null;
            cert = GetCert(thumbprint, StoreName.My, StoreLocation.LocalMachine);
            return cert;
        }
    }
}
