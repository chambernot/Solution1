CREATE PROCEDURE [dbo].[sp_CriaProvisaoCobertura]
(
	@CoberturaContratadaId UNIQUEIDENTIFIER,
	@TipoProvisaoId SMALLINT
)
AS
BEGIN
	DECLARE @id UNIQUEIDENTIFIER = NEWID();

	IF NOT  EXISTS (SELECT 1 FROM ProvisaoCobertura PC WHERE PC.TipoProvisaoId = @TipoProvisaoId AND PC.CoberturaContratadaId = @CoberturaContratadaId)
	BEGIN
		INSERT INTO ProvisaoCobertura (Id, CoberturaContratadaId, TipoProvisaoId)
			VALUES (@id, @CoberturaContratadaId, @TipoProvisaoId);

		SELECT @id;
	END
	ELSE
	BEGIN
		SELECT PC.Id FROM ProvisaoCobertura PC WHERE PC.TipoProvisaoId = @TipoProvisaoId AND PC.CoberturaContratadaId = @CoberturaContratadaId
	END

	RETURN 0;
END