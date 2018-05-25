CREATE TABLE [dbo].[MovimentoProvisaoPremio]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProvisaoCoberturaId] UNIQUEIDENTIFIER NOT NULL, 
    [EventoId] UNIQUEIDENTIFIER NOT NULL, 
    [PremioId] UNIQUEIDENTIFIER NULL, 
    [Sequencial] INT NOT NULL,     
    [DataMovimento] DATE NOT NULL, 
    [QuantidadeContribuicoes] INT NOT NULL, 
	[Fator] NUMERIC(20,10),
    [PercentualCarregamento] NUMERIC(12, 10) NULL, 
    [ValorBeneficioCorrigido] NUMERIC(12, 2) NULL,
    [ValorJuros] NUMERIC(12, 2) NULL, 
    [ValorAtualizacao] NUMERIC(12, 2) NULL, 
    [ValorSobrevivencia] NUMERIC(12, 2) NULL, 
    [ValorProvisao] NUMERIC(12, 2) NOT NULL, 
	[Desvio] NUMERIC(12, 2) NULL, 
    [ValorFIF] NUMERIC(10, 8) NULL, 
    CONSTRAINT [FK_MovimentoProvisaoPremio_ToEvento] FOREIGN KEY ([EventoId]) REFERENCES [dbo].[Evento]([Id]), 
    CONSTRAINT [FK_MovimentoProvisaoPremio_ToPremio] FOREIGN KEY ([PremioId]) REFERENCES [dbo].[Premio]([Id]), 
    CONSTRAINT [FK_MovimentoProvisaoPremio_ToProvisaoCobertura] FOREIGN KEY ([ProvisaoCoberturaId]) 
	REFERENCES [dbo].[ProvisaoCobertura]([Id]), 

)

GO

CREATE INDEX [IX_MovimentoProvisaoPremio_ProvisaoCoberturaId] ON [dbo].[MovimentoProvisaoPremio] ([ProvisaoCoberturaId])

GO

CREATE INDEX [IX_MovimentoProvisaoPremio_EventoId] ON [dbo].[MovimentoProvisaoPremio] ([EventoId])

GO

CREATE INDEX [IX_MovimentoProvisaoPremio_PremioId] ON [dbo].[MovimentoProvisaoPremio] ([PremioId])
