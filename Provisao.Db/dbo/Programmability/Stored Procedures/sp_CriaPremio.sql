CREATE PROCEDURE [dbo].[sp_CriaPremio]
(
	@EventoId UNIQUEIDENTIFIER,
	@HistoricoCoberturaId UNIQUEIDENTIFIER,
	@TipoMovimentoId SMALLINT,
	@Numero INT,
	@Competencia DATETIME,
	@InicioVigencia DATETIME,
	@FimVigencia DATETIME,
	@ValorPremio NUMERIC(12, 2),
	@ValorCarregamento NUMERIC(12, 2),
	@ValorBeneficio NUMERIC(12, 2),
	@ValorCapitalSegurado NUMERIC(12, 2),
	@DataPagamento DATE = NULL,
	@DataApropriacao DATE = NULL,
	@ValorPago NUMERIC(12, 2) = NULL,
	@Desconto NUMERIC(12, 2) = NULL,
	@Multa NUMERIC(12, 2) = NULL,
	@IOFRetido NUMERIC(12, 2) = NULL,
	@IOFARecolher NUMERIC(12, 2) = NULL,
	@IdentificadorCredito NVARCHAR(50) = NULL,
	@CodigoSusep NVARCHAR(100) = NULL
)
AS
BEGIN
	DECLARE @id UNIQUEIDENTIFIER = NEWID();	
	DECLARE @Sequencial INT = 1;
	
	SELECT Top 1 @Sequencial = CASE WHEN Sequencial = NULL THEN @Sequencial ELSE Sequencial + 1 END
	  FROM Premio P
	 INNER JOIN HistoricoCoberturaContratada H ON H.Id = P.HistoricoCoberturaId	 
	 WHERE H.Id = @HistoricoCoberturaId
	   AND P.Numero = @Numero
	 ORDER BY Sequencial DESC

	INSERT INTO Premio (Id, EventoId, HistoricoCoberturaId, TipoMovimentoId, Numero, Competencia, InicioVigencia, FimVigencia, 
			ValorPremio, ValorCarregamento, ValorBeneficio, ValorCapitalSegurado, Sequencial,
			DataPagamento, DataApropriacao, ValorPago, Desconto, Multa, IOFRetido, IOFARecolher, IdentificadorCredito, CodigoSusep)
		VALUES (@id, @EventoId, @HistoricoCoberturaId, @TipoMovimentoId, @Numero, @Competencia, @InicioVigencia, @FimVigencia, 
			@ValorPremio, @ValorCarregamento, @ValorBeneficio, @ValorCapitalSegurado, @Sequencial,
			@DataPagamento, @DataApropriacao, @ValorPago, @Desconto, @Multa, @IOFRetido, @IOFARecolher, @IdentificadorCredito, @CodigoSusep)

	SELECT @id;

	RETURN 0
END

