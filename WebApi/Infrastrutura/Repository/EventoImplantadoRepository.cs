using Dapper;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;
using System.Text;
using Domain.Enum;

namespace Infrastrutura.Repository
{
    public class EventoImplantadoRepository : RepositoryBase, IEventoImplantadoRepository
    {
        public async Task<EventoImplantado> Obter(int id)
        {
            using (var db = ProvisaoConnection)
            {
                return await db.QuerySingleAsync<EventoImplantado>("SELECT * FROM EventoImplantado WHERE EventoImplantadoId = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<EventoImplantado>> ObterTodos()
        {
            using (var db = ProvisaoConnection)
            {
                return await db.QueryAsync<EventoImplantado>("SELECT * FROM EventoImplantado");
            }
        }

        public async Task<EventoImplantado> Obter(long id, FiltroEventoEnum filtro)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT E.*, C.*, H.* ");
            query.Append("  FROM EventoImplantado E");
            query.Append(" INNER JOIN CoberturaContratada C ON E.EventoImplantadoId = C.EventoImplantadoId");
            query.Append("  LEFT JOIN HistoricoCobertura H ON E.EventoImplantadoId = H.EventoImplantadoId");

            switch (filtro)
            {
                case FiltroEventoEnum.PorEventoImplantadoId:
                    query.Append(" WHERE E.EventoImplantadoId = @Id");
                    break;
                case FiltroEventoEnum.PorInscricaoId:
                    query.Append(" WHERE C.InscricaoId = @Id");
                    break;
                case FiltroEventoEnum.PorItemCertificadoApoliceId:
                    query.Append(" WHERE C.ItemCertificadoApoliceId = @Id");
                    break;
            }
            query.Append(" ORDER BY H.DataCriacao DESC, E.EventoImplantadoId");

            using (var db = ProvisaoConnection)
            {
                var result = await db.QueryAsync<EventoImplantado, CoberturaContratada, HistoricoCobertura, EventoImplantado>(                
                    query.ToString(),
                    (evento, cobertura, historico) =>
                    {
                        evento.cobertura = cobertura;
                        evento.historico = historico;
                        return evento;
                    }, new {
                        Id = id
                        }, 
                    commandType: System.Data.CommandType.Text, splitOn: "EventoImplantadoId");

                return result.SingleOrDefault();
            }            
        }
    }
}
