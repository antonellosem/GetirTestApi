using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PgadCommunication.Business;
using System.Xml.Serialization;
using System.IO;
using PgadCommunication;

namespace PgadTest
{

    public partial class PGADForm : Form
    {
        public PGADForm()
        {
            InitializeComponent();

            sessoBox.Items.Add('M');
            sessoBox.Items.Add('F');
            sessoBox.SelectedIndex = 0;

            codiceContrattoTxt.Text = "4098-000001";
            pivaText.Text = "09255551005";
            cncTxt.Text = "15115";
            pivaSub.Text = "09255551005";
            cncSub.Text = "15115";

            opsPIVA.Text = "09255551005";
            opsTitolare.Text = "15115";

            statoCombo.ValueMember = "Id";
            statoCombo.DisplayMember = "Descrizione";
            statoCombo.Items.Add(new Dato(1, "Aperto"));
            statoCombo.Items.Add(new Dato(2, "Sospeso"));
            statoCombo.Items.Add(new Dato(3, "Chiuso"));
            statoCombo.Items.Add(new Dato(4, "Dormiente"));


            causale.Items.Add(new Dato(1, "Cambio stato richiesta da AAMS"));
            causale.Items.Add(new Dato(2, "Cambio stato richiesto dal Concessionario"));
            causale.Items.Add(new Dato(3, "Cambio stato richiesto del conto"));
            causale.Items.Add(new Dato(4, "Cambio stato richiesto da Autorità Giudiziaria"));
            causale.Items.Add(new Dato(5, "Cambio stato richiesto da AMMS per decesso"));
            causale.ValueMember = "Id";
            causale.DisplayMember = "Descrizione";


            tipoDocumentoBox.ValueMember = "Tipo";
            tipoDocumentoBox.DisplayMember = "Descrizione";
            tipoDocumentoBox.Items.Add(new Documento(1, "Carta d'identità"));
            tipoDocumentoBox.Items.Add(new Documento(2, "Patente di guida"));
            tipoDocumentoBox.Items.Add(new Documento(3, "Passaporto"));
            tipoDocumentoBox.Items.Add(new Documento(4, "Tessera personale di riconoscimento Mod. BT"));
            tipoDocumentoBox.Items.Add(new Documento(5, "Tessera personale di riconoscimento Mod. AT"));
            tipoDocumentoBox.Items.Add(new Documento(6, "Porto d'armi"));
            tipoDocumentoBox.Items.Add(new Documento(10, "Altro"));
            tipoDocumentoBox.SelectedIndex = 0;

            indirizzoResidenzaTxt.Text = "Via Tiburtina n. 1155";
            comuneResidenzaTxt.Text = "ROMA";
            provinciaResidenzaTxt.Text = "RM";
            capResidenzaTxt.Text = "00100";


            numeroDocumentoTxt.Text = "0000000";
            autoritaTxt.Text = "SINDACO";
            dataRilascioTxt.Text = "2010/01/01";
            localitaTxt.Text = "ROMA";


            /*Persona Giuridica*/
            pgTitolare.Text = "4098";
            pgPIVAFS.Text = "09255551005";
            pgComune.Text = "ROMA";
            pgPR.Text = "RM";
            pgCAP.Text = "00100";
            pgSede.Text = "Via Tiburtina n. 1155";
            pgMail.Text = "sistemi@intralot.it";
            pgUsername.Text = "cncintralot";
            pgPIVA.Text = "09255551005";
            ragioneSociale.Text = "Intralot Italia s.p.a.";

            //
            causaleBox.Items.Add(new Dato(1, "Ricarica"));
            causaleBox.Items.Add(new Dato(2, "Storno Ricarica"));
            causaleBox.Items.Add(new Dato(3, "Prelievo"));
            causaleBox.Items.Add(new Dato(4, "Storno Prelievo"));
            causaleBox.Items.Add(new Dato(7, "Costi Servizi Aggiuntivi"));
            causaleBox.ValueMember = "Tipo";
            causaleBox.DisplayMember = "Descrizione";

            pagBox.Items.Add(new Dato(2, "Carta di Credito"));
            pagBox.Items.Add(new Dato(3, "Carta Prepagata"));
            pagBox.Items.Add(new Dato(4, "Bonifico Bancario/Postale"));
            pagBox.Items.Add(new Dato(5, "Bollettino Postale"));
            pagBox.Items.Add(new Dato(6, "Assegno di Conto Corrente"));
            pagBox.Items.Add(new Dato(7, "Assegno Circolare"));
            pagBox.Items.Add(new Dato(8, "Vaglia Postale"));
            pagBox.Items.Add(new Dato(9, "Ricarica Scratch"));
            pagBox.Items.Add(new Dato(10, "Altro"));
            pagBox.ValueMember = "Tipo";
            pagBox.DisplayMember = "Descrizione";


            mbCausale.Items.Add(new Dato(5, "Bonus"));
            mbCausale.Items.Add(new Dato(6, "Storno Bonus"));
            mbCausale.ValueMember = "Tipo";
            mbCausale.DisplayMember = "Descrizione";


        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void codiceFiscaleTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void PGADForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            AddConto(pivaText.Text, Convert.ToInt32(cncTxt.Text), codiceFiscaleTxt.Text,
                codiceContrattoTxt.Text, nomeTxt.Text, CognomeTxt.Text, Convert.ToChar(sessoBox.Text), usernameTxt.Text, mailTxt.Text,
                    comuneNascitaTxt.Text, provincaNascitaTxt.Text, DateTime.Parse(dataNascitaTxt.Text),
                    indirizzoResidenzaTxt.Text, comuneResidenzaTxt.Text, provinciaResidenzaTxt.Text, capResidenzaTxt.Text,
                    DateTime.Parse(dataRilascioTxt.Text), autoritaTxt.Text, localitaTxt.Text, numeroDocumentoTxt.Text, ((Documento)(tipoDocumentoBox.SelectedItem)).Tipo);
        }


