using System;
using System.Data;
using System.Linq;
using System.Xml;
using Dapper;
using Gamenet.Infrastructure;
using Gamenet.Logger;
using PgadCommon.Enum;
using PgadData.Const;

namespace PgadData
{
    public class TransazioniDao : DapperBaseDao
    {
        private ITransazioniConnectionStringsProvider _transazioniConnectionStringsProvider;

        public TransazioniDao(ITransazioniConnectionStringsProvider connectionStringsProvider, ILog logger = null) : base(connectionStringsProvider, logger)
        {
            _transazioniConnectionStringsProvider = connectionStringsProvider;
        }

        public long GetIdTransazionePacg()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Direzione", 1);

            CommandDefinition cmd = new CommandDefinition(StoredProcedure.GestisciTransazionePgad, parameters: parameters, commandType: CommandType.StoredProcedure);

            int output = Query<int>(cmd).FirstOrDefault(); ;

            return output;
        }

        public long GestisciTransazionePacg(string xmlMessage, Direction direction, int idTransazioneInvio)
        {
            if (xmlMessage.StartsWith("<?"))
                xmlMessage = xmlMessage.Substring(xmlMessage.IndexOf("?>") + 2);  //Rimuovo eventuale tag xml
            
            var doc = new XmlDocument { InnerXml = xmlMessage };
            doc.InnerXml = doc.InnerXml.Replace("UTF-8", "UTF-16");

            long idCnc = 0;
            var operation = "";
            var idRicevuta = "";
            var esito = 0;
            var idTransazione = 0;
            const string nsSogei = "http://model.ws.contidigioco.sogei.it";
            const string nsSoap = "http://schemas.xmlsoap.org/soap/envelope/";

            var nodeList = doc.GetElementsByTagName("idTransazione", nsSogei);

            if (nodeList.Count > 0)
                idTransazione = Convert.ToInt32(nodeList[0].InnerText);  //Se è una risposta corretta contiene idTransazione


            nodeList = doc.GetElementsByTagName("idCn", nsSogei);
            if (nodeList.Count > 0)
                idCnc = Convert.ToInt64(nodeList[0].InnerText);
            

            nodeList = doc.GetElementsByTagName("Body", nsSoap);
            if (nodeList.Count > 0)
                operation = nodeList[0].FirstChild.Name; //Nome del webmethod che stiamo chiamando
            

            nodeList = doc.GetElementsByTagName("idRicevuta", nsSogei);
            if (nodeList.Count > 0)
                idRicevuta = nodeList[0].InnerText;
            

            nodeList = doc.GetElementsByTagName("esitoRichiesta", nsSogei);
            if (nodeList.Count > 0)
                esito = Convert.ToInt32(nodeList[0].InnerText);
            
            var transactionTime = DateTime.UtcNow; //Data Utc

            var parameters = new DynamicParameters();

            if (direction == Direction.Outwards)
                parameters.Add("@IdTransazione", idTransazione);
            
            parameters.Add("@CodiceConessionario", idCnc);
            parameters.Add("@IdTransazioneInvio", idTransazioneInvio);
            parameters.Add("@Direzione", direction);
            parameters.Add("@Dati_XML", doc.InnerXml);
            parameters.Add("@CodiceTransazione", idTransazione);
            parameters.Add("@CodiceOperazione", operation);
            parameters.Add("@IdRicevuta", idRicevuta);
            parameters.Add("@Esito", esito);
            parameters.Add("@Data_App", DateTime.Now.ToUniversalTime());

            var cmd = new CommandDefinition(StoredProcedure.GestisciTransazionePgad, parameters: parameters, commandType: CommandType.StoredProcedure);

            var output = Query<int>(cmd).FirstOrDefault(); ;

            return output;
        }

    }
}
