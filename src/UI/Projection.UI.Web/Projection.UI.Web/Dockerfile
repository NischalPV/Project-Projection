#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UI/Projection.UI.Web/Projection.UI.Web/Projection.UI.Web.csproj", "src/UI/Projection.UI.Web/Projection.UI.Web/"]
COPY ["Projection.ServiceDefaults/Projection.ServiceDefaults.csproj", "Projection.ServiceDefaults/"]
COPY ["src/Common/Exceptions/Projection.ApiExceptions/Projection.ApiExceptions.csproj", "src/Common/Exceptions/Projection.ApiExceptions/"]
COPY ["src/BuildingBlocks/EventBus/Projection.EventBusRabbitMQ/Projection.EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBus/Projection.EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/Shared/Projection.Shared/Projection.Shared.csproj", "src/BuildingBlocks/Shared/Projection.Shared/"]
COPY ["src/BuildingBlocks/EventBus/Projection.EventBus/Projection.EventBus.csproj", "src/BuildingBlocks/EventBus/Projection.EventBus/"]
COPY ["src/UI/Projection.UI.Web/Projection.UI.Web.Client/Projection.UI.Web.Client.csproj", "src/UI/Projection.UI.Web/Projection.UI.Web.Client/"]
RUN dotnet restore "./src/UI/Projection.UI.Web/Projection.UI.Web/Projection.UI.Web.csproj"
COPY . .
WORKDIR "/src/src/UI/Projection.UI.Web/Projection.UI.Web"
RUN dotnet build "./Projection.UI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Projection.UI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projection.UI.Web.dll"]