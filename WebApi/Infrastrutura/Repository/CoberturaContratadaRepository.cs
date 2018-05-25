using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Dapper;

namespace Infrastrutura.Repository
{
    public class CoberturaContratadaRepository : RepositoryBase, ICoberturaContratadaRepository
    {
        public async Task<CoberturaContratada> Obter(int id)
        {
            using (var db = ProvisaoConnection)
            {
                return await db.QuerySingleAsync<CoberturaContratada>("SELECT * FROM CoberturaContratada WHERE EventoImplantadoId = @Id", new { Id = id });
            }
        }        

        //TODO alterar para filtrar por matricula
        public async Task<IEnumerable<CoberturaContratada>> ObterPorMatricula(long matricula)
        {
            using (var db = ProvisaoConnection)
            {
                //return await db.QueryAsync<CoberturaContratada>("SELECT * FROM CoberturaContratada  WHERE Maatricula = @matricula new { matricula = matricula });
                return await db.QueryAsync<CoberturaContratada>("SELECT * FROM CoberturaContratada");
            }
        }

        public async Task<IEnumerable<CoberturaContratada>> ObterTodos()
        {
            using (var db = ProvisaoConnection)
            {
                return await db.QueryAsync<CoberturaContratada>("SELECT * FROM CoberturaContratada");
            }
        }
    }
}
