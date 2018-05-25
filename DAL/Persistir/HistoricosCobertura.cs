using Mongeral.Infrastructure.DataAccess;
using Mongeral.Provisao.V2.Domain;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL.Persistir
{
    public class HistoricosCobertura: PersistenceBase
    {
        public HistoricosCobertura(DataAccessSessionManager session)
           : base(session) { }

        public async Task Salvar(HistoricoCoberturaContratada dto)
        {
            const string proc = "sp_CriaHistoricoCobertura";

            var objeto = CarregarParametros(dto);

            dto.Id = await CreateCommand.ExecuteScalarAsync<Guid>(proc, objeto, commandType: CommandType.StoredProcedure);            
        }
        public object CarregarParametros(HistoricoCoberturaContratada dto)
        {
            return new
            {                
                dto.EventoId,
                dto.CoberturaContratadaId,
                dto.PeriodicidadeId,
                DataNascimentoBeneficiario = dto.DataNascimentoBeneficiario != default(DateTime) ? dto.DataNascimentoBeneficiario : default(DateTime?),
                dto.SexoBeneficiario,
                dto.ValorBeneficio,
                dto.ValorCapital,
                dto.ValorContribuicao,
                StatusCoberturaId = (short)dto.Status.Staus
            };
        }

        public async Task Remover(Guid identificador)
        {
            var sql = @"DELETE H
                          FROM Evento E,
                               CoberturaContratada C,
                               HistoricoCoberturaContratada H
                         WHERE E.Identificador = @Id
                           AND ((H.CoberturaContratadaId = C.Id AND H.EventoId IS NULL AND C.EventoId = E.Id)
	                           OR (H.EventoId IS NOT NULL AND H.EventoId = E.Id))";

            await CreateCommand.ExecuteAsync(sql, new { Id = identificador });
        }        
    }
}
