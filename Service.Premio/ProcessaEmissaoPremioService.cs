using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Premio;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Infrastructure.ServiceBus.Subscribe;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaEmissaoPremioService: OrchestrationSubscribeService<IParcelaEmitida>
    {
        private readonly IPipe<EmissaoPremioContext> _pipeEmissaoPremio;
        private readonly IPipe<CompensacaoContext<EventoEmissaoPremio>> _pipeCompensacao;
                
        public ProcessaEmissaoPremioService(
            IPipe<EmissaoPremioContext> pipeEmissaoPremio,
            IPipe<CompensacaoContext<EventoEmissaoPremio>> pipeCompensacao)
        {
            _pipeEmissaoPremio = pipeEmissaoPremio;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IParcelaEmitida> Execute(IParcelaEmitida message)
        {
            var retorno = new EmissaoPremioContext(message);

            await _pipeEmissaoPremio.Send(retorno);

            return message;
        }

        public async Task Compensate(IParcelaEmitida message)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoEmissaoPremio>(message));
        }
    }
}
