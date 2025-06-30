using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Diagnostics;

namespace AnagraficaContiGiocoForniture
{
    public partial class FormForniture : Form
    {

        static string BasePath="";
        static string CrtPath = "";
        static string ForniturePath = "";
        static string XSDPath = "";

        bool fileFound = true;
        Stream streamErrors = null;

        private void checkFile()
        {
            // Certificati

            texErrorsLog.Text += string.Format("---------------\r\nControllo i file Necessari\r\n---------------\r\n");
            if (!File.Exists(CrtPath + "aams_certificate.cer"))
            {
                texErrorsLog.Text += string.Format("File aams_certificate.cer (Certificato di AAMS) Non Trovato\r\n");
                fileFound = false;
            }
            else
            {
                texErrorsLog.Text += string.Format("aams_certificate.cer OK\r\n");
            }

            if (!File.Exists(CrtPath + "aams_public_key.pem"))
            {
                texErrorsLog.Text += string.Format("File aams_public_key.pem (Chiave Pubblica di AAMS) Non Trovata\r\n");
                fileFound = false;
            }
            else
            {
                texErrorsLog.Text += string.Format("aams_public_key.pem OK\r\n");
            }

            if (!File.Exists(CrtPath + "ilot.pem"))
            {
                texErrorsLog.Text += string.Format("File ilot.pem (Chiave Privata di Intralot) Non Trovata\r\n");
                fileFound = false;
            }
            else
            {
                texErrorsLog.Text += string.Format("ilot.pem OK\r\n");
            }

            //XSD
            if (!File.Exists(XSDPath + "forniture.xsd"))
            {
                texErrorsLog.Text += string.Format("File forniture.xsd  (Schema XSD delle forniture) Non Trovata\r\n");
                fileFound = false;
            }
            else
            {
                texErrorsLog.Text += string.Format("forniture.xsd OK\r\n");
            }
            texErrorsLog.Text += string.Format("---------------\r\n---------------\r\n\r\n");
        }

