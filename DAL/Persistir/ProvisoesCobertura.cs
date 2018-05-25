using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mongeral.Infrastructure.DataAccess;
using System.Data;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.DAL.Persistir
{
    public class ProvisoesCobertura : PersistenceBase
    {
        public ProvisoesCobertura(DataAccessSessionManager session) : base(session)
        { }

        public async Task Adicionar(ProvisaoCobertura dto)
        {
            const string proc = "sp_CriaProvisaoCobertura";

            var objeto = new
            {
                CoberturaContratadaId = dto.CoberturaContrataId,
                TipoProvisaoId = dto.TipoProvisaoId,
            };

            dto.Id = await CreateCommand.ExecuteScalarAsync<Guid>(proc, objeto, commandType: CommandType.StoredProcedure);
        }
       
        public async Task Remover(IEnumerable<Guid> listaIds)
        {
            var sql = @"DELETE FROM ProvisaoCobertura 
                         WHERE Id IN @Ids
                           AND Id NOT IN (SELECT ProvisaoCoberturaId FROM MovimentoProvisaoPremio WHERE ProvisaoCoberturaId in @Ids)";

            await CreateCommand.ExecuteAsync(sql, new { Ids = listaIds });
        }
    }
}
