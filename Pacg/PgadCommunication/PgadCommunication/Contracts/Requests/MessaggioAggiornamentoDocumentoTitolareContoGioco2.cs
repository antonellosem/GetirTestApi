using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioAggiornamentoDocumentoTitolareContoGioco2: MessaggioPGAD
    {
        private readonly aggiornaDatiDocumentoTitolareConto2RequestElements _aggiornaDocumento;
        public aggiornaDatiDocumentoTitolareConto2RequestElements AggiornaDocumento
        {
            get
            {
                if (_aggiornaDocumento.idTransazione == 0)
                {
                    _aggiornaDocumento.idTransazione = this.GetIdTransazione();
                }
                return _aggiornaDocumento;
            }
        }

        public MessaggioAggiornamentoDocumentoTitolareContoGioco2(int titolareSistema, string contoGioco,long IdTransazione,sbyte tipoFornituraDatiPersonali)
            : base(titolareSistema, IdTransazione)
        {
            _aggiornaDocumento = new aggiornaDatiDocumentoTitolareConto2RequestElements()
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco,
                tipoFornituraDatiPersonali = tipoFornituraDatiPersonali
            };
        }

        public void SetDocumento(DateTime dataRilascio, string autoritaRilascio, string localitaRilascio, string numeroDocumento, int tipo)
        {
            _aggiornaDocumento.documento = new Documento
            {
                autoritaRilascio = autoritaRilascio,
                dataRilascio = new Data
                {
                    anno = dataRilascio.Year.ToString("0000"),
                    mese = dataRilascio.Month.ToString("00"),
                    giorno = dataRilascio.Day.ToString("00")
                },
                localitaRilascio = localitaRilascio,
                numero = numeroDocumento,
                tipo = Convert.ToSByte(tipo)
            };
            ValidaDocumento();
        }

        private void ValidaDocumento()
        {
            if (!(_aggiornaDocumento.documento.autoritaRilascio.Length >= 1 || _aggiornaDocumento.documento.autoritaRilascio.Length <= (int)MessaggioPGADConstant.DEFAULT_STR))
            {
                throw new MessaggioPGADException("Parametro Errato: autoritaRilascio " + _aggiornaDocumento.documento.autoritaRilascio);
            }
            if (!(_aggiornaDocumento.documento.localitaRilascio.Length >= 1 || _aggiornaDocumento.documento.localitaRilascio.Length <= (int)MessaggioPGADConstant.DEFAULT_STR))
            {
                throw new MessaggioPGADException("Parametro Errato: localitaRilascio " + _aggiornaDocumento.documento.localitaRilascio);
            }
        }

       
        
    }
}
