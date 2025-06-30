using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PgadCommunication.Utility;
using PgadCommunication.Configuration;
using PgadCommunication.Contracts.Requests;


namespace PgadCommunication.Model
{
    public class PgadAdapter
    {

        public static Dictionary<short, string> getCodiciRitorno()
        {
            Dictionary<short, string> codici = new Dictionary<short, string>();
            try
            {
                DataTable data = new DataTable();
                using (SqlConnection conn = new SqlConnection(Settings.Instance.AnagraficaContiGiocoConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Protocollo.CodiceRitorno_Get", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adpter = new SqlDataAdapter(cmd);
                    adpter.Fill(data);
                }
                foreach (DataRow r in data.Rows)
                {
                    codici.Add(Convert.ToInt16(r["esito"].ToString()), r["descrizione"].ToString());
                }

                if (!codici.ContainsKey(0))
                {
                    codici.Add(0,"Errore Generico");
                }
                return codici;
            }
            catch (ApplicationException ex)
            {
                throw new PgadAdapterException("Dati.getCodiciRitorno", ex);
            }
        }

        public static HeaderPgad getHeaderInfo(int titolareSistema)
        {
            try
            {
                DataTable data = new DataTable();
                using (SqlConnection conn = new SqlConnection(Settings.Instance.AnagraficaContiGiocoConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Protocollo.HeaderInfo_Get", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodiceTitolareSistema", titolareSistema));
                    SqlDataAdapter adpter = new SqlDataAdapter(cmd);
                    adpter.Fill(data);
                }

                if (data.Rows != null && data.Rows.Count == 1)
                {
                    HeaderPgad header = new HeaderPgad();
                    header.idFsc = Convert.ToInt32(data.Rows[0]["FornitoreSevizi"]);
                    header.idCnc = Convert.ToInt32(data.Rows[0]["Concessionario"]);
                    header.idRete = Convert.ToInt32(data.Rows[0]["ReteConcessionario"]);
                    header.idCnConto = Convert.ToInt32(data.Rows[0]["TitolareSistema"]);
                    header.idReteConto = Convert.ToInt32(data.Rows[0]["ReteTitolare"]);
                    return header;
                }
                throw new PgadAdapterException(
                    $"Dati.getHeaderInfo dati non trovati per Titolare di Sitema {titolareSistema}");
            }
            catch (ApplicationException ex)
            {
                throw new PgadAdapterException("Dati.getHeaderInfo", ex);
            }
        }

        public static Dictionary<string, Config> getConfigurazioneConcessionario(string connectionString)
        {
            try
            {
                DataTable data = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Protocollo.ConfigurazioneConcessionario_Get", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(data);
                }
                Dictionary<string, Config> configurazione = new Dictionary<string, Config>();
                if (data == null || data.Rows.Count == 0)
                {
                    throw new Exception("GetConfigurazioneConcessionario: Configurazioni non trovate");
                }
                foreach (DataRow row in data.Rows)
                {
                    configurazione.Add(row["PartitaIva"].ToString() + "-" + row["TitolareSistema"].ToString(), new Config(row["PartitaIva"].ToString(), Convert.ToInt32(row["FornitoreSevizi"]), Convert.ToInt32(row["Concessionario"]), Convert.ToInt32(row["ReteConcessionario"]), Convert.ToInt32(row["TitolareSistema"]), Convert.ToInt32(row["ReteTitolare"]), row["ChiavePubblciaAAMS"].ToString(), row["ChiavePrivata"].ToString()));
                }
                return configurazione;
            }
            catch (Exception ex)
            {
                throw new PgadAdapterException("Dati.getConfigurazioneConcessionario " + ex.Message, ex);
            }
        }
        
    }
}
