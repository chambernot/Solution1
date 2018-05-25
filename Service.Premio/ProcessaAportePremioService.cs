using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaAportePremioService: OrchestrationSubscribeService<IAporteApropriado>
    {
        private readonly IPipe<AportePremioContext> _pipeAporte;
        private readonly IPipe<CompensacaoContext<EventoAportePremio>> _pipeCompensacao;

        public ProcessaAportePremioService(
            IPipe<AportePremioContext> pipeAporte,
            IPipe<CompensacaoContext<EventoAportePremio>> pipeCompensacao)
        {
            _pipeAporte = pipeAporte;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IAporteApropriado> Execute(IAporteApropriado message)
        {
            var context = new AportePremioContext(message);

            await _pipeAporte.Send(context);

            return message;
        }

        public async Task Compensate(IAporteApropriado message)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoAportePremio>(message));
        }
    }
}
