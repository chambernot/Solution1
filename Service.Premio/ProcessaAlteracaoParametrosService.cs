using GreenPipes;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaAlteracaoParametrosService : OrchestrationSubscribeService<IProposta>
    {
        public IPipe<AlteracaoContext> _alteracaoPipe;
        private readonly IPipe<CompensacaoContext<EventoAlteracao>> _compensacaoPipe;

        public ProcessaAlteracaoParametrosService(
            IPipe<AlteracaoContext> alteracaoPipe, 
            IPipe<CompensacaoContext<EventoAlteracao>> compensacaoPipe)
        {
            _alteracaoPipe = alteracaoPipe;
            _compensacaoPipe = compensacaoPipe;
        }

        public async Task<IProposta> Execute(IProposta contrato)
        {
            await _alteracaoPipe.Send(new AlteracaoContext(contrato));
            return contrato;
        }

        public async Task Compensate(IProposta contrato)
        {
            await _compensacaoPipe.Send(new CompensacaoContext<EventoAlteracao>(contrato));
        }
    }
}
