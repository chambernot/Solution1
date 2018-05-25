CREATE TABLE [dbo].[Premio]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [EventoId] UNIQUEIDENTIFIER NOT NULL, 
    [HistoricoCoberturaId] UNIQUEIDENTIFIER NOT NULL, 
    [TipoMovimentoId] SMALLINT NOT NULL, 
	[Numero] INT NOT NULL,
    [Competencia] DATE NOT NULL, 
    [InicioVigencia] DATE NULL, 
    [FimVigencia] DATE NULL, 
    [ValorPremio] NUMERIC(12, 2) NOT NULL, 
    [ValorCarregamento] NUMERIC(12, 2) NOT NULL, 
    [ValorBeneficio] NUMERIC(12, 2) NOT NULL, 
    [ValorCapitalSegurado] NUMERIC(12, 2) NOT NULL, 
    [DataPagamento] DATE NULL, 
    [DataApropriacao] DATE NULL, 
    [ValorPago] NUMERIC(12, 2) NULL, 
    [Desconto] NUMERIC(12, 2) NULL, 
    [Multa] NUMERIC(12, 2) NULL, 
    [IOFRetido] NUMERIC(12, 2) NULL, 
    [IOFARecolher] NUMERIC(12, 2) NULL, 
    [IdentificadorCredito] NVARCHAR(50) NULL, 
    [CodigoSusep] NVARCHAR(100) NULL, 
    [Sequencial] INT NULL, 
    CONSTRAINT [FK_Premio_ToEvento] FOREIGN KEY ([EventoId]) REFERENCES [dbo].[Evento]([Id]), 
    CONSTRAINT [FK_Premio_ToHistoricoCobertura] FOREIGN KEY ([HistoricoCoberturaId]) REFERENCES [dbo].[HistoricoCoberturaContratada]([Id])
)

GO

CREATE INDEX [IX_Premio_Evento] ON [dbo].[Premio] ([EventoId])

GO

CREATE INDEX [IX_Premio_HisotricoCobertura] ON [dbo].[Premio] ([HistoricoCoberturaId])
