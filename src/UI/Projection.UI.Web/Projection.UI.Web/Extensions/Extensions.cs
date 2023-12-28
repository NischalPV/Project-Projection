
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.IdentityModel.JsonWebTokens;
using Projection.ServiceDefaults;
using Projection.UI.Web.Services;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var identityUrl = configuration.GetRequiredValue("IdentityUrl");

        builder.AddAuthenticationServices();

        builder.AddRabbitMqEventBus("EventBus")
               .AddEventBusSubscriptions();

        builder.Services.AddScoped<LogOutService>();

        builder.Services.AddHttpClient<AccountingService>(o => o.BaseAddress = new("http://projection-accounting"))
            .AddAuthToken();

        builder.Services.AddHttpClient<UserService>(o => o.BaseAddress = new(identityUrl))
            .AddAuthToken();
    }

    public static void AddAuthenticationServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        var identityUrl = configuration.GetRequiredValue("IdentityUrl");
        var callBackUrl = configuration.GetRequiredValue("CallBackUrl");
        var openApiSection = configuration.GetSection("OpenApi");
        var authSection = openApiSection.GetSection("Auth");

        var clientId = authSection.GetRequiredValue("ClientId");
        var clientSecret = authSection.GetRequiredValue("ClientSecret");

        var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

        // Add Authentication services
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
        .AddOpenIdConnect(options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Authority = identityUrl;
            options.SignedOutRedirectUri = callBackUrl;
            options.ClientId = clientId;
            options.ClientSecret = clientSecret;
            options.ResponseType = "code";
            options.ResponseMode = "query";
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.RequireHttpsMetadata = false;
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("AccountingAPI");
        });

        // Blazor auth services
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();
    }

    public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
    }
}
