using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaAjustePremioService : OrchestrationSubscribeService<IParcelaAjustada>
    {
        private readonly IPipe<AjustePremioContext> _pipeAjustePremio;
        private readonly IPipe<CompensacaoContext<EventoAjustePremio>> _pipeCompensacao;

        public ProcessaAjustePremioService(
            IPipe<AjustePremioContext> pipeAjustePremio,
            IPipe<CompensacaoContext<EventoAjustePremio>> pipeCompensacao)
        {
            _pipeAjustePremio = pipeAjustePremio;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IParcelaAjustada> Execute(IParcelaAjustada premioAjustado)
        {
            var retorno = new AjustePremioContext(premioAjustado);

            await _pipeAjustePremio.Send(retorno);

            return premioAjustado;
        }

        public async Task Compensate(IParcelaAjustada message)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoAjustePremio>(message));
        }
    }
}
