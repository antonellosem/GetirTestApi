using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioContoDormiente : MessaggioPGAD
    {
        private readonly ContoDormienteRequestElements _contodormiente;
        public ContoDormienteRequestElements ContoDormiente
        {
            get
            {
                if (_contodormiente.idTransazione == 0)
                {
                    _contodormiente.idTransazione = this.GetIdTransazione();
                }
                return _contodormiente;
            }
        }

        public MessaggioContoDormiente(int saldo, int titolareSistema, string contoGioco, long IdTransazione, DateTime data)
            : base(titolareSistema, IdTransazione)
        {
            _contodormiente = new ContoDormienteRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco
            };

            _contodormiente.idCnConto = titolareSistema;
            _contodormiente.importoSaldo = saldo;
            _contodormiente.dataDormiente = new Data();
            _contodormiente.dataDormiente.anno = data.Year.ToString("0000");
            _contodormiente.dataDormiente.mese = data.Month.ToString("00");
            _contodormiente.dataDormiente.giorno = data.Day.ToString("00");
        }

    }
}
