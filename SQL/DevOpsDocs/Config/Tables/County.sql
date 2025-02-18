CREATE TABLE [Config].[Country] (
    [Id]            BIGINT  IDENTITY(1, 1)  NOT NULL PRIMARY KEY,
    [PublicId]      NVARCHAR(128)           NOT NULL,
    [Description]   NVARCHAR(256)           NOT NULL,
    [CurrencyId]    BIGINT                  NOT NULL,
    [Created]       DATETIMEOFFSET          NOT NULL DEFAULT (SYSDATETIMEOFFSET())
)
GO
ALTER TABLE [Config].[Country]
    ADD CONSTRAINT FK_Country_Currency
    FOREIGN KEY (CurrencyId) REFERENCES [Config].Currency;