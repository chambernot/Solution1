CREATE TABLE [dbo].[CoberturaContratada](
	[Id] [uniqueidentifier] NOT NULL,
	[EventoId] [uniqueidentifier] NOT NULL,
	[InscricaoId] [bigint] NOT NULL,
	[ItemCertificadoApoliceId] [bigint] NOT NULL,
	[ItemProdutoId] [int] NOT NULL,
	[ClasseRiscoId] [int] NULL,
	[TipoFormaContratacaoId] [int] NULL,
	[TipoRendaId] [int] NULL,
	[DataNascimento] DATE NOT NULL,
	[Sexo] [nvarchar](20) NOT NULL,
	[CodigoProduto] [int] NULL,
	[Matricula] [bigint] NOT NULL,
	[DataInicioVigencia] DATE NOT NULL,
	[DataFimVigencia] DATE NULL, 
	[DataAssinatura] DATE NOT NULL,
	[IndiceBeneficioId] [int] NULL,
	[IndiceContribuicaoId] [int] NULL,
	[ModalidadeCoberturaId] [int] NULL,
	[ProdutoId] [int] NULL,
	[RegimeFinanceiroId] [int] NULL,
	[TipoItemProdutoId] [int] NULL,
	[NomeProduto] [nvarchar](200) NULL,
	[NumeroBeneficioSusep] [int] NULL,
	[NumeroProcessoSusep] [nvarchar](100) NULL,
	[PlanoFipSusep] [int] NULL,
	[TipoProvisoes] [int] NULL,
	[PermiteResgateParcial] [bit] NULL, 
    [PrazoCoberturaEmAnos] INT NULL, 
    [PrazoDecrescimoEmAnos] INT NULL, 
    [PrazoPagamentoEmAnos] INT NULL, 
    [NumeroContribuicoesInicial] INT NULL DEFAULT(0),
    CONSTRAINT [PK_CoberturaContratada] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CoberturaContratada]  ADD CONSTRAINT [FK_CoberturaContratada_ToEvento] FOREIGN KEY([EventoId])
REFERENCES [dbo].[Evento] ([Id])
GO

ALTER TABLE [dbo].[CoberturaContratada] CHECK CONSTRAINT [FK_CoberturaContratada_ToEvento]
GO




CREATE INDEX [IX_CoberturaContratada_EventoId] ON [dbo].[CoberturaContratada] ([EventoId])

GO

CREATE INDEX [IX_CoberturaContratada_ItemCertificadoApoliceId] ON [dbo].[CoberturaContratada] ([ItemCertificadoApoliceId])

GO




CREATE INDEX [IX_CoberturaContratada_InscricaoId] ON [dbo].[CoberturaContratada] ([InscricaoId])

GO

CREATE INDEX [IX_CoberturaContratada_ItemProdutoId] ON [dbo].[CoberturaContratada] ([ItemProdutoId])
