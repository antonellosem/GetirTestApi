using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioAggiornaPostaElettronicaContoDiGioco : MessaggioPGAD
    {
        private readonly AggiornaPostaElettronicaContoRequestElements _messaggioAggiornaPostaElettronicaContoDiGioco;

        public AggiornaPostaElettronicaContoRequestElements PersonaFisica
        {
            get
            {
                if (_messaggioAggiornaPostaElettronicaContoDiGioco.idTransazione == 0)
                {
                    _messaggioAggiornaPostaElettronicaContoDiGioco.idTransazione = this.GetIdTransazione();
                }

                return _messaggioAggiornaPostaElettronicaContoDiGioco;
            }
        }

        public MessaggioAggiornaPostaElettronicaContoDiGioco(int titolareSistema, long idTransazione, string CodiceConto, string PostaElettronica)
            : base(titolareSistema, idTransazione)
        {
            _messaggioAggiornaPostaElettronicaContoDiGioco = new AggiornaPostaElettronicaContoRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = CodiceConto,
                postaElettronica = PostaElettronica
            };
        }
        
        public void SetPostaElettronica(string PostaElettronica)
        {
            _messaggioAggiornaPostaElettronicaContoDiGioco.postaElettronica = PostaElettronica;

            ValidaDatiPostaElettronica();
        }

        private void ValidaDatiPostaElettronica()
        {
            if (_messaggioAggiornaPostaElettronicaContoDiGioco.postaElettronica.Length < 1 || _messaggioAggiornaPostaElettronicaContoDiGioco.postaElettronica.Length > (int)MessaggioPGADConstant.EMAIL)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: PostaElettronica {_messaggioAggiornaPostaElettronicaContoDiGioco.postaElettronica}");
            }
        }

      


    }
}
