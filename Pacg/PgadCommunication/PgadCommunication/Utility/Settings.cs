using System;
using System.Collections.Generic;
using System.Configuration;
using PgadCommunication.Model;
using PgadCommunication.Configuration;
using Gamenet.Logger;
using System.Net;
using NLog;

namespace PgadCommunication.Utility
{
    public class Settings
    {
        private static object _sync= new object();
        private static Settings _instance;
        
        public string PgadGatewayClass { get; set; }
        public string AnagraficaContiGiocoConnectionString { get; set; }
        public string PgadDBTransactionConnectionString { get; set; }
        public Dictionary<string, Config> Config { get; set; }
        public static ILog logger = null;
        public string ToolsDBConnectionString { get; set; }
        private void LoadConfiguration( )
        {
            Config = new Dictionary<string, Config>();

            this.AnagraficaContiGiocoConnectionString = ConfigurationManager.ConnectionStrings["AnagraficaContiGiocoConnectionString"].ConnectionString;
            this.PgadDBTransactionConnectionString = ConfigurationManager.ConnectionStrings["PgadDBTransaction"].ConnectionString;
            this.ToolsDBConnectionString = ConfigurationManager.ConnectionStrings["ToolsDBConnectionString"].ConnectionString;
            this.PgadGatewayClass = ConfigurationManager.AppSettings["PgadGatewayClass"].ToString();
            logger = new LogNLog();
            this.Config = PgadAdapter.getConfigurazioneConcessionario(this.AnagraficaContiGiocoConnectionString);
            //var nLogConfigFileName = ConfigurationManager.AppSettings["Ambiente"].ToString().ToLower() == "prod"
            //      ? "nlog.Production.config"
            //      : "nlog.config";

            var nLogConfigFileName = "nlog.config";

            LogManager.LoadConfiguration(nLogConfigFileName);
            GlobalDiagnosticsContext.Set("hostname", Dns.GetHostName().ToLower());

            logger = new LogNLog();
        }

        private Settings()
        {
            LoadConfiguration();
        }

        public static Settings Instance
        {
            get {
                lock (_sync)
                {
                    if (_instance==null)
                    {
                        _instance= new Settings();
                    }
                }
                return _instance;
            }
        }
    }
}
