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
    public class ProvisaoCobertura : PersistenceBase
    {
        public ProvisaoCobertura(DataAccessSessionManager session) 
            : base(session){ }


        public async Task<int> ListarProvisoesCobertura(long itemCertificadoApoliceId,short tipoevento)
        {
            return await CreateCommand.ExecuteScalarAsync<int>
            (@"SELECT Count(*)
                    FROM MovimentoProvisaoPremio M INNER JOIN Evento E ON E.Id = M.EventoId
                    INNER JOIN ProvisaoCobertura P ON P.Id   = M.ProvisaoCoberturaId
                    INNER JOIN CoberturaContratada C ON C.Id = P.CoberturaContratadaId
                WHERE C.ItemCertificadoApoliceId = @Id AND TipoEventoId = @tipoeventoid", new { Id = itemCertificadoApoliceId, tipoeventoid= tipoevento }
                
             );
        }


        public async Task<MovimentoProvisao> ObterMovimentoProvisao(long itemCertificadoApoliceId, short tipoevento)
        {
            return await CreateCommand.QueryOneAsync<MovimentoProvisao>
            (@"SELECT M.*
                    FROM MovimentoProvisaoPremio M INNER JOIN Evento E ON E.Id = M.EventoId
                    INNER JOIN ProvisaoCobertura P ON P.Id   = M.ProvisaoCoberturaId
                    INNER JOIN CoberturaContratada C ON C.Id = P.CoberturaContratadaId
                WHERE C.ItemCertificadoApoliceId = @Id AND TipoEventoId = @tipoeventoid", new { Id = itemCertificadoApoliceId, tipoeventoid = tipoevento }

             );
        }
    }
}
