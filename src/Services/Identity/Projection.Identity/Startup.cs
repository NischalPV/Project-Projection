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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
        })

        //options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
        //{
        //    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
        //    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        //})
        );



        services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<AppSettings>(Configuration);

        services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();
        services.AddTransient<IRedirectService, RedirectService>();

        services.AddIdentityServer(x =>
        {
            x.IssuerUri = "null";
            x.Authentication.CookieLifetime = TimeSpan.FromHours(2);

            x.Events.RaiseErrorEvents = true;
            x.Events.RaiseInformationEvents = true;
            x.Events.RaiseFailureEvents = true;
            x.Events.RaiseSuccessEvents = true;
        })
        .AddDevspacesIfNeeded(Configuration.GetValue("EnableDevspaces", false))
        .AddDeveloperSigningCredential()
        //.AddSigningCredential(Certificate.Get(Configuration.GetValue<string>("SigningCertificate")))
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

            //options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
            //    sqlServerOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly(migrationsAssembly);
            //        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
            //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //    });
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

            //options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
            //    sqlServerOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly(migrationsAssembly);
            //        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
            //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //    });
        })
        .Services.AddTransient<IProfileService, ProfileService>();
        // .AddSingleton<OperationalStoreOptions>();

        services.AddControllers();
        services.AddControllersWithViews();
        services.AddRazorPages();

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

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Append("Content-Security-Policy", "script-src 'unsafe-inline'");
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

        });
    }
}

