using Mongeral.Provisao.V2.Domain.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface IPremios
    {
        Task Salvar(Premio premio);
        Task<bool> Contem(long itemCertificadoApoliceId);        
        Task<T> ObterPorItemCertificado<T>(long itemCertificadoApolice, short tipoMovimentoId, int numeroParcela) where T : class;
        Task<Premio> ObterPremioAnterior(long itemCertificadoApoliceId, int numeroParcela);
        Task<IEnumerable<Premio>> ObterPremiosPorCertificado(long itemCertificadoApoliceId, TipoMovimentoEnum tipoMovimento);
    }
}
