using Mongeral.Infrastructure.DataAccess;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL.Persistir
{
    public class Coberturas : PersistenceBase, ICoberturas
    {
        
        public Coberturas(DataAccessSessionManager session)
            : base(session)
        { }

        public async Task<bool> Contem(long itemCertificadoApoliceId)
        {
            var query = await CreateCommand.QueryOneAsync<Guid>
            (@"SELECT top 1 Id
                        FROM CoberturaContratada
                    WHERE ItemCertificadoApoliceId = @Id", new { Id = itemCertificadoApoliceId });

            return query != default(Guid);
        }

        public async Task<CoberturaContratada> ObterPorItemCertificado(long itemCertificadoApoliceId)
        {
            var query = await CreateCommand.QueryOneAsync<CoberturaContratada>
            (@"SELECT Top 1 *
                     FROM CoberturaContratada
                    WHERE ItemCertificadoApoliceId = @Id", new { Id = itemCertificadoApoliceId });

            if (query != null)
                query.Historico = await ObterHistoricosCobertura(query.Id);
                
                
            return query;
        }

        public async Task<CoberturaContratada> ObterProvisaoPorCobertura(long itemCertificadoApoliceId)
        {
            
            
           var cobertura =  await ObterPorItemCertificado(itemCertificadoApoliceId);

           
           const string query = @" SELECT 
										* 
									FROM [dbo].[fn_ObterProvisaoPorCobertura] 
									(
										@CoberturaContratadaId
									)";


           var provisoes = (await CreateCommand.QueryAsync<ProvisaoCobertura>(query, new
            {
                CoberturaContratadaId = cobertura.Id,

            }));

            cobertura.AdicionarProvisao(provisoes);
            
            return cobertura;
        }

        public async Task<HistoricoCoberturaContratada> ObterHistoricosCobertura(Guid Id)
        {
            return await CreateCommand.QueryOneAsync<HistoricoCoberturaContratada>
                (@"SELECT TOP 1 H.Id,
	                    H.EventoId,
	                    H.CoberturaContratadaId,
	                    H.StatusCoberturaId AS StatusId,
	                    H.DataNascimentoBeneficiario,
	                    H.SexoBeneficiario,
	                    H.PeriodicidadeId,
	                    H.Sequencia,
                        H.ValorBeneficio,
                        H.ValorCapital,
                        H.ValorContribuicao
                     FROM HistoricoCoberturaContratada H
                    WHERE CoberturaContratadaId = @Id
                    ORDER BY Sequencia DESC", new { Id = Id });
        }

        public async Task<IEnumerable<CoberturaContratada>> ObterPorItensCertificadosApolices(IEnumerable<long> itensCertificadosApoliceIds)
        {
            var query = await CreateCommand.QueryAsync<CoberturaContratada>
            (@"SELECT * FROM CoberturaContratada WHERE ItemCertificadoApoliceId in @Id",
                new { Id = itensCertificadosApoliceIds });

            return query;
        }

        public async Task Salvar(CoberturaContratada dto)
        {
            const string proc = "sp_CriarCoberturaContratada";

            var objeto = CarregarParametros(dto);

            dto.Id = await CreateCommand.ExecuteScalarAsync<Guid>(proc, objeto, commandType: CommandType.StoredProcedure);
        }

        public object CarregarParametros(CoberturaContratada dto)
        {
            return new
            {
                dto.EventoId,
                dto.InscricaoId,
                dto.ItemCertificadoApoliceId,
                dto.ItemProdutoId,
                dto.ClasseRiscoId,
                dto.TipoFormaContratacaoId,
                dto.TipoRendaId,
                dto.DataNascimento,
                dto.Sexo,
                CodigoProduto = dto.ProdutoId,
                dto.Matricula,
                dto.DataInicioVigencia,
                dto.DataFimVigencia,
                dto.DataAssinatura,
                dto.IndiceBeneficioId,
                IndiceContribuicaoId = dto.IndiceContribuicaoId.GetValueOrDefault(),
                dto.ModalidadeCoberturaId,
                dto.ProdutoId,
                dto.RegimeFinanceiroId,
                dto.TipoItemProdutoId,
                dto.NomeProduto,
                dto.NumeroBeneficioSusep,
                dto.NumeroProcessoSusep,
                dto.PlanoFipSusep,
                dto.TipoProvisoes,
                dto.PermiteResgateParcial,
                dto.PrazoCoberturaEmAnos,
                dto.PrazoDecrescimoEmAnos,
                dto.PrazoPagamentoEmAnos,
                dto.NumeroContribuicoesInicial
            };
        }

        public async Task Remover(Guid identificador)
        {
            var sql = @"DELETE C
                          FROM Evento E
                         INNER JOIN CoberturaContratada C ON C.EventoId = E.Id
                         WHERE E.Identificador = @Id";

            await CreateCommand.ExecuteAsync(sql, new { Id = identificador });
        }
    }
}
