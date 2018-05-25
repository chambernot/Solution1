using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Services
{
    public class ProvisoesService: IProvisoesService
    {
        private readonly IProvisoes _provisao;        

        public ProvisoesService(IProvisoes provisao)
        {
            _provisao = provisao;
        }        

        public async Task<MovimentoProvisao> ObterUltimoMovimentoProvisao(Premio premio, TipoProvisaoEnum tipoProvisao)
        {
            return await _provisao.ObterUltimoMovimento(premio.Cobertura.ItemCertificadoApoliceId, tipoProvisao);
        }
    }
}
