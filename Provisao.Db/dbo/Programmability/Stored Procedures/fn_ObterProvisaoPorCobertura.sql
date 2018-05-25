CREATE FUNCTION [dbo].[fn_ObterProvisaoPorCobertura]
(
	@CoberturaContratadaId uniqueidentifier
)
RETURNS TABLE
AS RETURN



	SELECT P.CoberturaContratadaId,P.Id,P.TipoProvisaoId
                            FROM ProvisaoCobertura P
                             WHERE P.CoberturaContratadaId = @CoberturaContratadaId 