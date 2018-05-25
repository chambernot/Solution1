using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaPortabilidadePremioService: OrchestrationSubscribeService<IPortabilidadeApropriada>
    {
        private readonly IPipe<PortabilidadePremioContext> _pipePortabilidade;
        private readonly IPipe<CompensacaoContext<EventoPortabilidadePremio>> _pipeCompensacao;

        public ProcessaPortabilidadePremioService(
            IPipe<PortabilidadePremioContext> pipePortabilidade,
            IPipe<CompensacaoContext<EventoPortabilidadePremio>> pipeCompensacao)
        {
            _pipePortabilidade = pipePortabilidade;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IPortabilidadeApropriada> Execute(IPortabilidadeApropriada message)
        {
            var context = new PortabilidadePremioContext(message);

            await _pipePortabilidade.Send(context);

            return message;
        }

        public async Task Compensate(IPortabilidadeApropriada message)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoPortabilidadePremio>(message));
        }
    }
}
