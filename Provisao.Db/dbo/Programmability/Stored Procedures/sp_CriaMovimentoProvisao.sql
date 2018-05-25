CREATE PROCEDURE [dbo].[sp_CriaMovimentoProvisao]
(
	@ProvisaoCoberturaId UNIQUEIDENTIFIER,
	@EventoId UNIQUEIDENTIFIER,
	@PremioId UNIQUEIDENTIFIER,	
	@DataMovimento DATE,
	@QuantidadeContribuicoes INT,
	@Fator NUMERIC(20,10) = NULL,
	@PercentualCarregamento NUMERIC(12, 10) = NULL,
	@ValorBeneficioCorrigido NUMERIC(12, 2) = NULL,
	@ValorJuros NUMERIC(12, 2),
	@ValorAtualizacao NUMERIC(12, 2),
	@ValorSobrevivencia NUMERIC(12, 2),
	@ValorProvisao NUMERIC(12, 2),
	@Desvio NUMERIC(12, 2),
	@ValorFIF NUMERIC(10, 8) = NULL
)
AS
BEGIN
	DECLARE @id UNIQUEIDENTIFIER = NEWID();
	DECLARE @Sequencial INT = 1;

	SELECT TOP 1 @Sequencial = CASE WHEN Sequencial = NULL THEN @Sequencial ELSE Sequencial + 1 END
	  FROM MovimentoProvisaoPremio
	 WHERE ProvisaoCoberturaId = @ProvisaoCoberturaId	  
	 ORDER BY Sequencial DESC;

	INSERT INTO MovimentoProvisaoPremio 
			(Id, ProvisaoCoberturaId, EventoId, PremioId, Sequencial, DataMovimento,
			 QuantidadeContribuicoes, Fator, PercentualCarregamento, ValorBeneficioCorrigido, ValorJuros,
			 ValorAtualizacao, ValorSobrevivencia, ValorProvisao, Desvio, ValorFIF)
		VALUES
			(@id, @ProvisaoCoberturaId, @EventoId, @PremioId, @Sequencial, @DataMovimento,
			 @QuantidadeContribuicoes, @Fator, @PercentualCarregamento, @ValorBeneficioCorrigido, @ValorJuros,
			 @ValorAtualizacao, @ValorSobrevivencia, @ValorProvisao, @Desvio, @ValorFIF)
				

	SELECT @id;

	RETURN 0;
END

