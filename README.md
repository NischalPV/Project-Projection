# Project-Projection

[![Status Check](https://github.com/NischalPV/Project-Projection/actions/workflows/dotnet.yml/badge.svg?branch=experimental)](https://github.com/NischalPV/Project-Projection/actions/workflows/dotnet.yml)

## Setting up database

Project projection uses PostgreSQL at relational database.

run below docker/podman command to setup the database

```bash
sudo podman run --name PostgreSQL -e POSTGRES_PASSWORD=Radeon1GB# -e POSTGRES_USER=sa -p 5432:5432 -v "/mnt/sdc/postgre-sql/data:/var/lib/postgresql/data:Z" -d postgres
podman run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Radeon@1GB#' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --name SQL-Server-2022 mcr.microsoft.com/mssql/server:2022-latest
```

## Setting up messaging bus

We are using rabbitMQ as messaging bus.

run below docker/podman command to setup the rabbitMQ

```bash
podman run -d --rm -it -p 15672:15672 -p 5672:5672 --name RabbitMQ rabbitmq:3-management
```

## Setting up NoSQL database - MongoDb

```bash
```
