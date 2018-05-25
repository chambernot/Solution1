using System.Threading.Tasks;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaDesapropriacaoPremioService : OrchestrationSubscribeService<IParcelaDesapropriada>
    {
        private readonly IPipe<DesapropriacaoPremioContext> _pipeDesapropriacao;
        private readonly IPipe<CompensacaoContext<EventoDesapropriacaoPremio>> _pipeCompensacao;

        public ProcessaDesapropriacaoPremioService(
            IPipe<DesapropriacaoPremioContext> pipeDesapropriacao,
            IPipe<CompensacaoContext<EventoDesapropriacaoPremio>> pipeCompensacao)
        {
            _pipeDesapropriacao = pipeDesapropriacao;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IParcelaDesapropriada> Execute(IParcelaDesapropriada message)
        {
            var context = new DesapropriacaoPremioContext(message);

            await _pipeDesapropriacao.Send(context);

            return message;
        }

        public async Task Compensate(IParcelaDesapropriada contrato)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoDesapropriacaoPremio>(contrato));
        }
    }
}
