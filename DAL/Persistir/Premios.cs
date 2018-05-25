using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Infrastructure.DataAccess;
using System.Data;
using Mongeral.Provisao.V2.Domain.Enum;
using System.Collections.Generic;

namespace Mongeral.Provisao.V2.DAL.Persistir
{
    public class Premios : PersistenceBase, IPremios
    {
        public Premios(DataAccessSessionManager session) : base(session)
        { }

        public async Task<bool> Contem(long itemCertificadoApoliceId)
        {
            var query = await CreateCommand.QueryOneAsync<Guid>
                (@"SELECT Top 1 P.Id
                     FROM Premio P
                    INNER JOIN HistoricoCoberturaContratada H ON H.Id = P.HistoricoCoberturaId
                    INNER JOIN CoberturaContratada C ON C.Id = H.CoberturaContratadaId
                    WHERE C.ItemCertificadoApoliceId = @Id", new { Id = itemCertificadoApoliceId});

            return query != default(Guid);
            
        }

        public async Task<Premio> ObterPremioAnterior(long itemCertificadoApoliceId, int numeroParcela)
        {
            var query = @"SELECT Top 1 P.*
                            FROM Premio P
                           INNER JOIN HistoricoCoberturaContratada H ON H.Id = P.HistoricoCoberturaId
                           INNER JOIN CoberturaContratada C ON C.Id = H.CoberturaContratadaId
                           WHERE C.ItemCertificadoApoliceId = @Id                             
                             AND P.Numero = @Numero
                           ORDER BY P.Sequencial DESC";

            return await CreateCommand.QueryOneAsync<Premio>(query, new { Id = itemCertificadoApoliceId, Numero = numeroParcela });
        }

        public async Task<IEnumerable<Premio>> ObterPremiosPorCertificado(long itemCertificadoApoliceId, TipoMovimentoEnum tipoMovimento)
        {
            var query = @"SELECT P.*
                            FROM Premio P
                           INNER JOIN HistoricoCoberturaContratada H ON H.Id = P.HistoricoCoberturaId
                           INNER JOIN CoberturaContratada C ON C.Id = H.CoberturaContratadaId
                           WHERE C.ItemCertificadoApoliceId = @Id                             
                             AND P.TipoMovimentoId = @Tipo";

            return await CreateCommand.QueryAsync<Premio>(query, new { Id = itemCertificadoApoliceId, Tipo = (int)tipoMovimento });
        }

        public async Task<T> ObterPorItemCertificado<T>(long itemCertificadoApoliceId, short tipoMovimentoId, int numeroParcela) where T: class
        {
            return await CreateCommand.QueryOneAsync<T>
                (@"SELECT P.Id 
                         ,P.EventoId
                         ,P.HistoricoCoberturaId
                         ,P.TipoMovimentoId
                         ,P.Numero
                         ,P.Competencia
                         ,P.InicioVigencia
                         ,P.FimVigencia
                         ,P.ValorPremio
                         ,P.ValorCarregamento
                         ,P.ValorBeneficio
                         ,P.ValorCapitalSegurado
                         ,P.DataPagamento
                         ,P.DataApropriacao
                         ,P.ValorPago
                         ,P.Desconto
                         ,P.Multa
                         ,P.IOFRetido
                         ,P.IOFARecolher
                         ,P.IdentificadorCredito
                         ,C.ItemCertificadoApoliceId
                         ,P.CodigoSusep
                     FROM Premio P
                    INNER JOIN HistoricoCoberturaContratada H ON H.Id = P.HistoricoCoberturaId
                    INNER JOIN CoberturaContratada C ON C.Id = H.CoberturaContratadaId
                    WHERE C.ItemCertificadoApoliceId = @Id
                      AND P.TipoMovimentoId = @TipoId
                      AND P.Numero = @Numero"
                , new
                {
                    Id = itemCertificadoApoliceId,
                    TipoId = tipoMovimentoId,
                    Numero = numeroParcela
                });
        }        

        public async Task Salvar(Premio dto)
        {
            const string proc = "sp_CriaPremio";

            var objeto = CarregarParametros(dto);

            dto.Id = await CreateCommand.ExecuteScalarAsync<Guid>(proc, objeto, commandType: CommandType.StoredProcedure);
        }

        public object CarregarParametros(Premio dto)
        {
            return new
            {
                dto.EventoId,
                dto.HistoricoCoberturaId,
                dto.TipoMovimentoId,
                dto.Numero,
                dto.Competencia,
                dto.InicioVigencia,
                dto.FimVigencia,
                dto.ValorPremio,
                dto.ValorCarregamento,
                dto.ValorBeneficio,
                dto.ValorCapitalSegurado,
                dto.DataPagamento,
                dto.DataApropriacao,
                dto.ValorPago,
                dto.Desconto,
                dto.Multa,
                dto.IOFRetido,
                dto.IOFARecolher,
                dto.IdentificadorCredito,
                dto.CodigoSusep
            };
        }

        public async Task Remover(Guid identificador)
        {
            var sql = @"DELETE P
                          FROM Evento E
                         INNER JOIN Premio P ON P.EventoId = E.Id
                         WHERE E.Identificador = @Identificador";

            await CreateCommand.ExecuteAsync(sql, new { Identificador = identificador });
        }
    }
}
