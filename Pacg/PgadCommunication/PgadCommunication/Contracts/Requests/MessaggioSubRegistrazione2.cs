using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioSubRegistrazione2 : MessaggioPGAD
    {
        private readonly Subregistrazione2RequestElements _subregistrazione;
        public Subregistrazione2RequestElements Subregistrazione
        {
            get
            {
                if (_subregistrazione.idTransazione == 0)
                {
                    _subregistrazione.idTransazione = this.GetIdTransazione();
                }
                return _subregistrazione;
            }
        }

        public MessaggioSubRegistrazione2(int saldo, int saldoBonus, int titolareSistema, string contoGioco, long IdTransazione)
            : base(titolareSistema, IdTransazione)
        {
            _subregistrazione = new Subregistrazione2RequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco
            };

            _subregistrazione.idCnConto = titolareSistema;
            _subregistrazione.importoSaldo = saldo;
            _subregistrazione.importoBonusSaldo = saldoBonus;
        }

      
    }
}
