
CREATE PROCEDURE [dbo].[sp_CriaEvento](		
	@Identificador uniqueidentifier,
	@TipoEventoId INT,	
	@IdentificadorNegocio VARCHAR(36),
	@IdentificadorCorrelacao VARCHAR(36),
	@DataExecucaoEvento DATETIME,
	@DataCriacaoEvento DATETIME
)
AS
BEGIN
	DECLARE @id UNIQUEIDENTIFIER = NEWID();

	INSERT INTO Evento (Id, Identificador, TipoEventoId, IdentificadorCorrelacao, IdentificadorNegocio, DataExecucaoEvento, DataCriacaoEvento)
		VALUES (@id, @Identificador, @TipoEventoId, @IdentificadorCorrelacao, @IdentificadorNegocio, @DataExecucaoEvento, @DataCriacaoEvento)

	SELECT @id;

	RETURN 0;

END
