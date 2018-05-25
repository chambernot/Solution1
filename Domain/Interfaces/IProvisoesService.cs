using Mongeral.Provisao.V2.Domain.Enum;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface IProvisoesService
    {
        Task<MovimentoProvisao> ObterUltimoMovimentoProvisao(Premio premio, TipoProvisaoEnum tipoProvisao);
    }
}