        public void AddConto(string piva, int titolareSitema, string codiceFiscale, string conto, string nome, string cognome, char sesso, string username, string email,
                        string comuneNascita, string provinciaNascita, DateTime dataNascita,
                        string viaResidenza, string comuneResidenza, string provinciaResidenza, string capResidenza,
                        DateTime dataRilascioDocumento, string autoritaRilascioDocumento, string comuneDocumento, string numeroDocumento, int tipoDocumento)
        {
            Persona p = new Persona(piva, titolareSitema, codiceFiscale, conto, nome, cognome, sesso, username, email,
                         comuneNascita, provinciaNascita, dataNascita,
                         viaResidenza, comuneResidenza, provinciaResidenza, capResidenza,
                         dataRilascioDocumento, autoritaRilascioDocumento, comuneDocumento, numeroDocumento, tipoDocumento);


            logBoxTxt.Text += String.Format("---------------------\r\nInivo Apertura Conto\r\nCodice Fiscale {0} Codice Conto {1} \r\n", p.CodiceFiscale, p.Conto);
            SaveFile(p);

            PgadGatewayResponse res = p.ApriContoPersonaFisica();
            //PrintResponse("ApriContoPersonaFisica", res);
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);
        }


        public static void PrintResponse(string method, PgadGatewayResponse resp)
        {
            Console.WriteLine("{0}: {1}", method, resp.ToString());
            Console.ReadLine();
        }

        public static void SaveFile(Persona p)
        {
            XmlSerializer ser = new XmlSerializer(p.GetType());
            string path=p.CodiceFiscale + ".xml";
            FileStream f;
            if(File.Exists(path))
            {
                 f = new FileStream(path, FileMode.OpenOrCreate| FileMode.Truncate, FileAccess.ReadWrite);
            }else
            {
                f = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }

            ser.Serialize(f, p);
            f.Close();
        }

