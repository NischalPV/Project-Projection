CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'integration') THEN
        CREATE SCHEMA integration;
    END IF;
END $EF$;

CREATE TABLE integration."IntegrationEventLogs" (
    "EventId" uuid NOT NULL,
    "EventTypeName" text NOT NULL,
    "State" integer NOT NULL,
    "TimesSent" integer NOT NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "Content" text NOT NULL,
    "TransactionId" uuid NOT NULL,
    CONSTRAINT "PK_IntegrationEventLogs" PRIMARY KEY ("EventId")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231209203736_Initial', '8.0.0');

COMMIT;

