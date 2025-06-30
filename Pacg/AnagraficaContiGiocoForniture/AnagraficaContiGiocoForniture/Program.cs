using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace AnagraficaContiGiocoForniture
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {

            FormForniture myForm = new FormForniture();
            Application.Run(myForm);

            /*
            int ID_FORNITURA =47;
            int TOP = 2000;
            FormForniture.GeneraXml(@"C:\Users\augusto.proietti\Desktop\PGAD_PRODUZIONE\" + ID_FORNITURA + ".xml", ID_FORNITURA, TOP);
            */

            //int ID_FORNITURA = 9;
            //int ID_FORNITURA = 8;
            //generaXml();

            //StreamReader s = new StreamReader(@"C:\Users\augusto.proietti\Desktop\PGAD_PRODUZIONE\Fornitura46\errori_fornitura_46.xml");
            //addErrori(s);

            //LoadError.loadFile(@"C:\workspace\anagraficaContiGioco\trunk\AnagraficaContiGiocoForniture\AnagraficaContiGiocoForniture\out\Fornitura5\errori_fornitura_5.xml");
        }
    }
}
