using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]

    public class MessaggioPersonaGiuridica : MessaggioPGAD
    {
        public MessaggioPersonaGiuridica(int titolareSistema, string contoGioco, long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _personaGiuridica = new AperturaContoPersonaGiuridicaRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione
            };
            _personaGiuridica.idRete = Convert.ToSByte(this.header.ReteTitolare);
            _personaGiuridica.codiceConto = contoGioco;

        }

        public void SetAnagrafica(string piva, string ragioneSociale, string mail, string nickName)
        {
            _personaGiuridica.titolareConto = new PersonaGiuridica
            {
                partitaIva = piva, postaElettronica = mail, pseudonimo = nickName, ragioneSociale = ragioneSociale
            };
            ValidaDatiAnagrafica();
        }

        private void ValidaDatiAnagrafica()
        {
            if (_personaGiuridica.titolareConto.partitaIva.Length != (int)MessaggioPGADConstant.PARTITA_IVA)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: parita iva {_personaGiuridica.titolareConto.partitaIva}");
            }

            if (_personaGiuridica.titolareConto.ragioneSociale == null ||
                !(_personaGiuridica.titolareConto.ragioneSociale.Length >= 1 ||
                _personaGiuridica.titolareConto.ragioneSociale.Length <= (int)MessaggioPGADConstant.DEFAULT_STR))
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: Ragione Sociale {_personaGiuridica.titolareConto.ragioneSociale}");
            }

        }


        public void SetSede(string indirizzo, string comune, string provincia, string cap)
        {
            _personaGiuridica.titolareConto.sede = new Residenza();
            _personaGiuridica.titolareConto.sede.indirizzo = indirizzo;
            _personaGiuridica.titolareConto.sede.comune = comune;
            _personaGiuridica.titolareConto.sede.provincia = provincia;
            _personaGiuridica.titolareConto.sede.cap = cap;
            ValidaSede();
        }

        private void ValidaSede()
        {
            if (_personaGiuridica.titolareConto.sede.cap.Length != (int)MessaggioPGADConstant.CAP)
            {
                throw new MessaggioPGADException($"Parametro Errato: cap {_personaGiuridica.titolareConto.sede.cap}");
            }

            if (_personaGiuridica.titolareConto.sede.provincia.Length != (int)MessaggioPGADConstant.PROVINCIA)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: residenza.provincia {_personaGiuridica.titolareConto.sede.provincia}");
            }
        }

        private AperturaContoPersonaGiuridicaRequestElements _personaGiuridica;
        public AperturaContoPersonaGiuridicaRequestElements PersonaGiuridica
        {
            get
            {
                if (_personaGiuridica.idTransazione == 0)
                {
                    _personaGiuridica.idTransazione = this.GetIdTransazione();
                }
                return _personaGiuridica;
            }
        }


    }
}
