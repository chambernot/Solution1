using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Dapper;

namespace Infrastrutura.Repository
{
    public class HistoricoCoberturaRepository : RepositoryBase, IHistoricoCoberturaRepository
    {
        public async Task<HistoricoCobertura> Obter(int id)
        {
            using (var db = ProvisaoConnection)
            {
                return await db.QuerySingleAsync<HistoricoCobertura>("SELECT * FROM HistoricoCobertura WHERE EventoImplantadoId = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<HistoricoCobertura>> ObterTodos()
        {
            using (var db = ProvisaoConnection)
            {
                return await db.QueryAsync<HistoricoCobertura>("SELECT * FROM HistoricoCobertura");
            }
        }
    }
}
