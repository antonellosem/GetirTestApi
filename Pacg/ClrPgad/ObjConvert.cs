using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClrPgad.PgadCommunication;
using System.Data;

namespace ClrPgad
{
    public static class  ObjConvert
    {

        public static List<MessaggioDettaglioBonus> DatasetToMessaggioDettaglioBonus(DataSet dsElencoBonus) 
        {
            List<MessaggioDettaglioBonus> listBonus = new List<MessaggioDettaglioBonus>();
            if (dsElencoBonus.Tables.Count > 0)
            {
                foreach (DataRow riga in dsElencoBonus.Tables[0].Rows)
                {
                    int Importo = Convert.ToInt32(riga["Importo"].ToString());
                    int TipoGioco = Convert.ToInt32(riga["CodiceGioco"].ToString());
                    int CodiceFamiglia = Convert.ToInt32(riga["CodiceFamiglia"].ToString());
                    MessaggioDettaglioBonus bonus = new MessaggioDettaglioBonus(Importo, CodiceFamiglia, TipoGioco);
                    listBonus.Add(bonus);
                }
            }
            return listBonus;
        }

    }
}
