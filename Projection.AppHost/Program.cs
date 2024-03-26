var builder = DistributedApplication.CreateBuilder(args);
builder.AddForwardedHeaders();

//var redis = builder.AddRedisContainer("redis");
//var rabbitMq = builder.AddRabbitMQContainer("EventBus", 5672, "guest");

var identity = builder.AddProject<Projects.Projection_Identity>("projection-identity");
var idpHttp = identity.GetEndpoint("http");

var accountingApi = builder.AddProject<Projects.Projection_Accounting>("projection-accounting")
    //.WithReference(rabbitMq)
    .WithEnvironment("Identity__Url", idpHttp);

var apiGateway = builder.AddProject<Projects.Projection_ApiGateway>("projection-apigateway");

var webUi = builder.AddProject<Projects.Projection_UI_Web>("projection-ui-web")
    .WithReference(accountingApi)
    .WithReference(apiGateway)
    .WithEnvironment("IdentityUrl", idpHttp)
    .WithLaunchProfile("https");


webUi.WithEnvironment("CallBackUrl", webUi.GetEndpoint("https"));

identity.WithEnvironment("webUiClient", webUi.GetEndpoint("https"));



builder.AddProject<Projects.Projection_Orchestration>("projection.orchestration");



builder.Build().Run();
