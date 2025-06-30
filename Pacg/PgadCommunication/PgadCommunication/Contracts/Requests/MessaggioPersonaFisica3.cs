using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioPersonaFisica3 : MessaggioPGAD
    {
        private readonly AperturaContoPersonaFisica3RequestElements _personaFisica;
        public AperturaContoPersonaFisica3RequestElements PersonaFisica
        {
            get
            {
                if (_personaFisica.idTransazione == 0)
                {
                    _personaFisica.idTransazione = this.GetIdTransazione();
                }
                return _personaFisica;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="titolareSistema"></param>
        /// <param name="contoGioco"></param>
        /// <param name="idTransazione"></param>
        /// <param name="tipoFornituraDatiPersonali">1 = Manuale (procedura con inserimento manuale dei dati) ; 2 = SPID (Sistema Pubblico di Identità Digitale)</param>
        public MessaggioPersonaFisica3(int titolareSistema, string contoGioco, long idTransazione, sbyte tipoFornituraDatiPersonali)
            : base(titolareSistema, idTransazione)
        {
            _personaFisica = new AperturaContoPersonaFisica3RequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                codiceConto = contoGioco,
                tipoFornituraDatiPersonali = tipoFornituraDatiPersonali
            };
        }

        public void setAnagrafica(string codiceFiscale, string cognome, string nome, char sesso, string mail, string nickName)
        {
            _personaFisica.titolareConto = new PersonaFisica
            {
                codiceFiscale = codiceFiscale,
                cognome = cognome,
                nome = nome
            };
            switch (sesso)
            {
                case 'M':
                case 'm':
                case '1':
                    _personaFisica.titolareConto.sesso = PersonaFisicaSesso.M;
                    break;
                case 'F':
                case 'f':
                case '2':
                    _personaFisica.titolareConto.sesso = PersonaFisicaSesso.F;
                    break;
            }
            _personaFisica.titolareConto.pseudonimo = nickName;
            _personaFisica.titolareConto.postaElettronica = mail;
            ValidaDatiAnagrafica();
        }

        private void ValidaDatiAnagrafica()
        {
            if (_personaFisica.titolareConto.codiceFiscale.Length != (int)MessaggioPGADConstant.CODICE_FISCALE)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: codiceFiscale {_personaFisica.titolareConto.codiceFiscale}");
            }
            if (_personaFisica.titolareConto.sesso != PersonaFisicaSesso.F &&
                _personaFisica.titolareConto.sesso != PersonaFisicaSesso.M)
            {
                throw new MessaggioPGADException($"Parametro Errato: sesso {_personaFisica.titolareConto.sesso}");
            }
            if (!(_personaFisica.titolareConto.cognome.Length >= 1 || _personaFisica.titolareConto.cognome.Length <= (int)MessaggioPGADConstant.DEFAULT_STR))
            {
                throw new MessaggioPGADException($"Parametro Errato: cognome {_personaFisica.titolareConto.cognome}");
            }
            if (!(_personaFisica.titolareConto.nome.Length >= 1 || _personaFisica.titolareConto.nome.Length <= (int)MessaggioPGADConstant.DEFAULT_STR))
            {
                throw new MessaggioPGADException($"Parametro Errato: nome {_personaFisica.titolareConto.nome}");
            }
            if (_personaFisica.titolareConto.pseudonimo.Length > (int)MessaggioPGADConstant.PSEUDONIMO)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: pseudonimo {_personaFisica.titolareConto.pseudonimo}");
            }
            if (_personaFisica.titolareConto.postaElettronica.Length > (int)MessaggioPGADConstant.DEFAULT_STR)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: postaElettronica {_personaFisica.titolareConto.postaElettronica}");
            }
        }

        public void SetDocumento(DateTime dataRilascio, string autoritaRilascio, string localitaRilascio, string numeroDocumento, int tipo)
        {
            _personaFisica.titolareConto.documento = new Documento
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
            if (!(_personaFisica.titolareConto.documento.autoritaRilascio.Length >= 1 || _personaFisica.titolareConto.documento.autoritaRilascio.Length <= (int)MessaggioPGADConstant.DEFAULT_STR))
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: autoritaRilascio {_personaFisica.titolareConto.documento.autoritaRilascio}");
            }
            if (!(_personaFisica.titolareConto.documento.localitaRilascio.Length >= 1 || _personaFisica.titolareConto.documento.localitaRilascio.Length <= (int)MessaggioPGADConstant.DEFAULT_STR))
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: localitaRilascio {_personaFisica.titolareConto.documento.localitaRilascio}");
            }
        }

        public void SetDatiNascita(string comune, string siglaProvincia, DateTime data)
        {
            _personaFisica.titolareConto.nascita = new DatiNascita
            {
                comune = comune,
                provincia = siglaProvincia,
                data = new Data
                {
                    anno = data.Year.ToString("0000"),
                    mese = data.Month.ToString("00"),
                    giorno = data.Day.ToString("00")
                }
            };
            validaDatiNascita();
        }

        private void validaDatiNascita()
        {
            if (_personaFisica.titolareConto.nascita.provincia.Length != (int)MessaggioPGADConstant.PROVINCIA)
            {
                throw new MessaggioPGADException($"Parametro Errato: provincia {_personaFisica.titolareConto.nascita.provincia}");
            }
        }

        public void SetResidenza(string indirizzo, string comune, string provincia, string cap)
        {
            _personaFisica.titolareConto.residenza = new Residenza
            {
                indirizzo = indirizzo,
                comune = comune,
                provincia = provincia,
                cap = cap
            };
            ValidaResidenza();
        }

        private void ValidaResidenza()
        {
            if (_personaFisica.titolareConto.residenza.cap.Length != (int)MessaggioPGADConstant.CAP)
            {
                throw new MessaggioPGADException($"Parametro Errato: cap {_personaFisica.titolareConto.residenza.cap}");
            }

            if (_personaFisica.titolareConto.residenza.provincia.Length != (int)MessaggioPGADConstant.PROVINCIA)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: residenza.provincia {_personaFisica.titolareConto.residenza.provincia}");
            }
        }

        public void SetLimiti(sbyte NumeroLimiti, List<MessaggioLimite> ElencoLimiti)
        {
            _personaFisica.numeroLimiti = NumeroLimiti;
            _personaFisica.limite = new Limite[NumeroLimiti];

            int index = 0;
            foreach (MessaggioLimite _limite in ElencoLimiti)
            {
                if (_limite.limite != null)
                {
                    _personaFisica.limite[index] = _limite.limite;
                    //_personaFisica.limite[index] = new Limite();
                    //_personaFisica.limite[index].tipoLimite = _limite.limite.TipoLimite;
                    //_personaFisica.limite[index].importo = _limite.limite.Importo;
                    index++;
                }
            }
        }



    }
}
