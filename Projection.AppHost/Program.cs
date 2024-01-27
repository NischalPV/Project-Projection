var builder = DistributedApplication.CreateBuilder(args);

//var redis = builder.AddRedisContainer("redis");
//var rabbitMq = builder.AddRabbitMQContainer("EventBus", 5672, "guest");

var identity = builder.AddProject<Projects.Projection_Identity>("projection-identity");

var accountingApi = builder.AddProject<Projects.Projection_Accounting>("projection-accounting")
    //.WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("Identity__Url", identity);

var webUi = builder.AddProject<Projects.Projection_UI_Web>("projection-ui-web")
    .WithReference(accountingApi)
    .WithEnvironmentForServiceBinding("IdentityUrl", identity)
    .WithLaunchProfile("https");

var apiGateway = builder.AddProject<Projects.Projection_ApiGateway>("projection-apigateway");

webUi.WithEnvironmentForServiceBinding("CallBackUrl", webUi, bindingName: "https");

identity.WithEnvironmentForServiceBinding("webUiClient", webUi, bindingName: "https");


builder.Build().Run();
