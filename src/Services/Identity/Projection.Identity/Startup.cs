using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Builder;
using Projection.Identity.Models;
using Projection.Identity.Services;
using Projection.Identity.Devspaces;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Projection.Identity;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    // public ILifetimeScope AutofacContainer { get; private set; }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        // RegisterAppInsights(services);

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
        }));

        services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<AppSettings>(Configuration);

        if (Configuration.GetValue<string>("IsClusterEnv") == bool.TrueString)
        {
            services.AddDataProtection(opts =>
            {
                opts.ApplicationDiscriminator = "projection.identity";
            })
            .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(Configuration["DPConnectionString"]), "DataProtection-Keys");
        }

        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddNpgSql(connectionString,
                name: "IdentityDB-check",
                tags: new string[] { "IdentityDB" });

        services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();
        services.AddTransient<IRedirectService, RedirectService>();

        services.AddIdentityServer(x =>
        {
            x.IssuerUri = "null";
            x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
        })
        .AddDevspacesIfNeeded(Configuration.GetValue("EnableDevspaces", false))
        .AddSigningCredential(Certificate.Get(Configuration.GetValue<string>("SigningCertificate")))
        .AddAspNetIdentity<ApplicationUser>()
        .AddConfigurationStore<Data.ConfigurationDbContext>(options =>
        {
            options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                    //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                });
        })
        .AddOperationalStore<Data.PersistedGrantDbContext>(options =>
        {
            options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                    //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                });
        })
        .Services.AddTransient<IProfileService, ProfileService>();
        // .AddSingleton<OperationalStoreOptions>();

        services.AddControllers();
        services.AddControllersWithViews();
        services.AddRazorPages();

        //var container = new ContainerBuilder();
        //container.Populate(services);

        //return new AutofacServiceProvider(container.Build());

    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
            await next();
        });

        app.UseForwardedHeaders();
        app.UseIdentityServer();

        // Fix a problem with chrome. Chrome enabled a new feature "Cookies without SameSite must be secure", 
        // the coockies shold be expided from https, but in eShop, the internal comunicacion in aks and docker compose is http.
        // To avoid this problem, the policy of cookies shold be in Lax mode.
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None,
            Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest,
            MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax,
        });

        app.UseRouting();

        // app.UseAuthentication();
        // app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

        });
    }
    // ConfigureContainer is where you can register things directly
    // with Autofac. This runs after ConfigureServices so the things
    // here will override registrations made in ConfigureServices.
    // Don't build the container; that gets done for you by the factory.
    // public void ConfigureContainer(ContainerBuilder builder)
    // {
    //     // Register your own things directly with Autofac here. Don't
    //     // call builder.Populate(), that happens in AutofacServiceProviderFactory
    //     // for you.
    //     //builder.RegisterModule(new MyApplicationModule());
    // }

    // private void RegisterAppInsights(IServiceCollection services)
    // {
    //     services.AddApplicationInsightsTelemetry(Configuration);
    //     services.AddApplicationInsightsKubernetesEnricher();
    // }
}

