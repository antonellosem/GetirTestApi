using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGADInterface;

namespace TestApplication
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void btnSubReg_Click(object sender, EventArgs e)
        {
            PgadResponse result = PgadInterface.SubRegistrazione("09255551005", 15115, "4098-0074924", "http://backend.intralot.it/PgadService/PGADService.svc");
            //PgadResponse result = PgadInterface.SubRegistrazione("09255551005", 15115, "4098-0074924", "http://backend.collaudo.intralot.it/PgadService/PGADService.svc");

            lblSubResponse.Text = result._descrizione + " " + result._esito;
        }

        private void btnCambiaStato_Click(object sender, EventArgs e)
        {
            //PgadResponse result = PgadInterface.CambiaStato("09255551005", "15115-0240412", 15115, 2, 2, "http://backend.intralot.it/PgadService/PGADService.svc");
            PgadResponse result = PgadInterface.CambiaStato("09255551005", "15115-0229660", 15115, 2, 2, "http://backend.collaudo.intralot.it/PgadService/PGADService.svc");

            lblSubResponse.Text = result._descrizione + " " + result._esito;
        }
    }
}
