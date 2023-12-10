var builder = DistributedApplication.CreateBuilder(args);

//var redis = builder.AddRedisContainer("redis");
//var rabbitMq = builder.AddRabbitMQContainer("EventBus");

var identity = builder.AddProject<Projects.Projection_Identity>("projection-identity");

var accountingApi = builder.AddProject<Projects.Projection_Accounting>("projection-accounting");

builder.Build().Run();
