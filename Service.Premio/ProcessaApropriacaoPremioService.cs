using GreenPipes;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaApropriacaoPremioService : OrchestrationSubscribeService<IParcelaApropriada>
    {
        private readonly IPipe<ApropriacaoPremioContext> _pipeApropriacao;
        private readonly IPipe<CompensacaoContext<EventoApropriacaoPremio>> _pipeCompensacao;

        public ProcessaApropriacaoPremioService(
            IPipe<ApropriacaoPremioContext> pipeApropriacao,
            IPipe<CompensacaoContext<EventoApropriacaoPremio>> pipeCompensacao)
        {
            _pipeApropriacao = pipeApropriacao;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IParcelaApropriada> Execute(IParcelaApropriada message)
        {
            var context = new ApropriacaoPremioContext(message);

            await _pipeApropriacao.Send(context);

            return message;
        }

        public async Task Compensate(IParcelaApropriada message)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoApropriacaoPremio>(message));
        }
    }
}
