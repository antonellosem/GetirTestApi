using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using Ionic.Zip;

namespace AnagraficaContiGiocoForniture
{
    public enum Brand 
    { 
        INTRALOT=1,
        GIOKER =2
    }


    public enum StatoAccount
    { 
        ATTIVO=1
    }

    public class Estrazione
    {
        //private static string Stored = "[dbo].[EstraiAccountPGAD]";
        private static string Stored = "[Account].[EstraiAccountPGAD]";

        //public static  DataTable EstraiAccount(int Idx, int Limit,StatoAccount stato, Brand brand)
        public static DataTable EstraiAccount(int stato, Brand brand,int idfornitura,int top)
        {
            /*
            SqlConnection con=new SqlConnection(Settings.Instance.WhiteLabelConnection);
            SqlCommand cmd = new SqlCommand(Stored,con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Idx", Idx));
            cmd.Parameters.Add(new SqlParameter("@Limit", Limit));
            cmd.Parameters.Add(new SqlParameter("@Stato", (int)stato));
            cmd.Parameters.Add(new SqlParameter("@IdBrand", (int)brand));
            SqlDataAdapter adpter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpter.Fill(dt);
            */
            SqlConnection con = new SqlConnection(Settings.Instance.WhiteLabelConnection);
            SqlCommand cmd = new SqlCommand(Stored, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Stato", stato));
            cmd.Parameters.Add(new SqlParameter("@IdBrand", (int)brand));
            cmd.Parameters.Add(new SqlParameter("@TOP", (int)top));
            cmd.Parameters.Add(new SqlParameter("@IdFornitura", (int)idfornitura));

            SqlDataAdapter adpter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpter.Fill(dt);

            return dt;
        }

        
        public static string CreaXmlFornitura(int idFornitura,DataTable dt,string filePath)
        {
            anag_conti fornitura = new anag_conti();
            fornitura.id_fornitura = idFornitura.ToString();
            List<conto> conti=new List<conto>();

            foreach (DataRow row in dt.Rows)
            {
                conto c = new conto();
                c.cod_conc = row["cod_conc"].ToString();

                /*
                2:  GIOCHI PUBBLICI SPORT
                3:  GIOCHI PUBBLICI IPPICA
                7:  RINNOVATO SCOMMESSE IPPICHE
                8:  RINNOVATO SCOMMESSE SPORTIVE
                12: SUPERENALOTTO
                13: BINGO
                */
                switch (row["cod_rete"].ToString())
                { 
                    case "2":
                        c.cod_rete = cod_rete.Item2;
                        break;
                    case "3":
                        c.cod_rete = cod_rete.Item3;
                        break;
                    case "7":
                        c.cod_rete = cod_rete.Item7;
                        break;
                    case "8":
                        c.cod_rete = cod_rete.Item8;
                        break;
                    case "12":
                        c.cod_rete = cod_rete.Item12;
                        break;
                    case "13":
                        c.cod_rete = cod_rete.Item13;
                        break;
                }

                
                c.data_attivazione = Convert.ToDateTime(row["data_attivazione"]);
                c.email = row["email"].ToString();
                c.id_conto = row["id_conto"].ToString();
                c.nickname = row["nickname"].ToString();
                c.prog = row["IdUtente"].ToString(); //<!-- deve essere un Int, libero, ma univoco all'interno della fornitura (permette di identificare gli errori) -->

                persona_fisica utente=new persona_fisica();
                utente.cognome = row["cognome"].ToString();
                utente.nome = row["nome"].ToString();
                utente.prov_nascita = row["prov_nascita"].ToString();
                utente.prov_residenza = row["prov_residenza"].ToString();
                switch (row["sesso"].ToString())
                { 
                    case "M":
                        utente.sesso = sesso.M;
                        break;
                    case "F":
                        utente.sesso = sesso.F;
                        break;
                }

                utente.comune_nascita = row["comune_nascita"].ToString();
                utente.data_nascita = Convert.ToDateTime(row["data_nascita"]);
                utente.id_soggetto = row["id_soggetto"].ToString();

                c.soggetto = new soggetto();
                c.soggetto.Item = utente;
                conti.Add(c);
            }
            fornitura.conto = conti.ToArray();
            fornitura.num_record = fornitura.conto.Length.ToString();

            XmlSerializer ser = new XmlSerializer(fornitura.GetType());

            MemoryStream stream = new MemoryStream();
            ser.Serialize(stream, fornitura);
            stream.Position=0;

            StreamReader reader = new StreamReader(stream);
            string xml=reader.ReadToEnd();
            reader.Close();
            stream.Close();
            
            return xml;
        }

        public static void SaveAsFile(string str,string file )
        {
            StreamWriter stream = new StreamWriter(file);
            stream.Write(str);
            stream.Close();
        }


        public static void SaveAsZipFile(string str,string file)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddEntry("a", str);
                zip.Save(file);
            }
        }
    }
}
