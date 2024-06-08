var builder = DistributedApplication.CreateBuilder(args);
builder.AddForwardedHeaders();

# region Services

/* Identity Service*/
var identity = builder.AddProject<Projects.Projection_Identity>("projection-identity");
var idpHttp = identity.GetEndpoint("http");

/* Accounting Service */
var accountingApi = builder.AddProject<Projects.Projection_Accounting>("projection-accounting")
    .WithEnvironment("Identity__Url", idpHttp);

/* Api Gateway */
var apiGateway = builder.AddProject<Projects.Projection_ApiGateway>("projection-apigateway");

/* Web UI */
var webUi = builder.AddProject<Projects.Projection_UI_Web>("projection-ui-web")
    .WithReference(accountingApi)
    .WithReference(apiGateway)
    .WithEnvironment("IdentityUrl", idpHttp)
    .WithLaunchProfile("https");

/* Background Service */
builder.AddProject<Projects.Projection_Orchestration>("projection-orchestration");

/* Process Management */
builder.AddProject<Projects.Projection_ProcessManagement>("projection-processmanagement")
    .WithEnvironment("Identity__Url", idpHttp)
    .WithLaunchProfile("https");


#endregion

webUi.WithEnvironment("CallBackUrl", webUi.GetEndpoint("https"));

identity.WithEnvironment("webUiClient", webUi.GetEndpoint("https"));

builder.Build().Run();
