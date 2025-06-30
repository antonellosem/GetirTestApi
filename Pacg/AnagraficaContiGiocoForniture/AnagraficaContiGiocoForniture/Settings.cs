using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AnagraficaContiGiocoForniture
{
    public class Settings
    {
        private static object _padlock = new object();
        private static Settings _instance;
        

        public string WhiteLabelConnection;
        public string FilePathEstrazione { get; set; }
        public string PathCRT { get; set; }
        
        private string ambiente;
        

        private Settings()
        { 
            this.ambiente=ConfigurationManager.AppSettings["Ambiente"];
            this.WhiteLabelConnection = ConfigurationManager.ConnectionStrings[this.ambiente + "_" + "WHITELABEL"].ToString();
            this.FilePathEstrazione = ConfigurationManager.AppSettings[this.ambiente + "_" + "FilePath_Estrazione"];
            this.PathCRT = ConfigurationManager.AppSettings[this.ambiente + "_" + "CRT"];
        }

        

        public static Settings Instance
        {
            get 
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    { 
                        _instance=new Settings();
                    }
                }

                return _instance;
            }
        }
    }
}
