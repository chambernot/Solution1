using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Service.Premio
{
    public class ProcessaInclusaoVgService: OrchestrationSubscribeService<IInclusaoCoberturaGrupal>
    {
        private readonly IPipe<InclusaoVgContext> _pipelineInclusaoVg;
        private readonly IPipe<CompensacaoContext<EventoInclusaoVg>> _pipeCompensacao;

        public ProcessaInclusaoVgService(
            IPipe<InclusaoVgContext> pipelineInclusaoVg,
            IPipe<CompensacaoContext<EventoInclusaoVg>> pipeCompensacao)
        {
            _pipelineInclusaoVg = pipelineInclusaoVg;
            _pipeCompensacao = pipeCompensacao;
        }

        public async Task<IInclusaoCoberturaGrupal> Execute(IInclusaoCoberturaGrupal message)
        {
            await _pipelineInclusaoVg.Send(new InclusaoVgContext(message));
            return message;
        }

        public async Task Compensate(IInclusaoCoberturaGrupal message)
        {
            await _pipeCompensacao.Send(new CompensacaoContext<EventoInclusaoVg>(message));
        }
    }
}
