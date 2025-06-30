using System;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using PgadCommon.Enum;
using PgadCommunication.Utility;

namespace PgadCommunication.Model
{
    public class PgadTransactionStore
    {
        static PgadTransactionStore()
        {

        }
        public static string ConnectionString { get; set; }
        public static string StoredName { get; set; }

        public static int StoreTransaction(string xmlMessage, Direction direction, int idTransazioneInvio)
        {
            try
            {
                //Rimuovo eventuale tag xml
                if (xmlMessage.StartsWith("<?"))
                {
                    xmlMessage = xmlMessage.Substring(xmlMessage.IndexOf("?>") + 2);
                }

                var doc = new XmlDocument {InnerXml = xmlMessage};
                doc.InnerXml = doc.InnerXml.Replace("UTF-8", "UTF-16");

                long idCnc = 0;
                var operation = "";
                var idRicevuta = "";
                var esito = 0;
                var idTransazione = 0;
                const string nsSogei = "http://model.ws.contidigioco.sogei.it";
                const string nsSoap = "http://schemas.xmlsoap.org/soap/envelope/";

                XmlNodeList nodeList = null;
                nodeList = doc.GetElementsByTagName("idTransazione", nsSogei);
                if (nodeList.Count > 0)
                {
                    //Se è una risposta corretta contiene idTransazione
                    idTransazione = Convert.ToInt32(nodeList[0].InnerText);
                }

                nodeList = doc.GetElementsByTagName("idCn", nsSogei);
                if (nodeList.Count > 0)
                {
                    idCnc = Convert.ToInt64(nodeList[0].InnerText);
                }

                nodeList = doc.GetElementsByTagName("Body", nsSoap);
                if (nodeList.Count > 0)
                {
                    //Nome del webmethod che stiamo chiamando
                    operation = nodeList[0].FirstChild.Name;
                }

                nodeList = doc.GetElementsByTagName("idRicevuta", nsSogei);
                if (nodeList.Count > 0)
                {
                    idRicevuta = nodeList[0].InnerText;
                }

                nodeList = doc.GetElementsByTagName("esitoRichiesta", nsSogei);
                if (nodeList.Count > 0)
                {
                    esito = Convert.ToInt32(nodeList[0].InnerText);
                }
                var transactionTime = DateTime.UtcNow; //Data Utc
                var data = new DataTable();
                using (var conn = new SqlConnection(Settings.Instance.PgadDBTransactionConnectionString))
                {
                    SqlCommand cmd = null;
                    //cmd = new SqlCommand("[dbo].[InsUpdTransazionePGAD]", conn)
                    //{
                    //    CommandType = CommandType.StoredProcedure
                    //};

                    /*IdTransazione viene inviato solamente durante la richiesta, per eseguire l'aggiornamento della riga, 
                     * in fase di risposta si inserisce una riga nuova*/
                    if (direction == Direction.Outwards)
                    {
                        cmd = new SqlCommand("dbo.UpdateTransazionePGAD", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new SqlParameter("@IdTransazione", idTransazione));
                    }
                    else
                    {
                        cmd = new SqlCommand("dbo.InsertTransazionePGAD", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                    }
                    cmd.Parameters.Add(new SqlParameter("@CodiceConessionario", idCnc));
                    cmd.Parameters.Add(new SqlParameter("@IdTransazioneInvio", idTransazioneInvio));
                    cmd.Parameters.Add(new SqlParameter("@Direzione", direction));
                    cmd.Parameters.Add(new SqlParameter("@Dati_XML", doc.InnerXml));
                    cmd.Parameters.Add(new SqlParameter("@CodiceTransazione", idTransazione));
                    cmd.Parameters.Add(new SqlParameter("@CodiceOperazione", operation));
                    cmd.Parameters.Add(new SqlParameter("@IdRicevuta", idRicevuta));
                    cmd.Parameters.Add(new SqlParameter("@Esito", esito));
                    cmd.Parameters.Add(new SqlParameter("@Data_App", DateTime.Now.ToUniversalTime()));

                    var adpter = new SqlDataAdapter(cmd);
                    adpter.Fill(data);
                }

                if (data.Rows.Count > 0)
                {
                    idTransazione = Convert.ToInt32(data.Rows[0]["IdTransazione"]);
                }
                return idTransazione;
            }
            catch (ApplicationException ex)
            {
                throw new PgadTransactionStoreException("StoreTransaction", ex);
            }
        }

        public static int GetTransactionId()
        {
            int idTransazione = 0;
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(Settings.Instance.PgadDBTransactionConnectionString))
            {
                //SqlCommand cmd = new SqlCommand("[dbo].[InsUpdTransazionePGAD]", conn);
                SqlCommand cmd = new SqlCommand("[dbo].[InsertTransazionePGAD]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Direzione", Direction.Outwards));
                cmd.Parameters.Add(new SqlParameter("@Data_App", DateTime.UtcNow));
                SqlDataAdapter adpter = new SqlDataAdapter(cmd);
                adpter.Fill(data);
                idTransazione=Convert.ToInt32(data.Rows[0]["IdTransazione"]);
            }
            return idTransazione;
        }

    }
}
