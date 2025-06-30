using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGADInterface
{
    public class PgadResponse
    {
        private string _descrizioneField;

        private short _esitoField;

        private string _idRicevutaField;

        private long _idTransazioneField;

        private bool _successField;

        private System.DateTime _timestampField;

        public string _descrizione
        {
            get
            {
                return this._descrizioneField;
            }
            set
            {
                this._descrizioneField = value;
            }
        }

        public short _esito
        {
            get
            {
                return this._esitoField;
            }
            set
            {
                this._esitoField = value;
            }
        }

        public string _idRicevuta
        {
            get
            {
                return this._idRicevutaField;
            }
            set
            {
                this._idRicevutaField = value;
            }
        }

        public long _idTransazione
        {
            get
            {
                return this._idTransazioneField;
            }
            set
            {
                this._idTransazioneField = value;
            }
        }

        public bool _success
        {
            get
            {
                return this._successField;
            }
            set
            {
                this._successField = value;
            }
        }

        public System.DateTime _timestamp
        {
            get
            {
                return this._timestampField;
            }
            set
            {
                this._timestampField = value;
            }
        }
    }    

    public class PgadStatoContoResponse : PgadResponse
    {
        private int causaleField;

        private string codiceContoField;

        private long idCnContoField;

        private int idReteContoField;

        private int statoField;

        public int Causale
        {
            get
            {
                return this.causaleField;
            }
            set
            {
                this.causaleField = value;
            }
        }

        public string CodiceConto
        {
            get
            {
                return this.codiceContoField;
            }
            set
            {
                this.codiceContoField = value;
            }
        }

        public long IdCnConto
        {
            get
            {
                return this.idCnContoField;
            }
            set
            {
                this.idCnContoField = value;
            }
        }

        public int IdReteConto
        {
            get
            {
                return this.idReteContoField;
            }
            set
            {
                this.idReteContoField = value;
            }
        }

        public int Stato
        {
            get
            {
                return this.statoField;
            }
            set
            {
                this.statoField = value;
            }
        }
    }
}
