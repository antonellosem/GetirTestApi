using System;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using Gamenet.Infrastructure;
using Gamenet.Logger;
using PgadCommunication;
using PgadCommunication.Contracts.Requests;
using PgadCommunication.Profiles;
using PgadData;
using PgadData.Entity;
using PGADServiceLibrary;

namespace PGADService
{
    public class Global : System.Web.HttpApplication
    {

        

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LogNLog>().As<ILog>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfilePacg>();
                cfg.CreateMap<ConcessionarioInfo, HeaderPgad>();
            });

            builder.Register(m => new Mapper(config)).As<IMapper>();
            builder.RegisterType<PGADServiceLibrary.PGADService>();
            builder.RegisterType<PgadServiceJson>();
            
            var pacgConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AnagraficaContiGiocoConnectionString"];
            var transazioniConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PgadDBTransaction"];

            builder.Register(c => new DaoConnectionStringsProvider(
                pacgConnectionString.ToString(),
                transazioniConnectionString.ToString())).As<ITransazioniConnectionStringsProvider>();

            builder.RegisterType<TransazioniDao>().As<IDapperBaseDao>();
            builder.RegisterType<PgadGateway>().As<IPgadGateway>();

            var container = builder.Build();
            AutofacHostFactory.Container = container;

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}