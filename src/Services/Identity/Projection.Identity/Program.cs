
using Projection.Identity;
using Serilog;

string Namespace = typeof(Startup).Namespace;
string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

var host = BuildWebHost(configuration, args);
host.Run();

IConfiguration GetConfiguration()
{
    string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var config = builder.Build();

    return builder.Build();
}

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://localhost:8080" : logstashUrl, queueLimitBytes: null)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

IHost BuildWebHost(IConfiguration configuration, string[] args) =>

    Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.CaptureStartupErrors(false);
        webBuilder.ConfigureAppConfiguration(x => x.AddConfiguration(configuration));
        webBuilder.UseStartup<Startup>();
        webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
    })
    .UseSerilog()
    .Build();