using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface IProvisoes
    {
        Task<IEnumerable<Guid>> ObterProvisaoCobertura(Guid identificador);
        Task<MovimentoProvisao> Obter(Guid premioId, Guid coberturaId, short tipoProvisaoId, short tipoMovimentoId, int numeroParcela);
        Task<MovimentoProvisao> ObterUltimoMovimento(long itemCertificadoApoliceId, TipoProvisaoEnum tipoProvisao);
       
    }
}
