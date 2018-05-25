CREATE TABLE [dbo].[ProvisaoCobertura]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [CoberturaContratadaId] UNIQUEIDENTIFIER NOT NULL, 
    [TipoProvisaoId] SMALLINT NOT NULL, 
    CONSTRAINT [FK_ProvisaoCobertura_Cobertura] FOREIGN KEY ([CoberturaContratadaId]) REFERENCES [dbo].[CoberturaContratada]([Id])
)

GO

CREATE INDEX [IX_ProvisaoCobertura_Cobertura] ON [dbo].[ProvisaoCobertura] ([CoberturaContratadaId])

GO

CREATE INDEX [IX_ProvisaoCobertura_TipoProvisaoId] ON [dbo].[ProvisaoCobertura] ([TipoProvisaoId])
