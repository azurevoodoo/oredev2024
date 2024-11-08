CREATE TABLE [Config].[Currency] (
    [Id]            BIGINT  IDENTITY(1, 1)  NOT NULL PRIMARY KEY,
    [PublicId]      NVARCHAR(128)           NOT NULL,
    [Description]   NVARCHAR(256)           NOT NULL,
    [Created]       DATETIMEOFFSET          NOT NULL DEFAULT (SYSDATETIMEOFFSET())
)