        public static void SaveFile(MessaggioPersonaGiuridica pg)
        {
            XmlSerializer ser = new XmlSerializer(pg.GetType());
            string path = pg.PersonaGiuridica.titolareConto.partitaIva + ".xml";
            FileStream f;
            if (File.Exists(path))
            {
                f = new FileStream(path, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.ReadWrite);
            }
            else
            {
                f = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            //FileStream f = new FileStream(pg.PersonaGiuridica.titolareConto.partitaIva + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            ser.Serialize(f, pg);
            f.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader(fileTxt.Text);
            XmlSerializer ser = new XmlSerializer(typeof(Persona));
            persona = (Persona)ser.Deserialize(reader);
            setPersona(persona);
            reader.Close();
        }

        Persona persona;

        public void setPersona(Persona p)
        {
            if (p.Sesso == 'M')
            {
                sessoBox.SelectedIndex = 0;
            }
            else
            {
                sessoBox.SelectedIndex = 1;
            }

            codiceContrattoTxt.Text = p.Conto;
            pivaText.Text = p.PIVAFS;
            cncTxt.Text = p.TitolareSistema.ToString();

            int i = 0;
            foreach (Documento d in tipoDocumentoBox.Items)
            {
                if (d.Tipo == p.TipoDocumento)
                {
                    break;
                }
                i++;
            }
            tipoDocumentoBox.SelectedIndex = i;

            codiceFiscaleTxt.Text = p.CodiceFiscale;
            nomeTxt.Text = p.Nome;
            CognomeTxt.Text = p.Cognome;
            dataNascitaTxt.Text = p.DataNascita.ToString("yyyy/MM/dd");
            usernameTxt.Text = p.Username;
            mailTxt.Text = p.Email;
            comuneNascitaTxt.Text = p.ComuneNascita;
            provincaNascitaTxt.Text = p.ProvinciaNascita;
            cncTxt.Text = p.TitolareSistema.ToString();


            indirizzoResidenzaTxt.Text = p.ViaResidenza;
            comuneResidenzaTxt.Text = p.ComuneResidenza;
            provinciaResidenzaTxt.Text = p.ProvinciaResidenza;
            capResidenzaTxt.Text = p.CapResidenza;

            numeroDocumentoTxt.Text = p.NumeroDocumento;
            autoritaTxt.Text = p.AutoritaRilascioDocumento;
            dataRilascioTxt.Text = p.DataRilascioDocumento.ToString("yyyy/MM/dd");
            localitaTxt.Text = p.ComuneDocumento;


            opsPIVA.Text = p.PIVAFS;
            opsTitolare.Text = p.TitolareSistema.ToString();
            opsConto.Text = p.Conto;

        }

        private void fileTxt_TextChanged(object sender, EventArgs e)
        {


        }

        private void logBoxTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pivaText_TextChanged(object sender, EventArgs e)
        {

        }

        private void cncTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void codiceContrattoTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            logBoxTxt.Text += String.Format("---------------------\r\nSubRegistrazione \r\n Cnc: {0} Codice Conto {1}  \r\n", cncSub.Text, contoSub.Text);

            IPgadGateway pgad = Factory.NewPgadInstance(pivaSub.Text, Convert.ToInt32(cncSub.Text));

            MessaggioSubRegistrazione subregistrazione = new MessaggioSubRegistrazione(Convert.ToInt32(cncSub.Text), contoSub.Text,0);
            PgadGatewayResponse res = pgad.subRegistrazione(subregistrazione);
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);
        }

        private void CambioBt_Click(object sender, EventArgs e)
        {

            CAUSALE_CAMBIO_STATO_CONTO cs;
            switch (((Dato)causale.SelectedItem).Id)
            {
                case 1:
                    cs = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DA_AAMS;
                    break;
                case 2:
                    cs = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_CONCESSIONARIO;
                    break;
                case 3:
                    cs = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_TITOLARE_CONTO;
                    break;
                case 4:
                    cs = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DA_AUTORITA_GIUDIZIARIA;
                    break;
                case 5:
                    cs = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DA_AAMS_DECESSO;
                    break;
                default:
                    cs = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_CONCESSIONARIO;
                    break;
            }

            STATO_CONTO stato;
            switch (((Dato)statoCombo.SelectedItem).Id)
            {
                case 1:
                    stato = STATO_CONTO.APERTO;
                    break;
                case 2:
                    stato = STATO_CONTO.SOSPESO;
                    break;
                case 3:
                    stato = STATO_CONTO.CHIUSO;
                    break;
                case 4:
                    stato = STATO_CONTO.DORMIENTE;
                    break;
                default:
                    stato = STATO_CONTO.APERTO;
                    break;
            }
            logBoxTxt.Text += String.Format("---------------------\r\nCambio Stato \r\n Cnc {0}, Codice Conto {1} Causale {2} Stato {3} \r\n", cncSub.Text, contoSub.Text, cs, stato);
            IPgadGateway pgad = Factory.NewPgadInstance(pivaSub.Text, Convert.ToInt32(cncSub.Text));
            MessaggioCambioStatoConto cambioStato = new MessaggioCambioStatoConto(Convert.ToInt32(cncSub.Text), contoSub.Text,0);
            cambioStato.setStato(cs, stato);
            PgadGatewayResponse res = pgad.cambioStato(cambioStato);
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            logBoxTxt.Text += String.Format("---------------------\r\nCambio Residenza \r\n Cnc {0}, Codice Conto {1} Residenza {2} \r\n", cncSub.Text, contoSub.Text, prRes.Text);
            IPgadGateway pgad = Factory.NewPgadInstance(pivaSub.Text, Convert.ToInt32(cncSub.Text));
            MessaggioModificaProvinciaResidenza modificaProvincia = new MessaggioModificaProvinciaResidenza(Convert.ToInt32(cncSub.Text), contoSub.Text,0);
            modificaProvincia.setProvincia(prRes.Text);
            PgadGatewayResponse res = pgad.modificaResidenza(modificaProvincia);
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            logBoxTxt.Text += String.Format("---------------------\r\nInfo Stato Conto \r\n Cnc {0}, Codice Conto {1} \r\n", cncSub.Text, contoSub.Text);
            IPgadGateway pgad = Factory.NewPgadInstance(pivaSub.Text, Convert.ToInt32(cncSub.Text));
            MessaggioStatoConto stato = new MessaggioStatoConto(Convert.ToInt32(cncSub.Text), contoSub.Text,0);
            PgadGatewayStatoResponse res = (PgadGatewayStatoResponse)pgad.stato(stato);
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3} Causale {4} Stato {5} CodiceConto {6}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione, res.Causale, res.Stato, res.CodiceConto);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PersonaFisica pf = new PersonaFisica();
            pf.TitolareSistema = Convert.ToInt32(pgTitolare.Text);
            pf.Conto = pgCodiceContratto.Text;
            pf.Nome = ragioneSociale.Text;
            pf.PIVA = pgPIVA.Text;
            pf.CodiceFiscale = pgPIVA.Text;
            pf.Email = pgMail.Text;
            pf.Username = pgUsername.Text;
            pf.ViaResidenza = pgSede.Text;
            pf.ComuneResidenza = pgComune.Text;
            pf.ProvinciaResidenza = pgPR.Text;
            pf.CapResidenza = pgCAP.Text;

            logBoxTxt.Text += String.Format("---------------------\r\nApertura Persona Giuridica Codice Conto {0} \r\n", pgCodiceContratto.Text);
            PgadGatewayResponse res = pf.ApriContoPersonaGiuridica();
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);

            SaveFile(pf);
        }

        private void CaricaBt_Click(object sender, EventArgs e)
        {

            StreamReader reader = new StreamReader(opsFile.Text);
            XmlSerializer ser = new XmlSerializer(typeof(Persona));
            persona = (Persona)ser.Deserialize(reader);
            setPersona(persona);
            reader.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            logBoxTxt.Text += String.Format("---------------------\r\nSaldo ContoGioco Codice Conto {0} Saldo {1} SaldoBonus{2} Dettagli {3}\r\n", pgCodiceContratto.Text, persona.SaldoUtente.ImportoTotale, persona.SaldoUtente.ImportoSaldoBonus, persona.SaldoUtente.DettaglioBonus.Count);
            PgadGatewayResponse res = persona.Saldo();
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);


        }

        private void button8_Click(object sender, EventArgs e)
        {
            int causale = ((Dato)causaleBox.SelectedItem).Id;
            int mezzo = ((Dato)pagBox.SelectedItem).Id;
            int importo = Convert.ToInt32(movImporto.Text);

            logBoxTxt.Text += String.Format("---------------------\r\nMovimento ContoGioco Codice Conto {0} Causale {1} Mezzo {2} Importo {3}\r\n", pgCodiceContratto.Text, causale, mezzo, importo);
            PgadGatewayResponse res = persona.MovimentoConto(causale, mezzo, importo);
            SaveFile(persona);

            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int causale = ((Dato)mbCausale.SelectedItem).Id;
            int famiglia = Convert.ToInt32(bFamiglia.Text);
            int gioco = Convert.ToInt32(bGioco.Text);
            int importo = Convert.ToInt32(importoBonus.Text);

            logBoxTxt.Text += String.Format("---------------------\r\nMovimento Bonus ContoGioco Codice Conto {0} Causale {1} Famiglia {2} Gioco {3} Importo {4}\r\n", pgCodiceContratto.Text, causale, famiglia, gioco, importo);

            PgadGatewayResponse res = persona.MovimentoBonus(causale, famiglia, gioco, importo);
            SaveFile(persona);
            logBoxTxt.Text += String.Format("IdTransazione {0} Ricevuta AAMS {1} Esito {2} Descrizione {3}\r\n\r\n-------------------------\r\n", res.IdTransazione, res.IdRicevuta, res.Esito, res.Descrizione);
        }

        private void cncSub_TextChanged(object sender, EventArgs e)
        {

        }

    }

    public class Dato
    {

        public Dato(int id, string descrizione)
        {
            Id = id;
            Descrizione = descrizione;
        }

        public int Id { get; set; }
        public string Descrizione { get; set; }
    }

    public class Documento
    {
        public Documento(int tipo, string descrizione)
        {
            Tipo = tipo;
            Descrizione = descrizione;
        }
        public int Tipo { get; set; }
        public string Descrizione { get; set; }
    }
}
