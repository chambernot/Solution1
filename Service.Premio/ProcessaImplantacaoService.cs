using System.Threading.Tasks;
using Mongeral.Integrador.Contratos;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaImplantacaoService : OrchestrationSubscribeService<IProposta>
    {
        private readonly IPipe<ImplantacaoContext> _pipelineImplantacao;
        private readonly IPipe<CompensacaoContext<EventoImplantacao>> _pipeCompensacao;

        public ProcessaImplantacaoService(IPipe<ImplantacaoContext> pipeImplantacao, IPipe<CompensacaoContext<EventoImplantacao>> pipeCompensacao)
        {
            _pipelineImplantacao = pipeImplantacao;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IProposta> Execute(IProposta contrato)
        {
            await _pipelineImplantacao.Send(new ImplantacaoContext(contrato));
            return contrato;
        }
        
        public async Task Compensate(IProposta contrato)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoImplantacao>(contrato));
        }
        
    }
}