#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/Projection.Identity/Projection.Identity.csproj", "src/Services/Identity/Projection.Identity/"]
RUN dotnet restore "src/Services/Identity/Projection.Identity/Projection.Identity.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Projection.Identity"
RUN dotnet build "Projection.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projection.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projection.Identity.dll"]
