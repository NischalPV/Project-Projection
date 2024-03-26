using System.ComponentModel;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projection.BuildingBlocks.EventBus.Abstractions;
using Projection.BuildingBlocks.EventBusRabbitMQ;

namespace Microsoft.Extensions.Hosting;

public static class RabbitMqDependencyInjectionExtensions
{
    // {
    //   "EventBus": {
    //     "SubscriptionClientName": "...",
    //     "RetryCount": 10
    //   }
    // }

    private const string SectionName = "EventBus";

    public static IEventBusBuilder AddRabbitMqEventBus(this IHostApplicationBuilder builder, string connectionName, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // builder.AddRabbitMQ(connectionName, configureConnectionFactory: factory =>
        // {
        //     ((ConnectionFactory)factory).DispatchConsumersAsync = true;
        // });

        builder.Services.AddMassTransit(config =>
        {
            config.AddConsumers(assembly);
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(builder.Configuration.GetConnectionString(connectionName), "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        // RabbitMQ.Client doesn't have built-in support for OpenTelemetry, so we need to add it ourselves
        builder.Services.AddOpenTelemetry()
           .WithTracing(tracing =>
           {
               tracing.AddSource(RabbitMQTelemetry.ActivitySourceName);
           });

        // Options support
        builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection(SectionName));

        // Abstractions on top of the core client API
        builder.Services.AddSingleton<RabbitMQTelemetry>();
        builder.Services.AddScoped<IEventBus, RabbitMQEventBus>();

        // // Start consuming messages as soon as the application starts
        // builder.Services.AddSingleton<IHostedService>(sp => (RabbitMQEventBus)sp.GetRequiredService<IEventBus>());

        return new EventBusBuilder(builder.Services);
    }


    private class EventBusBuilder(IServiceCollection services) : IEventBusBuilder
    {
        public IServiceCollection Services => services;
    }
}

