global using System;
global using System.Net.Sockets;
global using System.Text;
global using Autofac;
global using Microsoft.Extensions.Logging;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;
global using Polly;
global using Polly.Retry;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using RabbitMQ.Client.Exceptions;

global using Projection.BuildingBlocks.EventBus;
global using Projection.BuildingBlocks.EventBus.Abstractions;
global using Projection.BuildingBlocks.EventBus.Events;
global using Projection.BuildingBlocks.EventBus.Extensions;