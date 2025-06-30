using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioModificaProvinciaResidenza : MessaggioPGAD
    {
        private readonly ModificaProvinciaResidenzaContoRequestElements _modificaProvinciaResidenzaConto;
        public ModificaProvinciaResidenzaContoRequestElements ModificaProvinciaResidenzaConto
        {
            get
            {
                //Imposto l'id Transazione se non è stato settato
                if (_modificaProvinciaResidenzaConto.idTransazione == 0)
                {
                    _modificaProvinciaResidenzaConto.idTransazione = this.GetIdTransazione();
                }

                return _modificaProvinciaResidenzaConto;
            }
        }

        public MessaggioModificaProvinciaResidenza(int titolareSistema, string contoGioco,long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _modificaProvinciaResidenzaConto = new ModificaProvinciaResidenzaContoRequestElements
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

        public void SetProvincia(string provincia)
        {
            _modificaProvinciaResidenzaConto.provincia = provincia;
            ValidaProvincia();
        }


        public void ValidaProvincia()
        {
            if (_modificaProvinciaResidenzaConto.provincia == null ||
               _modificaProvinciaResidenzaConto.provincia.Length != (int)MessaggioPGADConstant.PROVINCIA)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: provincia {_modificaProvinciaResidenzaConto.provincia}");
            }
        }

        

    }
}
