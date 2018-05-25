using Mongeral.Infrastructure.DataAccess;
using Mongeral.Provisao.V2.DAL;
using Mongeral.Provisao.V2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Infra.Persisters
{
    public class HistoricoCobertura : PersistenceBase
    {
        public HistoricoCobertura(DataAccessSessionManager session) 
            : base(session){ }


        public async Task<HistoricoCoberturaContratada> ObterHistoricoCobertura(long itemCertificadoApoliceId, short tipoevento)
        {
            return await CreateCommand.QueryOneAsync<HistoricoCoberturaContratada>
            (@"SELECT H.*
                    FROM HistoricoCoberturaContratada H INNER JOIN Evento E ON E.Id = H.EventoId
                    INNER JOIN CoberturaContratada C ON C.Id = H.CoberturaContratadaId
                WHERE C.ItemCertificadoApoliceId = @Id AND E.TipoEventoId = @tipoeventoid", new { Id = itemCertificadoApoliceId, tipoeventoid = tipoevento }

             );
        }
    }
}
