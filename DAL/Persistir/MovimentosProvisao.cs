using System;
using System.Threading.Tasks;
using Mongeral.Infrastructure.DataAccess;
using Mongeral.Provisao.V2.Domain;
using System.Data;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.DAL.Persistir
{
    public class MovimentosProvisao : PersistenceBase, IProvisoes
    {
        public MovimentosProvisao(DataAccessSessionManager session) : base(session)
        { }

        public async Task<MovimentoProvisao> Obter(Guid premioId, Guid coberturaId, short tipoProvisaoId, short tipoMovimentoId, int numeroParcela)
        {
            return await CreateCommand.QueryOneAsync<MovimentoProvisao>
                (@"SELECT M.Id,
                          M.ProvisaoCoberturaId,
                          M.EventoId,
                          M.PremioId,
                          M.DataMovimento AS DataMovimentacao,
                          M.QuantidadeContribuicoes,
                          M.Fator,
                          M.PercentualCarregamento,
                          M.ValorBeneficioCorrigido,
                          M.ValorJuros,
                          M.ValorAtualizacao,
                          M.ValorSobrevivencia,
                          M.ValorProvisao,
                          M.Desvio,
                          M.ValorFIF
                     FROM MovimentoProvisaoPremio M
                    INNER JOIN ProvisaoCobertura P ON P.Id = M.ProvisaoCoberturaId
                    INNER JOIN Premio PR ON PR.Id = M.PremioId
                    WHERE P.CoberturaContratadaId = @CoberturaId
                      AND P.TipoProvisaoId = @TipoProvisaoId
                      AND M.PremioId = @PremioId
                      AND PR.TipoMovimentoId = @TipoMovimento
                      AND PR.Numero = @Numero",
                 new
                 {
                     @PremioId = premioId,
                     @CoberturaId = coberturaId,
                     @TipoProvisaoId = tipoProvisaoId,
                     @TipoMovimento = tipoMovimentoId,
                     @Numero = numeroParcela
                 });
        }

        public async Task<MovimentoProvisao> ObterUltimoMovimento(long itemCertificadoApoliceId, TipoProvisaoEnum tipoProvisao)
        {
            var query = @"SELECT Top 1 M.*
                            FROM MovimentoProvisaoPremio M
                           INNER JOIN Premio P ON P.Id = M.PremioId
                           INNER JOIN ProvisaoCobertura PC ON PC.Id = M.ProvisaoCoberturaId AND PC.TipoProvisaoId = @Tipo
                           INNER JOIN CoberturaContratada C ON C.Id = PC.CoberturaContratadaId
                           WHERE C.ItemCertificadoApoliceId = @Id
                           ORDER BY M.Sequencial DESC";

            return await CreateCommand.QueryOneAsync<MovimentoProvisao>(query, new { Id = itemCertificadoApoliceId, Tipo = (int)tipoProvisao });

        }

        public async Task Salvar(MovimentoProvisao dto)
        {
            const string proc = "sp_CriaMovimentoProvisao";

            var objeto = CarregarParametros(dto);

            dto.Id = await CreateCommand.ExecuteScalarAsync<Guid>(proc, objeto, commandType: CommandType.StoredProcedure);
        }

        private object CarregarParametros(MovimentoProvisao dto)
        {
            return new
            {
                ProvisaoCoberturaId = dto.ProvisaoCoberturaId,
                EventoId = dto.EventoId,
                PremioId = dto.PremioId,                
                DataMovimento = dto.DataMovimentacao,                
                ValorJuros = dto.ValorJuros,
                ValorAtualizacao = dto.ValorAtualizacao,
                ValorSobrevivencia = dto.ValorSobrevivencia,
                ValorProvisao = dto.ValorProvisao,
                QuantidadeContribuicoes = dto.QuantidadeContribuicoes, //Não existe no Calculo
                Fator = dto.Fator,
                PercentualCarregamento = dto.PercentualCarregamento, //dto possui apenas o ValorCarregamento
                ValorBeneficioCorrigido = dto.ValorBeneficioCorrigido,
                Desvio = dto.ValorDesvio,
                ValorFIF = dto.ValorFIF //não possui esse valor
            };
        }

        public async Task<IEnumerable<Guid>> ObterProvisaoCobertura(Guid identificador)
        {
            var sql = @"SELECT DISTINCT M.ProvisaoCoberturaId
                          FROM Evento E
                         INNER JOIN MovimentoProvisaoPremio M ON M.EventoId = E.Id
                         WHERE E.Identificador = @Identificador";

            return await CreateCommand.QueryAsync<Guid>(sql, new { Identificador = identificador });
        }

        public async Task Remover(Guid identificador)
        {          
            var sql = @"DELETE M
                          FROM Evento E
                         INNER JOIN MovimentoProvisaoPremio M ON M.EventoId = E.Id
                         WHERE E.Identificador = @Identificador";

            await CreateCommand.ExecuteAsync(sql, new { Identificador = identificador });
        }

        
    }
}
