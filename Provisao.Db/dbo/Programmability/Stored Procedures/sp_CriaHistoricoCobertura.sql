
CREATE PROCEDURE [dbo].[sp_CriaHistoricoCobertura]
(
	@EventoId UNIQUEIDENTIFIER, 
	@CoberturaContratadaId UNIQUEIDENTIFIER, 
	@PeriodicidadeId INT = NULL, 
	@DataNascimentoBeneficiario DATE = NULL, 
	@SexoBeneficiario NVARCHAR(20) = NULL,
	@ValorBeneficio NUMERIC(12,2) = NULL,
	@ValorCapital NUMERIC(12,2) = NULL,
	@ValorContribuicao NUMERIC(12,2) = NULL,
	@StatusCoberturaId SMALLINT
)
AS
BEGIN
	DECLARE @id UNIQUEIDENTIFIER = NEWID();
	DECLARE @Sequencia INT = 1;
	
	SELECT TOP 1 
		   @PeriodicidadeId = ISNULL(@PeriodicidadeId, PeriodicidadeId),
		   @DataNascimentoBeneficiario = ISNULL(@DataNascimentoBeneficiario , DataNascimentoBeneficiario),
		   @SexoBeneficiario = ISNULL(@SexoBeneficiario , SexoBeneficiario),
		   @ValorBeneficio = ISNULL(@ValorBeneficio, ValorBeneficio),
		   @ValorCapital = ISNULL(@ValorCapital, ValorCapital),
		   @ValorContribuicao = ISNULL(@ValorContribuicao, ValorContribuicao),
		   @Sequencia = CASE WHEN Sequencia = NULL THEN @Sequencia ELSE Sequencia + 1 END		   
	  FROM HistoricoCoberturaContratada 
  	 WHERE CoberturaContratadaId = @CoberturaContratadaId 
	 ORDER BY Sequencia DESC	 

	INSERT INTO HistoricoCoberturaContratada
            (Id, EventoId, CoberturaContratadaId, PeriodicidadeId, DataNascimentoBeneficiario, SexoBeneficiario, Sequencia, ValorBeneficio, ValorCapital, ValorContribuicao, StatusCoberturaId) 
        VALUES (@id, @EventoId, @CoberturaContratadaId, @PeriodicidadeId, @DataNascimentoBeneficiario, @SexoBeneficiario, @Sequencia, @ValorBeneficio, @ValorCapital, @ValorContribuicao, @StatusCoberturaId)

	SELECT @id;

	RETURN 0;
END