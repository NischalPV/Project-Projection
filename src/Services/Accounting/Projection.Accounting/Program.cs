using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Projection.Accounting;
using Projection.Accounting.Features.Accounting.Apis;
using Projection.Accounting.Infrastructure.Data;
using Projection.BuildingBlocks.EventBus;
using Projection.BuildingBlocks.EventBus.Abstractions;
using Projection.BuildingBlocks.EventBusRabbitMQ;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.BuildingBlocks.IntegrationEventLogEF.Services;
using Projection.Common.BaseEntities;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Configuration;

using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDefaultOpenApi();

builder.AddApplicationServices();

builder.Services.AddApiVersioning(
    config =>
    {
        config.DefaultApiVersion = new ApiVersion(1, 0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
        config.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


builder.Services.AddExceptionHandler<HttpGlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScopedServices();

var app = builder.Build();

app.UseDefaultOpenApi();

app.UseRouting();

app.UseAuthorization();

app.UseCors("CorsPolicy");

var versionSet = app.NewApiVersionSet()
                            .HasApiVersion(1, 0)
                            .HasApiVersion(2, 0)
                            .ReportApiVersions()
                            .Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();
app.MapControllers();

app.MapDefaultEndpoints();

app.MapGroup("api/accounts")
    .MapAccountsApi()
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(1, 0)
    .WithTags("Accounting")
    .RequireAuthorization();



Log.Logger = CreateSerilogLogger(builder.Configuration);

Serilog.ILogger CreateSerilogLogger(ConfigurationManager configuration)
{
    string Namespace = "Projection.Accounting";
    string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("AccountingContext", AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://localhost:8080" : logstashUrl, queueLimitBytes: null)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//app.UseHttpsRedirection();
app.UseStaticFiles();


app.Run();