        public FormForniture()
        {
            InitializeComponent();

            BasePath = System.IO.Directory.GetCurrentDirectory();
            CrtPath = System.IO.Path.Combine(BasePath, @"..\..\out\PGAD_PRODUZIONE\CRT\");
            ForniturePath = System.IO.Path.Combine(BasePath, @"..\..\out\PGAD_PRODUZIONE\");
            XSDPath = System.IO.Path.Combine(BasePath, @"..\..\out\PGAD_PRODUZIONE\CRT\");
            
            //Controllo Che esistato i file necessari nei Path specificati
            checkFile();

            textBoxCrt.Text = CrtPath;
            textBoxXSD.Text = XSDPath;
            textBoxFileForniture.Text = ForniturePath;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((streamErrors = openFileDialog1.OpenFile()) != null)
                {
                    textBoxErrori.Text = openFileDialog1.FileName;
                }
                else
                {
                    textBoxErrori.Text = "File non caricato";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (streamErrors != null)
            {
                errori errors = LoadError.loadFile(streamErrors);
                if (errors != null)
                {
                    texErrorsLog.Text = String.Format("File Caricato Correttamente:\r\n\r\nFornitura n. {0} Errori importati: {1}", errors.id_fornitura, errors.errore.Length);
                }
                else
                {
                    texErrorsLog.Text = String.Format("Attenzione: Si è verificato un errore in fase di caricamento del file degli errori");
                }
                streamErrors.Close();
                streamErrors = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileFound == true)
            {
                int idForniture = 0;
                int top = 0;
                Int32.TryParse(textBoxForniture.Text, out idForniture);
                Int32.TryParse(textBoxTop.Text, out top);

                if (idForniture > 0 && top > 0)
                {
                    if (GeneraXml(idForniture, top))
                    {

                        buildFile("fornitura" + idForniture);
                        string fileFornituraCifrata=this.textBoxFileForniture.Text+"Fornitura"+idForniture+"\\"+"fornitura"+idForniture+"Cifrata";
                        if(File.Exists(fileFornituraCifrata))
                        {
                            texErrorsLog.Text = String.Format("File Creati Correttamente\r\n\r\nLa Fornitura da inviare è fornitura{0}Cifrata PATH:\r\n{1}\r\n", idForniture, fileFornituraCifrata);
                        }
                        else
                        {
                         texErrorsLog.Text ="Attenzione: Si è verificato un errore nella generazione del file.\r\nControlla L'installazione di OpenSSL";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Attenzione: Il file xml e il file zip da cifrare non sono stati creati.");
                    }
                }
                else
                {
                    MessageBox.Show("Attenzione: Specificare dei valori per Idfornitura e Top");
                }
            }
            else
            {
                MessageBox.Show("Attenzione: Non sono presenti tutti i file necessari alla creazione del file delle forniture.\r\nControlla se nel path certificati sono specificati i file: \r\n aams_certificate.cer,aams_public_key.pem,ilot.pem.\r\ne nel Path XSD il file forniture.xsd");
            }
        }

        /// <summary>
        /// Ritorna il path della cartella relativa alla fornitura da inviare
        /// </summary>
        /// <param name="DirName"></param>
        /// <returns></returns>
        public static string  MkDir(string DirName)
        {
            string DirPath = ForniturePath + DirName;
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath); 
            }
            return DirPath + "\\";
        }

        public static bool GeneraXml(int ID_FORNITURA, int TOP)
        {
            string pathFornitura = MkDir("Fornitura" + ID_FORNITURA);

            DataTable dt = Estrazione.EstraiAccount(4, Brand.INTRALOT, ID_FORNITURA, TOP);
            string xml = Estrazione.CreaXmlFornitura(ID_FORNITURA, dt, pathFornitura);
            XSDValidator val = new XSDValidator();

            val.Validate(xml, XSDPath + "forniture.xsd");
            
            Estrazione.SaveAsFile(xml, pathFornitura + "fornitura" + ID_FORNITURA + ".xml");
            Estrazione.SaveAsZipFile(xml, pathFornitura + "fornitura" + ID_FORNITURA + ".zip");

            if (File.Exists(pathFornitura + "fornitura" + ID_FORNITURA + ".xml") &&
                File.Exists(pathFornitura + "fornitura" + ID_FORNITURA + ".zip")
                )
            {
                return true;
            }

            return false;
        }

        public static void signFile(string name)
        {
            string param = String.Format("smime -sign -in {0}.zip -outform DER -binary -nodetach -signer {1} -passin pass:intralot -out {2}Firmata", ForniturePath + name + "\\"+name, CrtPath + "ilot.pem", ForniturePath + name+"\\"+name);

            ProcessStartInfo PSI = new ProcessStartInfo("cmd", "/c" + " openssl " + param);
            PSI.RedirectStandardOutput = true;
            PSI.UseShellExecute = false;
            PSI.CreateNoWindow = true;
            Process proc = new Process();
            proc.StartInfo = PSI;
            proc.Start();
            proc.WaitForExit();

            /*
            X509Certificate2 cert = new X509Certificate2(Settings.Instance.PfxFirmaPath, Settings.Instance.PfxFirmaPassword);
            UnicodeEncoding encoding = new UnicodeEncoding();
            StreamReader reader = new StreamReader(path);
            string text = reader.ReadToEnd();
            reader.Close();

            byte[] data = encoding.GetBytes(text);

            SignedCms signedCms = new SignedCms(new ContentInfo(data));
            CmsSigner cmsSigner = new CmsSigner(cert);
            cmsSigner.IncludeOption = X509IncludeOption.None;

            //  calcola la firma
            signedCms.ComputeSignature(cmsSigner);

            //  firma il messaggio
            byte[] dataS = signedCms.Encode();

            
            BinaryWriter w = new BinaryWriter(File.Create("file.out"));
            w.Write(dataS);
            w.Close();
            return true;
             */
        }


        public static void cryptFile(string name)
        {
            string param = String.Format("smime -encrypt -inkey {0} -in {1}Firmata -outform DER -des3 -binary -out {2}Cifrata {3}", CrtPath + "aams_public_key.pem", ForniturePath + name + "\\" + name, ForniturePath + name + "\\" + name, CrtPath + "aams_certificate.cer");
            ProcessStartInfo PSI = new ProcessStartInfo("cmd", "/c" + " openssl " + param);
            PSI.RedirectStandardOutput = true;
            PSI.UseShellExecute = false;
            PSI.CreateNoWindow = true;
            Process proc = new Process();
            proc.StartInfo = PSI;
            proc.Start();
            proc.WaitForExit();
        }

        public static void buildFile(string fileName)
        {
            signFile(fileName);
            cryptFile(fileName);
        }

        private void FormForniture_Load(object sender, EventArgs e)
        {

        }
    }
}
