using System.Threading.Tasks;
using Mongeral.Infrastructure.DataAccess;
using Mongeral.Provisao.V2.DAL;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Testes.Infra.Persisters
{
    public class CoberturaContratadaHelper: PersistenceBase
    {
        public CoberturaContratadaHelper(DataAccessSessionManager session) 
            : base(session){ }

        public async Task<int> ListarCoberturas(long itemCertificadoApoliceId)
        {
            return await CreateCommand.ExecuteScalarAsync<int>
            (@"SELECT Count(*)
                    FROM CoberturaContratada
                WHERE ItemCertificadoApoliceId = @Id", new { Id = itemCertificadoApoliceId });
        }

        public async Task AtualizarCoberturaRegimeFinanceiro(long itemCertificadoApolice, TipoRegimeFinanceiroEnum regimeFinanceiro)
        {
            var sql = @"UPDATE CoberturaContratada
                           SET RegimeFinanceiroId = @RegimeFinanceiroId                          
                         WHERE ItemCertificadoApoliceId = @ItemCertificadoApoliceId";

            await CreateCommand.ExecuteAsync(sql, new
            {
                RegimeFinanceiroId = (int)regimeFinanceiro,  
                ItemCertificadoApoliceId = itemCertificadoApolice
            });
        }
    }
}
