using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;

namespace AnagraficaContiGiocoForniture
{
    public class LoadError
    {

        public static errori loadFile(Stream stream)
        {
            string conStr = ConfigurationManager.ConnectionStrings["DBSalvaEsiti"].ToString();
            string stored = "[Account].[AddErroreFornitureAAMS]";

            XmlSerializer ser = new XmlSerializer(typeof(errori));

            TextReader reader = new StreamReader(stream);
            errori errors = (errori)ser.Deserialize(reader);
            reader.Close();
            stream.Close();
            stream = null;

            
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            foreach (erroriErrore e in errors.errore)
            {
                SqlCommand cmd = new SqlCommand(stored, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_fornitura", errors.id_fornitura));
                cmd.Parameters.Add(new SqlParameter("@data_invio", errors.data_invio));
                cmd.Parameters.Add(new SqlParameter("@num_conti_ok", errors.num_conti_ok));
                cmd.Parameters.Add(new SqlParameter("@prog", e.prog));
                cmd.Parameters.Add(new SqlParameter("@code", e.code));
                cmd.Parameters.Add(new SqlParameter("@descr", e.descr));
                cmd.Parameters.Add(new SqlParameter("@type", e.type));
                cmd.ExecuteNonQuery();
            }
            con.Close();
            
            return errors;
        }
    }
}
