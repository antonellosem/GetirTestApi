using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioStatoConto : MessaggioPGAD
    {
        private readonly InterrogazioneStatoContoRequestElements _statoConto;
        public InterrogazioneStatoContoRequestElements StatoConto
        {
            get
            {
                if (_statoConto.idTransazione == 0)
                {
                    _statoConto.idTransazione = this.GetIdTransazione();
                }
                return _statoConto;
            }
        }

        public MessaggioStatoConto(int titolareSistema, string contoGioco, long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _statoConto = new InterrogazioneStatoContoRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco
            };
        }

    }
}
