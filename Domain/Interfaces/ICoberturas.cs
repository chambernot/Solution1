using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface ICoberturas
    {
        Task<bool> Contem(long itemCertificadoApoliceId);
        Task<CoberturaContratada> ObterPorItemCertificado(long itemCertificadoApolice);
        Task<IEnumerable<CoberturaContratada>> ObterPorItensCertificadosApolices(IEnumerable<long> itensCertificadosApoliceIds);
        Task<CoberturaContratada> ObterProvisaoPorCobertura(long itemCertificadoApoliceId);
    }
}