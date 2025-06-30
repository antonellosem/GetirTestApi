using System.Net;
using Backend.Proxy;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Gamenet.Common.Interface;
using Gamenet.Infrastructure;
using Gamenet.Logger;
using Gamenet.Proxy;
using Gamenet.RetryIntegration.CoreMiddleware;
using Gamenet.RetryIntegration.Dao;
using Gamenet.RetryIntegration.Manager;
using GrattaEVinci.Common;
using GrattaEVinci.Common.ModelConfiguration;
using GrattaEVinci.Service;
using GrattaEVinci.Data;
using GrattaEVinci.Manager;
using GrattaEVinciReference;
using NLog;
using ServiceEndpoint = CoreWCF.Description.ServiceEndpoint;
using BasicHttpBinding = CoreWCF.BasicHttpBinding;
using BasicHttpSecurityMode = CoreWCF.Channels.BasicHttpSecurityMode;
using GrattaEVinci;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers().AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var nLogConfigFileName = builder.Environment.IsProduction() ? "nlog.Production.config" : "nlog.config";
LogManager.LoadConfiguration(nLogConfigFileName);

GlobalDiagnosticsContext.Set("hostname", Dns.GetHostName().ToLower());

//utilizzato in caso di body vuoto nella chiamata post
builder.Services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));

// Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var configSection = builder.Configuration.GetSection("Config");
var appSetting = configSection.Get<Config>();
builder.Services.Configure<Config>(configSection);
builder.Services.Configure<Config>(c => c.BasePath = builder.Environment.ContentRootPath);

builder.Services.Configure<Gamenet.Proxy.Model.Seamless>(configSection.GetSection("Seamless"));
builder.Services.Configure<Backend.Proxy.Model.Backend>(configSection.GetSection("BackendEndPoints"));
builder.Services.Configure<Gamenet.Data.Model.MongoOptions>(options => options.ConnectionString = appSetting.ConnectionStrings.ConnectionStringTransactionMongoDbRetry);
builder.Services.Configure<TransactionMongoOptions>(options => options.ConnectionString = appSetting.ConnectionStrings.ConnectionStringTransactionMongoDb);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Mongo
builder.Services.AddSingleton<MongoDbRetry>();
builder.Services.AddSingleton<TransactionManagerMongoDb>();
builder.Services.AddTransient<ITransaction<Transaction>, MongoDbTransaction>();

//Log
builder.Services.AddSingleton<ILog, LogNLog>();

//Manager
builder.Services.AddSingleton<AccountManager>();
builder.Services.AddSingleton<IRetryIntegrationManager, WebApiRetryIntegrationManager>();
builder.Services.AddSingleton<CashierManager>();
builder.Services.AddSingleton<Validator>();
builder.Services.AddSingleton<DeskService_v2>();
builder.Services.AddSingleton<GestioneAnagrafica>();

//IDaoConnectionStringsProvider  
builder.Services.AddSingleton<IDaoConnectionStringsProvider>(new DaoConnectionStringsProvider(appSetting.ConnectionStrings.GrattaEVinci_GB, appSetting.ConnectionStrings.Tools));

//Dao
builder.Services.AddSingleton<GrattaEVinciDao>();

//Http Client
builder.Services.AddHttpClient<SeamlessProxy, SeamlessProxy>();
builder.Services.AddHttpClient<BackendAccountProxy, BackendAccountProxy>();
builder.Services.AddHttpClient<AuthBackendProxy, AuthBackendProxy>();
builder.Services.AddHttpClient<BackendCashierAndBonusProxy, BackendCashierAndBonusProxy>();

var allowOrigins = "AllowOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowOrigins,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.EnableRequestBuffering();
app.UseServiceModel(service =>
{
    var encodingOption = new Action<ServiceEndpoint>(endpoint =>
    {
        endpoint.Name = "DeskPort_v2";
    });

    service
        .AddService<DeskService_v2>()
        .AddService<GestioneAnagrafica>()
        .AddServiceEndpoint<DeskService_v2, Desk>(new BasicHttpBinding(BasicHttpSecurityMode.None), "/DeskService_v2.svc", encodingOption);
    service.AddService<DeskService_v2>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    })
        .AddServiceEndpoint<GestioneAnagrafica, IGestioneAnagrafica>(new BasicHttpBinding(BasicHttpSecurityMode.None), "/GestioneAnagrafica.svc");
    service.AddService<GestioneAnagrafica>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();

    serviceMetadataBehavior.HttpGetEnabled = true;
});
app.UseCors(allowOrigins);

app.MapControllers();

app.Run();
