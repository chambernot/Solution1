
CREATE TABLE [dbo].[Evento](
	[Id] [uniqueidentifier] NOT NULL,
	[TipoEventoId] [smallint] NOT NULL,
	[Identificador] [uniqueidentifier] NOT NULL,
	[IdentificadorCorrelacao] [varchar](36) NULL,
	[IdentificadorNegocio] [varchar](36) NULL,
	[DataExecucaoEvento] [datetime] NOT NULL,
	[DataCriacaoEvento] [datetime] NOT NULL,
 CONSTRAINT [PK_EventoImplantado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE INDEX [IX_Evento_Identificador] ON [dbo].[Evento] ([Identificador])

GO




CREATE INDEX [IX_Evento_TipoEventoId] ON [dbo].[Evento] ([TipoEventoId])
