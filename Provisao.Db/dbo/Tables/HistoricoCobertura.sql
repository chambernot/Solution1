CREATE TABLE [dbo].[HistoricoCoberturaContratada](
	[Id] [uniqueidentifier] NOT NULL,
	[EventoId] [uniqueidentifier] NULL,
	[CoberturaContratadaId] [uniqueidentifier] NOT NULL,
	[StatusCoberturaId] SMALLINT NOT NULL DEFAULT 1,
	[DataNascimentoBeneficiario] DATE NULL,
	[SexoBeneficiario] [nvarchar](20) NULL,
	[PeriodicidadeId] [int] NULL,
	[Sequencia] [int] NOT NULL,
 [ValorBeneficio] NUMERIC(12, 2) NULL, 
    [ValorCapital] NUMERIC(12, 2) NULL, 
    [ValorContribuicao] NUMERIC(12, 2) NULL, 
    CONSTRAINT [PK_HistoricoCoberturaContratada] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[HistoricoCoberturaContratada]  ADD CONSTRAINT [FK_HistoricoCoberturaContratada_ToCoberturaContratada] FOREIGN KEY([CoberturaContratadaId])
REFERENCES [dbo].[CoberturaContratada] ([Id])
GO

ALTER TABLE [dbo].[HistoricoCoberturaContratada] CHECK CONSTRAINT [FK_HistoricoCoberturaContratada_ToCoberturaContratada]
GO

ALTER TABLE [dbo].[HistoricoCoberturaContratada]  ADD CONSTRAINT [FK_HistoricoCoberturaContratada_ToEvento] FOREIGN KEY([EventoId])
REFERENCES [dbo].[Evento] ([Id])
GO

ALTER TABLE [dbo].[HistoricoCoberturaContratada] CHECK CONSTRAINT [FK_HistoricoCoberturaContratada_ToEvento]
GO

GO

CREATE INDEX [IX_HistoricoCoberturaContratada_CoberturaContratadaId] ON [dbo].[HistoricoCoberturaContratada] ([CoberturaContratadaId])

GO

CREATE INDEX [IX_HistoricoCoberturaContratada_EventoId] ON [dbo].[HistoricoCoberturaContratada] ([EventoId])
