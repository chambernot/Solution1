
CREATE PROCEDURE [dbo].[sp_CriarCoberturaContratada]
(	
	@EventoId UNIQUEIDENTIFIER,
	@InscricaoId BIGINT,
	@ItemCertificadoApoliceId BIGINT,
	@ItemProdutoId INTEGER,
	@ClasseRiscoId INTEGER = null,
	@TipoFormaContratacaoId INTEGER = NULL,
	@TipoRendaId INTEGER = NULL,	
	@Sexo NVARCHAR(20),
	@CodigoProduto INTEGER = NULL,
	@Matricula BIGINT,
	@DataNascimento DATE,
	@DataInicioVigencia DATE,	
	@DataFimVigencia DATE,	
	@DataAssinatura DATE,	
	@IndiceBeneficioId INTEGER = NULL,
	@IndiceContribuicaoId INTEGER = NULL,
	@ModalidadeCoberturaId INTEGER,
	@ProdutoId INTEGER,
	@RegimeFinanceiroId INTEGER = NULL,
	@TipoItemProdutoId INTEGER,
	@NomeProduto NVARCHAR(200),
	@NumeroBeneficioSusep INTEGER,
	@NumeroProcessoSusep NVARCHAR(100),
	@PlanoFipSusep INTEGER,
	@TipoProvisoes INTEGER,
	@PermiteResgateParcial BIT,
	@PrazoCoberturaEmAnos INT = NULL,
	@PrazoDecrescimoEmAnos INT = NULL,
	@PrazoPagamentoEmAnos INT = NULL,
	@NumeroContribuicoesInicial INT
)
AS
BEGIN
	DECLARE @Id UNIQUEIDENTIFIER = NEWID();

	INSERT INTO CoberturaContratada (Id, EventoId, InscricaoId, ItemCertificadoApoliceId, ItemProdutoId, ClasseRiscoId, 
			TipoFormaContratacaoId, TipoRendaId, DataNascimento, Sexo, CodigoProduto, Matricula, DataInicioVigencia, DataFimVigencia, DataAssinatura,
			IndiceBeneficioId, IndiceContribuicaoId, ModalidadeCoberturaId, ProdutoId, RegimeFinanceiroId, TipoItemProdutoId, NomeProduto, 
			NumeroBeneficioSusep, NumeroProcessoSusep, PlanoFipSusep, TipoProvisoes, PermiteResgateParcial, PrazoCoberturaEmAnos, PrazoDecrescimoEmAnos, PrazoPagamentoEmAnos, NumeroContribuicoesInicial)
		VALUES(@Id, @EventoId, @InscricaoId, @ItemCertificadoApoliceId, @ItemProdutoId, @ClasseRiscoId, 
			@TipoFormaContratacaoId, @TipoRendaId, @DataNascimento, @Sexo, @CodigoProduto, @Matricula, @DataInicioVigencia, @DataFimVigencia, @DataAssinatura, 
			@IndiceBeneficioId, @IndiceContribuicaoId, @ModalidadeCoberturaId, @ProdutoId, @RegimeFinanceiroId, @TipoItemProdutoId, @NomeProduto, 
			@NumeroBeneficioSusep, @NumeroProcessoSusep, @PlanoFipSusep, @TipoProvisoes, @PermiteResgateParcial, @PrazoCoberturaEmAnos, @PrazoDecrescimoEmAnos, @PrazoPagamentoEmAnos, @NumeroContribuicoesInicial)

	SELECT @Id;

	RETURN 0;

END
